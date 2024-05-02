using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mono_UnderTheSea;
using System;
using System.Collections.Generic;

namespace Mono_UnderTheSea
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sirena sirena;
        private int screenWidth = 330; // Tamaño de la ventana en Windows Forms
        private int screenHeight = 600;

        private Texture2D fondo;
        private Texture2D fondoarena;
        private List<Pez> peces;

        private SpriteFont font;
        private Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
        }

        protected override void Initialize()
        {
            // Inicializar la lista de peces
            peces = new List<Pez>();
            random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Cargar las imágenes de fondo
            fondo = Content.Load<Texture2D>("1fondo");
            fondoarena = Content.Load<Texture2D>("1fondoarena");

            // Cargar la textura de la sirena
            Texture2D sirenaTexture = Content.Load<Texture2D>("sirena");

            int sirenaWidth = 65;
            int sirenaHeight = 80;

            sirena = new Sirena(sirenaTexture, new Vector2(100, 350), sirenaWidth, sirenaHeight, screenWidth); // Pasar el ancho de la pantalla

            // Cargar imágenes de enemigos
            Texture2D enemigo1Texture = Content.Load<Texture2D>("anguila");
            Texture2D enemigo2Texture = Content.Load<Texture2D>("tiburon");
            Texture2D enemigo3Texture = Content.Load<Texture2D>("medusa");

            // Cargar imágenes de bonificaciones
            Texture2D bonus1Texture = Content.Load<Texture2D>("burbujas");
            Texture2D bonus2Texture = Content.Load<Texture2D>("tridente");

            // Cargar la fuente para mostrar la puntuación
            //font = Content.Load<SpriteFont>("ScoreFont");

            // Generar peces aleatorios
            GenerarPecesAleatorios();

            // Generar secuencia de inicio de plataformas
            Controlador.GenerateStartSequence(Content.Load<Texture2D>("coral"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        }

        protected override void Update(GameTime gameTime)
        {
            // Actualizar la sirena
            sirena.Update();

            // Generar plataformas aleatorias
            Controlador.GenerateRandomPlatform(Content.Load<Texture2D>("coral"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


            // Actualizar los peces
            foreach (Pez pez in peces)
            {
                pez.Actualizar();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Dibujar los fondos en capas con parallax
            DrawBackgrounds();

            // Dibujar las plataformas
            foreach (var plataforma in Controlador.plataformas)
            {
                plataforma.DrawSprite(spriteBatch);
            }

            // Dibujar los peces
            foreach (Pez pez in peces)
            {
                pez.Draw(spriteBatch);
            }

            // Dibujar la sirena
            sirena.Draw(spriteBatch);

            // Dibujar la puntuación
            //spriteBatch.DrawString(font, $"Score: {Controlador.score}", new Vector2(10, 10), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Generar peces aleatorios
        private void GenerarPecesAleatorios()
        {
            Random random = new Random();

            // Generar una cantidad de peces aleatorios
            int cantidadPeces = 11;
            for (int i = 0; i < cantidadPeces; i++)
            {
                // Generar posición aleatoria para el pez
                Vector2 posicionInicial = new Vector2(random.Next(GraphicsDevice.Viewport.Width), random.Next(GraphicsDevice.Viewport.Height));

                // Cargar una textura de pez aleatoria (asegúrate de tener imágenes nombradas pes1.png, pes2.png, etc.)
                string nombreTexturaPez = $"pes{i + 1}";
                Texture2D texturaPez = Content.Load<Texture2D>(nombreTexturaPez);

                // Tamaño fijo para todos los peces (ajustar según sea necesario)
                texturaPez = RedimensionarTextura(texturaPez, new Point(50, 50)); // Tamaño deseado

                // Generar velocidad aleatoria para el pez
                float velocidadY = random.Next(1, 5); // Velocidad aleatoria entre 1 y 5

                // Crear instancia de Pez y agregarlo a la lista, pasando 'this' como referencia al juego actual
                Pez pez = new Pez(posicionInicial, texturaPez, velocidadY, this);
                peces.Add(pez);
            }
        }

        private Texture2D RedimensionarTextura(Texture2D textura, Point nuevoTamaño)
        {
            // Crear una nueva textura con el tamaño deseado
            Texture2D nuevaTextura = new Texture2D(GraphicsDevice, nuevoTamaño.X, nuevoTamaño.Y);

            // Leer los datos de color de la textura original
            Color[] data = new Color[textura.Width * textura.Height];
            textura.GetData(data);

            // Redimensionar los datos de color a la nueva textura
            Color[] newData = new Color[nuevoTamaño.X * nuevoTamaño.Y];
            float xRatio = (float)textura.Width / nuevoTamaño.X;
            float yRatio = (float)textura.Height / nuevoTamaño.Y;
            for (int y = 0; y < nuevoTamaño.Y; y++)
            {
                for (int x = 0; x < nuevoTamaño.X; x++)
                {
                    int index = (int)(x * xRatio) + (int)(y * yRatio) * textura.Width;
                    newData[x + y * nuevoTamaño.X] = data[index];
                }
            }

            // Establecer los datos de color en la nueva textura
            nuevaTextura.SetData(newData);

            return nuevaTextura;
        }

        private void DrawBackgrounds()
        {
            // Obtener las dimensiones del viewport
            int viewportWidth = GraphicsDevice.Viewport.Width;
            int viewportHeight = GraphicsDevice.Viewport.Height;

            // Dibujar el fondo lejano (cielo o horizonte) ajustando al tamaño del viewport
            spriteBatch.Draw(fondo, new Rectangle(0, 0, viewportWidth, viewportHeight), Color.White);

            // Dibujar el fondo medio (montañas o edificios) ajustando al tamaño del viewport
            spriteBatch.Draw(fondoarena, new Rectangle(0, 0, viewportWidth, viewportHeight), Color.White);
        }
    }
}