using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.Classes
{
    public class Ball : GameObject
    {
        public Vector2 Velocity;
        public Random Randomic;

        public Ball()
        {
            Randomic = new Random();
        }
        public void Launch(float Speed)
        {
            Position = new Vector2(Game1.ScreenW / 2 - Texture.Width / 2, Game1.ScreenH / 2 - Texture.Height / 2);
            float rotation = (float)(Math.PI / 2 + (Randomic.NextDouble() * (Math.PI / 1.5f) - Math.PI / 3));

            Velocity.X = (float)Math.Sin(rotation);
            Velocity.Y = (float)Math.Cos(rotation);

            if (Randomic.Next(2) == 1)
            {
                Velocity.X *= -1;
            }
            Velocity *= Speed;
        }
    }
}
