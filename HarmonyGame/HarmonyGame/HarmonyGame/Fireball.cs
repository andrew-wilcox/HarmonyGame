using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace HarmonyGame
{
    class Fireball : Projectile
    {
        const int MAX_DISTANCE = 500;
        const string FIREBALL_ASSETNAME = "fireball";
        Vector2 VELOCITY = new Vector2(200, 0);

        public bool Visible = false;

        Sprite Creator;

        public Fireball(Sprite creator)
        {
            Creator = creator;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, FIREBALL_ASSETNAME);
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime, VELOCITY);
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
 	        base.Draw(theSpriteBatch);
        }

        public void Launch(Vector2 position)
        {
            base.Launch(VELOCITY, position);
        }
    }
}
