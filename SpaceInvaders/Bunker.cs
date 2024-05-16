///**************************************************************************************
///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
///**************************************************************************************
using System;
using System.Drawing; // Importation de l'espace de noms System.Drawing pour utiliser Rectangle

namespace SpaceInvaders
{
    internal class Bunker
    {
        // Propriétés pour la position et l'état du bunker
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool Damaged { get; set; }
        // Propriété OldX : ancienne position horizontale de l'envahisseur
        public int OldX { get; private set; } = 0;
        // Propriété OldY : ancienne position verticale de l'envahisseur
        public int OldY { get; private set; } = 0;
        // Propriété Symbol : apparence de l'envahisseur
        public string bunkerSymbol { get; set; }

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
            X = x;
            Y = y;
            Damaged = false;
            bunkerSymbol = "X"; // Symbole par défaut du bunker
        }

        /// <summary>
        /// Méthode pour simuler les dommages subis par le bunker
        /// </summary>
        public void TakeDamage()
        {
            Damaged = true;
            // À faire (fait à moitié): Autres actions à effectuer lorsque le bunker est endommagé, comme changer son apparence
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
                Console.Write(bunkerSymbol);
            }
        }

        /// <summary>
        /// Méthode pour effacer les caractère 
        /// </summary>
        public static class Helper
        {
            public static void Erase(int x, int y, int length)
            {
                Console.SetCursorPosition(x, y);

                // Efface les caractères à partir de la position spécifiée jusqu'à la longueur spécifiée
                for (int i = 0; i < length; i++)
                {
                    Console.Write(" "); // Remplace chaque caractère par un espace vide
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Drawfinalposition()
        {
            Helper.Erase(X, Y, bunkerSymbol.Length); // Efface un caractère à la position de l'ancien joueur  
        }

    }
}
