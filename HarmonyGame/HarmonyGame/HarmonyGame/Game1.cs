using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HarmonyGame
{
    //****************
    //BEGIN GAME LOGIC
    //****************
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        SpriteHandler SH;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Wizard mWizard;

        MainFloor mFloor1;
        MainFloor mFloor2;

        List<Sprite> bgList;
        List<MainFloor> platforms;
        List<Fireball> Fireballs;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SH = new SpriteHandler();
            bgList = new List<Sprite>();
            platforms = new List<MainFloor>();
            Fireballs = new List<Fireball>();

            mFloor1 = new MainFloor(new Vector2(0, 445), new Vector2(0, 1), false);
            mFloor2 = new MainFloor(new Vector2(175, 400), new Vector2(0, 1), false);

            mWizard = new Wizard();

            platforms.Add(mFloor1);
            platforms.Add(mFloor2);

            //Initialize all background objects
            for (int i = 0; i < 5; i++)
            {
                bgList.Add(new Sprite());
                bgList[i].mScale = 1.7f;
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mWizard.LoadContent(this.Content);
            mWizard.Scale = 3.0f;

            foreach (Fireball f in mWizard.mFireballs)
            {
                f.LoadContent(this.Content, f.AssetName);
            }

            int i = 0;
            foreach (Sprite bg in bgList)
            {
                bg.LoadContent(this.Content, "Background0" + (i+1));
                bg.Position = new Vector2(bg.Size.Width * i ,0);
            }

            mFloor1.LoadContent(this.Content);
            mFloor1.ResolutionX = mFloor1.SpriteTexture.Width;
            mFloor1.ResolutionY = mFloor1.SpriteTexture.Height;

            mFloor2.LoadContent(this.Content);
            mFloor2.ResolutionX = 300;
            mFloor2.ResolutionY = 3;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            foreach (MainFloor f in platforms)
            {
                f.Update(gameTime);
            }
            updateProjectiles(gameTime, Fireballs);
            moveBackground(gameTime, bgList);
            mWizard.Update(gameTime, platforms);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Sprite i in bgList)
            {
                i.Draw(spriteBatch);
            }

            foreach (Fireball f in Fireballs)
            {
                f.Draw(spriteBatch);
            }

            foreach (MainFloor f in platforms)
            {
                f.Draw(spriteBatch);
            }

            mWizard.Draw(spriteBatch);
  
            spriteBatch.End();

            base.Draw(gameTime);
        }
        //**************
        //END GAME LOGIC
        //**************

        //********************
        //BEGIN HELPER METHODS
        //********************
        private void updateProjectiles(GameTime gameTime, List<Fireball> p)
        {
            foreach (Fireball fireball in p)
            {
                fireball.Update(gameTime);
            }
        }
        public void moveBackground(GameTime gameTime, List<Sprite> l)
        {
            Vector2 speed = new Vector2(80, 0);
            Vector2 direction = new Vector2(-1, 0);

            if (l[0].Position.X < -l[0].Size.Width)
            {
                l[0].Position.X = l[4].Position.X + l[4].Size.Width;
            }

            if (l[1].Position.X < -l[1].Size.Width)
            {
                l[1].Position.X = l[0].Position.X + l[0].Size.Width;
            }

            if (l[2].Position.X < -l[2].Size.Width)
            {
                l[2].Position.X = l[1].Position.X + l[1].Size.Width;
            }

            if (l[3].Position.X < -l[3].Size.Width)
            {
                l[3].Position.X = l[2].Position.X + l[2].Size.Width;
            }

            if (l[4].Position.X < -l[4].Size.Width)
            {
                l[4].Position.X = l[3].Position.X + l[3].Size.Width;
            }

            l[0].Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            l[1].Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            l[2].Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            l[3].Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            l[4].Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        //******************
        //END HELPER METHODS
        //******************
    }
}
