using System;
using System.Collections.Generic;

namespace Uno
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans notre programe de jeu Uno !");
            
            // Saisi des joueurs
            Console.WriteLine("Entrez les noms des 3 joueurs, séparez les noms par un espace :");
            string input = Console.ReadLine();

            // Séparation des noms en utilisant l'espace comme séparateur
            string[] joueursSaisi = input.Split(' ');


            if (joueursSaisi.Length == 3)
            {

                // Créer une liste de joueurs
                List<string> nomsJoueurs = new List<string>();
                for (int i = 0; i < joueursSaisi.Length; i++)
                {
                    nomsJoueurs.Add(joueursSaisi[i]);
                }

                // On crée une instance de Partie avec les noms des joueurs
                Partie partie = new Partie(nomsJoueurs);

                // Démarrage de la partie
                partie.Demarrer();

                Console.WriteLine("Merci d'avoir joué et à bientot !");
            }
            else
            {
                Console.WriteLine("Vous n'avez pas fourni exactemt trois joueurs. recommencez");
            }
            

        }
    }
}
