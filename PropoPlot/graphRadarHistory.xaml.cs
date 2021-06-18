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
    /// Interaction logic for graphRadarHistory.xaml
    /// </summary>
    public partial class graphRadarHistory : Window
    {

        //arays to hold the data for one continent and one AVG, Raw or Count
        const int arrsize = 600;

        double[,] values = new double[2, arrsize];

        double[] piVals = new double[arrsize];

        List<string> thlist;
        public graphRadarHistory(List<string> athlist)
        {
            InitializeComponent();
            thlist = athlist;
            PrepareArrays();
 
            //this is our list of decodes


        }


        public void PrepareArrays()
        {

            string[] wrdmsg = { };
            int count = 0;
            var li = thlist.LastOrDefault();
            int listCount = thlist.Count();  // get the number of items in our list

            //   int howMany = int.Parse(cmboNumPoints.Text);

            // double defaultValue = 0;

            //     if (howMany > listCount)
            //         howMany = 1;

            //       for (int i = 0; i < listCount; i++)  // instead of array resizing, lets just limit how much work we do and get the last n records
            foreach (var item in thlist)
            {

                wrdmsg = item.Split(',');

                if (wrdmsg[4] != "")
                {

                    values[0, count] = 0;// double.Parse(wrdmsg[3]);
                    values[1, count] = double.Parse(wrdmsg[4]);
                    piVals[count] = double.Parse(wrdmsg[4]);

                    count += 1;
                    if (count >= arrsize)
                        break;

                }
            }

          var rp =  graphRH.Plot.AddRadar(values);
            rp.AxisType = RadarAxis.None;


         //   var piplot = graphRH.Plot.AddPie(piVals);





        }


        private void btnPlot_Click(object sender, RoutedEventArgs e)
        {

        }




    }//end class
}
