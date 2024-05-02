using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_UnderTheSea
{
    public class Plataformas
    {
        Texture2D sprite;
        public Transform transform;
        public bool isTouchedByPlayer;

        public Plataformas(Vector2 pos, Texture2D sprite)
        {
            this.sprite = sprite;
            // Mantenemos el tamaño fijo de las plataformas
            Vector2 size = new Vector2(100, 30); // Tamaño fijo para las plataformas
            transform = new Transform(pos, size);
            isTouchedByPlayer = false;
        }

        public Plataformas(Vector2 pos, Texture2D sprite, int width, int height)
        {
            this.sprite = sprite;
            // Mantenemos el tamaño fijo de las plataformas
            Vector2 size = new Vector2(width, height); // Tamaño personalizado para las plataformas
            transform = new Transform(pos, size);
            isTouchedByPlayer = false;
        }

        // Método para detectar colisiones con la sirena
        public bool CollidesWithSirena(Rectangle sirenaCollisionRectangle)
        {
            Rectangle plataformaRectangle = new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.size.X, (int)transform.size.Y);
            return plataformaRectangle.Intersects(sirenaCollisionRectangle);
        }

        public void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.size.X, (int)transform.size.Y), Color.White);
        }
    }
}
