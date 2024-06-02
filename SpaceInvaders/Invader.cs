///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
/// Descrition de classe: La classe Invader est utilisée pour créer et gérer les envahisseurs dans le jeu Space Invaders. 
/// Chaque instance de la classe représente un unique envahisseur. Les méthodes permettent de déplacer et de dessiner l'envahisseur sur la console en fonction de sa position.
using System;
using System.Drawing;


namespace SpaceInvaders
{
    public class Invader
    {
        /// <summary>
        /// Propriété privée pour contrôler si les ennemis se déplacent à gauche ou à droite
        /// </summary>
        private bool _leftOrRight = false;

        /// <summary>
        /// Propriété publique pour accéder et définir la direction de déplacement des ennemis
        /// </summary>
        public bool LeftorRight { get { return _leftOrRight; } set { _leftOrRight = value; } }

        /// <summary>
        /// Propriété privée pour contrôler si les ennemis se déplacent vers le bas
        /// </summary>
        private bool _down = false;

        /// <summary>
        /// Propriété publique pour accéder et définir le déplacement vers le bas des ennemis
        /// </summary>
        public bool Down { get { return _down; } set { _down = value; } }

        /// <summary>
        /// Propriété privée pour la position horizontale de l'envahisseur
        /// </summary>
        private int _x;

        /// <summary>
        /// Propriété publique pour accéder et définir la position horizontale de l'envahisseur
        /// </summary>
        public int X { get { return _x; } private set { _x = value; } }

        /// <summary>
        /// Propriété privée pour la position verticale de l'envahisseur
        /// </summary>
        private int _y;

        /// <summary>
        /// Propriété publique pour accéder et définir la position verticale de l'envahisseur
        /// </summary>
        public int Y { get { return _y; } set { _y = value; } }

        /// <summary>
        /// Propriété privée pour l'ancienne position horizontale de l'envahisseur
        /// </summary>
        private int _oldX = 0;

        /// <summary>
        /// Propriété publique pour accéder et définir l'ancienne position horizontale de l'envahisseur
        /// </summary>
        public int OldX { get { return _oldX; } private set { _oldX = value; } }

        /// <summary>
        /// Propriété privée pour l'ancienne position verticale de l'envahisseur
        /// </summary>
        private int _oldY = 0;

        /// <summary>
        /// Propriété publique pour accéder et définir l'ancienne position verticale de l'envahisseur
        /// </summary>
        public int OldY { get { return _oldY; } private set { _oldY = value; } }

        /// <summary>
        /// Propriété privée pour le symbole représentant l'envahisseur
        /// </summary>
        private string _invaderSymbol = ">(*-*)<";

        /// <summary>
        /// Propriété publique pour accéder et définir le symbole de l'envahisseur
        /// </summary>
        public string InvaderSymbol { get { return _invaderSymbol; } set { _invaderSymbol = value; } }

        /// <summary>
        /// Propriété privée pour le deuxième symbole représentant l'envahisseur
        /// </summary>
        private string _invaderSecondsymbol = "<|§¦§¦>";

        /// <summary>
        /// Propriété publique pour accéder et définir le deuxième symbole de l'envahisseur
        /// </summary>
        public string InvaderSecondsymbol { get { return _invaderSecondsymbol; } set { _invaderSecondsymbol = value; } }

        /// <summary>
        /// Propriété privée pour le troisième symbole représentant l'envahisseur
        /// </summary>
        private string _invaderThirdsymbol = "(°)-(°)";

        /// <summary>
        /// Propriété publique pour accéder et définir le troisième symbole de l'envahisseur
        /// </summary>
        public string InvaderThirdsymbol { get { return _invaderThirdsymbol; } set { _invaderThirdsymbol = value; } }

        /// <summary>
        /// Propriété privée pour le symbole représentant l'envahisseur Amiral
        /// </summary>
        private string _invaderBosssymbol = "|ÔÔ|";

        /// <summary>
        /// Propriété publique pour accéder et définir le symbole de l'envahisseur Amiral
        /// </summary>
        public string InvaderBosssymbol { get { return _invaderBosssymbol; } private set { _invaderBosssymbol = value; } }

        /// <summary>
        /// Propriété privée pour indiquer si l'envahisseur est actif
        /// </summary>
        private bool _isActivate;

        /// <summary>
        /// Propriété publique pour accéder et définir l'état de l'envahisseur
        /// </summary>
        public bool IsActive { get { return _isActivate; } set { _isActivate = value; } }

        /// <summary>
        /// Propriété privée pour compter le nombre d'envahisseurs
        /// </summary>
        private int _count = 0;

        /// <summary>
        /// Propriété publique pour accéder et définir le nombre d'envahisseurs
        /// </summary>
        public int Count { get { return _count; } private set { _count = value; } }

        /// <summary>
        /// Propriété privée pour le nombre actuel d'envahisseurs
        /// </summary>
        private int _currentNumberofInvaders = 0;


        /// <summary>
        /// Constructeur de la classe Invader
        /// </summary>
        /// <param name="initialX"></param>
        /// <param name="initialY"></param>
        public Invader(int initialX, int initialY)
        {
            X= initialX;
            Y = initialY;

            OldX = 10;
            OldY = 0;
        }

        /// <summary>
        /// Méthode Move : déplace l'envahisseur vers le bas (dans le sens de Y)
        /// </summary>
        public bool Move(int numberOfinvaders)
        {
            _currentNumberofInvaders = numberOfinvaders;

            // Vérifie si l'envahisseur est actif avant de le déplacer
            if (IsActive || InvaderSymbol == _invaderBosssymbol)
            {
                // Vérifie la direction de déplacement (gauche ou droite)
                if (!_leftOrRight) // Si leftOrRight est false, l'envahisseur se déplace vers la droite
                {
                    X++; // Déplace l'envahisseur d'une unité vers la droite
                    _down = false;
                }
                else // Sinon, l'envahisseur se déplace vers la gauche
                {
                    X--; // Déplace l'envahisseur d'une unité vers la gauche
                    _down = false;
                }
                
                {
                    // Vérifie si l'envahisseur atteint le bord droit de la console
                    if (X == Console.WindowWidth - 10)
                    {
                        _down = true;
                        //_leftOrRight = true; // Change la direction de déplacement vers la gauche
                    }
                    // Vérifie si l'envahisseur atteint le bord gauche de la console
                    else if (X == 5)
                    {
                        _down = true;
                        //_leftOrRight = false; // Change la direction de déplacement vers la droite
                    }
                }
            }
            return _down;
        }

        /// <summary>
        /// Méthode pour déplacer l'envahisseur Amiral.
        /// </summary>
        /// <returns>Retourne true si l'envahisseur est actif, false sinon.</returns>
        public bool MoveinvaderBoss()
        {
            // Vérifie si l'envahisseur Amiral ne sort pas de l'écran vers la gauche
            if (X > 0)
            {
                // Déplace l'envahisseur Amiral d'une position vers la gauche
                X--;
            }
            else
            {
                // Désactive l'envahisseur Amiral s'il sort de l'écran
                IsActive = false;
            }

            // Retourne l'état de l'envahisseur Amiral (actif ou non)
            return IsActive;
        }

        /// <summary>
        /// Méthode Draw : dessine l'envahisseur à sa position actuelle sur la console
        /// </summary>
        public void Draw()
        {
            // Vérifie si l'envahisseur est actif avant de le dessiner
            if (IsActive)
            {
                // Vérifie si l'envahisseur atteint le bord droit de la console
                if (X == 110)
                {
                    // Efface l'envahisseur de sa position actuelle
                    Console.SetCursorPosition(109, Y);
                    Console.Write("     ");
                    // Change la direction de déplacement vers la gauche
                }
                // Vérifie si l'envahisseur atteint le bord gauche de la console
                else if (X == 5)
                {
                    // Efface l'envahisseur de sa position actuelle
                    Console.SetCursorPosition(6, Y);
                    Console.Write("     ");
                }
                else
                {
                    // Efface l'ancienne position du joueur uniquement si elle a changé
                    if (X > 0 && Y > 0 && (OldX != X || OldY != Y))
                    {
                        Erase(OldX, OldY, InvaderSymbol.Length); // Efface un caractère à la position de l'ancien joueur
                        OldX = X;
                        OldY = Y;

                        // Dessine le missile à sa nouvelle position
                        Console.SetCursorPosition(X, Y);

                        // Couleur pour les vaisseaux générique
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        
                        Console.Write(InvaderSymbol);// Affiche l'envahisseur
                    }
                }
            }
        }

        /// <summary>
        /// Méthode pour dessiner l'envahisseur Amiral sur la console.
        /// </summary>
        public void DrawBossInvader()
        {
            // Efface l'ancienne position de l'envahisseur Amiral uniquement si elle a changé
            if (X > 0 && (OldX != X))
            {
                // Efface le symbole de l'envahisseur Amiral à sa position précédente
                Erase(OldX, OldY, InvaderSymbol.Length);

                // Efface la ligne entière où se trouve l'envahisseur Amiral pour préparer l'affichage à la nouvelle position
                for (int i = 0; i < Console.WindowWidth - 5; i++)
                {
                    Console.SetCursorPosition(i, Y);
                    Console.Write(" ");
                }

                // Dessine l'envahisseur Amiral à sa nouvelle position
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(InvaderSymbol);

                // Met à jour les anciennes positions pour la prochaine itération
                OldX = X;
                OldY = Y;
            }
        }

        /// <summary>
        /// Méthode pour effacer les dernieres positions des envahisseurs.
        /// </summary>
        public void Drawfinalposiion()
        {
            Erase(OldX, OldY, InvaderSymbol.Length); // Efface un caractère à la position de l'ancien joueur  
        }

        /// <summary>
        /// Méthode pour effacer les caractère 
        /// </summary>
        public static void Erase(int x, int y, int length)
        {
            Console.SetCursorPosition(x, y);

            // Efface les caractères à partir de la position spécifiée jusqu'à la longueur spécifiée
            for (int i = 0; i < length; i++)
            {
                Console.Write(" "); // Remplace chaque caractère par un espace vide
            }
        }

        /// <summary>
        /// Méthode pour obtenir la hitbox de l'envahisseur
        /// </summary>
        /// <returns>Hitbox d'un envahisseur</returns>
        public Rectangle GetHitbox()
        {
            // Retourne un rectangle autour de l'envahisseur pour détecter les collisions
            return new Rectangle(X, Y, InvaderSymbol.Length, 3); // Modifier les dimensions selon la taille de l'envahisseur
        }

        internal Game Game
        {
            get => default;
            set
            {
            }
        }
    }
}
