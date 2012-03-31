using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using TWrite = New2DRPGContent.Tileset.TilesetContent;
namespace New2DRPGContent.Tileset
{
    // Writes the information into content
    [ContentTypeWriter]
    public class TilesetTypeWriter : ContentTypeWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write(value.textureName);
            output.Write(value.tileWidth);
            output.Write(value.tileHeight);
            output.Write(value.tileRectangles.Count);
            foreach (Rectangle rect in value.tileRectangles)
            {
                output.Write(rect.X);
                output.Write(rect.Y);
                output.Write(rect.Width);
                output.Write(rect.Height);
            }
        }
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "New2DRPG.TilesetReader, HakaGame";
        }
    }
}