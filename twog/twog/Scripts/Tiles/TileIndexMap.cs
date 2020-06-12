using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyEngine;

namespace twog
{
    public class TileIndexMap
    {
        private TileJson tileJson;
        public int[,] Indices;
        public bool[,] Collision;
        public string Name;

        public TileIndexMap(string path)
        {
            LoadJson(path);
            Name = tileJson.Name;
            Indices = Calc.Make2DArray<int>(tileJson.Indices, tileJson.Height, tileJson.Width);
            Indices = Calc.Transpose<int>(Indices);
            if (tileJson.Collision.GetLength(0) != 0)
            {
                Collision = Calc.Make2DArray<bool>(tileJson.Collision, tileJson.Height, tileJson.Width);
                Collision = Calc.Transpose<bool>(Collision);
            }
                
        }

        private void LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(Path.Combine(Engine.ContentDirectory, path + ".json")))
            {
                string readJson = r.ReadToEnd();
                tileJson = JsonConvert.DeserializeObject<TileJson>(readJson);
            }
        }

        private class TileJson
        {
            public string Name { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public int[] Indices { get; set; }
            public bool[] Collision { get; set; }
        }
    }
}
