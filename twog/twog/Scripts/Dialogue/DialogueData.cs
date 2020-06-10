using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyEngine;
using Newtonsoft.Json;

namespace twog
{
    public class DialogueData
    {
        private static DialogueJson dialogueJson;

        #region Singleton

        private static DialogueData instance = null;

        public static DialogueData Instance
        {
            get
            {
                if (instance == null)
                    instance = new DialogueData();
                return instance;
            }
        }

        private DialogueData()
        {
            Initialize();
        }

        #endregion

        private void Initialize()
        {
            LoadJson("Dialogue/dialogue_data");
        }

        public static DialogueInfo GetDialogueInfo(string key)
        {
            return dialogueJson.Dialogues[key];
        }

        private void LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(Path.Combine(Engine.ContentDirectory, path + ".json")))
            {
                string readJson = r.ReadToEnd();
                dialogueJson = JsonConvert.DeserializeObject<DialogueJson>(readJson);
            }
        }

        private class DialogueJson
        {
            public Dictionary<String, DialogueInfo> Dialogues { get; set; }
        }
    }
}
