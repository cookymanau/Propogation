using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using M0LTE.WsjtxUdpLib.Client;
using System.Diagnostics;
using System.Text.RegularExpressions;

 //***************************************
     //ProgName: wstMessaging
     //Date: April 2021
     //Author: Ian Cook
     //Purose: To decode the UDP strings from WSJT-x
     //Comment:				
     //************************************************

namespace PropoPlot
{
    public partial  class  MainWindow
    {
        public double[] avgs = new double[8] ;  //declare an array of elements that we use for the lagging signal strength
        public int avgsCounter = 0;
        double laggingAvg = 0;  //wtd avg
        int laggingCount = 0; //wtd avg
        double laggingTotal = 0.0; //wtd avg
        int laggingWindow = 5; //wtd avg
        int timerInterval = 14;  //this is how often we should process the data (its seconds)
        bool laggingRound = false;
        
        public bool plotToDxAtlas = false;
        public int QSOsThiInterval = 0;

        

        public string[,] Udppoint = new string[100,6];  //101 rows of 3 columns Stores the data from the last decode run so we can write it to display
        //0 = callsign
        //1 = dBm
        //2 = maidenhead
        //3 = Latitude
        //4 = Longitude
        //5 = time
        

        /// <summary>
        /// This runs the M0LTE dll in a infinite loop. Have no control over this once it starts
        /// It runs continously and in a seperate timing loop we extract the information we want from
        /// the list of strings and then empty the list
        /// </summary>
        /// <returns></returns>
        //private async Task wsjtmessages()  //should this be returning a Task cause void is bad?
        private void wsjtmessages()  //should this be returning a Task cause void is bad?
        {

            int UDPport = int.Parse( UDPportEntry.Text) ;

        //input parameters
        var client = new WsjtxClient((msg, from) =>
                    {
                        //sequence of statments here
                        string strmsg = msg.ToString();
                        Debug.WriteLine(msg); //write to the immdiate window
                        udpStrings.Add(msg.ToString()); //collect the strings into a list
                    }, IPAddress.Parse("239.255.0.1"), multicast: true, debug: true, port:UDPport);
        }//end


        /// <summary>
        /// where to put the call to this
        /// </summary>
        /// <param name="message"></param>
        //private void GetQsos(string message)  //this should be asyn task as well maybe
        //currently just processes the UDP list and adds to out UdpDataload class
        private void GetQsosFromList()  //this should be asyn task as well maybe
        {
            try
            {


           int counter = 0;
           counter +=  counter;///???
            
            bool isEmpty = false;
            string cleandata = "";
            double averagedbm = 0;
            double totaldbm = 0;


                foreach (var item in udpStrings)
                {
                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex("[ ]{2,}", options);
                    cleandata = regex.Replace(item, " ");
                    string[] wrdmsg = cleandata.Split(' ');
                    ul.udptime = wrdmsg[1];
                    ul.udpdbm = wrdmsg[2];
                    ul.udpdt = wrdmsg[3];
                    ul.udphz = wrdmsg[4];
                    ul.udpqso1 = wrdmsg[6];
                    ul.udpqso2 = wrdmsg[7];
                    ul.udpqso3 = wrdmsg[8];

                    isEmpty = String.IsNullOrWhiteSpace(ul.udpqso3);
                    int qso3Length;

                    if (!isEmpty)
                        qso3Length = ul.udpqso3.Length;

                    if (!isEmpty && ul.udpqso3.Length > 1)
                        ul.udpqso3 = wrdmsg[8];
                    else
                        ul.udpqso3 = "$$";  //stop a exception 

                    if (ul.udpqso3.Length == 4  //ie if there is a valid length of 4 chars this is the filter to get rid
                        && (ul.udpqso3.Substring(0, 2) != "$$"  //of anything that is not a grid square.  Ther is only ever one instance
                        && ul.udpqso3.Substring(0, 2) != "73"   //for the record we are processing from the list
                        && ul.udpqso3.Substring(0, 2) != "RR"
                        && ul.udpqso3.Substring(0, 2) != "R-"
                        && ul.udpqso3.Substring(0, 2) != "R+"
                        && ul.udpqso3.Substring(0, 2) != "-0"
                        && ul.udpqso3.Substring(0, 2) != "-1"
                        && ul.udpqso3.Substring(0, 2) != "-2"
                        && ul.udpqso3.Substring(0, 2) != "+1"
                        && ul.udpqso3.Substring(0, 2) != "+2"
                     ))
                    { //this fires for every string that has grtid data

                        //lets try putting data into our matrix udppoint  //using counter (which is the number of decoded qsos with a grid square
                        Udppoint[counter, 0] = ul.udpqso2;
                        Udppoint[counter, 1] = ul.udpdbm;
                        Udppoint[counter, 2] = ul.udpqso3;
                        string latitude = Maiden2latitude(ul.udpqso3);
                        string longitude = Maiden2longitude(ul.udpqso3);

                        Udppoint[counter, 3] = latitude;
                        Udppoint[counter, 4] = longitude;
                        Udppoint[counter, 5] = ul.udptime;

                        plotmessage.Text += $"UTC:{ul.udptime} Grid:{ul.udpqso3} dBm:{ul.udpdbm} DX:{ul.udpqso2} Long:{longitude} Lat:{latitude} \r\n";   //this just a display of data
                        counter++;  //this counter goes up by not 1
                        if (ul.udpdbm != "" || ul.udpdbm != null)
                        {
                            totaldbm += double.Parse(ul.udpdbm);
                        }

                    }

                    loopCnt.Text = $"{counter.ToString()}";  //is the number of decodes with a grid square
                    QSOsThiInterval = counter;

                }//end of foreach loop
                        plotmessage.Text += "--------------------------------- -----------------------------\n";

                // we count all of the good grids only - ie where there is at least one grid
                if (counter > 0)
                {
                    avgdbm.Text = Math.Round(totaldbm / counter, 0).ToString();  //thi is the average for THIS time period
                    avgs[laggingCount] = totaldbm / counter; //shove the avgdbm data into an array. It doesnt matter which slot really (I dont think)
                    laggingCount += 1;
                    
                }

                //lets display the contents of the array

              //  for (int i = 0; i < counter; i++)
               // {
              //      plotmessage.Text += $"Time:{Udppoint[i, 5]} Grid: {Udppoint[i, 2]} dBm: {Udppoint[i, 1]} DX: {Udppoint[1, 0]} Long: {Udppoint[i, 4]} Lat:{Udppoint[i, 3]} \r\n";   //this just a display of data
              //  }

                sumChr.Text = plotmessage.Text.Length.ToString();
                //it would be good to just remove the first 8000 chars and then keep it like that

                if (plotmessage.Text.Length > 9000) 
                {
                    string pm = plotmessage.Text.Substring(plotmessage.Text.Length - 2000,2000);
                    
                    plotmessage.Text = "Truncated.." + pm;

                }

                //now lets workout the weightd average - its a lagging indicator of signal strength
                if (laggingCount > laggingWindow  || laggingRound == true) //lagging window is set elsewhere
                {
                    laggingRound = true;
                    laggingTotal = sumarray();  //thats sum array
                    laggingAvg = laggingTotal / laggingWindow +1 ;
                    runningAvgDbm.Text= Math.Round(laggingAvg,1).ToString();  //show the current weighted average

                    if (laggingCount > laggingWindow )  
                    {
                        laggingCount = 0; //go to set this back to 0 so we use the same array slots
                    }
                }
            }
            catch (Exception)
            {
            }

            //now some code to plot to dxAtlas
            //
            
            if (plotToDxAtlas == true)
               DXAtlasplotPoints();
            setTimerBarColour(laggingAvg); //this is our progress bar
 
        }


        private double sumarray()  //sum the lagging array 
        {
            double total=0;

            for (int i=0;i<8;i++)  //DANGER if the array size is changed, this must be too
            {
                total += avgs[i];
            }
            return total;
        }

        private void  setTimerBarColour(double value) {



            if (value > -40)
                timerBar.Foreground = System.Windows.Media.Brushes.LightYellow;

            if (value > -20)
                timerBar.Foreground = System.Windows.Media.Brushes.Aqua;

            if (value > -10.1)
                timerBar.Foreground = System.Windows.Media.Brushes.Blue;

//            if (value > -5)
//                timerBar.Foreground = System.Windows.Media.Brushes.IndianRed;

            if (value == 0 )
                timerBar.Foreground = System.Windows.Media.Brushes.LightGray;

            if (value > 0)
                timerBar.Foreground = System.Windows.Media.Brushes.Red;
        
        }



        private string Maiden2latitude(string grid)  //this is from loginfo, translated from VB
        {
            //0 1 2 3 4 5
            //O F 8 7 a a
            //O8 is longitude
            //F7 is latitude
            // =(CODE(MID(A1,2,1))-65)*10 + VALUE(MID(A1,4,1)) + (CODE(MID(A1,6,1))-97)/24 + 1/48 - 90 works in excel
            //for latitude we want 1, 3 and 5


            string latst1="", latst2="", latst3 = "";
            char latchr1;
            
            double lat1=0, lat2=0, lat3=0;
            int latint1 = 0 , latint2=0 , latint3=0;
            double latdub3 = 0.0;
            if (grid == null)
                grid = "JJ00";

            if (grid.Length == 4)
                grid += "mm";  //concatenate mm on the end - puts us in the middle of the gridquare

            char[] charArray = grid.ToCharArray();   //grid.Substring(1, 1);
            latint1 = (charArray[1] - 65) * 10;

            latst2 =    grid.Substring(3,1);
            latint2 = int.Parse(latst2);

            double a1 = charArray[5] - 97;
            double a2 = a1 / 24;
            double a3 = a2 +1;
            double a4 = a3 / 48;
            double a5 = a4 - 90;
            latdub3 = a5; //((charArray[5] - 97) / 24) + 1 / 48 - 90;
            double Latitude = latint1 + latint2 + latdub3;

            return Math.Round(Latitude,4).ToString();//latint1 + latint2 + latint3;
        } //end



        /// <summary>
        /// this is more or less the VB version of the conversion
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string Maiden2longitude(string grid)  //this is from loginfo, translated from VB
        {
            //          ' function to convert Maidenhead into a lat long - stolen from www.perlmonks.org
            //          'copied from https://ham.stackexchange.com/questions/6114/convert-maidenhead-to-lat-long-in-excel
            //          '=(CODE(MID(A1,1,1))-65)*20 + VALUE(MID(A1,3,1))*2 + (CODE(MID(A1,5,1))-97)/12 + 1/24 - 180

            //0 1 2 3 4 5
            //O F 8 7 a b
            //O8a is longitude
            //F7b is latitude
            // for longitude we want 0, 2 and 4

            string latst1 = "", latst2 = "", latst3 = "";
            char latchr1;

            double lat1 = 0, lat2 = 0, lat3 = 0;
            int latint1 = 0, latint2 = 0, latint3 = 0;
            double latdub3 = 0.0;
            if (grid == null)
                grid = "JJ00aa";

            if (grid.Length == 4)
                grid += "mm";  //concatenate mm on the end - puts us in the middle of the gridquare

            char[] charArray = grid.ToCharArray();   //grid.Substring(1, 1);
            latint1 = (charArray[0] - 65) * 20;

            latst2 = grid.Substring(2, 1) ;
            latint2 = int.Parse(latst2) * 2;

            double a1 = charArray[4] - 97;
            double a2 = a1 / 12;
            double a3 = a2 + 1;
            double a4 = a3 / 24;
            double a5 = a4 - 180;
            latdub3 = a5; 
            double Longitude = latint1 + latint2 + latdub3;

            return Math.Round(Longitude,4).ToString();

        }







    } //end of class
}//end of namespace
