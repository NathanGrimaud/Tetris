using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    class BarreL : Barre
    {        
        public BarreL()
        {
            this.emplacement = new List<int>() { 4, 5, 6, 14 };
            this.couleur = new SolidColorBrush(Couleurs[r.Next(Couleurs.Count)]);
        }

        public override void Tourner(ref Barre barre,ref Table grille) 
        {
            // Sauvegarde des précédents emplacements
            List<int> emplacements = new List<int>();
            Barre b = new BarreL();            
            foreach (var emp in barre.emplacement)
            {
                emplacements.Add(emp);
            }
            b.emplacement = emplacements;

            // Déménagement de la barre 
            if (position == 0)
            {
                this.emplacement[0] -= 9;
                this.emplacement[1] -= 9;
                this.emplacement[3] += 2;
                position++;
            }
            else if (position == 1)
            {
                this.emplacement[0] += 2;
                this.emplacement[1] += 11;
                this.emplacement[3] -= 11;
                position++;
            }
            else if (position == 2)
            {
                this.emplacement[0] += 20;
                this.emplacement[1] += 9;
                this.emplacement[3] -= 9;
                position++;
            }
            else if (position == 3)
            {
                this.emplacement[0] -= 11;
                this.emplacement[2] += 11;
                this.emplacement[3] -= 2;
                position = 0;
            }
            
            // Vérification que le déménagement est correct
            if (grille.EmplacementDispo(ref barre))
            {              
                foreach (int empl in b.emplacement)
                {
                    grille.tableau[empl] = null;
                }

                foreach (int empl in this.emplacement)
                {
                    grille.tableau[empl] = this;
                }

            }
            else
            {
                this.emplacement = b.emplacement;
            }
        }
    }
}
