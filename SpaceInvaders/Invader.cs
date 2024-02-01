using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

// Espace de noms SpaceInvaders
namespace SpaceInvaders
{
    // Classe Invader : représente un envahisseur dans le jeu SpaceInvaders
    internal class Invader
    {
        Random random = new Random(Console.WindowWidth);
        Random random2 = new Random(Console.WindowHeight);

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
            X = initialX;
            Y = initialY;

            initialY = random2.Next(Y);
            initialX = random2.Next(X);
            this.initialX = initialX;
            this.initialY = initialY;
        }

        // Méthode Move : déplace l'envahisseur vers le bas (dans le sens de Y)
        public void Move()
        {

            Y++;

            
        }

        // Méthode Draw : dessine l'envahisseur à sa position actuelle sur la console
        public void Draw()
        {
            // Positionne le curseur à la position de l'envahisseur
            Console.SetCursorPosition(X, Y);
            // Définit la couleur du texte pour représenter l'envahisseur
            Console.ForegroundColor = ConsoleColor.Red;
            // Affiche l'envahisseur comme un caractère 'X' de couleur rouge
            Console.Write("X");


        }


        //Méthode pour réinitailiser la position du invader.
        public void Reset()
        {
            X = initialX;
            Y = initialY;

        }
    }
}
