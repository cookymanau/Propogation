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
    /// Interaction logic for graphPopulation.xaml
    /// </summary>
    public partial class graphPopulation : Window
    {
        

        const int arrSize = 5000;

        double[] dataX = new double[arrSize];
        DateTime[] DTdates = new DateTime[arrSize];
        double[] Dubdates = new double[arrSize];

//        double[] dataEUA = new double[arrSize]; //Average
        double[] dataEUR = new double[arrSize]; //Raw
 //       double[] dataEUC = new double[arrSize]; //Count


  //      double[] dataJAA = new double[arrSize];
        double[] dataJAR = new double[arrSize];
    //    double[] dataJAC = new double[arrSize];


     //   double[] dataNAA = new double[arrSize];
        double[] dataNAR = new double[arrSize];
  //      double[] dataNAC = new double[arrSize];

    //    double[] dataOCA = new double[arrSize];
        double[] dataOCR = new double[arrSize];
   //     double[] dataOCC = new double[arrSize];

  //      double[] dataAFA = new double[arrSize];
        double[] dataAFR = new double[arrSize];
  //      double[] dataAFC = new double[arrSize];

  //      double[] dataSAA = new double[arrSize];
        double[] dataSAR = new double[arrSize];
 //       double[] dataSAC = new double[arrSize];

 //       double[] dataFAA = new double[arrSize];
        double[] dataFAR = new double[arrSize];
 //       double[] dataFAC = new double[arrSize];
        List<string> thlist;

        //the plot handles
        //ScottPlot.Plottable.PopulationPlot sigEUA;
        ScottPlot.Plottable.PopulationPlot sigEUR;
       // ScottPlot.Plottable.PopulationPlot sigEUC;
       // ScottPlot.Plottable.PopulationPlot sigJAA;
        ScottPlot.Plottable.PopulationPlot sigJAR;
       // ScottPlot.Plottable.PopulationPlot sigJAC;
       // ScottPlot.Plottable.PopulationPlot sigOCA;
        ScottPlot.Plottable.PopulationPlot sigOCR;
       // ScottPlot.Plottable.PopulationPlot sigOCC;
       // ScottPlot.Plottable.PopulationPlot sigNAA;
        ScottPlot.Plottable.PopulationPlot sigNAR;
       // ScottPlot.Plottable.PopulationPlot sigNAC;
       // ScottPlot.Plottable.PopulationPlot sigSAA;
        ScottPlot.Plottable.PopulationPlot sigSAR;
       // ScottPlot.Plottable.PopulationPlot sigSAC;
       // ScottPlot.Plottable.PopulationPlot sigAFA;
        ScottPlot.Plottable.PopulationPlot sigAFR;
       // ScottPlot.Plottable.PopulationPlot sigAFC;
       // ScottPlot.Plottable.PopulationPlot sigFAA;
        ScottPlot.Plottable.PopulationPlot sigFAR;
       // ScottPlot.Plottable.PopulationPlot sigFAC;



        int currentArrSize = 0;
        
        public graphPopulation(List<string> alist)
        {
            InitializeComponent();
            thlist = alist;  //this is where all of the data is
            PrepareArrays(); //this uses the sized arrays (done at declaration)
            PlotTheArrays(); //you only do this once-in the constructor

        }

private void arrayToFullSize()
        {
            Array.Resize(ref dataEUR, arrSize);
            Array.Resize(ref dataOCR, arrSize);
            Array.Resize(ref dataJAR, arrSize);
            Array.Resize(ref dataNAR, arrSize);
            Array.Resize(ref dataAFR, arrSize);
            Array.Resize(ref dataFAR, arrSize);
            Array.Resize(ref dataSAR, arrSize);
        }

        public void PrepareArrays()
        {
            string[] wrdmsg = { };

            string time = "";
            int count = 0;

            //  sizeTheArrays(arrSize);
            foreach (var item in thlist)
            {
                // double defaultValue = 0;

                wrdmsg = item.Split(',');

                dataX[count] = count; //the X values

                DTdates[count] = DateTime.Parse(wrdmsg[1]);
                Dubdates[count] = DTdates[count].ToOADate();

                double.TryParse(wrdmsg[2], out dataEUR[count]); //Europe
          //      double.TryParse(wrdmsg[3], out dataEUA[count]); //EuropeAverage
          //      double.TryParse(wrdmsg[4], out dataEUC[count]); //EuropeAverage

                double.TryParse(wrdmsg[5], out dataJAR[count]); //Japan
            //    double.TryParse(wrdmsg[6], out dataJAA[count]); //JapanAvg
            //    double.TryParse(wrdmsg[7], out dataJAC[count]); //JapanAvg

                double.TryParse(wrdmsg[8], out dataNAR[count]);
             //   double.TryParse(wrdmsg[9], out dataNAA[count]);
             //   double.TryParse(wrdmsg[10], out dataNAC[count]);

                double.TryParse(wrdmsg[11], out dataOCR[count]);
             //   double.TryParse(wrdmsg[12], out dataOCA[count]);
             //   double.TryParse(wrdmsg[13], out dataOCC[count]);

                double.TryParse(wrdmsg[14], out dataAFR[count]);
             //   double.TryParse(wrdmsg[15], out dataAFA[count]);
             //   double.TryParse(wrdmsg[16], out dataAFC[count]);

                double.TryParse(wrdmsg[17], out dataSAR[count]);
             //   double.TryParse(wrdmsg[18], out dataSAA[count]);
             //   double.TryParse(wrdmsg[19], out dataSAC[count]);

                double.TryParse(wrdmsg[20], out dataFAR[count]);
             //   double.TryParse(wrdmsg[21], out dataFAA[count]);
             //   double.TryParse(wrdmsg[22], out dataFAC[count]);

                count += 1;
            }
            currentArrSize = count;


        }

        public void PlotTheArrays()  //this is only ever called once - from the constructor
        {
            graphPop.Plot.Clear();

            //going to try reducing the arrays
            Array.Resize(ref dataEUR, currentArrSize);
            Array.Resize(ref dataOCR, currentArrSize);
            Array.Resize(ref dataJAR, currentArrSize);
            Array.Resize(ref dataNAR, currentArrSize);
            Array.Resize(ref dataSAR, currentArrSize);
            Array.Resize(ref dataAFR, currentArrSize);
            Array.Resize(ref dataFAR, currentArrSize);

            dataNAR = smoothArray(dataNAR);
            dataOCR = smoothArray(dataOCR);
            dataJAR = smoothArray(dataJAR);
            dataEUR = smoothArray(dataEUR);
            dataSAR = smoothArray(dataSAR);
            dataAFR = smoothArray(dataAFR);
            dataFAR = smoothArray(dataFAR);

            var OCRpop = new ScottPlot.Statistics.Population(dataOCR);
            //var OCRpop = new ScottPlot.Statistics.PopulationSeries(new ScottPlot.Statistics.Population[] { new ScottPlot.Statistics.Population(dataOCR) }, "OC");
            var EURpop = new ScottPlot.Statistics.Population(dataEUR);
            //var EURpop = new ScottPlot.Statistics.PopulationSeries(new ScottPlot.Statistics.Population[] { new ScottPlot.Statistics.Population(dataEUR) }, "EU");
            var JARpop = new ScottPlot.Statistics.Population(dataJAR);
            //var JARpop = new ScottPlot.Statistics.PopulationSeries(new ScottPlot.Statistics.Population[] { new ScottPlot.Statistics.Population(dataJAR) }, "JA");
            var NARpop = new ScottPlot.Statistics.Population(dataNAR);
            //var NARpop = new ScottPlot.Statistics.PopulationSeries(new ScottPlot.Statistics.Population[] { new ScottPlot.Statistics.Population(dataNAR) }, "NA");
            var SARpop = new ScottPlot.Statistics.Population(dataSAR);
            //var SARpop = new ScottPlot.Statistics.PopulationSeries(new ScottPlot.Statistics.Population[] { new ScottPlot.Statistics.Population(dataSAR) }, "SA");
            var AFRpop = new ScottPlot.Statistics.Population(dataAFR);
            //var AFRpop = new ScottPlot.Statistics.PopulationSeries(new ScottPlot.Statistics.Population[] { new ScottPlot.Statistics.Population(dataAFR) }, "AF");
            var FARpop = new ScottPlot.Statistics.Population(dataFAR);
            //var FARpop = new ScottPlot.Statistics.PopulationSeries(new ScottPlot.Statistics.Population[] { new ScottPlot.Statistics.Population(dataFAR) }, Properties.Settings.Default.UsrDefinedName);

            // combine several populations into an array and plot it
            var populations = new ScottPlot.Statistics.Population[]{  EURpop, JARpop, NARpop, OCRpop, AFRpop, SARpop, FARpop };
            //var multiSeries = new ScottPlot.Statistics.PopulationMultiSeries(new ScottPlot.Statistics.PopulationSeries[] { EURpop, JARpop, NARpop, OCRpop, AFRpop, SARpop, FARpop });
            string[] populationNames = { "EU", "JA", "NA", "OC", "AF", "SA",Properties.Settings.Default.UsrDefinedName };
            graphPop.Plot.AddPopulations(populations); 
            //graphPop.Plot.PlotPopulations(multiSeries); 

            graphPop.Plot.Title($"Signal Strength Plots ");
//            graphPop.Plot.AddPopulation(OCRpop);
            graphPop.Plot.XTicks(populationNames);
            graphPop.Plot.YLabel("dBm");

            // and now show the stats
            EUmean.Text = Math.Round(EURpop.mean,0).ToString();
            JAmean.Text = Math.Round(JARpop.mean,0).ToString();
            NAmean.Text = Math.Round(NARpop.mean,0).ToString();
            OCmean.Text = Math.Round(OCRpop.mean,0).ToString();
            AFmean.Text = Math.Round(AFRpop.mean,0).ToString();
            SAmean.Text = Math.Round(SARpop.mean,0).ToString();
            FAmean.Text = Math.Round(FARpop.mean,0).ToString();

            EUmedian.Text = Math.Round(EURpop.median, 0).ToString();
            JAmedian.Text = Math.Round(JARpop.median, 0).ToString();
            NAmedian.Text = Math.Round(NARpop.median, 0).ToString();
            OCmedian.Text = Math.Round(OCRpop.median, 0).ToString();
            AFmedian.Text = Math.Round(AFRpop.median, 0).ToString();
            SAmedian.Text = Math.Round(SARpop.median, 0).ToString();
            FAmedian.Text = Math.Round(FARpop.median, 0).ToString();

            EUIQR.Text = Math.Round(EURpop.IQR, 0).ToString();
            JAIQR.Text = Math.Round(JARpop.IQR, 0).ToString();
            NAIQR.Text = Math.Round(NARpop.IQR, 0).ToString();
            OCIQR.Text = Math.Round(OCRpop.IQR, 0).ToString();
            AFIQR.Text = Math.Round(AFRpop.IQR, 0).ToString();
            SAIQR.Text = Math.Round(SARpop.IQR, 0).ToString();
            FAIQR.Text = Math.Round(FARpop.IQR, 0).ToString();

            EUStDev.Text = Math.Round(EURpop.stDev, 0).ToString();
            JAStDev.Text = Math.Round(JARpop.stDev, 0).ToString();
            NAStDev.Text = Math.Round(NARpop.stDev, 0).ToString();
            OCStDev.Text = Math.Round(OCRpop.stDev, 0).ToString();
            AFStDev.Text = Math.Round(AFRpop.stDev, 0).ToString();
            SAStDev.Text = Math.Round(SARpop.stDev, 0).ToString();
            FAStDev.Text = Math.Round(FARpop.stDev, 0).ToString();

            EUQ1.Text = Math.Round(EURpop.Q1, 0).ToString();
            JAQ1.Text = Math.Round(JARpop.Q1, 0).ToString();
            NAQ1.Text = Math.Round(NARpop.Q1, 0).ToString();
            OCQ1.Text = Math.Round(OCRpop.Q1, 0).ToString();
            AFQ1.Text = Math.Round(AFRpop.Q1, 0).ToString();
            SAQ1.Text = Math.Round(SARpop.Q1, 0).ToString();
            FAQ1.Text = Math.Round(FARpop.Q1, 0).ToString();

            EUQ3.Text = Math.Round(EURpop.Q3, 0).ToString();
            JAQ3.Text = Math.Round(JARpop.Q3, 0).ToString();
            NAQ3.Text = Math.Round(NARpop.Q3, 0).ToString();
            OCQ3.Text = Math.Round(OCRpop.Q3, 0).ToString();
            AFQ3.Text = Math.Round(AFRpop.Q3, 0).ToString();
            SAQ3.Text = Math.Round(SARpop.Q3, 0).ToString();
            FAQ3.Text = Math.Round(FARpop.Q3, 0).ToString();

            EUPlus2STD.Text = Math.Round(EURpop.plus2stDev, 0).ToString();
            JAPlus2STD.Text = Math.Round(JARpop.plus2stDev, 0).ToString();
            NAPlus2STD.Text = Math.Round(NARpop.plus2stDev, 0).ToString();
            OCPlus2STD.Text = Math.Round(OCRpop.plus2stDev, 0).ToString();
            AFPlus2STD.Text = Math.Round(AFRpop.plus2stDev, 0).ToString();
            SAPlus2STD.Text = Math.Round(SARpop.plus2stDev, 0).ToString();
            FAPlus2STD.Text = Math.Round(FARpop.plus2stDev, 0).ToString();

            EUPlus3STD.Text = Math.Round(EURpop.plus3stDev, 0).ToString();
            JAPlus3STD.Text = Math.Round(JARpop.plus3stDev, 0).ToString();
            NAPlus3STD.Text = Math.Round(NARpop.plus3stDev, 0).ToString();
            OCPlus3STD.Text = Math.Round(OCRpop.plus3stDev, 0).ToString();
            AFPlus3STD.Text = Math.Round(AFRpop.plus3stDev, 0).ToString();
            SAPlus3STD.Text = Math.Round(SARpop.plus3stDev, 0).ToString();
            FAPlus3STD.Text = Math.Round(FARpop.plus3stDev, 0).ToString();

        }

        private void btnPlot_Click(object sender, RoutedEventArgs e)
        {
            doTheWork();
        }

        private void doTheWork() {
            //  sigEUA.DistributionCurve = true;
            arrayToFullSize();
            PrepareArrays(); //this uses the sized arrays (done at declaration)
            PlotTheArrays(); //you only do this once-in the constructor
            graphPop.Render();



        }



        /// <summary>
        /// Remove the -30's from the array.  Its crap data
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private double[] smoothArray(double[] arr) {
        

            for (int i=0;i < arr.Length-1; i++)
            {
                double firstVal = arr[i];
                double nextVal = arr[i+1];
                if (nextVal == -30)
                    arr[i + 1] = firstVal;
            }
                return arr;
        }


        //declared outside of method so I can reference it in other methods as per below
        System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
        private void chkLiveUpdate_Checked(object sender, RoutedEventArgs e)
        {
            btnPlot.Content = "Live";
            // now start the timer to process the UDP stuff now that we have started it.
            dispatcherTimer2.Tick += new EventHandler(dispatcherTimer2_Tick);
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer2.Start();
        }

        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            doTheWork();
        }






















    }//end class
}//end name space
