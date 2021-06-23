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
using Serilog;

namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphQSOCount.xaml
    /// </summary>
    public partial class graphQSOCount : Window
    {
        List<string> thlist;
       
        const int HrArrSize = 24;
        const int QtrArrSize = 96;

        double[] EUhr = new double[HrArrSize];
        double[] JAhr = new double[HrArrSize];
        double[] NAhr = new double[HrArrSize];
        double[] SAhr = new double[HrArrSize];
        double[] OChr = new double[HrArrSize];
        double[] AFhr = new double[HrArrSize];
        double[] FAhr = new double[HrArrSize];
        double[] EUqtr = new double[QtrArrSize];
        double[] JAqtr = new double[QtrArrSize];
        double[] NAqtr = new double[QtrArrSize];
        double[] SAqtr = new double[QtrArrSize];
        double[] OCqtr = new double[QtrArrSize];
        double[] AFqtr = new double[QtrArrSize];
        double[] FAqtr = new double[QtrArrSize];

        double[] ALLSummedHr = new double[HrArrSize];
        double[] ALLStackedHr = new double[HrArrSize];

        double[] EUStackedHr = new double[HrArrSize];
        double[] JAStackedHr = new double[HrArrSize];
        double[] NAStackedHr = new double[HrArrSize];
        double[] SAStackedHr = new double[HrArrSize];
        double[] OCStackedHr = new double[HrArrSize];
        double[] AFStackedHr = new double[HrArrSize];
        double[] FAStackedHr = new double[HrArrSize];

        double[] EUArrayToPlot = new double[HrArrSize];
        double[] JAArrayToPlot = new double[HrArrSize];
        double[] NAArrayToPlot = new double[HrArrSize];
        double[] SAArrayToPlot = new double[HrArrSize];
        double[] OCArrayToPlot = new double[HrArrSize];
        double[] AFArrayToPlot = new double[HrArrSize];
        double[] FAArrayToPlot = new double[HrArrSize];


        int EUcnt = 0;
        int JAcnt = 0;
        int NAcnt = 0;
        int SAcnt = 0;
        int AFcnt = 0;
        int OCcnt = 0;
        int FAcnt = 0;



        public graphQSOCount(List<string> athlist)
        {
            InitializeComponent();
            thlist = athlist;

            PrepareArrays();
            prepareArraysForPlotting();
            plotArrays();

        }


        private void PrepareArrays()
        {

            //we want to get our data, average it over 1 hou or 15 minute intervals and show a column plot against UTC time on the X axis
            //these are hourly timeslots
            TimeSpan t00 = new TimeSpan(00, 0, 0);
            TimeSpan t01 = new TimeSpan(01, 0, 0);
            TimeSpan t02 = new TimeSpan(02, 0, 0);
            TimeSpan t03 = new TimeSpan(03, 0, 0);
            TimeSpan t04 = new TimeSpan(04, 0, 0);
            TimeSpan t05 = new TimeSpan(05, 0, 0);
            TimeSpan t06 = new TimeSpan(06, 0, 0);
            TimeSpan t07 = new TimeSpan(07, 0, 0);
            TimeSpan t08 = new TimeSpan(08, 0, 0);
            TimeSpan t09 = new TimeSpan(09, 0, 0);
            TimeSpan t10 = new TimeSpan(10, 0, 0);
            TimeSpan t11 = new TimeSpan(11, 0, 0);
            TimeSpan t12 = new TimeSpan(12, 0, 0);
            TimeSpan t13 = new TimeSpan(13, 0, 0);
            TimeSpan t14 = new TimeSpan(14, 0, 0);
            TimeSpan t15 = new TimeSpan(15, 0, 0);
            TimeSpan t16 = new TimeSpan(16, 0, 0);
            TimeSpan t17 = new TimeSpan(17, 0, 0);
            TimeSpan t18 = new TimeSpan(18, 0, 0);
            TimeSpan t19 = new TimeSpan(19, 0, 0);
            TimeSpan t20 = new TimeSpan(20, 0, 0);
            TimeSpan t21 = new TimeSpan(21, 0, 0);
            TimeSpan t22 = new TimeSpan(22, 0, 0);
            TimeSpan t23 = new TimeSpan(23, 0, 0);

            foreach (var item in thlist)
            {
            string[] wrdmsg = item.Split(',');

                // new we have wrdmsg[1] is the hour, [4] is the count, but never the first N records

                TimeSpan timeData = TimeSpan.Parse(wrdmsg[1]);

                if (timeData > t00 && timeData < t01) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[0] +=  int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[0] +=  int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[0] +=  int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[0] +=  int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[0] +=  int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[0] +=  int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[0] +=  int.Parse(wrdmsg[22]);
                    stack100(00);
                }
             else   if (timeData > t01 && timeData < t02) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[1] +=  int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[1] +=  int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[1] +=  int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[1] +=  int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[1] +=  int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[1] +=  int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[1] +=  int.Parse(wrdmsg[22]);
                    stack100(1);
                }             
                else   if (timeData > t02 && timeData < t03) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[2] +=  int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[2] +=  int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[2] +=  int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[2] +=  int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[2] +=  int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[2] +=  int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[2] +=  int.Parse(wrdmsg[22]);
                    stack100(2);
                }
                else if (timeData > t03 && timeData < t04) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[3] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[3] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[3] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[3] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[3] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[3] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[3] += int.Parse(wrdmsg[22]);
                    stack100(3);
                }
                else if (timeData > t04 && timeData < t05) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[4] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[4] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[4] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[4] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[4] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[4] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[4] += int.Parse(wrdmsg[22]);
                    stack100(4);
                }

                else if (timeData > t05 && timeData < t06) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4]  != "") EUhr[5] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7]  != "") JAhr[5] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[5] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[5] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[5] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[5] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[5] += int.Parse(wrdmsg[22]);
                    stack100(5);
                }
                else if (timeData > t06 && timeData < t07) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "") EUhr[6] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "") JAhr[6] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[6] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[6] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[6] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[6] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[6] += int.Parse(wrdmsg[22]);
                    stack100(6);
                }

                else if (timeData > t07 && timeData < t08) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[7] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[7] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[7] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[7] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[7] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[7] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[7] += int.Parse(wrdmsg[22]);
                    stack100(7);
                }
                else if (timeData > t08 && timeData < t09) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[8] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[8] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[8] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[8] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[8] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[8] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[8] += int.Parse(wrdmsg[22]);
                    stack100(8);
                }
                else if (timeData > t09 && timeData < t10) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[9] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[9] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[9] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[9] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[9] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[9] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[9] += int.Parse(wrdmsg[22]);
                    stack100(9);
                }
                else if (timeData > t10 && timeData < t11) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[10] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[10] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[10] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[10] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[10] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[10] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[10] += int.Parse(wrdmsg[22]);
                    stack100(10);
                }
                else if (timeData > t11 && timeData < t12) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[11] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[11] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[11] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[11] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[11] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[11] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[11] += int.Parse(wrdmsg[22]);
                    stack100(11);
                }
                else if (timeData > t12 && timeData < t13) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[12] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[12] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[12] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[12] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[12] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[12] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[12] += int.Parse(wrdmsg[22]);
                    stack100(12);
                }
                else if (timeData > t13 && timeData < t14) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[13] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[13] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[13] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[13] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[13] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[13] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[13] += int.Parse(wrdmsg[22]);
                    stack100(13);
                }
                else if (timeData > t14 && timeData < t15) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[14] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[14] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[14] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[14] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[14] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[14] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[14] += int.Parse(wrdmsg[22]);
                    stack100(14);
                }
                else if (timeData > t15 && timeData < t16) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[15] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[15] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[15] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[15] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[15] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[15] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[15] += int.Parse(wrdmsg[22]);
                    stack100(15);
                }
                else if (timeData > t16 && timeData < t17) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[16] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[16] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[16] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[16] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[16] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[16] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[16] += int.Parse(wrdmsg[22]);
                    stack100(16);
                }
                else if (timeData > t17 && timeData < t18) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[17] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[17] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[17] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[17] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[17] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[17] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[17] += int.Parse(wrdmsg[22]);
                    stack100(17);
                }
                else if (timeData > t18 && timeData < t19) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[18] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[18] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[18] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[18] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[18] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[18] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[18] += int.Parse(wrdmsg[22]);
                    stack100(18);
                }
                else if (timeData > t19 && timeData < t20) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[19] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[19] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[19] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[19] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[19] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[19] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[19] += int.Parse(wrdmsg[22]);
                    stack100(19);
                }
                else if (timeData > t20 && timeData < t21) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[20] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[20] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[20] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[20] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[20] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[20] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[20] += int.Parse(wrdmsg[22]);
                    stack100(20);
                }
                else if (timeData > t21 && timeData < t22) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[21] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[21] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[21] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[21] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[21] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[21] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[21] += int.Parse(wrdmsg[22]);
                    stack100(21);
                }
                else if (timeData > t22 && timeData < t23) //1 hour
                {
                    //accumulate and store the data..index in the hour array would be 0
                    if (wrdmsg[4] != "")  EUhr[22] += int.Parse(wrdmsg[4]);
                    if (wrdmsg[7] != "")  JAhr[22] += int.Parse(wrdmsg[7]);
                    if (wrdmsg[10] != "") NAhr[22] += int.Parse(wrdmsg[10]);
                    if (wrdmsg[13] != "") OChr[22] += int.Parse(wrdmsg[13]);
                    if (wrdmsg[16] != "") AFhr[22] += int.Parse(wrdmsg[16]);
                    if (wrdmsg[19] != "") SAhr[22] += int.Parse(wrdmsg[19]);
                    if (wrdmsg[22] != "") FAhr[22] += int.Parse(wrdmsg[22]);
                    stack100(22);
                }

            }



        }//end


         private void stack100(int hr)
        {

            ALLSummedHr[hr] = EUhr[hr] + JAhr[hr] + NAhr[hr] + OChr[hr] + AFhr[hr] + SAhr[hr] + FAhr[hr];     //Uhr.Select(x => (int)x).Sum();              //EUhr.Sum();

            EUStackedHr[hr] = EUhr[hr] / ALLSummedHr[hr] * 100;
            JAStackedHr[hr] = JAhr[hr] / ALLSummedHr[hr] * 100;
            NAStackedHr[hr] = NAhr[hr] / ALLSummedHr[hr] * 100;
            OCStackedHr[hr] = OChr[hr] / ALLSummedHr[hr] * 100;
            AFStackedHr[hr] = AFhr[hr] / ALLSummedHr[hr] * 100;
            SAStackedHr[hr] = SAhr[hr] / ALLSummedHr[hr] * 100;
            FAStackedHr[hr] = FAhr[hr] / ALLSummedHr[hr] * 100;

          

        }

 
         private void prepareArraysForPlotting()
        {

            for (int i = 0; i < HrArrSize; i++)
            {
                EUArrayToPlot[i] = EUStackedHr[i];  //do this to keep all the array names similar, EU always on the bottom
                JAArrayToPlot[i] = EUStackedHr[i] + JAStackedHr[i];
                NAArrayToPlot[i] = EUStackedHr[i] + JAStackedHr[i] + NAStackedHr[i];
                OCArrayToPlot[i] = EUStackedHr[i] + JAStackedHr[i] + NAStackedHr[i] + OCStackedHr[i];
                AFArrayToPlot[i] = EUStackedHr[i] + JAStackedHr[i] + NAStackedHr[i] + OCStackedHr[i] + AFStackedHr[i];
                SAArrayToPlot[i] = EUStackedHr[i] + JAStackedHr[i] + NAStackedHr[i] + OCStackedHr[i] + AFStackedHr[i] + SAStackedHr[i];
                FAArrayToPlot[i] = EUStackedHr[i] + JAStackedHr[i] + NAStackedHr[i] + OCStackedHr[i] + AFStackedHr[i] + SAStackedHr[i]+FAStackedHr[i];


            }

        }


 private void plotArrays() {


            QsoCountPlotALL.Plot.Clear();

            var FAplot = QsoCountPlotALL.Plot.AddBar(FAArrayToPlot);
            var SAplot = QsoCountPlotALL.Plot.AddBar(SAArrayToPlot);
            var AFplot = QsoCountPlotALL.Plot.AddBar(AFArrayToPlot);
            var OCplot = QsoCountPlotALL.Plot.AddBar(OCArrayToPlot);
            var NAplot = QsoCountPlotALL.Plot.AddBar(NAArrayToPlot);
            var JAplot = QsoCountPlotALL.Plot.AddBar(JAArrayToPlot);
            var EUplot = QsoCountPlotALL.Plot.AddBar(EUArrayToPlot);

            EUplot.Label = "EU";
            JAplot.Label = "JA";
            NAplot.Label = "NA";
            OCplot.Label = "OC";
            AFplot.Label = "AF";
            SAplot.Label = "SA";
            FAplot.Label = "Usr";


           // BrushConverter bc = new BrushConverter();
            //cntCol1.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM1);
            //cntCol2.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM2);
            //cntCol3.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM3);
            //cntCol4.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM4);
            //cntCol5.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM5);
            //cntCol6.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM1);
            //cntCol7.Background = (System.Windows.Media.Brush)bc.ConvertFromString(Properties.Settings.Default.crDBM2);



            //FAplot.FillColor = ScottPlot.Drawing.Colormap.


               QsoCountPlotALL.Plot.Legend(location: ScottPlot.Alignment.UpperRight);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrepareArrays();
            prepareArraysForPlotting();
            plotArrays();

//            QsoCountPlotALL.Plot.Render();
        }
    }//end
}
