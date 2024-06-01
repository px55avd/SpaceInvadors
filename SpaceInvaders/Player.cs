///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
/// Descrition de classe: La classe Player est utilisée pour créer et gérer le personnage du joueur dans le jeu Space Invaders. Elle permet de déplacer le joueur sur l'écran, 
/// de récupérer sa zone de collision pour la détection de collisions avec d'autres objets du jeu, et de le dessiner correctement sur la console.
using System;
using System.Drawing;

namespace SpaceInvaders
{
    // Classe Player : représente le joueur dans le jeu SpaceInvaders
    public class Player
    {
        // Propriété X : position horizontale du vaisseaux du joueur.
        private int _x;
        public int X { get { return _x; } private set { _x = value; } }


        // Propriété Y : position verticale du vaisseaux du joueur.
        private int _y;
        public int Y { get { return _y; } private set { _y = value; } }


        // Propriété OldX : ancienne position horizontale du vaisseaux du joueur.
        private int _oldX = 0;
        public int OldX { get { return _oldX; } private set { _oldX = value; } }


        // Propriété OldY : ancienne position verticaledu vaisseaux du joueur.
        private int _oldY = 0;
        public int OldY { get { return _oldY; } private set { _oldY = value; } }


        // Propriété Symbol : apparence du vaisseaux du vaisseaux du joueur.
        private string _playersymbol = "<ÔÔ>";
        public string Playersymbol { get { return _playersymbol; } private set { _playersymbol = value; } }


        /// <summary>
        /// Constructeur de la classe Player
        /// </summary>
        /// <param name="initialX">Position horizontale initiale du joueur</param>
        public Player(int initialX)
        {
            X = initialX;
            Y = Console.WindowHeight -2; // Juste au-dessus de la bordure inférieure

            OldX = 0;
            OldY = 0;
            
        }

        /// <summary>
        /// Méthode Move : déplace le joueur horizontalement vers la position spécifiée
        /// </summary>
        /// <param name="newX">la nouvelle position donné par le joueur</param>
        public void Move(int newX)
        {

            X = newX;
        }

        /// <summary>
        /// // Retourne un rectangle autour du joueur pour détecter les collisions
        /// </summary>
        /// <returns>Une Hitbox autour du joueur</returns>
        public Rectangle GetHitbox()
        {
            return new Rectangle(X, Y, Playersymbol.Length, 1); //Reectangle de taille 1 sur 1
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
        /// Méthode Draw : dessine le joueur à sa position actuelle sur la console
        /// </summary>
        public void Draw()
        {
            // Efface l'ancienne position du joueur uniquement si elle a changé
            if (X > 0 && (OldX != X))
            {
                Erase(OldX, OldY, Playersymbol.Length); // Efface un caractère à la position de l'ancien joueur

                for (int i = 0;i < Console.WindowWidth -5 ; i++)
                {
                    Console.SetCursorPosition(i, Y);
                    Console.Write(" ");
                }
                

                // Dessine le joueur à sa nouvelle position
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Playersymbol);

                OldX = X;
                OldY = Y;
            }
        }

        public Game Game
        {
            get => default;
            set
            {
            }
        }
    }
}

