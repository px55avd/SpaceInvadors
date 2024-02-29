using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


// Espace de noms SpaceInvaders
namespace SpaceInvaders
{
    // Classe Player : représente le joueur dans le jeu SpaceInvaders
    internal class Player
    {
        

        // Propriété X : position horizontale du joueur
        public int X { get; private set; }
        // Propriété Y : position verticale du joueur
        public int Y { get; private set; }

        // Constructeur de la classe Player
        // Initialise la position horizontale du joueur et le place juste au-dessus de la bordure inférieure de la console
        public Player(int initialX)
        {
            X = initialX;
            Y = Console.WindowHeight - 2; // Juste au-dessus de la bordure inférieure
        }

        // Méthode Move : déplace le joueur horizontalement vers la position spécifiée
        public void Move(int newX)
        {
            X = newX;
            
        }

        // Méthode Draw : dessine le joueur à sa position actuelle sur la console
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

