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
            List<int> emplacements = new List<int>();
            foreach (var emp in barre.emplacement)
            {
                emplacements.Add(emp);
            }           
            if (position == 0)
            {
                emplacements[0] -= 9;
                emplacements[1] -= 9;
                emplacements[3] += 2;
                position++;
            }
            else if (position == 1)
            {
                emplacements[0] += 2;
                emplacements[1] += 11;
                emplacements[3] -= 11;
                position++;
            }
            else if (position == 2)
            {
                emplacements[0] += 20;
                emplacements[1] += 9;
                emplacements[3] -= 9;
                position++;
            }
            else if (position == 3)
            {
                emplacements[0] -= 11;
                emplacements[2] += 11;
                emplacements[3] -= 2;
                position = 0;
            }
            Barre b = new BarreL();
            b.emplacement = emplacements;
            if (grille.EmplacementDispo(ref b))
            {
                barre = b;
            }
        }
    }
}
