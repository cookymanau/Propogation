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


namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      List<string> udpStrings = new List<string>(); // this is our in memory buffer. It belongs to all threads
        //make a global instatiation of our class
        UdpDataload ul = new UdpDataload();
        int timercounter = 0; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonUDPport_click(object sender, RoutedEventArgs e)
        {

        }

   //  private void ButtonClearTxtBlock(object sender, RoutedEventArgs e)
   //  {
   //   //   message.Text = String.Empty;
   //  }

  //   private void ButtonClearTxtList(object sender, RoutedEventArgs e)
  //   {
  //       udpStrings.Clear();
  //
  //   }

  //   private void message_TextChanged(object sender, TextChangedEventArgs e)
  //   {
  //     //  message.ScrollToEnd();
  //   }
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

            // now start the timer to process the UDP stuff
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
    }

}


