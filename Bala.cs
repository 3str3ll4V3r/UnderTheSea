using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Mono_UnderTheSea
{
    public class Bala
    {
        public Verlet verlet;
        public Texture2D sprite;

        public Bala(Vector2 pos, Texture2D sprite)
        {
            this.sprite = sprite;
            verlet = new Verlet(pos, new Vector2(25, 20));
        }

        public void MoveUp()
        {
            verlet.transform.position.Y -= 15;
        }

        public void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, verlet.transform.position, Color.White); // Cambiado de verlet.position a verlet.transform.position
        }
    }
}