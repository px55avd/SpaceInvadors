using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;

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
        private List<Rocket> invadersRockets; //Liste des missile tirés par les invaders

        /// <summary>
        /// Constructeur de la classe Game
        /// </summary>
        public Game()
        {
            // Initialise les champs
            playerPosition = Console.WindowWidth / 2;
            gameOver = false;
            score = 0;
            invaders = new List<Invader>();
            player = new Player(playerPosition);
            rockets = new List<Rocket>();
            invadersRockets = new List<Rocket>();
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
                Thread.Sleep(30); // Pause pour contrôler la vitesse du jeu
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

                foreach(Rocket rocket in invadersRockets)
                {
                    // Vérifie si la hitbox du missile entre en collision avec la hitbox de du joueur
                    if (rocket.GetHitbox().IntersectsWith(player.GetHitbox()))
                    {
                        gameOver = true; // fin du jeu
                        break;
                    }
                }

                //Vérifie que les invaders ne sont pas arrivés au niveau du joueur.
                if (invader.Y == Console.WindowHeight - 1)
                {
                    gameOver = true;
                    break;
                }

                //Déplacement des missiles
                foreach (Rocket rocket in rockets)
                {

                        rocket.Move();//Déplace le missile vers le haut de la console
              
                    
                }
                foreach(Rocket rocket in invadersRockets)
                {
                   
                        rocket.NegativMove();//Déplace le missile vers le haut de la console
                    
                    
                    
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

            // Crée une copie des liste des missiles pour éviter les modifications concurrentes
            List<Rocket> rocketsCopy = new List<Rocket>(rockets);
            List<Rocket> invadersRocketscopy = new List<Rocket>(invadersRockets);

            // Dessine les missiles
            foreach (Rocket rocket in rocketsCopy)
            {
                rocket.Draw();
            }
            foreach (Rocket rocket in invadersRocketscopy)
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
            // Compteur pour suivre les déplacements du joueur
            int cptr = 0;

            // Boucle pour traiter les entrées utilisateur tant que le jeu n'est pas terminé
            while (!gameOver)
            {
                // Vérifie si une touche est disponible dans la console
                if (Console.KeyAvailable)
                {
                    // Récupère la touche enfoncée par l'utilisateur
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    // Déplace le joueur vers la gauche si la touche enfoncée est la flèche gauche et que le joueur n'est pas au bord gauche de la console
                    if (key.Key == ConsoleKey.LeftArrow && playerPosition > 0)
                    {
                        playerPosition--; // Décrémente la position du joueur
                        cptr++; // Incrémente le compteur de déplacement

                        // Pour chaque envahisseur, tire un missile toutes les 3 itérations du compteur
                        foreach (Invader invader in invaders)
                        {
                            if (cptr % 3 == 0)
                            {
                                //foreach (Rocket rocket in invadersRockets) pas finit d'implmenter
                                {
                                    //while (!rocket.IsActive == true) pas finit d'implémenter
                                    {
                                        // Crée un nouveau missile à la position de l'envahisseur
                                        Rocket newRocket = new Rocket(invader.X, invader.Y + 1);
                                        newRocket.InvadersActivate(invader.X, invader.Y); // Active le missile pour cibler les envahisseurs
                                        invadersRockets.Add(newRocket); // Ajoute le missile à la liste des missiles des envahisseurs
                                        
                                    }
                                }
                            }
                        }
                    }
                    // Déplace le joueur vers la droite si la touche enfoncée est la flèche droite et que le joueur n'est pas au bord droit de la console
                    else if (key.Key == ConsoleKey.RightArrow && playerPosition < Console.WindowWidth - 1)
                    {
                        playerPosition++; // Incrémente la position du joueur
                        cptr++; // Incrémente le compteur de déplacement

                        // Pour chaque envahisseur, tire un missile toutes les 3 itérations du compteur
                        foreach (Invader invader in invaders)
                        {
                            if (cptr % 3 == 0)
                            {
                                foreach (Rocket rocket in invadersRockets)//pas finit d'implémenter
                                {
                                    while (rocket.Y != 35) //pas finit d'implémenter
                                    {
                                        // Crée un nouveau missile à la position de l'envahisseur
                                        Rocket newRocket = new Rocket(invader.X, invader.Y + 1);
                                        newRocket.InvadersActivate(invader.X, invader.Y); // Active le missile pour cibler les envahisseurs
                                        invadersRockets.Add(newRocket); // Ajoute le missile à la liste des missiles des envahisseurs
                                    }
                                }
                            }
                        }
                    }
                    // Tire un missile vers le haut si la touche enfoncée est la barre d'espace
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        // Crée un nouveau missile à la position actuelle du joueur
                        Rocket newRocket = new Rocket(playerPosition, Console.WindowHeight - 3);
                        newRocket.Activate(playerPosition); // Active le missile pour cibler les ennemis
                        rockets.Add(newRocket); // Ajoute le missile à la liste des missiles du joueur
                    }
                    // Déplace le joueur en fonction de sa position actuelle
                    player.Move(playerPosition);
                }
            }
        }


        // Méthode pour initialiser les envahisseurs du jeu
        private void InitializeInvaders()
        {
            // Ajoute un envahisseur initial
            invaders.Add(new Invader((Console.WindowWidth)/2, 5));
        }

        //[STAThread]
        //public void Run()
        //{
        //    Start();
        //    Update();
        //    Draw();
        //    DrawScore();
        //    UserInput();
        //    InitializeInvaders();
        //}
        
    }
}