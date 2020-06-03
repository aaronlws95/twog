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
    public class TileIndexMap : Entity
    {
        private TileJson tileJson;
        public int[,] Indices;
        public string Name;

        public TileIndexMap(string path)
        {
            LoadJson(path);
            Name = tileJson.Name;
            Indices = tileJson.Indices;
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
            public string Name;
            public int[,] Indices;
        }
    }
}
