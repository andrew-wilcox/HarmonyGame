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
    class Collidable : Sprite
    {
        public virtual bool Collides(Sprite sprite)
        {
            if (this.Bounds.Intersects(sprite.Bounds))
            {
                return true;
            }
            return false;
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
