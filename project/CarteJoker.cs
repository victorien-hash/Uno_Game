using System;

namespace Uno
{
    public class CarteJoker : Carte
    {
        // Constructeur 
        public CarteJoker(string nom) : base("multicolore", nom)  // "multicolore" est la couleur par defaut pour un joker
        {
        }

        // Implémentation de la méthode  Jouer
        public override void Jouer()
        {
            Console.WriteLine($"Carte Joker jouée : {nom}");
        }

        // Méthode pour afficher la carte Joker 
        public override void AfficherCarte()
        {
            Console.WriteLine($"Carte Joker : {nom}");
        }
    }
}
