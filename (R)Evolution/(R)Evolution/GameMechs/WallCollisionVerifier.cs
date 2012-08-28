using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _R_Evolution.GameObjects;
using _R_Evolution.GameObjects.Hero;
using _R_Evolution.GameObjects.Wall;

namespace _R_Evolution.GameMechs
{
    class WallCollisionVerifier
    {
        private readonly List<Wall> _wallCollection;
        private readonly Border _border;

        internal WallCollisionVerifier(List<Wall> wallCollection, Border border)
        {
            _wallCollection = wallCollection;
            _border = border;
        }

        internal bool CanMoveUp(Vector2 currentPosition, Texture2D spriteTexture)
        {
            bool isCollision = IsCollision(
                _border.TopVector.X, _border.TopVector.Y, Border.BorderWidth, Border.BorderThick,
                currentPosition.X, currentPosition.Y, spriteTexture.Width, spriteTexture.Height, MoveDirection.Up);

            return CheckWallCollision(currentPosition, spriteTexture, isCollision, MoveDirection.Up);
        }

        internal bool CanMoveDown(Vector2 currentPosition, Texture2D spriteTexture)
        {
            bool isCollision = IsCollision(
                _border.BottomVector.X, _border.BottomVector.Y, Border.BorderWidth, Border.BorderThick,
                currentPosition.X, currentPosition.Y, spriteTexture.Width, spriteTexture.Height, MoveDirection.Down);

            return CheckWallCollision(currentPosition, spriteTexture, isCollision, MoveDirection.Down);
        }        

        internal bool CanMoveLeft(Vector2 currentPosition, Texture2D spriteTexture)
        {
            bool isCollision = IsCollision(
                _border.LeftVector.X, _border.LeftVector.Y, Border.BorderThick, Border.BorderHeight,
                currentPosition.X, currentPosition.Y, spriteTexture.Width, spriteTexture.Height, MoveDirection.Left);
            
            return CheckWallCollision(currentPosition, spriteTexture, isCollision, MoveDirection.Left);
        }

        internal bool CanMoveRight(Vector2 currentPosition, Texture2D spriteTexture)
        {
            bool isCollision = IsCollision(
                _border.RightVector.X, _border.RightVector.Y, Border.BorderThick, Border.BorderHeight,
                currentPosition.X, currentPosition.Y, spriteTexture.Width, spriteTexture.Height, MoveDirection.Right);
            
            return CheckWallCollision(currentPosition, spriteTexture, isCollision, MoveDirection.Right);
        }

        private bool CheckWallCollision(Vector2 currentPosition, Texture2D spriteTexture, bool isCollision, MoveDirection moveDirection)
        {
            if (isCollision) return false;

            foreach (var wall in _wallCollection)
            {
                if(IsCollision(wall.CurrentPosition.X, wall.CurrentPosition.Y, wall.GetWidth(), wall.GetHeight(), currentPosition.X, currentPosition.Y, spriteTexture.Width, spriteTexture.Height, moveDirection))
                {
                    return false;
                }
            }

            return true;

            //return !isCollision && _wallCollection.All(wall => !IsCollision(wall.CurrentPosition.X, wall.CurrentPosition.Y, wall.GetWidth(), wall.GetHeight(), currentPosition.X, currentPosition.Y, spriteTexture.Width, spriteTexture.Height, moveDirection));
        }

        private bool IsCollision(float x1, float y1, int width1, int height1, float x2, float y2, int width2, int height2, MoveDirection moveDirection)
        {
            if (moveDirection == MoveDirection.Down) y2++;
            else if (moveDirection == MoveDirection.Up) y2--;
            else if (moveDirection == MoveDirection.Left) x2--;
            else if (moveDirection == MoveDirection.Right) x2++;






            Rectangle rectangleA = new Rectangle((int)x1, (int)y1, width1, height1);
            Rectangle rectangleB = new Rectangle((int)x2, (int)y2, width2, height2);

            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            
            
            
            bool result = top <= bottom && left <= right;




            if (result)
            {
                //_R_Evolution.Helpers.Logger.Log(this.ToString(), string.Format("Collision detected: x1:{0},y1:{1},w1:{2},h1:{3},x2:{4},y2:{5},w2:{6},h2:{7}",
                //    x1, y1, width1, height1, x2, y2, width2, height2));
            }

            return result;
        }
    }
}
