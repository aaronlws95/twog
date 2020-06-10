using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEngine;

namespace twog
{
    public class Level : Scene
    {
        public Background Background;
        public PlayerCamera Camera;
        public bool BoundCamera = false;

        public Level()
        {

        }
    }
}
