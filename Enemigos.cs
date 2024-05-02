using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_UnderTheSea
{
    public class Enemigos : Sirena
    {
        public Verlet verlet;

        public Enemigos(Vector2 pos, int type, Texture2D sprite) : base(sprite, pos, 80, 60, 0) // No necesitas pasar screenWidth aquí
        {
            switch (type)
            {
                case 1:
                    verlet = new Verlet(pos, new Vector2(80, 60));
                    break;
                case 2:
                    verlet = new Verlet(pos, new Vector2(80, 60));
                    break;
                case 3:
                    verlet = new Verlet(pos, new Vector2(70, 60));
                    break;
            }
        }
    }
}