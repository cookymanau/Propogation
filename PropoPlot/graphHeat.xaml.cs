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
using Serilog;



namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphHeat.xaml
    /// </summary>
    public partial class graphHeat : Window
    {


        int divisor = 6;
        int numberOfPoints = 5;

        const int arrSize = 1500;

        double[] Xs = new double[arrSize];
        double[] Ys = new double[arrSize];
        double[] Zs = new double[arrSize];

        double[] pXs = new double[arrSize];
        double[] pYs = new double[arrSize];
        double[] pZs = new double[arrSize];

        int count = 0;
        //   ScottPlot.Plottable.BubblePlot hmap;    //hmap = graphHeatmap.Plot.AddBubblePlot();

        List<string> thlist;
        public graphHeat(List<string> alist)
        {
            InitializeComponent();


            EventLogger.WriteLine("Entered into graphHeat");


            var bc = new BrushConverter();
            chkcr1.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM1);
            chkcr2.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM2);
            chkcr3.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM3);
            chkcr4.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM4);
            chkcr5.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM5);

            //set the window size
            this.MaxWidth = int.Parse(wXsizer.Text);  // for sizing the dot spaceing
            this.MaxHeight = int.Parse(wYsizer.Text);
            this.MinWidth = int.Parse(wXsizer.Text); // for sizing the actual window - works best
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
                int listCount = thlist.Count();  // get the number of items in our list

                //  double Xscale = double.Parse(Xscaler.Text);
                //  double Yscale = double.Parse(Yscaler.Text);

                int howMany = int.Parse(cmboNumPoints.Text);
                txtListCount.Text = listCount.ToString();  //for dubugging

                string[] wrdmsg = { };

                //    for (int i = listCount - howMany; i < listCount;i++)  // instead of array resizing, lets just limit how much work we do and get the last n records
                // all of the points in here should be from the top howMany records in the list
                // this way were are NOT traversing the entire list as it grows
                // this array should not be any bigger than ???

                if (howMany > listCount)
                    howMany = 1;

                for (int i = listCount - howMany; i < listCount; i++)  // instead of array resizing, lets just limit how much work we do and get the last n records
                {
                    wrdmsg = thlist[i].Split(',');
                    if (count >= arrSize)
                    {
                        Array.Clear(Xs, 0, arrSize);
                        Array.Clear(Ys, 0, arrSize);
                        Array.Clear(Zs, 0, arrSize);
                        count = 0;
                        EventLogger.WriteLine($"graphHeat: PrepareArrays : Array.Clear Xs na count now : {count} ");
                    }
                    Xs[count] = double.Parse(wrdmsg[2]);// * Xscale;  //Long
                    Ys[count] = double.Parse(wrdmsg[3]);//  * Yscale;  //Latitude
                    Zs[count] = ((double.Parse(wrdmsg[1])) + 30.0); //dbM the + 30 makes the negative number a positive.


                    count += 1;
                }
                // count will be incrementing by howMany each run

                // now lets set up what we want to plot - so this should be 

                // now we run into a problem where count can be 0 and howmany = 5 so you end up with a - number
                if (count - howMany <= 0)
                    howMany = 0;

                Array.Copy(Xs, count - howMany, pXs, count - howMany, howMany);
                Array.Copy(Ys, count - howMany, pYs, count - howMany, howMany);
                Array.Copy(Zs, count - howMany, pZs, count - howMany, howMany);

                EventLogger.WriteLine($"graphHeat: PrepareArrays : ArrayCopy Xs => pXs: { Xs.Count()} => {pXs.Count()}  ");
                EventLogger.WriteLine($"graphHeat: PrepareArrays : ArrayCopy Ys => pYs: { Ys.Count()} => {pYs.Count()}  ");
                EventLogger.WriteLine($"graphHeat: PrepareArrays : ArrayCopy Zs => pZs: { Zs.Count()} => {pZs.Count()}  ");
                


                txtCount.Text = count.ToString();

                plotPoints();

            }
            catch (Exception ex)
            {
                frmMessageDialog md = new frmMessageDialog();
                md.messageBoxUpper.Text = $"Error in PrepareArrays of graphHeat ";
                md.messageBoxLower.Text = $"{ex}";
                md.Show();

                EventLogger.WriteLine($"graphHeat:PrepareArrays: catch block:{ex}");

            }

        } //end of function





        private void plotPoints()
        {

            int howMany = int.Parse(cmboNumPoints.Text);

            Bitmap wmap = new Bitmap(@".\Map.png");
            var worldmap = graphHeatmap.Plot.AddImage(wmap, -180, 90);
          //  var hmap = graphHeatmap.Plot.AddBubblePlot();
           

            var bc = new BrushConverter();


            // resize the arrays for plotting - get rid of all of the 0's
            //make the number of points plotted adjustable so you are plotting a variable
            //history of points.  So lets copy the count to count - 100 points to get a 100 points on the map
            // count is all of the points. we can have another compbo for these numbers

            //lets start with 100 points instead of count

            //  int numberOfPoints = 0;
            EventLogger.WriteLine($"graphHeat:plotPoints: count {count} numberOfPoints: {numberOfPoints}");

            //if (count < numberOfPoints)
            //{
            //    numberOfPoints = count;
            //     EventLogger.WriteLine($"graphHeat:plotPoints:Inside if (count < numberOfPoints: numberOfPoints is now: {numberOfPoints}");

            //}
            //            else
            //                numberOfPoints = 100;


            // thse pot 4 known points on the map up in the corners
            double[] cornerX = new double[] { -180, -180, 180, 180 };
            double[] cornerY = new double[] { 90, -90, 90, -90 };


            float dGreen = 10;
            float dYellow = 15;
            float dAcqua = 22;
            float dBlue = 30;

 

            // polygon plot on the map Europe
            double[] xs1 = {-12,-12,60,60};
            double[] ys1 = {30,72,72,30};
            var hcont = graphHeatmap.Plot.AddPolygon(xs1,ys1);
            var hcontLabel = graphHeatmap.Plot.AddText("Europe", -12, 30, size: 14);

            double[] JAx = { 130, 130, 145, 145 };
            double[] JAy = { 30, 46, 46, 30 };
            var jacont = graphHeatmap.Plot.AddPolygon(JAx, JAy);

            double[] NAx = { -131, -131, -54, -54 };
            double[] NAy = { 12, 90, 90, 12 };
            var nacont = graphHeatmap.Plot.AddPolygon(NAx, NAy);

            double[] SAx = { -90, -90, -32, -32 };
            double[] SAy = { -60, 12, 12, -60 };
            var sacont = graphHeatmap.Plot.AddPolygon(SAx, SAy);

            double[] AFx = { -20, -20, 50, 50 };
            double[] AFy = { -35, 34, 34, -35 };
            var afcont = graphHeatmap.Plot.AddPolygon(AFx, AFy);

            double[] OCx = { 112, 112, 126, 126 };
            double[] OCy = { -54, 28, 28, -54 };
            var occont = graphHeatmap.Plot.AddPolygon(OCx, OCy);

            double[] FAx = { 60, 60, 144, 144 };
            double[] FAy = { -9, 90, 90, -9 };
            var facont = graphHeatmap.Plot.AddPolygon(FAx, FAy);

            var hmap = graphHeatmap.Plot.AddBubblePlot();
            //plot the corner points
            for (int i = 0; i < cornerX.Length; i++)
                hmap.Add(x: cornerX[i], y: cornerY[i], radius: 3, fillColor: System.Drawing.Color.Red, edgeWidth: 1, edgeColor: System.Drawing.Color.Black);


            //I only want to plot the top howMany Elements
            //for (int i = 0; i < pXs.Length; i++)
            if (count - howMany <= 0)
                howMany = 0;

            for (int i = count - howMany; i < count; i++)
            {

                if (pZs[i] < dGreen && chkcr1.IsChecked == true)
                    //               hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: System.Drawing.Color.LightGreen, edgeWidth: 1, edgeColor: System.Drawing.Color.Black);
                    hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM1)), edgeWidth: 1, edgeColor: System.Drawing.Color.Black);

                else if ((pZs[i] >= dGreen && pZs[i] < dYellow) && chkcr2.IsChecked == true)
                    hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM2)), edgeWidth: 1, edgeColor: System.Drawing.Color.DarkGreen);

                else if ((pZs[i] >= dYellow && pZs[i] < dAcqua) && chkcr3.IsChecked == true)
                    hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM3)), edgeWidth: 1, edgeColor: System.Drawing.Color.Blue);

                else if ((pZs[i] >= dAcqua && pZs[i] < dBlue) && chkcr4.IsChecked == true)
                    hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM4)), edgeWidth: 1, edgeColor: System.Drawing.Color.Blue);

                else if (pZs[i] > dBlue && chkcr5.IsChecked == true)  //dont want to plot 0's
                    hmap.Add(x: pYs[i], y: pXs[i], radius: pZs[i] / divisor, fillColor: (ColorTranslator.FromHtml(Properties.Settings.Default.crDBM5)), edgeWidth: 1, edgeColor: System.Drawing.Color.Red);
            }

            graphHeatmap.Plot.Render();
        }//end


        System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
        private void chkLiveUpdate_Checked(object sender, RoutedEventArgs e)
        {
            graphMapRedraw.Content = "Live";
            // now start the timer to process the UDP stuff now that we have started it.
            dispatcherTimer2.Tick += new EventHandler(dispatcherTimer2_Tick);
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer2.Start();

        }

        int timercounter = 0;
        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            timercounter += 2; //going up in 2 second increments.  It seemed less busy that way
            timerBar.Value = timercounter;  //up date the indicator - something IS happening

            if (timercounter >= 30) //every timerInterval seconds process the udp strings
            {

                PrepareArrays();
                timercounter = 0;
            }
        }

        private void chkLiveUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            graphMapRedraw.Content = "Refresh";
        }

        private void graphMapRedraw_Click(object sender, RoutedEventArgs e)  //aka refresh
        {
          //  count = 0;  
            PrepareArrays();
        }

        private void graphClear_Click(object sender, RoutedEventArgs e)
        {
            graphHeatmap.Plot.Clear();
            graphHeatmap.Plot.Render();
            count = 0;
            Array.Clear(Xs, 0, arrSize);
            Array.Clear(Ys, 0, arrSize);
            Array.Clear(Zs, 0, arrSize);

        }


        private void cmboSize_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("small");
            data.Add("medium");
            data.Add("large");
            data.Add("enormous");

            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 1;
        }

        private void cmboSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmboSize = sender as ComboBox;
            string size = cmboSize.SelectedItem as string;
            //  MessageBox.Show(name);
            //UDPportEntry.Text = name;

            switch (size)
            {
                case "small":
                    divisor = 4;
                    break;
                case "medium":
                    divisor = 3;
                    break;
                case "large":
                    divisor = 2;
                    break;
                case "enormous":
                    divisor = 1;
                    break;
            }
            PrepareArrays();
        } //end

        private void cmboNumPoints_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("5");
            data.Add("10");
            data.Add("20");
            data.Add("50");
            data.Add("75");
            data.Add("100");
            data.Add("200");
            data.Add("1000");
            

            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;

        }

        private void cmboNumPoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmboNpoints = sender as ComboBox;
            string Npoints = cmboNpoints.SelectedItem as string;
            //  MessageBox.Show(name);
            //UDPportEntry.Text = name;

            switch (Npoints)
            {
                case "5":
                    numberOfPoints = 5;
                    break;
                case "10":
                    numberOfPoints = 10;
                    break;
                case "20":
                    numberOfPoints = 20;
                    break;
                case "50":
                    numberOfPoints = 50;
                    break;
                case "75":
                    numberOfPoints = 75;
                    break;
                case "100":
                    numberOfPoints = 100;
                    break;
                case "200":
                    numberOfPoints = 200;
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

        private void chkcr1_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
