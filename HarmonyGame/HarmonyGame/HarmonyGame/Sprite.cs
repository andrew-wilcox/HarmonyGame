using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarmonyGame
{
    public class Sprite
    {
        //Current position of the sprite
        public Vector2 Position = new Vector2(0, 0);

        //The texture for the sprite
        private Texture2D Texture;

        //Size of the sprite (includes scale)
        public Rectangle Size;

        //Used to scale the sprite
        public float mScale = 1.0f;

        //Name for the sprite's asset
        public string AssetName;

        //****************
        //Begin Properties
        //****************

        public float Scale
        {
            get { return mScale; }

            set
            {
                mScale = value;
                Size = new Rectangle(0, 0, (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
            }
        }

        //**************
        //END PROPERTIES
        //**************

        public Sprite()
        {
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            Texture = theContentManager.Load<Texture2D>(theAssetName);
            Size = new Rectangle(0, 0, (int)(Texture.Width * mScale), (int)(Texture.Height * mScale));
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(Texture, Position,
                new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White,
                0.0f, Vector2.Zero, mScale, SpriteEffects.None, 0);
        }

        //*************************
        //BEGIN GETTERS AND SETTERS
        //*************************
        public Vector2 getPosition()
        {
            return Position;
        }

        public Texture2D getTexture()
        {
            return Texture;
        }

        public void setPosition(Vector2 vector)
        {
            Position = vector;
        }

        public void setTexture(Texture2D t)
        {
            Texture = t;
        }
        //***********************
        //END GETTERS AND SETTERS
        //***********************
    }

}
