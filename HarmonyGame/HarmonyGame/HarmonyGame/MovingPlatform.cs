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
    class MovingPlatform : Wall
    {
        const string ASSETNAME = "movingPlatform";

        private List<Vector2> pathPoints;
        private int mVelocity;
        private Vector2 currentVelocity;
        private Vector2 mPosition;
        private int currentPointIndex;

        ContentManager mContentManager;

        public MovingPlatform(Vector2 facingVector, bool thinBool, List<Vector2> points, int vel)
        {
            facing = facingVector;
            thin = thinBool;
            pathPoints = points;
            mVelocity = vel;
            Position = pathPoints[0];
            currentPointIndex = 0;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;

            base.LoadContent(theContentManager, ASSETNAME);
            Source = new Rectangle(0, 0, Source.Width, Source.Height);
        }

        public void Update(GameTime gameTime)
        {
            CheckNewPoint();

            Vector2 delta = -(Position - pathPoints[currentPointIndex + 1]);

            var magnitude = Math.Sqrt((delta.X * delta.X) + (delta.Y * delta.Y));

            Vector2 unitDelta = new Vector2((float) (delta.X / magnitude), (float) (delta.Y / magnitude));

            if ((float)gameTime.ElapsedGameTime.TotalSeconds > 0)
            {
                currentVelocity = mVelocity * unitDelta * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position += currentVelocity;
            }

            base.Update(gameTime, currentVelocity);
        }

        private void CheckNewPoint()
        {
            if (currentVelocity.X > 0 && mPosition.X != pathPoints[currentPointIndex + 1].X)
            {
                if (mPosition.X >= pathPoints[currentPointIndex + 1].X)
                {
                    currentPointIndex++;
                    mPosition.X = pathPoints[currentPointIndex].X;
                    mPosition.Y = pathPoints[currentPointIndex].Y;
                    currentVelocity = Vector2.Zero;
                }
            }
            else if (currentVelocity.X < 0 && mPosition.X != pathPoints[currentPointIndex + 1].X)
            {
                if (mPosition.X < pathPoints[currentPointIndex + 1].X)
                {
                    currentPointIndex++;
                    mPosition.X = pathPoints[currentPointIndex].X;
                    mPosition.Y = pathPoints[currentPointIndex].Y;
                    currentVelocity = Vector2.Zero;
                }
            }

            else if (currentVelocity.Y > 0 && mPosition.Y != pathPoints[currentPointIndex + 1].Y)
            {
                if (mPosition.Y >= pathPoints[currentPointIndex + 1].Y)
                {
                    currentPointIndex++;
                    mPosition.X = pathPoints[currentPointIndex].X;
                    mPosition.Y = pathPoints[currentPointIndex].Y;
                    currentVelocity = Vector2.Zero;
                }
            }
            else if (currentVelocity.Y < 0 && mPosition.Y != pathPoints[currentPointIndex + 1].Y)
            {
                if (mPosition.Y < pathPoints[currentPointIndex + 1].Y)
                {
                    currentPointIndex++;
                    mPosition.X = pathPoints[currentPointIndex].X;
                    mPosition.Y = pathPoints[currentPointIndex].Y;
                    currentVelocity = Vector2.Zero;
                }
            }

            if (currentPointIndex == pathPoints.Count - 1)
            {
                currentPointIndex = -1;
            }
        }
    }
}
