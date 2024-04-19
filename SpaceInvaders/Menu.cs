using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    internal class Menu
    {
        // Indice de l'option sélectionnée dans le menu
        private int _selectedIndex = 0;

        // Propriété publique pour accéder à l'indice sélectionné
        public int selectedIndex { get; set; }

        // Options du menu
        private string[] _options = { "Lancer une nouvelle partie", "Option 2", "Option 3", "Quitter" };

        /// <summary>
        /// Constructeur de la classe Menu
        /// </summary>
        /// <param name="selectedIndexMenu">Index du menu</param>
        public Menu(int selectedIndexMenu)
        {
            _selectedIndex = selectedIndexMenu;
        }

        /// <summary>
        /// Méthode pour changer les couleurs de la console en fonction de l'option sélectionnée
        /// </summary>
        public void ChangeBackColorConsole()
        {
            Console.Clear();

            for (int i = 0; i < _options.Length; i++)
            {
                if (i == this.selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.WriteLine(_options[i]);
            }
        }

        /// <summary>
        /// Méthode pour gérer les entrées utilisateur dans le menu
        /// </summary>
        public void UserInput()
        {
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleKeyInfo key = new ConsoleKeyInfo();
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    // Déplace la sélection vers le haut
                    selectedIndex = Math.Max(0, this.selectedIndex - 1);
                    Console.ResetColor();
                    ChangeBackColorConsole();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    // Déplace la sélection vers le bas
                    selectedIndex = Math.Min(_options.Length - 1, this.selectedIndex + 1);
                    ChangeBackColorConsole();
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Si l'option sélectionnée est "Quitter", sort de la boucle
                    if (this.selectedIndex == _options.Length - 1)
                    {
                        break;
                    }

                    // Si l'option sélectionnée est "Lancer une nouvelle partie"
                    if (this.selectedIndex == _options.Length - _options.Length)
                    {
                        // Masque le curseur dans la console
                        Console.CursorVisible = false;

                        // Crée une instance de la classe Game pour démarrer le jeu
                        Game game = new Game();
                        game.Start();
                    }
                    else
                    {
                        // Affiche l'option sélectionnée
                        Console.WriteLine("\nVous avez sélectionné : " + _options[selectedIndex]);
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
