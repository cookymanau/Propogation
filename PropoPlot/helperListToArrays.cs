using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public void colourQSO(string latitude, string longitude, string xDXA)
        {
            string message;
            message = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX: {ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10} \r\n";   //this just a display of data




            //if (aDXA == 1)  // this is our special continent
            //{
            //    System.Windows.Documents.Run run = new System.Windows.Documents.Run(message);
            //    run.Foreground = System.Windows.Media.Brushes.Green;
            //    run.FontWeight = FontWeights.ExtraBold;
            //    run.FontSize = 10;
            //    plotmessage.Inlines.Add(run);
            //    aDXA = 0;
            //    //over write the message
            //    message = $"UTC: {ul.udptime,-12}\tGrid: {ul.udpqso3,-6}\tdBm: {ul.udpdbm,-6}\tDX:{ul.udpqso2,-8}\tLat: {latitude,-10}\tLong: {longitude,-10} \r\n";   //this just a display of data
            //}


        }























    }//end of class
}//end
