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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        continentData cd = new continentData();  //this is our class holding all of the data we generate dBm PERIOD average, number of decodes
        continentData cdAvg = new continentData();  //this is our class holding all of the data we generate dBm WEIGHTED average, number of decodes

        List<string> udpStrings = new List<string>(); // this is our in memory buffer. It belongs to all threads. Its where write the decode period for 15 seconds
        List<string> continentAVGList = new List<string>();  //we store the data into a new list record every period

        toolsContLatLongSetting tll = new toolsContLatLongSetting();  //this class here so we can see it every where we need it Holds the lat min, lat max longMin and longMax for the continents
        UdpDataload ul = new UdpDataload(); //make a global instatiation of our class. We store the current data load here Only ever one record

        int timercounter = 0;
        int cycleCounter = 0;

        string avgFilename = "";
        string qsoFilename = "";

 
 

        public MainWindow()
        {
            InitializeComponent();

            this.Height = Properties.Settings.Default.mainwindowheight;
            this.Width =  Properties.Settings.Default.mainwindowwidth;



            EventLogger.DeleteLog();
            EventLogger.WriteLine("Propaplot started");

            Log.Logger = new LoggerConfiguration().WriteTo.File(@"c:\users\cooky\Documents\PropaPlot.log").CreateLogger();
            Log.Information("Log for PropaPlot");
            Log.Verbose("another line");



            string now = DateTime.Now.ToString("yyyyMMdd_hhmm tt");
            avgFilename = $"propDBM_{now}_{prefix}_Band_Ant";  // Default file name
            qsoFilename = $"propQSO_{now}_{prefix}_Band_Ant";  // Default file name


            // this throws an error in a stand alone published version

            BrushConverter bc = new BrushConverter();
            dBmCut1.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM1);
            dBmCut2.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM2);
            dBmCut3.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM3);
            dBmCut4.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM4);
            dBmCut5.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM5);



            usrDefinedLabel.Text = Properties.Settings.Default.UsrDefinedName;
            GraphsMainMenu.IsEnabled = false;

            window.FontSize = double.Parse(Properties.Settings.Default.myFontSize);
        }



        //private System.Windows.Media.Brush DrawingColorToBrush(System.Drawing.Color color)
        //{
        //    System.Windows.Media.Brush ret = null;
        //    BrushConverter m = new BrushConverter();
        //    string s = "#" + color.ToArgb().ToString("X8");
        //    if (m.CanConvertFrom(typeof(string)))
        //    {
        //        ret = (System.Windows.Media.Brush)m.ConvertFromString(s);
        //    }
        //    return ret;
        //}








        private void ButtonUDPport_click(object sender, RoutedEventArgs e)
        {

        }


        private void plotmessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            // plotmessage.ScrollToEnd();
        }

        //private void btnDXatlas_Click(object sender, RoutedEventArgs e)
        //{

        //    _connectAndConfigureAtlas();  // we only want to do this once, so doing it here

        //    plotToDxAtlas = !plotToDxAtlas;
        //    if (plotToDxAtlas == true)
        //    {
        //        btnDXAtlas.Content = "DX Atlas Plotting";
        //        //btnDXAtlas.Background = "#eee";
        //        DXAtlasplotPoints();  //THIS stuff is found in the PlotDXAtlas stuff
        //    }
        //    else
        //    {
        //        //btnDXAtlas.Background = "";
        //        btnDXAtlas.Content = "DX Atlas";
        //    }
        //}

 

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
            GraphsMainMenu.IsEnabled = false;

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

                if (cycleCounter > 1)
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



        private void btnClearMapOfArtifacts_Click(object sender, RoutedEventArgs e)
        {
         //   DxAtlasMapClear();
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


         private void ExitPropoPlot_Click(object sender, RoutedEventArgs e)
        {

            EventLogger.WriteLine($"MainWindow.Xaml.cs:ExitPropoPlot: Final Window size: Width {this.Width}, Height: {this.Height}");
            Properties.Settings.Default.mainwindowheight = this.Height;
            Properties.Settings.Default.mainwindowwidth = this.Width;
            Properties.Settings.Default.Save();

            //Log.CloseAndFlush();
            EventLogger.WriteLine("Closing PropaPlot");
            
            //kill threads some how
            //this the attempt to kill off the zombie process
            Process[] processes = Process.GetProcessesByName("PropoPlot");
            processes[0].Kill();
            this.Close();

        }


        //private void Continent_Click(object sender, RoutedEventArgs e)
        //{

        //}

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
            tll.Show();
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
            EventLogger.WriteLine($"btnReset Clicked: udpStrings cleared Count: { udpStrings.Count()}");
            EventLogger.WriteLine($"btnReset Clicked: contentAVGList cleared Count: {continentAVGList.Count()}");
            EventLogger.WriteLine($"btnReset Clicked: messages cleared Count: {messages.Count()}");
            EventLogger.WriteLine($"btnReset Clicked: heats cleared Count: {heats.Count()}");
            
            udpStrings.Clear();
            continentAVGList.Clear();
            messages.Clear();
            heats.Clear();
            displayTotalDecodes.Text = "0";
            displayTotalCycles.Text = "0";
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

 
        private void graphHeat_Click(object sender, RoutedEventArgs e)
        {
            graphHeat gh = new graphHeat(heats);
            //graphHeat gh = new graphHeat(udpStrings);
            gh.Show();
        }



        private void graphFaros_Click(object sender, RoutedEventArgs e)
        {

            // this is where take our list and divide it into 15 minute or 30 minute or 60 minute blocks and average the data
            // we have the data in a list, pull it out into arrays, average it and then show it.


            graphFaros gf = new graphFaros(continentAVGList);
            gf.Show();


        }
        /// <summary>
        /// Renamed this window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plotFaros_Click(object sender, RoutedEventArgs e)
        {
            plotOfFaros pf = new plotOfFaros(continentAVGList);
            pf.Show();

        }




        //You can use ScrollChangedEventArgs.ExtentHeightChange to know if a ScrollChanged is due to a change in the
        //content or to a user action...When the content is unchanged, the ScrollBar position sets or unsets the
        //auto-scroll mode. When the content has changed you can apply auto-scrolling.
        //https://stackoverflow.com/questions/2984803/how-to-automatically-scroll-scrollviewer-only-if-the-user-did-not-change-scrol

        private Boolean AutoScroll = true;
        private void qsoScroller_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // qsoScroller.ScrollToEnd();
            // User scroll event : set or unset auto-scroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (qsoScroller.VerticalOffset == qsoScroller.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set auto-scroll mode
                    AutoScroll = true;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset auto-scroll mode
                    AutoScroll = false;
                }
            }

            // Content scroll event : auto-scroll eventually
            if (AutoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and auto-scroll mode set
                // Autoscroll
                qsoScroller.ScrollToVerticalOffset(qsoScroller.ExtentHeight);
            }
        }

 

        /// <summary>
        /// This puts together the heats list from a file
        /// its called from the QSO's open file 
        /// </summary>
        /// <param name="line"></param>
        private void reconstructHeats(string line)
        {

            // get the data we want out of line and put into the list heats
            string[] wrdmsg = { };
            string[] timea = { };
            string[] lata = { };
            string[] lona = { };
            string[] dBma = { };

            string dBm = "";
            string lat = "";
            string lon = "";
            string utc = "";


            wrdmsg = line.Split('\t');

            timea = wrdmsg[0].Split(' ');
            utc = timea[1];
                
            dBma = wrdmsg[2].Split(':');
            dBm = dBma[1];

            lata = wrdmsg[4].Split(':');
            lat = lata[1];

            lona = wrdmsg[5].Split(':');
            lon = lona[1];


            // this list will get overwhelmingly long with time, a few hours only.
            // so we should try and limit its size to say 20k records.  Means we wont be able to go
            // back any further than that

            if (heats.Count > 20000)
            {
                heats.Clear();
                EventLogger.WriteLine($"btnReset Clicked: udpStrings cleared Count: { udpStrings.Count()}");
                EventLogger.WriteLine($"reconstructHeats: heats Count exceeded 20000 and cleared: { heats.Count()}");

            }
            else
                heats.Add($"{utc},{dBm},{lat},{lon}");

        }




        private void TextBlock_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                ++window.FontSize;
            if (e.Delta < 0 && window.FontSize > 1)
                --window.FontSize;

            EventLogger.WriteLine($"TextBlock_PreviewMouseWheel: FontSize Changed with mouse wheel: {window.FontSize.ToString()}");
        }

        private void graphRadarHistory_Click(object sender, RoutedEventArgs e)
        {
            graphRadarHistory grh = new graphRadarHistory(continentAVGList);
            grh.Show();
        }

 
        private void graphQSOcount_Click_1(object sender, RoutedEventArgs e)
        {
            graphQSOCount gc = new graphQSOCount(continentAVGList);
            gc.Show();

        }
    }//end of class

}


