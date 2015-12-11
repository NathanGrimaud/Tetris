using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    class BarreLongue : Barre
    {
        public BarreLongue()
        {
            this.emplacement = new List<int>() { 4, 5, 6, 7 };
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

            if (position == 0)
            {
                if (this.emplacement[0] - 9 > 0)
                {
                    this.emplacement[0] -= 9;
                    this.emplacement[2] += 9;
                    this.emplacement[3] += 18;
                    position++; 
                }
            }
            else
            {
                if (this.emplacement[2] - 9 > 0 && this.emplacement[3] - 18 > 0)
                {
                    this.emplacement[0] += 9;
                    this.emplacement[2] -= 9;
                    this.emplacement[3] -= 18;
                    position = 0; 
                }
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
