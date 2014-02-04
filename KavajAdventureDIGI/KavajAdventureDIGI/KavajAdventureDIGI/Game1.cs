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

namespace KavajAdventureDIGI
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        public int CharSpeed = 3;
        public int Numb;
        public int tmp;
        public int CoinC = 0;
        public bool CheckColl;
        public bool CheckFire = true;
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
        Texture2D RedDoodI;
        Vector2 RedDoodPos = new Vector2(300, 300);
        Texture2D CyanDoodI;
        Vector2 CyanDoodPos = new Vector2(400, 350);
        Texture2D BigDoodI;
        Vector2 BigDoodPos = new Vector2(80, 180);
        Texture2D LilDoodI;
        Vector2 LilDoodPos = new Vector2(750, 450);
        Texture2D PowerUpI;
        Vector2 PowerUpPos = new Vector2(100, 100);
        Texture2D Bck;
        Texture2D BckGrndI;
        Texture2D BulletI;
        List<Vector2> BulletPos = new List<Vector2>();
        KeyboardState prevKeyState;
        SpriteFont Font1;
        SpriteFont Font1a1;
        Rectangle RedDoodColl;
        Rectangle PowerUpColl;
        Rectangle CoinColl;
        KeyboardState Ks;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            RedDoodI = Content.Load<Texture2D>("RedDood");
            CyanDoodI = Content.Load<Texture2D>("CyanDood");
            BigDoodI = Content.Load<Texture2D>("BigDood");
            LilDoodI = Content.Load<Texture2D>("LilDood");
            BckGrndI = Content.Load<Texture2D>("Smile");
            BulletI = Content.Load<Texture2D>("Poop");
            PowerUpI = Content.Load<Texture2D>("PowerUp");
            Font1 = Content.Load<SpriteFont>("SpriteFont1");
            Font1a1 = Content.Load<SpriteFont>("FontX");
            Bck = Content.Load<Texture2D>("Arrowz");

            
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            prevKeyState = Ks;
            Ks = Keyboard.GetState();
            if (Ks.IsKeyDown(Keys.LeftShift))
            {
                CharSpeed = 10;
            }
            if (Ks.IsKeyUp(Keys.LeftShift))
            {
                CharSpeed = 3;
            }
            if (Ks.IsKeyDown(Keys.A))
            {
                RedDoodPos.X -= CharSpeed;
                RedDoodColl.X -= CharSpeed;
            }
            if (Ks.IsKeyDown(Keys.D))
            {
                RedDoodPos.X += CharSpeed;
                RedDoodColl.X += CharSpeed;
            }
            if (Ks.IsKeyDown(Keys.W))
            {
                RedDoodPos.Y -= CharSpeed;
                RedDoodColl.Y -= CharSpeed;
            }
            if (Ks.IsKeyDown(Keys.S))
            {
                RedDoodPos.Y += CharSpeed;
                RedDoodColl.Y += CharSpeed;
            }
            if (Ks.IsKeyDown(Keys.Space) && CheckFire)
            {
                BulletPos.Add(new Vector2(RedDoodPos.X + RedDoodI.Width / 3, RedDoodPos.Y + RedDoodI.Width / 3));
                CheckFire = false;
            }
            if (prevKeyState.IsKeyUp(Keys.Space))
            {
                CheckFire = true;
            }
            for (int i = 0; i < BulletPos.Count(); i++)
            {
                BulletPos[i] += new Vector2(0, 5);
                if (BulletPos[i].Y > 480)
                {
                    BulletPos.RemoveAt(i);
                    break;
                }
            }
            if (DetectColl())
            {
                Random Rando = new Random();
                Numb = Rando.Next(1, 5);
                CoinC += 150;
                switch (Numb)
                {
                    case 1:
                        if (tmp.Equals(1))
                        {
                            Numb += 1;
                            RedDoodI = Content.Load<Texture2D>("CyanDood");
                        }
                        else
                        {
                            RedDoodI = Content.Load<Texture2D>("LilDood");
                        }
                        break;
                    case 2:
                        if (tmp.Equals(2))
                        {
                            Numb -= 1;
                            RedDoodI = Content.Load<Texture2D>("LilDood");
                        }
                        else
                        {
                            RedDoodI = Content.Load<Texture2D>("CyanDood");
                        }
                        break;
                    case 3:
                        if (tmp.Equals(3))
                        {
                            Numb += 1;
                            RedDoodI = Content.Load<Texture2D>("RedDood");
                        }
                        else
                        {
                            RedDoodI = Content.Load<Texture2D>("BigDood");
                        }
                        break;
                    case 4:
                        if (tmp.Equals(4))
                        {
                            Numb -= 1;
                            RedDoodI = Content.Load<Texture2D>("BigDood");
                        }
                        else
                        {
                            RedDoodI = Content.Load<Texture2D>("RedDood");
                        }
                        break;
                }
                tmp = Numb;
                int Nom = Rando.Next(0,720);
                PowerUpPos.X = Nom;
                PowerUpColl.X = Nom;
                Nom = Rando.Next(0, 400);
                PowerUpPos.Y = Nom;
                PowerUpColl.Y = Nom;
            }
            else
            {
                
            }
            RedDoodColl = new Rectangle((int)RedDoodPos.X, (int)RedDoodPos.Y, RedDoodI.Width, RedDoodI.Height);
            PowerUpColl = new Rectangle((int)PowerUpPos.X, (int)PowerUpPos.Y, PowerUpI.Width, PowerUpI.Height);
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
            switch (CheckColl)
            {
                case (true):
                    spriteBatch.Draw(BckGrndI, new Vector2(0, 0), Color.White);
                    break;
                case (false):
                    spriteBatch.Draw(Bck, new Vector2(0, 0), Color.White);
                    break;
            }
            for (int i = 0; i < BulletPos.Count(); i++)
            {
                spriteBatch.Draw(BulletI, BulletPos[i], Color.White);
            }
            spriteBatch.Draw(RedDoodI, RedDoodPos, Color.White);
            spriteBatch.Draw(CyanDoodI, CyanDoodPos, Color.White);
            spriteBatch.Draw(BigDoodI, BigDoodPos, Color.White);
            spriteBatch.Draw(LilDoodI, LilDoodPos, Color.White);
            spriteBatch.Draw(PowerUpI, PowerUpPos, Color.White);
            spriteBatch.DrawString(Font1a1, "Points: " + CoinC.ToString(), new Vector2(30, 26), Color.Black);
            spriteBatch.DrawString(Font1, "Points: " + CoinC.ToString(), new Vector2(32, 32), Color.Blue);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Boolean DetectColl()
        {
            if (RedDoodColl.Intersects(PowerUpColl))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
