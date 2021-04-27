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


namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        continentData cd = new continentData();  //this is our class holding all of the data we generate dBm PERIOD average, number of decodes
        continentData cdAvg = new continentData();  //this is our class holding all of the data we generate dBm WEIGHTED average, number of decodes
     
        List<string> udpStrings = new List<string>(); // this is our in memory buffer. It belongs to all threads. Its where write the decode period for 15 seconds

        List<string> continentList = new List<string>();  //we store the data into a new list record every period
        List<string> continentListDC = new List<string>();  //we store the data into a new list record every period

        List<string> continentAVGList = new List<string>();  //we store the data into a new list record every period

        toolsContLatLongSetting tll = new toolsContLatLongSetting();  //this class here so we can see it every where we need it Holds the lat min, lat max longMin and longMax for the continents

     static  toolsSettings tset = new toolsSettings(); //this is the tools options dialog box

        UdpDataload ul = new UdpDataload(); //make a global instatiation of our class. We store the current data load here Only ever one record

        int timercounter = 0; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonUDPport_click(object sender, RoutedEventArgs e)
        {

        }

 
        private void plotmessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            plotmessage.ScrollToEnd();
        }

        private void btnDXatlas_Click(object sender, RoutedEventArgs e)
        {

            _connectAndConfigureAtlas();  // we only want to do this once, so doing it here

            plotToDxAtlas = !plotToDxAtlas;
            if (plotToDxAtlas == true)
            {
                btnDXAtlas.Content = "DX Atlas Plotting";
                //btnDXAtlas.Background = "#eee";
                DXAtlasplotPoints();  //THIS stuff is found in the PlotDXAtlas stuff
            }
            else
            {
                //btnDXAtlas.Background = "";
                btnDXAtlas.Content = "DX Atlas";
            }
        }

        private void btnGoogleEarth_Click(object sender, RoutedEventArgs e)
        {
          // GetQsosFromList();
        }

        /// <summary>
        /// This is a wrapper around the wsjtMessaging I got from git hub. I dont want
        /// to pick it apart, or even understand it really. I just want to use its output
        /// Things to do though is to pass in the actual port number I want it to use.
        /// At the moment that that is hard coded inthe M0lte dll.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUDPStart_Click(object sender, RoutedEventArgs e)
        {
            //stopLoop = true;
            wsjtmessages();

            btnUDPStart.Content = "Running";

            // now start the timer to process the UDP stuff now that we have started it.
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);//two second increments
            dispatcherTimer.Start();
        }

          
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //anything we want to do in a timer, do it here

            timercounter += 2; //going up in 2 second increments.  It seemed less busy that way
            timerBar.Value = timercounter;  //up date the indicator - something IS happening
           
            if (timercounter >= timerInterval) //every timerInterval seconds process the udp strings
            {
                GetQsosFromList();  //get the qsos for this round of 15 seconds
                
                timercounter = 0; //reset this
               // timerBar.Value = timercounter;
               udpStrings.Clear(); //clear the previous work - otherwise its a memory suck
                                    //***              tblaggingCount.Text = laggingCount.ToString(); // and show something on the UI
               // runningAvgDbm.Text = laggingCount.ToString();
            }
        }

        private void btnTest_click(object sender, RoutedEventArgs e)
        {
            // Maiden2latitude("OF87aa");
            //plotToDxAtlas = !plotToDxAtlas;
            //if(plotToDxAtlas)

            //*****************************            
            //      Udppoint[0, 0] = "vk6dw";
            //      Udppoint[0, 1] = "-11";
            //      Udppoint[0, 2] = "OF87";
            //      Udppoint[0, 3] = ""; 
            //      Udppoint[0, 4] = "";
            //      Udppoint[0, 5] = "";
            //      Udppoint[0,3] =   Maiden2latitude(Udppoint[0, 2]);
            //      Udppoint[0,4] =   Maiden2longitude(Udppoint[0, 2]);
            //
            //      Udppoint[1, 0] = "vk6dw";
            //      Udppoint[1, 1] = "5";
            //      Udppoint[1, 2] = "OF86";
            //      Udppoint[1, 3] = "";
            //      Udppoint[1, 4] = "";
            //      Udppoint[1, 5] = "";
            //      Udppoint[1, 3] = Maiden2latitude(Udppoint[1, 2]);
            //      Udppoint[1, 4] = Maiden2longitude(Udppoint[1, 2]);
            //
            //
            //      QSOsThiInterval = 2;
            //      DXAtlasplotPoints();
            //*************************************************

            //      _connectAndConfigureAtlas();

            //    _atlas.Map.BeginUpdate();
            //    _atlas.Map.CustomLayers.Clear();


            //     _atlas.Map.EndUpdate();
           // DxAtlasMapClear();

            

        }

        private void btnClearMap_Click(object sender, RoutedEventArgs e)
        {
            clearDXAtlasEachTime = !clearDXAtlasEachTime;

            if (clearDXAtlasEachTime == true)
            {

                btnClearMap.Content = "Clear Dots";
               // DxAtlasMapClear();  //does not do anything

            }
            else  //does this when false
            {
                btnClearMap.Content = "Keep Dots";
            }
        }

        private void btnClearMapOfArtifacts_Click(object sender, RoutedEventArgs e)
        {
            DxAtlasMapClear();
        }

        private void cmboUDP_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("2334");
            data.Add("2237");
            data.Add("2233");
            data.Add("2234");
            data.Add("2222");
            data.Add("2221");
            data.Add("2223");

            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;

        }

        private void cmboUDP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedcomboitem = sender as ComboBox;
            string name = selectedcomboitem.SelectedItem as string;
            //  MessageBox.Show(name);
            UDPportEntry.Text = name;
        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {

        }//end function


        private void save15secCont_Click(object sender, RoutedEventArgs e)
        {
            //you need to have using Microsoft.Win32; up top.  No dragging a toolbox item onto the form
            //using System.IO; is for SttreamWriter

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "PropoPlot"; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "PropoPlot documents (.csv)|*.csv|All files (*.*)|*.*"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                // now send all to the filename
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine("RingAnt,Zulu,EUdbm,JAdbm,NAdbm,OCdbm,AFdbm,SAdbm,FAdbm,EUcnt,JAcnt,NAcnt,OCcnt,AFcnt,SAcnt,FAcnt"); //this is the csv file header

                    foreach (string item in continentList)
                    {
                        //writer.WriteLine($"Kenwood 1,{cd.pEUdbm},{cd.pEUnumber},{cd.pJAdbm},{cd.pJAnumber},{cd.pNAdbm},{cd.pNAnumber},{cd.pOCdbm},{cd.pOCnumber},{cd.pAFdbm},{cd.pAFnumber},{cd.pSAdbm} ,{cd.pSAnumber},{cd.pFAdbm} ,{cd.pFAnumber}");
                        writer.WriteLine(item);
                    }//end foreach - writing the list
                }//und using


            }//end if result == true


        }

        private void saveAvgCont_Click(object sender, RoutedEventArgs e)
        {
            //you need to have using Microsoft.Win32; up top.  No dragging a toolbox item onto the form
            //using System.IO; is for SttreamWriter

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "PropoAverages"; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "PropoPlot documents (.csv)|*.csv|All files (*.*)|*.*"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                // now send all to the filename
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine("RingAnt,Zulu,EUdbm,EUdbmAVG,EUcnt,JAdbm,JAdbmAVG,JAcnt,NAdbm,NAdbmAVG,NAcnt,OCdbm,OCdbmAVG,OCcnt,AFdbm,AFdbmAVG,AFcnt,SAdbm,SAdbmAVG,SAcnt,FAdbm,FAdbmAVG,FAcnt"); //this is the csv file header

                    foreach (string item in continentAVGList)
                    {
                        //writer.WriteLine($"Kenwood 1,{cd.pEUdbm},{cd.pEUnumber},{cd.pJAdbm},{cd.pJAnumber},{cd.pNAdbm},{cd.pNAnumber},{cd.pOCdbm},{cd.pOCnumber},{cd.pAFdbm},{cd.pAFnumber},{cd.pSAdbm} ,{cd.pSAnumber},{cd.pFAdbm} ,{cd.pFAnumber}");
                        writer.WriteLine(item);
                    }//end foreach - writing the list
                }//und using


            }//end if result == true


        }

        private void ExitPropoPlot_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dbmAvg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Continent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UDP_Click(object sender, RoutedEventArgs e)
        {
            helpUDP hu = new helpUDP();
            hu.ShowDialog();
        }

        private void Overview_Click(object sender, RoutedEventArgs e)
        {
            helpOverview ho = new helpOverview();
            ho.ShowDialog();
        }

        private void toolsContLatLongSettings_Click(object sender, RoutedEventArgs e)
        {
           // toolsContLatLongSetting tll = new toolsContLatLongSetting();
            tll.ShowDialog();
        }

        private void toolOptions_Click(object sender, RoutedEventArgs e)
        {

            toolsSettings ts = new toolsSettings();
            ts.ShowDialog();
        }

        private void btnGraphPlot_Click(object sender, RoutedEventArgs e)
        {
            graphPlot gp = new graphPlot(continentAVGList);
            gp.Show();
        }
    }

}


