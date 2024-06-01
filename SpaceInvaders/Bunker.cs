///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
/// Descrition de classe: La classe Bunker est utilisée pour créer et gérer les bunkers dans le jeu Space Invaders. Chaque instance de la classe représente un unique bunker. 
/// Les méthodes permettent de gérer les dégâts subis par le bunker et de le dessiner correctement sur la console.
using System;
using System.Drawing; // Importation de l'espace de noms System.Drawing pour utiliser Rectangle

namespace SpaceInvaders
{
    public class Bunker
    {

        // Propriétés pour la position et l'état du bunker
        private int _x;
        public int X { get {return _x;} private set { _x = value; } }

        // Champ privé pour stocker la coordonnée Y
        private int _y;

        // Propriété publique pour accéder à la coordonnée Y
        // Le setter est privé, donc seul la classe peut modifier cette valeur
        public int Y
        {
            get { return _y; }
            private set { _y = value; }
        }

        // Champ privé pour indiquer si le bunker est endommagé
        private bool _damaged;

        // Propriété publique pour accéder et modifier l'état d'endommagement du bunker
        public bool Damaged
        {
            get { return _damaged; }
            set { _damaged = value; }
        }

        // Champ privé pour le symbole du bunker
        private string _bunkerSymbol = "X";

        // Propriété publique pour accéder et modifier le symbole du bunker
        public string BunkerSymbol
        {
            get { return _bunkerSymbol; }
            set { _bunkerSymbol = value; }
        }

        // Champ privé pour le symbole d'un bunker endommagé
        private string _damagedBunkerSymbol = "/";

        // Propriété publique pour accéder et modifier le symbole d'un bunker endommagé
        public string DamagedBunkerSymbol
        {
            get { return _damagedBunkerSymbol; }
            set { _damagedBunkerSymbol = value; }
        }

        internal Game Game
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Constructeur de la classe Bunker
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public Bunker(int x, int y)
        {
            _x = x;
            _y = y;
            _damaged = false;
        }

        /// <summary>
        /// Méthode pour simuler les dommages subis par le bunker
        /// </summary>
        public void TakeDamage()
        {
            Damaged = true;
            // À faire (fait à moitié): Autres actions à effectuer lorsque le bunker est endommagé, comme changer son apparence
            this.BunkerSymbol = _damagedBunkerSymbol;
        }

        /// <summary>
        /// Méthode pour obtenir la hitbox du bunker
        /// </summary>
        /// <returns> hitbox du bunker</returns>
        public Rectangle GetHitbox()
        {
            // Retourne un rectangle représentant la hitbox du bunker
            // Ici, un rectangle de 3x3 autour de la position du bunker est utilisé pour représenter sa hitbox
            return new Rectangle(X, Y, 1, 1);
        }

        /// <summary>
        /// Méthode pour dessiner le bunker sur la console
        /// </summary>
        public void Draw()
        {
            if (X > 0 && Y > 0)
            {
                // Positionne le curseur à la position du bunker
                Console.SetCursorPosition(X, Y);
                // Définit la couleur du texte pour représenter le bunker
                Console.ForegroundColor = ConsoleColor.White;
                // Affiche le bunker comme une chaîne de caractères
                Console.Write(BunkerSymbol);
            }
        }

        /// <summary>
        /// Méthode pour effacer les caractère 
        /// </summary>
        public static void Erase(int x, int y, int length)
        {
            Console.SetCursorPosition(x, y);

            // Efface les caractères à partir de la position spécifiée jusqu'à la longueur spécifiée
            for (int i = 0; i < length; i++)
            {
                Console.Write(" "); // Remplace chaque caractère par un espace vide
            }
        }
        

        /// <summary>
        /// Méthode pour effacer les derniere position des bunkers.
        /// </summary>
        public void Drawfinalposition()
        {
            //Appel de la méthode 
            Erase(X, Y, BunkerSymbol.Length); // Efface un caractère à la position de l'ancien joueur  
        }
    }
}
