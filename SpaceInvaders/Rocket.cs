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




// Espace de noms SpaceInvaders
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

        // Constructeur de la classe Rocket
        // Initialise la position horizontale et verticale du missile
        public Rocket(int initialX, int initialY)
        {
            X = initialX;
            Y = initialY;
            IsActive = false;
        }

        // Méthode pour activer le missile et le faire partir
        public void Activate(int playerX)
        {
            X = playerX;
            Y = Console.WindowHeight - 3; // Juste au-dessus du joueur
            IsActive = true;
        }

        public void InvadersActivate(int invadersX, int invadersY)
        {
            X = invadersX;
            Y = invadersY + 1; // Juste au-dessous de l'invader
            IsActive = true;
        }

        // Méthode Move : déplace le missile vers le haut (dans le sens de Y)
        public bool Move()
        {

            // Vérifie si le missile ne sort pas de l'écran vers le haut
            if (Y > 0)
            {
                Y--;
            }
            else
            {
                // Désactive le missile s'il sort de l'écran
                IsActive = false;
            }
            
            return IsActive;
        }

        public bool NegativMove()
        {
            // Vérifie si le missile ne sort pas de l'écran vers le haut
            if (Y < Console.WindowHeight)
            {
                Y++;
            }
            else
            {
                // Désactive le missile s'il sort de l'écran
                IsActive = false;
            }
                return IsActive;
        }

        // Méthode pour obtenir la hitbox du missile
        public Rectangle GetHitbox()
        {
            // Retourne un rectangle autour du missile pour détecter les collisions
            return new Rectangle(X, Y, 2, 2); // Modifier les dimensions selon la taille du missile
        }

        // Méthode Draw : dessine le missile à sa position actuelle sur la console
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


