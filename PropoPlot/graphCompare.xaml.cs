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

        double[] dataXf1 = new double[6000];
        double[] dataXf2 = new double[6000];
        DateTime[] DTdates = new DateTime[6000];
        double[] Dubdates = new double[6000];

        double[] dataEUA = new double[6000]; //Average
        double[] dataEUR = new double[6000]; //Raw
        double[] dataEUC = new double[6000]; //Count
        double[] dataEUA2 = new double[6000]; //Average
        double[] dataEUR2 = new double[6000]; //Raw
        double[] dataEUC2 = new double[6000]; //Count

        double[] dataJAA = new double[6000];
        double[] dataJAR = new double[6000];
        double[] dataJAC = new double[6000];
        double[] dataJAA2 = new double[6000];
        double[] dataJAR2 = new double[6000];
        double[] dataJAC2 = new double[6000];

        double[] dataNAA = new double[6000];
        double[] dataNAR = new double[6000];
        double[] dataNAC = new double[6000];
        double[] dataNAA2 = new double[6000];
        double[] dataNAR2 = new double[6000];
        double[] dataNAC2 = new double[6000];

        double[] dataOCA = new double[6000];
        double[] dataOCR = new double[6000];
        double[] dataOCC = new double[6000];
        double[] dataOCA2 = new double[6000];
        double[] dataOCR2 = new double[6000];
        double[] dataOCC2 = new double[6000];

        double[] dataAFA = new double[6000];
        double[] dataAFR = new double[6000];
        double[] dataAFC = new double[6000];
        double[] dataAFA2 = new double[6000];
        double[] dataAFR2 = new double[6000];
        double[] dataAFC2 = new double[6000];



        double[] dataSAA = new double[6000];
        double[] dataSAR = new double[6000];
        double[] dataSAC = new double[6000];
        double[] dataSAA2 = new double[6000];
        double[] dataSAR2 = new double[6000];
        double[] dataSAC2 = new double[6000];



        double[] dataFAA = new double[6000];
        double[] dataFAR = new double[6000];
        double[] dataFAC = new double[6000];
        double[] dataFAA2 = new double[6000];
        double[] dataFAR2 = new double[6000];
        double[] dataFAC2 = new double[6000];





      //  public string cFile1 { get; set; }




        public graphCompare()
        {
            InitializeComponent();
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
            try
            {


            
            //got to set the arrays back to full size , rinse and repeat style
            int count = 6000;
            // get rid of the 0's on the end
            Array.Resize(ref dataXf1, count);
            Array.Resize(ref dataXf2, count);
 
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
            try
            {


            string time = "";

            int count = 0;
            foreach (var item in listFile1)
            {
                // double defaultValue = 0;

                string[] wrdmsg = item.Split(',');

                dataXf1[count] = count; //the X values time
                //DTdates[count] = DateTime.Parse(wrdmsg[1]);

                //DateTime.TryParse(wrdmsg[1], out DTdates[count]);

               // Dubdates[count] = DTdates[count].ToOADate();


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

                count += 1;
            }

            // get rid of the 0's on the end
            Array.Resize(ref dataXf1, count);
            Array.Resize(ref Dubdates, count);

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
            }
            catch (Exception ex)
            {

            frmMessageDialog md = new frmMessageDialog();
            md.messageBoxUpper.Text = $"Error PrepareArraysFiles1() ";
            md.messageBoxLower.Text = $"{ex}";
            md.Show();
                
            }



        }

        public void PrepareArraysFile2()
        {

            try
            {


            string time = "";

            int count = 0;
            foreach (var item in listFile2)
            {
                // double defaultValue = 0;

                string[] wrdmsg = item.Split(',');

                dataXf2[count] = count; //the X values
                //DTdates[count] = DateTime.Parse(wrdmsg[1]);
               // DateTime.TryParse(wrdmsg[1], out DTdates[count]);
               // Dubdates[count] = DTdates[count].ToOADate();


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

                count += 1;
            }

            // get rid of the 0's on the end
            Array.Resize(ref Dubdates, count);
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

            }
            catch (Exception ex)
            {
                frmMessageDialog md = new frmMessageDialog();
                md.messageBoxUpper.Text = $"Error PrepareArraysFiles2() ";
                md.messageBoxLower.Text = $"{ex}";
                md.Show();


            }

        }





        public void PlotTheLists()
        {
            try
            {


            ScottPlot.PlottableSignal sigFaAvg;
            ScottPlot.PlottableSignal sigFaCnt;

            graphCompare2Files.plt.PlotHLine(0, color: System.Drawing.Color.Black);

            if (chkEUGraphs.IsChecked == true)
            {
                if (chkAvgsGraphs.IsChecked == true)
                {
                    graphCompare2Files.plt.PlotScatter(dataXf1, dataEUA, label: "EUAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);
                    graphCompare2Files.plt.PlotScatter(dataXf2, dataEUA2, label: "EUAvg2", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.LightBlue);
                }
                if (chkCountsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1, dataEUC, label: "EUCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1, dataEUR, label: "EURaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);
                if (chkSpline.IsChecked == true)
                {
                    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataEUA, resolution: 20);
                    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataEUA2, resolution: 20);
                    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Blue, label: "EU1Spline",markerSize:0);
                    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkBlue, label: "EU2Spline",markerSize:0);
                    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                }


            }

            if (chkJAGraphs.IsChecked == true)
            {
                if (chkAvgsGraphs.IsChecked == true)
                {

                    graphCompare2Files.plt.PlotScatter(dataXf1, dataJAA, label: "JAAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkRed);
                    graphCompare2Files.plt.PlotScatter(dataXf2, dataJAA2, label: "JAAvg2", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkSalmon);
                }
                if (chkCountsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1,dataJAC, label: "JACnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Maroon);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1,dataJAR, label: "JARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Maroon);
                if (chkSpline.IsChecked == true)
                {
                    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataJAA, resolution: 20);
                    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataJAA2, resolution: 20);
                    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Maroon, label: "JA1Spline", markerSize: 0);
                    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: "JA2Spline", markerSize: 0);
                    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                }


            }

            if (chkNAGraphs.IsChecked == true)
            {
                if (chkAvgsGraphs.IsChecked == true)
                {
                    graphCompare2Files.plt.PlotScatter(dataXf1, dataNAA, label: "NAAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Brown);
                    graphCompare2Files.plt.PlotScatter(dataXf2, dataNAA2, label: "NAAvg2", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.RosyBrown);
                }
                if (chkCountsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1, dataNAC, label: "NACnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Brown);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1, dataNAR, label: "NARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Brown);
  
                    if (chkSpline.IsChecked == true)
                {
                    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataNAA, resolution: 20);
                    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataNAA2, resolution: 20);
                    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: "NA1Spline", markerSize: 0);
                    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: "NA2Spline", markerSize: 0);
                    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                }



            }

            if (chkOCGraphs.IsChecked == true)
            {
                if (chkAvgsGraphs.IsChecked == true)
                {
                    graphCompare2Files.plt.PlotScatter(dataXf1, dataOCA, label: "OCAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkCyan);
                    graphCompare2Files.plt.PlotScatter(dataXf2, dataOCA2, label: "OCAvg2", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Cyan);
                }
                if (chkCountsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1,dataOCC, label: "OCCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkCyan);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1,dataOCR, label: "OCRaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkCyan);
                if (chkSpline.IsChecked == true)
                {
                    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataOCA, resolution: 20);
                    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataOCA2, resolution: 20);
                    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: "OC1Spline", markerSize: 0);
                    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: "OC2Spline", markerSize: 0);
                    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                }


            }

            if (chkAFGraphs.IsChecked == true)
            {
                if (chkAvgsGraphs.IsChecked == true)
                {
                    graphCompare2Files.plt.PlotScatter(dataXf1, dataAFA, label: "AFAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Red);
                    graphCompare2Files.plt.PlotScatter(dataXf2, dataAFA2, label: "AFAvg2", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.LightSalmon);
                }  
                if (chkCountsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1,dataAFC, label: "AFCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Red);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1,dataAFR, label: "AFRaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Red);
                if (chkSpline.IsChecked == true)
                {
                    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataAFA, resolution: 20);
                    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataAFA2, resolution: 20);
                    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: "AF1Spline", markerSize: 0);
                    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: "AF2Spline", markerSize: 0);
                    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                }


            }

            if (chkSAGraphs.IsChecked == true)
            {
                if (chkAvgsGraphs.IsChecked == true)
                {

                    graphCompare2Files.plt.PlotScatter(dataXf1, dataSAA, label: "SAAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Green);
                    graphCompare2Files.plt.PlotScatter(dataXf2, dataSAA2, label: "SAAvg2", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.LightGreen);
                }
                if (chkCountsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1,dataSAC, label: "SACnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Green);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1,dataSAR, label: "SARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Green);
                if (chkSpline.IsChecked == true)
                {
                    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataSAA, resolution: 20);
                    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataSAA2, resolution: 20);
                    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: "SA1Spline", markerSize: 0);
                    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: "SA2Spline", markerSize: 0);
                    //   var psi = new ScottPlot.Statistics.Interpolation.PeriodicSpline(dataXf1, dataFAA, resolution: 20);
                    //   var esi = new ScottPlot.Statistics.Interpolation.EndSlopeSpline(dataXf1, dataFAA, resolution: 20);
                }



            }

            if (chkFAGraphs.IsChecked == true)
            {
                if (chkAvgsGraphs.IsChecked == true)
                {

                    graphCompare2Files.plt.PlotScatter(dataXf1, dataFAA, label: "FaAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Magenta);
                    graphCompare2Files.plt.PlotScatter(dataXf2, dataFAA2, label: "FaAvg2", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.LightPink);
                }
                if (chkCountsGraphs.IsChecked == true)
                  graphCompare2Files.plt.PlotScatter(dataXf1,dataFAC, label: "FaCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Magenta);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphCompare2Files.plt.PlotScatter(dataXf1,dataFAR, label: "FARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Magenta);

                if (chkSpline.IsChecked == true)
                {
                    var nsi = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf1, dataFAA, resolution: 20);
                    var nsi2 = new ScottPlot.Statistics.Interpolation.NaturalSpline(dataXf2, dataFAA2, resolution: 20);
                    graphCompare2Files.plt.PlotScatter(nsi.interpolatedXs, nsi.interpolatedYs, System.Drawing.Color.Magenta, label: "USR1Spline", markerSize: 1);
                    graphCompare2Files.plt.PlotScatter(nsi2.interpolatedXs, nsi2.interpolatedYs, System.Drawing.Color.DarkMagenta, label: "USR2Spline", markerSize: 1);
                }
            }


            graphCompare2Files.plt.Legend(location: legendLocation.lowerLeft);
            graphCompare2Files.plt.YLabel("dBm");
            graphCompare2Files.plt.XLabel("RecordNumber");
            // graphCompare2Files.plt.XLabel("Time(Zulu)");
            //   graphCompare2Files.plt.Ticks(dateTimeX: true, dateTimeFormatStringX: "HH:mm:ss");

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












    }

}
