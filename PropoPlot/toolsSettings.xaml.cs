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

            toolsAvg2Color.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.Avg2Clr);
            toolsCnt2Color.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.Cnt2Clr);
            toolsRaw2Color.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.Raw2Clr);


            
            toolsPanLeftPct.Text = Properties.Settings.Default.txtPan;


            AvgLineThickness.Value= int.Parse(Properties.Settings.Default.AvgLineThick);
            Avg2LineThickness.Value= int.Parse(Properties.Settings.Default.Avg2LineThick);
            RawLineThickness.Value = int.Parse(Properties.Settings.Default.RawLineThick);
            Raw2LineThickness.Value = int.Parse(Properties.Settings.Default.Raw2LineThick);
            CntLineThickness.Value = int.Parse(Properties.Settings.Default.CntLineThick);
            GraphAvgDotSize.Value = int.Parse(Properties.Settings.Default.GraphAvgDotSize);
            GraphRawDotSize.Value = int.Parse(Properties.Settings.Default.GraphRawDotSize);
            GraphCntDotSize.Value = int.Parse(Properties.Settings.Default.GraphCntDotSize);

            yourCall.Text = Properties.Settings.Default.yourCall;
            theirCall.Text = Properties.Settings.Default.theirCall;
            truncateValue.Text = Properties.Settings.Default.truncateValue;
            myFontSize.Text = Properties.Settings.Default.myFontSize;

            //these are colour ramp colours
            crDBM1.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.crDBM1);
            crDBM2.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.crDBM2);
            crDBM3.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.crDBM3);
            crDBM4.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.crDBM4);
            crDBM5.SelectedColor = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.crDBM5);

            //these are the cut off values
            dBm1Value.Value = int.Parse(Properties.Settings.Default.dBm1Value); 
          dBm2Value.Value = int.Parse(Properties.Settings.Default.dBm2Value); 
          dBm3Value.Value = int.Parse(Properties.Settings.Default.dBm3Value); 
          dBm4Value.Value = int.Parse(Properties.Settings.Default.dBm4Value); 


        }

        private void toolSettingsClose_Click(object sender, RoutedEventArgs e)
        {

            saveAllSettings();
            this.Close();
        }

        private void toolSettingsApply_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mw = new MainWindow();
            
            //   Style = (Style)FindResource(typeof(Window));

            mw.window.FontSize = double.Parse(myFontSize.Text);
            mw.FontSize = double.Parse(myFontSize.Text);
            saveAllSettings();


            var bc = new BrushConverter();
            
            mw.dBmCut1.Background = (System.Windows.Media.Brush)bc.ConvertFrom(Properties.Settings.Default.crDBM1);
            mw.dBmCut2.Background = (System.Windows.Media.Brush)bc.ConvertFrom(Properties.Settings.Default.crDBM2);
            mw.dBmCut3.Background = (System.Windows.Media.Brush)bc.ConvertFrom(Properties.Settings.Default.crDBM3);
            mw.dBmCut4.Background = (System.Windows.Media.Brush)bc.ConvertFrom(Properties.Settings.Default.crDBM4);
            mw.dBmCut5.Background = (System.Windows.Media.Brush)bc.ConvertFrom(Properties.Settings.Default.crDBM5);
            mw.dBmCut1.UpdateLayout();
            mw.dBmCut2.UpdateLayout();
            mw.dBmCut3.InvalidateVisual();
            mw.dBmCut4.InvalidateVisual();
            mw.dBmCut5.InvalidateVisual();
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
            Properties.Settings.Default.yourCall = yourCall.Text;
            Properties.Settings.Default.theirCall = theirCall.Text;
            Properties.Settings.Default.truncateValue = truncateValue.Text;
            Properties.Settings.Default.myFontSize = myFontSize.Text;
            Properties.Settings.Default.Raw2LineThick = Raw2LineThickness.Text;

            Properties.Settings.Default.Avg2Clr = toolsAvg2Color.SelectedColor.ToString();
            Properties.Settings.Default.Cnt2Clr = toolsCnt2Color.SelectedColor.ToString();
            Properties.Settings.Default.Raw2Clr = toolsRaw2Color.SelectedColor.ToString();

            //these are colour ramp colours
            Properties.Settings.Default.crDBM1 = crDBM1.SelectedColor.Value.ToString();
            Properties.Settings.Default.crDBM2 = crDBM2.SelectedColor.Value.ToString();
            Properties.Settings.Default.crDBM3 = crDBM3.SelectedColor.Value.ToString();
            Properties.Settings.Default.crDBM4 = crDBM4.SelectedColor.Value.ToString();
            Properties.Settings.Default.crDBM5 = crDBM5.SelectedColor.Value.ToString();

            //these are the cut off values
            Properties.Settings.Default.dBm1Value = dBm1Value.Value.ToString();
            Properties.Settings.Default.dBm2Value = dBm2Value.Value.ToString();
            Properties.Settings.Default.dBm3Value = dBm3Value.Value.ToString();
            Properties.Settings.Default.dBm4Value = dBm4Value.Value.ToString();


            //   Properties.Settings.Default.chkHiLiteDX = (chkHiLiteDX.IsChecked==true);
            Properties.Settings.Default.Save();
            
            //clean up th UI


        }

        private void theirCall_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.dxCallSign.Text = "";
            mw.dxLastHeardTime.Text = "";
            mw.dxdbm.Text = "";
            mw.dxCount.Text = "";


        }

        private void myFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            // App.Current.Resources["FontSizeVal"] = 10.0;

            //Application.Current.MainWindow.FontSize = double.Parse(myFontSize.Text);

           // MainWindow mw = new MainWindow();
           //mw.FontSize = double.Parse(myFontSize.Text);


        }





    }
}
