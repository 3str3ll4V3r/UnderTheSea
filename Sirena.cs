using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_UnderTheSea
{
    public class Sirena
    {
        private Texture2D textura;
        private Vector2 posicion;
        private int width;
        private int height;
        private int screenWidth; // Ancho de la pantalla
        private Rectangle collisionRectangle;
        private float jumpVelocity = -12f; // Velocidad inicial de salto
        private float gravity = 0.5f; // Gravedad para simular la caída
        private bool isJumping = false;

        // Propiedad para determinar si la sirena puede saltar
        public bool CanJump { get; set; }

        // Propiedad para el rectángulo de colisión de la sirena
        public Rectangle CollisionRectangle
        {
            get { return collisionRectangle; }
        }

        public Sirena(Texture2D sprite, Vector2 position, int width, int height, int screenWidth)
        {
            textura = sprite;
            posicion = position;
            this.width = width;
            this.height = height;
            this.screenWidth = screenWidth;
            CanJump = true; // La sirena puede saltar al inicio
            collisionRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // Mover la sirena con las teclas de flecha
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                MoveLeft();
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                MoveRight();
            }

            // Permitir que la sirena salte si está en una plataforma y presiona la tecla de espacio
            if (keyboardState.IsKeyDown(Keys.Space) && CanJump)
            {
                Jump();
            }

            // Aplicar gravedad a la sirena si no está en una plataforma
            if (!CanJump)
            {
                ApplyGravity();
            }

            // Actualizar el rectángulo de colisión
            collisionRectangle.X = (int)posicion.X;
            collisionRectangle.Y = (int)posicion.Y;

            // Hacer que la sirena regrese por el otro lado si sale de la pantalla
            if (posicion.X < -width)
            {
                // Si la sirena sale por la izquierda, aparece por la derecha
                posicion.X = screenWidth;
            }
            else if (posicion.X > screenWidth)
            {
                // Si la sirena sale por la derecha, aparece por la izquierda
                posicion.X = -width;
            }
        }

        private void MoveRight()
        {
            posicion.X += 5;
        }

        private void MoveLeft()
        {
            posicion.X -= 5;
        }

        private void Jump()
        {
            isJumping = true;
            posicion.Y += jumpVelocity; // Aplicar la velocidad inicial de salto
            CanJump = false; // La sirena ya no puede saltar hasta que toque una plataforma nuevamente
        }

        private void ApplyGravity()
        {
            // Aplicar la gravedad
            posicion.Y += gravity;
            // Aumentar la gravedad para simular una caída más rápida
            gravity += 0.2f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, new Rectangle((int)posicion.X, (int)posicion.Y, width, height), Color.White);
        }
    }
}
