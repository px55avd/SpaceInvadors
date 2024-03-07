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
        Random random = new Random();
        bool leftOrRight = false;


        // Propriété X : position horizontale de l'envahisseur
        public int X { get; private set; }
        // Propriété Y : position verticale de l'envahisseur
        public int Y { get; private set; }

        private int initialX;
        private int initialY;

        // Constructeur de la classe Invader
        // Initialise la position horizontale et verticale de l'envahisseur
        public Invader(int initialX, int initialY)
        {
            this.initialX = random.Next(Console.WindowWidth);
            this.initialY = random.Next(Console.WindowHeight);


            X = this.initialX = initialX;
            Y = this.initialY = initialY;
        }

        // Méthode Move : déplace l'envahisseur vers le bas (dans le sens de Y)
        public void Move()
        {
            

            if (leftOrRight is false)
            {
                X++;
            }
            else if (leftOrRight is true)
            {
                X--;
            }
            if (X == Console.WindowWidth - 10)
            {

                Y++;
                leftOrRight = true;
            }
            else if (X == 5)
            {

                Y++;
                leftOrRight = false;
            }

        }

        // Méthode Draw : dessine l'envahisseur à sa position actuelle sur la console
        public void Draw()
        {
            if (X == 110)
            {
                Console.SetCursorPosition(109, Y);
                Console.Write("     ");
                Y++;
                leftOrRight = true;
            }
            else if (X == 5)
            {
                Console.SetCursorPosition(6, Y);
                Console.Write("     ");
                Y++;
                leftOrRight = false;
            }
            else
            {
                // Positionne le curseur à la position de l'envahisseur
                Console.SetCursorPosition(X, Y);
                // Définit la couleur du texte pour représenter l'envahisseur
                Console.ForegroundColor = ConsoleColor.Red;
                // Affiche l'envahisseur comme un caractère 'X' de couleur rouge
                Console.Write("X");
            }

        }


        // Méthode pour obtenir la hitbox de l'envahisseur
        public Rectangle GetHitbox()
        {
            // Retourne un rectangle autour de l'envahisseur pour détecter les collisions
            return new Rectangle(X, Y, 2, 2); // Modifier les dimensions selon la taille de l'envahisseur
        }


        //Méthode pour réinitailiser la position du invader.
        public void Reset()
        {
            X = initialX;
            Y = initialY;
            this.initialX = random.Next(Console.WindowWidth);
            this.initialY = random.Next(Console.WindowHeight -8);   

        }
    }
}
