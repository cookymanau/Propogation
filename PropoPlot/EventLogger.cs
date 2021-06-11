using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PropoPlot
{

    // mke this static so you can just refer to EventLogg.methodname any where
    // stole this from the internet   http://csharphelper.com/blog/2017/03/make-a-simple-event-logger-in-c/
    //


    public static class EventLogger
    {

        // Calculate the log file's name.
        static string fred = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\PropaPlotLog.txt";
        private static string LogFile = fred;
           // Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\PropaPlotLog.txt";
           //@"c:\Users\cooky\Documents\PropaPlotLog.txt";


        // Write the current date and time plus
        // a line of text into the log file.
        public static void WriteLine(string txt)
        {
            File.AppendAllText(LogFile,
                DateTime.Now.ToString() + ": " + txt + "\n");
        }

        // Delete the log file.
        public static void DeleteLog()
        {
            File.Delete(LogFile);
        }






    }
}
