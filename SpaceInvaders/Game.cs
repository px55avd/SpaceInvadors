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

        /// <summary>
        /// Méthode pour démarrer le jeu
        /// </summary>
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
                Thread.Sleep(5); // Pause pour contrôler la vitesse du jeu

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

        /// <summary>
        /// Méthode pour mettre à jour l'état du jeu
        /// </summary>
        private void Update()
        {
            for(int i = 0; i < invaders.Count; i++)
            {
                Invader invader = invaders[i];

                invader.Move();

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
                        score++; // Incrémente le score
                    }


                    for (int k = 0; k < bunkers.Count; k++)
                    {
                        Bunker bunker = bunkers[k];

                        // Vérifie si la hitbox missile joueur entre en collision avec la hitbox du bunker
                        if (rocket.GetHitbox().IntersectsWith(bunker.GetHitbox()))
                        {
                            rocket.IsActive = false;
                            rockets.Remove(rocket);

                            if (bunker.Damaged == true && bunker.bunkerSymbol == "/")
                            {
                                bunkers.Remove(bunker);
                            }
                            else
                            {
                                bunker.bunkerSymbol = "/";
                                bunker.Damaged = true;
                            }
                        }
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

                    for (int k = 0; k < bunkers.Count; k++)
                    {
                        Bunker bunker = bunkers[k];

                        // Vérifie si la hitbox du missile envahisseur entre en collision avec la hitbox du bunker
                        if (rocket1.GetHitbox().IntersectsWith(bunker.GetHitbox()))
                        {
                            rocket1.IsActive = false;
                            rockets.Remove(rocket1);

                            if (bunker.Damaged == true && bunker.bunkerSymbol == "/")
                            {
                                bunkers.Remove(bunker);
                                
                            }
                            else
                            {
                                bunker.bunkerSymbol = "/";
                                bunker.Damaged = true;
                            }
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
                    //Instanciation d'une variable boléene pour récupérer le retour de la méthode Move().
                    bool soFar;
                    Rocket rocket = rocketsCopy[j];

                    rocket.Move();// Méthode pour déplacer les missile du joueur.

                    //Récupérer le retour de la méthode
                    soFar = rocket.Move();

                    //Condition si le missile sort de l'écran.
                    if(soFar == false)
                    {
                        rocketsCopy.Remove(rocket);// Supprime le  missile de la liste.
                    }

                }

                //Déplacement des missiles des ennemi
                for (int j = 0; j < invadersRocketsCopy.Count; j++)
                {
                    //Instanciation d'une variable boléene pour récupérer le retour de la méthode Move().
                    bool soFar;

                    Rocket rocket1 = invadersRocketsCopy[j];
                    rocket1.NegativMove();

                    //Récupérer le retour de la méthode
                    soFar = rocket1.NegativMove();

                    //Condition si le missile sort de l'écran.
                    if (soFar == false)
                    {
                        rocketsCopy.Remove(rocket1);// Supprime le  missile de la liste.
                    }
                }
            }

            score++; // Augmente le score à chaque mise à jour
        }

        /// <summary>
        /// Méthode pour dessiner les éléments du jeu sur la console
        /// </summary>
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
           
            //Dessine les bunkers dans la console.
            for (int i = 0; i < bunkers.Count(); i++)
            {
                Bunker bunker = bunkers[i];
                bunker.Draw();
            }

            // Dessine le score du joueur
            DrawScore();
        }

        /// <summary>
        /// Méthode pour dessiner le score du joueur sur la console
        /// </summary>
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
                    }
                    // Déplace le joueur vers la droite si la touche enfoncée est la flèche droite et que le joueur n'est pas au bord droit de la console
                    else if (key.Key == ConsoleKey.RightArrow && playerPosition < Console.WindowWidth - 1)
                    {
                        playerPosition++; // Incrémente la position du joueur
                    }
                    // Tire un missile vers le haut si la touche enfoncée est la barre d'espace
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        //Condition pour ne tiré qu'u
                        if(rockets.Count() < 1)
                        {
                            Rocket newRocket = new Rocket(playerPosition, Console.WindowHeight - 3);// Crée un nouveau missile à la position actuelle du joueur
                            newRocket.Activate(playerPosition); // Active le missile pour cibler les ennemis
                            rockets.Add(newRocket); // Ajoute le missile à la liste des missiles du joueur
                            rocketsCopy = rockets;
                            rocketsCopy.ToList<Rocket>();
                        }
                    }
                    // Déplace le joueur en fonction de sa position actuelle
                    player.Move(playerPosition);
                }
            }
        }

        /// <summary>
        /// Méthode pour initialiser les envahisseurs du jeu
        /// </summary>
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
                {                    // Calculez la position horizontale pour chaque envahisseur dans la rangée
                    int invaderX = startX + col * (InvaderWidth + InvaderSpacingX);
                    // Calculez la position verticale pour chaque envahisseur dans la colonne
                    int invaderY = startY + row * (InvaderHeight + InvaderSpacingY);

                    // Créez un nouvel envahisseur avec les positions calculées
                    Invader invader = new Invader(invaderX, invaderY);

                    //Active les missile.
                    invader.IsActive = true;

                    // Ajoutez l'envahisseur à la liste des envahisseurs
                    invaders.Add(invader);
                }
            }
        }

        /// <summary>
        /// Méthode pour trouver l'envahisseur le plus proche du joueur
        /// </summary>
        /// <returns>L'envahisseur le plus proche du joueur</returns>
        private Invader FindClosestInvader()
        {
            Invader closestInvader = null;
            double minDistance = double.MaxValue; // peut être réduit si necessaire

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

        /// <summary>
        /// Méthode pour permettre à l'envahisseur le plus proche de tirer
        /// </summary>
        private void FireClosestInvaderRocket()
        {
            Invader closestInvader = FindClosestInvader();

            // Vérifiez si un envahisseur a été trouvé
            if (closestInvader != null)
            {
                
                Rocket newRocket = new Rocket(closestInvader.X, closestInvader.Y + 1); // Créez un nouveau missile à la position de l'envahisseur
                newRocket.InvadersActivate(closestInvader.X, closestInvader.Y + 1); // Active le missile pour cibler le joueur
                invadersRockets.Add(newRocket); // Ajoute le missile à la liste des missiles des envahisseurs
            }
        }

        /// <summary>
        /// Méthode pour initialiser les bunkers.
        /// </summary>
        public void InitializeBunkers()
        {
            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                bunkers.Add(new Bunker(Console.WindowWidth - 35 + (i), Console.WindowHeight - 6));
            }

            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                bunkers.Add(new Bunker(Console.WindowWidth - 95 + (i), Console.WindowHeight - 6));
            }

            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                bunkers.Add(new Bunker(Console.WindowWidth - 75 + (i), Console.WindowHeight - 6));
            }

            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                bunkers.Add(new Bunker(Console.WindowWidth - 55 + (i), Console.WindowHeight - 6));
            }
        }
    }
}