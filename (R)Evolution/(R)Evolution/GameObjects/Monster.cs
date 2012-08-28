using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _R_Evolution.GameMechs;
using _R_Evolution.GameObjects.Hero;

namespace _R_Evolution.GameObjects
{
    class Monster : DrawableGameComponent
    {
        private const float MovementStep = 0.6f;

        private SpriteBatch _spriteBatch;
        private Texture2D _spriteTexture;
        private Vector2 _currentPosition;

        private readonly MonsterType _monsterType;
        private readonly int _startX;
        private readonly int _startY;
        private readonly HeroPlayer _target;

        internal Monster(Game game, MonsterType monsterType, int startX, int startY, HeroPlayer target)
            :base(game)
        {
            _monsterType = monsterType;
            _startX = startX;
            _startY = startY;
            _target = target;
        }

        public override void Initialize()
        {
            _currentPosition = new Vector2(_startX, _startY);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            if(_monsterType == MonsterType.Polaris) _spriteTexture = Game.Content.Load<Texture2D>("monster_polaris");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spriteTexture, _currentPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            var collisionVerifier = (WallCollisionVerifier)Game.Services.GetService(typeof(WallCollisionVerifier));

            MoveDirection moveLeftRight = WhereToGoLeftRight(collisionVerifier);            

            if (moveLeftRight == MoveDirection.Left) _currentPosition.X -= MovementStep;
            else if (moveLeftRight == MoveDirection.Right) _currentPosition.X += MovementStep;

            MoveDirection moveUpDown = WhereToGoUpDown(collisionVerifier);

            if (moveUpDown == MoveDirection.Up) _currentPosition.Y -= MovementStep;
            else if (moveUpDown == MoveDirection.Down) _currentPosition.Y += MovementStep;

            if (Math.Abs(_target.CurrentPosition.X - _currentPosition.X) < 10 && Math.Abs(_target.CurrentPosition.Y - _currentPosition.Y) < 10)
            {
                Game.Content.Load<SoundEffect>("dead").Play();
                Game.Exit();
            }


            base.Update(gameTime);
        }

        private MoveDirection WhereToGoLeftRight(WallCollisionVerifier collisionVerifier)
        {
            if(_target.CurrentPosition.X < _currentPosition.X && collisionVerifier.CanMoveLeft(_currentPosition, _spriteTexture)) return MoveDirection.Left;
            else if (_target.CurrentPosition.X > _currentPosition.X && collisionVerifier.CanMoveRight(_currentPosition, _spriteTexture)) return MoveDirection.Right;

            return RandomMove(collisionVerifier);
        }        

        private MoveDirection WhereToGoUpDown(WallCollisionVerifier collisionVerifier)
        {
            if (_target.CurrentPosition.Y > _currentPosition.Y && collisionVerifier.CanMoveDown(_currentPosition, _spriteTexture)) return MoveDirection.Down;
            else if (_target.CurrentPosition.Y < _currentPosition.Y && collisionVerifier.CanMoveUp(_currentPosition, _spriteTexture)) return MoveDirection.Up;

            return RandomMove(collisionVerifier);
        }

        private MoveDirection RandomMove(WallCollisionVerifier collisionVerifier)
        {
            if(DateTime.Now.Ticks % 5 == 0 && collisionVerifier.CanMoveUp(_currentPosition, _spriteTexture)) return MoveDirection.Up;
            if (DateTime.Now.Ticks % 4 == 0 && collisionVerifier.CanMoveDown(_currentPosition, _spriteTexture)) return MoveDirection.Down;
            if (DateTime.Now.Ticks % 3 == 0 && collisionVerifier.CanMoveLeft(_currentPosition, _spriteTexture)) return MoveDirection.Left;
            if (collisionVerifier.CanMoveRight(_currentPosition, _spriteTexture)) return MoveDirection.Right;

            return RandomMove(collisionVerifier);
        }
    }
}
