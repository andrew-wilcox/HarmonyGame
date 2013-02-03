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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Wizard mWizard;

        Sprite mFloor;
        Sprite mFloor2;

        List<Sprite> bgList;
        List<Sprite> platforms;

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
            bgList = new List<Sprite>();
            platforms = new List<Sprite>();

            mFloor = new Sprite();
            mFloor2 = new Sprite();

            mWizard = new Wizard();
            mFloor = new Sprite();

            platforms.Add(mFloor);
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
            mWizard.Scale = 2.5f;

            bgList[0].LoadContent(this.Content, "Background01");
            bgList[0].Position = new Vector2(0, 0);

            bgList[1].LoadContent(this.Content, "Background02");
            bgList[1].Position = new Vector2(bgList[0].Position.X + bgList[0].Size.Width, 0);

            bgList[2].LoadContent(this.Content, "Background03");
            bgList[2].Position = new Vector2(bgList[1].Position.X + bgList[1].Size.Width, 0);

            bgList[3].LoadContent(this.Content, "Background04");
            bgList[3].Position = new Vector2(bgList[2].Position.X + bgList[2].Size.Width, 0);

            bgList[4].LoadContent(this.Content, "Background05");
            bgList[4].Position = new Vector2(bgList[3].Position.X + bgList[3].Size.Width, 0);

            mFloor.LoadContent(this.Content, "floor");
            mFloor.Position = new Vector2(0, 445);

            mFloor2.LoadContent(this.Content, "floor");
            mFloor2.Position = new Vector2(175, 445);
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
            // Allows the game to exit
            mFloor.Update(gameTime, Vector2.Zero, Vector2.Zero);
            mFloor2.Update(gameTime, Vector2.Zero, Vector2.Zero);
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

            mFloor.Draw(spriteBatch);
            mFloor2.Draw(spriteBatch);

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
