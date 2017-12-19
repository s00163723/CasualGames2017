using CommonData;
using textInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.AspNet.SignalR.Client;
using System;

namespace MonoGameClient
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont sf;
        string connectionMessage = string.Empty;

        HubConnection serverConnection;
        IHubProxy proxy;

        public bool Connected { get; set; }

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
            Helpers.GraphicsDevice = GraphicsDevice;
            new GetGameInputComponent(this);
            sf = Content.Load<SpriteFont>("keyboardfont");
            // TODO: Add your initialization logic here
             //serverConnection = new HubConnection("http://localhost:15878");
            serverConnection = new HubConnection("http://g-teamcasualgames.azurewebsites.net");
             serverConnection.StateChanged += severConnection_StateChanged;
            proxy = serverConnection.CreateHubProxy("GameHub");
            serverConnection.Start();
            base.Initialize();
        }

        private void severConnection_StateChanged(StateChange State)
        {
            switch (State.NewState)
            {
                case ConnectionState.Connected:
                    connectionMessage = "Connected.....";
                    Connected = true;
                    startGame();
                    break;
                case ConnectionState.Disconnected:
                    connectionMessage = "Disconnected.....";
                    if (State.OldState == ConnectionState.Connected)
                        connectionMessage = "Lost Connection.....";
                    Connected = false;
                    break;
                case ConnectionState.Connecting:
                    connectionMessage = "Connecting.....";
                    Connected = false;
                    break;
            }
        }
        private void startGame()
        {
            proxy.Invoke<PlayerData>("Join")
                .ContinueWith(
                    (p) => { //Do with p
                        if (p.Result == null)
                            connectionMessage = "No Player Data Returned";
                        else
                        {
                            CreatePlayer(p.Result);
                            //create the player
                        }
                    });


        }

        private void CreatePlayer(PlayerData result)
        { }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(sf,connectionMessage,Vector2.Zero,Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
