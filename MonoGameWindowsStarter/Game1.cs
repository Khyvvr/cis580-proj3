using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteSheet gimp;
        SpriteSheet olive;

        Texture2D background;
        Rectangle mainFrame = new Rectangle(0, 0, 800, 800);

        Texture2D ast;
        Sprite asteroid = new Sprite();


        Player player;
        Olive olive1, olive2, olive3, olive4, olive5;
        List<Barrier> barriers;
        AxisList world;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            barriers = new List<Barrier>();
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
#if VISUAL_DEBUG
            VisualDebugging.LoadContent(Content);
#endif
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("background");
            ast = Content.Load<Texture2D>("asteriodbarrier");
            var gTexture = Content.Load<Texture2D>("gimpMasterSchwag");
            var oTexture = Content.Load<Texture2D>("evilOlive");

            gimp = new SpriteSheet(gTexture, 87, 87, 8, 17);
            olive = new SpriteSheet(oTexture, 87, 87, 20, 25);

            var gimpFrames = from index in Enumerable.Range(0, 2) select gimp[index];
            var oliveFrames = from index in Enumerable.Range(0, 2) select olive[index];

            player = new Player(gimpFrames);
            olive1 = new Olive(oliveFrames);
            olive2 = new Olive(oliveFrames);
            olive3 = new Olive(oliveFrames);
            olive4 = new Olive(oliveFrames);
            olive5 = new Olive(oliveFrames);

            //create asteroid barrier here:
            barriers.Add(new Barrier(new BoundingRectangle(200, 75, 400, 125), asteroid));      
            barriers.Add(new Barrier(new BoundingRectangle(50, 200, 50, 400), asteroid));
            barriers.Add(new Barrier(new BoundingRectangle(700, 200, 50, 400), asteroid));
            barriers.Add(new Barrier(new BoundingRectangle(350, 300, 100, 200), asteroid));
            barriers.Add(new Barrier(new BoundingRectangle(200, 600, 400, 125), asteroid));

            world = new AxisList();
            foreach (Barrier barrier in barriers)
            {
                world.AddGameObject(barrier);
            }
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
            olive1.Update(gameTime);
            olive2.Update(gameTime);
            olive3.Update(gameTime);
            olive4.Update(gameTime);
            olive5.Update(gameTime);

            var barrierQuery = world.QueryRange(player.Bounds.X, player.Bounds.X + player.Bounds.Width);
            player.CheckForBarrierCollision(barrierQuery);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background, mainFrame, Color.Transparent);
            spriteBatch.End();

            spriteBatch.Begin();

            barriers.ForEach(barriers =>
            {
                barriers.Draw(spriteBatch);
            });

            player.Draw(spriteBatch);

            olive1.Draw(spriteBatch);
            olive2.Draw(spriteBatch);
            olive3.Draw(spriteBatch);
            olive4.Draw(spriteBatch);
            olive5.Draw(spriteBatch);

            for(var i = 0; i <= 2; i++)
            {
                gimp[i].Draw(spriteBatch, new Vector2(i * 87, 87), Color.Transparent);
                olive[i].Draw(spriteBatch, new Vector2(i * 87, 87), Color.Transparent);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
