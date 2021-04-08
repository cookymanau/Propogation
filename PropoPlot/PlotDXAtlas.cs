using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxAtlas;

namespace PropoPlot
{
    //class PlotDXAtlas
    
    /// <summary>
    /// helper class to do the work of plotting points to dXatlas
    /// </summary>
    public partial class MainWindow
    {

        int DotSize = 3;
        string Colour = "red";
        
        DxAtlas.Atlas _atlas = new Atlas();  //DxAtlas is a 32 bit dll, so we will have to build a 32bit software


        private void DXAtlasplotPoints()
        {

            CustomLayer AquaPoints, RedLabels;
            
            


            dxAtlasSetup();

            AquaPoints = _atlas.Map.CustomLayers.Add(EnumLayerKind.LK_POINTS);


            _atlas.Map.BeginUpdate();
            _atlas.Map.Projection = DxAtlas.EnumProjection.PRJ_AZIMUTHAL;
            _atlas.Visible = true;
                _atlas.BringToFront();
            //_atlas.Map.EndUpdate();
        }

        private void dxAtlasSetup()
        {

            _atlas.Map.Ocean3D = true;
            _atlas.Map.PrefixesVisible = false;
            _atlas.Map.Land3D = true;
            _atlas.Map.PinsVisible = false;
            _atlas.Map.PrefixesVisible = false;
            _atlas.Map.CqZonesVisible = false;
            _atlas.Clock.Visible = true;
            _atlas.Clock.UtcMode = true;
            //            ' _atlas.Map.Projection = EnumProjection.PRJ_RECTANGULAR

            //          '// Your Home location goes here 'f' indicates to the compiler
            //        '// that the numeric values are type float (single)
            _atlas.Map.HomeLatitude = -32.67302F;
            _atlas.Map.HomeLongitude = 116.04F;



        }
    }
}
