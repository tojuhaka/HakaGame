using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace New2DRPGContent.Tileset
{

    // Simple calss for storing 
    // the information from tileset
    public class TilesetContent
    {
            public string textureName;
            public int tileWidth, tileHeight;
            public List<Rectangle> tileRectangles;
    }
}
