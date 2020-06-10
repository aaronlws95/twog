using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEngine;

namespace twog
{
    /// <summary>
    /// Global Access class
    /// </summary>
    public static class GAccess
    {
        #region Game Tags
        public static BitTag HouseTag = new BitTag("house");
        public static BitTag DoorTag = new BitTag("door");
        public static BitTag SolidTag = new BitTag("solid");
        public static BitTag NPCTag = new BitTag("npc");
        public static BitTag HittableTag = new BitTag("hittable");
        public static BitTag MonsterTag = new BitTag("monster");
        #endregion

    }

}
