using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Helper class
/// </summary>
namespace PropoPlot
{
    public partial class MainWindow
    {

        private void setTimerBarColour(double value)
        {

            if (value < -15)
                timerBar.Foreground = System.Windows.Media.Brushes.LightYellow;

            if (value < -8)
                timerBar.Foreground = System.Windows.Media.Brushes.Aqua;

            if (value < 0)
                timerBar.Foreground = System.Windows.Media.Brushes.Blue;

            if (value == 0)
                timerBar.Foreground = System.Windows.Media.Brushes.LightGray;

            if (value > 0)
                timerBar.Foreground = System.Windows.Media.Brushes.Red;
        }

        private void setColourJA(double value)
        {
            if (value == -100)
                JAprog.Background = System.Windows.Media.Brushes.White;

            else if (value < -15)
            {
                JAprog.Background = System.Windows.Media.Brushes.Yellow;
                JAdbm.Foreground = System.Windows.Media.Brushes.Black;
            }
            else if (value < -8)
            {
                JAprog.Background = System.Windows.Media.Brushes.Aqua;
                JAdbm.Foreground = System.Windows.Media.Brushes.Black;
            }

            else if (value < 0)
            {
                JAprog.Background = System.Windows.Media.Brushes.Blue;
                JAdbm.Foreground = System.Windows.Media.Brushes.White;
            }
            else if (value == 0)
            {
                JAprog.Background = System.Windows.Media.Brushes.LightGray;
                JAdbm.Foreground = System.Windows.Media.Brushes.Black;
            }
            else if (value > 0)
            {
                JAprog.Background = System.Windows.Media.Brushes.Red;
                JAdbm.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void setColourEU(double value)
        {
            if (value == -100)
                EUprog.Background = System.Windows.Media.Brushes.White;

           else  if (value < -15)
            {
                EUprog.Background = System.Windows.Media.Brushes.Yellow;
                EUdbm.Foreground = System.Windows.Media.Brushes.Black;
            }
           else if (value < -8)
            {
                EUprog.Background = System.Windows.Media.Brushes.Aqua;
                EUdbm.Foreground = System.Windows.Media.Brushes.Black;
            }

            else if (value < 0)
            {
                EUprog.Background = System.Windows.Media.Brushes.Blue;
                EUdbm.Foreground = System.Windows.Media.Brushes.White;
            }
            else if (value == 0)
            {
                EUprog.Background = System.Windows.Media.Brushes.LightGray;
                EUdbm.Foreground = System.Windows.Media.Brushes.Black;
            }
            else if (value > 0)
            {
                EUprog.Background = System.Windows.Media.Brushes.Red;
                EUdbm.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void setColourNA(double value)
        {
            if (value == -100)
                NAprog.Background = System.Windows.Media.Brushes.White;

           else if (value < -15)
            {
                NAprog.Background = System.Windows.Media.Brushes.Yellow;
                NAdbm.Foreground = System.Windows.Media.Brushes.Black;
            }
            else if (value < -8)
            {
                NAprog.Background = System.Windows.Media.Brushes.Aqua;
                NAdbm.Foreground = System.Windows.Media.Brushes.Black;
            }

            else if (value < 0)
            {
                NAprog.Background = System.Windows.Media.Brushes.Blue;
                NAdbm.Foreground = System.Windows.Media.Brushes.White;
            }
            else if (value == 0)
            {
                NAprog.Background = System.Windows.Media.Brushes.LightGray;
                NAdbm.Foreground = System.Windows.Media.Brushes.Black;
            }
            else if (value > 0)
            {
                NAprog.Background = System.Windows.Media.Brushes.Red;
                NAdbm.Foreground = System.Windows.Media.Brushes.Black;
            }
        }


    }//end class
}//end
