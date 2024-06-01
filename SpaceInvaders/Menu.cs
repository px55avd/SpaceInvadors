///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
/// Descrition de classe: La classe Menu gère l'affichage et la naviguation de l'utilsateur dans le menu de l'application. 
/// Selon les entreés clavier de l'utilisateur gérée par la méthode "UserinputMenu()" , la classe modifie l'affichage pour mettre en évidence 
/// les éléments pointés par l'utilisateur avec la méthode "ChangeBackColorConsole()".
using System;
using System.Runtime.CompilerServices;
using System.IO;

[assembly: InternalsVisibleToAttribute("TestunitSpaceinvader")] // Accès de classe au test unitaire. 

namespace SpaceInvaders
{
    public class Menu
    {
        // Propriété publique pour accéder à l'indice sélectionné
        private int _selectedIndex = 0;

        // Options du menu principal
        private string[] _options = new string[]{ "Lancer une nouvelle partie", "Difficulté", "Son", "A propos", "Quitter" };
        public string[] Options { get { return _options;} set { value = _options; } }

        // Options du sous-menu Difficulté
        private string[] _optionsForIndex1 = new string[] { "Facile", "Moyen", "Difficle", "Retour", "Quitter"};
        public string[] OptionsForIndex1 { get { return _optionsForIndex1; } set { value = _optionsForIndex1; } }

        // Options du sous-menu Son
        private string[] _optionsForIndex2 = new string[] { "Activer", "Désactiver","Retour", "Quitter" };
        public string[] OptionsForIndex2 { get { return OptionsForIndex2; } set { value = OptionsForIndex2; } }

        // Options du sous-menu A propos
        private string[] _optionsForIndex3 = new string[] {"Avoir les information du jeu","Quitter", "Retour"};
        public string[] OptionsForIndex3 { get { return _optionsForIndex3; } set { value = _optionsForIndex3; } }

        // Jeu à lancer
        private Game _game;
        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }

        /// <summary>
        /// Constructeur de la classe Menu
        /// </summary>
        /// <param name="selectedIndexMenu">Index du menu</param>
        public Menu()
        {
            _selectedIndex = 0;
            
            //Instanciation d'un nouveau Game.
            _game = new Game();
        }

        /// <summary>
        /// Méthode pour changer les couleurs de la console en fonction de l'option sélectionnée
        /// </summary>
        public void ChangeBackColorConsole(string[] menu)
        {
            Console.Clear();

            // Pour chaque index du menu
            for (int i = 0; i < menu.Length; i++)
            {
                // Vérifie l'index correspond au defilement
                if (i == _selectedIndex)
                {
                    //Changement des éléments de la console pour mettre en évidence l'index pointé par l'utilisateur
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    // Applique les options d'affichage de base
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                // Affiche le index dans la console
                Console.WriteLine(menu[i]);
            } 
        }

        /// <summary>
        /// Méthode pour gérer les entrées utilisateur dans le menu
        /// </summary>
        public void UserinputMenu()
        {
            // efface le curseur
            Console.CursorVisible = false;

            // récupère le menu princiaple
            string[] menu = _options;

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
                    // Ne fait rien pour le moment
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    // Ne fait rien pour le moment

                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    //TODO :Utiliser une arboresence de switch pour gérer les menu et sous menus à la palce des moulte de condition actuelles.

                    // Si l'option sélectionnée est "Lancer une nouvelle partie" si le menu en cours est egal au tableau _option
                    if (_selectedIndex == 0 && menu == _options)
                    {
                        // Masque le curseur dans la console
                        Console.CursorVisible = false;

                        //Efface tout ce qu'il ya dans la console c'est à dire le menu.
                        Console.Clear();

                        //pour démarrer le jeu
                        _game.Start();

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
                    // Si l'option sélectionnée est "A propos", sort de la boucle si le menu en cours est egal au tableau _option
                    if (_selectedIndex == 3 && menu == _options)
                    {
                        //remise de l'index à zéro
                        _selectedIndex = 0;

                        //Attribution du sous menu A props
                        menu = _optionsForIndex3;

                        ChangeBackColorConsole(_optionsForIndex3);
                    }

                    // Si l'option sélectionnée est "Quitter", sort de la boucle si le menu en cours est egal au tableau _option
                    if (_selectedIndex == 4 && menu == _options)
                    {
                        break;
                    }

                    // Si l'option sélectionnée est "Facile" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 0 && menu == _optionsForIndex1)
                    {
                        // attribut le bon niveau de difficulté
                        _game.GameMode = 0;
                        _game.PlayerLives = 3; // nombre de vie du joueur en mode facile
                        
                        //Affiche le texte le bon niveau selectionné.
                        Console.WriteLine("Le nieveau de difficlté est sur facile");
                        
                    }
                    // Si l'option sélectionnée est "Moyen" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 1 && menu == _optionsForIndex1)
                    {
                        // attribut le bon niveau de difficulté
                        _game.GameMode = 1;
                        _game.PlayerLives = 2; // nombre de vie du joueur en mode moyen

                        //Affiche le texte le bon niveau selectionné.
                        Console.WriteLine("Le nieveau de difficlté est sur Moyen");
                    }
                    // Si l'option sélectionnée est "Difficle" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 2 && menu == _optionsForIndex1)
                    {
                        // attribut le bon niveau de difficulté
                        _game.GameMode = 2;
                        _game.PlayerLives = 1; // nombre de vie du joueur en mode difficle

                        //Affiche le texte le bon niveau selectionné.
                        Console.WriteLine("Le nieveau de difficlté est sur Difficle");
                    }
                    // Si l'option sélectionnée est "Retour" et si le menu en cours est egal au tableau _optionsForIndex1
                    if (_selectedIndex == 3 && menu == _optionsForIndex1)
                    {

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

                    // Si l'option sélectionnée est "Info" et si le menu en cours est egal au tableau _optionsForIndex3
                    if (_selectedIndex == 0 && menu == _optionsForIndex3)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Vous êtes dans la matrice !");
                    }

                    // Si l'option sélectionnée est "Quitter" et si le menu en cours est egal au tableau _optionsForIndex3
                    if (_selectedIndex == 2 && menu == _optionsForIndex3)
                    {
                        break; // quitter
                    }

                    // Si l'option sélectionnée est "Retour" et si le menu en cours est egal au tableau _optionsForIndex3
                    if (_selectedIndex == 1 && menu == _optionsForIndex3)
                    {


                        //remise de l'index à zéro
                        _selectedIndex = 0;

                        //Attribution du menu principale 
                        menu = _options;

                        ChangeBackColorConsole(_options);
                    }
                }
            }
        }

        internal SpicyInvader.Program Program
        {
            get => default;
            set
            {
            }
        }
    }
}
