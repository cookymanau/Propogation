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
    /// Interaction logic for graphSinglePlot.xaml
    /// </summary>
    public partial class graphSinglePlot : Window
    {


        public bool liveRedraw { get; set; }

        List<string> _thlist;

        double[] dataX = new double[6000];
        DateTime[] DTdates = new DateTime[6000];
        double [] Dubdates = new double[6000];

        double[] dataEUA = new double[6000]; //Average
        double[] dataEUR = new double[6000]; //Raw
        double[] dataEUC = new double[6000]; //Count


        double[] dataJAA = new double[6000];
        double[] dataJAR = new double[6000];
        double[] dataJAC = new double[6000];


        double[] dataNAA = new double[6000];
        double[] dataNAR = new double[6000];
        double[] dataNAC = new double[6000];

        double[] dataOCA = new double[6000];
        double[] dataOCR = new double[6000];
        double[] dataOCC = new double[6000];

        double[] dataAFA = new double[6000];
        double[] dataAFR = new double[6000];
        double[] dataAFC = new double[6000];

        double[] dataSAA = new double[6000];
        double[] dataSAR = new double[6000];
        double[] dataSAC = new double[6000];

        double[] dataFAA = new double[6000];
        double[] dataFAR = new double[6000];
        double[] dataFAC = new double[6000];
       
/// <summary>
/// Constructor
/// </summary>
/// <param name="thlist"></param>
        public graphSinglePlot(List<string> thlist)
        {
            InitializeComponent();
            _thlist = thlist;
           // _thlist.Add("");

            PrepareArrays();
           
            PlotTheLists();
        }


        public void PrepareArrays()
        {
            string[] wrdmsg= { };

            try
            {

            string time = "";
            int count = 0;
            foreach (var item in _thlist)
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
            catch (Exception ex)
            {
                frmMessageDialog md = new frmMessageDialog();
                md.messageBoxUpper.Text = $"Error in graphSinglePlot.PrepareArrays()  string is {wrdmsg}";
                md.messageBoxLower.Text = $"{ex}";
                md.Show();


            }

        }

        public void PlotTheLists()
        {
 

            ScottPlot.PlottableSignal sigFaAvg;
            ScottPlot.PlottableSignal sigFaCnt;


            //graphSingle.Background.Opacity = 0.5;
                        
            graphSingle.plt.PlotHLine(0, color: System.Drawing.Color.Black);
            
            if (chkEUGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataEUA, label: "EUAvg", markerSize: 0, lineWidth: 3, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkBlue);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataEUC, label: "EUCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataEUR, label: "EURaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);
            }

            if (chkJAGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataJAA, label: "JAAvg", markerSize: 0, lineWidth: 3, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkGray);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataJAC, label: "JACnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Gray);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataJAR, label: "JARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Gray);

            }

            if (chkNAGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataNAA, label: "NAAvg", markerSize: 0, lineWidth: 3, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkOliveGreen);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataNAC, label: "NACnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkOliveGreen);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataNAR, label: "NARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Olive);

            }

            if (chkOCGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                graphSingle.plt.PlotScatter(Dubdates,dataOCA, label: "OCAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkCyan);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates,dataOCC, label: "OCCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkCyan);
                if (chkRawPointsGraphs.IsChecked == true)
                     graphSingle.plt.PlotScatter(Dubdates,dataOCR, label: "OCRaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkCyan);

 
            }

            if (chkAFGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataAFA, label: "AFAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkGoldenrod);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataAFC, label: "AFCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Goldenrod);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataAFR, label: "AFRaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkGoldenrod);

            }

            if (chkSAGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataSAA, label: "SAAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkGreen);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataSAC, label: "SACnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkGreen);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataSAR, label: "SARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkGreen);

            }



            if (chkFAGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataFAA, label: "UsrAvg", markerSize: 0, lineWidth: 3, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkMagenta);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataFAC, label: "UsrCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Magenta);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataFAR, label: "UsrRaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Magenta);
            }




            graphSingle.plt.Legend(location:legendLocation.lowerLeft);
            graphSingle.plt.YLabel("dBm");
            graphSingle.plt.XLabel("Time(Zulu)");
            graphSingle.plt.Ticks(dateTimeX: true, dateTimeFormatStringX: "HH:mm:ss");

           

        }//end

        public void graphSingleRedraw_Click(object sender, RoutedEventArgs e)
        {

            if (chkLiveUpdate.IsChecked == true)
            {
                // now start the timer to process the UDP stuff now that we have started it.
                System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer2.Tick += new EventHandler(dispatcherTimer2_Tick);
                dispatcherTimer2.Interval = new TimeSpan(0, 0, 10);
                dispatcherTimer2.Start();

            }
            else
             redrawThePlot();
        }

        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            //code for timer2 in here
            redrawThePlot();
        }

        public void redrawThePlot()
        {


            //got to set the arrays back to full size , rinse and repeat style
            int count = 6000;
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

            PrepareArrays();
            graphSingle.plt.Clear();
            PlotTheLists();
            graphSingle.Render();

        }

    }
}
