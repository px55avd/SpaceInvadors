///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
/// Descrition de classe: La classe Invader est utilisée pour créer et gérer les envahisseurs dans le jeu Space Invaders. 
/// Chaque instance de la classe représente un unique envahisseur. Les méthodes permettent de déplacer et de dessiner l'envahisseur sur la console en fonction de sa position.
using System;
using System.Drawing;


// Espace de noms SpaceInvaders
namespace SpaceInvaders
{
    // Classe Invader : représente un envahisseur dans le jeu SpaceInvaders
    public class Invader
    {
        //Instantiation d'un objet Ramdom
        Random random = new Random();

        // Instantiation d'un bool pour controler la si les ennemi se déplace à gauche ou à droite
        private bool _leftOrRight = false;
        public bool LeftorRight { get { return _leftOrRight; } set { _leftOrRight = value; } }

        // Instantiation d'un bool pour controler la si les ennemi se déplace à gauche ou à droite
        private bool _down = false;
        public bool Down { get { return _down; } set { _down = value; } }


        // Propriété X : position horizontale de l'envahisseur
        private int _x;
        public int X { get { return _x; } private set { _x = value; } }


        // Propriété Y : position verticale de l'envahisseur
        private int _y;
        public int Y { get { return _y; }  set { _y = value; } }


        // Propriété OldX : ancienne position horizontale de l'envahisseur
        private int _oldX = 0;
        public int OldX { get { return _oldX; } private set { _oldX = value; } }


        // Propriété OldY : ancienne position verticale de l'envahisseur
        private int _oldY = 0;
        public int OldY { get { return _oldY; } private set { _oldY = value; } }


        // Propriété Symbol : apparence de l'envahisseur
        private string _invaderSymbol = ">(*-*)<";
        public string InvaderSymbol { get { return _invaderSymbol; }  set { _invaderSymbol = value; } }

        // Propriété Symbol : apparence de l'envahisseur
        private string _invaderSecondsymbol = "<|§¦§¦>";
        public string InvaderSecondsymbol { get { return _invaderSecondsymbol; } set { _invaderSecondsymbol = value; } }

        // Propriété Symbol : apparence de l'envahisseur
        private string _invaderThirdsymbol = "(°)-(°)";
        public string InvaderThirdsymbol { get { return _invaderThirdsymbol; } set { _invaderThirdsymbol = value; } }



        // Propriété Symbol : apparence de l'envahisseur Amiral
        private string _invaderBosssymbol = "|ÔÔ|";
        public string InvaderBosssymbol { get { return _invaderBosssymbol; } private set { _invaderBosssymbol = value; } }


        // Propriété IsActivate : L'etat de l'ennemi
        private bool _isActivate;
        public bool IsActive { get { return _isActivate; } set { _isActivate = value; } }

        // Propriété X : position horizontale de l'envahisseur
        private int _count = 0;
        public int Count { get { return _count; } private set { _count = value; } }

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

        public bool MoveinvaderBoss()
        {

            // Vérifie si le envahisseur Amiral ne sort pas de l'écran vers la gauche
            if (X > 0)
            {
                X--;
            }
            else
            {
                IsActive = false; // Désactive le missile s'il sort de l'écran


            }
                
            

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


        public void DrawbossInvader()
        {
            // Efface l'ancienne position du joueur uniquement si elle a changé
            if (X > 0 && (OldX != X))
            {
                Erase(OldX, OldY, InvaderSymbol.Length); // Efface un caractère à la position de l'ancien joueur

                for (int i = 0; i < Console.WindowWidth - 5; i++)
                {
                    Console.SetCursorPosition(i, Y);
                    Console.Write(" ");
                }


                // Dessine le missile à sa nouvelle position
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(InvaderSymbol);

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
