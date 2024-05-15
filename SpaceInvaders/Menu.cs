using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    internal class Menu
    {
        // Indice de l'option sélectionnée dans le menu

        // Propriété publique pour accéder à l'indice sélectionné
        private int _selectedIndex = 0;

        // Options du menu principal
        private string[] _options = new string[]{ "Lancer une nouvelle partie", "Difficulté", "Son", "Quitter" };
        public string[] Options { get { return _options;} set { value = _options; } }

        // Options du sous-menu Difficulté
        private string[] _optionsForIndex1 = new string[] { "Facile", "Moyen", "Difficle", "Retour", "Quitter"};
        public string[] OptionsForIndex1 { get { return _optionsForIndex1; } set { value = _optionsForIndex1; } }

        // Options du sous-menu Son
        private string[] _optionsForIndex2 = new string[] { "Activer", "Désactiver","Retour", "Quitter" };
        public string[] OptionsForIndex2 { get { return OptionsForIndex2; } set { value = OptionsForIndex2; } }



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
        public void ChangeBackColorConsole(string[] menu)
        {
            Console.Clear();

            for (int i = 0; i < menu.Length; i++)
            {
                if (i == _selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.WriteLine(menu[i]);
            } 
        }

        /// <summary>
        /// Méthode pour gérer les entrées utilisateur dans le menu
        /// </summary>
        public void UserInput()
        {
            Console.CursorVisible = false;

            string[] menu = _options;

            string[,] allMenu = new string[,] { };

            while (true)
            {
                ConsoleKeyInfo key = new ConsoleKeyInfo();
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                        _selectedIndex = Math.Max(0, _selectedIndex - 1); // Déplace la sélection vers le haut
                        Console.ResetColor(); //Réinitialiser les couleurs de la console
                        ChangeBackColorConsole(menu); //Appliquer le chamgement de couleurs souhaité      
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                        _selectedIndex = Math.Min(menu.Length - 1, _selectedIndex + 1); // Déplace la sélection vers le bas
                        Console.ResetColor(); //Réinitialiser les couleurs de la console
                        ChangeBackColorConsole(menu); //Appliquer le chamgement de couleurs souhaité  
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {

                }
                else if (key.Key == ConsoleKey.RightArrow)
                {

                }
                else if (key.Key == ConsoleKey.Enter)
                {

                    //TODO :Utiliser une arboresence de switch pour gérer les menu et sous menus à la palce des moulte de condition actuelles.

                    // Si l'option sélectionnée est "Lancer une nouvelle partie" si le menu en cours est egal au tableau _option
                    if (_selectedIndex == 0 && menu == _options)
                    {
                        // Masque le curseur dans la console
                        Console.CursorVisible = false;

                        //Console.WriteLine("nsdjkvngjksd");

                        //Crée une instance de la classe Game pour démarrer le jeu
                        Game game = new Game();
                        Console.Clear();
                        game.Start();

                    }
                    // Affichage du sous menu de la difficulté si le menu en cours est egal au tableau _option
                    if (_selectedIndex == 1 && menu == _options)
                    {

                        //remise de l'index à zéro
                        _selectedIndex = 0;

                        //Attribution du sous menu Difficulté
                        menu = _optionsForIndex1;

                        //Attribution Du changement de couleur au menu en cours d'affichage
                        ChangeBackColorConsole(_optionsForIndex1);
                        
                    }
                    // Affichage sous-menu pour le son si le menu en cours est egal au tableau _option
                    if (_selectedIndex == 2 && menu == _options)
                    {
                        //remise de l'index à zéro
                        _selectedIndex = 0;

                        //Attribution du sous menu Son
                        menu = _optionsForIndex2;

                        ChangeBackColorConsole (_optionsForIndex2);
                    }
                    // Si l'option sélectionnée est "Quitter", sort de la boucle si le menu en cours est egal au tableau _option
                    if (_selectedIndex == 3 && menu == _options)
                    {
                        break;
                    }


                    // Si l'option sélectionnée est "Facile" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 0 && menu == _optionsForIndex1)
                    {
                        Console.WriteLine("Le nieveau de difficlté est sur facile");
                    }
                    // Si l'option sélectionnée est "Moyen" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 1 && menu == _optionsForIndex1)
                    {
                        Console.WriteLine("Le nieveau de difficlté est sur Moyen");
                    }
                    // Si l'option sélectionnée est "Difficle" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 2 && menu == _optionsForIndex1)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Le nieveau de difficlté est sur Difficle");
                    }
                    // Si l'option sélectionnée est "Retour" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 3 && menu == _optionsForIndex1)
                    {
                        //TODO: retourner au menu pricipale

                        //remise de l'index à zéro
                        _selectedIndex = 0;

                        //Attribution du menu principal
                        menu = _options;

                        ChangeBackColorConsole(_options);
                    }
                    // Si l'option sélectionnée est "Quitter", sort de la boucle si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 4 && menu == _optionsForIndex1)
                    {
                        break;
                    }


                    // Si l'option sélectionnée est "Activer" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 0 && menu == _optionsForIndex2)
                    {
                        Console.WriteLine("Tu active le son");
                    }
                    // Si l'option sélectionnée est "Désactiver" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 1 && menu == _optionsForIndex2)
                    {
                        Console.WriteLine("Tu désactive le son");
                    }
                    // Si l'option sélectionnée est "Retour" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 2 && menu == _optionsForIndex2)
                    {
                        //remise de l'index à zéro
                        _selectedIndex = 0;

                        //Attribution du menu principale 
                        menu = _options;

                        ChangeBackColorConsole(_options);
                    }
                    // Si l'option sélectionnée est "Quitter" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 3 && menu == _optionsForIndex2)
                    {
                        break;//Quitter
                    }
                }
            }
        }
    }
}
