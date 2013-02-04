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
    class Wizard : Sprite
    {
        const string WIZARD_ASSETNAME = "wizardSheet";
        const int MAX_JUMP_HEIGHT = 150;
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int WIZARD_SPEED = 160;
        const int JUMP_SPEED = 500;
        const float GRAVITY = 25f;

        enum State
        {
            Walking,
            Ducking,
            Jumping
        }

        State mCurrentState = State.Walking;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mVelocity = Vector2.Zero;
        Vector2 mStartingPosition = Vector2.Zero;

        KeyboardState mPreviousKeyboardState;

        public List<Fireball> mFireballs = new List<Fireball>();

        ContentManager mContentManager;

        Sprite onFloorSprite;

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;

            foreach (Fireball aFireball in mFireballs)
            {
                aFireball.LoadContent(mContentManager);
            }

            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, WIZARD_ASSETNAME);
            Source = new Rectangle(0, 0, 13, Source.Height);
        }

        public void Update(GameTime gameTime, List<Sprite> platforms)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            //Check collision against all floors
            ManageFloorCollisions(platforms);

            UpdateMovement(aCurrentKeyboardState, gameTime);
            UpdateDuck(aCurrentKeyboardState);
            UpdateFireball(gameTime, aCurrentKeyboardState);

            mPreviousKeyboardState = aCurrentKeyboardState;

            base.Update(gameTime, mVelocity);
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (Fireball aFireball in mFireballs)
            {
                aFireball.Draw(theSpriteBatch);
            }
            base.Draw(theSpriteBatch);
        }

        private void UpdateFireball(GameTime gameTime, KeyboardState aCurrentKeyboardState)
        {
            foreach (Fireball aFireball in mFireballs)
            {
                aFireball.Update(gameTime);
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.RightControl) && !mPreviousKeyboardState.IsKeyDown(Keys.RightControl))
            {
                LaunchFireball();
            }
        }

        private void LaunchFireball()
        {
            if (mCurrentState == State.Walking)
            {
                bool aCreateNew = true;
                foreach (Fireball aFireball in mFireballs)
                {
                    if (!aFireball.Visible)
                    {
                        aCreateNew = false;
                        aFireball.Launch(Position + new Vector2(Size.Width / 2, Size.Height / 2));
                        break;
                    }
                }

                if (aCreateNew)
                {
                    Fireball aFireball = new Fireball(this);
                    aFireball.LoadContent(mContentManager);
                    aFireball.Launch(Position + new Vector2(Size.Width / 2, Size.Height / 2));
                    mFireballs.Add(aFireball);
                }
            }
        }

        private void UpdateDuck(KeyboardState aCurrentKeyboardState)
        {
            if (aCurrentKeyboardState.IsKeyDown(Keys.RightShift))
            {
                Duck();
            }
            else
            {
                StopDucking();
            }
        }

        private void Duck()
        {
            if (mCurrentState == State.Walking)
            {
                mCurrentState = State.Ducking;
                mDirection = mVelocity = Vector2.Zero;

                Source = new Rectangle(13, 0, 13, Source.Height);
            }
        }

        private void StopDucking()
        {
            if (mCurrentState == State.Ducking)
            {
                mCurrentState = State.Walking;

                Source = new Rectangle(0, 0, 13, Source.Height);
            }
        }

        private void Jump(GameTime gameTime)
        {
            if (mCurrentState != State.Jumping)
            {
                onFloor = false;

                mCurrentState = State.Jumping;
                mVelocity.Y = -JUMP_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState, GameTime gameTime)
        {
            ManageInput(aCurrentKeyboardState, gameTime);

            UpdateGravity(gameTime);

            if (onFloor)
                ManageFloorContact();

            Position += mVelocity;
        }

        public void ManageInput(KeyboardState aCurrentKeyboardState, GameTime gameTime)
        {
            if (aCurrentKeyboardState.IsKeyDown(Keys.Left))
            {
                mVelocity.X = -WIZARD_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            else if (aCurrentKeyboardState.IsKeyDown(Keys.Right))
            {
                mVelocity.X = WIZARD_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
                mVelocity.X = 0;

            if (aCurrentKeyboardState.IsKeyDown(Keys.Space))
                Jump(gameTime);
        }

        public void ManageFloorContact()
        {
            if (onFloorSprite != null)
            {
                if (this.Position.X > onFloorSprite.Position.X + onFloorSprite.SpriteTexture.Width)
                {
                    onFloor = false;
                    onFloorSprite = null;
                }

                else if (this.Position.X < onFloorSprite.Position.X)
                {
                    onFloor = false;
                    onFloorSprite = null;
                }
            }
        }

        public void UpdateGravity(GameTime gameTime)
        {
            if (!onFloor)
            {
                mVelocity.Y += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void ManageFloorCollisions(List<Sprite> platforms)
        {
            if (!onFloor)
            {
                foreach (Sprite s in platforms)
                {
                    if (DumbCollides(this, s))
                    {
                        onFloorSprite = s;
                        mCurrentState = State.Walking;
                        Position.Y = s.Position.Y - (int)(this.SpriteTexture.Height * this.Scale);
                        onFloor = true;
                        mVelocity.Y = 0;
                        break;
                    }
                }
            }
        }

        public bool DumbCollides(Sprite sprite1, Sprite sprite2)
        {
            if (sprite1.Bounds.Intersects(sprite2.Bounds))
            {
                return true;
            }

            return false;
        }
       
    }
}
