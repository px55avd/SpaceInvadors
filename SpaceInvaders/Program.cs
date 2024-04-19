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
using System.Runtime.CompilerServices;
using SpaceInvaders;
using System.Threading;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;

[assembly: InternalsVisibleToAttribute("Game")]

// Espace de noms SpicyInva der
namespace SpicyInvader
{
    // Classe Program : Point d'entrée du programme
    internal class Program
    {

        // Méthode principale du programme
        [STAThread]
        static void Main(string[] args)
        {
            // Définit le titre de la console
            Console.Title = "SpicyInvader";

            // Cache le curseur de la console
            Console.CursorVisible = false;

            //Instanciation d'un nouvelle objet Menu 
            Menu menu = new Menu(selectedIndexMenu: 0);

            //Mise en page dynamique du menu
            menu.ChangeBackColorConsole();

            //Appel de la méthode pour naviger
            menu.UserInput();


            //Thread gameThread = new Thread(game.Run);
            //gameThread.SetApartmentState(ApartmentState.STA); // Définit le thread en mode STA
            //gameThread.Start();

            //gameThread.Join(); // Attend la fin de l'exécution du thread de jeu
        }
    }
}

