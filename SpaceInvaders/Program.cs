using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using SpaceInvaders;

[assembly: InternalsVisibleToAttribute("Game")]

// Espace de noms SpicyInvader
namespace SpicyInvader
{
    // Classe Program : Point d'entrée du programme
    internal class Program
    {
        // Méthode principale du programme
        static void Main(string[] args)
        {
            // Définit le titre de la console
            Console.Title = "SpicyInvader";
            // Masque le curseur dans la console
            Console.CursorVisible = false;

            // Crée une instance de la classe Game pour démarrer le jeu
            Game game = new Game();
            game.Start(); // Démarre le jeu
        }
    }
}

