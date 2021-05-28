using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PropoPlot
{
    public partial class MainWindow 
    {
        // this is a helper class, an extension of the main

 
        private double[] smoothArray(double[] arr)
        {


            for (int i = 0; i < arr.Length - 1; i++)
            {
                double firstVal = arr[i];
                double nextVal = arr[i + 1];
                if (nextVal == -30)
                    arr[i + 1] = firstVal;
            }
            return arr;
        }


        public void colourQSO(List<string> message )
        {

            string cont = "";

            foreach (var item in message)
            {

                cont = item.Substring(item.Length - 5, 2);


                if (cont == "DX")  // this is our special continent
                {
                    System.Windows.Documents.Run run = new System.Windows.Documents.Run(item);
                    run.Foreground = System.Windows.Media.Brushes.Green;
                    run.FontWeight = FontWeights.ExtraBold;
                    run.FontSize = 10;
                    plotmessage.Inlines.Add(run);
                }

                else if ( cont == "FA" )  // this is our special continent
                {
                    System.Windows.Documents.Run run = new System.Windows.Documents.Run(item);
                    run.Foreground = System.Windows.Media.Brushes.Red;
                    run.FontWeight = FontWeights.ExtraBold;
                    run.FontSize = 10;
                    plotmessage.Inlines.Add(run);
                }
                else if (cont == "AF" )
                {
                    System.Windows.Documents.Run run = new System.Windows.Documents.Run(item);
                    run.Foreground = System.Windows.Media.Brushes.Black;
                    plotmessage.Inlines.Add(run);
                }
                else if (cont == "JA" )
                {
                    System.Windows.Documents.Run run = new System.Windows.Documents.Run(item);
                    run.Foreground = System.Windows.Media.Brushes.Black;
                    plotmessage.Inlines.Add(run);
                }
                else if (cont == "SA" )
                {
                    System.Windows.Documents.Run run = new System.Windows.Documents.Run(item);
                    run.Foreground = System.Windows.Media.Brushes.Black;
                    plotmessage.Inlines.Add(run);
                }
                else if (cont == "NA" )
                {
                    System.Windows.Documents.Run run = new System.Windows.Documents.Run(item);
                    run.Foreground = System.Windows.Media.Brushes.Black;
                    plotmessage.Inlines.Add(run);
                }
                else if (cont == "EU")
                {
                    System.Windows.Documents.Run run = new System.Windows.Documents.Run(item);
                    run.Foreground = System.Windows.Media.Brushes.Black;
                    plotmessage.Inlines.Add(run);
                }
                else if (cont == "OC")
                {
                    System.Windows.Documents.Run run = new System.Windows.Documents.Run(item);
                    run.Foreground = System.Windows.Media.Brushes.Black;
                    plotmessage.Inlines.Add(run);
                }
                else
                {
                    System.Windows.Documents.Run run = new System.Windows.Documents.Run(item);
                    run.Foreground = System.Windows.Media.Brushes.DarkViolet;
                    plotmessage.Inlines.Add(run);
                }
            }//end of foreach
           // plotmessage.Text =  ;  //write all back to the display
        }























    }//end of class
}//end
