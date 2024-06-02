///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
///Classe Program : Point d'entrée du programme.
using SpaceInvaders;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Game")]

// Espace de noms SpicyInva der
namespace SpicyInvader
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Définit le titre de la console
            Console.Title = "SpicyInvader";

            // Cache le curseur de la console
            Console.CursorVisible = false;

            // Disable vertical scrolling
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);



            //Instanciation d'un nouvelle objet Menu 
            Menu menu = new Menu();

            //Mise en page dynamique du menu principal
            menu.ChangeBackColorConsole(menu.Options);

            //Appel de la méthode pour naviguer dans les menus
            menu.UserinputMenu();
        }
    }
}

