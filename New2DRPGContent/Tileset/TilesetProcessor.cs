using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System.Xml;
using New2DRPGContent.Tileset;
using TInput = System.Xml.XmlDocument;
using TOutput = New2DRPGContent.Tileset.TilesetContent;

namespace New2DRPGContent.Tileset
{
    // Process the xml-file
    [ContentProcessor(DisplayName = "Tileset Processor")]
    public class TilesetProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input,
        ContentProcessorContext context)
        {
            TilesetContent tilesetContent = new TilesetContent();
            foreach (XmlNode node in input.DocumentElement.ChildNodes)
            {
                if (node.Name == "TextureElement")
                {
                    tilesetContent.textureName =
                    node.Attributes["TextureName"].Value;
                }
                if (node.Name == "TilesetDefinitions")
                {
                    tilesetContent.tileWidth =
                    Int32.Parse(node.Attributes["TileWidth"].Value);
                    tilesetContent.tileHeight =
                    Int32.Parse(node.Attributes["TileHeight"].Value);
                }
                if (node.Name == "TilesetRectangles")
                {
                    List<Rectangle> rectangles = new List<Rectangle>();
                    foreach (XmlNode rectNode in node.ChildNodes)
                    {
                        if (rectNode.Name == "Rectangle")
                        {
                            Rectangle rect;
                            rect = new Rectangle(
                            Int32.Parse(rectNode.Attributes["X"].Value),
                            Int32.Parse(rectNode.Attributes["Y"].Value),
                            Int32.Parse(rectNode.Attributes["Width"].Value),
                            Int32.Parse(rectNode.Attributes["Height"].Value));
                            rectangles.Add(rect);
                        }
                    }
                    tilesetContent.tileRectangles = rectangles;
                }
            }
            return tilesetContent;
        }
    }
}