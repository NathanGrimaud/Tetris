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
            var emp = barre.emplacement;
            if (position == 0)
            {
                emp[0] -= 9;
                emp[2] -= 11;
                emp[3] -= 2;
                position++;
            }
            else
            {
                emp[0] += 9;
                emp[2] += 11;
                emp[3] += 2;
                position = 0;
            }
        }
    }
}
