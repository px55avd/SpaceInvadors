///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
/// Descrition de classe: La classe gère la logique du jeu, récupere les entrée clavier de l'utilisateur avec la méthode
/// UserInput() qui est présente dans la méthode Update() qui elle met à jour les éléments afficher tel que le vaisseaux du jour,
/// les missiles et les bunkers et les vaisseaux ennimi
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;
using System.Reflection;

[assembly: InternalsVisibleToAttribute("TestunitSpaceinvader")] // Accès de classe au test unitaire. 

namespace SpaceInvaders
{
    public class Game
    {
        /// <summary>
        /// Position du joueur
        /// </summary>
        private int _playerPosition;

        /// <summary>
        /// Propriété publique pour accéder et définir la position du joueur
        /// </summary>
        public int PlayerPosition
        {
            get { return _playerPosition; }
            set { _playerPosition = value; }
        }

        /// <summary>
        /// Indique si le jeu est terminé
        /// </summary>
        private bool _gameOver;

        /// <summary>
        /// Propriété publique pour accéder et définir l'état de fin de jeu
        /// </summary>
        public bool GameOver
        {
            get { return _gameOver; }
            set { _gameOver = value; }
        }

        /// <summary>
        /// Nombre de vies du joueur
        /// </summary>
        private int _playerLives;

        /// <summary>
        /// Propriété publique pour accéder et définir le nombre de vies du joueur
        /// </summary>
        public int PlayerLives
        {
            get { return _playerLives; }
            set { _playerLives = value; }
        }

        /// <summary>
        /// Score du joueur
        /// </summary>
        private int _score = 0;

        /// <summary>
        /// Propriété publique pour accéder et définir le score du joueur
        /// </summary>
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// Indique le mode de jeu de la partie
        /// </summary>
        private int _gameMode;

        /// <summary>
        /// Propriété publique pour accéder et définir le mode de jeu
        /// </summary>
        public int GameMode
        {
            get { return _gameMode; }
            set { _gameMode = value; }
        }

        /// <summary>
        /// Vitesse du jeu
        /// </summary>
        private int _speedGame;

        /// <summary>
        /// Liste des envahisseurs
        /// </summary>
        private List<Invader> _invaders;

        /// <summary>
        /// Propriété publique pour accéder et définir la liste des envahisseurs
        /// </summary>
        public List<Invader> Invaders
        {
            get { return _invaders; }
            set { _invaders = value; }
        }

        /// <summary>
        /// Le joueur
        /// </summary>
        private Player _player;

        /// <summary>
        /// Propriété publique pour accéder et définir le joueur
        /// </summary>
        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        /// <summary>
        /// L'envahisseur Amiral
        /// </summary>
        private Invader _invaderBoss;

        /// <summary>
        /// Propriété publique pour accéder et définir l'envahisseur Amiral
        /// </summary>
        public Invader InvaderBoss
        {
            get { return _invaderBoss; }
            set { _invaderBoss = value; }
        }

        /// <summary>
        /// Liste des missiles tirés par le joueur
        /// </summary>
        private List<Rocket> _rockets;

        /// <summary>
        /// Propriété publique pour accéder et définir la liste des missiles tirés par le joueur
        /// </summary>
        public List<Rocket> Rockets
        {
            get { return _rockets; }
            set { _rockets = value; }
        }

        /// <summary>
        /// Liste des missiles tirés par les envahisseurs
        /// </summary>
        private List<Rocket> _invadersRockets;

        /// <summary>
        /// Propriété publique pour accéder et définir la liste des missiles tirés par les envahisseurs
        /// </summary>
        public List<Rocket> InvadersRockets
        {
            get { return _invadersRockets; }
            set { _invadersRockets = value; }
        }

        /// <summary>
        /// Liste des bunkers
        /// </summary>
        private List<Bunker> _bunkers;

        /// <summary>
        /// Propriété publique pour accéder et définir la liste des bunkers
        /// </summary>
        public List<Bunker> Bunkers
        {
            get { return _bunkers; }
            set { _bunkers = value; }
        }

        /// <summary>
        /// Copie de la liste des envahisseurs pour éviter les modifications concurrentes
        /// </summary>
        private List<Invader> _invadersCopy;

        /// <summary>
        /// Copie de la liste des missiles tirés par le joueur pour éviter les modifications concurrentes
        /// </summary>
        private List<Rocket> _rocketsCopy;

        /// <summary>
        /// Propriété publique pour accéder et définir la copie de la liste des missiles tirés par le joueur
        /// </summary>
        public List<Rocket> RocketsCopy
        {
            get { return _rocketsCopy; }
            set { _rocketsCopy = value; }
        }

        /// <summary>
        /// Copie de la liste des missiles tirés par les envahisseurs pour éviter les modifications concurrentes
        /// </summary>
        private List<Rocket> _invadersRocketsCopy;

        /// <summary>
        /// Propriété publique pour accéder et définir la copie de la liste des missiles tirés par les envahisseurs
        /// </summary>
        public List<Rocket> InvadersRocketsCopy
        {
            get { return _invadersRocketsCopy; }
            set { _invadersRocketsCopy = value; }
        }


        /// <summary>
        /// Constructeur de la classe Game
        /// </summary>
        public Game()
        {
            // Initialise les champs
            _playerPosition = Console.WindowWidth / 2;
            _invaderBoss = new Invader(0, 0);
            _playerLives = 3;
            _score = 0;
            _gameOver = false;
            _gameMode = 0;
            _speedGame = 0;
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
            Console.ForegroundColor = ConsoleColor.Red;
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

            // Variable ajustable aux niveaux de difficulté selectionnée par l'utilisateur
            int numberRocketmoduloMove = 0;
            int numberInvadermoduloMove = 0;
            int numberInvaderMissilefrequence = 0;

            //Ajustement de la vitesse de déplacement des éléments à l'écran selon le niveau de difficulté
            switch (_gameMode)
            {
                case 0:
                    numberRocketmoduloMove = 2;
                    numberInvadermoduloMove = 3;
                    numberInvaderMissilefrequence = 3;
                    break;

                case 1:
                    numberRocketmoduloMove = 2;
                    numberInvadermoduloMove = 2;
                    numberInvaderMissilefrequence = 2;
                    break;

                case 2:
                    numberRocketmoduloMove = 1;
                    numberInvadermoduloMove = 2;
                    numberInvaderMissilefrequence = 0;
                    break;

                default:
                    numberRocketmoduloMove = 2;
                    numberInvadermoduloMove = 3;
                    numberInvaderMissilefrequence = 3;
                    break;
            }

            //Déplacement des missiles du joueur
            for (int j = 0; j < _rocketsCopy.Count; j++)
            {
                //Instanciation d'une variable booléene pour récupérer le retour de la méthode Move().
                bool soFar;

                Rocket rocket = _rocketsCopy[j];

                if (_speedGame % randommove == numberRocketmoduloMove)
                {
                    soFar = rocket.Move();//Déplace les missile du joueurs
                    
                    //Condition si le missile sort de l'écran.
                    if (soFar is false)
                    {
                        rocket.Drawfinalposition();//Efface le dernier caractère affiché

                        _rocketsCopy.Remove(rocket);// Supprime le  missile de la liste.
                    }
                }

                // Parcourt la liste copie d'ennemis
                for (int i = 0; i < _invadersCopy.Count; i++)
                {
                    Invader invader = _invaders[i]; // Attribution des chaque envahisseur de la liste

                    // Vérifie si la hitbox du missile du joueur entre en collision avec la hitbox de l'envahisseur
                    if (rocket.GetHitbox().IntersectsWith(invader.GetHitbox()))
                    {

                        rocket.IsActive = false; // Désactive le missile
                        invader.IsActive = false; // Déscative l'envahisseur

                        //Effacer le caractère
                        rocket.Drawfinalposition();

                        //supprime le missile de la liste
                        _rockets.Remove(rocket);

                        invader.Drawfinalposiion(); //Effacer le caractère 
                        _invaders.Remove(invader); //supprime l'envahisseur de la liste

                        //Vérifie la liste d'envahisseur 
                        if (_invaders.Count % 3 == 0)
                        {
                            if (_invaderBoss.IsActive is false)
                            {
                                _invaderBoss = new Invader((Console.WindowWidth - _invaderBoss.InvaderBosssymbol.Length) - 1, Console.WindowHeight - 28);

                                _invaderBoss.IsActive = true; // active envahisseur Amiral

                                _invaderBoss.InvaderSymbol = _invaderBoss.InvaderBosssymbol; // lui attribue le bon design
                            }
                        }

                        //Vérifie s'il n'y plus d'envahisseur
                        if (_invaders.Count() == 0)
                        {
                            //Relance un paquet d'ennmis
                            InitializeInvaders();
                        }
                        _speedGame++; // Incrémente la vitesse du jeu
                        _score += 20; // Augmente le score de 20 points
                    }

                    // Vérifie si la hitbox du missile du joueur entre en collision avec la hitbox de l'envahisseur Amiral
                    if (rocket.GetHitbox().IntersectsWith(_invaderBoss.GetHitbox()))
                    {

                        rocket.IsActive = false; // désactive le missile
                        if (rocket.IsActive is false)
                        {
                            _score += 15; // Augmente le score  
                        }
                        rocket.Drawfinalposition(); // efface le dernier caractère affiché
                        _rockets.Remove(rocket);// Enlève le missile de la liste


                        _invaderBoss.IsActive = false; // desactive le vaisseaux amiral
                        //rocket.Y = 0; 
                        //rocket.X = 120;
                        _invaderBoss.Drawfinalposiion(); //Effacer le caractère du vaisseau Amiral

                        
                    }
                }

                // Parcourt les bunkers.
                for (int k = 0; k < _bunkers.Count; k++)
                {
                    Bunker bunker = _bunkers[k];

                    // Vérifie si la hitbox missile joueur entre en collision avec la hitbox du bunker
                    if (rocket.GetHitbox().IntersectsWith(bunker.GetHitbox()))
                    {

                        rocket.IsActive = false; // désactive le missile
                        rocket.Drawfinalposition(); // efface le dernier caractère affiché
                        _rockets.Remove(rocket);// Enlève le missile de la liste

                        // Vérifie si le bunker est endommagé.
                        if (bunker.Damaged == true && bunker.BunkerSymbol == bunker.DamagedBunkerSymbol)
                        {
                            bunker.Drawfinalposition();//efface le dernier cractère afficher
                            _bunkers.Remove(bunker);// retire le bunker de la liste
                        }
                        else
                        {
                            bunker.TakeDamage();// Appel de la méthode pour endommagé le bunker.
                        }
                    }
                }

                //parcourt les missiles ennmies.
                for (int l = 0; l < _invadersRocketsCopy.Count; l++)
                {
                    Rocket rocketOne = _invadersRocketsCopy[l];

                    // Vérifie si la hitbox missile joueur entre en collision avec la hitbox du mssile de l'envahisseur.
                    if (rocket.GetHitbox().IntersectsWith(rocketOne.GetHitbox()))
                    {
                        rocket.IsActive = false; // désactive le missile
                        rocket.Drawfinalposition(); // efface le dernier caractère affiché
                        _rockets.Remove(rocket);// Enlève le missile de la liste


                        rocketOne.IsActive = false; // désactive le missile
                        rocketOne.Drawfinalposition(); // efface le dernier caractère affiché
                        _rockets.Remove(rocketOne);// Enlève le missile de la liste

                        _score += 5; // Augmente le score de 5 points
                    }
                }
            }

            //Déplacement des missiles des ennemi
            for (int j = 0; j < _invadersRocketsCopy.Count; j++)
            {
                //Instanciation d'une variable boléene pour récupérer le retour de la méthode Move().
                bool soFar;

                Rocket rocket = _invadersRocketsCopy[j];

                // Vitesse des missiles ennemis
                if (_speedGame % randommove == numberRocketmoduloMove)
                {
                    //Récupérer le retour de la méthode
                    soFar = rocket.NegativMove();

                    //Condition si le missile sort de l'écran.
                    if (soFar is false)
                    {
                        rocket.Drawfinalposition();// efface les dernière position

                        _rocketsCopy.Remove(rocket);// Supprime le  missile de la liste.
                    }
                }

                // Vérifie si la hitbox du missile entre en collision avec la hitbox de du joueur
                if (rocket.GetHitbox().IntersectsWith(_player.GetHitbox()))
                {
                    rocket.IsActive = false; // desactive le missile 
                    rocket.Drawfinalposition(); // efface le dernier caractère
                    _invadersRocketsCopy.Remove(rocket); // supprime le missile de la liste

                    _playerLives = _playerLives - 1; // diminue la vie du joueur

                    // Vérifie les points de vie du joueur
                    if (_playerLives < 1)
                    {
                        _gameOver = true; // fin du jeu
                        break;
                    }
                }

                // Parcourt la liste des bunkers
                for (int k = 0; k < _bunkers.Count; k++)
                {
                    Bunker bunker = _bunkers[k];

                    // Vérifie si la hitbox du missile envahisseur entre en collision avec la hitbox du bunker
                    if (rocket.GetHitbox().IntersectsWith(bunker.GetHitbox()))
                    {
                        
                        rocket.IsActive = false; // desactive le missile 
                        rocket.Drawfinalposition(); // efface le dernier caractère

                        _invadersRocketsCopy.Remove(rocket);


                        // Vérifie si le bunker est endommagé
                        if (bunker.BunkerSymbol == bunker.DamagedBunkerSymbol)
                        {
                            bunker.Drawfinalposition(); // efface le mur a ses dernières positions
                            _bunkers.Remove(bunker); // retire le bunker de la lsite
                        }
                        else
                        {
                            bunker.TakeDamage(); // endommage le bunker
                        }
                    }
                }
            }


            // parcourt ma liste copie d'envahisseuer 
            for (int i = 0; i < _invadersCopy.Count; i++)
            {
                //pour randomisé le tire de l'ennemi le plus proches
                Random random = new Random();

                //Instanciation d'une variable boléene pour récupérer le retour de la méthode Move().
                bool soDown;

                Invader invader = _invaders[i];

                if (_speedGame % randommove == numberInvadermoduloMove)
                {
                    //Récupérer le retour de la méthode
                    soDown = invader.Move(_invaders.Count);

                    //si les envahisseur doivent descendre
                    if (soDown is true)
                    {
                        // Parcourt la liste des envahisseurs
                        for (int j = 0; j < _invaders.Count; j++)
                        {
                            // Récupère l'envahisseur à l'indice j
                            Invader invaderBis = _invaders[j];

                            // Déplace l'envahisseur vers le bas
                            invaderBis.Y++;

                            // Inverse la direction de déplacement de l'envahisseur
                            if (!invaderBis.LeftorRight)
                            {
                                // Si l'envahisseur se déplace vers la gauche, change la direction vers la droite
                                invaderBis.LeftorRight = true;
                            }
                            else if (invaderBis.LeftorRight)
                            {
                                // Si l'envahisseur se déplace vers la droite, change la direction vers la gauche
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
                    if (_speedGame % randomtouch == numberInvaderMissilefrequence)
                    {
                        // Si oui, tirez un missile
                        FireClosestInvaderRocket();
                    }
                }
               
                
                //Vérifie que les invaders ne sont pas arrivés au niveau du joueur.
                if (invader.Y == Console.WindowHeight - 1)
                {
                    _gameOver = true;
                    break;
                }
            }


            if (_speedGame % randommove == numberRocketmoduloMove)
            {
                bool soFarboss;

                soFarboss = _invaderBoss.MoveinvaderBoss();//Déplace le vaisseau amiral

                // Dessine l'envahisseur Amiral
                _invaderBoss.DrawBossInvader();

                //Condition si le missile sort de l'écran.
                if (soFarboss is false)
                {
                    _invaderBoss.Drawfinalposiion();//Efface le dernier caractère affiché
                }
            }

            _speedGame++; // Incrémente la vitesse à chaque mise à jour

            // Crée une copie des liste des missiles pour éviter les modifications concurrentes
            _rocketsCopy = _rockets;
            _invadersRocketsCopy = _invadersRockets;
            _invadersCopy = _invaders;

            //Conversion en liste
            _rocketsCopy.ToList<Rocket>();
            _invadersRocketsCopy.ToList<Rocket>();
            _invadersCopy.ToList<Invader>();
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

            // Dessine le score 
            DrawScore();

            // Dessine les points de vie du joueur
            DrawplayerLive();

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
        /// Méthode pour dessiner les point de vie  du joueur sur la console
        /// </summary>
        private void DrawplayerLive()
        {
            Console.SetCursorPosition(0, Console.WindowHeight -1);
            if (_playerLives == 3)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            if (_playerLives == 2) 
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            if (_playerLives == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.Write($"Vie : {_playerLives}");
        }

        /// <summary>
        /// Méthode pour gérer la saisie utilisateur
        /// </summary>
        private void UserInput()
        {
            // Initialisation d'un générateur de nombres aléatoires
            Random random = new Random();

            // Déclaration d'une variable pour la valeur maximale d'un nombre aléatoire
            int randomtouch = 30;

            // Génération d'un nombre aléatoire entre 0 et randomtouch (exclus)
            random.Next(randomtouch);

            // Initialisation d'une variable pour le nombre de missiles
            int numberMissile = 0;


            // Utilisation d'une instruction switch pour définir le nombre de missiles en fonction du mode de jeu (_gameMode)
            switch (_gameMode)
            {
                // Si _gameMode vaut 0, alors le nombre de missiles est 1
                case 0:
                    numberMissile = 1;
                    break;

                // Si _gameMode vaut 1, alors le nombre de missiles est 2
                case 1:
                    numberMissile = 2;
                    break;

                // Si _gameMode vaut 2, alors le nombre de missiles est 4
                case 2:
                    numberMissile = 4;
                    break;

                // Par défaut, si _gameMode a une valeur différente, le nombre de missiles est 1
                default:
                    numberMissile = 1;
                    break;
            }

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
                    if (_rockets.Count() < numberMissile)
                    {
                        Rocket newRocket = new Rocket(_playerPosition + 1, _player.Y -1);// Crée un nouveau missile à la position actuelle du joueur
                        newRocket.Activate(_playerPosition + 1 , _player.Y - 1); // Active le missile pour cibler les ennemis
                        _rockets.Add(newRocket); // Ajoute le missile à la liste des missiles du joueur
                        _rocketsCopy = _rockets;
                        _rocketsCopy.ToList<Rocket>();
                    }
                }
                // Déplace le joueur en fonction de sa position actuelle
                _player.Move(_playerPosition);
            }  
        }

        /// <summary>
        /// Méthode pour initialiser les envahisseurs du jeu
        /// </summary>
        public void InitializeInvaders()
        {
            _invaders.Clear(); // efface tous les éléments de la liste.

            int InvaderWidth = 5;
            int InvaderHeight = 1;
            int InvaderSpacingX = 5;
            int InvaderSpacingY = 1;

            // Déterminez les positions initiales pour le bloc d'envahisseurs
            int startX = ((Console.WindowWidth - (InvaderWidth * 3)) / 2) + 3; // Position horizontale de départ
            int startY = 5; // Position verticale de départ

            // Boucle pour créer un bloc d'envahisseurs en rangée de 3 par 5
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 5; col++)
                {   
                    // Calculez la position horizontale pour chaque envahisseur dans la rangée
                    int invaderX = startX + col * (InvaderWidth + InvaderSpacingX);
                    // Calculez la position verticale pour chaque envahisseur dans la colonne
                    int invaderY = startY + row * (InvaderHeight + InvaderSpacingY);

                    // Créez un nouvel envahisseur avec les positions calculées
                    Invader invader = new Invader(invaderX, invaderY);

                    // Vérifie le numéro de la ligne
                    if (row == 1)
                    {
                        // Attribue un nouveau design aux ennemi de la troisième ligne
                        invader.InvaderSymbol = invader.InvaderSecondsymbol;
                    }

                    // Vérifie le numéro de la ligne
                    if (row == 2)
                    {
                        // Attribue un nouveau design aux ennemi de la troisième ligne
                        invader.InvaderSymbol = invader.InvaderThirdsymbol; 
                    }

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
            // nombre de mur dans une ligne d'un bunker
            int  maxNumberwallInbunker = 10;

            // Nombre de bunkers
            const int MAXNUMBERBUNKER = 4;

            // decalage des bunker par rapport au joueur.
            const int POSITIONYFORALLBUNKER = 4;

            // Position X du premier mur du bunker
            const int XPOSITIONFIRSTWALL = 35;

            // Nombre a ajouter pour créer la distance avec le prochain bunker
            const int ADDNUMBERFORNEXTBUKER = 20;

            // nombre total de ligne de mur du bunker
            int maxNumberlineBunker = 2;

            // Ajoutez des bunkers à des positions spécifiques
            for (int i = 0; i < maxNumberlineBunker; i++)
            {
                for (int j = 0; j < MAXNUMBERBUNKER; j++)
                {   
                    for (int k = 0; k < maxNumberwallInbunker; k++)
                    {
                        _bunkers.Add(new Bunker(Console.WindowWidth - (XPOSITIONFIRSTWALL + ADDNUMBERFORNEXTBUKER * j) + k, _player.Y - (i + POSITIONYFORALLBUNKER)));
                    }
                }
            }
        }
    }
}