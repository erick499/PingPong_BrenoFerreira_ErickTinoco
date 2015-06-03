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
using PingPong.Classes;

namespace PingPong
{
  
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenW;
        public static int ScreenH;
        private int SpeedP = 5;
        const int Offset = 70;
        private double deltaTime;
        private int Count = 3;
        const float Speed = 8f;
        private bool Go = false;
        private string Scenes = "Menu";

        Player Jogador1;
        Player Jogador2;
        GameObject TelaInicial;
        GameObject Fundo;
        GameObject Meio;
        SoundEffect WallS;
        SoundEffect PlayerS;
        Ball Bola;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        { 
            ScreenW = GraphicsDevice.Viewport.Width;
            ScreenH = GraphicsDevice.Viewport.Height;
            TelaInicial = new GameObject();
            Fundo = new GameObject();
            Meio = new GameObject();
            Jogador1 = new Player();
            Jogador1.cor = Color.Orange;
            Jogador2 = new Player();
            Jogador2.cor = Color.Lime;
            Bola = new Ball();
            Bola.cor = Color.Red;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Content/Fonte1");
            Jogador1.Texture = Content.Load<Texture2D>("Content/Paddle");
            Jogador2.Texture = Content.Load<Texture2D>("Content/Paddle");
            TelaInicial.Texture = Content.Load<Texture2D>("Content/StartScreen");
            Fundo.Texture = Content.Load<Texture2D>("Content/Background");
            Meio.Texture = Content.Load<Texture2D>("Content/Middle");
            Bola.Texture = Content.Load<Texture2D>("Content/Ball");
            WallS = Content.Load<SoundEffect>("Content/BallWallCollision");
            PlayerS = Content.Load<SoundEffect>("Content/PaddleBallCollision");
            TelaInicial.Position = new Vector2(0, 0);
            Fundo.Position = new Vector2(0, 0);
            Meio.Position = new Vector2(ScreenW/2, 0);
            Jogador1.Position = new Vector2(Offset, ScreenH / 2 - Jogador1.Texture.Height / 2);
            Jogador2.Position = new Vector2(ScreenW - Jogador2.Texture.Width - Offset, ScreenH / 2 - Jogador2.Texture.Height / 2);
            Bola.Launch(Speed);
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            switch (Scenes)
            {
                case "Menu":

                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        Scenes = "Multiplayer";
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Scenes = "Single Player";
                    }
                    break;
                case "Multiplayer":
                    deltaTime += gameTime.ElapsedGameTime.TotalSeconds;
                    if (deltaTime > 1 && Count > 0 && !Go)
                    {
                        deltaTime = 0;
                        Count -= 1;
                    }
                    else if(Count <= 0)
                    {
                        Go = true;
                    }
                    ScreenW = GraphicsDevice.Viewport.Width;
                    ScreenH = GraphicsDevice.Viewport.Height;
                    if (Go)
                    {
                        Bola.Move(Bola.Velocity);
                        if (Keyboard.GetState().IsKeyDown(Keys.S) && Jogador1.Position.Y < ScreenH - Jogador1.Texture.Height)
                        {
                            Jogador1.Position.Y += SpeedP;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.W) && Jogador1.Position.Y > 0)
                        {
                            Jogador1.Position.Y -= SpeedP;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down) && Jogador2.Position.Y < ScreenH - Jogador2.Texture.Height)
                        {
                            Jogador2.Position.Y += SpeedP;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Up) && Jogador2.Position.Y > 0)
                        {
                            Jogador2.Position.Y -= SpeedP;
                        }
                        if (Bola.Position.X >= ScreenW - Bola.Texture.Width || Bola.Position.X <= 0)
                        {
                            Go = false;
                            Count = 3;
                            deltaTime = 0;
                            Bola.cor = Color.Red;
                            Bola.Launch(Speed);
                        }
                        else if (Bola.Position.Y > ScreenH - Bola.Texture.Height || Bola.Position.Y < 0)
                        {
                            WallS.Play();
                            Bola.Velocity.Y *= -1;
                        }
                        else if (Bola.Position.X <= Jogador1.Position.X + Jogador1.Texture.Width && Bola.Position.Y >= Jogador1.Position.Y && Bola.Position.Y <= Jogador1.Position.Y + Jogador1.Texture.Height && Bola.Velocity.X < 0 && Bola.Position.X > Jogador1.Position.X)
                        {
                            PlayerS.Play();
                            Bola.Velocity.X *= -1;
                            Bola.cor = Color.Orange;
                        }
                        else if (Bola.Position.X >= Jogador2.Position.X - Jogador2.Texture.Width && Bola.Position.Y >= Jogador2.Position.Y && Bola.Position.Y <= Jogador2.Position.Y + Jogador2.Texture.Height && Bola.Velocity.X > 0 && Bola.Position.X < Jogador2.Position.X + Jogador1.Texture.Width)
                        {
                            PlayerS.Play();
                            Bola.Velocity.X *= -1;
                            Bola.cor = Color.Lime;
                        }
                    }
                    break;
                case "Single Player":
                    deltaTime += gameTime.ElapsedGameTime.TotalSeconds;
                    if (deltaTime > 1 && Count > 0 && !Go)
                    {
                        deltaTime = 0;
                        Count -= 1;
                    }
                    else if(Count <= 0)
                    {
                        Go = true;
                    }
                    ScreenW = GraphicsDevice.Viewport.Width;
                    ScreenH = GraphicsDevice.Viewport.Height;
                    if (Go)
                    {
                        Bola.Move(Bola.Velocity);
                        Jogador2.Position.Y = Bola.Position.Y / 2;
                        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down) && Jogador1.Position.Y < ScreenH - Jogador1.Texture.Height)
                        {
                            Jogador1.Position.Y += SpeedP;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) && Jogador1.Position.Y > 0)
                        {
                            Jogador1.Position.Y -= SpeedP;
                        }
                        if (Bola.Position.X >= ScreenW - Bola.Texture.Width || Bola.Position.X <= 0)
                        {
                            Go = false;
                            Count = 3;
                            deltaTime = 0;
                            Bola.cor = Color.Red;
                            Bola.Launch(Speed);
                        }
                        else if (Bola.Position.Y > ScreenH - Bola.Texture.Height || Bola.Position.Y < 0)
                        {
                            WallS.Play();
                            Bola.Velocity.Y *= -1;
                        }
                        else if (Bola.Position.X <= Jogador1.Position.X + Jogador1.Texture.Width && Bola.Position.Y >= Jogador1.Position.Y && Bola.Position.Y <= Jogador1.Position.Y + Jogador1.Texture.Height && Bola.Velocity.X < 0 && Bola.Position.X > Jogador1.Position.X)
                        {
                            PlayerS.Play();
                            Bola.Velocity.X *= -1;
                            Bola.cor = Color.Orange;
                        }
                        else if (Bola.Position.X >= Jogador2.Position.X - Jogador2.Texture.Width && Bola.Position.Y >= Jogador2.Position.Y && Bola.Position.Y <= Jogador2.Position.Y + Jogador2.Texture.Height && Bola.Velocity.X > 0 && Bola.Position.X < Jogador2.Position.X + Jogador1.Texture.Width)
                        {
                            PlayerS.Play();
                            Bola.Velocity.X *= -1;
                            Bola.cor = Color.Lime;
                        }
                    }
                    break;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (Scenes)
            {
                case "Menu":
                    TelaInicial.Draw(spriteBatch);
                    break;
                case "Multiplayer":
                    Fundo.Draw(spriteBatch);
                    Meio.Draw(spriteBatch);
                    Jogador1.Draw(spriteBatch);
                    Jogador2.Draw(spriteBatch);
                    if (Go)
                    {
                        Bola.Draw(spriteBatch);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "" + Count, new Vector2(ScreenW / 2 - 15, ScreenH / 2 - 50), Color.Red);
                    }
                    break;
                case "Single Player":
                    Fundo.Draw(spriteBatch);
                    Meio.Draw(spriteBatch);
                    Jogador1.Draw(spriteBatch);
                    Jogador2.Draw(spriteBatch);
                    if (Go)
                    {
                        Bola.Draw(spriteBatch);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "" + Count, new Vector2(ScreenW / 2 - 15, ScreenH / 2 - 50), Color.Red);
                    }
                    break;
            }
           
           
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
