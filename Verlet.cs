using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_UnderTheSea
{
    public class Verlet
    {
        public Vector2 position;
        public Vector2 size;
        public float gravity;
        float a;
        float dx;
        public Transform transform;

        public Verlet(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
            gravity = 0;
            a = 0.4f;
            dx = 0;
            transform = new Transform(position, size);
        }

        public void ApplyVerlet()
        {
            CalculateVerlet();
        }

        public void CalculateVerlet()
        {
            if (transform.position.X != 0) // Usa transform en lugar de position
            {
                transform.position.X += dx;
            }
            if (transform.position.Y < 700) // Usa transform en lugar de position
            {
                transform.position.Y += gravity;
                gravity += a;
            }
        }
    }
}
