using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    class BarreZinv : Barre
    {
        public BarreZinv()
        {
            this.emplacement = new List<int>() { 4, 5, 15,16  };
            this.couleur = new SolidColorBrush(Couleurs[r.Next(Couleurs.Count)]);
        }
        public override void Tourner(ref Barre barre, ref Table grille)
        {
            // Sauvegarde des précédents emplacements
            List<int> emplacements = new List<int>();
            Barre b = new BarreL();
            int precposition = position;

            foreach (var emp in barre.emplacement)
            {
                emplacements.Add(emp);
            }
            b.emplacement = emplacements;
            if (position == 0 )
            {
                if (this.emplacement[0] - 9 > 0 && this.emplacement[2] - 11 > 0 &&
                    this.emplacement[3] - 2 > 0)
                {
                    this.emplacement[0] -= 9;
                    this.emplacement[2] -= 11;
                    this.emplacement[3] -= 2;
                    position++; 
                }
            }
            else if(position == 1)
            {
                this.emplacement[0] += 9;
                this.emplacement[2] += 11;
                this.emplacement[3] += 2;
                position = 0; 
            }
            // Vérification que le déménagement est correct
            if (grille.EmplacementDispo(this))
            {
                grille.write(ref b.emplacement, this);

            }
            else
            {
                this.emplacement = b.emplacement;
                this.position = precposition;
            }
        }
    }
}
