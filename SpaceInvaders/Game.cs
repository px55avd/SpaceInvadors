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
        // Déclaration des champs de la classe
        private int playerPosition; // Position du joueur
        private bool gameOver; // Indique si le jeu est terminé
        private int score; // Score du joueur
        private List<Invader> invaders; // Liste des envahisseurs
        private Player player; // Le joueur
        private List<Rocket> rockets; // Liste des missiles tirés par le joueur

        // Constructeur de la classe Game
        public Game()
        {
            // Initialise les champs
            playerPosition = Console.WindowWidth / 2;
            gameOver = false;
            score = 0;
            invaders = new List<Invader>();
            player = new Player(playerPosition);
            rockets = new List<Rocket>();
            InitializeInvaders(); // Initialise les envahisseurs
        }

        // Méthode pour démarrer le jeu
        public void Start()
        {
            // Crée un thread pour gérer la saisie utilisateur
            Thread userInputThread = new Thread(UserInput);
            userInputThread.Start();

            // Boucle principale du jeu
            while (!gameOver)
            {
                Update(); // Met à jour l'état du jeu
                Draw(); // Dessine les éléments du jeu sur la console
                Thread.Sleep(50); // Pause pour contrôler la vitesse du jeu
            }

            // Affiche un message de fin de jeu lorsque le jeu est terminé
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Game Over! Press any key to exit...");
            Console.ReadKey();
        }

        // Méthode pour mettre à jour l'état du jeu
        private void Update()
        {
            foreach (Invader invader in invaders)
            {
                invader.Move();
                foreach (Rocket rocket in rockets)
                {
                    // Vérifie si la hitbox du missile entre en collision avec la hitbox de l'envahisseur
                    if (rocket.GetHitbox().IntersectsWith(invader.GetHitbox()))
                    {
                        invader.Reset(); // Réinitialise l'envahisseur
                        score++; // Incrémente le score
                        rocket.IsActive = false; // Désactive le missile
                    }
                }
                if (invader.Y == Console.WindowHeight - 1)
                {
                    gameOver = true;
                    break;
                }

                //Déplacement des missiles
                foreach (Rocket rocket in rockets)
                {
                    rocket.Move();//Déplace le missile 
                }
            }

            score++; // Augmente le score à chaque mise à jour
        }

        // Méthode pour dessiner les éléments du jeu sur la console
        private void Draw()
        {
            // Efface le contenu actuel de la console
            Console.Clear();

            // Dessine le joueur
            player.Draw();

            // Dessine les envahisseurs
            foreach (Invader invader in invaders)
            {
                invader.Draw();
            }

            // Crée une copie de la liste des missiles pour éviter les modifications concurrentes
            List<Rocket> rocketsCopy = new List<Rocket>(rockets);

            // Dessine les missiles
            foreach (Rocket rocket in rocketsCopy)
            {
                rocket.Draw();
            }

            // Dessine le score du joueur
            DrawScore();
        }

        // Méthode pour dessiner le score du joueur sur la console
        private void DrawScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Score: {score}");
        }

        /// <summary>
        /// Méthode pour gérer la saisie utilisateur
        /// </summary>
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

        // Méthode pour initialiser les envahisseurs du jeu
        private void InitializeInvaders()
        {
            // Ajoute un envahisseur initial
            invaders.Add(new Invader(Console.WindowWidth / 2, 5));
        }
    }
}
