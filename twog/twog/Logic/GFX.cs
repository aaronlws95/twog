using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEngine;

namespace twog
{
    public class GFX
    {

        #region Fields

        private static SpriteBank spriteBank = null;
        List<Atlas> Atlases;

        public static SpriteBank SpriteBank
        {
            get
            {
                return spriteBank;
            }
        }

        #endregion

        #region Singleton

        private static GFX instance = null;

        public static GFX Instance
        {
            get
            {
                if (instance == null)
                    instance = new GFX();
                return instance;
            }
        }

        private GFX()
        {
            Initialize();
        }

        #endregion

        private void Initialize()
        {
            Atlases = new List<Atlas>();
            Atlases.Add(Atlas.FromAtlas("Sprites/Spritesheets/player_spritesheet_0", Atlas.AtlasDataFormat.CrunchXmlOrBinary));
            spriteBank = new SpriteBank(Atlases[0], "Sprites/sprite_data_0.xml"); 
        }

    }
}
