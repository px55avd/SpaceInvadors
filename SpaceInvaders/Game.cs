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
    public class Game
    {
        // Déclaration des champs de la classe
        private int _playerPosition; // Position du joueur
        public int PlayerPosition
        {
            get { return _playerPosition; }
            set { _playerPosition = value; }
        }

        private bool _gameOver; // Indique si le jeu est terminé
        public bool GameOver
        {
            get { return _gameOver; }
            set { _gameOver = value; }
        }

        private int _gameMode; // Indique si le mode jeu de la partie
        public int GameMode
        {
            get { return _gameMode; }
            set { _gameMode = value; }
        }




        private int _score; // Score du joueur
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        private List<Invader> _invaders; // Liste des envahisseurs
        public List<Invader> Invaders
        {
            get { return _invaders; }
            set { _invaders = value; }
        }

        private Player _player; // Le joueur
        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        private List<Rocket> _rockets; // Liste des missiles tirés par le joueur
        public List<Rocket> Rockets
        {
            get { return _rockets; }
            set { _rockets = value; }
        }

        private List<Rocket> _invadersRockets; //Liste des missile tirés par les invaders
        public List<Rocket> InvadersRockets
        {
            get { return _invadersRockets; }
            set { _invadersRockets = value; }
        }

        private List<Bunker> _bunkers;
        public List<Bunker> Bunkers
        {
            get { return _bunkers; }
            set { _bunkers = value; }
        }

        // Crée une copie de la liste des envahisseurs pour éviter les modifications concurrentes
        private List<Invader> _invadersCopy;

        private List<Rocket> _rocketsCopy;
        public List<Rocket> RocketsCopy
        {
            get { return _rocketsCopy; }
            set { _rocketsCopy = value; }
        }

        private List<Rocket> _invadersRocketsCopy;
        public List<Rocket> InvadersRocketsCopy
        {
            get { return _invadersRocketsCopy; }
            set { _invadersRocketsCopy = value;}
        }
        
        /// <summary>
        /// Constructeur de la classe Game
        /// </summary>
        public Game()
        {
            // Initialise les champs
            _playerPosition = Console.WindowWidth / 2;
            _gameOver = false;
            _gameMode = 0;
            _score = 0;
            _invaders = new List<Invader>();
            _player = new Player(_playerPosition);
            _rockets = new List<Rocket>();
            _invadersRockets = new List<Rocket>();
            _invadersCopy = new List<Invader>(_invaders);
            _rocketsCopy = new List<Rocket>(_rockets);
            _invadersRocketsCopy = new List<Rocket>(_invadersRockets);
            _bunkers = new List<Bunker>();

            InitializeInvaders(); // Initialise les envahisseurs
            InitializeBunkers(); // Initialisation des bunkers
        }

        internal Menu Menu
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Méthode pour démarrer le jeu
        /// </summary>
        public void Start()
        {
            // Boucle principale du jeu
            while (!_gameOver)
            {
                Update(); // Met à jour l'état du jeu
                Draw(); // Dessine les éléments du jeu sur la console

                switch (_gameMode)
                {
                    case 0:
                        Thread.Sleep(25);
                        break;

                    case 1:
                        Thread.Sleep(18);
                        break;

                    case 2:
                        Thread.Sleep(10);
                        break;
                            
                    default:
                        Thread.Sleep(25);
                        break;
                }

                
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
            UserInput();

            //Selection arbitraire
            int randomtouch = 30;

            //Selection arbitraire
            int randommove = 4;

            //Déplacement des missiles du joueur
            for (int j = 0; j < _rocketsCopy.Count; j++)
            {
                //Instanciation d'une variable boléene pour récupérer le retour de la méthode Move().
                bool soFar;

                Rocket rocket = _rocketsCopy[j];

                if (_score % randommove == 2)
                {
                    rocket.Move();//Déplacce les missile du joueurs
                }

                //Récupérer le retour de la méthode
                soFar = rocket.Move();

                //Condition si le missile sort de l'écran.
                if (soFar is false)
                {
                    rocket.Drawfinalposition();
                    
                    _rocketsCopy.Remove(rocket);// Supprime le  missile de la liste.
                    
                }
            }

            //Déplacement des missiles des ennemi
            for (int j = 0; j < _invadersRocketsCopy.Count; j++)
            {
                //Instanciation d'une variable boléene pour récupérer le retour de la méthode Move().
                bool soFar;

                Rocket rocket1 = _invadersRocketsCopy[j];




                if (_score % randommove == 2)
                {
                    rocket1.NegativMove();
                }

                //Récupérer le retour de la méthode
                soFar = rocket1.NegativMove();

                if (soFar is false)
                {
                    rocket1.Drawfinalposition();

                    _invadersRocketsCopy.Remove(rocket1);// Supprime le  missile de la liste.
                }




                //Condition si le missile sort de l'écran.
                if (soFar is false)
                {
                    rocket1.Drawfinalposition();
                    
                    _rocketsCopy.Remove(rocket1);// Supprime le  missile de la liste.
                }
            }

            for (int i = 0; i < _invaders.Count; i++)
            {
                //pour randomisé le tire de l'ennemi le plus proches
                Random random = new Random();

                //Instanciation d'une variable boléene pour récupérer le retour de la méthode Move().
                bool soDown;

                Invader invader = _invaders[i];

                if (_score % randommove == 3)
                {
                    //Récupérer le retour de la méthode
                    soDown = invader.Move(_invaders.Count);


                    if (soDown is true)
                    {
                        for (int j = 0; j < _invaders.Count; j++)
                        {
                            Invader invaderBis = _invaders[j];

                            invaderBis.Y++;

                            if (!invaderBis.LeftorRight)

                            {
                                invaderBis.LeftorRight = true;
                            }
                            else if (invaderBis.LeftorRight)
                            {
                                invaderBis.LeftorRight = false;
                            }
                        }
                    }
                }


                //On pousse la variable dans le random
                random.Next(randomtouch);

                // Vérifiez si cet envahisseur est le plus proche du joueur
                Invader closestInvader = FindClosestInvader();
                if (closestInvader == invader)
                {
                    if (_score % randomtouch == 0)
                    {
                        // Si oui, tirez un missile
                        FireClosestInvaderRocket();
                    }
                }
                



                for (int j = 0; j < _rocketsCopy.Count; j++)
                {
                    Rocket rocket = _rocketsCopy[j];

                    // Vérifie si la hitbox du missile du joeur entre en collision avec la hitbox de l'envahisseur
                    if (rocket.GetHitbox().IntersectsWith(invader.GetHitbox()))
                    {

                        rocket.IsActive = false; // Désactive le missile
                        invader.IsActive = false;

                        //Effacer le caracteère avant de l'effacer
                        rocket.Drawfinalposition();

                        _rockets.Remove(rocket);

                        invader.Drawfinalposiion();
                        _invaders.Remove(invader);

                        if (_invaders.Count() == 0)
                        {
                            InitializeInvaders();
                        }
                        _score++; // Incrémente le score
                    }


                    for (int k = 0; k < _bunkers.Count; k++)
                    {
                        Bunker bunker = _bunkers[k];

                        // Vérifie si la hitbox missile joueur entre en collision avec la hitbox du bunker
                        if (rocket.GetHitbox().IntersectsWith(bunker.GetHitbox()))
                        {
                            //Instanciation d'une variable boléene pour récupérer le retour de la méthode Move().
                            bool touch;


                            rocket.IsActive = false;
                            rocket.Drawfinalposition();
                            _rockets.Remove(rocket);

                                                       
                            if (bunker.Damaged == true && bunker.BunkerSymbol == bunker.DamagedBunkersymbol)
                            {
                                bunker.Drawfinalposition();
                                _bunkers.Remove(bunker);
                            }
                            else
                            {
                                bunker.TakeDamage();
                            }
                        }
                    }
                }


                for (int j = 0; j < _invadersRocketsCopy.Count; j++)
                {
                    Rocket rocket1 = _invadersRocketsCopy[j];

                    // Vérifie si la hitbox du missile entre en collision avec la hitbox de du joueur
                    if (rocket1.GetHitbox().IntersectsWith(_player.GetHitbox()))
                    {
                        _gameOver = true; // fin du jeu
                        break;
                    }

                    for (int k = 0; k < _bunkers.Count; k++)
                    {
                        Bunker bunker = _bunkers[k];

                        // Vérifie si la hitbox du missile envahisseur entre en collision avec la hitbox du bunker
                        if (rocket1.GetHitbox().IntersectsWith(bunker.GetHitbox()))
                        {
                            rocket1.IsActive = false;

                            rocket1.Drawfinalposition();
                            _rockets.Remove(rocket1);

                            if(bunker.BunkerSymbol == bunker.DamagedBunkersymbol)
                            {
                                bunker.Drawfinalposition();
                                _bunkers.Remove(bunker);
                            }
                            else
                            {
                                bunker.TakeDamage();
                            }
                        }
                    }
                }
                
                //Vérifie que les invaders ne sont pas arrivés au niveau du joueur.
                if (invader.Y == Console.WindowHeight - 1)
                {
                    _gameOver = true;
                    break;
                }
            }

            _score++; // Augmente le score à chaque mise à jour

            // Crée une copie des liste des missiles pour éviter les modifications concurrentes
            _rocketsCopy = _rockets;
            _invadersRocketsCopy = _invadersRockets;

            //Conversion en liste
            _rocketsCopy.ToList<Rocket>();
            _invadersRocketsCopy.ToList<Rocket>();
        }

        /// <summary>
        /// Méthode pour dessiner les éléments du jeu sur la console
        /// </summary>
        private void Draw()
        {
            // Efface le contenu actuel de la console
            //Console.Clear();


            // Dessine les missiles du joueur
            for (int i = 0; i < _rocketsCopy.Count; i++)
            {
                Rocket rocket = _rocketsCopy[i];
                rocket.Draw();
            }

            // Dessine le joueur
            _player.Draw();


            //Dessine les bunkers dans la console.
            for (int i = 0; i < _bunkers.Count(); i++)
            {
                Bunker bunker = _bunkers[i];
                bunker.Draw();
            }

            // Dessine les envahisseurs
            foreach (Invader invader in _invaders)
            {
                invader.Draw();
            }

            // Dessine les missiles de l'envahisseur
            for (int i = 0; i < _invadersRocketsCopy.Count; i++)
            {
                Rocket rocket1 = _invadersRocketsCopy[i];
                rocket1.Draw();
            }

            // Dessine le score du joueur
            DrawScore();

            // Crée une copie des liste des missiles pour éviter les modifications concurrentes
            _rocketsCopy = _rockets;
            _invadersRocketsCopy = _invadersRockets;

            //Conversion en liste
            _rocketsCopy.ToList<Rocket>();
            _invadersRocketsCopy.ToList<Rocket>();
        }

        /// <summary>
        /// Méthode pour dessiner le score du joueur sur la console
        /// </summary>
        private void DrawScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Score: {_score}");
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
            //while (!_gameOver)
            {
                // Vérifie si une touche est disponible dans la console
                if (Console.KeyAvailable)
                {
                    // Récupère la touche enfoncée par l'utilisateur
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    // Déplace le joueur vers la gauche si la touche enfoncée est la flèche gauche et que le joueur n'est pas au bord gauche de la console
                    if (key.Key == ConsoleKey.LeftArrow && _playerPosition > 0)
                    {
                        _playerPosition--; // Décrémente la position du joueur
                    }
                    // Déplace le joueur vers la droite si la touche enfoncée est la flèche droite et que le joueur n'est pas au bord droit de la console
                    else if (key.Key == ConsoleKey.RightArrow && _playerPosition < (Console.WindowWidth - _player.Playersymbol.Length) - 1)
                    {
                        _playerPosition++; // Incrémente la position du joueur
                    }
                    // Tire un missile vers le haut si la touche enfoncée est la barre d'espace
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        //Condition pour ne tiré qu'u
                        if (_rockets.Count() < 1)
                        {
                            Rocket newRocket = new Rocket(_playerPosition, _player.Y -1);// Crée un nouveau missile à la position actuelle du joueur
                            newRocket.Activate(_playerPosition, _player.Y - 1); // Active le missile pour cibler les ennemis
                            _rockets.Add(newRocket); // Ajoute le missile à la liste des missiles du joueur
                            _rocketsCopy = _rockets;
                            _rocketsCopy.ToList<Rocket>();
                        }
                    }
                    // Déplace le joueur en fonction de sa position actuelle
                    _player.Move(_playerPosition);
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
            _invaders.Clear();

            int InvaderWidth = 5;
            int InvaderHeight = 1;
            int InvaderSpacingX = 5;
            int InvaderSpacingY = 1;

            // Déterminez les positions initiales pour le bloc d'envahisseurs
            int startX = (Console.WindowWidth - (InvaderWidth * 3)) / 2; // Position horizontale de départ
            int startY = 5; // Position verticale de départ

            // Boucle pour créer un bloc d'envahisseurs en rangée de 3 par 5
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 5; col++)
                {                    // Calculez la position horizontale pour chaque envahisseur dans la rangée
                    int invaderX = startX + col * (InvaderWidth + InvaderSpacingX);
                    // Calculez la position verticale pour chaque envahisseur dans la colonne
                    int invaderY = startY + row * (InvaderHeight + InvaderSpacingY);

                    // Créez un nouvel envahisseur avec les positions calculées
                    Invader invader = new Invader(invaderX, invaderY);

                    //Active les missile.
                    invader.IsActive = true;

                    // Ajoutez l'envahisseur à la liste des envahisseurs
                    _invaders.Add(invader);
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

            foreach (Invader invader in _invaders)
            {
                // Calculez la distance horizontale entre l'envahisseur et le joueur
                double distance = Math.Abs(invader.X - _playerPosition);

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
                
                Rocket newRocket = new Rocket(closestInvader.X, closestInvader.Y); // Créez un nouveau missile à la position de l'envahisseur
                newRocket.InvadersActivate(closestInvader.X, closestInvader.Y); // Active le missile pour cibler le joueur
                _invadersRockets.Add(newRocket); // Ajoute le missile à la liste des missiles des envahisseurs
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
                _bunkers.Add(new Bunker(Console.WindowWidth - 35 + (i), _player.Y - 5));
            }
            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 35 + (i), _player.Y - 6));
            }
            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 35 + (i), _player.Y - 7));
            }



            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 55 + (i), _player.Y - 5));
            }
            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 55 + (i), _player.Y - 6));
            }
            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 55 + (i), _player.Y - 7));
            }




            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 75 + (i), _player.Y - 5));
            }
            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 75 + (i), _player.Y - 6));
            }
            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 75 + (i), _player.Y - 7));
            }




            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 95 + (i), _player.Y - 5));
            }
            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 95 + (i), _player.Y - 6));
            }
            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < 10; i++)
            {
                _bunkers.Add(new Bunker(Console.WindowWidth - 95 + (i), _player.Y - 7));
            }




        }
    }
}