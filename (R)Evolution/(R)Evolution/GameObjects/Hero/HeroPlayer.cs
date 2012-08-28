using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _R_Evolution.GameMechs;

namespace _R_Evolution.GameObjects.Hero
{
    class HeroPlayer : DrawableGameComponent
    {
        private const int MovementStep = 1;
        private const int StartingPosX = 35, StartingPosY = 33;
        private const int BumpMultiplier = 1;

        private string _playerName;
        private SpriteBatch _spriteBatch;
        private Texture2D _spriteTexture;        
        private SpriteFont PositionFont;
        private Vector2 _currentPosition;

        public Vector2 CurrentPosition
        {
            get { return _currentPosition; }
        }

        internal HeroPlayer(Game game, string playerName)
            :base(game)
        {
            _playerName = playerName;
        }

        public override void Initialize()
        {
            _currentPosition = new Vector2(StartingPosX, StartingPosY);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteTexture = Game.Content.Load<Texture2D>("hero");

            PositionFont = Game.Content.Load<SpriteFont>("SpriteFont");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();            
            _spriteBatch.Draw(_spriteTexture, _currentPosition, Color.White);

            string positionText = string.Format("({0}, {1})", _currentPosition.X, _currentPosition.Y);
            Vector2 FontOrigin = PositionFont.MeasureString(positionText) / 2;
            _spriteBatch.DrawString(PositionFont, positionText, new Vector2(50, 15) , Color.LightGreen, 0, FontOrigin, 0.7f, SpriteEffects.None, 0.5f);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();

            var collisionVerifier = (WallCollisionVerifier)Game.Services.GetService(typeof(WallCollisionVerifier));

            // Allows the game to exit
            if (currentState.GetPressedKeys().Contains(Keys.Left))
            {
                if(collisionVerifier.CanMoveLeft(_currentPosition, _spriteTexture))  _currentPosition.X -= MovementStep;
                else _currentPosition.X += BumpMultiplier * MovementStep;    //bump from wall
            }

            if (currentState.GetPressedKeys().Contains(Keys.Right))
            {
                if(collisionVerifier.CanMoveRight(_currentPosition, _spriteTexture)) _currentPosition.X += MovementStep;
                else _currentPosition.X -= BumpMultiplier * MovementStep;    //bump
            }
            if (currentState.GetPressedKeys().Contains(Keys.Up))
            {
                if(collisionVerifier.CanMoveUp(_currentPosition, _spriteTexture)) _currentPosition.Y -= MovementStep;
                else _currentPosition.Y += BumpMultiplier * MovementStep;
            }
            if (currentState.GetPressedKeys().Contains(Keys.Down))
            { 
                if(collisionVerifier.CanMoveDown(_currentPosition, _spriteTexture)) _currentPosition.Y += MovementStep;
                else _currentPosition.Y -= BumpMultiplier * MovementStep;
            }

            if (currentState.GetPressedKeys().Contains(Keys.Space))
            {
                //place bomb
                var bombDestructor = (BombDestructor)Game.Services.GetService(typeof(BombDestructor));
                bombDestructor.Boooom(_currentPosition);
            }


            base.Update(gameTime);
        }
    }
}
