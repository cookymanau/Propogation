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
        double[] dataX = new double[1000] ;
        double[] dataEU = new double[1000] ;
        double[] dataEUA = new double[1000] ;
        double[] dataJA = new double[1000] ;
        double[] dataJAA = new double[1000] ;
        double[] dataNA = new double[1000] ;
        double[] dataNAA = new double[1000] ;
        double[] dataOC = new double[1000] ;
        double[] dataOCA = new double[1000] ;
        double[] dataAF = new double[1000] ;
        double[] dataAFA = new double[1000] ;
        double[] dataSA = new double[1000] ;
        double[] dataSAA = new double[1000] ;
        double[] dataFA = new double[1000] ;
        double[] dataFAA = new double[1000] ;

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

                dataX[count] = count;
                 double.TryParse(wrdmsg[2],out dataEU[count]); //Europe
                 double.TryParse(wrdmsg[3],out dataEUA[count]); //EuropeAverage
                 double.TryParse(wrdmsg[5],out dataJA[count]); //Japan
                 double.TryParse(wrdmsg[6],out dataJAA[count]); //JapanAvg

                 double.TryParse(wrdmsg[8],out dataNA[count]);
                 double.TryParse(wrdmsg[9],out dataNAA[count]);
                 double.TryParse(wrdmsg[11],out dataOC[count]);
                 double.TryParse(wrdmsg[12],out dataOCA[count]);
                 double.TryParse(wrdmsg[14],out dataAF[count]);
                 double.TryParse(wrdmsg[17],out dataSA[count]);
                 double.TryParse(wrdmsg[20],out dataFA[count]);
                //dataOC[count] = double.TryParse(wrdmsg[6],out 0);

                count += 1;
            }

            // get rid of the 0's on the end
            Array.Resize(ref dataX, count );
            Array.Resize(ref dataEU, count );
            Array.Resize(ref dataEUA, count );
            Array.Resize(ref dataJA, count );
            Array.Resize(ref dataJAA, count );
            Array.Resize(ref dataNA, count );
            Array.Resize(ref dataNAA, count );
            Array.Resize(ref dataOC, count );
            Array.Resize(ref dataOCA, count );
            Array.Resize(ref dataAF, count );
            Array.Resize(ref dataSA, count );
            Array.Resize(ref dataFA, count );
            

           

        }

        private void PlotTheLists() {


            dataPlot.plt.PlotScatter(dataX, dataEU,label:"Eu"); 
            dataPlot.plt.PlotScatter(dataX, dataEUA,label:"EuAvg"); 
           
            dataPlot.plt.PlotScatter(dataX, dataJA, label:"Ja", lineWidth: 3, markerSize: 4, lineStyle:LineStyle.DashDot);
            dataPlot.plt.PlotScatter(dataX, dataJAA, label:"JaAvg", lineWidth: 3, markerSize: 4, lineStyle:LineStyle.DashDot);
            dataPlot.plt.PlotScatter(dataX, dataNA,label:"Na") ;
            dataPlot.plt.PlotScatter(dataX, dataNAA,label:"NaAvg") ;
           dataPlot.plt.PlotScatter(dataX, dataOC, label:"Oc", lineStyle: LineStyle.DashDot);
           dataPlot.plt.PlotScatter(dataX, dataOCA, label:"OcAvg", lineStyle: LineStyle.DashDot);
//           dataPlot.plt.PlotScatter(dataX, dataAF, label:"Af");
//           dataPlot.plt.PlotScatter(dataX, dataSA, label:"Sa");
//           dataPlot.plt.PlotScatter(dataX, dataFA, label:"Usr",lineWidth:3,markerSize:4);

            dataPlot.plt.Legend();
            dataPlot.plt.YLabel("dBm");
            dataPlot.plt.XLabel("Periods");
            
          //  dataPlot.plt.PlotScatter(xs, DataGen.Cos(xs));
        
        
        }
    }
}
