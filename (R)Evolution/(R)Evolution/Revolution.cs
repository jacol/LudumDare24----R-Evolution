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
using _R_Evolution.GameMechs;
using _R_Evolution.GameObjects;
using _R_Evolution.GameObjects.Hero;
using _R_Evolution.GameObjects.Wall;

namespace _R_Evolution
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Revolution : Microsoft.Xna.Framework.Game
    {
        private const int RevolutionInterval = 5;

        private TimeSpan _lastRevolutionTime;
        private List<Wall> _wallCollection;
        


        public Revolution()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Labirynth (R)Evolution v0.1";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //main hero
            HeroPlayer player = new HeroPlayer(this, "Jacek placek");
            this.Components.Add(player);

            //build walls
            _wallCollection = LabFactory.Create(this);
            foreach (var wall in _wallCollection)
            {
                this.Components.Add(wall);
            }
            _lastRevolutionTime = new TimeSpan(0, 0, 0, 0);

            //main border
            Border border = new Border(this);
            this.Components.Add(border);

            //monsters
            Monster polaris1 = new Monster(this, MonsterType.Polaris, 155, 305, player);
            this.Components.Add(polaris1);
            Monster polaris2 = new Monster(this, MonsterType.Polaris, 305, 155, player);
            this.Components.Add(polaris2);


            //wall collisiona mechanism
            this.Services.AddService(typeof(WallCollisionVerifier), new WallCollisionVerifier(_wallCollection, border));

            //bomb desctructor
            this.Services.AddService(typeof(BombDestructor), new BombDestructor(_wallCollection, this));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            new SpriteBatch(GraphicsDevice);            


            // TODO: use this.Content to load your game content here
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
            KeyboardState currentState = Keyboard.GetState();

            // Allows the game to exit
            if (currentState.GetPressedKeys().Contains(Keys.Escape))
                this.Exit();
            
            if(gameTime.TotalGameTime.Subtract(_lastRevolutionTime).Seconds > RevolutionInterval)
            {
                LabRevolutionMaker.Rebuild(_wallCollection);
                _lastRevolutionTime = gameTime.TotalGameTime;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            base.Draw(gameTime);

        }
    }
}
