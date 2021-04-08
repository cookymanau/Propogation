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
                                                      //Declaring timer - a FORMS timer
    

        //make a global instatiation of our class
        UdpDataload ul = new UdpDataload();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonUDPport_click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClearTxtBlock(object sender, RoutedEventArgs e)
        {
         //   message.Text = String.Empty;
        }

        private void ButtonClearTxtList(object sender, RoutedEventArgs e)
        {
            udpStrings.Clear();

        }

        private void message_TextChanged(object sender, TextChangedEventArgs e)
        {
          //  message.ScrollToEnd();
        }
        private void plotmessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            plotmessage.ScrollToEnd();
        }

        private void btnDXatlas_Click(object sender, RoutedEventArgs e)
        {
            
            DXAtlasplotPoints();
        }

        private void btnGoogleEarth_Click(object sender, RoutedEventArgs e)
        {
          // GetQsosDXAtlas();
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

            // now start the timer to process the UDP stuff

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();


        }

             int  timercounter = 0;
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {


            timercounter += 2;
            listCnt.Text = timercounter.ToString();
            if (timercounter >= 10)
            {
                GetQsosDXAtlas();
                timercounter = 0;
                udpStrings.Clear();
            }

        }





        private void btnUDPStop_Click(object sender, RoutedEventArgs e)
        {
            stopLoop = false;
        }
    }

}
