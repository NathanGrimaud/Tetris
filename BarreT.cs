using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    class BarreT :  Barre
    {
        public BarreT()
        {
            this.emplacement = new List<int>() { 4, 5, 6, 15 };
            this.couleur = new SolidColorBrush(Couleurs[r.Next(Couleurs.Count)]);
        }
        public override void Tourner(ref Barre barre, ref Table grille)
        {
            var emp = barre.emplacement;
            if (position == 0)
            {
                emp[0] -= 9;
                emp[2] += 9;
                emp[3]  -= 11;
                position++;
            }
            else if(position == 1)

            {
                emp[0] += 11;
                emp[2] -= 11;
                emp[3] -= 9;
                position ++;
            }
            else if (position == 2)
            {
                emp[0] -= 11;
                emp[2] += 11;
                emp[3] += 11;
                position ++;
            }
            else if (position == 3)
            {
                emp[0] += 9;
                emp[2] -= 9;
                emp[3] += 9;
                position = 0;
            }
        }
    }
}
