using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using M0LTE.WsjtxUdpLib.Client;
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using Serilog;


namespace PropoPlot
{
    //this a helper class for mainwindow
    //it has the main window file interaction methods
    public partial class MainWindow
    {

        private void saveAvgCont_Click(object sender, RoutedEventArgs e)
        {
            //Log.Logger = new LoggerConfiguration().WriteTo.File(@"c:\users\cooky\Documents\PropaPlot.log").CreateLogger();
            EventLogger.WriteLine("Save File DBm");


            //you need to have using Microsoft.Win32; up top.  No dragging a toolbox item onto the form
            //using System.IO; is for SttreamWriter
            string now = DateTime.Now.ToString("yyyyMMdd_hhmm tt");
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = avgFilename; //$"propdBm_{now}_{prefix}_Band_Ant";  // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "PropoPlot documents (.csv)|*.csv|All files (*.*)|*.*"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                saveFileName.Text = dlg.FileName;  //write the file name on the UI
                avgFilename = dlg.FileName; // saving it for ron

                // now send all to the filename
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine($"{prefix},UTC,EUdbm,EUdbmAVG,EUcnt,JAdbm,JAdbmAVG,JAcnt,NAdbm,NAdbmAVG,NAcnt,OCdbm,OCdbmAVG,OCcnt,AFdbm,AFdbmAVG,AFcnt,SAdbm,SAdbmAVG,SAcnt,FAdbm,FAdbmAVG,FAcnt"); //this is the csv file header

                    foreach (string item in continentAVGList)
                    {
                        //writer.WriteLine($"Kenwood 1,{cd.pEUdbm},{cd.pEUnumber},{cd.pJAdbm},{cd.pJAnumber},{cd.pNAdbm},{cd.pNAnumber},{cd.pOCdbm},{cd.pOCnumber},{cd.pAFdbm},{cd.pAFnumber},{cd.pSAdbm} ,{cd.pSAnumber},{cd.pFAdbm} ,{cd.pFAnumber}");
                        writer.WriteLine(item);
                    }//end foreach - writing the list
                }//und using


            }//end if result == true

            avgFilename = dlg.FileName;  //write it back to the variable just in case we changed it
            EventLogger.WriteLine($"savedAvgCont_Click: FileName saved: {avgFilename}");
        }


        private void readAvgCont_Click(object sender, RoutedEventArgs e)
        {
             {
                //you need to have using Microsoft.Win32; up top.  No dragging a toolbox item onto the form
                //using System.IO; is for SttreamWriter
                string now = DateTime.Now.ToString("h_mm tt");
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = $"Propo";  // Default file name
                dlg.DefaultExt = ".csv"; // Default file extension
                dlg.Filter = "PropoPlot documents (.csv)|*.csv|All files (*.*)|*.*"; // Filter files by extension

                int count = 0;
                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();
                // Process save file dialog box results
                if (result == true)
                {

                    // Open document
                    string filename = dlg.FileName;
                    saveFileName.Text = dlg.FileName;  //write the file name on the UI

                    //// now send all to the filename
                    //continentAVGList.Add("RingAnt,Zulu,EUdbm,EUdbmAVG,EUcnt,JAdbm,JAdbmAVG,JAcnt,NAdbm,NAdbmAVG,NAcnt,OCdbm,OCdbmAVG,OCcnt,AFdbm,AFdbmAVG,AFcnt,SAdbm,SAdbmAVG,SAcnt,FAdbm,FAdbmAVG,FAcnt"); //this is the csv file header
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (count > 0) //miss the first row of the file - it has header info we dont want
                            {
                                continentAVGList.Add(line);

                            }
                            count += 1;
                        }

                    }//end using
                }//end if result == true
                //remove the first line of the list - it breaks things
                displayTotalDecodes.Text = (int.Parse(displayTotalDecodes.Text) + count).ToString();

                GraphsMainMenu.IsEnabled = true;
            }
            EventLogger.WriteLine($"readAvgCont_Click: FileName saved: {saveFileName.Text}");
        } //end of method


        private void saveQSOCont_Click(object sender, RoutedEventArgs e)
        {
            //you need to have using Microsoft.Win32; up top.  No dragging a toolbox item onto the form
            //using System.IO; is for SttreamWriter
            string now = DateTime.Now.ToString("yyyyMMdd_hhmm tt");
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = qsoFilename; //$"propQSO_{now}_{prefix}_Band_Ant";  // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "PropoPlot documents (.csv)|*.csv|All files (*.*)|*.*"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                saveQSOFileName.Text = dlg.FileName;  //write the file name on the UI

                // now send all to the filename
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine($"{prefix},UTC,dB,dT,Freq"); //this is the csv file header

                    foreach (string item in messages)
                    {
                        //writer.WriteLine($"Kenwood 1,{cd.pEUdbm},{cd.pEUnumber},{cd.pJAdbm},{cd.pJAnumber},{cd.pNAdbm},{cd.pNAnumber},{cd.pOCdbm},{cd.pOCnumber},{cd.pAFdbm},{cd.pAFnumber},{cd.pSAdbm} ,{cd.pSAnumber},{cd.pFAdbm} ,{cd.pFAnumber}");
                        writer.WriteLine(item);
                    }//end foreach - writing the list
                }//und using

            }
            qsoFilename = dlg.FileName;
            EventLogger.WriteLine($"saveQSOCont_Click: FileName Loaded: {qsoFilename}");
        }


        private void readQSOCont_Click(object sender, RoutedEventArgs e)
        {
            {
                //you need to have using Microsoft.Win32; up top.  No dragging a toolbox item onto the form
                //using System.IO; is for SttreamWriter
                string now = DateTime.Now.ToString("h_mm tt");
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = $"PropoQSO";  // Default file name
                dlg.DefaultExt = ".csv"; // Default file extension
                dlg.Filter = "PropoPlot documents (.csv)|*.csv|All files (*.*)|*.*"; // Filter files by extension

                int count = 0;
                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();
                // Process save file dialog box results
                if (result == true)
                {

                    // Open document
                    string filename = dlg.FileName;
                    saveFileName.Text = dlg.FileName;  //write the file name on the UI

                    //// now send all to the filename
                    //continentAVGList.Add("RingAnt,Zulu,EUdbm,EUdbmAVG,EUcnt,JAdbm,JAdbmAVG,JAcnt,NAdbm,NAdbmAVG,NAcnt,OCdbm,OCdbmAVG,OCcnt,AFdbm,AFdbmAVG,AFcnt,SAdbm,SAdbmAVG,SAcnt,FAdbm,FAdbmAVG,FAcnt"); //this is the csv file header
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (count > 0) //miss the first row of the file - it has header info we dont want
                            {
                                messages.Add($"{line}\r\n");
                                reconstructHeats(line);
                            }
                            count += 1;
                        }

                    }//end using
                }//end if result == true

                colourQSO(messages);

                //continentAVGList.RemoveAt(0);
                //remove the first line of the list - it breaks things
                displayTotalDecodes.Text = (int.Parse(displayTotalDecodes.Text) + count).ToString();
                GraphsMainMenu.IsEnabled = true;

                //window.FontSize = 8.0;
            }
            EventLogger.WriteLine($"ReadQSOCont_Click: FileName saved: {saveFileName}");
        }











    }//end
}
