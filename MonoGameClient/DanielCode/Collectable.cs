using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameClient.DanielCode
{
    class Collectable
    {
        public Vector2 position { get; set; }
        public Rectangle rect { get; set; }
        public Texture2D texture { get; set; }
        public bool InCollision { get; set; }

        public Rectangle bounds;

        public void CollisionWithPlayer(List<Player> players)
        {
            foreach (var p in players)
            {
                bounds = new Rectangle((int)this.position.X, (int)this.position.Y, this.texture.Width, this.texture.Height);
                Rectangle otherBound = new Rectangle((int)p.position.X, (int)p.position.Y, p.tx.Width, this.texture.Height);
                if (bounds.Intersects(p))
                {
                    InCollision = true;
                }
                else
                {
                    InCollision = false;
                }
            }
        }
    }
}
