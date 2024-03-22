///**************************************************************************************
///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
///**************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.Net.Sockets;

namespace SpaceInvaders
{
    internal class Game
    {
        // Déclaration des champs de la classe
        private int playerPosition; // Position du joueur
        private bool gameOver; // Indique si le jeu est terminé
        private int score; // Score du joueur
        public List<Invader> invaders; // Liste des envahisseurs
        private Player player; // Le joueur
        private List<Rocket> rockets; // Liste des missiles tirés par le joueur
        private List<Rocket> invadersRockets; //Liste des missile tirés par les invaders
        private List<Bunker> bunkers;

        // Crée une copie de la liste des envahisseurs pour éviter les modifications concurrentes
        List<Invader> invadersCopy;
        List<Rocket> rocketsCopy;
        List<Rocket> invadersRocketsCopy ;
        
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
            invadersCopy = new List<Invader>(invaders);
            rocketsCopy = new List<Rocket>(rockets);
            invadersRocketsCopy = new List<Rocket>(invadersRockets);
            bunkers = new List<Bunker>();
            InitializeInvaders(); // Initialise les envahisseurs
            InitializeBunkers(); // Initialisation des bunkers
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

                // Crée une copie des liste des missiles pour éviter les modifications concurrentes
                rocketsCopy = rockets;
                invadersRocketsCopy = invadersRockets;

                //Conversion en liste
                rocketsCopy.ToList<Rocket>();
                invadersRocketsCopy.ToList<Rocket>();
            }

            // Affiche un message de fin de jeu lorsque le jeu est terminé
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Game Over! Press any key to exit...");
            Console.ReadKey();
        }

        // Méthode pour mettre à jour l'état du jeu
        private void Update()
        {


            for(int i = 0; i < invaders.Count; i++)
            {
                Invader invader = invaders[i];

                invader.Move();

                for (int j = 0; j < rocketsCopy.Count; j++)
                {
                    Rocket rocket = rocketsCopy[j];

                    // Vérifie si la hitbox du missile du joeur entre en collision avec la hitbox de l'envahisseur
                    if (rocket.GetHitbox().IntersectsWith(invader.GetHitbox()))
                    {
                        rocket.IsActive = false; // Désactive le missile
                        invader.IsActive = false;
                        rockets.Remove(rocket);
                        invaders.Remove(invader);
                        if (invaders.Count() == 0)
                        {
                            InitializeInvaders();
                        }
                        //invader.Reset(); // Réinitialise l'envahisseur//
                        score++; // Incrémente le score
                    }
                }

                //pour randomisé le tire de l'ennemi le plus proches
                Random random = new Random();

                //Selection arbitraire
                int randomtouch = 30;

                //On pousse la variable dans le random
                random.Next(randomtouch);

                // Vérifiez si cet envahisseur est le plus proche du joueur
                Invader closestInvader = FindClosestInvader();
                if (closestInvader == invader)
                {
                    if (score % randomtouch == 0)
                    {
                        // Si oui, tirez un missile
                        FireClosestInvaderRocket();
                    }
                }

                for (int j = 0; j < invadersRocketsCopy.Count; j++)
                {
                    Rocket rocket1 = invadersRocketsCopy[j];

                    // Vérifie si la hitbox du missile entre en collision avec la hitbox de du joueur
                    if (rocket1.GetHitbox().IntersectsWith(player.GetHitbox()))
                    {
                        gameOver = true; // fin du jeu
                        break;
                    }

                    foreach (Bunker bunker in bunkers)
                    {
                        if (rocket1.GetHitbox().IntersectsWith(bunker.GetHitbox()))
                        {
                            bunker.TakeDamage(); // Bunker endommagé
                            rocket1.IsActive = false; // Désactive le missile
                        }
                    }
                }
                
                //Vérifie que les invaders ne sont pas arrivés au niveau du joueur.
                if (invader.Y == Console.WindowHeight - 1)
                {
                    gameOver = true;
                    break;
                }

                //Déplacement des missiles du joueur
                for (int j = 0; j < rocketsCopy.Count; j++)
                {
                    Rocket rocket = rocketsCopy[j];
                    rocket.Move();
                }

                //Déplacement des missiles des ennemi
                for (int j = 0; j < invadersRocketsCopy.Count; j++)
                {
                    Rocket rocket1 = invadersRocketsCopy[j];
                    rocket1.NegativMove();
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

            // Dessine les missiles du joueur
            for (int i = 0; i < rocketsCopy.Count; i++)
            {
                Rocket rocket = rocketsCopy[i];
                rocket.Draw();
            }

            // Dessine les missiles de l'envahisseur
            for (int i = 0; i < invadersRocketsCopy.Count; i++)
            {
                Rocket rocket1 = invadersRocketsCopy[i];
                rocket1.Draw();
            }

            //Finir d'implémanter les bunker

            //for (int i = 0; i < bunkers.Count(); i++)
            //{
            //    Bunker bunker = bunkers[i];
            //    bunker.Draw();
            //}


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

            Random random = new Random();

            int randomtouch = 30;

            random.Next(randomtouch);

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
                            if (cptr % randomtouch == 0)
                            {
                                //// Crée un nouveau missile à la position de l'envahisseur
                                //Rocket newRocket = new Rocket(invader.X, invader.Y + 1);
                                //newRocket.InvadersActivate(invader.X, invader.Y); // Active le missile pour cibler les envahisseurs
                                //invadersRockets.Add(newRocket); // Ajoute le missile à la liste des missiles des envahisseurs
                                //invadersRocketsCopy = invadersRockets;
                                //invadersRocketsCopy.ToList<Rocket>();  
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


                            if (cptr % randomtouch == 0)
                            {  
                                //// Crée un nouveau missile à la position de l'envahisseur
                                //Rocket newRocket = new Rocket(invader.X, invader.Y + 1);
                                //newRocket.InvadersActivate(invader.X, invader.Y); // Active le missile pour cibler les envahisseurs
                                //invadersRockets.Add(newRocket); // Ajoute le missile à la liste des missiles des envahisseurs
                                //invadersRocketsCopy = invadersRockets;
                                //invadersRocketsCopy.ToList<Rocket>();
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
                        rocketsCopy = rockets;
                        rocketsCopy.ToList<Rocket>();
                    }
                    // Déplace le joueur en fonction de sa position actuelle
                    player.Move(playerPosition);
                }
            }
        }

        // Méthode pour initialiser les envahisseurs du jeu
        public void InitializeInvaders()
        {
            // Ajoute un envahisseur initial
            //invaders.Add(new Invader((Console.WindowWidth) / 2, 5));
            invaders.Clear();

            int InvaderWidth = 5;
            int InvaderHeight = 1;
            int InvaderSpacingX = 5;
            int InvaderSpacingY = 1;

            // Déterminez les positions initiales pour le bloc d'envahisseurs
            int startX = (Console.WindowWidth - (InvaderWidth * 3)) / 2; // Position horizontale de départ
            int startY = 5; // Position verticale de départ

            // Boucle pour créer un bloc d'envahisseurs en rangée de 3 par 5
            for (int row = 0; row < 1; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                     

                    // Calculez la position horizontale pour chaque envahisseur dans la rangée
                    int invaderX = startX + col * (InvaderWidth + InvaderSpacingX);
                    // Calculez la position verticale pour chaque envahisseur dans la colonne
                    int invaderY = startY + row * (InvaderHeight + InvaderSpacingY);

                    // Créez un nouvel envahisseur avec les positions calculées
                    Invader invader = new Invader(invaderX, invaderY);

                    invader.IsActive = true;
                    // Ajoutez l'envahisseur à la liste des envahisseurs
                    invaders.Add(invader);
                }
            }
        }


        // Méthode pour trouver l'envahisseur le plus proche du joueur
        private Invader FindClosestInvader()
        {
            Invader closestInvader = null;
            double minDistance = double.MaxValue;

            foreach (Invader invader in invaders)
            {
                // Calculez la distance horizontale entre l'envahisseur et le joueur
                double distance = Math.Abs(invader.X - playerPosition);

                // Si la distance est inférieure à la distance minimale enregistrée jusqu'à présent
                if (distance < minDistance)
                {
                    // Mettez à jour l'envahisseur le plus proche et la distance minimale
                    closestInvader = invader;
                    minDistance = distance;
                }
            }

            return closestInvader;
        }

        // Méthode pour permettre à l'envahisseur le plus proche de tirer
        private void FireClosestInvaderRocket()
        {
            Invader closestInvader = FindClosestInvader();

            // Vérifiez si un envahisseur a été trouvé
            if (closestInvader != null)
            {
                // Créez un nouveau missile à la position de l'envahisseur
                Rocket newRocket = new Rocket(closestInvader.X, closestInvader.Y + 1);
                newRocket.InvadersActivate(closestInvader.X, closestInvader.Y + 1); // Active le missile pour cibler le joueur
                invadersRockets.Add(newRocket); // Ajoute le missile à la liste des missiles des envahisseurs
            }
        }

        public void InitializeBunkers()
        {
            

            // Ajoutez des bunkers à des positions spécifiques
            bunkers.Add(new Bunker(20, Console.WindowHeight - 10));
            bunkers.Add(new Bunker(40, Console.WindowHeight - 10));
            // Ajoutez d'autres bunkers selon votre conception
        }
    }

}