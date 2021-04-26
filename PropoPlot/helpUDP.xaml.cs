using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for helpUDP.xaml
    /// </summary>
    public partial class helpUDP : Window
    {
        public helpUDP()
        {
            InitializeComponent();
            string text = @"UDP Setup

JTDX sends its decodes to a UDP networking port
You must make these settings in JTDX File\Settings\Reporting tab in
the Primary UDP Server pane
Set the IP Address of you PC here.  Mine is 192.168.1.45  or 127.0.0.1
Set the UDP Server port number to 2233 (the default I think).  But it can be
any number.  I have used 2337, 2333....whatever really.  
Ticks in the Accept UDP requests
Tick in Notify on accepted UDP request
Tick Accepted UDP request restores window

WSJT-X will be similar.

If you are using JT Alert, then JT Alert will be wanting to use this UDP port.
So in JT Alert you must use the 
   Settings \ Manage Settings \ Applications \WSJT-X / JTDX menu option and put
a tick into the 'Resend WSJT-X UDP packets (recieve only) AND
 the ip address of where you want to send them (192.168.1.45) and the UDP port
you want to use.  DEFINITELY NOT the one you set in JTDX!!!!  Use 2334 

So now you are all set up, when you use PropoPlot without JT Alert, use the
UDP port number you set in JTDX.  If you are using JT Alert, then use that one.

Use the 'Select' drop down combo box or just type the number into the UDP port box.

Click the button 'Start Capture' to start getting the decodes from JTDX or WSJT-X

";

            helpUDPText.Text = text;



        }

        private void helpUDPExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
