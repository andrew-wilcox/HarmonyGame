using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarmonyGame
{
    class Projectile : Sprite
    {
        Vector2 mVelocity;
        bool Visible = false;

        int maxDistance;
        double distanceTraveled = 0;

        Vector2 lastPosition;

        Sprite Creator;

        public void Update(GameTime gameTime)
        {
            distanceTraveled += Pythagorean(Position - lastPosition);

            if (distanceTraveled > maxDistance)
            {
                Visible = false;
            }

            if (Visible)
            {
                base.Update(gameTime, mVelocity);
            }
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            base.LoadContent(theContentManager, theAssetName);
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (Visible)
            {
                base.Draw(theSpriteBatch);
            }
        }

        public void Launch(Vector2 velocity, Vector2 position)
        {
            Visible = true;
            distanceTraveled = 0;
            Position = position;
            mVelocity = velocity;
        }

        public double Pythagorean(Vector2 v)
        {
            return Math.Sqrt((Math.Pow(v.X, 2) + Math.Pow(v.Y, 2)));
        }
    }
}
