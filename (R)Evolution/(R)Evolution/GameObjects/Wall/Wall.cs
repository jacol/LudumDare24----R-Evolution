using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _R_Evolution.GameObjects.Wall
{
    class Wall : DrawableGameComponent
    {
        public const int Width = 3, Length = 25;
        private static readonly Color Color = Microsoft.Xna.Framework.Color.YellowGreen;

        private SpriteBatch _spriteBatch;
        private Texture2D _spriteTexture;        
        private WallOrientation _orientation;        

        public Vector2 CurrentPosition { get; set; }

        internal Wall(Game game, int posX, int posY, WallOrientation orientation)
            :base(game)
        {
            CurrentPosition = new Vector2(posX, posY);
            _orientation = orientation;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            RecalculateArea();

            base.LoadContent();
        }        

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spriteTexture, CurrentPosition, Color);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ChangeOrientation()
        {
            if(_orientation == WallOrientation.Horizontal) _orientation = WallOrientation.Vertical;
            else _orientation = WallOrientation.Horizontal;
        }

        public void RecalculateArea()
        {
            Color[] data = new Color[Width * Length];
            if (_orientation == WallOrientation.Horizontal)
            {
                _spriteTexture = new Texture2D(GraphicsDevice, Length, Width);
            }
            else
            {
                _spriteTexture = new Texture2D(GraphicsDevice, Width, Length);
            }

            for (int i = 0; i < data.Length; ++i) data[i] = Color;
            _spriteTexture.SetData(data);
        }

        public int GetWidth()
        {
            return _orientation == WallOrientation.Horizontal ? Length : Width;
        }

        public int GetHeight()
        {
            return _orientation == WallOrientation.Horizontal ? Width : Length;
        }
    }
}
