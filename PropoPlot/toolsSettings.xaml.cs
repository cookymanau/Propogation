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
            goTryLine.Text = Properties.Settings.Default.goTryLine;


            toolsEUAvgColor.SelectedColor = (Color)ColorConverter.ConvertFromString( Properties.Settings.Default.EUAvgColor);
           toolsEURawColor.SelectedColor = (Color)ColorConverter.ConvertFromString( Properties.Settings.Default.EURawColor);
           toolsEUCntColor.SelectedColor = (Color)ColorConverter.ConvertFromString( Properties.Settings.Default.EUCntColor);

            toolsJAAvgColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.JAAvgColor);
            toolsJARawColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.JARawColor);
            toolsJACntColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.JACntColor);

            toolsNAAvgColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.NAAvgColor);
            toolsNARawColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.NARawColor);
            toolsNACntColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.NACntColor);

            toolsOCAvgColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.OCAvgColor);
            toolsOCRawColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.OCRawColor);
            toolsOCCntColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.OCCntColor);

            toolsAFAvgColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.AFAvgColor);
            toolsAFRawColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.AFRawColor);
            toolsAFCntColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.AFCntColor);

            toolsSAAvgColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.SAAvgColor);
            toolsSARawColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.SARawColor);
            toolsSACntColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.SACntColor);

            toolsFAAvgColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.FAAvgColor);
            toolsFARawColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.FARawColor);
            toolsFACntColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.FACntColor);

            toolsPanLeftPct.Text = Properties.Settings.Default.txtPan;


            AvgLineThickness.Value= int.Parse(Properties.Settings.Default.AvgLineThick);
            Avg2LineThickness.Value= int.Parse(Properties.Settings.Default.Avg2LineThick);
            RawLineThickness.Value = int.Parse(Properties.Settings.Default.RawLineThick);
            CntLineThickness.Value = int.Parse(Properties.Settings.Default.CntLineThick);
            GraphAvgDotSize.Value = int.Parse(Properties.Settings.Default.GraphAvgDotSize);
            GraphRawDotSize.Value = int.Parse(Properties.Settings.Default.GraphRawDotSize);
            GraphCntDotSize.Value = int.Parse(Properties.Settings.Default.GraphCntDotSize);

            
        }

        private void toolSettingsClose_Click(object sender, RoutedEventArgs e)
        {

            //Properties.Settings.Default.AvgPrd = toolsAvgPrd.Text;  //save whatever the user puts into the averaging interval box.
            //Properties.Settings.Default.PeriodTimer = toolsMainTimer.Text; //this is the number we ut into the decode timer loop


            //Properties.Settings.Default.EUAvgColor = toolsEUAvgColor.SelectedColor.ToString();
            //Properties.Settings.Default.EURawColor = toolsEURawColor.SelectedColor.ToString();
            //Properties.Settings.Default.EUCntColor = toolsEUCntColor.SelectedColor.ToString();

            //Properties.Settings.Default.JAAvgColor = toolsJAAvgColor.SelectedColor.ToString();
            //Properties.Settings.Default.JARawColor = toolsJARawColor.SelectedColor.ToString();
            //Properties.Settings.Default.JACntColor = toolsJACntColor.SelectedColor.ToString();

            //Properties.Settings.Default.NAAvgColor = toolsNAAvgColor.SelectedColor.ToString();
            //Properties.Settings.Default.NARawColor = toolsNARawColor.SelectedColor.ToString();
            //Properties.Settings.Default.NACntColor = toolsNACntColor.SelectedColor.ToString();

            //Properties.Settings.Default.OCAvgColor = toolsOCAvgColor.SelectedColor.ToString();
            //Properties.Settings.Default.OCRawColor = toolsOCRawColor.SelectedColor.ToString();
            //Properties.Settings.Default.OCCntColor = toolsOCCntColor.SelectedColor.ToString();

            //Properties.Settings.Default.AFAvgColor = toolsAFAvgColor.SelectedColor.ToString();
            //Properties.Settings.Default.AFRawColor = toolsAFRawColor.SelectedColor.ToString();
            //Properties.Settings.Default.AFCntColor = toolsAFCntColor.SelectedColor.ToString();

            //Properties.Settings.Default.SAAvgColor = toolsSAAvgColor.SelectedColor.ToString();
            //Properties.Settings.Default.SARawColor = toolsSARawColor.SelectedColor.ToString();
            //Properties.Settings.Default.SACntColor = toolsSACntColor.SelectedColor.ToString();

            //Properties.Settings.Default.FAAvgColor = toolsFAAvgColor.SelectedColor.ToString();
            //Properties.Settings.Default.FARawColor = toolsFARawColor.SelectedColor.ToString();
            //Properties.Settings.Default.FACntColor = toolsFACntColor.SelectedColor.ToString();

            //Properties.Settings.Default.AvgLineThick = AvgLineThickness.Value.ToString();
            //Properties.Settings.Default.Avg2LineThick = Avg2LineThickness.Value.ToString();
            //Properties.Settings.Default.RawLineThick = RawLineThickness.Value.ToString();
            //Properties.Settings.Default.CntLineThick = CntLineThickness.Value.ToString();
            //Properties.Settings.Default.GraphAvgDotSize = GraphAvgDotSize.Value.ToString();
            //Properties.Settings.Default.GraphRawDotSize = GraphRawDotSize.Value.ToString();
            //Properties.Settings.Default.GraphCntDotSize = GraphCntDotSize.Value.ToString();
            //Properties.Settings.Default.Save();

            saveAllSettings();
            this.Close();
        }

        private void toolSettingsApply_Click(object sender, RoutedEventArgs e)
        {
            saveAllSettings();
            
        }

         private void saveAllSettings()
        {
            Properties.Settings.Default.AvgPrd = toolsAvgPrd.Text;  //save whatever the user puts into the averaging interval box.
            Properties.Settings.Default.PeriodTimer = toolsMainTimer.Text; //this is the number we ut into the decode timer loop

            Properties.Settings.Default.EUAvgColor = toolsEUAvgColor.SelectedColor.ToString();
            Properties.Settings.Default.EURawColor = toolsEURawColor.SelectedColor.ToString();
            Properties.Settings.Default.EUCntColor = toolsEUCntColor.SelectedColor.ToString();

            Properties.Settings.Default.JAAvgColor = toolsJAAvgColor.SelectedColor.ToString();
            Properties.Settings.Default.JARawColor = toolsJARawColor.SelectedColor.ToString();
            Properties.Settings.Default.JACntColor = toolsJACntColor.SelectedColor.ToString();

            Properties.Settings.Default.NAAvgColor = toolsNAAvgColor.SelectedColor.ToString();
            Properties.Settings.Default.NARawColor = toolsNARawColor.SelectedColor.ToString();
            Properties.Settings.Default.NACntColor = toolsNACntColor.SelectedColor.ToString();

            Properties.Settings.Default.OCAvgColor = toolsOCAvgColor.SelectedColor.ToString();
            Properties.Settings.Default.OCRawColor = toolsOCRawColor.SelectedColor.ToString();
            Properties.Settings.Default.OCCntColor = toolsOCCntColor.SelectedColor.ToString();

            Properties.Settings.Default.AFAvgColor = toolsAFAvgColor.SelectedColor.ToString();
            Properties.Settings.Default.AFRawColor = toolsAFRawColor.SelectedColor.ToString();
            Properties.Settings.Default.AFCntColor = toolsAFCntColor.SelectedColor.ToString();

            Properties.Settings.Default.SAAvgColor = toolsSAAvgColor.SelectedColor.ToString();
            Properties.Settings.Default.SARawColor = toolsSARawColor.SelectedColor.ToString();
            Properties.Settings.Default.SACntColor = toolsSACntColor.SelectedColor.ToString();

            Properties.Settings.Default.FAAvgColor = toolsFAAvgColor.SelectedColor.ToString();
            Properties.Settings.Default.FARawColor = toolsFARawColor.SelectedColor.ToString();
            Properties.Settings.Default.FACntColor = toolsFACntColor.SelectedColor.ToString();

            Properties.Settings.Default.AvgLineThick = AvgLineThickness.Value.ToString();
            Properties.Settings.Default.Avg2LineThick = Avg2LineThickness.Value.ToString();
            Properties.Settings.Default.RawLineThick = RawLineThickness.Value.ToString();
            Properties.Settings.Default.CntLineThick = CntLineThickness.Value.ToString();
            Properties.Settings.Default.GraphAvgDotSize = GraphAvgDotSize.Value.ToString();
            Properties.Settings.Default.GraphRawDotSize = GraphRawDotSize.Value.ToString();
            Properties.Settings.Default.GraphCntDotSize = GraphCntDotSize.Value.ToString();

            Properties.Settings.Default.txtPan = toolsPanLeftPct.Text;
            Properties.Settings.Default.goTryLine = goTryLine.Text;

            Properties.Settings.Default.Save();

        }


    }
}
