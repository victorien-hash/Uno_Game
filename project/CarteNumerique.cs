using System;

namespace Uno
{
    
    public class CarteNumerique : Carte
    {
        // Attribut pour la valeur numérique de la carte
        private string valeur;
    

        // Constructeur qui initialise la couleur et la valeur
        public CarteNumerique(string couleurCarte, string valeurCarte)
            : base(couleurCarte, valeurCarte)  // Ici le nom de la carte est le même que la valeur d'une carte numérique
        {
            valeur = valeurCarte;
        }


        // Implémentation de la méthode abstraite Jouer()
        public override void Jouer()
        {
            Console.WriteLine("Carte numérique jouée : " + valeur + " de couleur " + couleur);
        }

        // Méthode pour afficher la carte
        public override void AfficherCarte()
        {
            Console.WriteLine("Carte numérique : " + valeur + " de couleur " + couleur);
        }
    }
}
