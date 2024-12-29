using System;
using System.Collections.Generic;

namespace Uno
{
    public class Defausse
    {
        private List<Carte> cartesDefaussees;  // Liste de cartes déjà jouées

        // Constructeur
        public Defausse()
        {
            cartesDefaussees = new List<Carte>();  // Ici on initialise la liste des cartes de la défausse
        }

        // Méthode pour ajouter une carte à la défausse
        public void AjouterCarte(Carte carte)
        {
            cartesDefaussees.Add(carte);
        }

        // Méthode pour obtenir la carte au sommet de la défausse
        public Carte CarteDuSommet()
        {
            if (cartesDefaussees.Count > 0)
            {
                return cartesDefaussees[cartesDefaussees.Count - 1];
            }
            else
            {
                return null;  // Retourne null si la défausse est vide
            }
        }

        // Méthode pour afficher les cartes de la défausse
        public void AfficherDefausse()
        {
            Console.WriteLine("Cartes dans la défausse : ");
            foreach (Carte carte in cartesDefaussees)
            {
                carte.AfficherCarte();
            }
        }
    }
}
