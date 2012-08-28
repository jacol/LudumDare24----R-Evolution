using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using _R_Evolution.GameObjects;
using _R_Evolution.GameObjects.Wall;

namespace _R_Evolution.GameMechs
{
    class BombDestructor
    {
        private const int DestructionRadius = 20;

        private readonly List<Wall> _wallCollection;
        private readonly Game _game;

        internal BombDestructor(List<Wall> wallCollection, Game game)
        {
            _wallCollection = wallCollection;
            _game = game;
        }

        public void Boooom(Vector2 location)
        {
            _game.Content.Load<SoundEffect>("bomb").Play();

            var toDestroy = _wallCollection.Where(w => Math.Abs((w.CurrentPosition.X + 4 - location.X)) < DestructionRadius &&
                                                       Math.Abs((w.CurrentPosition.Y + 4 - location.Y)) < DestructionRadius);
            foreach (var toD in toDestroy)
                _game.Components.Remove(toD);



            _wallCollection.RemoveAll(w => Math.Abs((w.CurrentPosition.X + 4 - location.X)) < DestructionRadius &&
                                            Math.Abs((w.CurrentPosition.Y + 4 - location.Y)) < DestructionRadius);        
            
        }

    }
}
