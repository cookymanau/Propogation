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

        const int arrSize = 10000;

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


            double hi = this.Height;
            double wi = this.Width;

            //set the window size
            this.MaxWidth = wi; // int.Parse(wXsizer.Text);  // for sizing the dot spaceing
            this.MaxHeight = hi; // int.Parse(wYsizer.Text);
            this.MinWidth = wi;// int.Parse(wXsizer.Text); // for sizing the actual window - works best
            this.MinHeight = hi; // int.Parse(wYsizer.Text);



            thlist = alist;  //this is where all of the data is  =>>  time, dBm, Lat, Long
            PrepareArrays();


            // plotPoints();
            //  plotHeatMAp();

        }


        private void PrepareArrays()
        {
            try
            {
                 // count = 0;

                int listCount = thlist.Count();  // get the number of items in our list

                //  double Xscale = double.Parse(Xscaler.Text);
                //  double Yscale = double.Parse(Yscaler.Text);

                int howMany = int.Parse(cmboNumPoints.Text);  // this the number of points we want to see on the map
                txtListCount.Text = listCount.ToString();     //Shows the absolute number of records in the list

                string[] wrdmsg = { };

                //    for (int i = listCount - howMany; i < listCount;i++)  // instead of array resizing, lets just limit how much work we do and get the last n records
                // all of the points in here should be from the top howMany records in the list
                // this way were are NOT traversing the entire list as it grows
                // this array should not be any bigger than ???

                if (howMany > listCount)
                    howMany = listCount-1;


                // show the times of the records we want to see
                string[] OldestQsoTime = thlist[listCount - howMany].Split(',');
                txtEarliestTime.Text = OldestQsoTime[0];

                string[] YoungestQsoTime = thlist[listCount-1].Split(',');
                txtYoungestTime.Text = YoungestQsoTime[0];

                // is is used to get the correct lines out of the list.  count is used as an index starting from 0 to index the arrays

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
// now we resize the arrays down to the count -get rid of the 0's
                Array.Copy(Xs, count - howMany, pXs, count - howMany, howMany);
                Array.Copy(Ys, count - howMany, pYs, count - howMany, howMany);
                Array.Copy(Zs, count - howMany, pZs, count - howMany, howMany);

             //   EventLogger.WriteLine($"graphHeat: PrepareArrays : ArrayCopy Xs => pXs: { Xs.Count()} => {pXs.Count()}  ");
             //   EventLogger.WriteLine($"graphHeat: PrepareArrays : ArrayCopy Ys => pYs: { Ys.Count()} => {pYs.Count()}  ");
            //    EventLogger.WriteLine($"graphHeat: PrepareArrays : ArrayCopy Zs => pZs: { Zs.Count()} => {pZs.Count()}  ");
                


                txtCount.Text = count.ToString();  // this is how many points we have plotted???

                
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
           // Bitmap wmap = new Bitmap(@".\MapOz.png");

            var worldmap = graphHeatmap.Plot.AddImage(wmap, -180, 90);
//            var worldmap = graphHeatmap.Plot.AddImage(wmap, -34, 90);
          //  var hmap = graphHeatmap.Plot.AddBubblePlot();
           

            var bc = new BrushConverter();

            //  int numberOfPoints = 0;
            EventLogger.WriteLine($"graphHeat:plotPoints: count {count} numberOfPoints: {numberOfPoints}");


            // these put 4 known points on the map up in the corners
            double[] cornerX = new double[] { -180, -180, 180, 180 };
            double[] cornerY = new double[] { 90, -90, 90, -90 };


            float dGreen = 10;
            float dYellow = 15;
            float dAcqua = 22;
            float dBlue = 30;

            if (chkShowContinents.IsChecked == true){ 
            // put a box on the screen so we can see ourt continents. Used lines instead of polygons because couldnt get transparency working
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.EULongMin), double.Parse(Properties.Settings.Default.EULatMin), double.Parse(Properties.Settings.Default.EULongMax), double.Parse(Properties.Settings.Default.EULatMin), System.Drawing.Color.Blue, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.EULongMax), double.Parse(Properties.Settings.Default.EULatMin), double.Parse(Properties.Settings.Default.EULongMax), double.Parse(Properties.Settings.Default.EULatMax), System.Drawing.Color.Green, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.EULongMin), double.Parse(Properties.Settings.Default.EULatMax), double.Parse(Properties.Settings.Default.EULongMax), double.Parse(Properties.Settings.Default.EULatMax), System.Drawing.Color.Magenta, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.EULongMin), double.Parse(Properties.Settings.Default.EULatMin), double.Parse(Properties.Settings.Default.EULongMin), double.Parse(Properties.Settings.Default.EULatMax), System.Drawing.Color.Red, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.JALongMin), double.Parse(Properties.Settings.Default.JALatMin), double.Parse(Properties.Settings.Default.JALongMax), double.Parse(Properties.Settings.Default.JALatMin), System.Drawing.Color.Blue, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.JALongMax), double.Parse(Properties.Settings.Default.JALatMin), double.Parse(Properties.Settings.Default.JALongMax), double.Parse(Properties.Settings.Default.JALatMax), System.Drawing.Color.Green, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.JALongMin), double.Parse(Properties.Settings.Default.JALatMax), double.Parse(Properties.Settings.Default.JALongMax), double.Parse(Properties.Settings.Default.JALatMax), System.Drawing.Color.Magenta, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.JALongMin), double.Parse(Properties.Settings.Default.JALatMin), double.Parse(Properties.Settings.Default.JALongMin), double.Parse(Properties.Settings.Default.JALatMax), System.Drawing.Color.Red, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.NALongMin), double.Parse(Properties.Settings.Default.NALatMin), double.Parse(Properties.Settings.Default.NALongMax), double.Parse(Properties.Settings.Default.NALatMin), System.Drawing.Color.Blue, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.NALongMax), double.Parse(Properties.Settings.Default.NALatMin), double.Parse(Properties.Settings.Default.NALongMax), double.Parse(Properties.Settings.Default.NALatMax), System.Drawing.Color.Green, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.NALongMin), double.Parse(Properties.Settings.Default.NALatMax), double.Parse(Properties.Settings.Default.NALongMax), double.Parse(Properties.Settings.Default.NALatMax), System.Drawing.Color.Magenta, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.NALongMin), double.Parse(Properties.Settings.Default.NALatMin), double.Parse(Properties.Settings.Default.NALongMin), double.Parse(Properties.Settings.Default.NALatMax), System.Drawing.Color.Red, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.SALongMin), double.Parse(Properties.Settings.Default.SALatMin), double.Parse(Properties.Settings.Default.SALongMax), double.Parse(Properties.Settings.Default.SALatMin), System.Drawing.Color.Blue, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.SALongMax), double.Parse(Properties.Settings.Default.SALatMin), double.Parse(Properties.Settings.Default.SALongMax), double.Parse(Properties.Settings.Default.SALatMax), System.Drawing.Color.Green, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.SALongMin), double.Parse(Properties.Settings.Default.SALatMax), double.Parse(Properties.Settings.Default.SALongMax), double.Parse(Properties.Settings.Default.SALatMax), System.Drawing.Color.Magenta, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.SALongMin), double.Parse(Properties.Settings.Default.SALatMin), double.Parse(Properties.Settings.Default.SALongMin), double.Parse(Properties.Settings.Default.SALatMax), System.Drawing.Color.Red, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.AFLongMin), double.Parse(Properties.Settings.Default.AFLatMin), double.Parse(Properties.Settings.Default.AFLongMax), double.Parse(Properties.Settings.Default.AFLatMin), System.Drawing.Color.Blue, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.AFLongMax), double.Parse(Properties.Settings.Default.AFLatMin), double.Parse(Properties.Settings.Default.AFLongMax), double.Parse(Properties.Settings.Default.AFLatMax), System.Drawing.Color.Green, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.AFLongMin), double.Parse(Properties.Settings.Default.AFLatMax), double.Parse(Properties.Settings.Default.AFLongMax), double.Parse(Properties.Settings.Default.AFLatMax), System.Drawing.Color.Magenta, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.AFLongMin), double.Parse(Properties.Settings.Default.AFLatMin), double.Parse(Properties.Settings.Default.AFLongMin), double.Parse(Properties.Settings.Default.AFLatMax), System.Drawing.Color.Red, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.FALongMin), double.Parse(Properties.Settings.Default.FALatMin), double.Parse(Properties.Settings.Default.FALongMax), double.Parse(Properties.Settings.Default.FALatMin), System.Drawing.Color.Blue, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.FALongMax), double.Parse(Properties.Settings.Default.FALatMin), double.Parse(Properties.Settings.Default.FALongMax), double.Parse(Properties.Settings.Default.FALatMax), System.Drawing.Color.Green, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.FALongMin), double.Parse(Properties.Settings.Default.FALatMax), double.Parse(Properties.Settings.Default.FALongMax), double.Parse(Properties.Settings.Default.FALatMax), System.Drawing.Color.Magenta, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.FALongMin), double.Parse(Properties.Settings.Default.FALatMin), double.Parse(Properties.Settings.Default.FALongMin), double.Parse(Properties.Settings.Default.FALatMax), System.Drawing.Color.Red, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.OCLongMin), double.Parse(Properties.Settings.Default.OCLatMin), double.Parse(Properties.Settings.Default.OCLongMax), double.Parse(Properties.Settings.Default.OCLatMin), System.Drawing.Color.Blue, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.OCLongMax), double.Parse(Properties.Settings.Default.OCLatMin), double.Parse(Properties.Settings.Default.OCLongMax), double.Parse(Properties.Settings.Default.OCLatMax), System.Drawing.Color.Green, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.OCLongMin), double.Parse(Properties.Settings.Default.OCLatMax), double.Parse(Properties.Settings.Default.OCLongMax), double.Parse(Properties.Settings.Default.OCLatMax), System.Drawing.Color.Magenta, 1);
                graphHeatmap.Plot.AddLine(double.Parse(Properties.Settings.Default.OCLongMin), double.Parse(Properties.Settings.Default.OCLatMin), double.Parse(Properties.Settings.Default.OCLongMin), double.Parse(Properties.Settings.Default.OCLatMax), System.Drawing.Color.Red, 1);

                var EUcontLabel = graphHeatmap.Plot.AddText("Europe", double.Parse(Properties.Settings.Default.EULongMin), double.Parse(Properties.Settings.Default.EULatMax), size: 18);
                var JAcontLabel = graphHeatmap.Plot.AddText("Japan", double.Parse(Properties.Settings.Default.JALongMin), double.Parse(Properties.Settings.Default.JALatMax), size: 18);
                var NAcontLabel = graphHeatmap.Plot.AddText("NorthAmerica", double.Parse(Properties.Settings.Default.NALongMin), double.Parse(Properties.Settings.Default.NALatMax), size: 18);
                var SAcontLabel = graphHeatmap.Plot.AddText("SouthAmerica", double.Parse(Properties.Settings.Default.SALongMin), double.Parse(Properties.Settings.Default.SALatMax), size: 18);
                var AFcontLabel = graphHeatmap.Plot.AddText("Africa", double.Parse(Properties.Settings.Default.AFLongMin), double.Parse(Properties.Settings.Default.AFLatMax), size: 18);
                var OCcontLabel = graphHeatmap.Plot.AddText("Oceania", double.Parse(Properties.Settings.Default.OCLongMin), double.Parse(Properties.Settings.Default.OCLatMax), size: 18);
                var FAcontLabel = graphHeatmap.Plot.AddText("User", double.Parse(Properties.Settings.Default.FALongMin), double.Parse(Properties.Settings.Default.FALatMax), size: 18);
            }


            // now the actual points
            var hmap = graphHeatmap.Plot.AddBubblePlot();

            //plot the corner points
            for (int i = 0; i < cornerX.Length; i++)
                hmap.Add(x: cornerX[i], y: cornerY[i], radius: 3, fillColor: System.Drawing.Color.Red, edgeWidth: 1, edgeColor: System.Drawing.Color.Black);


            //this stops over running the array
            if (count - howMany < 0)
            {
                howMany = 5;  // maybe try 10 here
                

            }

            if (count >  arrSize - 100)
            {
                howMany = 1;  // maybe try 10 here
                count = 2;

            }


            for (int i = count - howMany; i < count; i++)
            {

                if (pZs[i] < dGreen && chkcr1.IsChecked == true)
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

            // I took this out by mistake.  seems to work OK wothout it
            //graphHeatmap.Plot.AxisScaleLock(true);
           

          //  graphHeatmap.Plot.Render();
        //    count = 0;
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
            data.Add("300");
            data.Add("400");
            data.Add("500");
            data.Add("600");
            data.Add("700");
            data.Add("800");
            data.Add("900");
            data.Add("1000");
            data.Add("2000");
            data.Add("3000");
            data.Add("4000");
            data.Add("5000");
            

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
                case "300":
                    numberOfPoints = 300;
                    break;
                case "400":
                    numberOfPoints = 400;
                    break;
                case "500":
                    numberOfPoints = 500;
                    break;
                case "600":
                    numberOfPoints = 600;
                    break;
                case "700":
                    numberOfPoints = 700;
                    break;
                case "800":
                    numberOfPoints = 800;
                    break;
                case "900":
                    numberOfPoints = 900;
                    break;
                case "1000":
                    numberOfPoints = 1000;
                    break;
                case "2000":
                    numberOfPoints = 2000;
                    break;
                case "3000":
                    numberOfPoints = 3000;
                    break;
                case "4000":
                    numberOfPoints = 4000;
                    break;
                case "5000":
                    numberOfPoints = 5000;
                    break;
                case "All":
                    numberOfPoints = arrSize;
                    break;


            }



        //    PrepareArrays();

        }//end

        private void chkcr1_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
