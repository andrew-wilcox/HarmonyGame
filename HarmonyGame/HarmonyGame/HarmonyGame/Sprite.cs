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

        //Rectangular area that defines
        //the sprite
        Rectangle mSource;

        //****************
        //BEGIN PROPERTIES
        //****************

        public Rectangle Source
        {
            get { return mSource; }

            set
            {
                mSource = value;
                Size = new Rectangle(0, 0, (int)(mSource.Width * Scale), (int)(mSource.Height * Scale));
            }
        }

        public float Scale
        {
            get { return mScale; }

            set
            {
                mScale = value;
                Size = new Rectangle(0, 0, (int)(Source.Width * Scale), (int)(Source.Height * Scale));
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
            AssetName = theAssetName;
            Source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Size = new Rectangle(0, 0, (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(Texture, Position, Source, Color.White,
                0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime, Vector2 speed, Vector2 direction)
        {
            Position += speed * direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
