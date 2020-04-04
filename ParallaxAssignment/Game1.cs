using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ParallaxAssignment
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        private SpriteFont font;
        private int lives = 3;

        public Game1()
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
            // TODO: Add your initialization logic here

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

            //font = Content.Load<SpriteFont>("basicFont");

            // TODO: use this.Content to load your game content here
            var spritesheet = Content.Load<Texture2D>("BatsFlipped");
            player = new Player(spritesheet);

            var backgroundTexture = Content.Load<Texture2D>("BackgroundLayer");
            var backgroundSprite = new StaticSprite(backgroundTexture);
            var backgroundLayer = new ParallaxLayer(this);
            backgroundLayer.Sprites.Add(backgroundSprite);
            backgroundLayer.DrawOrder = 1;
            Components.Add(backgroundLayer);

            var playerLayer = new ParallaxLayer(this);
            playerLayer.Sprites.Add(player);
            playerLayer.DrawOrder = 3;
            Components.Add(playerLayer);

            //Creating the Midground
            var midgroundTextures = Content.Load<Texture2D>("MidgroundLayer");
            var midgroundSprite = new StaticSprite(midgroundTextures, new Vector2(0, 0));
            var midgroundLayer = new ParallaxLayer(this);
            midgroundLayer.Sprites.Add(midgroundSprite);
            midgroundLayer.DrawOrder = 2;
            Components.Add(midgroundLayer);

            var testing = Content.Load<Texture2D>("test");
            var testingSprites = new StaticSprite(testing, new Vector2(0, 90));
            var testingLayer = new ParallaxLayer(this);
            testingLayer.Sprites.Add(testingSprites);
            testingLayer.DrawOrder = 5;

            font = Content.Load<SpriteFont>("basicFont");
            var fontSprite = new StaticSprite(font, new Vector2(0, 0));
            var fontLayer = new ParallaxLayer(this);

            Components.Add(testingLayer);



            backgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.1f);
            midgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.4f);
            playerLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            testingLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);



        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Current Lives: " + lives, new Vector2(0, 50), Color.Black, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 6);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
