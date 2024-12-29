using System;
using System.Collections.Generic;

namespace   Uno
{
    public class Pioche
    {
        private List<Carte> cartes; // Liste contenant les cartes de la pioche

        public List<Carte> Cartes 
        { 
            get 
            { 
                return cartes; 
            }
        }

        // Constructeur pour initialiser la pioche
        public Pioche()
        {
            cartes = new List<Carte>();
            // Ici on initialise la liste des cartes de la pioche
        }

        // Méthode pour ajouter une carte dans la pioche
        public void AjouterCarte(Carte carte)
        {
            cartes.Add(carte);
        }

        // Méthode pour tirer une carte de la pioche
        public Carte TirerCarte()
        {
            if (cartes.Count > 0)
            {
                Carte carteTiree = cartes[0];  // On récupère la carte du sommet
                cartes.RemoveAt(0);  // On la retire de la pioche
                return carteTiree;
            }
            else
            {
                Console.WriteLine("La pioche est vide !");
                return null;  // Retourne null si la pioche est vide
            }
        }

        // Méthode pour afficher les cartes restantes dans la pioche
        public void AfficherPioche()
        {
            Console.WriteLine("Cartes dans la pioche : ");
            foreach (Carte carte in cartes)
            {
                carte.AfficherCarte();
            }
        }
    }
}
