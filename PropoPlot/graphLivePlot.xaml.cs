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

 //***************************************
     //ProgName:  This is the timer plot of propogation Allowing you to see how propogation changes over time
     //Date:  MAy 2021, mostly working
     //Author:Ian Cook
     //Purose:See propogation change over time
     //Comment:This works!		
     //Updates:		
     //************************************************



namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphLivePlot.xaml
    /// </summary>
    public partial class graphLivePlot : Window
    {

        const int arrSize = 5000;

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

        ScottPlot.Plottable.ScatterPlot sigEUA;
       // ScottPlot.Plottable.SignalPlot sigEUA;
        ScottPlot.Plottable.ScatterPlot sigEUR;
        ScottPlot.Plottable.ScatterPlot sigEUC;
        ScottPlot.Plottable.ScatterPlot sigJAA;
        ScottPlot.Plottable.ScatterPlot sigJAR;
        ScottPlot.Plottable.ScatterPlot sigJAC;
        ScottPlot.Plottable.ScatterPlot sigOCA;
        ScottPlot.Plottable.ScatterPlot sigOCR;
        ScottPlot.Plottable.ScatterPlot sigOCC;
        ScottPlot.Plottable.ScatterPlot sigNAA;
        ScottPlot.Plottable.ScatterPlot sigNAR;
        ScottPlot.Plottable.ScatterPlot sigNAC;
        ScottPlot.Plottable.ScatterPlot sigSAA;
        ScottPlot.Plottable.ScatterPlot sigSAR;
        ScottPlot.Plottable.ScatterPlot sigSAC;
        ScottPlot.Plottable.ScatterPlot sigAFA;
        ScottPlot.Plottable.ScatterPlot sigAFR;
        ScottPlot.Plottable.ScatterPlot sigAFC;
        ScottPlot.Plottable.ScatterPlot sigFAA;
        ScottPlot.Plottable.ScatterPlot sigFAR;
        ScottPlot.Plottable.ScatterPlot sigFAC;

        ScottPlot.Plottable.LegendItem legi;
        


        /// <summary>
        /// The Constructor - happens only once
        /// </summary>
        /// <param name="alist"></param>
        public graphLivePlot(List<string> alist)
        {
            InitializeComponent();
           // txtPan.Text = Properties.Settings.Default.txtPan;  //load the default

            thlist = alist;  //this is where all of the data is

            setupContextMenu();

            chkFAGraphs.Content = Properties.Settings.Default.UsrDefinedName;

            PrepareArrays(); //this uses the sized arrays (done at declaration)
            PlotTheArrays(); //you only do this once-in the constructor
            buttonClickMethod();  //force the first button click


            //experimental to see how to add financial
            //OHLC[] ohlcs = DataGen.RandomStockPrices(null, 75);
            //var candlePlot = graphLive.Plot.AddCandlesticks(ohlcs);
            //var sma20 = candlePlot.GetSMA(20);






        }



        private void btnPlot_Click(object sender, RoutedEventArgs e)
        {
            buttonClickMethod();

 
       }

        /// <summary>
        /// Step 1 - load the arrays with the latest data
        /// Step 2 - update the number of points to show on the graph -its the entire populated array
        /// Step 3 - select the plots to draw , depending on what is selected
        /// Step 4 - render the plot
        /// </summary>
        private void buttonClickMethod()
        {

            PrepareArrays();  //Load new data into them from the list of strings (it DOES NOT resize them)

            sigEUA.MaxRenderIndex = (currentArrSize - 1);
            sigEUR.MaxRenderIndex = (currentArrSize - 1);
            sigEUC.MaxRenderIndex = (currentArrSize - 1);

            sigNAA.MaxRenderIndex = (currentArrSize - 1);
            sigNAR.MaxRenderIndex = (currentArrSize - 1);
            sigNAC.MaxRenderIndex = (currentArrSize - 1);

            sigSAA.MaxRenderIndex = (currentArrSize - 1);
            sigSAR.MaxRenderIndex = (currentArrSize - 1);
            sigSAC.MaxRenderIndex = (currentArrSize - 1);

            sigOCA.MaxRenderIndex = (currentArrSize - 1);
            sigOCR.MaxRenderIndex = (currentArrSize - 1);
            sigOCC.MaxRenderIndex = (currentArrSize - 1);

            sigJAA.MaxRenderIndex = (currentArrSize - 1);
            sigJAR.MaxRenderIndex = (currentArrSize - 1);
            sigJAC.MaxRenderIndex = (currentArrSize - 1);

            sigAFA.MaxRenderIndex = (currentArrSize - 1);
            sigAFR.MaxRenderIndex = (currentArrSize - 1);
            sigAFC.MaxRenderIndex = (currentArrSize - 1);

            sigFAA.MaxRenderIndex = (currentArrSize - 1);
            sigFAR.MaxRenderIndex = (currentArrSize - 1);
            sigFAC.MaxRenderIndex = (currentArrSize - 1);

            drawsSelectedGraphs();
            graphLive.Render();
        }

        /// <summary>
        /// Respond to the check boxes
        /// </summary>
        private void drawsSelectedGraphs()
        {
            //averages on
            if (chkAvgPointsGraphs.IsChecked == true)
            {
                if (chkEUGraphs.IsChecked == true)
                   sigEUA.IsVisible = true;
                else
                   sigEUA.IsVisible = false;

                if (chkJAGraphs.IsChecked == true)
                   sigJAA.IsVisible = true;
                else
                   sigJAA.IsVisible = false;

                if (chkNAGraphs.IsChecked == true)
                    sigNAA.IsVisible = true;
                else
                    sigNAA.IsVisible = false;

                if (chkSAGraphs.IsChecked == true)
                    sigSAA.IsVisible = true;
                else
                    sigSAA.IsVisible = false;

                if (chkOCGraphs.IsChecked == true)
                    sigOCA.IsVisible = true;
                else
                    sigOCA.IsVisible = false;

                if (chkAFGraphs.IsChecked == true)
                    sigAFA.IsVisible = true;
                else
                    sigAFA.IsVisible = false;

                if (chkFAGraphs.IsChecked == true)
                    sigFAA.IsVisible = true;
                else
                    sigFAA.IsVisible = false;
            }

            //averages off then everything is off
            if (chkAvgPointsGraphs.IsChecked == false)
            {
                    sigEUA.IsVisible = false;
                    sigJAA.IsVisible = false;
                    sigNAA.IsVisible = false;
                    sigSAA.IsVisible = false;
                    sigOCA.IsVisible = false;
                    sigAFA.IsVisible = false;
                    sigFAA.IsVisible = false;
            }







            //raw on
            if (chkRawPointsGraphs.IsChecked == true)
            {
                if (chkEUGraphs.IsChecked == true)
                    sigEUR.IsVisible = true;
                else
                    sigEUR.IsVisible = false;

                if (chkJAGraphs.IsChecked == true)
                    sigJAR.IsVisible = true;
                else
                    sigJAR.IsVisible = false;

                if (chkNAGraphs.IsChecked == true)
                    sigNAR.IsVisible = true;
                else
                    sigNAR.IsVisible = false;

                if (chkSAGraphs.IsChecked == true)
                    sigSAR.IsVisible = true;
                else
                    sigSAR.IsVisible = false;

                if (chkOCGraphs.IsChecked == true)
                    sigOCR.IsVisible = true;
                else
                    sigOCR.IsVisible = false;

                if (chkAFGraphs.IsChecked == true)
                    sigAFR.IsVisible = true;
                else
                    sigAFR.IsVisible = false;

                if (chkFAGraphs.IsChecked == true)
                    sigFAR.IsVisible = true;
                else
                    sigFAR.IsVisible = false;
            }

            //raw off
            if (chkRawPointsGraphs.IsChecked == false)
            {
                    sigEUR.IsVisible = false;
                    sigJAR.IsVisible = false;
                    sigNAR.IsVisible = false;
                    sigSAR.IsVisible = false;
                    sigOCR.IsVisible = false;
                    sigAFR.IsVisible = false;
                    sigFAR.IsVisible = false;
            }







            //counts on
            if (chkCountPointsGraphs.IsChecked == true)
            {
                if (chkEUGraphs.IsChecked == true)
                    sigEUC.IsVisible = true;
                else
                    sigEUC.IsVisible = false;

                if (chkJAGraphs.IsChecked == true)
                    sigJAC.IsVisible = true;
                else
                    sigJAC.IsVisible = false;

                if (chkNAGraphs.IsChecked == true)
                    sigNAC.IsVisible = true;
                else
                    sigNAC.IsVisible = false;

                if (chkSAGraphs.IsChecked == true)
                    sigSAC.IsVisible = true;
                else
                    sigSAC.IsVisible = false;

                if (chkOCGraphs.IsChecked == true)
                    sigOCC.IsVisible = true;
                else
                    sigOCC.IsVisible = false;

                if (chkAFGraphs.IsChecked == true)
                    sigAFC.IsVisible = true;
                else
                    sigAFC.IsVisible = false;

                if (chkFAGraphs.IsChecked == true)
                    sigFAC.IsVisible = true;
                else
                    sigFAC.IsVisible = false;
            }

            //counts off
            if (chkCountPointsGraphs.IsChecked == false)
            {
                    sigEUC.IsVisible = false;
                    sigJAC.IsVisible = false;
                    sigNAC.IsVisible = false;
                    sigSAC.IsVisible = false;
                    sigOCC.IsVisible = false;
                    sigAFC.IsVisible = false;
                    sigFAC.IsVisible = false;
            }
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
 
                count += 1;
            }
            currentArrSize = count;
        }

        public void PlotTheArrays()  //this is only ever called once - from the constructor
                {
            //get the tools options user selected things
            int AvgLineThickness = int.Parse(Properties.Settings.Default.AvgLineThick);
            int RawLineThickness = int.Parse(Properties.Settings.Default.RawLineThick);
            int CntLineThickness = int.Parse(Properties.Settings.Default.CntLineThick);
            int LineAvgDotSize = int.Parse(Properties.Settings.Default.GraphAvgDotSize);
            int LineRawDotSize = int.Parse(Properties.Settings.Default.GraphRawDotSize);
            int LineCntDotSize = int.Parse(Properties.Settings.Default.GraphCntDotSize);

            sigEUA = graphLive.Plot.AddScatter(Dubdates, dataEUA, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EUAvgColor)), label: "EUavg", lineWidth: AvgLineThickness, markerSize: LineAvgDotSize);
            sigEUR = graphLive.Plot.AddScatter(Dubdates, dataEUR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EURawColor)), label: "EUraw", lineWidth: RawLineThickness, markerSize: LineRawDotSize);
            sigEUC = graphLive.Plot.AddScatter(Dubdates, dataEUC, color: (ColorTranslator.FromHtml(Properties.Settings.Default.EUCntColor)), label: "EUcnt", lineWidth: CntLineThickness, markerSize: LineCntDotSize);


            sigNAA = graphLive.Plot.AddScatter(Dubdates,dataNAA, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NAAvgColor)), label:"NAavg", lineWidth: AvgLineThickness, markerSize: LineAvgDotSize);
            sigNAR = graphLive.Plot.AddScatter(Dubdates,dataNAR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NARawColor)), label:"NAraw", lineWidth: RawLineThickness, markerSize: LineRawDotSize);
            sigNAC = graphLive.Plot.AddScatter(Dubdates,dataNAC, color: (ColorTranslator.FromHtml(Properties.Settings.Default.NACntColor)), label:"NAcnt", lineWidth: CntLineThickness, markerSize: LineCntDotSize);

            sigJAA = graphLive.Plot.AddScatter(Dubdates,dataJAA, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JAAvgColor)), label:"JAavg", lineWidth: AvgLineThickness, markerSize: LineAvgDotSize);
            sigJAR = graphLive.Plot.AddScatter(Dubdates,dataJAR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JARawColor)), label:"JAraw", lineWidth: RawLineThickness, markerSize: LineRawDotSize);
            sigJAC = graphLive.Plot.AddScatter(Dubdates,dataJAC, color: (ColorTranslator.FromHtml(Properties.Settings.Default.JACntColor)), label:"JAcnt", lineWidth: CntLineThickness, markerSize: LineCntDotSize);
            
            sigSAA = graphLive.Plot.AddScatter(Dubdates,dataSAA, color: (ColorTranslator.FromHtml(Properties.Settings.Default.SAAvgColor)), label:"SAavg", lineWidth: AvgLineThickness, markerSize: LineAvgDotSize);
            sigSAR = graphLive.Plot.AddScatter(Dubdates,dataSAR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.SARawColor)), label:"SAraw", lineWidth: RawLineThickness, markerSize: LineRawDotSize);
            sigSAC = graphLive.Plot.AddScatter(Dubdates,dataSAC, color: (ColorTranslator.FromHtml(Properties.Settings.Default.SACntColor)), label:"SAcnt", lineWidth: CntLineThickness, markerSize: LineCntDotSize);
            
            sigOCA = graphLive.Plot.AddScatter(Dubdates,dataOCA, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCAvgColor)), label:"OCavg", lineWidth: AvgLineThickness, markerSize: LineAvgDotSize);
            sigOCR = graphLive.Plot.AddScatter(Dubdates,dataOCR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCRawColor)), label:"OCraw", lineWidth: RawLineThickness, markerSize: LineRawDotSize);
            sigOCC = graphLive.Plot.AddScatter(Dubdates,dataOCC, color: (ColorTranslator.FromHtml(Properties.Settings.Default.OCCntColor)), label:"OCcnt", lineWidth: CntLineThickness, markerSize: LineCntDotSize);
            
            sigAFA = graphLive.Plot.AddScatter(Dubdates,dataAFA, color: (ColorTranslator.FromHtml(Properties.Settings.Default.AFAvgColor)), label:"AFavg", lineWidth: AvgLineThickness, markerSize: LineAvgDotSize);
            sigAFR = graphLive.Plot.AddScatter(Dubdates,dataAFR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.AFRawColor)), label:"AFraw", lineWidth: RawLineThickness, markerSize: LineRawDotSize);
            sigAFC = graphLive.Plot.AddScatter(Dubdates,dataAFC, color: (ColorTranslator.FromHtml(Properties.Settings.Default.AFCntColor)), label:"AFcnt", lineWidth: CntLineThickness, markerSize: LineCntDotSize);
            
            sigFAA = graphLive.Plot.AddScatter(Dubdates,dataFAA, color: (ColorTranslator.FromHtml(Properties.Settings.Default.FAAvgColor)), label:"FAavg", lineWidth: AvgLineThickness, markerSize: LineAvgDotSize);
            sigFAR = graphLive.Plot.AddScatter(Dubdates,dataFAR, color: (ColorTranslator.FromHtml(Properties.Settings.Default.FARawColor)), label:"FAraw", lineWidth: RawLineThickness, markerSize: LineRawDotSize);
            sigFAC = graphLive.Plot.AddScatter(Dubdates,dataFAC, color: (ColorTranslator.FromHtml(Properties.Settings.Default.FACntColor)), label:"FAcnt", lineWidth: CntLineThickness, markerSize: LineCntDotSize);
            
            
            graphLive.Plot.Legend(location: Alignment.LowerLeft);
            graphLive.Plot.PlotHLine(0, color: System.Drawing.Color.Black);
            graphLive.Plot.PlotHLine(-30, color: System.Drawing.Color.Black);
            graphLive.Plot.XAxis.DateTimeFormat(true);
            graphLive.Plot.XAxis.TickLabelFormat("HH:mm:ss", true);
            graphLive.Plot.YLabel("dBm");
            graphLive.Plot.XLabel("Time(Zulu)");


        }

        //here we add the right clicker *vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
        private void setupContextMenu() { //call this from the constructor - only once

            graphLive.RightClicked -= graphLive.DefaultRightClickEvent; //this removes the original context menu
            MenuItem shoAll = new MenuItem() { Header = "Show All" };
            shoAll.Click += ShowAllItem;

            MenuItem clearPlotMenuItem = new MenuItem() { Header = "Test1" };
            clearPlotMenuItem.Click += ClearPlot;

            MenuItem LegendToggleMenuItem = new MenuItem() { Header = "Test2" };
            LegendToggleMenuItem.Click += legendToggle;


            //here we add the menu items to the context menu
            ContextMenu rightClickMenu = new ContextMenu();  //instantiate the object
            rightClickMenu.Items.Add(shoAll);
            rightClickMenu.Items.Add(clearPlotMenuItem);
            rightClickMenu.Items.Add(LegendToggleMenuItem);

            graphLive.ContextMenu = rightClickMenu;
        }


        //here are the methods for the right click menu
        private void graphLive_RightClicked(object sender, EventArgs e)
        {
            //this is nothing like the cook book recipe for context menu
            // comes from https://github.com/ScottPlot/ScottPlot/issues/337
            // and some other sleuthing
        }

        //and here is where we do the work ****vvvvvvvvvv******

        private void ShowAllItem(object sender, RoutedEventArgs e)
        {
            buttonClickMethod();
            graphLive.Plot.AxisAuto();
            graphLive.Render();

        }

       //using as a test bed
        private void ClearPlot(object sender, RoutedEventArgs e)
        {
            double dublePan = double.Parse(Properties.Settings.Default.txtPan); //this is supposed to tell me how much to pan
            graphLive.Plot.AxisPan(dublePan, 0);
            graphLive.Render();
        }

        //this is Test2
        private void legendToggle(object sender, RoutedEventArgs e)
        {
            double dublePan = double.Parse(Properties.Settings.Default.txtPan);
            var al =graphLive.Plot.GetAxisLimits(xAxisIndex:0);

            //graphLive.Plot.SetAxisLimitsX(al.XMax - (al.XSpan/2), al.XMax + (al.XSpan/2));
            //pan left just a wee bit

            double xma = al.XMax;
            double xmi = al.XMin;
            double xspan = al.XSpan;
            double xcentre = al.XCenter;

            double panleft = xspan * dublePan;  //say xcentre * .1 = 10% of the current  xspan
            // so make the new window xmi + panleft and  xma + panleft (shifting the window)

            graphLive.Plot.SetAxisLimitsX(al.XMin +panleft , al.XMax + panleft);

            graphLive.Render();
           
        }
        //The context menu items above here ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^


        //this is the Timer for live data.  It starts in response to clicking the check box
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
            if (chkShowAll.IsChecked == true)
            {
                graphLive.Plot.AxisAuto();
                buttonClickMethod();
                //graphLive.Render();
            }
            else if (chkLiveUpdate.IsChecked == true && chkPanLeft.IsChecked == true)
            {
                double dublePan = double.Parse(Properties.Settings.Default.txtPan); //this is supposed to tell me how much to pan
                var al = graphLive.Plot.GetAxisLimits(xAxisIndex: 0);
                //graphLive.Plot.SetAxisLimitsX(al.XMax - (al.XSpan / 2) + dublePan, al.XMax + (al.XSpan / 2) + dublePan);
                double xma = al.XMax;
                double xmi = al.XMin;
                double xspan = al.XSpan;
                double xcentre = al.XCenter;

                double panleft = xspan * dublePan;  //say xcentre * .1 = 10% of the current  xspan


                graphLive.Plot.SetAxisLimitsX(al.XMin + panleft, al.XMax + panleft);

                buttonClickMethod();
               
            }
            else 
                buttonClickMethod();

        }

        private void chkLiveUpdate_Unchecked(object sender, RoutedEventArgs e)
        {

            btnPlot.Content = "Plot";
            dispatcherTimer2.Stop();
        }

        //private void txtPan_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    Properties.Settings.Default.txtPan = txtPan.Text;
        //    Properties.Settings.Default.Save();
        //}
    }//end class
}//end
