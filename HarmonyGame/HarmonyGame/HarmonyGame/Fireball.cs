using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace HarmonyGame
{
    class Fireball : Sprite
    {
        const int MAX_DISTANCE = 500;
        const string FIREBALL_ASSETNAME = "fireball";

        public bool Visible = false;

        Vector2 mStartPosition, mSpeed, mDirection;

        Sprite Creator;

        public Fireball(Sprite creator)
        {
            Creator = creator;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, FIREBALL_ASSETNAME);
            Scale = 1.0f;
        }

        public void Update(GameTime gameTime)
        {
            if(Vector2.Distance(mStartPosition, Position) > MAX_DISTANCE)
            {
                Visible = false;
            }

            if (Visible)
            {
                base.Update(gameTime, mSpeed, mDirection);
            }
        }

        public override void  Draw(SpriteBatch theSpriteBatch)
        {
            if(Visible)
            {
 	            base.Draw(theSpriteBatch);
            }
        }

        public void Launch(Vector2 theStartPosition, Vector2 theSpeed, Vector2 theDirection)
        {
            Position = theStartPosition;
            mStartPosition = theStartPosition;
            mSpeed = theSpeed;
            mDirection = theDirection;
            Visible = true;
        }


    }
}
