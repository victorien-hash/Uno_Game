using System;

namespace Uno
{

    public abstract class Carte
    {
        // Liste des attributs communs aux cartes
        protected string couleur;
        protected string nom;

        // Accesseurs pour accéder et definir les attributs de la carte
        public string Nom 
        { 
            get { return nom; }
        }

        public string Couleur
        {
            get { return couleur; }
            set { couleur = value; }
        }
     

        // Le constructeur
        public Carte(string couleurCarte, string nomCarte)
        {
            couleur = couleurCarte;
            nom = nomCarte;
        }

        // Méthodes d'affichage commune à toutes les cartes
        public virtual void AfficherCarte()
        {
            Console.WriteLine("Carte : " + nom + " de couleur " + couleur);
        }

        // Méthode qui permettra de jouer une carte
        public abstract void Jouer();
    }
}
