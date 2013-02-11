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
    class Enemy : AnimateObject
    {
        public override bool Collides(Sprite sprite)
        {
            return base.Collides(sprite);
        }

        public override void HandleInput(KeyboardState aCurrentKeyboardState, KeyboardState previousKeyboardState)
        {
            base.HandleInput(aCurrentKeyboardState, previousKeyboardState);
        }

        //Game Logic Functions
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            base.LoadContent(theContentManager, theAssetName);
        }

        public void Update(GameTime gameTime, Vector2 velocity)
        {
            base.Update(gameTime, velocity);
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch);
        }
    }
}
