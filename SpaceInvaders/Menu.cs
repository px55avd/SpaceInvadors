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
        private string[] _options = { "Lancer une nouvelle partie", "Difficulté", "Son", "Quitter" };

        private string[] _optionsForIndex2 = { "Facile", "Moyen", "Difficle", "Retour", "Quitter"};

        private string[] _optionsForIndex3 = { "Activer", "Désactiver","Retour", "Quitter" };

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


                if (this.selectedIndex == _options.Length - 3)
                {
                    Console.Clear();
                    this.selectedIndex = 0;
                    

                    for (int j = 0; j < _optionsForIndex2.Length; j++)
                    {
                        

                        if (j == this.selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        
                        Console.WriteLine(_optionsForIndex2[j]);

                        break;
                    }

                    
                }

                if (this.selectedIndex == _options.Length - 3)
                {
                    Console.Clear();
                    this.selectedIndex = 0;

                    for (int j = 0; j < _optionsForIndex3.Length; j++)
                    {
                        

                        if (j == this.selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        Console.WriteLine(_optionsForIndex3[j]);
                    }

                    break;
                }
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
                    
                    selectedIndex = Math.Max(0, this.selectedIndex - 1); // Déplace la sélection vers le haut
                    Console.ResetColor(); //Réinitialiser les couleurs de la console
                    ChangeBackColorConsole(); //Appliquer le chamgement de couleurs souhaité
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    
                    selectedIndex = Math.Min(_options.Length - 1, this.selectedIndex + 1); // Déplace la sélection vers le bas
                    Console.ResetColor(); //Réinitialiser les couleurs de la console
                    ChangeBackColorConsole(); //Appliquer le chamgement de couleurs souhaité
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {

                }
                else if (key.Key == ConsoleKey.RightArrow)
                {

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
                    // Affichage du sous menu de la difficulté
                    if (this.selectedIndex == _options.Length - 3)
                    {
                        UserInput();
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
