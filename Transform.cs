using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_UnderTheSea
{
    public class Transform
    {
        public Vector2 position;
        public Vector2 size;

        public Transform(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }
    }
}
