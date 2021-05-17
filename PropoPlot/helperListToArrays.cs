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




    }
}
