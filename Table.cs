using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Table
    {
        public Barre[] tableau = new Barre[180];
        public Table() { } // Constructeur 
        
        public bool Placer (Barre aplacer) // Pour placer une barre grace a la liste de ces coordonnées 
        {
            bool placementvalide = EmplacementDispo(ref aplacer);

            foreach (int emplacement in aplacer.emplacement)
            {
                tableau[emplacement] = aplacer;           
            }

            return true;
        }

        public bool Descendre(ref Barre aplacer) // Pour faire descendre une barre 
        {
            bool placementvalide = EmplacementDispo(ref aplacer);
            List<int> precemplacement = new List<int>();

            for(int i = 0; i < aplacer.emplacement.Count; i++)
            {
                // Je copie la liste des emplacement dans la barre dans une liste afin de les garder en mémoire en cas de Fail
                precemplacement.Add(aplacer.emplacement[i]);
                // J'effectue le deplacement sur la vrai liste
                aplacer.emplacement[i] = aplacer.emplacement[i] + 10;
            }
            // je verifie si tout est okay. Si oui, j'écris les nouveaux emplacement dans le tableau
            if (EmplacementDispo(ref aplacer)) {

                foreach (int emplacement in precemplacement) // Je libére les précédent emplacement 
                {
                    tableau[emplacement] = null;
                }

                foreach (int emplacement in aplacer.emplacement)// Je rempli les nouveaux 
                {
                    tableau[emplacement] = aplacer;
                }
                return true;  

            }
            else
            {
                // Sinon, je remets tout comme avant grace a la liste copié auparavant 
                aplacer.emplacement = precemplacement;
                aplacer.bloquer = true;
                return false;
            }
            
        }


        public bool Droite(ref Barre aplacer)
        {
            // Je vérifie qu'on essaye pas de deplacer un objet non bloqué 
            if (!aplacer.bloquer) { 
            bool placementvalide = EmplacementDispo(ref aplacer);
            List<int> precemplacement = new List<int>();

            for (int i = 0; i < aplacer.emplacement.Count; i++)
            {
                // Je copie la liste des emplacement dans la barre dans une liste afin de les garder en mémoire en cas de Fail
                precemplacement.Add(aplacer.emplacement[i]);
                // J'effectue le deplacement sur la vrai liste
                aplacer.emplacement[i] = aplacer.emplacement[i] + 1;
            }
                // je verifie si tout est okay. Si oui, j'écris les nouveaux emplacement dans le tableau
                if (EmplacementDispo(ref aplacer) && DepacementLargeur(ref aplacer, precemplacement))
            {

                foreach (int emplacement in precemplacement) // Je libére les précédent emplacement 
                {
                    tableau[emplacement] = null;
                }

                foreach (int emplacement in aplacer.emplacement)// Je rempli les nouveaux 
                {
                    tableau[emplacement] = aplacer;
                }

                return true;

            }
            else
            {
                // Sinon, je remets tout comme avant grace a la liste copié auparavant 
                aplacer.emplacement = precemplacement;
                return false;
            }

        }
            return false;
        }

        public bool Gauche(ref Barre aplacer)
        {
            // Je vérifie qu'on essaye pas de deplacer un objet non bloqué 
            if (!aplacer.bloquer)
            {
                bool placementvalide = EmplacementDispo(ref aplacer);

               
                List<int> precemplacement = new List<int>();

                for (int i = 0; i < aplacer.emplacement.Count; i++)
                {
                    // Je copie la liste des emplacement dans la barre dans une liste afin de les garder en mémoire en cas de Fail
                    precemplacement.Add(aplacer.emplacement[i]);
                    // J'effectue le deplacement sur la vrai liste
                    aplacer.emplacement[i] = aplacer.emplacement[i] - 1;
                }

              // je verifie si tout est okay. Si oui, j'écris les nouveaux emplacement dans le tableau
                if (EmplacementDispo(ref aplacer) && DepacementLargeur(ref aplacer, precemplacement))
                {

                    foreach (int emplacement in precemplacement)
                    {
                        tableau[emplacement] = null;
                    }

                    foreach (int emplacement in aplacer.emplacement)
                    {
                        tableau[emplacement] = aplacer;
                    }

                    return true;

                }
                else
                {
                    // Sinon, je remets tout comme avant grace a la liste copié auparavant 
                    aplacer.emplacement = precemplacement;
                    return false;
                }

            }
            return false;
        }

        public bool EmplacementDispo(ref Barre acheck)
        {
           // Je tourne chaque emplacement de la barre a check 
            foreach (int emplacement in acheck.emplacement)
            {
                // Si l'emplacement est en dehors des limites du tableau, je return false
                if (emplacement >= 180)
                    return false;
                // Sinon si l'emplacement du tableau est vide ou corresponds déja a la même barre, alors je continue le foreach 
                else if (tableau[emplacement] == null || tableau[emplacement] == acheck) { }

                // Sinon, je retourne faux 
                else
                    return false;                

            }

            return true;
        }

        public bool DepacementLargeur(ref Barre acheck, List<int> precemplacement)
        {
            for (int i = 0; i < acheck.emplacement.Count; i++)
            {
                // Je vérifie que les déplacement en largeur reste bien sur la même ligne
                if (acheck.emplacement[i] / 10 != precemplacement[i] / 10)
                    return false;
            }

            return true;

        }

        public void checkLigne()
        {
            // Je tourne les 18 lignes du tableau
            for (int i = 17; i > 0; i--)
            {
                // pour chaque ligne, je multiplie par 10 afin de transformer j en index du tableau
                int j = i*10;
                bool entier = true;
                // Tant que j n'est pas arrivé au bout de la ligne et qu'il n'y a pas de trou dans la ligne je boucle 
                while (j != i*10+9 && entier)
                {
                    if(tableau[j] == null)
                    {
                        entier = false;
                    }

                    j++;
                }

                // Si il n'y a pas eux de trou dans la ligne, je la supprime
                if (entier) {                   
                    this.supprimerLigne(i * 10);
                    this.checkLigne();
                }
                    

            }

        }

        public void supprimerLigne(int j)
        {
            //Pour supprimer une ligne, je passe par chaque index du tableau correspondant à cette ligne, et je les mets à null 
           for(int i = 0; i <= 9; i++)
            {
                this.tableau[j + i] = null;
            }

            for (int i = j; i >= 0; i--) 
            {
                if (this.tableau[i] != null) { 
                    Barre encours = this.tableau[i];
                    // Puis, tant que les barre du dessus peuvent descendre, elle descende 
                    while (this.Descendre(ref encours));
                }

            }
            }
    }

}
