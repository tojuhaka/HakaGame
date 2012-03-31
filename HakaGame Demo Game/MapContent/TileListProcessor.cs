using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content;

using System.Xml;
using TInput = System.Xml.XmlDocument;
using TOutput = HakaGame.TileListContent;

namespace HakaGame
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "HakaGame.MapContent.TileListProcessor")]
    public class TileListProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            TileListContent list = new TileListContent();
            Tile tile;
            string filename;

            foreach (XmlNode node in input.DocumentElement.ChildNodes) // tilesetFile nodes
            {
                //TODO Make tilesets 
                if (node.Name == "tiles") // if node is <tiles>
                {
                    foreach (XmlNode imageTileNode in node.ChildNodes) // <imageTiles>
                    {

                        int id = Int32.Parse(imageTileNode.Attributes["id"].Value);
                        int width = Int32.Parse(imageTileNode.Attributes["width"].Value);
                        int height = Int32.Parse(imageTileNode.Attributes["height"].Value);

                        foreach (XmlNode imageTileNodes in imageTileNode.ChildNodes)
                        {
                            if (imageTileNodes.Name == "image")
                            {
                                string file = imageTileNodes.Attributes["file"].Value;
                                string[] contentName = file.Split('.');

                                try
                                {
                                    file = contentName[0];
                                }
                                catch
                                {
                                    throw new Exception("Invalid picture name at " + file);
                                }
                                filename = "Content/images/" + contentName[0];

                                tile = new Tile(filename, id);
                                list.TileList.Add(tile);
                            }

                            //TODO Optimize tilesets 
                            if (imageTileNodes.Name == "tilesetImage")
                            {
                                string file = imageTileNodes.Attributes["file"].Value;
                                string[] contentName = file.Split('.');

                                try
                                {
                                    file = contentName[0];
                                }
                                catch
                                {
                                    throw new Exception("Invalid picture name at " + file);
                                }

                                //Loads the whole texture. for optimizing just load the tileset
                                //Once then use the same texture in all tiles
                                filename = "Content/images/" + contentName[0];
                                int x = Int32.Parse(imageTileNodes.Attributes["posX"].Value);
                                int y = Int32.Parse(imageTileNodes.Attributes["posY"].Value);
                                                      
                                Rectangle tilesetPart = new Rectangle(x, y, width, height);
                                tile = new Tile(filename, id, tilesetPart);
                                tile.IsPartOfTileset = true;
                                list.TileList.Add(tile);
                            }
                        }

                        //TODO: Check for bugs
                        //Load nulltiles with collision
                        if (imageTileNode.Name == "nullTile")
                        {
                            foreach (XmlNode imageTileNodes in imageTileNode.ChildNodes)
                            {
                                if (imageTileNodes.Name == "collision")
                                {
                                    string collision = imageTileNodes.Attributes["area"].Value;
                                    if (collision != "none")
                                    {
                                        tile = new Tile(CollisionType.Unpassable);
                                    }
                                    else
                                    {
                                        tile = new Tile(CollisionType.Passable);
                                    }
                                    tile.TileID = id;
                                    list.TileList.Add(tile);
                                }
                            }
                        }
                    }
                }
            }

            return list;
        }
    }
}