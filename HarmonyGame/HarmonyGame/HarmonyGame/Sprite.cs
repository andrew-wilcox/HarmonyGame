﻿using System;
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
    public class Sprite
    {
        //Resolution of each sprite image
        public int ResolutionX;
        public int ResolutionY;

        //Current position of the sprite
        public Vector2 Position = Vector2.Zero;

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

        //Quick check to see if this sprite is on a floor
        public bool onFloor = false;

        //Sprite's creator, may be null
        Sprite creator = null;

        //****************
        //BEGIN PROPERTIES
        //****************
        public Texture2D SpriteTexture
        {
            get { return Texture;}
            set { Texture = value; }
        }
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

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)(ResolutionX * Scale), (int)(ResolutionY * Scale)); }
        }

        public Color[,] ColorData
        {
            get { return TextureTo2DArray(this.Texture); }
        }
            

        //**************
        //END PROPERTIES
        //**************
        
        //******************
        //BEGIN MAIN METHODS
        //******************
        public Sprite()
        {
        }

        public Sprite(Sprite creatorSprite)
        {
            creator = creatorSprite;
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            Texture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Size = new Rectangle(0, 0, (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
        }

        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(Texture, Position, Source, Color.White,
                0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime, Vector2 velocity, List<Sprite> Platforms)
        {
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Update(GameTime gameTime, Vector2 velocity)
        {
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        //****************
        //END MAIN METHODS
        //****************

        //********************
        //BEGIN HELPER METHODS
        //********************
        Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        public void Logger(String lines)
        {

            // Write the string to a file.append mode is enabled so that the log
            // lines get appended to  test.txt than wiping content and writing the log

            System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test.txt", true);
            file.WriteLine(lines);

            file.Close();

        }
        //******************
        //END HELPER METHODS
        //******************
    }

}
