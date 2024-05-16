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
    // Classe Invader : représente un envahisseur dans le jeu SpaceInvaders
    internal class Invader
    {
        //Instantiation d'un objet Ramdom
        Random random = new Random();

        // Instantiation d'un bool pour controler la si les ennemi se déplace à gauche ou à droite
        bool leftOrRight = false;
        // Propriété X : position horizontale de l'envahisseur
        public int X { get; private set; }
        // Propriété Y : position verticale de l'envahisseur
        public int Y { get; private set; }
        // Propriété OldX : ancienne position horizontale de l'envahisseur
        public int OldX { get; private set; } = 0;
        // Propriété OldY : ancienne position verticale de l'envahisseur
        public int OldY { get; private set; } = 0;
        // Propriété Symbol : apparence de l'envahisseur
        public string Playersymbol { get; private set; } = "X";
        // Propriété IsActivate : L'etat de l'ennemi
        public bool IsActive { get; set; }
        // Initialise la position horizontale et verticale de l'envahisseur
        private int initialX = 0;
        private int initialY = 0;

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
        public void Move()
        {
            // Vérifie si l'envahisseur est actif avant de le déplacer
            if (IsActive)
            {
                // Vérifie la direction de déplacement (gauche ou droite)
                if (!leftOrRight) // Si leftOrRight est false, l'envahisseur se déplace vers la droite
                {
                    X++; // Déplace l'envahisseur d'une unité vers la droite
                }
                else // Sinon, l'envahisseur se déplace vers la gauche
                {
                    X--; // Déplace l'envahisseur d'une unité vers la gauche
                }

                // Vérifie si l'envahisseur atteint le bord droit de la console
                if (X == Console.WindowWidth - 10)
                {
                    Y++; // Déplace l'envahisseur vers le bas
                    leftOrRight = true; // Change la direction de déplacement vers la gauche
                }
                // Vérifie si l'envahisseur atteint le bord gauche de la console
                else if (X == 5)
                {
                    Y++; // Déplace l'envahisseur vers le bas
                    leftOrRight = false; // Change la direction de déplacement vers la droite
                }
            }
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
                    // Déplace l'envahisseur vers le bas
                    Y++;
                    // Change la direction de déplacement vers la gauche
                    leftOrRight = true;
                }
                // Vérifie si l'envahisseur atteint le bord gauche de la console
                else if (X == 5)
                {
                    // Efface l'envahisseur de sa position actuelle
                    Console.SetCursorPosition(6, Y);
                    Console.Write("     ");
                    // Déplace l'envahisseur vers le bas
                    Y++;
                    // Change la direction de déplacement vers la droite
                    leftOrRight = false;
                }
                else
                {
                    // Efface l'ancienne position du joueur uniquement si elle a changé
                    if (X > 0 && Y > 0 && (OldX != X || OldY != Y))
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

        /// <summary>
        /// 
        /// </summary>
        public void Drawfinalposiion()
        {
            Helper.Erase(OldX, OldY, Playersymbol.Length); // Efface un caractère à la position de l'ancien joueur  
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
        /// Méthode pour obtenir la hitbox de l'envahisseur
        /// </summary>
        /// <returns>Hitbox d'un envahisseur</returns>
        public Rectangle GetHitbox()
        {
            // Retourne un rectangle autour de l'envahisseur pour détecter les collisions
            return new Rectangle(X, Y, 4, 3); // Modifier les dimensions selon la taille de l'envahisseur
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
