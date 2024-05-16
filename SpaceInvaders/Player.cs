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
using System.Drawing;
using System.Diagnostics;

namespace SpaceInvaders
{
    // Classe Player : représente le joueur dans le jeu SpaceInvaders
    internal class Player
    {
        // Propriété X : position horizontale du joueur
        public int X { get; private set; }
        // Propriété Y : position verticale du joueur
        public int Y { get; private set; }
        public int OldX { get; private set; } = 0;
        // Propriété OldY : ancienne position verticale du missile
        public int OldY { get; private set; } = 0;
        // Propriété Symbol : apparence du vaisseaux du joueur
        public string Playersymbol { get; private set; } = "<O>";

        /// <summary>
        /// Constructeur de la classe Player
        /// </summary>
        /// <param name="initialX">Position horizontale initiale du joueur</param>
        public Player(int initialX)
        {
            X = initialX;
            Y = Console.WindowHeight -1; // Juste au-dessus de la bordure inférieure

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
            return new Rectangle(X, Y, 3, 1); //Reectangle de taille 1 sur 1
        }

        /// <summary>
        /// Méthode pour effacer les caractère 
        /// </summary>
        public static class Helper
        {
            public static void Erase(int x, int y, int length)
            {
                Console.SetCursorPosition(x, y);

                // Efface les caractères à partir de la position spécifiée jusqu'à la longueur spécifiée
                for (int i = 0; i < length; i++)
                {
                    Console.Write(" "); // Remplace chaque caractère par un espace vide
                }
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
                Helper.Erase(OldX, OldY, Playersymbol.Length); // Efface un caractère à la position de l'ancien joueur
                OldX = X;
                OldY = Y;

                // Dessine le missile à sa nouvelle position
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(Playersymbol);
            }
        }
    }
}

