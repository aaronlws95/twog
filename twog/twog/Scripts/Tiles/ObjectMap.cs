using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MyEngine;

namespace twog
{
    public class ObjectMap : Entity
    {
        private Tileset tileSet;
        public int GridWidth;
        public int GridHeight;

        public ObjectMap(string spritesheetPath, string mapPath)
        {
            MTexture tileSpritesheet = MTexture.FromFile(Path.Combine(Engine.ContentDirectory, spritesheetPath));
            tileSet = new Tileset(tileSpritesheet, 16, 16);
            TileIndexMap tileIndexMap = new TileIndexMap(mapPath);
            TileGrid tileGrid = new TileGrid(16, 16, tileIndexMap.Indices.GetLength(0), tileIndexMap.Indices.GetLength(1));
            tileGrid.PopulateOneIndex(tileSet, tileIndexMap.Indices);
            
            // set 0 to null
            for (int x = 0; x < tileIndexMap.Indices.GetLength(0); x++)
                for (int y = 0; y < tileIndexMap.Indices.GetLength(1); y++)
                {
                    if (tileIndexMap.Indices[x, y] == 0)
                        tileGrid.Tiles[x, y] = null;
                }
                    

            Add(tileGrid);
            GridWidth = tileIndexMap.Indices.GetLength(0) * 16;
            GridHeight = tileIndexMap.Indices.GetLength(1) * 16;

            Collider = new Grid(16, 16, tileIndexMap.Collision);
            Tag = GAccess.SolidTag;
        }
    }

}
