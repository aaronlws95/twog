using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace twog
{
    public class MapJson
    {
        public string Name;
        public List<int> Tiles; 
    }

    public class Map
    {
        public MapJson mapjson;

        public Map(string map_file)
        {
            LoadJson(map_file);
        }

        private void LoadJson(string map_file)
        {
            using (StreamReader r = new StreamReader(map_file))
            {
                string json = r.ReadToEnd();
                mapjson = JsonConvert.DeserializeObject<MapJson>(json);
            }
        }
    }
}
