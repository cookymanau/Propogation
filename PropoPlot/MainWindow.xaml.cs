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

        // static List<string>  continentAVGList = new List<string>();  //we store the data into a new list record every period
        List<string> continentAVGList = new List<string>();  //we store the data into a new list record every period

        toolsContLatLongSetting tll = new toolsContLatLongSetting();  //this class here so we can see it every where we need it Holds the lat min, lat max longMin and longMax for the continents

        static toolsSettings tset = new toolsSettings(); //this is the tools options dialog box

        UdpDataload ul = new UdpDataload(); //make a global instatiation of our class. We store the current data load here Only ever one record

        int timercounter = 0;
        int cycleCounter = 0;




        public MainWindow()
        {
            InitializeComponent();
            //continentAVGList.Add("");  // initialising the list with something

            usrDefinedLabel.Text = Properties.Settings.Default.UsrDefinedName;

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
                cycleCounter += 1;  //keep counting how many times we go throuh this
                displayTotalCycles.Text = cycleCounter.ToString();
                // timerBar.Value = timercounter;
                if (cycleCounter > 2)
                    GraphsMainMenu.IsEnabled = true;


                udpStrings.Clear(); //clear the previous work - otherwise its a memory suck
                                    //***  tblaggingCount.Text = laggingCount.ToString(); // and show something on the UI

                //  gs.redrawThePlot();

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
            data.Add("2238");
            data.Add("2239");
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
            string now = DateTime.Now.ToString("h:mm:ss tt");

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = $"Propo_{now}_"; // Default file name
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
            string now = DateTime.Now.ToString("yyyyMMdd_hhmm tt");
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = $"Propo_{now}_{prefix}_Band_Ant";  // Default file name
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

                // now send all to the filename
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine($"{prefix},Zulu,EUdbm,EUdbmAVG,EUcnt,JAdbm,JAdbmAVG,JAcnt,NAdbm,NAdbmAVG,NAcnt,OCdbm,OCdbmAVG,OCcnt,AFdbm,AFdbmAVG,AFcnt,SAdbm,SAdbmAVG,SAcnt,FAdbm,FAdbmAVG,FAcnt"); //this is the csv file header

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
            //kill threads some how
            //this the attempt to kill off the zombie process
            Process[] processes = Process.GetProcessesByName("PropoPlot");
            processes[0].Kill();


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
            toolsContLatLongSetting tll = new toolsContLatLongSetting();
            tll.ShowDialog();
        }

        private void toolOptions_Click(object sender, RoutedEventArgs e)
        {

            toolsSettings ts = new toolsSettings();
            ts.Show();
        }

        private void btnGraphPlot_Click(object sender, RoutedEventArgs e)
        {
            //   graphPlot gp = new graphPlot(continentAVGList);
            //    gp.Show();
        }

        private void graphAll_Click(object sender, RoutedEventArgs e)
        {
            graphPlot gp = new graphPlot(continentAVGList);
            gp.Show();
        }

        //private void graphEu_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void graphMain_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void graphSingle_Click(object sender, RoutedEventArgs e)
        {
            redrawSingle();
        }


        private void redrawSingle()
        {
            graphSinglePlot gs = new graphSinglePlot(continentAVGList);

            //if (gs.chkLiveUpdate.IsChecked == true)
            //   gs.liveRedraw = true;

            gs.Show();

        }


        private void graphCompare_Click(object sender, RoutedEventArgs e)
        {
            //open a new window with two open file dialogs and then graph the contents
            //but it means you have to save the files first - use the 'File Save Avg continents'
            graphCompare gp = new graphCompare();
            gp.Show();

            //cfFileOne.Text = gp.cFile1;

        }
        /// <summary>
        /// Experimental
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetList_Click(object sender, RoutedEventArgs e)
        {
            udpStrings.Clear();
            continentAVGList.Clear();
            continentList.Clear();
            displayTotalDecodes.Text = "0";
            frmMessageDialog md = new frmMessageDialog();
            md.messageBoxUpper.Text = $"Cleared The Lists ";
            md.messageBoxLower.Text = $"{e}";
            md.Show();
        }
        /// <summary>
        /// another experimental thing 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetAvgs_Click(object sender, RoutedEventArgs e)
        {
            // continentAVGList.Clear();
            Array.Clear(FAavgs, 0, 120);
            Array.Clear(EUavgs, 0, 120);
            Array.Clear(JAavgs, 0, 120);
            Array.Clear(NAavgs, 0, 120);
            Array.Clear(OCavgs, 0, 120);
            Array.Clear(SAavgs, 0, 120);
            Array.Clear(AFavgs, 0, 120);


            //   udpStrings.Clear();
            //   continentAVGList.Clear();
            //   continentList.Clear();
            frmMessageDialog md = new frmMessageDialog();
            md.messageBoxUpper.Text = $"Cleared The Lists ";
            md.messageBoxLower.Text = $"{e}";
            md.Show();


        }

        private void graphLive_Click(object sender, RoutedEventArgs e)
        {
            graphLivePlot gl = new graphLivePlot(continentAVGList);
            gl.Show();
        }

        private void graphPop_Click(object sender, RoutedEventArgs e)
        {
            graphPopulation gp = new graphPopulation(continentAVGList);
            //gp.Cursor = Cursors.Wait;
            gp.Show();

        }

        private void readAvgCont_Click(object sender, RoutedEventArgs e)
        {
                 Cursor fred = this.Cursor; //sav e the current cursor
                //this.Cursor = Cursors.Wait;
                

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

                //continentAVGList.RemoveAt(0);
                //remove the first line of the list - it breaks things
                displayTotalDecodes.Text = (int.Parse(displayTotalDecodes.Text) + count).ToString();
                this.Cursor = fred;
                GraphsMainMenu.IsEnabled = true; 
            } //end of method



        }

        private void graphHeat_Click(object sender, RoutedEventArgs e)
        {
            graphHeat gh = new graphHeat();
            gh.Show();
        }

 

        private void graphFaros_Click(object sender, RoutedEventArgs e)
        {

            // this is where take our list and divide it into 15 minute or 30 minute or 60 minute blocks and average the data
            // we have the data in a list, pull it out into arrays, average it and then show it.


            graphFaros gf = new graphFaros(continentAVGList);
            gf.Show();


        }

        private void plotFaros_Click(object sender, RoutedEventArgs e)
        {
            plotOfFaros pf = new plotOfFaros();
            pf.Show();
            
        }
    }//end of class

}


