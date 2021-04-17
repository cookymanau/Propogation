using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxAtlas;
using System;
using System.Drawing;

 //***************************************
     //ProgName: Plot points to class
     //Date: April 2021
     //Author: Ian cook and Various
     //Purose: put points on a DX Atlas map in real time
     //Comment:				
     //************************************************


namespace PropoPlot
{
    //class PlotDXAtlas
    
    /// <summary>
    /// helper class to do the work of plotting points to dXatlas This is just like using th mainwindow form
    /// you can put any thing in here that wou would put on the main form
    /// </summary>
    public partial class MainWindow
    {

        private bool weHaveConnectedAlready = false;
        private bool clearDXAtlasEachTime = true;

        // Declare the CustomLayer objects
        private CustomLayer _redLabels, _greenLabels, _blueLines, _aquaPoints, _redPoints, _niceGlyphs, _yellowPoints,_greenPoints,_bluePoints;

        private Atlas _atlas;

//        DxAtlas.Atlas _atlas = new Atlas();  //DxAtlas is a 32 bit dll, so we will have to build a 32bit software

      //  private object[,] Dx = new object[100, 3]; //not used
        private object[,] DxC = new object[100, 4];  //lat,long,callsign and dBm


        private void DXAtlasplotPoints()
        {

            // CustomLayer AquaPoints, RedLabels;

        //    if (!weHaveConnectedAlready) //************If we comment this out and run through connect and Configure, then we dont clear the map each time
       //         DxAtlasMapClear();

            // _connectAndConfigureAtlas();
            _fillDxLocations();

            if (clearDXAtlasEachTime == true)  //this gets asked each period
            {
              //  DxAtlasMapClear(); //this is currenlty empty  and does nothing, but it does not make up custom layer each time
            }
            else  //starts up in true so these are not made each time anew. That means they hang around
            {
                _aquaPoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);
                _yellowPoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);
                _redPoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);
                _greenPoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);
                _bluePoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);

           //this method only sort of works.  If you start Keeping points, and then go back not
           //clearing pointsd, all the ones already there stay on the map.
                
            }


            _showQso();

        }

        private void _fillDxLocations()
        {
            // define dx locations
            // the f flags indicate the value as a float (single) for the 
            // C# compiler
            for (int i = 0; i < QSOsThiInterval; i++)
            {
                // plotmessage.Text += $"Time:{Udppoint[i, 5]} Grid: {Udppoint[i, 2]} dBm: {Udppoint[i, 1]} DX: {Udppoint[1, 0]} Long: {Udppoint[i, 4]} Lat:{Udppoint[i, 3]} \r\n";   //this just a display of data

                //all the same 
                float longitude = float.Parse(Udppoint[i, 4]);
                float latitude = float.Parse(Udppoint[i, 3]);

                DxC[i, 0] = longitude;
                DxC[i, 1] = latitude;
                DxC[i, 2] = Udppoint[i, 0];  //the callsign
                DxC[i, 3] = Udppoint[i, 1];  //the dBm

            }

        }

        private void _showQso()
        {
            // this code is the same as the VB code in the sample although the 
            // sample, but has been changed to C# syntax and switching code added

            _atlas.Map.BeginUpdate();

                 _showPointsC();

                 _showLines();
                 _showLabels();

             //_redLabels.Visible = !_redLabels.Visible;
             //_greenLabels.Visible = !_greenLabels.Visible;
             //_blueLines.Visible = !_blueLines.Visible;
             //_redPoints.Visible = !_redPoints.Visible;
             //_yellowPoints.Visible = !_yellowPoints.Visible;
             //_niceGlyphs.Visible = false;
            
            _aquaPoints.Visible = true;
            _redPoints.Visible = true;
            _yellowPoints.Visible = true;
            _greenPoints.Visible = true;

            _atlas.Map.EndUpdate();
            _atlas.Visible = true;
            _atlas.BringToFront();
        }//end


        private void _showPointsC()
        {
            object[] pointsLT20 = new object[QSOsThiInterval]; //points < -10dBm
            object[] pointsLT10 = new object[QSOsThiInterval]; //points < -10dBm
            object[] pointsLT0 = new object[QSOsThiInterval]; //points < -10dBm
            object[] pointsGT0 = new object[QSOsThiInterval];  //points > 0 dBm

// going for a colour ramp of
// yellow < -20 dBm  --hard
// acqua -10 to -20 -- should get them eventually
//greem -0 to -10   -- so that should be easy peesy
//red anything > 0 dBm --next door

            for (int i = 0; i < QSOsThiInterval; i++) //this is where we divide into coloured points based on signal strength
            {
                string dBmstr = DxC[i, 3].ToString();
                int dBm = int.Parse(dBmstr) ;
                

                  if (dBm > -40  && dBm <= -15){ //yellow
                    object[] pt = new object[2];
                    pt[0] = DxC[i, 0];
                    pt[1] = DxC[i, 1];
                    pointsLT20[i] = pt;
                }


                if (dBm > -15 && dBm <= -8) //these are aqua
                {
                    object[] pt = new object[2];
                    pt[0] = DxC[i, 0];
                    pt[1] = DxC[i, 1];
                    pointsLT10[i] = pt;
                }

                if (dBm > -8 && dBm <= 0) //these are green
                {
                    object[] pt = new object[2];
                    pt[0] = DxC[i, 0];
                    pt[1] = DxC[i, 1];
                    pointsLT0[i] = pt;
                }
                if (dBm > 0 )  //these are red
                {
                    object[] pt = new object[2];
                    pt[0] = DxC[i, 0];
                    pt[1] = DxC[i, 1];
                    pointsGT0[i] = pt;
                }


            }// end for loop


            pointsLT20 = pointsLT20.Where(c => c != null).ToArray();//get rid of any nulls.  There shouldnt be any
            _yellowPoints.SetData(pointsLT20);
            _yellowPoints.BrushColor = EnumColor.clYellow;
            _yellowPoints.PenColor = EnumColor.clDkGray;
            _yellowPoints.PointSize = 4;
            _yellowPoints.Visible = true;


            pointsLT10 = pointsLT10.Where(c => c != null).ToArray();
            _aquaPoints.SetData(pointsLT10);
            _aquaPoints.BrushColor = EnumColor.clAqua;
            _aquaPoints.PenColor = EnumColor.clBlue;
            _aquaPoints.PointSize = 5;
            _aquaPoints.Visible = true;       
            
            pointsLT0 = pointsLT0.Where(c => c != null).ToArray();
            _greenPoints.SetData(pointsLT0);
            _greenPoints.BrushColor = EnumColor.clBlue;
            _greenPoints.PenColor = EnumColor.clFuchsia;
            _greenPoints.PointSize = 5;
            _greenPoints.Visible = true;

            pointsGT0 = pointsGT0.Where(c => c != null).ToArray();
            _redPoints.SetData(pointsGT0);
            _redPoints.BrushColor = EnumColor.clRed;
            _redPoints.PenColor = EnumColor.clRed;
            _redPoints.PointSize = 7;
            _redPoints.Visible = true;
 
        
        }//end

        private void _showLabels()
        {
            try
            {
                // Note the object declarations....
                object[] labels = new object[2];
                //Font font = new Font(FontFamily.GenericMonospace,                        9.0F, FontStyle.Regular);

                // red Labels
                object[] lb = new object[10]; //was 3
                lb[0] = DxC[0, 0];
                lb[1] = DxC[0, 1];
                lb[2] = DxC[0, 2];
                labels[0] = lb;

                lb = new object[10]; //was3
                lb[0] = DxC[1, 0];
                lb[1] = DxC[1, 1];
                lb[2] = DxC[1, 2];
                labels[1] = lb;

                _redLabels.SetData(labels);

                lb = new object[3];
                // green labels
                lb[0] = DxC[2, 0];
                lb[1] = DxC[2, 1];
                lb[2] = DxC[2, 2];
                labels[0] = lb;

                lb = new object[3];
                lb[0] = DxC[3, 0];
                lb[1] = DxC[3, 1];
                lb[2] = DxC[3, 2];
                labels[1] = lb;

                _greenLabels.SetData(labels);

            }
            catch (Exception ex)
            {
                //throw ex;
                System.Windows.MessageBox.Show("Bad things just happened in labels");
            }
        }


        private void _showLines()
        {
            try
            {
                // Note the object declarations....
                object[] lines = new object[4];
                for (int i = 0; i < 4; i++)
                {
                    //first point
                    object[] pt = new object[2];
                    object[] ln = new object[2];
                    pt[0] = _atlas.Map.HomeLongitude;
                    pt[1] = _atlas.Map.HomeLatitude;
                    ln[0] = pt;

                    // second point
                    pt = new object[2];
                    pt[0] = DxC[i, 0];
                    pt[1] = DxC[i, 1];
                    ln[1] = pt;
                    lines[i] = ln;
                }
                _blueLines.SetData(lines);

            }
            catch (Exception ex)
            {
                // throw ex; 
            //    System.Windows.MessageBox.Show("Bad things just happened in lines");
            }
        }

        private void _connectAndConfigureAtlas()
        {
            // this code is the same as the VB code in the sample although the 
            // sample, but has been changed to C# syntax
            //            try
            //            {
          //  weHaveConnectedAlready = true;  //only want to do thjis once...maybe

             _atlas = new Atlas();
           _atlas.Map.CustomLayers.Clear();  // ***************
          
                 _atlas.Map.Ocean3D = true;
            //     _atlas.Map.PrefixesVisible = false;
                 _atlas.Map.Land3D = true;
                 _atlas.Map.PinsVisible = false;
            //     _atlas.Map.PrefixesVisible = false;
                 _atlas.Map.CqZonesVisible = false;
                 _atlas.Clock.Visible = true;
                 _atlas.Clock.UtcMode = true;
            _atlas.ToolbarVisible = false;
            //
            _atlas.Map.PrefixesVisible = false;
               // _atlas.Map.Projection = EnumProjection.PRJ_AZIMUTHAL;  // dont set this - we wnat it to stay where we put it
             //  _atlas.Map.Projection = EnumProjection.PRJ_RECTANGULAR;

            // Your Home location goes here 'f' indicates to the compiler
            // that the numeric values are type float (single)
                 _atlas.Map.HomeLatitude = -31.990f;
                _atlas.Map.HomeLongitude = 116.0500f;
            
                _atlas.Map.CenterLatitude = 0F;   //dont home the map
                 _atlas.Map.CenterLongitude = 116F;

            // layers
                _aquaPoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);
              _yellowPoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);
              _redPoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);
              _greenPoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);
              _bluePoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);
                _blueLines = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_LINES);
                _redLabels = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_LABELS);
                _greenLabels = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_LABELS);
                _niceGlyphs = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_GLYPHS);

                // in the beginning these are false due to added switching code
                _redLabels.Visible = false;
                _greenLabels.Visible = false;
                _blueLines.Visible = false;
                _aquaPoints.Visible = false;
            _yellowPoints.Visible = false;

                _niceGlyphs.Visible = false;

                // points
 //         _aquaPoints.BrushColor = EnumColor.clAqua;
 //         _aquaPoints.PenColor = EnumColor.clBlue;
 //         _aquaPoints.PointSize = 3;

 //
 //     _yellowPoints.BrushColor = EnumColor.clYellow;
 //     _yellowPoints.PenColor = EnumColor.clYellow;
 //     _yellowPoints.PointSize = 2;
 //

                // lines
            _blueLines.PenColor = EnumColor.clBlue;

                // red labels
                _redLabels.LabelsTransparent = true;
                _redLabels.PenColor = EnumColor.clRed;
                //Font _rlFont = new Font(FontFamily.GenericMonospace,                        9.0F, FontStyle.Bold);


                // green labels
 //           _greenLabels.LabelsTransparent = false;
 //           _greenLabels.BrushColor = EnumColor.clLime;
 //           _greenLabels.PenColor = EnumColor.clGreen;
 //           //Font _glfont = new Font(FontFamily.GenericMonospace,                        9.0F, FontStyle.Bold | FontStyle.Italic);

                // glyphs

                //_niceGlyphs.LoadGlyphsFromFile(Application.StartupPath + "\\Glyphs.bmp", 1, 15);

                _atlas.Height = 550;
                _atlas.Width = 900;
               // this.Top = _atlas.Top + _atlas.Height + 10;
              //  this.Left = _atlas.Left + ((_atlas.Width - this.Width) / 2);

                //show atlas window
                _atlas.Visible = true;

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
        }


        /// <summary>
        /// clears the map
        /// </summary>
        private void DxAtlasMapClear() {

            try
            {
              _atlas.Map.CustomLayers.Clear();
            _connectAndConfigureAtlas();

            }
            catch (Exception)
            {

//                throw;
            }
            

     
         

        }





    }
}
