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

namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphPlot.xaml
    /// </summary>
    public partial class graphPlot : Window
    {

        List<string> _thlist;

        double[] dataX = new double[10000] ;
        double[] dataEU = new double[10000] ;
        double[] dataEUA = new double[10000] ;
        double[] dataEUC = new double[10000] ;

        double[] dataJA = new double[10000] ;
        double[] dataJAA = new double[10000] ;
        double[] dataJAC = new double[10000] ;

        double[] dataNA = new double[10000] ;
        double[] dataNAA = new double[10000] ;
        double[] dataNAC = new double[10000] ;
        double[] dataOC = new double[10000] ;
        double[] dataOCA = new double[10000] ;
        double[] dataOCC = new double[10000] ;
        double[] dataAF = new double[10000] ;
        double[] dataAFA = new double[10000] ;
        double[] dataAFC = new double[10000] ;
        double[] dataSA = new double[10000] ;
        double[] dataSAA = new double[10000] ;
        double[] dataSAC = new double[10000] ;
        double[] dataFA = new double[10000] ;
        double[] dataFAA = new double[10000] ;
        double[] dataFAC = new double[10000] ;

        public graphPlot(List<string> thlist)
        {
            InitializeComponent();

            _thlist = thlist;

            PrepareArrays();
            PlotTheLists();
        }





        private void PrepareArrays() {




            string time = "";

            
            
            int count = 0;
            foreach (var item in _thlist)
            {
               // double defaultValue = 0;

                string[] wrdmsg = item.Split(',');

                dataX[count] = count; //the X values
                 double.TryParse(wrdmsg[2],out dataEU[count]); //Europe
                 double.TryParse(wrdmsg[3],out dataEUA[count]); //EuropeAverage
                 double.TryParse(wrdmsg[4],out dataEUC[count]); //EuropeAverage
                 double.TryParse(wrdmsg[5],out dataJA[count]); //Japan
                 double.TryParse(wrdmsg[6],out dataJAA[count]); //JapanAvg
                 double.TryParse(wrdmsg[7],out dataJAC[count]); //JapanAvg

                 double.TryParse(wrdmsg[8],out dataNA[count]);
                 double.TryParse(wrdmsg[9],out dataNAA[count]);
                 double.TryParse(wrdmsg[10],out dataNAC[count]);
                 double.TryParse(wrdmsg[11],out dataOC[count]);
                 double.TryParse(wrdmsg[12],out dataOCA[count]);
                 double.TryParse(wrdmsg[13],out dataOCC[count]);
                 double.TryParse(wrdmsg[14],out dataAF[count]);
                 double.TryParse(wrdmsg[15],out dataAFA[count]);
                 double.TryParse(wrdmsg[16],out dataAFC[count]);
                 double.TryParse(wrdmsg[17],out dataSA[count]);
                 double.TryParse(wrdmsg[18],out dataSAA[count]);
                 double.TryParse(wrdmsg[19],out dataSAC[count]);
                 double.TryParse(wrdmsg[20],out dataFA[count]);
                 double.TryParse(wrdmsg[21],out dataFAA[count]);
                 double.TryParse(wrdmsg[22],out dataFAC[count]);
                //dataOC[count] = double.TryParse(wrdmsg[6],out 0);

                count += 1;
            }

            // get rid of the 0's on the end
            Array.Resize(ref dataX, count );
            Array.Resize(ref dataEU, count );
            Array.Resize(ref dataEUA, count );
            Array.Resize(ref dataEUC, count );
            Array.Resize(ref dataJA, count );
            Array.Resize(ref dataJAA, count );
            Array.Resize(ref dataJAC, count );
            Array.Resize(ref dataNA, count );
            Array.Resize(ref dataNAA, count );
            Array.Resize(ref dataNAC, count );
            Array.Resize(ref dataOC, count );
            Array.Resize(ref dataOCA, count );
            Array.Resize(ref dataOCC, count );
            Array.Resize(ref dataAF, count );
            Array.Resize(ref dataAFA, count );
            Array.Resize(ref dataAFC, count );
            Array.Resize(ref dataSA, count );
            Array.Resize(ref dataSAA, count );
            Array.Resize(ref dataSAC, count );
            Array.Resize(ref dataFA, count );
            Array.Resize(ref dataFAA, count );
            Array.Resize(ref dataFAC, count );
            

           

        }

        private void PlotTheLists() {


            dataPlotEU.plt.PlotScatter(dataX, dataEU,label:"Eu"); 
            dataPlotEU.plt.PlotScatter(dataX, dataEUA,label:"EuAvg" , lineWidth: 2, markerSize: 4, lineStyle: LineStyle.Solid);
            dataPlotEU.plt.PlotScatter(dataX, dataEUC,label:"EuCnt");
            dataPlotEU.plt.Legend(location: legendLocation.lowerLeft);
            dataPlotEU.plt.YLabel("dBm");
            dataPlotEU.plt.XLabel("Periods");

            dataPlotJA.plt.PlotScatter(dataX, dataJA, label:"Ja", lineWidth: 1, markerSize: 4, lineStyle:LineStyle.DashDot);
            dataPlotJA.plt.PlotScatter(dataX, dataJAA, label:"JaAvg", lineWidth: 2, markerSize: 4, lineStyle:LineStyle.Solid);
            dataPlotJA.plt.PlotScatter(dataX, dataJAC, label:"JaCnt", lineWidth: 2, markerSize: 4, lineStyle:LineStyle.Solid);
            dataPlotJA.plt.Legend(location: legendLocation.lowerLeft);
            dataPlotJA.plt.YLabel("dBm");
            dataPlotJA.plt.XLabel("Periods");

            dataPlotNA.plt.PlotScatter(dataX, dataNA,label:"Na") ;
            dataPlotNA.plt.PlotScatter(dataX, dataNAA,label:"NaAvg", lineWidth: 2, markerSize: 4, lineStyle: LineStyle.Solid) ;
            dataPlotNA.plt.PlotScatter(dataX, dataNAC,label:"NaCnt") ;
            dataPlotNA.plt.Legend(location: legendLocation.lowerLeft);
            dataPlotNA.plt.YLabel("dBm");
            dataPlotNA.plt.XLabel("Periods");


            dataPlotOC.plt.PlotScatter(dataX, dataOC, label:"Oc", lineStyle: LineStyle.DashDot);
            dataPlotOC.plt.PlotScatter(dataX, dataOCA, label:"OcAvg", lineWidth: 2, lineStyle: LineStyle.Solid);
            dataPlotOC.plt.PlotScatter(dataX, dataOCC, label:"OcCnt", lineStyle: LineStyle.DashDot);
            dataPlotOC.plt.Legend(location: legendLocation.lowerLeft);
            dataPlotOC.plt.YLabel("dBm");
            dataPlotOC.plt.XLabel("Periods");

            dataPlotAF.plt.PlotScatter(dataX, dataAF, label:"Af", lineStyle: LineStyle.DashDot);
            dataPlotAF.plt.PlotScatter(dataX, dataAFA, label:"AfAvg", lineWidth: 2, markerSize: 4, lineStyle: LineStyle.Solid);
            dataPlotAF.plt.PlotScatter(dataX, dataAFC, label:"AfCnt", lineStyle: LineStyle.DashDot);
            dataPlotAF.plt.Legend(location: legendLocation.lowerLeft);
            dataPlotAF.plt.YLabel("dBm");
            dataPlotAF.plt.XLabel("Periods");

            dataPlotSA.plt.PlotScatter(dataX, dataSA, label: "Sa", lineStyle: LineStyle.DashDot);
            dataPlotSA.plt.PlotScatter(dataX, dataSAA, label: "SaAvg", lineWidth: 2, markerSize: 4, lineStyle: LineStyle.Solid);
            dataPlotSA.plt.PlotScatter(dataX, dataSAC, label: "SaCnt", lineStyle: LineStyle.DashDot);
            dataPlotSA.plt.Legend(location: legendLocation.lowerLeft);
            dataPlotSA.plt.YLabel("dBm");
            dataPlotSA.plt.XLabel("Periods");

            dataPlotFA.plt.PlotScatter(dataX, dataFA, label: "Fa", lineStyle: LineStyle.DashDot);
            dataPlotFA.plt.PlotScatter(dataX, dataFAA, label: "FaAvg", lineWidth: 2, markerSize: 4, lineStyle: LineStyle.Solid);
            dataPlotFA.plt.PlotScatter(dataX, dataFAC, label: "FaCnt", lineStyle: LineStyle.DashDot);
            dataPlotFA.plt.Legend(location: legendLocation.lowerLeft);
            dataPlotFA.plt.YLabel("dBm");
            dataPlotFA.plt.XLabel("Periods");



            //           dataPlot.plt.PlotScatter(dataX, dataAF, label:"Af");
            //           dataPlot.plt.PlotScatter(dataX, dataSA, label:"Sa");
            //           dataPlot.plt.PlotScatter(dataX, dataFA, label:"Usr",lineWidth:3,markerSize:4);



            //  dataPlot.plt.PlotScatter(xs, DataGen.Cos(xs));


        }
    }
}
