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
using System.IO;
using System.Windows.Controls;
using ScottPlot;
using System.Drawing;


namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphCompare.xaml
    /// </summary>
    public partial class graphCompare : Window
    {
        //a couple of lists to write the contents of the csv to
        List<string> listFile1 = new List<string>();
        List<string> listFile2 = new List<string>();

        const int arrSize = 5000;

        double[] dataXf1 = new double[arrSize];
        double[] dataXf2 = new double[arrSize];

        DateTime[] DTdates = new DateTime[arrSize];
        double[] Dubdates = new double[arrSize];

        DateTime[] DTdates2 = new DateTime[arrSize];
        double[] Dubdates2 = new double[arrSize];




        double[] dataEUA = new double[arrSize]; //Average
        double[] dataEUR = new double[arrSize]; //Raw
        double[] dataEUC = new double[arrSize]; //Count
        double[] dataEUA2 = new double[arrSize]; //Average
        double[] dataEUR2 = new double[arrSize]; //Raw
        double[] dataEUC2 = new double[arrSize]; //Count

        double[] dataJAA = new double[arrSize];
        double[] dataJAR = new double[arrSize];
        double[] dataJAC = new double[arrSize];
        double[] dataJAA2 = new double[arrSize];
        double[] dataJAR2 = new double[arrSize];
        double[] dataJAC2 = new double[arrSize];

        double[] dataNAA = new double[arrSize];
        double[] dataNAR = new double[arrSize];
        double[] dataNAC = new double[arrSize];
        double[] dataNAA2 = new double[arrSize];
        double[] dataNAR2 = new double[arrSize];
        double[] dataNAC2 = new double[arrSize];

        double[] dataOCA = new double[arrSize];
        double[] dataOCR = new double[arrSize];
        double[] dataOCC = new double[arrSize];
        double[] dataOCA2 = new double[arrSize];
        double[] dataOCR2 = new double[arrSize];
        double[] dataOCC2 = new double[arrSize];

        double[] dataAFA = new double[arrSize];
        double[] dataAFR = new double[arrSize];
        double[] dataAFC = new double[arrSize];
        double[] dataAFA2 = new double[arrSize];
        double[] dataAFR2 = new double[arrSize];
        double[] dataAFC2 = new double[arrSize];



        double[] dataSAA = new double[arrSize];
        double[] dataSAR = new double[arrSize];
        double[] dataSAC = new double[arrSize];
        double[] dataSAA2 = new double[arrSize];
        double[] dataSAR2 = new double[arrSize];
        double[] dataSAC2 = new double[arrSize];



        double[] dataFAA = new double[arrSize];
        double[] dataFAR = new double[arrSize];
        double[] dataFAC = new double[arrSize];
        double[] dataFAA2 = new double[arrSize];
        double[] dataFAR2 = new double[arrSize];
        double[] dataFAC2 = new double[arrSize];



        string usrLabel = Properties.Settings.Default.UsrDefinedName + "Raw";
        string usrLabelAvg = Properties.Settings.Default.UsrDefinedName + "Avg";
        string usrLabelAvg2 = Properties.Settings.Default.UsrDefinedName + "Avg2";
        string usrLabelCnt = Properties.Settings.Default.UsrDefinedName + "Cnt";

        public graphCompare()
        {
            InitializeComponent();
            chkFAGraphs.Content = Properties.Settings.Default.UsrDefinedName;
        }

        private void fileOpen1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "PropoAverages"; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "PropoPlot documents (.csv)|*.csv|All files (*.*)|*.*"; // Filter files by extension
                                                                                 // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;  // the name of the file to read

                using (var file = new StreamReader(filename))
                {
                    var line = string.Empty;

                    while ((line = file.ReadLine()) != null)
                    {
                        listFile1.Add(line);
                    }
                }
            }//end if result == true


            cfFileName1.Text = dlg.FileName;



        }

        private void fileOpen2_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "PropoAverages"; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "PropoPlot documents (.csv)|*.csv|All files (*.*)|*.*"; // Filter files by extension
                                                                                 // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;  // the name of the file to read

                using (var file = new StreamReader(filename))
                {
                    var line = string.Empty;

                    while ((line = file.ReadLine()) != null)
                    {
                        listFile2.Add(line);
                    }
                }
            }//end if result == true
           
          
            cfFileName2.Text = dlg.FileName;

        }

        private void graphCompareRedraw_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void redraw()
        {


            try
            {



                //got to set the arrays back to full size , rinse and repeat style
                int count = arrSize;
                // get rid of the 0's on the end
                Array.Resize(ref dataXf1, count);
                Array.Resize(ref dataXf2, count);
                Array.Resize(ref Dubdates, count);
                Array.Resize(ref Dubdates2, count);


                Array.Resize(ref dataEUR, count);
                Array.Resize(ref dataEUA, count);
                Array.Resize(ref dataEUC, count);
                Array.Resize(ref dataJAR, count);
                Array.Resize(ref dataJAA, count);
                Array.Resize(ref dataJAC, count);
                Array.Resize(ref dataNAR, count);
                Array.Resize(ref dataNAA, count);
                Array.Resize(ref dataNAC, count);
                Array.Resize(ref dataOCR, count);
                Array.Resize(ref dataOCA, count);
                Array.Resize(ref dataOCC, count);
                Array.Resize(ref dataAFR, count);
                Array.Resize(ref dataAFA, count);
                Array.Resize(ref dataAFC, count);
                Array.Resize(ref dataSAR, count);
                Array.Resize(ref dataSAA, count);
                Array.Resize(ref dataSAC, count);
                Array.Resize(ref dataFAR, count);
                Array.Resize(ref dataFAA, count);
                Array.Resize(ref dataFAC, count);

                Array.Resize(ref dataEUR2, count);
                Array.Resize(ref dataEUA2, count);
                Array.Resize(ref dataEUC2, count);
                Array.Resize(ref dataJAR2, count);
                Array.Resize(ref dataJAA2, count);
                Array.Resize(ref dataJAC2, count);
                Array.Resize(ref dataNAR2, count);
                Array.Resize(ref dataNAA2, count);
                Array.Resize(ref dataNAC2, count);
                Array.Resize(ref dataOCR2, count);
                Array.Resize(ref dataOCA2, count);
                Array.Resize(ref dataOCC2, count);
                Array.Resize(ref dataAFR2, count);
                Array.Resize(ref dataAFA2, count);
                Array.Resize(ref dataAFC2, count);
                Array.Resize(ref dataSAR2, count);
                Array.Resize(ref dataSAA2, count);
                Array.Resize(ref dataSAC2, count);
                Array.Resize(ref dataFAR2, count);
                Array.Resize(ref dataFAA2, count);
                Array.Resize(ref dataFAC2, count);

                graphCompare2Files.plt.Clear();

                PrepareArraysFile1();
                PrepareArraysFile2();
                PlotTheLists();
                // graphCompare2Files.Render();
            }
            catch (Exception ex)
            {

                frmMessageDialog md = new frmMessageDialog();
                md.messageBoxUpper.Text = $"Error Redraw() ";
                md.messageBoxLower.Text = $"{ex}";
                md.Show();

            }



        }






        private void graphCompareFile_Click(object sender, RoutedEventArgs e)
        {

            // press this button, and prepare the data and plot
            //graphCompare2FilesPlot gsp = new(listFile1);

            PrepareArraysFile1();            
            PrepareArraysFile2();
            PlotTheLists();

        }




        public void PrepareArraysFile1()
        {
            //try
            //{


            string time = "";

            int count = 0;
            foreach (var item in listFile1.Skip(0))
            {
                if (count > 0)
                {


                    string[] wrdmsg = item.Split(',');

                    dataXf1[count] = count; //the X values time

                    DTdates[count] = DateTime.Parse(wrdmsg[1]);
                    DateTime.TryParse(wrdmsg[1], out DTdates[count]);
                    Dubdates[count] = DTdates[count].ToOADate();




                    double.TryParse(wrdmsg[2], out dataEUR[count]); //Europe
                    double.TryParse(wrdmsg[3], out dataEUA[count]); //EuropeAverage
                    double.TryParse(wrdmsg[4], out dataEUC[count]); //EuropeAverage

                    double.TryParse(wrdmsg[5], out dataJAR[count]); //Japan
                    double.TryParse(wrdmsg[6], out dataJAA[count]); //JapanAvg
                    double.TryParse(wrdmsg[7], out dataJAC[count]); //JapanAvg

                    double.TryParse(wrdmsg[8], out dataNAR[count]);
                    double.TryParse(wrdmsg[9], out dataNAA[count]);
                    double.TryParse(wrdmsg[10], out dataNAC[count]);

                    double.TryParse(wrdmsg[11], out dataOCR[count]);
                    double.TryParse(wrdmsg[12], out dataOCA[count]);
                    double.TryParse(wrdmsg[13], out dataOCC[count]);

                    double.TryParse(wrdmsg[14], out dataAFR[count]);
                    double.TryParse(wrdmsg[15], out dataAFA[count]);
                    double.TryParse(wrdmsg[16], out dataAFC[count]);

                    double.TryParse(wrdmsg[17], out dataSAR[count]);
                    double.TryParse(wrdmsg[18], out dataSAA[count]);
                    double.TryParse(wrdmsg[19], out dataSAC[count]);

                    double.TryParse(wrdmsg[20], out dataFAR[count]);
                    double.TryParse(wrdmsg[21], out dataFAA[count]);
                    double.TryParse(wrdmsg[22], out dataFAC[count]);
                    //dataOC[count] = double.TryParse(wrdmsg[6],out 0);

                }
                    count += 1;
            }

            // get rid of the 0's on the end
            Array.Resize(ref dataXf1, count);
            Array.Resize(ref Dubdates, count);
            Dubdates[0] = Dubdates[1];  //because the first record will ALWAYS be 0

            Array.Resize(ref dataEUR, count);
            Array.Resize(ref dataEUA, count);
            Array.Resize(ref dataEUC, count);

            Array.Resize(ref dataJAR, count);
            Array.Resize(ref dataJAA, count);
            Array.Resize(ref dataJAC, count);

            Array.Resize(ref dataNAR, count);
            Array.Resize(ref dataNAA, count);
            Array.Resize(ref dataNAC, count);

            Array.Resize(ref dataOCR, count);
            Array.Resize(ref dataOCA, count);
            Array.Resize(ref dataOCC, count);

            Array.Resize(ref dataAFR, count);
            Array.Resize(ref dataAFA, count);
            Array.Resize(ref dataAFC, count);
            Array.Resize(ref dataSAR, count);
            Array.Resize(ref dataSAA, count);
            Array.Resize(ref dataSAC, count);
            Array.Resize(ref dataFAR, count);
            Array.Resize(ref dataFAA, count);
            Array.Resize(ref dataFAC, count);
            //}
            //catch (Exception ex)
            //{

            //frmMessageDialog md = new frmMessageDialog();
            //md.messageBoxUpper.Text = $"Error PrepareArraysFiles1() ";
            //md.messageBoxLower.Text = $"{ex}";
            //md.Show();
                
            //}



        }

        public void PrepareArraysFile2()
        {

            //try
            //{


            string time = "";

            int count = 0;
            foreach (var item in listFile2.Skip(0))
            {
                // double defaultValue = 0;

                if (count > 0)
                {

                    string[] wrdmsg = item.Split(',');

                    dataXf2[count] = count; //the X values
                    DTdates2[count] = DateTime.Parse(wrdmsg[1]);
                    DateTime.TryParse(wrdmsg[1], out DTdates2[count]);
                    Dubdates2[count] = DTdates2[count].ToOADate();


                    double.TryParse(wrdmsg[2], out dataEUR2[count]); //Europe
                    double.TryParse(wrdmsg[3], out dataEUA2[count]); //EuropeAverage
                    double.TryParse(wrdmsg[4], out dataEUC2[count]); //EuropeAverage

                    double.TryParse(wrdmsg[5], out dataJAR2[count]); //Japan
                    double.TryParse(wrdmsg[6], out dataJAA2[count]); //JapanAvg
                    double.TryParse(wrdmsg[7], out dataJAC2[count]); //JapanAvg

                    double.TryParse(wrdmsg[8], out dataNAR2[count]);
                    double.TryParse(wrdmsg[9], out dataNAA2[count]);
                    double.TryParse(wrdmsg[10], out dataNAC2[count]);

                    double.TryParse(wrdmsg[11], out dataOCR2[count]);
                    double.TryParse(wrdmsg[12], out dataOCA2[count]);
                    double.TryParse(wrdmsg[13], out dataOCC2[count]);

                    double.TryParse(wrdmsg[14], out dataAFR2[count]);
                    double.TryParse(wrdmsg[15], out dataAFA2[count]);
                    double.TryParse(wrdmsg[16], out dataAFC2[count]);

                    double.TryParse(wrdmsg[17], out dataSAR2[count]);
                    double.TryParse(wrdmsg[18], out dataSAA2[count]);
                    double.TryParse(wrdmsg[19], out dataSAC2[count]);

                    double.TryParse(wrdmsg[20], out dataFAR2[count]);
                    double.TryParse(wrdmsg[21], out dataFAA2[count]);
                    double.TryParse(wrdmsg[22], out dataFAC2[count]);
                    //dataOC[count] = double.TryParse(wrdmsg[6],out 0);

                }
                    count += 1;
            }

            // get rid of the 0's on the end
            Array.Resize(ref Dubdates2, count);

            Dubdates2[0] = Dubdates2[1];  //because the first record will ALWAYS be 0

            Array.Resize(ref dataXf2, count);

            Array.Resize(ref dataEUR2, count);
            Array.Resize(ref dataEUA2, count);
            Array.Resize(ref dataEUC2, count);

            Array.Resize(ref dataJAR2, count);
            Array.Resize(ref dataJAA2, count);
            Array.Resize(ref dataJAC2, count);

            Array.Resize(ref dataNAR2, count);
            Array.Resize(ref dataNAA2, count);
            Array.Resize(ref dataNAC2, count);

            Array.Resize(ref dataOCR2, count);
            Array.Resize(ref dataOCA2, count);
            Array.Resize(ref dataOCC2, count);

            Array.Resize(ref dataAFR2, count);
            Array.Resize(ref dataAFA2, count);
            Array.Resize(ref dataAFC2, count);
            Array.Resize(ref dataSAR2, count);
            Array.Resize(ref dataSAA2, count);
            Array.Resize(ref dataSAC2, count);
            Array.Resize(ref dataFAR2, count);
            Array.Resize(ref dataFAA2, count);
            Array.Resize(ref dataFAC2, count);

            //}
            //catch (Exception ex)
            //{
            //    frmMessageDialog md = new frmMessageDialog();
            //    md.messageBoxUpper.Text = $"Error PrepareArraysFiles2() ";
            //    md.messageBoxLower.Text = $"{ex}";
            //    md.Show();


            //}

        }



        ScottPlot.Plottable.ScatterPlot sigEUA;

        public void PlotTheLists()
        {
            try
            {
                int AvgLineThickness = int.Parse(Properties.Settings.Default.AvgLineThick);
                int Avg2LineThickness = int.Parse(Properties.Settings.Default.Avg2LineThick);
                int RawLineThickness = int.Parse(Properties.Settings.Default.RawLineThick);
                int Raw2LineThickness = int.Parse(Properties.Settings.Default.Raw2LineThick);
                int CntLineThickness = int.Parse(Properties.Settings.Default.CntLineThick);
                int LineAvgDotSize = int.Parse(Properties.Settings.Default.GraphAvgDotSize);
                int LineRawDotSize = int.Parse(Properties.Settings.Default.GraphRawDotSize);
                int LineCntDotSize = int.Parse(Properties.Settings.Default.GraphCntDotSize);

            //    ScottPlot.PlottableSignal sigFaAvg;
           // ScottPlot.PlottableSignal sigFaCnt;

           

            if (chkEUGraphs.IsChecked == true)
            {
                if (chkAvgsGraphs.IsChecked == true)
                {
                    graphCompare2Files.Plot.AddScatter(Dubdates, dataEUA, label: $"EUAvg {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EUAvgColor)));
                    graphCompare2Files.Plot.AddScatter(Dubdates2, dataEUA2, label: $"EUAvg {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Avg2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Avg2Clr)));
                }
                    if (chkCountsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataEUC, label: $"EUCnt", markerSize: LineRawDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EUCntColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataEUC2, label: $"EUCnt2", markerSize: LineRawDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Cnt2Clr)));
                    }
                    if (chkRawPointsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataEUR, label: "EURaw", markerSize: LineAvgDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EURawColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataEUR2, label: "EURaw2", markerSize: LineAvgDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Raw2Clr)));

                    }
                   // if (chkSpline.IsChecked == true)
                    //{
                    //    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataEUA, resolution: 20);
                    //    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataEUA2, resolution: 20);
                    //    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Blue, label: $"EU1Spline {cfFileName1.Text}",markerSize:0);
                    //    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkBlue, label: $"EU2Spline {cfFileName2.Text}",markerSize:0);
                    //    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                    //}
                }

                if (chkJAGraphs.IsChecked == true)
                {
                    if (chkAvgsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataJAA, label: $"JAAvg {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JAAvgColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataJAA2, label: $"JAAvg2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Avg2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Avg2Clr)));

                    }
                    if (chkCountsGraphs.IsChecked == true) {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataJAC, label: $"JACnt {cfFileName1.Text}", markerSize: LineRawDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JACntColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataJAC2, label: $"JACnt2 {cfFileName1.Text}", markerSize: LineRawDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Cnt2Clr)));
                    }
                    if (chkRawPointsGraphs.IsChecked == true) { 
                    graphCompare2Files.Plot.AddScatter(Dubdates, dataJAR, label: $"JARaw {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JARawColor)));
                    graphCompare2Files.Plot.AddScatter(Dubdates2, dataJAR2, label: $"JARaw2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Raw2Clr)));
                }
//                    if (chkSpline.IsChecked == true)
                //{
                //    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataJAA, resolution: 20);
                //    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataJAA2, resolution: 20);
                //    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Maroon, label: $"JA1Spline {cfFileName1.Text}", markerSize: 0);
                //    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: $"JA2Spline {cfFileName2.Text}", markerSize: 0);
                //    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                //    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                //}
                }

                if (chkNAGraphs.IsChecked == true)
                {
                    if (chkAvgsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataNAA, label: $"NAAvg {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NAAvgColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataNAA2, label: $"NAAvg2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Avg2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Avg2Clr)));
                    }
                if (chkCountsGraphs.IsChecked == true)
                {
                    graphCompare2Files.Plot.AddScatter(Dubdates, dataNAC, label: $"NACnt {cfFileName1.Text}", markerSize: LineRawDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NACntColor)));
                    graphCompare2Files.Plot.AddScatter(Dubdates2, dataNAC2, label: $"NACnt2 {cfFileName2.Text}", markerSize: LineRawDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Cnt2Clr)));
                }
                if (chkRawPointsGraphs.IsChecked == true)
                {
                    graphCompare2Files.Plot.AddScatter(Dubdates, dataNAR, label: $"NARaw {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NARawColor)));
                    graphCompare2Files.Plot.AddScatter(Dubdates2, dataNAR2, label: $"NARaw2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Raw2Clr)));
                }
                    //    if (chkSpline.IsChecked == true)
                    //    {
                    //        var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataNAA, resolution: 20);
                    //        var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataNAA2, resolution: 20);
                    //        graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: $"NA1Spline {cfFileName1.Text}", markerSize: 0);
                    //        graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: $"NA2Spline {cfFileName2.Text}", markerSize: 0);
                    //        //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //        //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                    //    }
                }

                if (chkOCGraphs.IsChecked == true)
                {
                    if (chkAvgsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataOCA, label: $"OCAvg {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCAvgColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataOCA2, label: $"OCAvg2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Avg2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Avg2Clr)));
                    }
                    if (chkCountsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataOCC, label: $"OCCnt {cfFileName1.Text}", markerSize: LineRawDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCCntColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataOCC2, label: $"OCCnt {cfFileName2.Text}", markerSize: LineRawDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Cnt2Clr)));
                    }
                    if (chkRawPointsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataOCR, label: $"OCRaw {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCRawColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataOCR2, label: $"OCRaw2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Raw2Clr)));
                    }
                    //if (chkSpline.IsChecked == true)
                    //{
                    //    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataOCA, resolution: 20);
                    //    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataOCA2, resolution: 20);
                    //    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: $"OC1Spline {cfFileName1.Text}", markerSize: 0);
                    //    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: $"OC2Spline {cfFileName2.Text}", markerSize: 0);
                    //    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                    //}
                }

                if (chkAFGraphs.IsChecked == true)
                {
                    if (chkAvgsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataAFA, label: $"AFAvg {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.AFAvgColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataAFA2, label: $"AFAvg2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Avg2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Avg2Clr)));
                    }
                    if (chkCountsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataAFC, label: $"AFCnt {cfFileName1.Text}", markerSize: LineRawDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.AFCntColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataAFC2, label: $"AFCnt2 {cfFileName2.Text}", markerSize: LineRawDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Cnt2Clr)));
                    }
                    if (chkRawPointsGraphs.IsChecked == true) { 
                    graphCompare2Files.Plot.AddScatter(Dubdates, dataAFR, label: $"AFRaw {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.AFRawColor)));
                    graphCompare2Files.Plot.AddScatter(Dubdates2, dataAFR2, label: $"AFRaw2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Raw2Clr)));
                }
                 //   if (chkSpline.IsChecked == true)
                //    {
                //        var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataAFA, resolution: 20);
                //        var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataAFA2, resolution: 20);
                //        graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: $"AF1Spline {cfFileName1.Text}", markerSize: 0);
                //        graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: $"AF2Spline {cfFileName2.Text}", markerSize: 0);
                //        //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                //        //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                //    }
                }

                if (chkSAGraphs.IsChecked == true)
                {
                    if (chkAvgsGraphs.IsChecked == true)
                    {

                        graphCompare2Files.Plot.AddScatter(Dubdates, dataSAA, label: $"SAAvg {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.SAAvgColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataSAA2, label: $"SAAvg2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Avg2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Avg2Clr)));
                    }
                    if (chkCountsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataSAC, label: $"SACnt {cfFileName1.Text}", markerSize: LineRawDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.SACntColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataSAC2, label: $"SACnt2 {cfFileName2.Text}", markerSize: LineRawDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Cnt2Clr)));
                    }
                    if (chkRawPointsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataSAR, label: $"SARaw {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.SARawColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataSAR2, label: $"SARaw2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Raw2Clr)));
                    }
                    //if (chkSpline.IsChecked == true)
                    //{
                    //    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataSAA, resolution: 20);
                    //    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataSAA2, resolution: 20);
                    //    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: $"SA1Spline {cfFileName1.Text}", markerSize: 0);
                    //    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: $"SA2Spline {cfFileName2.Text}", markerSize: 0);
                    //    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                    //}
                }

                if (chkFAGraphs.IsChecked == true)
                {
                    if (chkAvgsGraphs.IsChecked == true)
                    {

                        graphCompare2Files.Plot.AddScatter(Dubdates, dataFAA, label: $"{usrLabelAvg}:{cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.FAAvgColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataFAA2, label: $"{usrLabelAvg2}: {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Avg2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Avg2Clr)));
                    }
                    if (chkCountsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataFAC, label: $"{usrLabelCnt} {cfFileName1.Text}", markerSize: LineRawDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.FACntColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataFAC2, label: $"{usrLabel}2 {cfFileName2.Text}", markerSize: LineRawDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Cnt2Clr)));
                    }
                    if (chkRawPointsGraphs.IsChecked == true)
                    {
                        graphCompare2Files.Plot.AddScatter(Dubdates, dataFAR, label: $"{usrLabel} {cfFileName1.Text}", markerSize: LineAvgDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.FARawColor)));
                        graphCompare2Files.Plot.AddScatter(Dubdates2, dataFAR2, label: $"{usrLabel}2 {cfFileName2.Text}", markerSize: LineAvgDotSize, lineWidth: Raw2LineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.Raw2Clr)));
                    }

                    //if (chkSpline.IsChecked == true)
                    //{
                    //    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataFAA, resolution: 20);
                    //    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataFAA2, resolution: 20);
                    //    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: $"USR1Spline {cfFileName1.Text}", markerSize: 1);
                    //    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: $"USR2Spline {cfFileName2.Text}", markerSize: 1);
                    //}
                }


                 graphCompare2Files.Plot.Legend(location: Alignment.LowerLeft);
                 graphCompare2Files.Plot.YLabel("dBm");
                 graphCompare2Files.Plot.XLabel("Time(Zulu)");
               
                graphCompare2Files.Plot.XAxis.DateTimeFormat(true);
                graphCompare2Files.Plot.XAxis.TickLabelFormat("HH:mm:ss", true);
                




                // graphCompare2Files.plt.XLabel("Time(Zulu)");

                graphCompare2Files.plt.PlotHLine(0, color: System.Drawing.Color.Black, draggable:true, lineStyle: LineStyle.DashDotDot);
                graphCompare2Files.plt.PlotHLine(0, color: System.Drawing.Color.Red,draggable:true, lineStyle: LineStyle.DashDot);


                graphCompare2Files.Render();
            }
            catch (Exception ex)
            {
                frmMessageDialog md = new frmMessageDialog();
                md.messageBoxUpper.Text = $"Error in PlotTheList() ";
                md.messageBoxLower.Text = $"{ex}";
                md.Show();

            }


        }//end

        private void chkEUGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkJAGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkNAGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkOCGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkAFGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkSAGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkFAGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkCountsGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkAvgsGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkRawPointsGraphs_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void chkSpline_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }
    }

}
