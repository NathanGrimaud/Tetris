using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    class BarreCarre : Barre
    {
        public BarreCarre()
        {
            this.emplacement = new List<int>() { 5, 6, 15, 16 };
            this.couleur = new SolidColorBrush(Couleurs[r.Next(Couleurs.Count)]);
        }
        public override void Tourner(ref Barre barre, ref Table grille)
        {
            
        }
    }
}
