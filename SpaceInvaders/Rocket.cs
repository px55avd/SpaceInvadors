﻿///**************************************************************************************
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




namespace SpaceInvaders
{
    // Classe Rocket : représente un missile tiré par le joueur
    internal class Rocket
    {
        // Propriété X : position horizontale du missile
        public int X { get; private set; }
        // Propriété Y : position verticale du missile
        public int Y { get; private set; }
        // Indique si le missile est actif (en vol) ou non
        public bool IsActive { get; set; }

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
        }

        /// <summary>
        /// Méthode pour activer le missile et le faire partir du joueur
        /// </summary>
        /// <param name="playerX">Position horizontale du joueur</param>
        public void Activate(int playerX)
        {
            X = playerX;
            Y = Console.WindowHeight - 3; // Juste au-dessus du joueur
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
            Y = invadersY + 1; // Juste au-dessous de l'envahisseur
            IsActive = true;
        }

        /// <summary>
        /// Méthode Move : déplace le missile vers le haut (dans le sens de Y)
        /// </summary>
        /// <returns>True si le missile est toujours actif après le déplacement, sinon False</returns>
        public bool Move()
        {
            // Vérifie si le missile ne sort pas de l'écran vers le haut
            if (Y > 0)
            {
                Y--;
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
            if (Y < Console.WindowHeight)
            {
                Y++;
            }
            else
            {
                IsActive = false; // Désactive le missile s'il sort de l'écran
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
            return new Rectangle(X, Y, 2, 2); // Modifier les dimensions selon la taille du missile
        }

        /// <summary>
        /// Méthode Draw : dessine le missile à sa position actuelle sur la console
        /// </summary>
        public void Draw()
        {
            if (IsActive)
            {
                if (X > 0 && Y > 0)
                {
                    Console.SetCursorPosition(X, Y);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("|");
                }
            }
        }
    }
}



