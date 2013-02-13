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
    class MainFloor : Wall
    {
        const String ASSETNAME = "floor";

        private bool THIN = false;
        private Vector2 Facing = new Vector2(0, 1);

        Vector2 mVelocity = Vector2.Zero;
        Vector2 mStartingPosition = Vector2.Zero;

        ContentManager mContentManager;

        public MainFloor(Vector2 startingPosition, Vector2 facing, bool thin)
        {
            Facing = facing;
            mStartingPosition = startingPosition;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;

            Position = mStartingPosition;
            base.LoadContent(theContentManager, ASSETNAME);
            Source = new Rectangle(0, 0, Source.Width, Source.Height);
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime, mVelocity);
        }

        /*public void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch);
        }*/
    }
}
