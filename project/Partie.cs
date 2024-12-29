using System;
using System.Collections.Generic;

namespace Uno
{
        public class Partie
        {
            private List<Joueur> joueurs;
            private Pioche pioche;
            private Defausse defausse;
            private int joueurActuel;  // Indice du joueur qui joue actuellement

            // Constructeur
            public Partie(List<string> nomsJoueurs)
            {
                joueurs = new List<Joueur>();
                foreach (string nom in nomsJoueurs)
                {
                    joueurs.Add(new Joueur(nom));
                }

                pioche = new Pioche();
                defausse = new Defausse();
                joueurActuel = 0;

                // Initialiser la pioche et distribuer les cartes
                InitialiserJeu();
            }

            // Méthode pour initialiser la pioche et distribuer les cartes aux joueurs
        
        public void InitialiserJeu()
        {
            // Couleurs de cartes standards
            string[] couleurs = { "rouge", "vert", "bleu", "jaune" };

            // Ajouter les cartes numériques (0-9) pour chaque couleur
            foreach (string couleur in couleurs)
            {

                // Cartes 0 à 9 par couleur 
                for (int valeur = 0; valeur <= 9; valeur++)
                {
                    pioche.AjouterCarte(new CarteNumerique(couleur, valeur.ToString()));
                    
                }

                // Ajouter les cartes d'action (Passer, Inversion, +2)
                for (int i = 0; i < 2; i++)  // Chaque carte d'action est présente deux fois par couleur
                {
                    pioche.AjouterCarte(new CarteAction(couleur, "Passer"));
                    pioche.AjouterCarte(new CarteAction(couleur, "Inversion"));
                    pioche.AjouterCarte(new CarteAction(couleur, "+2"));
                }
            }

            // Ajouter les cartes Joker (Changer de couleur et +4)
            for (int i = 0; i < 2; i++)  // Chaque joker est présent 2 fois
            {
                pioche.AjouterCarte(new CarteJoker("joker"));
                pioche.AjouterCarte(new CarteJoker("+4"));
            }

            // Mélanger la pioche après avoir ajouté toutes les cartes
            MelangerPioche();

            // Distribuer les cartes aux joueurs
            foreach (Joueur joueur in joueurs)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        joueur.AjouterCarte(pioche.TirerCarte());
                    }
                }



            // Placer une première carte au dessus apres le mélange. pour debuter la partie
            // on se rassure que la carte du haut n'est pas un joker
            Carte carteDuHaut = null;

            do
            {
                carteDuHaut = pioche.TirerCarte();
            } while (carteDuHaut is CarteJoker); 

            defausse.AjouterCarte(carteDuHaut);

        }

        // Méthode pour mélanger la pioche
        public void MelangerPioche()
        {
            Random melange = new Random();
            int n = pioche.Cartes.Count;
            while (n > 1)
            {
                n--;
                int k = melange.Next(n + 1);
                Carte value = pioche.Cartes[k];
                pioche.Cartes[k] = pioche.Cartes[n];
                pioche.Cartes[n] = value;
            }
        }

        // Méthode pour démarrer le jeu, toutes les contraintes de notre jeu sont ici
            
        public void Demarrer()
        {
            bool jeuEnCours = true;
            bool sensNormal = true; // Sens du jeu (normal ou inversé)
            string couleurActuelle = ""; // Couleur choisie après un Joker

            while (jeuEnCours)
            {
                Joueur joueur = joueurs[joueurActuel];
                Console.WriteLine($"\nC'est le tour de {joueur.Nom}");
                joueur.AfficherMain();

                // Affiche la carte actuelle ou la couleur sélectionnée après un Joker
                if (string.IsNullOrEmpty(couleurActuelle))
                {
                    Console.Write("La carte en cours est : ");
                    defausse.CarteDuSommet().AfficherCarte();
                }
                else
                {
                    Console.WriteLine($"La carte en cours est un Joker de couleur {couleurActuelle}");
                }

                bool carteValide = false;

                while (!carteValide)
                {

                    // avant de jouer son avant-dernière carte on doit dire "UNO"

                    if (joueur.NombreDeCartes() == 2)  // Si le joueur a une seule carte restante avant la dernière
                    {
                        Console.WriteLine("Vous êtes sur votre avant-dernière carte. Dites 'UNO' !");
                        string unoReponse = Console.ReadLine().Trim().ToUpper();
                        
                        if (unoReponse != "UNO")
                        {
                            Console.WriteLine("Vous n'avez pas dit 'UNO'. Le tour est annulé et vous devez piocher une carte supplémentaire.");
                            Carte cartePiochée = pioche.TirerCarte();
                            joueur.AjouterCarte(cartePiochée);
                            carteValide = true;  // Fin du tour, même si le joueur n'a pas dit 'UNO'
                            continue;  // Passe au joueur suivant
                        }
                    }


                    // debut de la partie

                    Console.WriteLine("Choisissez une carte à jouer (1, 2, 3, ... ou 0 pour piocher) :");
                    if (!int.TryParse(Console.ReadLine(), out int choix)) // ici on convertit l'entrée utilisateur en int et on stocke le resultat dans la variable choix
                    {
                        Console.WriteLine("Entrée invalide, veuillez entrer un nombre.");
                        continue;
                    }

                    choix -= 1; // Convertit l'entrée utilisateur en index (0-based)

                    if (choix == -1) // Pioche si 0 est choisi
                    {
                        Carte piochee = pioche.TirerCarte();
                        joueur.AjouterCarte(piochee);
                        Console.WriteLine("Vous avez pioché :");
                        piochee.AfficherCarte();
                        carteValide = true; // Fin du tour après pioche
                    }
                    else if (choix >= 0 && choix < joueur.NombreDeCartes())
                    {
                        Carte carteChoisie = joueur.VoirCarte(choix);
                        Carte carteSommet = defausse.CarteDuSommet();

                        // Vérifie si la carte peut être jouée
                        if (carteChoisie.Couleur == carteSommet.Couleur || 
                            carteChoisie.Nom == carteSommet.Nom || 
                            carteChoisie.Couleur == "multicolore" ||  
                            carteChoisie.Couleur == couleurActuelle )  // Après Joker
                        {
                            joueur.RetirerCarte(choix);
                            defausse.AjouterCarte(carteChoisie);
                            carteChoisie.Jouer();

                            // Réinitialise la couleur uniquement si une carte normale est jouée
                            if (carteChoisie.Couleur != "multicolore")
                            {
                                couleurActuelle = "";
                            }

                            carteValide = true;

                            // Gestion des cartes d'action
                            if (carteChoisie is CarteAction carteAction)
                            {
                                switch (carteAction.Action)
                                {
                                    case "Passer":
                                        joueurActuel = (joueurActuel + (sensNormal ? 1 : -1) + joueurs.Count) % joueurs.Count;
                                        break;
                                    case "Inversion":
                                        sensNormal = !sensNormal;
                                        break;
                                    case "+2":
                                        int cumulCartes = 2;
                                        bool contrePossible = true;
                                        int prochainJoueur = (joueurActuel + (sensNormal ? 1 : -1) + joueurs.Count) % joueurs.Count;

                                        while (contrePossible)
                                        {
                                            Joueur joueurSuivant = joueurs[prochainJoueur];
                                            Console.WriteLine($"\n{joueurSuivant.Nom}, vous êtes attaqué avec +{cumulCartes}. Jouez un +2 pour contrer ou saisissez « O/Y » pour accepter de recevoir les cartes :");
                                            joueurSuivant.AfficherMain();

                                            string reponse = Console.ReadLine().Trim().ToUpper();
                                            if (reponse == "O" || reponse == "Y")
                                            {
                                                for (int i = 0; i < cumulCartes; i++)
                                                {
                                                    joueurSuivant.AjouterCarte(pioche.TirerCarte());
                                                }
                                                contrePossible = false; // Fin de la boucle de contre
                                            }
                                            else if (int.TryParse(reponse, out int choixContre) && choixContre > 0 && choixContre <= joueurSuivant.NombreDeCartes())
                                            {
                                                Carte carteContre = joueurSuivant.VoirCarte(choixContre - 1);
                                                if (carteContre.Nom == "+2")
                                                {
                                                    joueurSuivant.RetirerCarte(choixContre - 1);
                                                    defausse.AjouterCarte(carteContre);
                                                    carteContre.Jouer();
                                                    cumulCartes += 2;  // Augmente les cartes cumulées
                                                    Console.WriteLine($"{joueurSuivant.Nom} a contré avec un +2 !");
                                                    prochainJoueur = (prochainJoueur + (sensNormal ? 1 : -1) + joueurs.Count) % joueurs.Count;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Vous devez jouer un +2 pour contrer !");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Entrée invalide, essayez encore.");
                                            }
                                        }
                                        break;
                                }
                            }
                            // Gestion des cartes Joker
                            else if (carteChoisie is CarteJoker carteJoker)
                            {
                                Console.WriteLine("Choisissez une couleur (rouge, vert, bleu, jaune) :");
                                couleurActuelle = Console.ReadLine().ToLower();

                                if (carteJoker.Nom == "+4")
                                {
                                    int prochainJoueur = (joueurActuel + (sensNormal ? 1 : -1) + joueurs.Count) % joueurs.Count;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        joueurs[prochainJoueur].AjouterCarte(pioche.TirerCarte());
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cette carte ne peut pas être jouée. Choisissez une autre carte.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Choix invalide. Essayez à nouveau.");
                    }
                }

                // Vérifie si le joueur a gagné
                if (!joueur.AEncoreDesCartes())
                {
                    Console.WriteLine($"{joueur.Nom} a gagné !");
                    jeuEnCours = false;
                }

                // Passe au joueur suivant en fonction du sens du jeu
                joueurActuel = (joueurActuel + (sensNormal ? 1 : -1) + joueurs.Count) % joueurs.Count;
            }
        }



    }
}
