///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
/// Descrition de classe: La classe Rocket est utilisée pour créer et gérer les missiles tirés par le joueur et les envahisseurs dans le jeu Space Invaders. 
/// Elle permet de déplacer les missiles, de détecter les collisions avec d'autres objets du jeu et de les dessiner correctement sur la console.
using System;
using System.Drawing;
using System.IO;



namespace SpaceInvaders
{
    public class Rocket
    {
        /// <summary>
        /// Champ privé pour stocker la position horizontale du missile
        /// </summary>
        private int _x;

        /// <summary>
        /// Propriété publique X : position horizontale du missile
        /// </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Champ privé pour stocker la position verticale du missile
        /// </summary>
        private int _y;

        /// <summary>
        /// Propriété publique Y : position verticale du missile
        /// </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Champ privé pour stocker l'ancienne position horizontale du missile
        /// </summary>
        private int _oldX = 0;

        /// <summary>
        /// Propriété publique OldX : ancienne position horizontale du missile
        /// </summary>
        public int OldX
        {
            get { return _oldX; }
            set { _oldX = value; }
        }

        /// <summary>
        /// Champ privé pour stocker l'ancienne position verticale du missile
        /// </summary>
        private int _oldY = 0;

        /// <summary>
        /// Propriété publique OldY : ancienne position verticale du missile
        /// </summary>
        public int OldY
        {
            get { return _oldY; }
            private set { _oldY = value; }
        }

        /// <summary>
        /// Champ privé pour stocker l'apparence du missile
        /// </summary>
        private string _rocketSymbol = "|";

        /// <summary>
        /// Propriété publique Rocketsymbol : apparence du missile
        /// </summary>
        public string Rocketsymbol
        {
            get { return _rocketSymbol; }
            private set { _rocketSymbol = value; }
        }

        /// <summary>
        /// Champ privé pour indiquer si le missile est actif (en vol) ou non
        /// </summary>
        private bool _isActivate;

        /// <summary>
        /// Propriété publique IsActive : indique si le missile est actif (en vol) ou non
        /// </summary>
        public bool IsActive
        {
            get { return _isActivate; }
            set { _isActivate = value; }
        }


        /// <summary>
        /// Constructeur de la classe Rocket
        /// </summary>
        /// <param name="initialX">Position horizontale initiale du missile</param>
        /// <param name="initialY">Position verticale initiale du missile</param>
        public Rocket(int initialX, int initialY)
        {
            X = initialX;
            Y = initialY;
            IsActive = false;

            OldX = 10;
            OldY = 0;
        }

        /// <summary>
        /// Méthode pour activer le missile et le faire partir du joueur
        /// </summary>
        /// <param name="playerX">Position horizontale du joueur</param>
        public void Activate(int playerX, int playerY)
        {
            X = playerX;
            Y = playerY; // Juste au-dessus du joueur
            IsActive = true;
        }

        /// <summary>
        /// Méthode pour activer le missile et le faire partir des envahisseurs
        /// </summary>
        /// <param name="invadersX">Position horizontale de l'envahisseur</param>
        /// <param name="invadersY">Position verticale de l'envahisseur</param>
        public void InvadersActivate(int invadersX, int invadersY)
        {
            X = invadersX;
            Y = invadersY +1
                ; // Juste au-dessous de l'envahisseur
            IsActive = true;
        }

        /// <summary>
        /// Méthode Move : déplace le missile vers le haut (dans le sens de Y)
        /// </summary>
        /// <returns>True si le missile est toujours actif après le déplacement, sinon False</returns>
        public bool Move()
        {
            // Vérifie si le missile ne sort pas de l'écran vers le haut
            if (Y > 0 )
            {
                Y--; // diminie la position verticale 
            }
            else
            {
                IsActive = false; // Désactive le missile s'il sort de l'écran
                
               
            }
            return IsActive;
        }

        /// <summary>
        /// Méthode NegativMove : déplace le missile vers le bas (dans le sens de Y)
        /// </summary>
        /// <returns>True si le missile est toujours actif après le déplacement, sinon False</returns>
        public bool NegativMove()
        {
            // Vérifie si le missile ne sort pas de l'écran vers le bas
            if (Y < Console.WindowHeight - 1)
            {
                Y++;
            }
            else
            {
                IsActive = false; // Désactive le missile s'il sort de l'écran

                
                X = 120;
                Y = 0;
            }
            return IsActive;
        }

        /// <summary>
        /// Méthode pour obtenir la hitbox du missile
        /// </summary>
        /// <returns>Un rectangle représentant la hitbox du missile</returns>
        public Rectangle GetHitbox()
        {
            // Retourne un rectangle autour du missile pour détecter les collisions
            return new Rectangle(X, Y, 1, 1); // Modifier les dimensions selon la taille du missile
        }

        /// <summary>
        /// Méthode pour effacer les caractère 
        /// </summary>
        public static void Erase(int x, int y, int length)
        {
            string s = " ";
            Console.SetCursorPosition(x, y);

            // Efface les caractères à partir de la position spécifiée jusqu'à la longueur spécifiée
            for (int i = 0; i < length; i++)
            {
                Console.Write(s); // Remplace chaque caractère par un espace vide
            }
        }
        
        /// <summary>
        /// Méthode Draw : dessine le missile à sa position actuelle sur la console
        /// </summary>
        public void Draw()
        {
            if (IsActive is true)
            {
                // Efface l'ancienne position du joueur uniquement si elle a changé
                if (X > 0 && Y > 0 && (OldX != X || OldY != Y))
                {
                    Erase(OldX, OldY, Rocketsymbol.Length); // Efface un caractère à la position de l'ancien joueur

                    // Dessine le missile à sa nouvelle position      
                    Console.SetCursorPosition(X, Y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Rocketsymbol);

                    OldX = X;
                    OldY = Y;
                }                     
            }
        }

        /// <summary>
        /// Méthode pour effacer les caractère 
        /// </summary>
        public void Drawfinalposition()
        {
            Erase(OldX, OldY, Rocketsymbol.Length); // Efface un caractère à la position de l'ancien joueur  
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




