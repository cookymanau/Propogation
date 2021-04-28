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

        List<string> _thlist;

        double[] dataX = new double[6000];

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
       
        public graphSinglePlot(List<string> thlist)
        {
            InitializeComponent();
            _thlist = thlist;

            PrepareArrays();
           
            PlotTheLists();

        }


        private void PrepareArrays()
        {

            string time = "";



            int count = 0;
            foreach (var item in _thlist)
            {
                // double defaultValue = 0;

                string[] wrdmsg = item.Split(',');

                dataX[count] = count; //the X values

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

        private void PlotTheLists()
        {
 

            ScottPlot.PlottableSignal sigFaAvg;
            ScottPlot.PlottableSignal sigFaCnt;


            //graphSingle.Background.Opacity = 0.5;
                        
            graphSingle.plt.PlotHLine(0, color: System.Drawing.Color.Black);
            
            if (chkEUGraphs.IsChecked == true)
            {
                graphSingle.plt.PlotSignal(dataEUA, label: "EUAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);
                if(chkCountsGraphs.IsChecked == true)
                graphSingle.plt.PlotSignal(dataEUC, label: "EUCnt", markerSize: 0, lineWidth: 2, lineStyle:LineStyle.Solid,    color: System.Drawing.Color.Blue);
                if(chkRawPointsGraphs.IsChecked == true)
                graphSingle.plt.PlotSignal(dataEUR, label: "EURaw", markerSize: 0, lineStyle:LineStyle.Solid,    color: System.Drawing.Color.Blue);
            }

            if (chkJAGraphs.IsChecked == true)
            {
                graphSingle.plt.PlotSignal(dataJAA, label: "JAAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Maroon);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataJAC, label: "JACnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.DashDotDot, color: System.Drawing.Color.Maroon);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataJAR, label: "JARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Maroon);

            }

            if (chkNAGraphs.IsChecked == true)
            {
                graphSingle.plt.PlotSignal(dataNAA, label: "NAAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Brown);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataNAC, label: "NACnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Dash, color: System.Drawing.Color.Brown);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataNAR, label: "NARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Brown);

            }

            if (chkOCGraphs.IsChecked == true)
            {
                graphSingle.plt.PlotSignal(dataOCA, label: "OCAvg", markerSize: 0, lineWidth:2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkCyan);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataOCC, label: "OCCnt", markerSize: 0, lineWidth:2, lineStyle : LineStyle.Dot, color: System.Drawing.Color.DarkCyan);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataOCR, label: "OCRaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkCyan);

            }

            if (chkAFGraphs.IsChecked == true)
            {
                graphSingle.plt.PlotSignal(dataAFA, label: "AFAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Red);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataAFC, label: "AFCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Dot, color: System.Drawing.Color.Red);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataAFR, label: "AFRaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Red);

            }

            if (chkSAGraphs.IsChecked == true)
            {
                graphSingle.plt.PlotSignal(dataSAA, label: "SAAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Green);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataSAC, label: "SACnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Dot, color: System.Drawing.Color.Green );
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataSAR, label: "SARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Green);

            }



            if (chkFAGraphs.IsChecked == true)
            {
            sigFaAvg = graphSingle.plt.PlotSignal(dataFAA,label:"FaAvg", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Magenta);
                if (chkCountsGraphs.IsChecked == true)
                    sigFaCnt = graphSingle.plt.PlotSignal(dataFAC,label:"FaCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Dot, color:System.Drawing.Color.Magenta);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotSignal(dataFAR, label: "FARaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Magenta);


                //sigFaAvg.color = Color.R
                //sigFaAvg.visible = true;
                //sigFaCnt.visible = true;
            }




            graphSingle.plt.Legend(location:legendLocation.lowerLeft);
            graphSingle.plt.YLabel("dBm");
            graphSingle.plt.XLabel("Periods");

           

        }//end

        private void graphSingleRedraw_Click(object sender, RoutedEventArgs e)
        {
           //got to set the arrays back to full size , rinse and repeat style
            int count = 6000;
            // get rid of the 0's on the end
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
