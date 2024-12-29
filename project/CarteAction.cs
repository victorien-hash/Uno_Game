using System;

namespace Uno
{
    public class CarteAction : Carte
    {
        private string action;  // Description de l'action (exemple : Inversion, Passer, +2)

        // Constructeur pour initialiser la carte d'action
        public CarteAction(string couleurCarte, string actionCarte)
            : base(couleurCarte, actionCarte)  // Ici l'action represente le nom de la carte
        {
            action = actionCarte;
        }

        // Acesseur pour obtenir l'action de la carte
        public string Action{
            get { return action; }
        }

        // Implémentation de la méthode  Jouer
        public override void Jouer()
        {
            Console.WriteLine("Carte d'action jouée : " + action + " de couleur " + couleur);
        }

        // Méthode pour afficher la carte d'action
        public override void AfficherCarte()
        {
            Console.WriteLine("Carte d'action : " + action + " de couleur " + couleur);
        }
    }
}
