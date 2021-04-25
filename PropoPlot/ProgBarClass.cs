using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

/// <summary>
/// Helper class
/// </summary>
namespace PropoPlot
{
    public partial class MainWindow
    {

      //  continentData cd = new continentData();
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

  

        private void setBarColour(double value,ProgressBar barname,TextBlock contDbm)
        {
            
            if (value == -100)
                barname.Background = System.Windows.Media.Brushes.White;

            else if (value < -15)
            {
                barname.Background = System.Windows.Media.Brushes.Yellow;
                contDbm.Foreground = System.Windows.Media.Brushes.Black;
            }
            else if (value < -8)
            {
                barname.Background = System.Windows.Media.Brushes.Aqua;
                contDbm.Foreground = System.Windows.Media.Brushes.Black;
            }

            else if (value < 0)
            {
                barname.Background = System.Windows.Media.Brushes.Blue;
                contDbm.Foreground = System.Windows.Media.Brushes.White;
            }
            else if (value == 0)
            {
                barname.Background = System.Windows.Media.Brushes.LightGray;
                contDbm.Foreground = System.Windows.Media.Brushes.Black;
            }
            else if (value > 0)
            {
                barname.Background = System.Windows.Media.Brushes.Red;
                contDbm.Foreground = System.Windows.Media.Brushes.Black;
            }
        }



        //************* this is a the stuff that calculates each continents Average dBm over some defined period.  Currently 4 decoding periods - 1 minute for FT8

       
        int FAavgsCnt = 0;
        public void runningFAContinentalAverage(double pAv,ProgressBar barname, TextBlock contDbm, int counterFA,double[] arr)
        {
          //  double totaldbmFA = 0;
            double sumFAarray = 0;
            double averageFAarray = 0;
           // int counterFA = 0;

            FAdbmCount.Text = counterFA.ToString(); //this is the number of stations decoded

            arr[FAavgsCnt] = pAv;

            sumFAarray = arr.Aggregate((total, next) => total + next);
            averageFAarray = Math.Round(sumFAarray / 4, 1);
           // rTotal.Text = sumFAarray.ToString(); //sums the array of whatever is in it..uses LINQ

            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > 3) //we have filled the array
            {
               // rAverage.Text = averageFAarray.ToString();
                FAdbm.Text = averageFAarray.ToString();  //this is the current period average.  We should also see it down in the array represerntation
                setBarColour(averageFAarray, barname, contDbm);  //this is the average
            }
          //  arraycounterFa.Text = FAavgsCnt.ToString();  //show the current count value 0,1,2,3

            if (FAavgsCnt == 3)
            {
                FAavgsCnt = -1;  //start again
            }

            FAavgsCnt += 1;

            //thats the end lets store something
            cd.pFAdbm = pAv.ToString();
            cd.pFAnumber = counterFA.ToString();

        }


        int JAavgsCnt = 0;
        public void runningJAContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)
        {
            double sumFAarray = 0;
            double averageOfarray = 0;
            
            JAdbmCount.Text = counter.ToString(); //this is the number of stations decoded
            arr[JAavgsCnt] = pAv; //*********

            sumFAarray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumFAarray / 4, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 
 
            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > 3) //we have filled the array
            {
               // rAverage.Text = averageOfarray.ToString();
                JAdbm.Text = averageOfarray.ToString();  //this is the current period average.  We should also see it down in the array represerntation
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }
 
            if (JAavgsCnt == 3)//this is 0,1,2,3 ie 4 slots in the array
                   JAavgsCnt = -1;  //start again
            
            JAavgsCnt += 1;

            cd.pJAdbm = pAv.ToString();
            cd.pJAnumber = counter.ToString();
        }//end of the averaging

        int EUavgsCnt = 0;
        public void runningEUContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)
        {
            double sumOfArray = 0;
            double averageOfarray = 0;

            EUdbmCount.Text = counter.ToString(); //this is the number of stations decoded
            arr[EUavgsCnt] = pAv;  //**********

            sumOfArray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumOfArray / 4, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 
           // rTotal.Text = sumOfArray.ToString(); //sums the array of whatever is in it..uses LINQ

            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > 3) //we have filled the array
            {
              //  rAverage.Text = averageOfarray.ToString();
                EUdbm.Text = averageOfarray.ToString();  //this is the current period average.  We should also see it down in the array represerntation
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

            if (EUavgsCnt == 3)//this is 0,1,2,3 ie 4 slots in the array
                EUavgsCnt = -1;  //start again

            EUavgsCnt += 1;
            cd.pEUdbm = pAv.ToString();
            cd.pEUnumber = counter.ToString();
        }//end of the averaging

        int NAavgsCnt = 0;  //****this one
        public void runningNAContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)  //***this one
        {
            double sumOfArray = 0;
            double averageOfarray = 0;

            NAdbmCount.Text = counter.ToString(); //this is the number of stations decoded   ***this one
            arr[NAavgsCnt] = pAv; //*********

            sumOfArray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumOfArray / 4, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 
           
            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > 3) //we have filled the array
            {
                NAdbm.Text = averageOfarray.ToString();  //****this one
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

            if (EUavgsCnt == 3)//this is 0,1,2,3 ie 4 slots in the array
                NAavgsCnt = -1;  //start again   **** this one

            NAavgsCnt += 1;  //****this one
            cd.pNAdbm = pAv.ToString();
            cd.pNAnumber = counter.ToString();
        }//end of the av


        int OCavgsCnt = 0;  //****this one
        public void runningOCContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)  //***this one
        {
            double sumOfArray = 0;
            double averageOfarray = 0;

            OCdbmCount.Text = counter.ToString(); //***this is the number of stations decoded   ***this one
            arr[OCavgsCnt] = pAv; //******this one

            sumOfArray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumOfArray / 4, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 
           

            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > 3) //we have filled the array
            {
           
                OCdbm.Text = averageOfarray.ToString();  //****this one
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

            if (OCavgsCnt == 3)//**** this one   this is 0,1,2,3 ie 4 slots in the array
                OCavgsCnt = -1;  //start again   **** this one

            OCavgsCnt += 1;  //****this one
            cd.pOCdbm = pAv.ToString();
            cd.pOCnumber = counter.ToString();
        }//end of the av

        int AFavgsCnt = 0;  //****this one
        public void runningAFContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)  //***this one
        {
            double sumOfArray = 0;
            double averageOfarray = 0;

            AFdbmCount.Text = counter.ToString(); //***this is the number of stations decoded   ***this one
            arr[AFavgsCnt] = pAv; //*** this one

            sumOfArray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumOfArray / 4, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 


            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > 3) //we have filled the array
            {

                AFdbm.Text = averageOfarray.ToString();  //****this one
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

            if (AFavgsCnt == 3)//**** this one   this is 0,1,2,3 ie 4 slots in the array
                AFavgsCnt = -1;  //start again   **** this one

            AFavgsCnt += 1;  //****this one
            cd.pAFdbm = pAv.ToString();
            cd.pAFnumber = counter.ToString();
        }//end of the av

        int SAavgsCnt = 0;  //****this one
        public void runningSAContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)  //***this one
        {
            double sumOfArray = 0;
            double averageOfarray = 0;

            SAdbmCount.Text = counter.ToString(); //***this is the number of stations decoded   ***this one
            arr[SAavgsCnt] = pAv; //*** this one

            sumOfArray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumOfArray / 4, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 


            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > 3) //we have filled the array
            {

                SAdbm.Text = averageOfarray.ToString();  //****this one
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

            if (SAavgsCnt == 3)//**** this one   this is 0,1,2,3 ie 4 slots in the array
                SAavgsCnt = -1;  //start again   **** this one

            SAavgsCnt += 1;  //****this one
            cd.pSAdbm = pAv.ToString();
            cd.pSAnumber = counter.ToString();
        }//end 









    }//end class
}//end
