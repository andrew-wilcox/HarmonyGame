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
        private Vector2 mVelocity;
        private Vector2 mPosition;
        private int currentPointIndex;

        ContentManager mContentManager;

        public MovingPlatform(Vector2 facingVector, bool thinBool, List<Vector2> points, Vector2 vel)
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

            Vector2 delta = Position - pathPoints[currentPointIndex + 1];

            var magnitude = Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);

            Vector2 unitDelta = new Vector2((float) (delta.X / magnitude), (float) (delta.Y / magnitude));

            mVelocity *= unitDelta;

            Position += mVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //base.Update(gameTime, mVelocity);
        }

        private void CheckNewPoint()
        {
            if (mVelocity.X > 0)
            {
                if (mPosition.X > pathPoints[currentPointIndex + 1].X)
                {
                    currentPointIndex++;
                    mPosition.X = pathPoints[currentPointIndex].X;
                }
            }
            else
            {
                if (mPosition.X < pathPoints[currentPointIndex + 1].X)
                {
                    currentPointIndex++;
                    mPosition.X = pathPoints[currentPointIndex].X;
                }
            }

            if (mVelocity.Y > 0)
            {
                if (mPosition.Y > pathPoints[currentPointIndex + 1].Y)
                {
                    currentPointIndex++;
                    mPosition.Y = pathPoints[currentPointIndex].Y;
                }
            }
            else
            {
                if (mPosition.Y < pathPoints[currentPointIndex + 1].Y)
                {
                    currentPointIndex++;
                    mPosition.Y = pathPoints[currentPointIndex].Y;
                }
            }
            if (currentPointIndex == pathPoints.Count-1)
            {
                currentPointIndex = 0;
            }
        }
    }
}
