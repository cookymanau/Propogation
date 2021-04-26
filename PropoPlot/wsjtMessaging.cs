﻿using System;
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
        
       // int EUavgsCnt = 0;

        public double[] FAavgs = new double[8];
        public double[] JAavgs = new double[8];
        public double[] EUavgs = new double[8];
        public double[] NAavgs = new double[8];
        public double[] OCavgs = new double[8];
        public double[] SAavgs = new double[8];
        public double[] AFavgs = new double[8];


        public int avgsCounter = 0;
        double laggingAvg = 0;  //wtd avg
        int laggingCount = 0; //wtd avg
        double laggingTotal = 0.0; //wtd avg
        int laggingWindow = 5; //wtd avg
        int timerInterval = 14;  //this is how often we should process the data (its seconds)
        bool laggingRound = false;
        
        public bool plotToDxAtlas = false;
        public int QSOsThiInterval = 0;

        //public bool wsjtClientAbort = false;


        public string[,] Udppoint = new string[100,6];  //101 rows of 3 columns Stores the data from the last decode run so we can write it to display
        //0 = callsign
        //1 = dBm
        //2 = maidenhead
        //3 = Latitude
        //4 = Longitude
        //5 = time
       // public string[,] UdppointEU = new string[100,6];  //101 rows of 3 columns Stores the data from the last decode run so we can write it to display
       // public string[,] UdppointJA = new string[100,6];  //101 rows of 3 columns Stores the data from the last decode run so we can write it to display
        public string[,] UdppointOC = new string[100,6];  //101 rows of 3 columns Stores the data from the last decode run so we can write it to display
        public string[,] UdppointNA = new string[100,6];  //101 rows of 3 columns Stores the data from the last decode run so we can write it to display
        public string[,] UdppointSA = new string[100,6];  //101 rows of 3 columns Stores the data from the last decode run so we can write it to display
        public string[,] UdppointAF = new string[100,6];  //101 rows of 3 columns Stores the data from the last decode run so we can write it to display
        



        /// <summary>
        /// This runs the M0LTE dll in a infinite loop. Have no control over this once it starts
        /// It runs continously and in a seperate timing loop we extract the information we want from
        /// the list of strings and then empty the list
        /// </summary>
        /// <returns></returns>
        //private async Task wsjtmessages()  //should this be returning a Task cause void is bad?
        private void wsjtmessages()  //should this be returning a Task cause void is bad?
        {
            

      //      if (wsjtClientAbort == false) {

     //           wsjtClientAbort = true;

                int UDPport = int.Parse(UDPportEntry.Text);

                //input parameters
                  var client = new WsjtxClient((msg, from) =>
                            {
                        //sequence of statments here
                        string strmsg = msg.ToString();
                                Debug.WriteLine(msg); //write to the immdiate window
                        udpStrings.Add(msg.ToString()); //collect the strings into a list
                    }, IPAddress.Parse("239.255.0.1"), multicast: true, debug: true, port: UDPport);

       //     }
       //     else
       //     {
       //       //  btnUDPStart.Content = "Start Capture";
       //         wsjtClientAbort = true;
       //     }

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

            double totaldbmJA = 0; //these are for the average for a single period  - add all of the dBms together
                int counterJA = 0; // then divide by the number of reports
            double totaldbmEU = 0;
                int counterEU = 0;
            double totaldbmNA = 0;
                int counterNA = 0;
            double totaldbmSA = 0;
                int counterSA = 0;
            double totaldbmAF = 0;
                int counterAF = 0;
            double totaldbmOC = 0;
                int counterOC = 0;
           
            double totaldbmFA = 0;
            double sumFAarray = 0;
                double averageFAarray = 0;
                int counterFA = 0;

           

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

                    cd.pTime = ul.udptime;  //add the time to the record

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
                    { //this fires for every string that has grid data

                        //lets try putting data into our matrix udppoint  //using counter (which is the number of decoded qsos with a grid square
                        Udppoint[counter, 0] = ul.udpqso2;
                        Udppoint[counter, 1] = ul.udpdbm;
                        Udppoint[counter, 2] = ul.udpqso3;
                        string latitude = Maiden2latitude(ul.udpqso3);
                        string longitude = Maiden2longitude(ul.udpqso3);

                        double dlatitude = double.Parse(latitude);
                        double dlongitude = double.Parse(longitude);


                        Udppoint[counter, 3] = latitude;
                        Udppoint[counter, 4] = longitude;
                        Udppoint[counter, 5] = ul.udptime;

                        // this subdivdes the dataload into areas on the globe and keeps a running tally of the dbms
                        //JA
                        if (dlatitude > double.Parse(tll.JALatMin.Text) && dlatitude < double.Parse(tll.JALatMax.Text) && dlongitude > double.Parse(tll.JALongMin.Text) && dlongitude < double.Parse(tll.JALongMax.Text)) 
                        //if (dlatitude > 30 && dlatitude < 46  && dlongitude > 130 && dlongitude <  146) 
                        {
                            totaldbmJA += double.Parse(ul.udpdbm);  //this is the running tally of the dbms
                            counterJA += 1;                         //this is the number of stations in the continent
                        }

                        //Europe
                        //if (dlatitude > 34 && dlatitude < 72  && dlongitude > -12 && dlongitude <  60) 
                        if (dlatitude > double.Parse(tll.EULatMin.Text) && dlatitude < double.Parse(tll.EULatMax.Text) && dlongitude > double.Parse(tll.EULongMin.Text) && dlongitude < double.Parse(tll.EULongMax.Text)) 
                        {
                            totaldbmEU += double.Parse(ul.udpdbm);
                            counterEU += 1;
                        }

                        //NA
                        //if (dlatitude > 12 && dlatitude < 90 && dlongitude > -131 && dlongitude < -54)
                        if (dlatitude > double.Parse(tll.NALatMin.Text) && dlatitude < double.Parse(tll.NALatMax.Text) && dlongitude > double.Parse(tll.NALongMin.Text) && dlongitude < double.Parse(tll.NALongMax.Text))
                        {
                            totaldbmNA += double.Parse(ul.udpdbm);
                            counterNA += 1;
                        }


                        //OC
                        //if (dlatitude > -54 && dlatitude < 28 && dlongitude > 112 && dlongitude < 126)
                        if (dlatitude > double.Parse(tll.OCLatMin.Text) && dlatitude < double.Parse(tll.OCLatMax.Text) && dlongitude > double.Parse(tll.OCLongMin.Text) && dlongitude < double.Parse(tll.OCLongMax.Text))
                        {
                            totaldbmOC += double.Parse(ul.udpdbm);
                            counterOC += 1;
                        }


                        //AF
                        //if (dlatitude > -35 && dlatitude < 34 && dlongitude > -20 && dlongitude < 50)
                        if (dlatitude > double.Parse(tll.AFLatMin.Text) && dlatitude < double.Parse(tll.AFLatMax.Text) && dlongitude > double.Parse(tll.AFLongMin.Text) && dlongitude < double.Parse(tll.AFLongMax.Text))
                        {
                            totaldbmAF += double.Parse(ul.udpdbm);
                            counterAF += 1;
                        }

                        //SA
                        //if (dlatitude > -60 && dlatitude < 12 && dlongitude > -90 && dlongitude < -32)
                        if (dlatitude > double.Parse(tll.SALatMin.Text) && dlatitude < double.Parse(tll.SALatMax.Text) && dlongitude > double.Parse(tll.SALongMin.Text) && dlongitude < double.Parse(tll.SALongMax.Text))
                        {
                            totaldbmSA += double.Parse(ul.udpdbm);
                            counterSA += 1;
                        }
                        //FA Far East China india indonesia phillppines Japan
//                        if (dlatitude > -9 && dlatitude < 90 && dlongitude > 60 && dlongitude < 144)
                        if (dlatitude > double.Parse(tll.FALatMin.Text) && dlatitude < double.Parse(tll.FALatMax.Text) && dlongitude > double.Parse(tll.FALongMin.Text) && dlongitude < double.Parse(tll.FALongMax.Text))
                        {
                            totaldbmFA += double.Parse(ul.udpdbm);
                            counterFA += 1;
                        }



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


                //thes call a series of functions to calculate the weightd average of the dBm
                if (totaldbmEU / counterEU > -40)
                {
                    runningEUContinentalAverage(totaldbmEU / counterEU, EUprog, EUdbm, counterEU, EUavgs);
                }
                else if (totaldbmEU == 0 && counterEU == 0)
                    runningEUContinentalAverage(-30, EUprog, EUdbm, 0, EUavgs);
                else
                    EUdbmCount.Text = "0";


                if (totaldbmFA / counterFA > -40)
                {
                    runningFAContinentalAverage(totaldbmFA / counterFA, FAprog, FAdbm, counterFA, FAavgs);
                }//end of if
                else if(totaldbmFA == 0 && counterFA == 0)
                    runningFAContinentalAverage(-30, FAprog, FAdbm, 0, FAavgs);
                else 
                    FAdbmCount.Text = "0";


                if (totaldbmJA / counterJA > -40)
                {
                    runningJAContinentalAverage(totaldbmJA / counterJA, JAprog, JAdbm, counterJA, JAavgs);
                }//end of if
                else if (totaldbmJA == 0 && counterJA == 0)
                    runningJAContinentalAverage(-30, JAprog, JAdbm, 0, JAavgs);
                else
                    JAdbmCount.Text = "0";



                if (totaldbmNA / counterNA > -40)
                {
                    runningNAContinentalAverage(totaldbmNA / counterNA, NAprog, NAdbm, counterNA, NAavgs);
                }//end of if
                else if (totaldbmNA == 0 && counterNA == 0)
                    runningNAContinentalAverage(-30, NAprog, NAdbm, 0, NAavgs);
                else
                    NAdbmCount.Text = "0";



                if (totaldbmOC / counterOC > -40)
                {
                    runningOCContinentalAverage(totaldbmOC / counterOC, OCprog, OCdbm, counterOC, OCavgs);
                }//end of if
                else if (totaldbmOC == 0 && counterOC == 0)
                    runningOCContinentalAverage(-30, OCprog, OCdbm, 0, OCavgs);
                else
                    OCdbmCount.Text = "0";


                if (totaldbmAF / counterAF > -40)
                {
                    runningAFContinentalAverage(totaldbmAF / counterAF, AFprog, AFdbm, counterAF, AFavgs);
                }//end of if
                else if (totaldbmAF == 0 && counterAF == 0)
                    runningAFContinentalAverage(-30, AFprog, AFdbm, 0, AFavgs);
                else
                    AFdbmCount.Text = "0";


                if (totaldbmSA / counterSA > -40)
                {
                    runningSAContinentalAverage(totaldbmSA / counterSA, SAprog, SAdbm, counterSA, SAavgs);
                }//end 
                else if (totaldbmSA == 0 && counterSA == 0)
                    runningSAContinentalAverage(-30, SAprog, SAdbm, 0, SAavgs);
                else
                    SAdbmCount.Text = "0";

                //now we save the last lot of data to our continent list
                continentList.Add($"Kenwood 1,{cd.pTime},{cd.pEUdbm},{cd.pJAdbm},{cd.pNAdbm},{cd.pOCdbm},{cd.pAFdbm},{cd.pSAdbm},{cd.pFAdbm} ,{cd.pEUnumber},{cd.pJAnumber},{cd.pNAnumber},{cd.pOCnumber},{cd.pAFnumber},{cd.pSAnumber},{cd.pFAnumber}");
                continentListDC.Add($"Kenwood 1,{cd.pTime},{cd.pEUdbm},{cd.pEUnumber},{cd.pJAdbm},{cd.pJAnumber},{cd.pNAdbm},{cd.pNAnumber},{cd.pOCdbm},{cd.pOCnumber},{cd.pAFdbm},{cd.pAFnumber},{cd.pSAdbm},{cd.pSAnumber},{cd.pFAdbm},{cd.pFAnumber} ");

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
