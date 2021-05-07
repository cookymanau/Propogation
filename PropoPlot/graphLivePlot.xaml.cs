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
using ScottPlot;
using System.Drawing;

namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphLivePlot.xaml
    /// </summary>
    public partial class graphLivePlot : Window
    {

        const int arrSize = 5000;

        double[] dataX = new double[arrSize];
        DateTime[] DTdates = new DateTime[arrSize];
        double[] Dubdates = new double[arrSize];

        double[] dataEUA = new double[arrSize]; //Average
        double[] dataEUR = new double[arrSize]; //Raw
        double[] dataEUC = new double[arrSize]; //Count


        double[] dataJAA = new double[arrSize];
        double[] dataJAR = new double[arrSize];
        double[] dataJAC = new double[arrSize];


        double[] dataNAA = new double[arrSize];
        double[] dataNAR = new double[arrSize];
        double[] dataNAC = new double[arrSize];

        double[] dataOCA = new double[arrSize];
        double[] dataOCR = new double[arrSize];
        double[] dataOCC = new double[arrSize];
     
        double[] dataAFA = new double[arrSize];
        double[] dataAFR = new double[arrSize];
        double[] dataAFC = new double[arrSize];

        double[] dataSAA = new double[arrSize];
        double[] dataSAR = new double[arrSize];
        double[] dataSAC = new double[arrSize];

        double[] dataFAA = new double[arrSize];
        double[] dataFAR = new double[arrSize];
        double[] dataFAC = new double[arrSize];
        List<string> thlist;


        public graphLivePlot(List<string> alist)
        {
            InitializeComponent();
            thlist = alist;


        }

        private void btnPlot_Click(object sender, RoutedEventArgs e)
        {
            sizeTheArrays();
            PrepareArrays();
            PlotTheArrays();
        }


        public void PrepareArrays()
        {
            string[] wrdmsg = { };

            string time = "";
            int count = 0;
            foreach (var item in thlist)
            {
                // double defaultValue = 0;

                wrdmsg = item.Split(',');

                dataX[count] = count; //the X values

                DTdates[count] = DateTime.Parse(wrdmsg[1]);
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


                //   dataOCB[count] = $"OHLC:open={dataOCR[count]}";

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
            Array.Resize(ref Dubdates, count);

            Array.Resize(ref dataX, count);
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

        public void PlotTheArrays()
                {
                    //get the tools options user selected things

                    int AvgLineThickness = int.Parse(Properties.Settings.Default.AvgLineThick);
                    int RawLineThickness = int.Parse(Properties.Settings.Default.RawLineThick);
                    int CntLineThickness = int.Parse(Properties.Settings.Default.CntLineThick);
                    int LineAvgDotSize = int.Parse(Properties.Settings.Default.GraphAvgDotSize);
                    int LineRawDotSize = int.Parse(Properties.Settings.Default.GraphRawDotSize);
                    int LineCntDotSize = int.Parse(Properties.Settings.Default.GraphCntDotSize);



                 //   graphLive.plt.PlotHLine(0, color: System.Drawing.Color.Black);

                    //if (chkEUGraphs.IsChecked == true)
                    //{
                    //    if (chkAvgPointsGraphs.IsChecked == true)
                    //        graphLive.plt.PlotScatter(Dubdates, dataEUA, label: "EUAvg", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EUAvgColor)));
                    //graphLive.plt.PlotScatter(Dubdates, dataEUA, label: "EUAvg", markerSize: 0, lineWidth: 3, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkBlue);
                    //if (chkCountsGraphs.IsChecked == true)

              //     graphLive.plt.PlotScatter(Dubdates, dataEUC, label: "EUCnt", markerSize: LineCntDotSize, lineWidth: CntLineThickness, lineStyle:LineStyle.Solid  , color: (ColorTranslator.FromHtml(Properties.Settings.Default.EUCntColor)));

                    //graphLive.plt.PlotScatter(Dubdates, dataEUC, label: "EUCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);
                    //if (chkRawPointsGraphs.IsChecked == true)

                    //graphLive.plt.PlotScatter(Dubdates, dataEUR, label: "EURaw", markerSize: LineRawDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EURawColor)));
                    //graphLive.plt.PlotScatter(Dubdates, dataJAR, label: "JARaw", markerSize: LineRawDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JARawColor)));
                    //graphLive.plt.PlotScatter(Dubdates, dataOCR, label: "OCRaw", markerSize: LineRawDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCRawColor)));


            var fred1 = graphLive.Plot.AddScatter(Dubdates, dataEUR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EURawColor)), lineWidth: RawLineThickness,label:"EURaw");
            var fred2 = graphLive.Plot.AddScatter(Dubdates, dataEUA, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EUAvgColor)), lineWidth: RawLineThickness,label:"EUAvg");

            //graphLive.plt.PlotScatter(Dubdates, dataEUR, label: "EURaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);

            graphLive.Plot.Legend(true, Alignment.LowerLeft);
        
        }


            
        private void sizeTheArrays()
        {

             
            Array.Resize(ref Dubdates, arrSize);
            Array.Resize(ref dataX, arrSize);
            Array.Resize(ref dataEUR, arrSize);
            Array.Resize(ref dataEUA, arrSize);
            Array.Resize(ref dataEUC, arrSize);
            Array.Resize(ref dataJAR, arrSize);
            Array.Resize(ref dataJAA, arrSize);
            Array.Resize(ref dataJAC, arrSize);
            Array.Resize(ref dataNAR, arrSize);
            Array.Resize(ref dataNAA, arrSize);
            Array.Resize(ref dataNAC, arrSize);
            Array.Resize(ref dataOCR, arrSize);
            Array.Resize(ref dataOCA, arrSize);
            Array.Resize(ref dataOCC, arrSize);
            Array.Resize(ref dataAFR, arrSize);
            Array.Resize(ref dataAFA, arrSize);
            Array.Resize(ref dataAFC, arrSize);
            Array.Resize(ref dataSAR, arrSize);
            Array.Resize(ref dataSAA, arrSize);
            Array.Resize(ref dataSAC, arrSize);
            Array.Resize(ref dataFAR, arrSize);
            Array.Resize(ref dataFAA, arrSize);
            Array.Resize(ref dataFAC, arrSize);

        }





    }//end class
}//end
