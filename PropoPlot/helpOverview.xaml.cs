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
    /// Interaction logic for helpOverview.xaml
    /// </summary>
    public partial class helpOverview : Window
    {
        public helpOverview()
        {
            InitializeComponent();
            string text = @"PropoPlot Overview 

Monitor the decodes from WSJT-X or JTDX via UDP.  See Help\UDP
for instructions for setting this up.

Gather statistics on a period basis.  EG 15 second intervals for FT8

Statisits for the entire capture period, weighted average for 75 seconds 
    and weighted average dBm for the continents

Save the data to various text files for analysis in EXCEL

Send spots to DX Atlas for visual indication of current propogation


";

            helpOverviewText.Text = text;





        }

        private void helpOverviewExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
