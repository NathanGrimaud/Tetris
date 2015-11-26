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
        //public static bool Placer (Barre aplacer, ref Table grille) // Pour placer une barre grace a la liste de ces coordonnées 
        //{
        //    bool placementvalide = aplacer.EmplacementDispo(ref grille);

        //    foreach (int emplacement in aplacer.emplacement)
        //    {
        //        grille.tableau[emplacement] = aplacer;           
        //    }

        //    return true;
        //}

       
        public void write (ref List<int> precbarre, Barre barre)
        {

            foreach (int emplacement in precbarre)
            {
                this.tableau[emplacement] = null;
            }

            foreach (int emplacement in barre.emplacement)
            {
                this.tableau[emplacement] = barre;
            }

        }
    }

}
