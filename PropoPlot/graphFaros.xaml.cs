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
using System.Drawing;

namespace PropoPlot
{
    /// <summary>
    /// Interaction logic for graphFaros.xaml
    /// </summary>
    public partial class graphFaros : Window
    {
        const int arrSize = 5000;

        double[] dataX = new double[arrSize];
       
        DateTime[] DTdates = new DateTime[arrSize];
        double[] Dubdates = new double[arrSize];



        double[] farosEU = new double[96];
        double[] farosJA = new double[96];
        double[] farosNA = new double[96];
        double[] farosSA = new double[96];
        double[] farosAF = new double[96];
        double[] farosOC = new double[96];
        double[] farosFA = new double[96];

        double[] farosEUday15 = new double[96];  //these are for 15 minute looks at the data
        double[] farosJAday15 = new double[96];
        double[] farosOCday15 = new double[96];
        double[] farosNAday15 = new double[96];

        double[] farosEUDay60 = new double[24];
        double[] farosJADay60 = new double[24];
        double[] farosOCDay60 = new double[24];
        double[] farosNADay60 = new double[24];
    
        List<string> thlist;

        int currentArrSize = 0;
    

        public graphFaros(List<string> alist)
        {
            InitializeComponent();
            thlist = alist;  //this is where all of the data is

            PrepareArrays(); //this uses the sized arrays (done at declaration)
            PlotArrays();
        }



        public void PrepareArrays()
        {

            // here is a look up list


            string[] wrdmsg = { };

            string time = "";
            int count = 0;

            //these are the time spans these are 15minutes

            TimeSpan t00_1 = new TimeSpan(00, 0, 0);
            TimeSpan t00_2 = new TimeSpan(00, 15, 0);
            TimeSpan t00_3 = new TimeSpan(00, 30, 0);
            TimeSpan t00_4 = new TimeSpan(00, 45, 0);
            TimeSpan t01_1 = new TimeSpan(01, 0, 0);
            TimeSpan t01_2 = new TimeSpan(01, 15, 0);
            TimeSpan t01_3 = new TimeSpan(01, 30, 0);
            TimeSpan t01_4 = new TimeSpan(01, 45, 0);
            TimeSpan t02_1 = new TimeSpan(02, 0, 0);
            TimeSpan t02_2 = new TimeSpan(02, 15, 0);
            TimeSpan t02_3 = new TimeSpan(02, 30, 0);
            TimeSpan t02_4 = new TimeSpan(02, 45, 0);
            TimeSpan t03_1 = new TimeSpan(03, 0, 0);
            TimeSpan t03_2 = new TimeSpan(03, 15, 0);
            TimeSpan t03_3 = new TimeSpan(03, 30, 0);
            TimeSpan t03_4 = new TimeSpan(03, 45, 0);
            TimeSpan t04_1 = new TimeSpan(04, 0, 0);
            TimeSpan t04_2 = new TimeSpan(04, 15, 0);
            TimeSpan t04_3 = new TimeSpan(04, 30, 0);
            TimeSpan t04_4 = new TimeSpan(04, 45, 0);


            TimeSpan t05_1 = new TimeSpan(05, 0, 0);
            TimeSpan t05_2 = new TimeSpan(05, 15, 0);
            TimeSpan t05_3 = new TimeSpan(05, 30, 0);
            TimeSpan t05_4 = new TimeSpan(05, 45, 0);
            TimeSpan t06_1 = new TimeSpan(06, 0, 0);
            TimeSpan t06_2 = new TimeSpan(06, 15, 0);
            TimeSpan t06_3 = new TimeSpan(06, 30, 0);
            TimeSpan t06_4 = new TimeSpan(06, 45, 0);
            TimeSpan t07_1 = new TimeSpan(07, 0, 0);
            TimeSpan t07_2 = new TimeSpan(07, 15, 0);
            TimeSpan t07_3 = new TimeSpan(07, 30, 0);
            TimeSpan t07_4 = new TimeSpan(07, 45, 0);
            TimeSpan t08_1 = new TimeSpan(08, 0, 0);
            TimeSpan t08_2 = new TimeSpan(08, 15, 0);
            TimeSpan t08_3 = new TimeSpan(08, 30, 0);
            TimeSpan t08_4 = new TimeSpan(08, 45, 0);
            TimeSpan t09_1 = new TimeSpan(09, 0, 0);
            TimeSpan t09_2 = new TimeSpan(09, 15, 0);
            TimeSpan t09_3 = new TimeSpan(09, 30, 0);
            TimeSpan t09_4 = new TimeSpan(09, 45, 0);
            TimeSpan t10_1 = new TimeSpan(10, 0, 0);
            TimeSpan t10_2 = new TimeSpan(10, 15, 0);
            TimeSpan t10_3 = new TimeSpan(10, 30, 0);
            TimeSpan t10_4 = new TimeSpan(10, 45, 0);
            TimeSpan t11_1 = new TimeSpan(11, 0, 0);
            TimeSpan t11_2 = new TimeSpan(11, 15, 0);
            TimeSpan t11_3 = new TimeSpan(11, 30, 0);
            TimeSpan t11_4 = new TimeSpan(11, 45, 0);
            TimeSpan t12_1 = new TimeSpan(12, 0, 0);
            TimeSpan t12_2 = new TimeSpan(12, 15, 0);
            TimeSpan t12_3 = new TimeSpan(12, 30, 0);
            TimeSpan t12_4 = new TimeSpan(12, 45, 0);
            TimeSpan t13_1 = new TimeSpan(13, 0, 0);
            TimeSpan t13_2 = new TimeSpan(13, 15, 0);
            TimeSpan t13_3 = new TimeSpan(13, 30, 0);
            TimeSpan t13_4 = new TimeSpan(13, 45, 0);



                TimeSpan t14_1 = new TimeSpan(14, 0, 0);
                TimeSpan  t14_2 = new TimeSpan(14, 15, 0);
                TimeSpan  t14_3 = new TimeSpan(14, 30, 0);
                TimeSpan  t14_4 = new TimeSpan(14, 45, 0);
                TimeSpan  t15_1 = new TimeSpan(15, 0, 0);
                TimeSpan  t15_2 = new TimeSpan(15, 15, 0);
                TimeSpan  t15_3 = new TimeSpan(15, 30, 0);
                TimeSpan  t15_4 = new TimeSpan(15, 45, 0);
                TimeSpan t16_1 = new TimeSpan(16, 0, 0);
                TimeSpan t16_2 = new TimeSpan(16, 15, 0);
                TimeSpan t16_3 = new TimeSpan(16, 30, 0);
                TimeSpan t16_4 = new TimeSpan(16, 45, 0);
                TimeSpan t17_1 = new TimeSpan(17, 0, 0);
                TimeSpan t17_2 = new TimeSpan(17, 15, 0);
                TimeSpan t17_3 = new TimeSpan(17, 30, 0);
                TimeSpan t17_4 = new TimeSpan(17, 45, 0);
                TimeSpan t18_1 = new TimeSpan(18, 0, 0);
           
            const int arrSize = 10000;

                 double[] tpEU15 = new double[arrSize];  // = 4 per minute * 15 minute blocks
                 double[] tpJA15 = new double[arrSize];
                double[] tpOC15 = new double[arrSize];
                double[] tpNA15 = new double[arrSize];

            int index = 0;

                int tpCount = 0;
            foreach (var item in thlist)
            {
                wrdmsg = item.Split(',');

                //dataX[count] = count; //the X values

                //DTdates[count] = DateTime.Parse(wrdmsg[1]);
                //Dubdates[count] = DTdates[count].ToOADate();


                TimeSpan timeData = TimeSpan.Parse(wrdmsg[1]);
                if (timeData > t00_1 && timeData < t00_2) // 15minute interval
                {
                    index = 0;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t00_2 && timeData < t00_3) // 15minute interval
                {
                    index = 1;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe

                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe                    tpCount += 1;
                }
                if (timeData > t00_3 && timeData < t00_4) // 15minute interval
                {
                    index = 2;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t00_4 && timeData < t01_1) // 15minute interval
                {
                    index = 3;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t01_1 && timeData < t01_2) // 15minute interval
                {
                    index = 4;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t01_2 && timeData < t01_3) // 15minute interval
                {
                    index = 5;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t01_3 && timeData < t01_4) // 15minute interval
                {
                    index = 6;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t01_4 && timeData < t02_1) // 15minute interval
                {
                    index = 7;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t02_1 && timeData < t02_2) // 15minute interval
                {
                    index = 8;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t02_2 && timeData < t02_3) // 15minute interval
                {
                    index = 9;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t02_3 && timeData < t02_4) // 15minute interval
                {
                    index = 10;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }


                if (timeData > t02_4 && timeData < t03_1) // 15minute interval
                {
                    index = 11;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;                }
//-----------------------------------------
                if (timeData > t03_1 && timeData < t03_2) // 15minute interval
                {
                    index = 12;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;}
                if (timeData > t03_2 && timeData < t03_3) // 15minute interval
                {
                    index = 13;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;}                
                if (timeData > t03_3 && timeData < t03_4) // 15minute interval
                {
                    index = 14;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t03_4 && timeData < t04_1) // 15minute interval
                {
                    index = 15;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
//-------------------------------
//-----------------------------------------
                if (timeData > t04_1 && timeData < t04_2) // 15minute interval
                {
                    index = 16;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;}
                if (timeData > t04_2 && timeData < t04_3) // 15minute interval
                {
                    index = 17;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;}                
                if (timeData > t04_3 && timeData < t04_4) // 15minute interval
                {
                    index = 18;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t04_4 && timeData < t05_1) // 15minute interval
                {
                    index = 19;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
//-------------------------------




                if (timeData > t05_1 && timeData < t05_2) // 15minute interval
                {
                    index = 20;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t05_2 && timeData < t05_3) // 15minute interval
                {
                    index = 21;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t05_3 && timeData < t05_4) // 15minute interval
                {
                    index = 22;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t05_4 && timeData < t06_1) // 15minute interval
                {
                    index = 23;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t06_1 && timeData < t06_2) // 15minute interval
                {
                    index = 24;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t06_2 && timeData < t06_3) // 15minute interval
                {
                    index = 25;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t06_3 && timeData < t06_4) // 15minute interval
                {
                    index = 26;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t06_4 && timeData < t07_1) // 15minute interval
                {
                    index = 27;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t07_1 && timeData < t07_2) // 15minute interval
                {
                    index = 28;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t07_2 && timeData < t07_3) // 15minute interval
                {
                    index = 29;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t07_3 && timeData < t07_4) // 15minute interval
                {
                    index = 30;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t07_4 && timeData < t08_1) // 15minute interval
                {
                    index = 31;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t08_1 && timeData < t08_2) // 15minute interval
                {
                    index = 32;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t08_2 && timeData < t08_3) // 15minute interval
                {
                    index = 33;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t08_3 && timeData < t08_4) // 15minute interval
                {
                    index = 34;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t08_4 && timeData < t09_1) // 15minute interval
                {
                    index = 35;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t09_1 && timeData < t09_2) // 15minute interval
                {
                    index = 36;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t09_2 && timeData < t09_3) // 15minute interval
                {
                    index = 37;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t09_3 && timeData < t09_4) // 15minute interval
                {
                    index = 38;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t09_4 && timeData < t10_1) // 15minute interval
                {
                    index = 39;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t10_1 && timeData < t10_2) // 15minute interval
                {
                    index = 40;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t10_2 && timeData < t10_3) // 15minute interval
                {
                    index = 41;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t10_3 && timeData < t10_4) // 15minute interval
                {
                    index = 42;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }


                if (timeData > t10_4 && timeData < t11_1) // 15minute interval
                {
                    index = 43;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t11_1 && timeData < t11_2) // 15minute interval
                {
                    index = 44;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t11_2 && timeData < t11_3) // 15minute interval
                {
                    index = 45;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t11_3 && timeData < t11_4) // 15minute interval
                {
                    index = 46;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t11_4 && timeData < t12_1) // 15minute interval
                {
                    index = 47;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t12_1 && timeData < t12_2) // 15minute interval
                {
                    index = 48;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t12_2 && timeData < t12_3) // 15minute interval
                {
                    index = 49;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t12_3 && timeData < t12_4) // 15minute interval
                {
                    index = 50;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t12_4 && timeData < t13_1) // 15minute interval
                {
                    index = 51;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t13_1 && timeData < t13_2) // 15minute interval
                {
                    index = 52;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t13_2 && timeData < t13_3) // 15minute interval
                {
                    index = 53;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t13_3 && timeData < t13_4) // 15minute interval
                {
                    index = 54;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t13_4 && timeData < t14_1) // 15minute interval
                {
                    index = 55;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }





                if (timeData > t14_1 && timeData < t14_2) // 15minute interval
                {
                    index = 56;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }


                if (timeData > t14_2 && timeData < t14_3) // 15minute interval
                {
                    index = 57;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }




                if (timeData > t14_3 && timeData < t14_4) // 15minute interval
                {
                    index = 58;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }



                if (timeData > t14_4 && timeData < t15_1) // 15minute interval
                {
                    index = 59;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t15_1 && timeData < t15_2) // 15minute interval
                {
                    index = 60;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t15_2 && timeData < t15_3) // 15minute interval
                {
                    index = 61;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t15_3 && timeData < t15_4) // 15minute interval
                {
                    index = 62;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t15_4 && timeData < t16_1) // 15minute interval
                {
                    index = 63;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t16_1 && timeData < t16_2) // 15minute interval
                {
                    index = 64;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t16_2 && timeData < t16_3) // 15minute interval
                {
                    index = 65;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t16_3 && timeData < t16_4) // 15minute interval
                {
                    index = 66;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }

                if (timeData > t16_4 && timeData < t17_1) // 15minute interval
                {
                    index = 67;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t17_1 && timeData < t17_2) // 15minute interval
                {
                    index = 68;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t17_2 && timeData < t17_3) // 15minute interval
                {
                    index = 69;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t17_3 && timeData < t17_4) // 15minute interval
                {
                    index = 70;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }
                if (timeData > t17_4 && timeData < t18_1) // 15minute interval
                {
                    index = 71;  //this is a calculated value (on paper it is the hour * 4 + position in the hour 14_1 =56.  14_2 = 57 etc
                                 // need to send this off to an array holding the numbers to average them
                    double.TryParse(wrdmsg[2], out tpEU15[tpCount]); //Europe
                    double.TryParse(wrdmsg[5], out tpJA15[tpCount]); //Europe
                    double.TryParse(wrdmsg[11], out tpOC15[tpCount]); //Europe
                    double.TryParse(wrdmsg[8], out tpNA15[tpCount]); //Europe
                    tpCount += 1;
                }


                //here is where we average the period
                double farosEU15 = averageTimeSlot(tpEU15, tpCount);
                double farosJA15 = averageTimeSlot(tpJA15, tpCount);
                double farosOC15 = averageTimeSlot(tpOC15, tpCount);
                double farosNA15 = averageTimeSlot(tpNA15, tpCount);
                //now write that to the array that holds the 15minute periods
                farosEUday15[index] = farosEU15;
                farosJAday15[index] = farosJA15;
                farosOCday15[index] = farosOC15;
                farosNAday15[index] = farosNA15;

            }


        }

        double[] xs =  {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95};
        //int[] xs = Enumerable.Range(0, 96).ToArray();


        
        double[] ys1 = {0,3,4,2,3,5,6,7,2 };
        double[] ys2 = {-1,0,1,1,3,4,2,6,2 };

        private void PlotArrays()
        {

            //gonna try grouped bars for this


            double[] eu5 = new double[5];
            double[] ja5 = new double[5];
            double[] oc5 = new double[5];

            for (int i = 0; i < 5; i++)
                eu5[i] = farosEUday15[i + 45];

            for (int i = 0; i < 5; i++)
                ja5[i] = farosJAday15[i + 45];

            for (int i = 0; i < 5; i++)
                oc5[i] = farosOCday15[i + 45];


            int groupCount = 5;
            string[] groupnames =  {"g1","g2","g3","g4","g5" };
            string[] seriesNames = { "Series 1", "Series 2", "Series 3" };
            double[][] valuesBySeries = { eu5, ja5, oc5 };

            var pl1 = graphFarosPlot.Plot.AddBarGroups(groupnames, seriesNames,valuesBySeries,null);
           // var EUbars = graphFarosPlot.Plot.AddBar(farosEUday15);
           //var OCbars = graphFarosPlot.Plot.AddBar(farosOCday15);

            //double[] EU2 = new double[farosEUday15.Length];
            //for (int i = 0; i < farosEUday15.Length; i++)
            //    EU2[i] = farosJAday15[i] + farosEUday15[i];

            //double[] EU3 = new double[farosNAday15.Length];
            //for (int i = 0; i < farosNAday15.Length; i++)
            //    EU3[i] = EU2[i] + farosNAday15[i];





            //       var barOC =    graphFarosPlot.Plot.AddBar(EU3);
            //var barJA =    graphFarosPlot.Plot.AddBar(EU2);
            //var barEU =   graphFarosPlot.Plot.AddBar(farosEUday15);


            // //  barOC.FillColor = System.Drawing.Color.Red;  //ColorTranslator.FromHtml(Properties.Settings.Default.OCAvgColor);
            //   barEU.FillColor = System.Drawing.Color.Blue;
            //   barEU.BorderColor = System.Drawing.Color.Blue;
            //   barEU.FillColorHatch = System.Drawing.Color.Blue;
            //   barEU.Label = "EU";

            //   barJA.FillColor = System.Drawing.Color.Green;
            //   barJA.BorderColor = System.Drawing.Color.Green;
            //   barJA.Label = "JA";

            //   //barOC.BorderColor = System.Drawing.Color.Cyan;
            //   //barOC.Label = "OC";


            graphFarosPlot.Plot.Legend();

            graphFarosPlot.Render();
            //graphFarosPlot.Plot.PlotBar(xs, farosEUday15);
            //graphFarosPlot.Plot.PlotBar(xs, farosJAday15);
            //graphFarosPlot.Plot.PlotBar(xs, farosOCday15);
            //graphFarosPlot.Plot.PlotBar(xs, farosNAday15);




        }


        private double averageTimeSlot(double[] arr, int tpCount)
        {
            double sumFAarray = arr.Aggregate((total, next) => total + next);
            double averageFAarray = Math.Round(sumFAarray / tpCount, 1);  // 4 slots in the arra?

            return averageFAarray;


        }




        /// <summary>
        /// Remove the -30's from the array.  Its crap data
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private double[] smoothArray(double[] arr)
        {


            for (int i = 0; i < arr.Length - 2; i++)
            {
                double firstVal = arr[i];  //get a reading
                double nextVal = arr[i + 1];  //get the next reading
                double nextVal1 = arr[i + 2]; //get the one after
                if (nextVal == -30 && nextVal1 == -30)  //two in a row probably means we have real -30 data, niot just one person on the band sending CQ
                    arr[i + 1] = -30;  //do nothing
                else if (nextVal == -30 && nextVal1 != -30)
                    arr[i + 1] = firstVal;
            }
            return arr;
        }





















        private void chkLiveUpdate_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkLiveUpdate_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
