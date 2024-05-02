using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Mono_UnderTheSea
{
    public static class Controlador
    {
        public static List<Bala> balas = new List<Bala>();
        public static List<Enemigos> enemigos = new List<Enemigos>();
        public static List<Bonus> bonuses = new List<Bonus>();
        public static int startPlatformPosY = 300;
        public static int score = 0;
        public static List<Plataformas> plataformas = new List<Plataformas>();

        public static void AddPlatform(Texture2D sprite, Vector2 position, int viewportWidth, int viewportHeight)
        {
            Plataformas plataforma = new Plataformas(position, sprite, viewportWidth, viewportHeight);
            plataformas.Add(plataforma);
        }

        public static void CreateBullet(Vector2 pos, Texture2D sprite)
        {
            var bala = new Bala(pos, sprite);
            balas.Add(bala);
        }

        public static void GenerateStartSequence(Texture2D sprite, int viewportWidth, int viewportHeight)
        {
            Random r = new Random();
            for (int i = 0; i < 11; i++)
            {
                int x = r.Next(0, viewportWidth);
                int y = r.Next(30, 45);
                startPlatformPosY -= y;
                Vector2 position = new Vector2(x, startPlatformPosY);
                Plataformas plataforma = new Plataformas(position, sprite);
                plataformas.Add(plataforma);
            }
        }

        public static void GenerateRandomPlatform(Texture2D sprite, int viewportWidth, int viewportHeight)
        {
            ClearPlatforms();
            Random r = new Random();
            int numberOfPlatforms = 5; // Define el número de plataformas a generar

            for (int i = 0; i < numberOfPlatforms; i++)
            {
                int x = r.Next(0, viewportWidth);
                int y = r.Next(30, 45); // Reducir la distancia vertical entre las plataformas
                startPlatformPosY -= y;
                Vector2 position = new Vector2(x, startPlatformPosY);
                Plataformas plataforma = new Plataformas(position, sprite); // Usa el constructor que toma solo la posición y el sprite
                plataformas.Add(plataforma);

                // Ajusta los valores para cambiar la frecuencia de generación de enemigos y bonificaciones
                int chanceOfEnemy = 2; // Ajusta este valor para cambiar la frecuencia de generación de enemigos
                int chanceOfBonus = 1; // Ajusta este valor para cambiar la frecuencia de generación de bonificaciones

                var c = r.Next(1, 10);

                if (c <= chanceOfEnemy)
                {
                    CreateEnemy(plataforma, sprite);
                }
                else if (c <= chanceOfEnemy + chanceOfBonus)
                {
                    CreateBonus(plataforma, sprite);
                }
            }
        }

        public static void CreateBonus(Plataformas plataforma, Texture2D sprite)
        {
            Random r = new Random();
            var bonusType = r.Next(1, 3);

            switch (bonusType)
            {
                case 1:
                    var bonus = new Bonus(new Vector2(plataforma.transform.position.X + (plataforma.transform.size.X / 2) - 7, plataforma.transform.position.Y - 15), bonusType, sprite);
                    bonuses.Add(bonus);
                    break;
                case 2:
                    bonus = new Bonus(new Vector2(plataforma.transform.position.X + (plataforma.transform.size.X / 2) - 15, plataforma.transform.position.Y - 30), bonusType, sprite);
                    bonuses.Add(bonus);
                    break;
            }
        }

        public static void CreateEnemy(Plataformas plataforma, Texture2D sprite)
        {
            Random r = new Random();
            var enemyType = r.Next(1, 4);

            switch (enemyType)
            {
                case 1:
                    var enemy = new Enemigos(new Vector2(plataforma.transform.position.X + (plataforma.transform.size.X / 2) - 20, plataforma.transform.position.Y - 40), enemyType, sprite);
                    enemigos.Add(enemy);
                    break;
                case 2:
                    enemy = new Enemigos(new Vector2(plataforma.transform.position.X + (plataforma.transform.size.X / 2) - 35, plataforma.transform.position.Y - 50), enemyType, sprite);
                    enemigos.Add(enemy);
                    break;
                case 3:
                    enemy = new Enemigos(new Vector2(plataforma.transform.position.X + (plataforma.transform.size.X / 2) - 35, plataforma.transform.position.Y - 60), enemyType, sprite);
                    enemigos.Add(enemy);
                    break;
            }
        }

        public static void RemoveEnemy(int i)
        {
            enemigos.RemoveAt(i);
            // Aumentar el puntaje cada vez que se elimina un enemigo
            score += 15;
        }

        public static void RemoveBullet(int i)
        {
            balas.RemoveAt(i);
        }

        public static void ClearPlatforms()
        {
            for (int i = 0; i < plataformas.Count; i++)
            {
                if (plataformas[i].transform.position.Y >= 700)
                {
                    plataformas.RemoveAt(i);
                }
            }
            for (int i = 0; i < bonuses.Count; i++)
            {
                if (bonuses[i].verlet.transform.position.Y >= 700)
                {
                    bonuses.RemoveAt(i);
                }
            }

            for (int i = 0; i < enemigos.Count; i++)
            {
                if (enemigos[i].verlet.transform.position.Y >= 700)
                {
                    enemigos.RemoveAt(i);
                }
            }
        }
    }
}