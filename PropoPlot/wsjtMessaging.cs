using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using M0LTE.WsjtxUdpLib.Client;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Diagnostics;    //this is the debug class
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;


//***************************************
//ProgName: wstMessaging
//Date: April 2021
//Author: Ian Cook
//Purose: To decode the UDP strings from WSJT-x
//Comment: This is the first part of the program	
//************************************************

namespace PropoPlot
{
    public partial class MainWindow
    {
        public double[] avgs = new double[120];  //declare an array of elements that we use for the lagging signal strength

        // int EUavgsCnt = 0;
        //4 per minute
        //8 is able to hold 2 minutes worth


        public double[] FAavgs = new double[120];
        public double[] JAavgs = new double[120];
        public double[] EUavgs = new double[120];
        public double[] NAavgs = new double[120];
        public double[] OCavgs = new double[120];
        public double[] SAavgs = new double[120];
        public double[] AFavgs = new double[120];

        List<string> messages = new List<string>(); // all of the messages.  
        List<string> heats = new List<string>(); //just for the heat map

        public string prefix = "JTDX";
        public int avgsCounter = 0;
        double laggingAvg = 0;  //wtd avg
        int laggingCount = 0; //wtd avg
        double laggingTotal = 0.0; //wtd avg
        int laggingWindow = 5; //wtd avg
        int timerInterval = int.Parse(Properties.Settings.Default.PeriodTimer); //14;  //this is how often we should process the data (its seconds)
        bool laggingRound = false;
        int aDXAcount = 0;
        string OldCallSign = "";

        public bool plotToDxAtlas = false;
        public int QSOsThiInterval = 0;

        //public bool wsjtClientAbort = false;


        public string[,] Udppoint = new string[100, 6];  //101 rows of 3 columns Stores the data from the last decode run so we can write it to display
        //0 = callsign
        //1 = dBm
        //2 = maidenhead
        //3 = Latitude
        //4 = Longitude
        //5 = time

        int totalDecodes = 0; //a running tally of how many decodes we have done


        /// <summary>
        /// This runs the M0LTE dll in a infinite loop. Have no control over this once it starts
        /// It runs continously and in a seperate timing loop we extract the information we want from
        /// the list of strings and then empty the list
        /// </summary>
        /// <returns></returns>
        //private async Task wsjtmessages()  //should this be returning a Task cause void is bad?
        private void wsjtmessages()  //should this be returning a Task cause void is bad?
        {

            int UDPport = int.Parse(UDPportEntry.Text);

            //input parameters
            var client = new WsjtxClient((msg, from) =>
                      {
                                //sequence of statments here
                                string strmsg = msg.ToString();
                         // Debug.WriteLine(msg); //write to the immdiate window
                                udpStrings.Add(msg.ToString()); //collect the strings into a list
                            }, IPAddress.Parse("239.255.0.1"), multicast: true, debug: false, port: UDPport);


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

                int aFA = 0;  // used to track a FA coordinate
                int aSA = 0;
                int aNA = 0;
                int aOC = 0;
                int aAF = 0;
                int aEU = 0;
                int aJA = 0;
                int aDXA = 0; //for the callsign we are chasing
                int aDEA = 0;  // for my callsign  VK6DW

                string continent = "";
                int counter = 0;
                counter += counter;///???

                //bool isEmpty = false;
                //double averagedbm = 0;

                string cleandata = "";
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
                int counterFA = 0;

                //          double sumFAarray = 0;
                //          double averageFAarray = 0;

                foreach (var item in udpStrings)
                {
                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex("[ ]{2,}", options); //gets rid of the multiple spaces
                    cleandata = regex.Replace(item, " ");
                    string[] wrdmsg = cleandata.Split(' '); //split on the single space


                    if (wrdmsg[0] == "Status")
                        prefix = "WSJT-X";

                    if (wrdmsg[0] == "Decode")  //wsjt-x puts ou lots of data lines without Decodes. We want to ig nore them
                    {



                        if (wrdmsg.Length == 11)   //this is normal  CQ VK6DW OF88
                        {
                            ul.udptime = wrdmsg[1];
                            ul.udpdbm = wrdmsg[2];
                            ul.udpdt = wrdmsg[3];
                            ul.udphz = wrdmsg[4];
                            ul.udpqso1 = wrdmsg[6];
                            ul.udpqso2 = wrdmsg[7];
                            ul.udpqso3 = wrdmsg[8];
                            ul.udpqso4 = wrdmsg[9];//this is usually empty ""

                            if (ul.udpqso4.Length > 1)  //this is for CQ NA VK6DW OF88
                                ul.udpqso3 = ul.udpqso4; // puts the qso4 into qso3, could also just make ul.udpqso3="$$$$" and ignore this

                            // Debug.WriteLine($"route 1    3 = {ul.udpqso3} 4 = {ul.udpqso4}  String is {cleandata} ");
                        }
                        else if (wrdmsg.Length == 10)  //WQ6L YB6C NJ93  
                        {
                            ul.udptime = wrdmsg[1];
                            ul.udpdbm = wrdmsg[2];
                            ul.udpdt = wrdmsg[3];
                            ul.udphz = wrdmsg[4];
                            ul.udpqso1 = wrdmsg[6];
                            ul.udpqso2 = wrdmsg[7];
                            ul.udpqso3 = wrdmsg[8];
                            // Debug.WriteLine($"route 2   3 = {ul.udpqso3} 4 = {ul.udpqso4} String is {cleandata} ");
                        }
                        else if (wrdmsg.Length < 10 || wrdmsg.Length > 11) //this is very bad  something like CQ MISSISSIPPI OR anything
                        {
                            ul.udptime = wrdmsg[1];
                            ul.udpdbm = wrdmsg[2];
                            ul.udpdt = wrdmsg[3];
                            ul.udphz = wrdmsg[4];
                            ul.udpqso1 = wrdmsg[6];
                            ul.udpqso2 = wrdmsg[7];
                            ul.udpqso3 = "$$$$";  //anything strange ignore it  
                                                  //  Debug.WriteLine($"route 3    3 = {ul.udpqso3} 4 = {ul.udpqso4} String is {cleandata} ");
                        }

                        //ul.udpqso4 = wrdmsg[9];//

                        cd.pTime = ul.udptime;  //add the time to the record


                        if (ul.udpqso3.Length == 4  //ie if there is a valid length of 4 chars this is the filter to get rid
                            && (ul.udpqso3.Substring(0, 2) != "$$"  //of anything that is not a grid square.  There is only ever one instance
                            && ul.udpqso3.Substring(0, 2) != "73"   //for the record we are processing from the list
                            && ul.udpqso3.Substring(0, 2) != "RR"
                            && ul.udpqso3.Substring(0, 2) != "R-"
                            && ul.udpqso3.Substring(0, 2) != "R+"
                            && ul.udpqso3.Substring(0, 2) != "-0"
                            && ul.udpqso3.Substring(0, 2) != "-1"
                            && ul.udpqso3.Substring(0, 2) != "-2"
                            && ul.udpqso3.Substring(0, 2) != "+1"
                            && ul.udpqso3.Substring(0, 2) != "+2"
                            && ul.udpqso3.Substring(0, 4) != "LOTW"
                            && ul.udpqso3.Substring(0, 4) != "eQSL"
                            && ul.udpqso3.Substring(0, 4) != "SGNL" //as in Nice SIGNAL
                         ))
                        { //this fires for every string that has grid data

                            //lets try putting data into our matrix udppoint  //using counter (which is the number of decoded qsos with a grid square
                            Udppoint[counter, 0] = ul.udpqso2;
                            Udppoint[counter, 1] = ul.udpdbm;
                            Udppoint[counter, 2] = ul.udpqso3;

                            //if there is an issue with the string  - like we can get a callsign like WU7X if he calls CQDX WU7X DN17
                            // Debug.WriteLine($"Sending a GRID of  {ul.udpqso3}  to lat long conversion");
                            string latitude = Maiden2latitude(ul.udpqso3);
                            string longitude = Maiden2longitude(ul.udpqso3);

                            double dlatitude = double.Parse(latitude);
                            double dlongitude = double.Parse(longitude);


                            Udppoint[counter, 3] = latitude;
                            Udppoint[counter, 4] = longitude;
                            Udppoint[counter, 5] = ul.udptime;

                            //for a test of this filtering and should not exist
                            // in a prod varsion
                            //dlatitude = -37.9688;
                            //dlongitude = 140.0833;


                            // this subdivdes the dataload into areas on the globe and keeps a running tally of the dbms
                            //JA
                            if (dlatitude > double.Parse(tll.JALatMin.Text) && dlatitude < double.Parse(tll.JALatMax.Text) && dlongitude > double.Parse(tll.JALongMin.Text) && dlongitude < double.Parse(tll.JALongMax.Text))
                            //if (dlatitude > 30 && dlatitude < 46  && dlongitude > 130 && dlongitude <  146) 
                            {
                                totaldbmJA += double.Parse(ul.udpdbm);  //this is the running tally of the dbms
                                counterJA += 1;                         //this is the number of stations in the continent
                                aJA = 1;
                                continent = "JA";
                            }

                            //Europe
                            //if (dlatitude > 34 && dlatitude < 72  && dlongitude > -12 && dlongitude <  60) 
                            else if (dlatitude > double.Parse(tll.EULatMin.Text) && dlatitude < double.Parse(tll.EULatMax.Text) && dlongitude > double.Parse(tll.EULongMin.Text) && dlongitude < double.Parse(tll.EULongMax.Text))
                            {
                                totaldbmEU += double.Parse(ul.udpdbm);
                                counterEU += 1;
                                aEU = 1;
                                continent = "EU";
                            }

                            //NA
                            //if (dlatitude > 12 && dlatitude < 90 && dlongitude > -131 && dlongitude < -54)
                            else if (dlatitude > double.Parse(tll.NALatMin.Text) && dlatitude < double.Parse(tll.NALatMax.Text) && dlongitude > double.Parse(tll.NALongMin.Text) && dlongitude < double.Parse(tll.NALongMax.Text))
                            {
                                totaldbmNA += double.Parse(ul.udpdbm);
                                counterNA += 1;
                                aNA = 1;
                                continent = "NA";
                            }


                            //OC
                            //if (dlatitude > -54 && dlatitude < 28 && dlongitude > 112 && dlongitude < 126)
                            else if (dlatitude > double.Parse(tll.OCLatMin.Text) && dlatitude < double.Parse(tll.OCLatMax.Text) && dlongitude > double.Parse(tll.OCLongMin.Text) && dlongitude < double.Parse(tll.OCLongMax.Text))
                            {
                                totaldbmOC += double.Parse(ul.udpdbm);
                                counterOC += 1;
                                aOC = 1;
                                continent = "OC";
                            }


                            //AF
                            //if (dlatitude > -35 && dlatitude < 34 && dlongitude > -20 && dlongitude < 50)
                            else if (dlatitude > double.Parse(tll.AFLatMin.Text) && dlatitude < double.Parse(tll.AFLatMax.Text) && dlongitude > double.Parse(tll.AFLongMin.Text) && dlongitude < double.Parse(tll.AFLongMax.Text))
                            {
                                totaldbmAF += double.Parse(ul.udpdbm);
                                counterAF += 1;
                                aAF = 1;
                                continent = "AF";
                            }

                            //SA
                            //if (dlatitude > -60 && dlatitude < 12 && dlongitude > -90 && dlongitude < -32)
                            else if (dlatitude > double.Parse(tll.SALatMin.Text) && dlatitude < double.Parse(tll.SALatMax.Text) && dlongitude > double.Parse(tll.SALongMin.Text) && dlongitude < double.Parse(tll.SALongMax.Text))
                            {
                                totaldbmSA += double.Parse(ul.udpdbm);
                                counterSA += 1;
                                aSA = 1;
                                continent = "SA";
                            }
                            //FA Far East China india indonesia phillppines Japan
                            //                        if (dlatitude > -9 && dlatitude < 90 && dlongitude > 60 && dlongitude < 144)
                            else if (dlatitude > double.Parse(tll.FALatMin.Text) && dlatitude < double.Parse(tll.FALatMax.Text) && dlongitude > double.Parse(tll.FALongMin.Text) && dlongitude < double.Parse(tll.FALongMax.Text))
                            {
                                totaldbmFA += double.Parse(ul.udpdbm);
                                counterFA += 1;
                                aFA = 1;
                                continent = Properties.Settings.Default.UsrDefinedName;
                            }
                            else
                            {
                                continent = "None";

                            }


                            string cs = Properties.Settings.Default.theirCall;
                            
                           
                            //this is for the DX call set up in tools\options.  ITs a substring search
                            if ( (ul.udpqso2.Contains(cs) || ul.udpqso3.Contains(cs)) && cs != "")
                        {
                                aDXAcount += 1;
                                aDXA = 1;
                                dxCallSign.Text = ul.udpqso2;
                                dxLastHeardTime.Text = ul.udptime;
                                dxdbm.Text = ul.udpdbm;
                                dxCount.Text = aDXAcount.ToString();


                           }


                            // end of the classification by continent
                            //a question - what happens to all of the spots not within these continents?  Like Antarctica fer instance?  these shouldall be else if's ?????
                            //write the message *******************************************************
                            //20210524 Changed this from a textbox to a texblock, so I can heve individual ines of colour formatting
                            string message = "";
                            //String.Format("{0,-15} {1,-15} {2,-15} --> {3,-15} {4,-15} {5,-15} ");
                            //using string interpolation to format the string as well
                         
                            if (aDXA == 1)
                                message = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,+8}\tDX:+{ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont: {continent,-3}\r\n";   //this just a display of data
                            else
                               message = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,+8}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont: {continent,-3} \r\n";   //this just a display of data
                            //message = $"UTC:{ul.udptime}\tGrid:{ul.udpqso3}\tdBm:{ul.udpdbm}\tDX:{ul.udpqso2}\tLat:{latitude}\tLong:{longitude} \r\n";   //this just a display of data

                            string messForList = "";  //for what we save as a formatted list
                            string heatsForList = "";
                            heatsForList = $"{ul.udptime},{ul.udpdbm},{latitude,-10},{longitude,-10}";


                           

                            // this only about making the QSO box look nice.  No stats collected here
                            if (aDXA == 1)  // this is our special continent
                            {
                                System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
                                run.Foreground = System.Windows.Media.Brushes.Green;
                                //run.FontWeight = FontWeights.ExtraBold;
                                //run.FontSize = 10;
                                plotmessage.Inlines.Add(run);
                                aDXA = 0;
                                messForList = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont:DX";

                                //over write the message
                            }


                          else  if (aFA == 1)  // this is our special continent
                            {
                                System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
                                run.Foreground = System.Windows.Media.Brushes.Red;
                                //run.FontWeight = FontWeights.ExtraBold;
                               // run.FontSize = 10;
                                plotmessage.Inlines.Add(run);
                                aFA = 0;
                                messForList = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont:{Properties.Settings.Default.UsrDefinedName}";
                            } 
                           else if (aAF == 1)
                            {
                                System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
                                run.Foreground = System.Windows.Media.Brushes.Black;
                                //run.FontWeight = FontWeights.ExtraBold;
                                //run.FontSize = 10;
                                plotmessage.Inlines.Add(run);
                                aAF = 0;
                                messForList = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont:AF";
                            }
                            else if (aJA == 1)
                            {
                                System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
                                run.Foreground = System.Windows.Media.Brushes.Black;
                                plotmessage.Inlines.Add(run);
                                aJA = 0;
                                messForList = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont:JA";

                            }
                            else if (aSA == 1)
                            {
                                System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
                                run.Foreground = System.Windows.Media.Brushes.Black;
                                plotmessage.Inlines.Add(run);
                                aSA = 0;
                                messForList = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont:SA";
                            }
                            else if (aNA == 1)
                            {
                                System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
                                run.Foreground = System.Windows.Media.Brushes.Black;
                                plotmessage.Inlines.Add(run);
                                aNA = 0;
                                messForList = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont:NA";
                            }
                            else if (aEU == 1)
                            {
                                System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
                                run.Foreground = System.Windows.Media.Brushes.Black;
                                plotmessage.Inlines.Add(run);
                                aEU = 0;
                                messForList = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont:EU";
                            }
                            else if (aOC == 1)
                            {
                                System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
                                run.Foreground = System.Windows.Media.Brushes.Black;
                                plotmessage.Inlines.Add(run);
                                aOC = 0;
                                messForList = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont:OC";
                            }
                            else
                            {
                                System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
                                run.Foreground = System.Windows.Media.Brushes.DarkViolet;
                                plotmessage.Inlines.Add(run);
                                messForList = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10}\tCont:None";
                            }

                            counter++;  //this counter goes up by not 1

                            if (ul.udpdbm != "" || ul.udpdbm != null)
                            {
                                totaldbm += double.Parse(ul.udpdbm);
                            }


                            messages.Add(messForList);    // so lets start making a list of the messages
                            heats.Add(heatsForList);
                        }

                        loopCnt.Text = $"{counter.ToString()}";  //is the number of decodes with a grid square
                        QSOsThiInterval = counter;

                        ul.udpqso4 = ""; //reset just this one, all of the others get overwritten each time

                        totalDecodes += 1;

                        displayTotalDecodes.Text = totalDecodes.ToString();
                    } // end of if wrdmsg[0]="Decode"
                }//end of foreach loop
                             plotmessage.Inlines.Add ("----------------------------------------------------- -------------------------------------------------\n");

             

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
 
                if (totaldbmFA / counterFA > -40)
                {
                    runningFAContinentalAverage(totaldbmFA / counterFA, FAprog, FAdbm, counterFA, FAavgs);
                }//end of if
 

                if (totaldbmJA / counterJA > -40)
                {
                    runningJAContinentalAverage(totaldbmJA / counterJA, JAprog, JAdbm, counterJA, JAavgs);
                }//end of if

                if (totaldbmNA / counterNA > -40)
                {
                    runningNAContinentalAverage(totaldbmNA / counterNA, NAprog, NAdbm, counterNA, NAavgs);
                }//end of if

                if (totaldbmOC / counterOC > -40) //-------------------------------
                {
                    runningOCContinentalAverage(totaldbmOC / counterOC, OCprog, OCdbm, counterOC, OCavgs);
                }//end of if

                if (totaldbmAF / counterAF > -40) //---------------------------------
                {
                    runningAFContinentalAverage(totaldbmAF / counterAF, AFprog, AFdbm, counterAF, AFavgs);
                }//end of if

                if (totaldbmSA / counterSA > -40) //-----------------------------------
                {
                    runningSAContinentalAverage(totaldbmSA / counterSA, SAprog, SAdbm, counterSA, SAavgs);
                }//end 


                //now we save the last lot of data to our continent list
                // the headers are set in MainWindow.xml.cs in their respective methods

                // at this point all of the arrays have these nasty -30 values in them and thay mostly signify that nobody was transmitting, not the level of reception
                // these -30 badly affect the averages and further processing.  Instead of doing that everey where, lets gid rid of them now befor we put the data into the string.
                // trouble is thats a bit hard.  The data only exist in the list at the moment

                //continentAVGList.Add($"WTDavg,{cd.pTime},{cd.pEUdbm},{cdAvg.pEUdbm},{cdAvg.pEUnumber},{cd.pJAdbm},{cdAvg.pJAdbm},{cdAvg.pJAnumber},{cd.pNAdbm},{cdAvg.pNAdbm},{cdAvg.pNAnumber},{cd.pOCdbm},{cdAvg.pOCdbm},{cdAvg.pOCnumber},{cd.pAFdbm},{cdAvg.pAFdbm},{cdAvg.pAFnumber},{cd.pSAdbm},{cdAvg.pSAdbm},{cdAvg.pSAnumber},{cd.pFAdbm},{cdAvg.pFAdbm},{cdAvg.pFAnumber}");
                continentAVGList.Add($"{prefix},{cd.pTime},{cd.pEUdbm},{cdAvg.pEUdbm},{cdAvg.pEUnumber},{cd.pJAdbm},{cdAvg.pJAdbm},{cdAvg.pJAnumber},{cd.pNAdbm},{cdAvg.pNAdbm},{cdAvg.pNAnumber},{cd.pOCdbm},{cdAvg.pOCdbm},{cdAvg.pOCnumber},{cd.pAFdbm},{cdAvg.pAFdbm},{cdAvg.pAFnumber},{cd.pSAdbm},{cdAvg.pSAdbm},{cdAvg.pSAnumber},{cd.pFAdbm},{cdAvg.pFAdbm},{cdAvg.pFAnumber}");



                //it would be good to just remove the first 8000 chars and then keep it like that ******************************************
                //this is just to keep the string length in reasonable size, otherwise we use a lot of memory.

                int tl = int.Parse(Properties.Settings.Default.truncateValue);
                
                int tl2 = tl * 2;
                int end = plotmessage.Text.Length;
                // int start = end / 2;  // half of it
                int start = Convert.ToInt32(tl * 0.7) ;
                
                int at=0;
              //  int tCount=0;
                //  if (plotmessage.Text.Length > tl2) //about 10 minutes worth on a busy band

               // Debug.WriteLine($"tl={tl}  start = {start} at = {at}  CurrentLength={end}");

                if (plotmessage.Text.Length > tl) //about 10 minutes worth on a busy band
                {

                    at = plotmessage.Text.IndexOf("----", start);
              //  Debug.WriteLine($"INSIDE tl={tl}  start = {start} at = {at}  CurrentLength={end}");

                     string pm = plotmessage.Text.Substring(at); //grab from ---- to the end of the text and toss the rest

                    //colourQSO(messages);
                    plotmessage.Text = pm;  //write all back to the display
                 }

                //now lets workout the weightd average - its a lagging indicator of signal strength
                if (laggingCount > laggingWindow || laggingRound == true) //lagging window is set elsewhere
                {
                    laggingRound = true;
                    laggingTotal = sumarray();  //thats sum array
                    laggingAvg = laggingTotal / laggingWindow + 1;
                    runningAvgDbm.Text = Math.Round(laggingAvg, 1).ToString();  //show the current weighted average

                    if (laggingCount > laggingWindow)
                    {
                        laggingCount = 0; //go to set this back to 0 so we use the same array slots
                    }
                }

            }//try
            catch (Exception ex)
            {

                // frmMessageDialog md = new frmMessageDialog();
                // md.messageBoxUpper.Text = $"Error in GetQsosFromList() {ul.udphz} {ul.udpdbm}{ul.udpqso1}{ul.udpqso2} {ul.udpqso3} {ul.udpqso3}";
                // md.messageBoxLower.Text = $"{ex}";
                //md.Show();
            }

            //now some code to plot to dxAtlas
            //

            if (plotToDxAtlas == true)
                DXAtlasplotPoints();

            setTimerBarColour(laggingAvg); //this is our progress bar

        }//end




        private double sumarray()  //sum the lagging array 
        {
            double total = 0;

            for (int i = 0; i < 8; i++)  //DANGER if the array size is changed, this must be too
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


            string latst1 = "", latst2 = "", latst3 = "";
            char latchr1;

            double lat1 = 0, lat2 = 0, lat3 = 0;
            int latint1 = 0, latint2 = 0, latint3 = 0;
            double latdub3 = 0.0;
            if (grid == null)
                grid = "JJ00";

            if (grid.Length == 4)
                grid += "mm";  //concatenate mm on the end - puts us in the middle of the gridquare

            char[] charArray = grid.ToCharArray();   //grid.Substring(1, 1);
            latint1 = (charArray[1] - 65) * 10;

            latst2 = grid.Substring(3, 1);
            latint2 = int.Parse(latst2);

            double a1 = charArray[5] - 97;
            double a2 = a1 / 24;
            double a3 = a2 + 1;
            double a4 = a3 / 48;
            double a5 = a4 - 90;
            latdub3 = a5; //((charArray[5] - 97) / 24) + 1 / 48 - 90;
            double Latitude = latint1 + latint2 + latdub3;

            return Math.Round(Latitude, 4).ToString();//latint1 + latint2 + latint3;
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

            latst2 = grid.Substring(2, 1);
            latint2 = int.Parse(latst2) * 2;

            double a1 = charArray[4] - 97;
            double a2 = a1 / 12;
            double a3 = a2 + 1;
            double a4 = a3 / 24;
            double a5 = a4 - 180;
            latdub3 = a5;
            double Longitude = latint1 + latint2 + latdub3;

            return Math.Round(Longitude, 4).ToString();

        }


        /// <summary>
        /// Remove the -30's from the array.  Its crap data
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        //private double[] smoothArray(double[] arr)
        //{


        //    for (int i = 0; i < arr.Length - 1; i++)
        //    {
        //        double firstVal = arr[i];
        //        double nextVal = arr[i + 1];
        //        if (nextVal == -30)
        //            arr[i + 1] = firstVal;
        //    }
        //    return arr;
        //}





    } //end of class
}//end of namespace
