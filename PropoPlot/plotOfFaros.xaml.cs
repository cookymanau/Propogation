using System;
using System.Collections.Generic;
using System.Data;  //for the dataGrid
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

namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for plotOfFaros.xaml
    /// </summary>
    public partial class plotOfFaros : Window
    {
        public plotOfFaros()
        {
            InitializeComponent();

            List<farosEUReading> frsEU = new List<farosEUReading>();   //frs is short for farodReadings
            frsEU.Add(new farosEUReading() { EU = -1.3 });//midnight
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });          
            frsEU.Add(new farosEUReading() { EU = -1.3 });//1am
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });           
            frsEU.Add(new farosEUReading() { EU = -1.3 });//2
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//3
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//4
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//5
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });//6
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//7am
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//8
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//9
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//10
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//11
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });//12
            frsEU.Add(new farosEUReading() { EU = -1.3 });//midnight
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//1am
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//2
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//3
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//4
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//5
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });//6
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//7am
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//8
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//9
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//10
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -1.3 });//11
            frsEU.Add(new farosEUReading() { EU = -1.4 });
            frsEU.Add(new farosEUReading() { EU = -4.4 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });
            frsEU.Add(new farosEUReading() { EU = -3.6 });//12

        



            List<farosJAReading> frsJA = new List<farosJAReading>();   //frs is short for farodReadings
            frsJA.Add(new farosJAReading() { JA = -1.3 });
            frsJA.Add(new farosJAReading() { JA = -1.4 });
            frsJA.Add(new farosJAReading() { JA = -4.4 });
            frsJA.Add(new farosJAReading() { JA = -3.6 });
            frsJA.Add(new farosJAReading() { JA = -1.3 });
            frsJA.Add(new farosJAReading() { JA = -1.4 });
            frsJA.Add(new farosJAReading() { JA = -4.4 });
            frsJA.Add(new farosJAReading() { JA = -3.6 });
            frsJA.Add(new farosJAReading() { JA = -1.3 });
            frsJA.Add(new farosJAReading() { JA = -1.4 });
            frsJA.Add(new farosJAReading() { JA = -4.4 });
            frsJA.Add(new farosJAReading() { JA = -3.6 });

            dgEU.ItemsSource = frsEU;
            dgJA.ItemsSource = frsJA;

            colourTheCells(dgEU);

        }

        private void colourTheCells(DataGrid mgrid)
        {



          //  string x = mgrid.Columns[0].GetCellContent(mgrid.Items[0]).ToString();

        }


           



        }// end of class

    public class farosEUReading
    {
        public double EU { get; set; }
    }

    public class farosJAReading
    {
        public double JA { get; set; }
    }
    public class farosNAReading
    {
        public double NA { get; set; }
    }
    public class farosSAReading
    {
        public double SA { get; set; }
    }
    public class farosOCReading
    {
        public double OC { get; set; }
    }
    public class farosAFReading
    {
        public double AF { get; set; }
    }
    public class farosFAReading
    {
        public double FA { get; set; }
    }



}//end of name space
