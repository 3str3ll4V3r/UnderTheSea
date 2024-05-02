using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono_UnderTheSea;
using System;

namespace Mono_UnderTheSea
{
    public class Pez
    {
        private Texture2D sprite;
        private Vector2 posicion;
        private float velocidadY;
        private Game1 game;

        public Pez(Vector2 posicion, Texture2D sprite, float velocidadY, Game1 game)
        {
            this.posicion = posicion;
            this.sprite = sprite;
            this.velocidadY = velocidadY;
            this.game = game;
        }

        public void Actualizar()
        {
            posicion.Y += velocidadY;

            if (posicion.Y > game.GraphicsDevice.Viewport.Height)
            {
                Reposicionar();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, posicion, Color.White);
        }

        private void Reposicionar()
        {
            Random random = new Random();
            posicion.X = random.Next(game.GraphicsDevice.Viewport.Width);
            posicion.Y = -sprite.Height;
            velocidadY = random.Next(1, 5);
        }
    }
}