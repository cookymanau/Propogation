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
    /// Interaction logic for toolsContLatLongSetting.xaml
    /// </summary>
    public partial class toolsContLatLongSetting : Window
    {
        public toolsContLatLongSetting()
        {
            InitializeComponent();
            //gaet the user settings back drom the init
            EULatMin.Text = Properties.Settings.Default.EULatMin;
            EULatMax.Text = Properties.Settings.Default.EULatMax;
            EULongMin.Text = Properties.Settings.Default.EULongMin;
            EULongMax.Text = Properties.Settings.Default.EULongMax;
            
            JALatMin.Text = Properties.Settings.Default.JALatMin;
            JALatMax.Text = Properties.Settings.Default.JALatMax;
            JALongMin.Text = Properties.Settings.Default.JALongMin;
            JALongMax.Text = Properties.Settings.Default.JALongMax;

            NALatMin.Text = Properties.Settings.Default.NALatMin;
            NALatMax.Text = Properties.Settings.Default.NALatMax;
            NALongMin.Text = Properties.Settings.Default.NALongMin;
            NALongMax.Text = Properties.Settings.Default.NALongMax;

            SALatMin.Text = Properties.Settings.Default.SALatMin;
            SALatMax.Text = Properties.Settings.Default.SALatMax;
            SALongMin.Text = Properties.Settings.Default.SALongMin;
            SALongMax.Text = Properties.Settings.Default.SALongMax;

            OCLatMin.Text = Properties.Settings.Default.OCLatMin;
            OCLatMax.Text = Properties.Settings.Default.OCLatMax;
            OCLongMin.Text = Properties.Settings.Default.OCLongMin;
            OCLongMax.Text = Properties.Settings.Default.OCLongMax;

            AFLatMin.Text = Properties.Settings.Default.AFLatMin;
            AFLatMax.Text = Properties.Settings.Default.AFLatMax;
            AFLongMin.Text = Properties.Settings.Default.AFLongMin;
            AFLongMax.Text = Properties.Settings.Default.AFLongMax;

            FALatMin.Text = Properties.Settings.Default.FALatMin;
            FALatMax.Text = Properties.Settings.Default.FALatMax;
            FALongMin.Text = Properties.Settings.Default.FALongMin;
            FALongMax.Text = Properties.Settings.Default.FALongMax;
            UsrDefinedName.Text = Properties.Settings.Default.UsrDefinedName;



        }

        private void toolContLatLongExit_Click(object sender, RoutedEventArgs e)
        {

            Properties.Settings.Default.FALatMin = FALatMin.Text;
            Properties.Settings.Default.FALatMax = FALatMax.Text;
            Properties.Settings.Default.FALongMin = FALongMin.Text;
            Properties.Settings.Default.FALongMax = FALongMax.Text;            
            Properties.Settings.Default.EULatMin = EULatMin.Text;
            Properties.Settings.Default.EULatMax = EULatMax.Text;
            Properties.Settings.Default.EULongMin = EULongMin.Text;
            Properties.Settings.Default.EULongMax = EULongMax.Text;

            Properties.Settings.Default.JALatMin = JALatMin.Text;
            Properties.Settings.Default.JALatMax = JALatMax.Text;
            Properties.Settings.Default.JALongMin = JALongMin.Text;
            Properties.Settings.Default.JALongMax = JALongMax.Text;
            Properties.Settings.Default.NALatMin = NALatMin.Text;
            Properties.Settings.Default.NALatMax = NALatMax.Text;
            Properties.Settings.Default.NALongMin = NALongMin.Text;
            Properties.Settings.Default.NALongMax = NALongMax.Text;

            Properties.Settings.Default.SALatMin = SALatMin.Text;
            Properties.Settings.Default.SALatMax = SALatMax.Text;
            Properties.Settings.Default.SALongMin = SALongMin.Text;
            Properties.Settings.Default.SALongMax = SALongMax.Text;
            Properties.Settings.Default.OCLatMin = OCLatMin.Text;
            Properties.Settings.Default.OCLatMax = OCLatMax.Text;
            Properties.Settings.Default.OCLongMin = OCLongMin.Text;
            Properties.Settings.Default.OCLongMax = OCLongMax.Text;

            Properties.Settings.Default.AFLatMin = AFLatMin.Text;
            Properties.Settings.Default.AFLatMax = AFLatMax.Text;
            Properties.Settings.Default.AFLongMin = AFLongMin.Text;
            Properties.Settings.Default.AFLongMax = AFLongMax.Text;
            
            Properties.Settings.Default.UsrDefinedName = UsrDefinedName.Text;


            Properties.Settings.Default.Save();
            this.Close();
        }

        private void UsrDefinedName_TextChanged(object sender, TextChangedEventArgs e)
        {


        }//end
    }
}
