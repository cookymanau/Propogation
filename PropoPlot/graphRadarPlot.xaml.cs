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


 //***************************************
     //ProgName:  RADAR Plot
     //Date:  May 2021
     //Author:Ian Cook
     //Purose: This is the Radar Plot.  It IS missnamed!
     //Comment:	I cant get this to obey yhe ScottPlot rules and
     //have to keep calling the plot routine with clear plot every time.
     //at this point it seems functional

// We are plotting the array values

     //Updates:
     //			
     //************************************************

namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphSinglePlot.xaml
    /// </summary>
    public partial class graphSinglePlot : Window
    {

        const int arrSize = 1;

        const int fact1 = -30; //was -30
        const int fact2 = +30; //was +29  // we add this to a negative number to make it +ve.  EG got -30, we want to display as 0...so add +30 to the dBm

        public bool liveRedraw { get; set; }

        List<string> _thlist;

        double[] dataX = new double[arrSize];
        DateTime[] DTdates = new DateTime[arrSize];
        double [] Dubdates = new double[arrSize];

        double[] dataEUA = new double[arrSize] {-30}; //Average
        double[] dataEUR = new double[arrSize] { -30 }; //Raw
        double[] dataEUC = new double[arrSize] { -30 }; //Count


        double[] dataJAA = new double[arrSize] { -30 };
        double[] dataJAR = new double[arrSize] { -30 };
        double[] dataJAC = new double[arrSize] { -30 };


        double[] dataNAA = new double[arrSize] { -30 };
        double[] dataNAR = new double[arrSize] { -30 };
        double[] dataNAC = new double[arrSize] { -30 };

        double[] dataOCA = new double[arrSize] { -30 };
        double[] dataOCR = new double[arrSize] { -30 };
        double[] dataOCC = new double[arrSize] { -30 };
        // double[] dataOCB = new double[arrSize];  //bollinger bands
        OHLC[] dataOCB = new OHLC[arrSize];
       

        double[] dataAFA = new double[arrSize] { -30 };
        double[] dataAFR = new double[arrSize] { -30 };
        double[] dataAFC = new double[arrSize] { -30 };

        double[] dataSAA = new double[arrSize] { -30 };
        double[] dataSAR = new double[arrSize] { -30 };
        double[] dataSAC = new double[arrSize] { -30 };

        double[] dataFAA = new double[arrSize] { -30 };
        double[] dataFAR = new double[arrSize] { -30 };
        double[] dataFAC = new double[arrSize] { -30 };

        double[,] values = new double[2, 7];


        string usrLabel = Properties.Settings.Default.UsrDefinedName + "Raw";
        string usrLabelAvg = Properties.Settings.Default.UsrDefinedName + "Avg";
        string usrLabelCnt = Properties.Settings.Default.UsrDefinedName + "Cnt";

        int currentArrSize = 0;

        ScottPlot.Plottable.SignalPlot sig1;
        ScottPlot.Plottable.SignalPlot sig2;
        ScottPlot.Plottable.SignalPlot sig3;

        ScottPlot.Plottable.RadarPlot radar;



        double JARi = fact1;
        double NARi = fact1;
        double SARi = fact1;
        double OCRi = fact1;
        double AFRi = fact1;
        double FARi = fact1;
        double EURi = fact1;        
        double JARa = fact1;
        double NARa = fact1;
        double SARa = fact1;
        double OCRa = fact1;
        double AFRa = fact1;
        double FARa = fact1;
        double EURa = fact1;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="thlist"></param>
        public graphSinglePlot(List<string> thlist)
        {
            InitializeComponent();

           //add the user defined text to the label onb the check box
          //  chkFAGraphs.Content = Properties.Settings.Default.UsrDefinedName;

            //this is our list of decodes
            _thlist = thlist;
            

            // make the graph
             PrepareArrays();
            PlotTheArrays();
        }


        /// <summary>
        /// Take the last entry in the list, parse it and fill arrays
        /// </summary>
        public void PrepareArrays()
        {
            string[] wrdmsg = { };

            string time = "";
            int count = 0;

            var li = _thlist.LastOrDefault();

            // double defaultValue = 0;

            wrdmsg = li.Split(',');

            dataX[count] = count; //the X values

            DTdates[count] = DateTime.Parse(wrdmsg[1]);
            Dubdates[count] = DTdates[count].ToOADate();
//--
            if (wrdmsg[2] != "")
                double.TryParse(wrdmsg[2], out dataEUR[count]); //Europe
            else
                dataEUR[count] = -30;

            if (wrdmsg[3] != "")
                double.TryParse(wrdmsg[3], out dataEUA[count]); //Europe
            else
                dataEUA[count] = -30;

            if (wrdmsg[4] != "")
                double.TryParse(wrdmsg[4], out dataEUC[count]); //EuropeAverage
            else
                dataEUC[count] = 0;
//------------------------------------------------------------

            if (wrdmsg[5] != "")
                double.TryParse(wrdmsg[5], out dataJAR[count]); //Japan Raw
            else
                dataJAR[count] = -30;

            if (wrdmsg[6] != "")
                double.TryParse(wrdmsg[6], out dataJAA[count]); //Japan Avg
            else
                dataJAA[count] = -30;

            if (wrdmsg[7] != "")
                double.TryParse(wrdmsg[7], out dataJAC[count]); 
            else
                dataJAC[count] = 0;

            //-------------------------------------

            if (wrdmsg[8] != "")
                double.TryParse(wrdmsg[8], out dataNAR[count]); //North America
            else
                dataNAR[count] = -30;

            if (wrdmsg[9] != "")
                double.TryParse(wrdmsg[9], out dataNAA[count]); 
            else
                dataNAA[count] = -30;

            if (wrdmsg[10] != "")
                double.TryParse(wrdmsg[10], out dataNAC[count]); 
            else
                dataNAC[count] = 0;

            //------------------------------------------
            if (wrdmsg[11] != "")
                double.TryParse(wrdmsg[11], out dataOCR[count]); //Oceania
            else
                dataOCR[count] = -30;

            if (wrdmsg[12] != "")
                double.TryParse(wrdmsg[12], out dataOCA[count]); 
            else
                dataOCA[count] = -30;

            if (wrdmsg[13] != "")
                double.TryParse(wrdmsg[13], out dataOCC[count]); 
            else
                dataOCC[count] = 0;

            

 
            if (wrdmsg[14] !=  "")
                double.TryParse(wrdmsg[14], out dataAFR[count]); //africa
            else
                dataAFR[count] = -30;

            if (wrdmsg[15] != "")
                double.TryParse(wrdmsg[15], out dataAFA[count]);
            else
                dataAFA[count] = -30;

            if (wrdmsg[16] != "")
                double.TryParse(wrdmsg[16], out dataAFC[count]);
            else
                dataAFC[count] = 0;



//------------------------------------
            if (wrdmsg[17] != "")
                double.TryParse(wrdmsg[17], out dataSAR[count]);  //south america
            else
                dataSAR[count] = -30;

            if (wrdmsg[18] != "")
                double.TryParse(wrdmsg[18], out dataSAA[count]);
            else
                dataSAA[count] = -30;

            if (wrdmsg[19] != "")
                double.TryParse(wrdmsg[19], out dataSAC[count]);
            else
                dataSAC[count] = 0;
  //-------------------------------------------------------
            if (wrdmsg[20] != "")
                double.TryParse(wrdmsg[20], out dataFAR[count]);  //user
            else
                dataFAR[count] = -30;

            if (wrdmsg[21] != "")
                double.TryParse(wrdmsg[21], out dataFAA[count]);
            else
                dataFAA[count] = -30;

            if (wrdmsg[22] != "")
                double.TryParse(wrdmsg[22], out dataFAC[count]);
            else
                dataFAC[count] = 0;

            //double.TryParse(wrdmsg[20], out dataFAR[count]);
            //double.TryParse(wrdmsg[21], out dataFAA[count]);
            //double.TryParse(wrdmsg[22], out dataFAC[count]);
            //dataOC[count] = double.TryParse(wrdmsg[6],out 0);


            JARi = dataJAR[0] + fact2;  //take the reading and add +30 to it to display it on the radar plot
            NARi = dataNAR[0] + fact2;
            SARi = dataSAR[0] + fact2;
            OCRi = dataOCR[0] + fact2;
            AFRi = dataAFR[0] + fact2;
            FARi = dataFAR[0] + fact2;
            EURi = dataEUR[0] + fact2;

            JARa = dataJAA[0] + fact2;
            NARa = dataNAA[0] + fact2;
            SARa = dataSAA[0] + fact2;
            OCRa = dataOCA[0] + fact2;
            AFRa = dataAFA[0] + fact2;
            FARa = dataFAA[0] + fact2;
            EURa = dataEUA[0] + fact2;

            if (chkAvg.IsChecked == true && chkRaw.IsChecked == true)
            {
                values[0, 0] = JARi; //ja
                values[0, 1] = NARi; //na
                values[0, 2] = SARi; //sa
                values[0, 3] = OCRi; //oc
                values[0, 4] = AFRi; //af
                values[0, 5] = FARi;//fa
                values[0, 6] = EURi;//eu

                values[1, 0] = JARa; //ja
                values[1, 1] = NARa; //na
                values[1, 2] = SARa; //sa
                values[1, 3] = OCRa; //oc
                values[1, 4] = AFRa; //af
                values[1, 5] = FARa;//fa
                values[1, 6] = EURa;//eu

            }
            else if (chkAvg.IsChecked == true && chkRaw.IsChecked == false)
            {
                values[0, 0] = 0; //ja
                values[0, 1] = 0; //na
                values[0, 2] = 0; //sa
                values[0, 3] = 0; //oc
                values[0, 4] = 0; //af
                values[0, 5] = 0;//fa
                values[0, 6] = 0;//eu

                values[1, 0] = JARa; //ja
                values[1, 1] = NARa; //na
                values[1, 2] = SARa; //sa
                values[1, 3] = OCRa; //oc
                values[1, 4] = AFRa; //af
                values[1, 5] = FARa;//fa
                values[1, 6] = EURa;//eu
            }

            else if (chkAvg.IsChecked == false && chkRaw.IsChecked == true)
            {
                values[0, 0] = JARi; //ja
                values[0, 1] = NARi; //na
                values[0, 2] = SARi; //sa
                values[0, 3] = OCRi; //oc
                values[0, 4] = AFRi; //af
                values[0, 5] = FARi;//fa
                values[0, 6] = EURi;//eu

                values[1, 0] = 0; //ja
                values[1, 1] = 0; //na
                values[1, 2] = 0; //sa
                values[1, 3] = 0; //oc
                values[1, 4] = 0; //af
                values[1, 5] = 0;//fa
                values[1, 6] = 0;//eu

            }
            else if (chkAvg.IsChecked == false && chkRaw.IsChecked == false)
            {
                values[0, 0] = 0; //ja
                values[0, 1] = 0; //na
                values[0, 2] = 0; //sa
                values[0, 3] = 0; //oc
                values[0, 4] = 0; //af
                values[0, 5] = 0;//fa
                values[0, 6] = 0;//eu

                values[1, 0] = 0; //ja
                values[1, 1] = 0; //na
                values[1, 2] = 0; //sa
                values[1, 3] = 0; //oc
                values[1, 4] = 0; //af
                values[1, 5] = 0;//fa
                values[1, 6] = 0;//eu
            }

            ////dont know if we want this in this plot
            //dataEUR = smoothArray(dataEUR);
            ////dataEUA = smoothArray(dataEUA);
            //dataJAR = smoothArray(dataJAR);
            ////dataJAA = smoothArray(dataJAA);
            //dataNAR = smoothArray(dataNAR);
            ////dataNAA = smoothArray(dataNAA);
            //dataSAR = smoothArray(dataSAR);
            ////dataSAA = smoothArray(dataSAA);
            //dataFAR = smoothArray(dataFAR);
            ////dataFAA = smoothArray(dataFAA);
            //dataAFR = smoothArray(dataAFR);
            ////dataAFA = smoothArray(dataAFA);
            //dataOCR = smoothArray(dataOCR);




        }
        /// <summary>
        /// Put the radar plot on screen
        /// </summary>
            public void PlotTheArrays()
        {



            radar =  graphSingle.Plot.AddRadar(values,independentAxes:false );

            radar.CategoryLabels = new string[] {"JA","NA","SA","OC","AF","Usr","EU" };
            radar.ShowAxisValues = false;
            radar.AxisType = RadarAxis.None;
            

        }//end


        /// <summary>
        /// This is method behind the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphSingleRedraw_Click(object sender, RoutedEventArgs e)
        {
            updatePlot();
        }

        /// <summary>
        /// Does the actual work of the button
        /// </summary>
        private void updatePlot()
        {
            PrepareArrays();
            graphSingle.Plot.Clear();
            PlotTheArrays();
            graphSingle.Render();

        }



        private double[] smoothArray(double[] arr)
        {


            for (int i = 0; i < arr.Length - 2; i++)
            {
                double firstVal = arr[i];  //get a reading
                double nextVal = arr[i + 1];  //get the next reading
                double nextVal1 = arr[i + 2]; //get the one after
                if (nextVal == -30 && nextVal1 == -30)  //two in a row probably means we have real -30 data, niot just one person on the band sending CQ
                    arr[i + 1] = -30;  //do nothing
                else if (nextVal == -30 && nextVal1 != -30)
                    arr[i + 1] = firstVal;
            }
            return arr;
        }









        /// <summary>
        /// This is the new thread that updates the plot every 15 seconds
        /// </summary>
        System.Windows.Threading.DispatcherTimer dispatcherTimer3 = new System.Windows.Threading.DispatcherTimer();
        private void chkLiveUpdate_Checked(object sender, RoutedEventArgs e)
        {
            graphSingleRedraw.Content = "Live";
            // now start the timer to process the UDP stuff now that we have started it.
            dispatcherTimer3.Tick += new EventHandler(dispatcherTimer2_Tick);
            dispatcherTimer3.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer3.Start();
        }

        /// <summary>
        /// the function that the new thread calls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            updatePlot();

        }
        /// <summary>
        /// When we untick the checkbox for live, we stop the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void chkLiveUpdate_Unchecked_1(object sender, RoutedEventArgs e)
        {

            graphSingleRedraw.Content = "Plot";
            dispatcherTimer3.Stop();
        }
    }
}
