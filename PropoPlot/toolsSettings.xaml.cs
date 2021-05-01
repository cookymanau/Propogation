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
    /// Interaction logic for toolsSettings.xaml
    /// </summary>
    public partial class toolsSettings : Window
    {
        public toolsSettings()
        {
            InitializeComponent();
            //so we have just opened the form...get the users last changes to the dialog boxes
            toolsAvgPrd.Text = Properties.Settings.Default.AvgPrd;
            toolsMainTimer.Text = Properties.Settings.Default.PeriodTimer;  
        }

        private void toolSettingsClose_Click(object sender, RoutedEventArgs e)
        {

            Properties.Settings.Default.AvgPrd = toolsAvgPrd.Text;  //save whatever the user puts into the averaging interval box.
            Properties.Settings.Default.PeriodTimer = toolsMainTimer.Text; //this is the number we ut into the decode timer loop
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void perType1_Checked(object sender, RoutedEventArgs e)
        {

       //     MessageBox.Show("period header, type 1 selected");
        }

        private void perType2_Checked(object sender, RoutedEventArgs e)
        {
          //  MessageBox.Show("period header, type 2 selected");
        }
    }
}
