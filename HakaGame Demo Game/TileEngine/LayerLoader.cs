using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace HakaGame
{
    // Loads layers from given xml-doc
    class LayerLoader
    {
        int mapWidth;
        int mapHeight;

        List<MapLayer> layers = new List<MapLayer>();

        public LayerLoader(string filename, TileList tileList)
        {
             XmlDocument xmlDoc = new XmlDocument();
             xmlDoc.Load(filename);

             foreach (XmlNode tileMapNode in xmlDoc.DocumentElement.ChildNodes) // tilesetFile nodes
             {
                 //TODO Get tileWidth and tileHeight also
                 mapWidth = int.Parse(tileMapNode.Attributes["rows"].Value);
                 mapHeight = int.Parse(tileMapNode.Attributes["columns"].Value);

                 foreach (XmlNode layerNode in tileMapNode.ChildNodes)
                 {
                     MapLayer layer = new MapLayer(mapWidth, mapHeight);

                     //Get data node inside layernode
                     XmlNode dataNode = layerNode.FirstChild;
                     string data = dataNode.InnerXml;

                     //Parse all extra marks from data
                     data = data.Replace("\r\n\t\t\t\t", "");
                     data = data.Replace("\n\t\t\t", "");

                     //Parse all , makrs also
                    
                     int counter = 0; // counter for data 

                     for (int y = 0; y < mapWidth; y++)
                     {
                         for (int x = 0; x < mapHeight; x++)
                         {
                             // Get data id
                           // int id = int.Parse(data[counter].ToString());
                             int id = RemoveInt(ref data);
                             if (id != 0)
                             {
                                 layer.SetTile(x, y, tileList.GetTile(id));
                             }
                             counter++;
                         }
                     }
                      layers.Add(layer);
                 }
             }        
        }

        private int RemoveInt(ref string data)
        {
            
            int index = data.IndexOf(',');
            if (index < 0)
            {
                return int.Parse(data);
            }
            string number = data.Substring(0, index);
            data = data.Substring(index+1);
            return int.Parse(number);
        }

        public List<MapLayer> GetLayerList()
        {
            return layers;
        }
    }
}
