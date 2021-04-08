using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using M0LTE.WsjtxUdpLib.Client;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PropoPlot
{
    public partial  class  MainWindow
    {

        public bool stopLoop = true;

        /// <summary>
        /// This runs the M0LTE dll in a infinite loop
        /// </summary>
        /// <returns></returns>
        //private async Task wsjtmessages()  //should this be returning a Task cause void is bad?
        private void wsjtmessages()  //should this be returning a Task cause void is bad?
        {
   //      int counter = 0;
   //      while (stopLoop == true)
   //      {
   //          counter++;
   //          //loopCnt.Text = counter.ToString();
   //     //     await Task.Run(() =>
   //         // {
   //              //input parameters
                    var client = new WsjtxClient((msg, from) =>
                    {
                        //sequence of statments here
                        string strmsg = msg.ToString();
                        Debug.WriteLine(msg); //write to the immdiate window
                        udpStrings.Add(msg.ToString());
                    }, IPAddress.Parse("239.255.0.1"), multicast: true, debug: true);
             //   });//this is the end of the M0LTE stuff.

         //       try
         //       {
         //       string UdpLoad = "";
         //       foreach (var item in udpStrings)
         //       {
         //          // GetQsos(item);
         //           UdpLoad += item + "\r\n";  //makes a big long string
         //       }
         //       message.Text = UdpLoad;
         //       listCnt.Text = "List" + udpStrings.Count.ToString();

         //       }
         //       catch (Exception)
         //       {
                    //do nothing here, we are ignoring bad decodes
         //       }

               // Task.Delay(2500);
              //  GetQsosDXAtlas();
     //       }

        }//end


        /// <summary>
        /// where to put the call to this
        /// </summary>
        /// <param name="message"></param>
        //private void GetQsos(string message)  //this should be asyn task as well maybe
        //currently just processes the UDP list and adds to out UdpDataload class
        private void GetQsosDXAtlas()  //this should be asyn task as well maybe
        {
            try
            {

            int counter = 0;
            counter += counter;
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
                        ul.udpqso3 = "$$";

                    if (ul.udpqso3.Length == 4
                        && (ul.udpqso3.Substring(0, 2) != "$$"
                        && ul.udpqso3.Substring(0, 2) != "73"
                        && ul.udpqso3.Substring(0, 2) != "RR"
                        && ul.udpqso3.Substring(0, 2) != "R-"
                        && ul.udpqso3.Substring(0, 2) != "R+"
                        && ul.udpqso3.Substring(0, 2) != "-0"
                        && ul.udpqso3.Substring(0, 2) != "-1"
                        && ul.udpqso3.Substring(0, 2) != "-2"
                        && ul.udpqso3.Substring(0, 2) != "+1"
                        && ul.udpqso3.Substring(0, 2) != "+2"
                     ))
                    {
                        plotmessage.Text += $"Time is:{ul.udptime}  Grid {ul.udpqso3} Strength {ul.udpdbm} \r\n";
                    counter++;
                        totaldbm += double.Parse(ul.udpdbm);
                    }
                    loopCnt.Text = counter.ToString();
                }

                avgdbm.Text = (totaldbm / counter).ToString();
              //  udpStrings.Clear();  //clear because we have what we need from the udp list
            }
            catch (Exception)
            {


            }

            //now some code to plot to dxAtlas
            //
            //  DXAtlasplotPoints();
            //and some code to empty ul
            //ul.Dispose =true;
        }

















    } //end of class
}//end of namespace
