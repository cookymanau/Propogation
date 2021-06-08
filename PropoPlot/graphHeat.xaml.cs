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


        int divisor = 6;
        int numberOfPoints = 100000;

        const int arrSize = 100000;

        double[] Xs = new double[arrSize];
        double[] Ys = new double[arrSize];
        double[] Zs = new double[arrSize];

        int count = 0;
     //   ScottPlot.Plottable.BubblePlot hmap;    //hmap = graphHeatmap.Plot.AddBubblePlot();

        List<string> thlist;
        public graphHeat(List<string> alist)
        {
            InitializeComponent();

            this.MaxWidth = int.Parse(wXsizer.Text);
            this.MaxHeight = int.Parse(wYsizer.Text);
            this.MinWidth = int.Parse(wXsizer.Text);
            this.MinHeight = int.Parse(wYsizer.Text);

            thlist = alist;  //this is where all of the data is  =>>  time, dBm, Lat, Long
            PrepareArrays();


            // plotPoints();
            //  plotHeatMAp();

        }


        private void PrepareArrays()
        {
            try
            {

                double Xscale = double.Parse(Xscaler.Text);
                double Yscale = double.Parse(Yscaler.Text);

               // Yscale = 1;
              //  Xscale = 1;//0.85;

            string[] wrdmsg = { };
            //  sizeTheArrays(arrSize);
            foreach (var item in thlist)
            {
                wrdmsg = item.Split(',');
                Xs[count] = double.Parse(wrdmsg[2]) * Xscale;  //Long
                Ys[count] = double.Parse(wrdmsg[3]) * Yscale;  //Latitude
                Zs[count] = ((double.Parse(wrdmsg[1])) + 30.0); //dbM the + 30 makes the negative number a positive.
                count += 1;

                //if(count >= arrSize)
                //{
                //    count = 0;
                //    Array.Clear(Xs,0,arrSize);
                //    Array.Clear(Ys,0,arrSize);
                //    Array.Clear(Zs,0,arrSize);
                //}
            }


            plotPoints();
          // plotHeatMAp();
            }
            catch (Exception ex)
            {
                frmMessageDialog md = new frmMessageDialog();
                md.messageBoxUpper.Text = $"Error in PrepareArrays of graphHeat ";
                md.messageBoxLower.Text = $"{ex}";
                md.Show();
            }

        } //end of function





        private void plotPoints()
        {

            Bitmap wmap = new Bitmap(@".\Map.png");
            var worldmap = graphHeatmap.Plot.AddImage(wmap, -180, 90);
            var hmap = graphHeatmap.Plot.AddBubblePlot();

            var bc = new BrushConverter();


            // resize the arrays for plotting - get rid of all of the 0's
            //make the number of points plotted adjustable so you are plotting a variable
            //history of points.  So lets copy the count to count - 100 points to get a 100 points on the map
            // count is all of the points. we can have another compbo for these numbers

            //lets start with 100 points instead of count

            //  int numberOfPoints = 0;

            if (count < numberOfPoints)
            {
                numberOfPoints = count;

            }
            //            else
            //                numberOfPoints = 100;


            double[] cornerX = new double[] {-180,-180,180,180};
            double[] cornerY = new double[] {90,-90,90,-90 };





            double[] pXs = new double[arrSize];
            double[] pYs = new double[arrSize];
            double[] pZs = new double[arrSize];


            //if (count > numberOfPoints * 2)
            //{

            //    Array.Copy(Xs, count - numberOfPoints, pXs, 0, numberOfPoints);
            //    Array.Copy(Ys, count - numberOfPoints, pYs, 0, numberOfPoints);
            //    Array.Copy(Zs, count - numberOfPoints, pZs, 0, numberOfPoints);
            //}
            //else
            //  { // this is the start up position
            Array.Copy(Xs, pXs, count);
            Array.Copy(Ys, pYs, count);
            Array.Copy(Zs, pZs, count);
            //}
            float dGreen = 10;
            float dYellow = 15;
            float dAcqua = 22 ;
            float dBlue = 30 ;

            hmap.Clear();
            //the corner points
            for (int i = 0; i < cornerX.Length; i++)
                hmap.Add(x: cornerX[i], y: cornerY[i], radius: 5, fillColor: System.Drawing.Color.Red, edgeWidth: 1, edgeColor: System.Drawing.Color.Black);

            for (int i = 0; i < pXs.Length; i++)
            {
                
                if(pZs[i] < dGreen)
 //               hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: System.Drawing.Color.LightGreen, edgeWidth: 1, edgeColor: System.Drawing.Color.Black);
                hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM1)), edgeWidth: 1, edgeColor: System.Drawing.Color.Black);
               
                else if (pZs[i] >= dGreen  && pZs[i] < dYellow)
                hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM2)), edgeWidth: 1, edgeColor: System.Drawing.Color.DarkGreen);
                
                else if (pZs[i] >= dYellow  && pZs[i] < dAcqua)
                hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM3)), edgeWidth: 1, edgeColor: System.Drawing.Color.Blue);
                
                else if (pZs[i] >= dAcqua  && pZs[i] < dBlue)
                hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM4)), edgeWidth: 1, edgeColor: System.Drawing.Color.Blue);
                
                else if (pZs[i] > dBlue)  //dont want to plot 0's
                hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM5)), edgeWidth: 1, edgeColor: System.Drawing.Color.Red);
            }

          //  hmap.Render();
            
           
        }//end


        System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
        private void chkLiveUpdate_Checked(object sender, RoutedEventArgs e)
        {
            graphMapRedraw.Content = "Live";
            // now start the timer to process the UDP stuff now that we have started it.
            dispatcherTimer2.Tick += new EventHandler(dispatcherTimer2_Tick);
            dispatcherTimer2.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer2.Start();
        }


        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {

            PrepareArrays();
          //  plotPoints();
        }

        private void chkLiveUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            graphMapRedraw.Content = "Refresh";
        }

        private void graphMapRedraw_Click(object sender, RoutedEventArgs e)
        {

            //  graphHeatmap.Plot.Clear();
            //  graphHeatmap.Plot.Render();

            this.Width = int.Parse(wXsizer.Text);
            this.Height = int.Parse(wYsizer.Text);

            PrepareArrays();
            //plotPoints();
        }

        private void cmboSize_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("wee");
            data.Add("small");
            data.Add("medium");
            data.Add("big");
            data.Add("huge");
            data.Add("enormous");

            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 2;
        }

        private void cmboSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmboSize = sender as ComboBox;
            string size = cmboSize.SelectedItem as string;
            //  MessageBox.Show(name);
            //UDPportEntry.Text = name;

            switch (size)
            {
                case "wee":
                    divisor = 10;
                    break;
                case "small":
                    divisor = 8;
                    break;
                case "medium":
                    divisor = 6;
                    break;
                case "huge":
                    divisor = 4;
                    break;
                case "enormous":
                    divisor = 2;
                    break;
                        
            }

            graphHeatmap.Plot.Clear();
            graphHeatmap.Plot.Render();

            PrepareArrays();




        } //end

        private void cmboNumPoints_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("50");
            data.Add("100");
            data.Add("500");
            data.Add("1000");
            data.Add("5000");
            data.Add("All");

            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 2;

        }

        private void cmboNumPoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmboNpoints = sender as ComboBox;
            string Npoints = cmboNpoints.SelectedItem as string;
            //  MessageBox.Show(name);
            //UDPportEntry.Text = name;

            switch (Npoints)
            {
                case "50":
                   numberOfPoints  = 10;
                    break;
                case "100":
                    numberOfPoints = 100;
                    break;
                case "500":
                    numberOfPoints = 500;
                    break;
                case "1000":
                    numberOfPoints = 1000;
                    break;
                case "5000":
                    numberOfPoints = 5000;
                    break;
                case "All":
                    numberOfPoints = arrSize;
                    break;


            }



            PrepareArrays();

        }//end

        private void graphClear_Click(object sender, RoutedEventArgs e)
        {
            graphHeatmap.Plot.Clear();
            graphHeatmap.Plot.Render();
            count = 0;
        }
    }
}
