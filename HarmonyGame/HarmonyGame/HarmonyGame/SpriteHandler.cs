using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarmonyGame
{
    class SpriteHandler
    {
        List<List<Sprite>> MasterList;
        List<String> Index;

        public SpriteHandler()
        {
            MasterList = new List<List<Sprite>>();
            Index = new List<String>();
        }

        public void addList(List<Sprite> newList)
        {
            Type t = newList[0].GetType();

            MasterList.Add(newList);
            Index.Add(t.ToString());
        }

        public void updateList(Sprite s, Type t)
        {
            int i = -1;
            String match = t.ToString();
            foreach (String str in Index)
            {
                i++;
                if (str.Equals(match))
                {
                    break;
                }
            }

            MasterList[i].Add(s);
        }

        public bool spriteExists(Sprite s)
        {
            foreach (List<Sprite> spriteList in MasterList)
            {
                foreach (Sprite sprite in spriteList)
                {
                    if (sprite.Equals(s))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

