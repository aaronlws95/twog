using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEngine;

namespace twog
{
    public class Background : Entity
    {
        private Tileset tileSet;

        public Background(string path)
        {
            tileSet = new Tileset(GFX.SpriteBank.Create("tile").Texture, 16, 16);
            TileIndexMap tileIndexMap = new TileIndexMap(path);
            TileGrid tileGrid = new TileGrid(16, 16, tileIndexMap.Indices.GetLength(0), tileIndexMap.Indices.GetLength(1));
            tileGrid.Populate(tileSet, tileIndexMap.Indices);
            Add(tileGrid);
            Depth = 1;
        }
    }

}
