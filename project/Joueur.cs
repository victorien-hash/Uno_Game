using System;
using System.Collections.Generic;

namespace Uno
{
    public class Joueur
    {
        public string nom;
        private List<Carte> main;  // Liste des cartes du joueur

        // Accesseur pour obtenir le nom du joueur
        public string Nom
        {
            get { return nom; }
        }      

        // Constructeur
        public Joueur(string nom)
        {
            this.nom = nom;
            main = new List<Carte>();
        }

        // Méthode pour ajouter une carte à la main du joueur
        public void AjouterCarte(Carte carte)
        {
            main.Add(carte);
        }

        // Méthode pour retirer une carte de la main
        public Carte JouerCarte(int index)
        {
            if (index >= 0 && index < main.Count)
            {
                Carte carteJouee = main[index];
                main.RemoveAt(index);
                return carteJouee;
            }
            else
            {
                Console.WriteLine("Index invalide !");
                return null;
            }
        }

        // Afficher les cartes du joueur
        public void AfficherMain()
        {
            Console.WriteLine($"voici les cartes de {Nom}:");
            for (int i = 0; i < main.Count; i++)
            {
                Console.Write($"{i + 1}: ");
                main[i].AfficherCarte();
            }
        }

        public int NombreDeCartes()
            {
            return main.Count;
            }

        public Carte VoirCarte(int index)
        {
            return main[index];
        }

        public void RetirerCarte(int index)
        {
            main.RemoveAt(index);
        }


        // Vérification pour savoir si le joueur a encore des cartes
        public bool AEncoreDesCartes()
        {
            return main.Count > 0;
        }
    }
}
