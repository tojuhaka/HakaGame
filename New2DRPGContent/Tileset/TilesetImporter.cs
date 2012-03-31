using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.Xml;
using TImport = System.Xml.XmlDocument;

namespace New2DRPGContent
{
    // Import the xml file
    [ContentImporter(".tset", DisplayName = "Tileset Importer",
    DefaultProcessor = "TilesetProcessor")]
    public class ContentImporter1 : ContentImporter<TImport>
    {
        public override TImport Import(string filename,
        ContentImporterContext context)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            return xmlDoc;
        }
    }
}
