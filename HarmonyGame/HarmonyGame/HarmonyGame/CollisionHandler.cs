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
    class CollisionHandler
    {
        public CollisionHandler()
        {
        }

        public bool DumbCollides(Sprite sprite1, Sprite sprite2)
        {
            if (sprite1.Bounds.Intersects(sprite2.Bounds))
            {
                return true;
            }

            return false;
        }

        public bool PerPixelCollides(Sprite sprite1, Sprite sprite2)
        {
            Color[,] sprite1Color = sprite1.ColorData;
            Color[,] sprite2Color = sprite2.ColorData;
            Rectangle rectangle1 = sprite1.Bounds;
            Rectangle rectangle2 = sprite2.Bounds;

            int yMax = Math.Max(sprite1Color.GetLength(0), sprite2Color.GetLength(0));
            int xMax = Math.Max(sprite1Color.GetLength(1), sprite2Color.GetLength(1));

            int top = Math.Max(rectangle1.Top, rectangle2.Top);
            int bottom = Math.Min(rectangle1.Bottom, rectangle2.Bottom);
            int left = Math.Max(rectangle1.Left, rectangle2.Left);
            int right = Math.Min(rectangle1.Right, rectangle2.Right);

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    Color colorA = sprite1Color[x,y];
                    Color colorB = sprite2Color[x,y];

                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                }
            }
            return false; 
        }

        //Convenience method
        public bool Collides(Sprite sprite1, Sprite sprite2)
        {
            if(DumbCollides(sprite1, sprite2))
            {
                if(PerPixelCollides(sprite1, sprite2))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
