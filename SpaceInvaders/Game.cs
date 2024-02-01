using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SpaceInvaders
{
    internal class Game
    {
        private int playerPosition;
        private bool gameOver;
        private int score;
        private List<Invader> invaders;
        private Player player;
        private List<Rocket> rockets;

        public Game()
        {
            playerPosition = Console.WindowWidth / 2;
            gameOver = false;
            score = 0;
            invaders = new List<Invader>();
            player = new Player(playerPosition);
            rockets = new List<Rocket>();
            InitializeInvaders();
        }

        public void Start()
        {
            Thread userInputThread = new Thread(UserInput);
            userInputThread.Start();

            while (!gameOver)
            {
                Update();
                Draw();
                Thread.Sleep(50);
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Game Over! Press any key to exit...");
            Console.ReadKey();
        }

        private void Update()
        {
            foreach (Invader invader in invaders)
            {
                invader.Move();
                if (invader.Y == Console.WindowHeight - 1)
                {
                    gameOver = true;
                    break;
                }
            }

            foreach (Invader invader in invaders)
            {
                if (invader.Y == Console.WindowHeight - 1)
                {
                    gameOver = true;
                    break;
                }
                if (invader.X == playerPosition && invader.Y == Console.WindowHeight - 2)
                {
                    gameOver = true;
                    break;
                }
            }

            foreach (Rocket rocket in rockets)
            {
                rocket.Move();

                foreach (Invader invader in invaders)
                {
                    if (rocket.X == invader.X && rocket.Y == invader.Y)
                    {
                        invader.Reset();
                        score++;
                        rocket.IsActive = false;
                    }
                }
            }

            score++;
        }

        private void Draw()
        {
            // Efface le contenu actuel de la console
            Console.Clear();

            // Dessine le joueur
            player.Draw();

            // Parcourt la liste des envahisseurs et les dessine
            foreach (Invader invader in invaders)
            {
                invader.Draw();
            }

            // Crée une copie de la liste des missiles pour éviter les modifications concurrentes
            List<Rocket> rocketsCopy = new List<Rocket>(rockets);

            // Parcourt la copie de la liste des missiles et les dessine
            foreach (Rocket rocket in rocketsCopy)
            {
                rocket.Draw();
            }

            // Dessine le score du joueur
            DrawScore();
        }



        private void DrawScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Score: {score}");
        }

        private void UserInput()
        {
            while (!gameOver)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.LeftArrow && playerPosition > 0)
                    {
                        playerPosition--;
                    }
                    else if (key.Key == ConsoleKey.RightArrow && playerPosition < Console.WindowWidth - 1)
                    {
                        playerPosition++;
                    }
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        Rocket newRocket = new Rocket(playerPosition, Console.WindowHeight - 3);
                        newRocket.Activate(playerPosition);
                        rockets.Add(newRocket);
                    }
                    player.Move(playerPosition);
                }
            }
        }

        private void InitializeInvaders()
        {
            // Add initial invaders
            // For simplicity, we'll add one invader for now
            invaders.Add(new Invader(Console.WindowWidth / 2, 5));
        }
    }
}
