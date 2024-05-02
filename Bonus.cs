using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Mono_UnderTheSea
{
    public class Bonus
    {
        public Verlet verlet;
        public Texture2D sprite;
        public int type;

        public Bonus(Vector2 pos, int type, Texture2D sprite)
        {
            switch (type)
            {
                case 1:
                    this.sprite = sprite;
                    verlet = new Verlet(pos, new Vector2(55, 50));
                    break;
                case 2:
                    this.sprite = sprite;
                    verlet = new Verlet(pos, new Vector2(70, 60));
                    break;
            }
            this.type = type;
        }

        public void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, verlet.transform.position, null, Color.White);
        }
    }
}