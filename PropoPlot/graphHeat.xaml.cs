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
using System.Drawing;



namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphHeat.xaml
    /// </summary>
    public partial class graphHeat : Window
    {


        const int arrSize = 50000;

        double[] Xs = new double[arrSize];
        double[] Ys = new double[arrSize];
        double[] Zs = new double[arrSize];

        int count = 0;

        List<string> thlist;
        public graphHeat(List<string> alist)
        {
            InitializeComponent();
          
            thlist = alist;  //this is where all of the data is  =>>  time, dBm, Lat, Long
            PrepareArrays();
            plotPoints();

       }


        private void PrepareArrays()
        {

            string[] wrdmsg = { };

            string time = "";
           

            //  sizeTheArrays(arrSize);
            foreach (var item in thlist)
            {
                // double defaultValue = 0;

                wrdmsg = item.Split(',');
                Xs[count] = double.Parse(wrdmsg[2]);  //Latitude
                Ys[count] = double.Parse(wrdmsg[3]);  //Latitude
                Zs[count] = ((double.Parse(wrdmsg[1])) + 30.0)/4; //dbM

                count += 1;
            }



            } //end of function


        private void plotPoints()
        {

            Random rand = new(0);

            var myBubblePlot = graphHeatmap.Plot.AddBubblePlot();

            double randomValue = rand.NextDouble();
            double bubbleSize = randomValue * 1 + 1;

            // resize the arrays for plotting - get rid of all of the 0's
            double[] pXs = new double[count];
            double[] pYs = new double[count];
            double[] pZs = new double[count];

            Array.Copy(Xs, pXs, count);
            Array.Copy(Ys, pYs, count);
            Array.Copy(Zs, pZs, count);

            // System.Windows.Media.Color bubbleColor = Drawing.Colormap.Jet.GetColor(randomValue, .5);


            for (int i = 0; i < pXs.Length; i++)
            {
                myBubblePlot.Add(x: pYs[i], y: pXs[i], radius: pZs[i], fillColor: System.Drawing.Color.Red, edgeWidth: 1, edgeColor: System.Drawing.Color.Red);

            }

        }




        System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
        private void chkLiveUpdate_Checked(object sender, RoutedEventArgs e)
        {
            graphMapRedraw.Content = "Live";
            // now start the timer to process the UDP stuff now that we have started it.
            dispatcherTimer2.Tick += new EventHandler(dispatcherTimer2_Tick);
            dispatcherTimer2.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimer2.Start();
        }


        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {

            PrepareArrays();
            plotPoints();
        }

        private void chkLiveUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            graphMapRedraw.Content = "Refresh";
        }

        private void graphMapRedraw_Click(object sender, RoutedEventArgs e)
        {
            PrepareArrays();
            plotPoints();
        }
    }
}
