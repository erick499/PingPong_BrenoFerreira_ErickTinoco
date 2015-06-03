using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.Classes
{
    public class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Color cor = Color.White;

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, cor);
        }
        public virtual void Move(Vector2 SV)
        {
            Position += SV;
        }

    }
}
