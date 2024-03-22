///**************************************************************************************
///ETML
///Auteur : Omar Egal Ahmed
///Date : 18.01.2024
///Description : Création d'un programme de type jeu Scicy Invaders en mode Console. 
///**************************************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    internal class Bunker
    {

        public int X { get; private set; }
        public int Y { get; private set; }
        public bool Damaged { get; private set; }

        public Bunker(int x, int y)
        {
            X = x;
            Y = y;
            Damaged = false;
        }

        public void TakeDamage()
        {
            Damaged = true;
            // à Faire : Autres actions à effectuer lorsque le bunker est endommagé, peut-être changer les caractères ?
        }

        public Rectangle GetHitbox()
        {
            // Retourne un rectangle représentant la hitbox du bunker
            return new Rectangle(X, Y, 10, 10);
        }
        public void Draw()
        {
            // Positionne le curseur à la position du joueur
            Console.SetCursorPosition(X, Y);
            // Définit la couleur du texte pour représenter le joueur
            Console.ForegroundColor = ConsoleColor.White;
            // Affiche le joueur comme une chaîne de caractère "<O>"
            Console.Write("¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬\n¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬\n¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬");
        }




    }
}
