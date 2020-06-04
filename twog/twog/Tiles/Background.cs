using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MyEngine;

namespace twog
{
    public class Background : Entity
    {
        private Tileset tileSet;

        public Background(string spritesheetPath, string mapPath)
        {
            MTexture tileSpritesheet = MTexture.FromFile(Path.Combine(Engine.ContentDirectory, spritesheetPath));
            tileSet = new Tileset(tileSpritesheet, 16, 16);
            TileIndexMap tileIndexMap = new TileIndexMap(mapPath);
            TileGrid tileGrid = new TileGrid(16, 16, tileIndexMap.Indices.GetLength(0), tileIndexMap.Indices.GetLength(1));
            tileGrid.Populate(tileSet, tileIndexMap.Indices);
            Add(tileGrid);
            Depth = 1;
        }
    }

}
