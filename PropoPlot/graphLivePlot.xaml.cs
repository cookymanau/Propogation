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
using System.Threading.Tasks;
using System.Diagnostics; //for the stopwatch
using System.Windows.Threading;

namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphLivePlot.xaml
    /// </summary>
    public partial class graphLivePlot : Window
    {

        const int arrSize = 50;

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

        int currentArrSize = 0;

        ScottPlot.Plottable.SignalPlot sig1;
        ScottPlot.Plottable.SignalPlot sig2;
        ScottPlot.Plottable.SignalPlot sig3;


        /// <summary>
        /// The Constructor - happens only once
        /// </summary>
        /// <param name="alist"></param>
        public graphLivePlot(List<string> alist)
        {
            InitializeComponent();

            thlist = alist;

     
            
            PrepareArrays();

          //  sizeTheArrays(currentArrSize);

            PlotTheArrays();
            
        }

        private void btnPlot_Click(object sender, RoutedEventArgs e)
        {

            PrepareArrays();  //Load new data into them from the list of strings

            sig1.MaxRenderIndex = (currentArrSize-1);
            sig2.MaxRenderIndex = (currentArrSize-1);
            sig3.MaxRenderIndex = (currentArrSize-1);
            //  sizeTheArrays(currentArrSize);

           

            graphLive.Render();

       }


        public void PrepareArrays()
        {
            string[] wrdmsg = { };

            string time = "";
            int count = 0;
           
            sizeTheArrays(arrSize);

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

            currentArrSize = count;
        }

        public void PlotTheArrays()
                {
                    //get the tools options user selected things

  


           // var fred4 = graphLive.Plot.AddScatter(Dubdates, dataNAR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NARawColor)), lineWidth: RawLineThickness,label:"NARaw");
            sig1 =  graphLive.Plot.AddSignal(dataOCR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCRawColor)), label:"OCRaw");
            sig2 = graphLive.Plot.AddSignal(dataEUR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EURawColor)), label:"EURaw");
            sig3 = graphLive.Plot.AddSignal(dataNAR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NARawColor)), label:"NARaw");


           
         //   sig.MaxRenderIndex = currentArrSize;
         //   sig2.MaxRenderIndex = currentArrSize;

            graphLive.Plot.Legend(location: Alignment.LowerLeft);

        }


            
        private void sizeTheArrays(int resizeSize)
        {


             //this just makes the arrays full sized again before loading the data
            Array.Resize(ref Dubdates, resizeSize);
            Array.Resize(ref dataX, resizeSize);
            Array.Resize(ref dataEUR, resizeSize);
            Array.Resize(ref dataEUA, resizeSize);
            Array.Resize(ref dataEUC, resizeSize);
            Array.Resize(ref dataJAR, resizeSize);
            Array.Resize(ref dataJAA, resizeSize);
            Array.Resize(ref dataJAC, resizeSize);
            Array.Resize(ref dataNAR, resizeSize);
            Array.Resize(ref dataNAA, resizeSize);
            Array.Resize(ref dataNAC, resizeSize);
            Array.Resize(ref dataOCR, resizeSize);
            Array.Resize(ref dataOCA, resizeSize);
            Array.Resize(ref dataOCC, resizeSize);
            Array.Resize(ref dataAFR, resizeSize);
            Array.Resize(ref dataAFA, resizeSize);
            Array.Resize(ref dataAFC, resizeSize);
            Array.Resize(ref dataSAR, resizeSize);
            Array.Resize(ref dataSAA, resizeSize);
            Array.Resize(ref dataSAC, resizeSize);
            Array.Resize(ref dataFAR, resizeSize);
            Array.Resize(ref dataFAA, resizeSize);
            Array.Resize(ref dataFAC, resizeSize);

        }

    }//end class
}//end
