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
using System.Runtime.CompilerServices;
using SpaceInvaders;
using System.Threading;
using System.Windows.Input;

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
            // Masque le curseur dans la console
            Console.CursorVisible = false;
           
            // Crée une instance de la classe Game pour démarrer le jeu
            Game game = new Game();
            game.Start();

            

            //Thread gameThread = new Thread(game.Run);
            //gameThread.SetApartmentState(ApartmentState.STA); // Définit le thread en mode STA
            //gameThread.Start();

            //gameThread.Join(); // Attend la fin de l'exécution du thread de jeu
        }
    }
}

