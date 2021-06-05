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


        const int arrSize = 25;

        double[,] dataEU = new double[arrSize, 2]; //Average
        double[,] dataJA = new double[arrSize, 2]; //Average
        double[,] dataNA = new double[arrSize, 2]; //Average
        double[,] dataSA = new double[arrSize, 2]; //Average
        double[,] dataOC = new double[arrSize, 2]; //Average
        double[,] dataAF = new double[arrSize, 2]; //Average
        double[,] dataFA = new double[arrSize, 2]; //Average

        List<string> thlist;

        public plotOfFaros(List<string> alist)
        {
            InitializeComponent();
            thlist = alist;  //this is where all of the data is



            prepareArrays();


        }


      //      int EUcnt =0, JAcnt =0 , NAcnt= 0, SAcnt = 0, OCcnt = 0, AFcnt =0 , FAcnt= 0 ;

        private void btnPlot_Click(object sender, RoutedEventArgs e)
        {
            prepareArrays();
        }

        private void prepareArrays()
        {
            try
            {

            string[] wrdmsg = { };
            int count = 0;

            TimeSpan tEU00 = new TimeSpan(00, 0, 0);
            TimeSpan tEU01 = new TimeSpan(01, 0, 0);
            TimeSpan tEU02 = new TimeSpan(02, 0, 0);
            TimeSpan tEU03 = new TimeSpan(03, 0, 0);
            TimeSpan tEU04 = new TimeSpan(04, 0, 0);
            TimeSpan tEU05 = new TimeSpan(05, 0, 0);
            TimeSpan tEU06 = new TimeSpan(06, 0, 0);
            TimeSpan tEU07 = new TimeSpan(07, 0, 0);
            TimeSpan tEU08 = new TimeSpan(08, 0, 0);
            TimeSpan tEU09 = new TimeSpan(09, 0, 0);
            TimeSpan tEU10 = new TimeSpan(10, 0, 0);
            TimeSpan tEU11 = new TimeSpan(11, 0, 0);
            TimeSpan tEU12 = new TimeSpan(12, 0, 0);
            TimeSpan tEU13 = new TimeSpan(13, 0, 0);
            TimeSpan tEU14 = new TimeSpan(14, 0, 0);
            TimeSpan tEU15 = new TimeSpan(15, 0, 0);
            TimeSpan tEU16 = new TimeSpan(16, 0, 0);
            TimeSpan tEU17 = new TimeSpan(17, 0, 0);
            TimeSpan tEU18 = new TimeSpan(18, 0, 0);
            TimeSpan tEU19 = new TimeSpan(19, 0, 0);
            TimeSpan tEU20 = new TimeSpan(20, 0, 0);
            TimeSpan tEU21 = new TimeSpan(21, 0, 0);
            TimeSpan tEU22 = new TimeSpan(22, 0, 0);
            TimeSpan tEU23 = new TimeSpan(23, 0, 0);
            TimeSpan tEU24 = new TimeSpan(24, 0, 0);
            foreach (var item in thlist.Skip(0))
            {
                if (count > 0)
                {// we are all set up to find the average of the raw data or even the raw counts
                 //divide the data into hours  - four sets per hour
                    wrdmsg = item.Split(',');

                    // double.TryParse(wrdmsg[2], out dataEUR[count]); //Europe
                    //get the hourly RAW data


                    TimeSpan timeData = TimeSpan.Parse(wrdmsg[1]);
 
                    if (timeData >= tEU00 && timeData < tEU01) // 15minute interval
                         accumIt(tEU00, tEU01, 00, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU00, JA00, NA00, OC00, AF00, SA00, FA00);
                    else if (timeData >= tEU01 && timeData < tEU02) // 15minute interval
                        accumIt(tEU01, tEU02, 01, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU01, JA01, NA01, OC01, AF01, SA01, FA01);
                    else if (timeData >= tEU02 && timeData < tEU03) // 15minute interval
                        accumIt(tEU02, tEU03, 02, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU02, JA02, NA02, OC02, AF02, SA02, FA02);
                    else if (timeData >= tEU03 && timeData < tEU04) // 15minute interval
                        accumIt(tEU03, tEU04, 03, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU03, JA03, NA03, OC03, AF03, SA03, FA03);
                    else if (timeData >= tEU04 && timeData < tEU05) // 15minute interval
                        accumIt(tEU04, tEU05, 04, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU04, JA04, NA04, OC04, AF04, SA04, FA04);
                    else if (timeData >= tEU05 && timeData < tEU06) // 15minute interval
                        accumIt(tEU05, tEU06, 05, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU05, JA05, NA05, OC05, AF05, SA05, FA05);
                    else if (timeData >= tEU06 && timeData < tEU07) // 15minute interval
                        accumIt(tEU06, tEU07, 06, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU06, JA06, NA06, OC06, AF06, SA06, FA06);
                    else if (timeData >= tEU07 && timeData < tEU08) // 15minute interval
                        accumIt(tEU07, tEU08, 07, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU07, JA07, NA07, OC07, AF07, SA07, FA07);

                    else if (timeData >= tEU08 && timeData < tEU09) // 15minute interval
                        accumIt(tEU08, tEU09, 08, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU08, JA08, NA08, OC08, AF08, SA08, FA08);

                   else if (timeData >= tEU09 && timeData < tEU10) // 15minute interval
                        accumIt(tEU09, tEU10, 09, wrdmsg[2],wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20],  EU09, JA09, NA09, OC09, AF09, SA09, FA09);

                   else if (timeData >= tEU10 && timeData < tEU11) // 15minute interval
                        accumIt(tEU10, tEU11, 10, wrdmsg[2],wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20],  EU10, JA10, NA10, OC10, AF10, SA10, FA10);

                   else  if (timeData >= tEU11 && timeData < tEU12) // 15minute interval
                        accumIt(tEU11, tEU12, 11, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU11, JA11, NA11, OC11, AF11, SA11, FA11);
 
                    else if (timeData >= tEU12 && timeData < tEU13) // 15minute interval
                        accumIt(tEU12, tEU13, 12, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU12, JA12, NA12, OC12, AF12, SA12, FA12);
                   else if (timeData >= tEU13 && timeData < tEU14) // 15minute interval
                        accumIt(tEU13, tEU14, 13, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU13, JA13, NA13, OC13, AF13, SA13, FA13);
                   else if (timeData >= tEU14 && timeData < tEU15) // 15minute interval
                        accumIt(tEU14, tEU15, 14, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU14, JA14, NA14, OC14, AF14, SA14, FA14);
                   else if (timeData >= tEU15 && timeData < tEU16) // 15minute interval
                        accumIt(tEU15, tEU16, 15, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU15, JA15, NA15, OC15, AF15, SA15, FA15);
                   else if (timeData >= tEU16 && timeData < tEU17) // 16minute interval
                        accumIt(tEU16, tEU17, 16, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU16, JA16, NA16, OC16, AF16, SA16, FA16);
                   else if (timeData >= tEU17 && timeData < tEU18) // 17minute interval
                        accumIt(tEU17, tEU18, 17, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU17, JA17, NA17, OC17, AF17, SA17, FA17);
                   else if (timeData >= tEU18 && timeData < tEU19) // 18minute interval
                        accumIt(tEU18, tEU19, 18, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU18, JA18, NA18, OC18, AF18, SA18, FA18);
                   else if (timeData >= tEU19 && timeData < tEU20) // 19minute interval
                        accumIt(tEU19, tEU20, 19, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU19, JA19, NA19, OC19, AF19, SA19, FA19);
                   else if (timeData >= tEU20 && timeData < tEU21) // 20minute interval
                        accumIt(tEU20, tEU21, 20, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU20, JA20, NA20, OC20, AF20, SA20, FA20);
                   else if (timeData >= tEU21 && timeData < tEU22) // 21minute interval
                        accumIt(tEU21, tEU22, 21, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU21, JA21, NA21, OC21, AF21, SA21, FA21);
                   else if (timeData >= tEU22 && timeData < tEU23) // 22minute interval
                        accumIt(tEU22, tEU23, 22, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU22, JA22, NA22, OC22, AF22, SA22, FA22);
                   else if (timeData >= tEU23 && timeData < tEU24) // 23minute interval
                        accumIt(tEU23, tEU24, 23, wrdmsg[2], wrdmsg[5], wrdmsg[8], wrdmsg[11], wrdmsg[14], wrdmsg[17], wrdmsg[20], EU23, JA23, NA23, OC23, AF23, SA23, FA23);









                }//end of count > 0
                count += 1;
                //EUcnt = 0;
                //JAcnt = 0;
                //NAcnt = 0;
                //OCcnt = 0;
                //AFcnt = 0;
                //FAcnt = 0;
            }//end foreach
            }
            catch (Exception ex)
            {
                 frmMessageDialog md = new frmMessageDialog();
                 md.messageBoxUpper.Text = $"Error in PrepareArrays of plotdBmPerHour TimeData ";
                 md.messageBoxLower.Text = $"{ex}";
                 md.Show();

            }
        }//end function


         private void accumIt(TimeSpan start, TimeSpan end, int hour,  string EUn, string JAn, string NAn, string OCn, string AFn, string SAn, string FAn, Rectangle EUr, Rectangle JAr, Rectangle NAr, Rectangle OCr, Rectangle AFr, Rectangle SAr, Rectangle FAr)
        {
            double number;

            if (double.TryParse(EUn, out number))
            {
                dataEU[hour, 0] = dataEU[hour, 0] + number;
                dataEU[hour, 1] = dataEU[hour, 1] + 1;
             //   EUcnt += 1;
            }

            if (double.TryParse(JAn, out number))
            {
                dataJA[hour, 0] = dataJA[hour, 0] + number;
                dataJA[hour, 1] = dataJA[hour, 1] + 1;
            //    JAcnt += 1;
            }

            if (double.TryParse(NAn, out number))
            {
                dataNA[hour, 0] = dataNA[hour, 0] + number;
                dataNA[hour, 1] = dataNA[hour, 1] + 1;
            //    NAcnt += 1;
            }

            if (double.TryParse(OCn, out number))
            {
                dataOC[hour, 0] = dataOC[hour, 0] + number;
                dataOC[hour, 1] = dataOC[hour, 1] + 1;
             //   OCcnt += 1;
            }

            if (double.TryParse(AFn, out number))
            {
                dataAF[hour, 0] = dataAF[hour, 0] + number;
                dataAF[hour, 1] = dataAF[hour, 1] + 1;
            //    AFcnt += 1;
            }

            if (double.TryParse(SAn, out number))
            {
                dataSA[hour, 0] = dataSA[hour, 0] + number;
                dataSA[hour, 1] = dataSA[hour, 1] + 1;
           //     SAcnt += 1;
            }
            if (double.TryParse(FAn, out number))
            {
                dataFA[hour, 0] = dataFA[hour, 0] + number;
                dataFA[hour, 1] = dataFA[hour, 1] + 1;
           //     FAcnt += 1;
            }

            double EUavg = dataEU[hour, 0] / dataEU[hour, 1];
            double JAavg = dataJA[hour, 0] / dataJA[hour, 1];
            double NAavg = dataNA[hour, 0] / dataNA[hour, 1];
            double OCavg = dataOC[hour, 0] / dataOC[hour, 1];
            double AFavg = dataAF[hour, 0] / dataAF[hour, 1];
            double SAavg = dataSA[hour, 0] / dataSA[hour, 1];
            double FAavg = dataFA[hour, 0] / dataFA[hour, 1];

            colourit(EUavg, EUr);
            colourit(JAavg, JAr);
            colourit(NAavg, NAr);
            colourit(OCavg, OCr);
            colourit(AFavg, AFr);
            colourit(SAavg, SAr);
            colourit(FAavg, FAr);


        }














        private void colourit(double avg, Rectangle grid)
        {
            if (avg < -20)
                grid.Fill = System.Windows.Media.Brushes.LightGreen;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
            else if (avg >= -20 && avg < -15)
                grid.Fill = System.Windows.Media.Brushes.Yellow;
            else if (avg >= -15 && avg < -8)
                grid.Fill = System.Windows.Media.Brushes.Aqua;
            else if (avg >= -8 && avg < 0)
                grid.Fill = System.Windows.Media.Brushes.Blue;


            else if (avg == 0 )
                grid.Fill = System.Windows.Media.Brushes.Black;  //special case for 0

            else if (avg > 0 )
                grid.Fill = System.Windows.Media.Brushes.Red;
            else
                grid.Fill = System.Windows.Media.Brushes.LightGray;


        }






        private void colourTheCells(DataGrid mgrid)
        {





        }

        //this is the Timer for live data.  It starts in response to clicking the check box
        //declared outside of method so I can reference it in other methods as per below
        System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();

        private void chkLiveUpdate_Checked(object sender, RoutedEventArgs e)
        {
            btnPlot.Content = "Live";
            // now start the timer to process the UDP stuff now that we have started it.
            dispatcherTimer2.Tick += new EventHandler(dispatcherTimer2_Tick);
            dispatcherTimer2.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimer2.Start();
        }


        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {

            prepareArrays();

        }


        private void chkLiveUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            btnPlot.Content = "Plot";
        }
    }// end of class




}//end of name space
