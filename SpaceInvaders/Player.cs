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

        /// <summary>
        /// Constructeur de la classe Player
        /// </summary>
        /// <param name="initialX">Position horizontale initiale du joueur</param>
        public Player(int initialX)
        {
            X = initialX;
            Y = Console.WindowHeight - 2; // Juste au-dessus de la bordure inférieure
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
            return new Rectangle(X, Y, 1, 1); //Reectangle de taille 1 sur 1
        }

        /// <summary>
        /// Méthode Draw : dessine le joueur à sa position actuelle sur la console
        /// </summary>
        public void Draw()
        {
            // Positionne le curseur à la position du joueur
            Console.SetCursorPosition(X, Y);
            // Définit la couleur du texte pour représenter le joueur
            Console.ForegroundColor = ConsoleColor.Green;
            // Affiche le joueur comme une chaîne de caractère "<O>"
            Console.Write("<O>");
        }
    }
}

