﻿using System;
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

        string EUlast, NAlast, SAlast, OClast, JAlast, FAlast, AFlast = "";


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
            int avgSet = 8;
            int avgSetm1 = avgSet - 1;

            // int counterFA = 0;

            FAdbmCount.Text = counterFA.ToString(); //this is the number of stations decoded

            arr[FAavgsCnt] = pAv;

            sumFAarray = arr.Aggregate((total, next) => total + next);
            averageFAarray = Math.Round(sumFAarray / avgSet, 1);
           // rTotal.Text = sumFAarray.ToString(); //sums the array of whatever is in it..uses LINQ

            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > avgSetm1) //we have filled the array
            {
               // rAverage.Text = averageFAarray.ToString();
                FAdbm.Text = averageFAarray.ToString();  //this is the current period average.  We should also see it down in the array represerntation
                setBarColour(averageFAarray, barname, contDbm);  //this is the average
            }
          //  arraycounterFa.Text = FAavgsCnt.ToString();  //show the current count value 0,1,2,3

            if (FAavgsCnt == avgSetm1)
            {
                FAavgsCnt = -1;  //start again
            }

            FAavgsCnt += 1;

            //thats the end lets store something
            if (pAv == -30)
            {
                cd.pFAdbm = FAlast;  //this should be the last good reading
            }
            else
            {
             cd.pFAdbm = pAv.ToString();
            }
            FAlast = pAv.ToString();  //store the last pAv so we can use it if there is no decode for that continent
            cd.pFAnumber = counterFA.ToString();

        }

//****************************************************************************
        int JAavgsCnt = 0;
        public void runningJAContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)
        {
            double sumFAarray = 0;
            double averageOfarray = 0;
            int avgSet = 8;
            int avgSetm1 = avgSet - 1;


            JAdbmCount.Text = counter.ToString(); //this is the number of stations decoded
            arr[JAavgsCnt] = pAv; //*********

            sumFAarray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumFAarray / avgSet, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 
 
            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > avgSetm1) //we have filled the array
            {
               // rAverage.Text = averageOfarray.ToString();
                JAdbm.Text = averageOfarray.ToString();  //this is the current period average.  We should also see it down in the array represerntation
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }
 
            if (JAavgsCnt == avgSetm1)//this is 0,1,2,3 ie 4 slots in the array
                   JAavgsCnt = -1;  //start again
            
            JAavgsCnt += 1;
            cd.pJAnumber = counter.ToString();

            //thats the end lets store something
            if (pAv == -30)
            {
                cd.pJAdbm = JAlast;  //this should be the last good reading
            }
            else
            {
                cd.pJAdbm = pAv.ToString();
            }
                JAlast = pAv.ToString();  //store the last pAv so we can use it if there is no decode for that continent
            cd.pJAnumber = counter.ToString();


        }//end of the averaging
         //******************************************************************************
        int EUavgsCnt = 0;
        public void runningEUContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)
        {
            double sumOfArray = 0;
            double averageOfarray = 0;
            int avgSet = 8;
            int avgSetm1 = avgSet - 1;

            EUdbmCount.Text = counter.ToString(); //this is the number of stations decoded
            arr[EUavgsCnt] = pAv;  //**********

            sumOfArray = arr.Aggregate((total, next) => total + next);
/* 4*/            averageOfarray = Math.Round(sumOfArray / avgSet, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 
           // rTotal.Text = sumOfArray.ToString(); //sums the array of whatever is in it..uses LINQ

            int countResults = arr.Count(x => x != 0);  //using linq again
/* 3 */            if (countResults > avgSetm1) //we have filled the array
            {
              //  rAverage.Text = averageOfarray.ToString();
                EUdbm.Text = averageOfarray.ToString();  //this is the current period average.  We should also see it down in the array represerntation
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

/*3 */            if (EUavgsCnt == avgSetm1)//this is 0,1,2,3 ie 4 slots in the array
                EUavgsCnt = -1;  //start again

            EUavgsCnt += 1;
            cd.pEUnumber = counter.ToString();

            //thats the end lets store something
            if (pAv == -30)
            {
                cd.pEUdbm = EUlast;  //this should be the last good reading
            }
            else
            {
                cd.pEUdbm = pAv.ToString();
            }
                EUlast = pAv.ToString();  //store the last pAv so we can use it if there is no decode for that continent
            cd.pEUnumber = counter.ToString();



        }//end of the averaging
         //*****************************************************************************
        int NAavgsCnt = 0;  //****this one
        public void runningNAContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)  //***this one
        {
            double sumOfArray = 0;
            double averageOfarray = 0;
            int avgSet = 8;
            int avgSetm1 = avgSet - 1;


            NAdbmCount.Text = counter.ToString(); //this is the number of stations decoded   ***this one
            arr[NAavgsCnt] = pAv; //*********

            sumOfArray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumOfArray / avgSet, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 
           
            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > avgSetm1) //we have filled the array
            {
                NAdbm.Text = averageOfarray.ToString();  //****this one
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

            if (NAavgsCnt == avgSetm1)//this is 0,1,2,3 ie 4 slots in the array
                NAavgsCnt = -1;  //start again   **** this one

            NAavgsCnt += 1;  //****this one
            NAlast = pAv.ToString();

            if (pAv == -30)
            {
                //NAlast = "";
                cd.pNAdbm = NAlast;  //this should be the last good reading
            }
            else
            {
                cd.pNAdbm = pAv.ToString();
            }
                NAlast = pAv.ToString();  //store the last pAv so we can use it if there is no decode for that continent
            cd.pNAnumber = counter.ToString();

        }//end of the av

//********************************************************************************
        int OCavgsCnt = 0;  //****this one
        public void runningOCContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)  //***this one
        {
            double sumOfArray = 0;
            double averageOfarray = 0;
            int avgSet = 8;
            int avgSetm1 = avgSet - 1;


            OCdbmCount.Text = counter.ToString(); //***this is the number of stations decoded   ***this one
            arr[OCavgsCnt] = pAv; //******this one

            sumOfArray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumOfArray / avgSet, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 
           

            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > avgSetm1) //we have filled the array
            {
           
                OCdbm.Text = averageOfarray.ToString();  //****this one
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

            if (OCavgsCnt == avgSetm1)//**** this one   this is 0,1,2,3 ie 4 slots in the array
                OCavgsCnt = -1;  //start again   **** this one

            OCavgsCnt += 1;  //****this one
            OClast = pAv.ToString();

            if (pAv == -30)
            {
                //OClast = "";
                cd.pOCdbm = OClast;  //this should be the last good reading
            }
            else
            {
                cd.pOCdbm = pAv.ToString();
            }
  /*****/   OClast = pAv.ToString();  //store the last pAv so we can use it if there is no decode for that continent
            cd.pOCnumber = counter.ToString();
        }//end of the av
//********************************************************************
        int AFavgsCnt = 0;  //****this one
        public void runningAFContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)  //***this one
        {
            double sumOfArray = 0;
            double averageOfarray = 0;
            int avgSet = 8;
            int avgSetm1 = avgSet - 1;


            AFdbmCount.Text = counter.ToString(); //***this is the number of stations decoded   ***this one
            arr[AFavgsCnt] = pAv; //*** this one

            sumOfArray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumOfArray / avgSet, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 


            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > avgSetm1) //we have filled the array
            {

                AFdbm.Text = averageOfarray.ToString();  //****this one
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

            if (AFavgsCnt == avgSetm1)//**** this one   this is 0,1,2,3 ie 4 slots in the array
                AFavgsCnt = -1;  //start again   **** this one

            AFavgsCnt += 1;  //****this one
            AFlast = pAv.ToString();

            if (pAv == -30)
            {
                //AFlast = "";
                cd.pAFdbm = AFlast;  //this should be the last good reading

            }
            else
            {
                cd.pAFdbm = pAv.ToString();
                AFlast = pAv.ToString();  //store the last pAv so we can use it if there is no decode for that continent
            }
            cd.pAFnumber = counter.ToString();

        }//end of the av

        int SAavgsCnt = 0;  //****this one
        public void runningSAContinentalAverage(double pAv, ProgressBar barname, TextBlock contDbm, int counter, double[] arr)  //***this one
        {
            double sumOfArray = 0;
            double averageOfarray = 0;
            int avgSet = 8;
            int avgSetm1 = avgSet - 1;


            SAdbmCount.Text = counter.ToString(); //***this is the number of stations decoded   ***this one
            arr[SAavgsCnt] = pAv; //*** this one

            sumOfArray = arr.Aggregate((total, next) => total + next);
            averageOfarray = Math.Round(sumOfArray / avgSet, 1);  //this number 4 is how many slots to fill for the averaging  4 is 1 minute in FT8 


            int countResults = arr.Count(x => x != 0);  //using linq again
            if (countResults > avgSetm1) //we have filled the array
            {

                SAdbm.Text = averageOfarray.ToString();  //****this one
                setBarColour(averageOfarray, barname, contDbm);  //this is the average
            }

            if (SAavgsCnt == avgSetm1)//**** this one   this is 0,1,2,3 ie 4 slots in the array
                SAavgsCnt = -1;  //start again   **** this one

            SAavgsCnt += 1;  //****this one
            SAlast = pAv.ToString();

            if (pAv == -30)
            {
                //SAlast = "";
                cd.pSAdbm = SAlast;  //this should be the last good reading
            }
            else
            {
                cd.pSAdbm = pAv.ToString();
                SAlast = pAv.ToString();  //store the last pAv so we can use it if there is no decode for that continent
            }
            cd.pSAnumber = counter.ToString();
        }//end 









    }//end class
}//end
