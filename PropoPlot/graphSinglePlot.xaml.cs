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

            //add the context menu
            

            // make the graph
            _thlist = thlist;
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
            //get the tools options user selected things

            int AvgLineThickness = int.Parse(Properties.Settings.Default.AvgLineThick);
            int RawLineThickness = int.Parse(Properties.Settings.Default.RawLineThick);
            int CntLineThickness = int.Parse(Properties.Settings.Default.CntLineThick);
            int  LineAvgDotSize = int.Parse(Properties.Settings.Default.GraphAvgDotSize);
            int  LineRawDotSize = int.Parse(Properties.Settings.Default.GraphRawDotSize);
            int  LineCntDotSize = int.Parse(Properties.Settings.Default.GraphCntDotSize);

                        
            graphSingle.plt.PlotHLine(0, color: System.Drawing.Color.Black);
            
            if (chkEUGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataEUA, label: "EUAvg", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EUAvgColor)));
                    //graphSingle.plt.PlotScatter(Dubdates, dataEUA, label: "EUAvg", markerSize: 0, lineWidth: 3, lineStyle: LineStyle.Solid, color: System.Drawing.Color.DarkBlue);
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataEUC, label: "EUCnt", markerSize: LineCntDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EUCntColor)));
                    //graphSingle.plt.PlotScatter(Dubdates, dataEUC, label: "EUCnt", markerSize: 0, lineWidth: 2, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataEUR, label: "EURaw", markerSize: LineRawDotSize, lineWidth:RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EURawColor)));
                    //graphSingle.plt.PlotScatter(Dubdates, dataEUR, label: "EURaw", markerSize: 0, lineStyle: LineStyle.Solid, color: System.Drawing.Color.Blue);
            }

            if (chkJAGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataJAA, label: "JAAvg", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JAAvgColor)));
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataJAC, label: "JACnt", markerSize: LineCntDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JACntColor)));
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataJAR, label: "JARaw", markerSize: LineRawDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JARawColor)));

            }

            if (chkNAGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataNAA, label: "NAAvg", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NAAvgColor)));
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataNAC, label: "NACnt", markerSize: LineCntDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NACntColor)));
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataNAR, label: "NARaw", markerSize: LineRawDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NARawColor)));

            }

            if (chkOCGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                graphSingle.plt.PlotScatter(Dubdates,dataOCA, label: "OCAvg", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCAvgColor)));
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates,dataOCC, label: "OCCnt", markerSize: LineCntDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCCntColor)));
                if (chkRawPointsGraphs.IsChecked == true)
                     graphSingle.plt.PlotScatter(Dubdates,dataOCR, label: "OCRaw", markerSize: LineRawDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCRawColor)));

 
            }

            if (chkAFGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataAFA, label: "AFAvg", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.AFAvgColor)));
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataAFC, label: "AFCnt", markerSize: LineCntDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.AFCntColor)));
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataAFR, label: "AFRaw", markerSize: LineRawDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.AFRawColor)));

            }

            if (chkSAGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataSAA, label: "SAAvg", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.SAAvgColor)));
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataSAC, label: "SACnt", markerSize: LineCntDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.SACntColor)));
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataSAR, label: "SARaw", markerSize: LineRawDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.SARawColor)));

            }



            if (chkFAGraphs.IsChecked == true)
            {
                if (chkAvgPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataFAA, label: "UsrAvg", markerSize: LineAvgDotSize, lineWidth: AvgLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.FAAvgColor)));
                if (chkCountsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataFAC, label: "UsrCnt", markerSize: LineCntDotSize, lineWidth: CntLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.FACntColor)));
                if (chkRawPointsGraphs.IsChecked == true)
                    graphSingle.plt.PlotScatter(Dubdates, dataFAR, label: "UsrRaw", markerSize: LineRawDotSize, lineWidth: RawLineThickness, lineStyle: LineStyle.Solid, color: (ColorTranslator.FromHtml(Properties.Settings.Default.FARawColor)));
            }




            graphSingle.plt.Legend(location: Alignment.LowerLeft);
            graphSingle.plt.YLabel("dBm");
            graphSingle.plt.XLabel("Time(Zulu)");
            //graphSingle.plt.Ticks(dateTimeX: true, dateTimeFormatStringX: "HH:mm:ss"); //this is obsolete now
            graphSingle.plt.XAxis.DateTimeFormat(true);
            
          //  graphSingle.plt.YAxis.Layout(null,   -30, 20);// setting the Y axis limits  the new way  the first is padding
          
           




        }//end

        public void graphSingleRedraw_Click(object sender, RoutedEventArgs e)
        {

            if (chkLiveUpdate.IsChecked == true)
            {
                graphSingleRedraw.Content = "Live";
                // now start the timer to process the UDP stuff now that we have started it.
                System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer2.Tick += new EventHandler(dispatcherTimer2_Tick);
                dispatcherTimer2.Interval = new TimeSpan(0, 0, 10);
                dispatcherTimer2.Start();

            }
            else
                graphSingleRedraw.Content = "Plot";
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
            //PlotTheLists();

            //this isnt working
            if (chkKeepZoom.IsChecked == true) //ie we are ticked
            {
                PlotTheLists();
                var axislimit = graphSingle.plt.GetAxisLimits();
         

                float xmin = (float)axislimit.XMin;
                float xmax = (float)axislimit.XMax;

                //graphSingle.plt.XAxis.Layout(50, xmin, xmax);

                graphSingle.plt.SetAxisLimitsX(axislimit.XMax - 0.0001, axislimit.XMax);
                graphSingle.plt.AxisPan(0.0001, 0);
                graphSingle.Render();
            }
            else
            {
            PlotTheLists();
                graphSingle.Render();
            }


        }

        private void chkEUGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkJAGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkNAGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkOCGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkAFGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkSAGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkFAGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkAvgPointsGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkRawPointsGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkCountsGraphs_Click(object sender, RoutedEventArgs e)
        {
            redrawThePlot();
        }

        private void chkKeepZoom_Click(object sender, RoutedEventArgs e)
        {
            double yy = graphSingle.Width;
            double ht = graphSingle.ActualHeight; //size of the actual box in pixels
            double wd = graphSingle.ActualWidth;
            (double x, double y) =graphSingle.GetMouseCoordinates();
            double a1 = graphSingle.MaxWidth;
            double a2 = graphSingle.MaxHeight;
         //   graphSingle.Configure(enableScrollWheelZoom: false);
          //  graphSingle.Configure(lockHorizontalAxis: true);
           // graphSingle.Configure();


            

        }

        private void graphSingle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this is nothing like the cook book recipe for context menu
            // comes from https://github.com/ScottPlot/ScottPlot/issues/337
            // and some other sleuthing

            graphSingle.RightClicked -= graphSingle.DefaultRightClickEvent; //this removes the original context menu
            

            //these are the new menu Items
            MenuItem zoomHereItem = new MenuItem() { Header = "Hold Zoom" };
            zoomHereItem.Click += zoomHere;
            MenuItem clearPlotMenuItem = new MenuItem() { Header = "Clear Plot" };
            clearPlotMenuItem.Click += ClearPlot;

            //here we add the menu items to the context menu
            ContextMenu rightClickMenu = new ContextMenu();  //instantiate the object
            rightClickMenu.Items.Add(zoomHereItem);
            rightClickMenu.Items.Add(clearPlotMenuItem);

            graphSingle.ContextMenu = rightClickMenu;
            

        }

        //and here is where we do the work ****vvvvvvvvvv******
  
        private void zoomHere(object sender, RoutedEventArgs e)
        {
           
            Double nnX;
            
            (double x, double y) = graphSingle.GetMouseCoordinates();
            var axislimit = graphSingle.plt.GetAxisLimits();

            float xmin = (float)axislimit.XMin;
            float xmax = (float)axislimit.XMax;

            graphSingle.plt.XAxis.Layout(null, xmin,xmax );
            graphSingle.plt.YAxis.Layout(null, (float)axislimit.YMin, (float)axislimit.YMax);

//            var axislimit = graphSingle.plt.GetAxisLimits();
            graphSingle.plt.SetAxisLimitsX(axislimit.XMax - 0.0001, axislimit.XMax);

            //convert the x from a big number to a time
            // DateTime dtX = DateTime.Parse(x);



           //  graphSingle.plt.Axis(x - 0.0001, x+0.0001, -30, 20);// setting the Y axis limits
           // graphSingle.plt.AxisBounds(x - .0001, x + .0001, -30, 20);

       //     graphSingle.Render();

        }

        private void ClearPlot(object sender, RoutedEventArgs e)
        {
            graphSingle.plt.Clear();
            graphSingle.plt.AxisAuto();
            //???redrawThePlot();
            graphSingle.Render();
        }

        //The context menu items above here


    }
}
