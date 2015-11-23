﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Table
    {
        public Barre[] tableau = new Barre[180];
        public Table()
        {

        }
        public bool Placer (Barre aplacer)
        {
            bool placementvalide = EmplacementDispo(ref aplacer);

            foreach (int emplacement in aplacer.emplacement)
            {
                tableau[emplacement] = aplacer;           
            }

            return true;
        }

        public bool Descendre(ref Barre aplacer)
        {
            bool placementvalide = EmplacementDispo(ref aplacer);
            List<int> precemplacement = new List<int>();

            for(int i = 0; i < aplacer.emplacement.Count; i++)
            {
                precemplacement.Add(aplacer.emplacement[i]);
                aplacer.emplacement[i] = aplacer.emplacement[i] + 10;
            }
             
            if (EmplacementDispo(ref aplacer)) {

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
                aplacer.emplacement = precemplacement;
                aplacer.bloquer = true;
                return false;
            }
            
        }


        public bool Droite(ref Barre aplacer)
        {
            if (!aplacer.bloquer) { 
            bool placementvalide = EmplacementDispo(ref aplacer);
            List<int> precemplacement = new List<int>();

            for (int i = 0; i < aplacer.emplacement.Count; i++)
            {
                precemplacement.Add(aplacer.emplacement[i]);
                aplacer.emplacement[i] = aplacer.emplacement[i] + 1;
            }

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
                aplacer.emplacement = precemplacement;
                return false;
            }

        }
            return false;
        }

        public bool Gauche(ref Barre aplacer)
        {
            if (!aplacer.bloquer)
            {
                bool placementvalide = EmplacementDispo(ref aplacer);
                List<int> precemplacement = new List<int>();

                for (int i = 0; i < aplacer.emplacement.Count; i++)
                {
                    precemplacement.Add(aplacer.emplacement[i]);
                    aplacer.emplacement[i] = aplacer.emplacement[i] - 1;
                }

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
                    aplacer.emplacement = precemplacement;
                    return false;
                }

            }
            return false;
        }

        public bool EmplacementDispo(ref Barre acheck)
        {
           
            foreach (int emplacement in acheck.emplacement)
            {
                if (emplacement >= 180)
                    return false;

                else if (tableau[emplacement] == null || tableau[emplacement] == acheck) { }

                else
                    return false;                

            }

            return true;
        }

        public bool DepacementLargeur(ref Barre acheck, List<int> precemplacement)
        {
            for (int i = 0; i < acheck.emplacement.Count; i++)
            {
                if (acheck.emplacement[i] / 10 != precemplacement[i] / 10)
                    return false;
            }

            return true;

        }

        public void checkLigne()
        {

            for (int i = 17; i > 0; i--)
            {
                int j = i*10;
                bool entier = true;
                while (j != i*10+9 && entier)
                {
                    if(tableau[j] == null)
                    {
                        entier = false;
                    }

                    j++;
                }

                if (entier)
                    this.supprimerLigne(i * 10);

            }

        }

        public void supprimerLigne(int j)
        {
           for(int i = 0; i <= 9; i++)
            {
                this.tableau[j + i] = null;
            }

            for (int i = j; i >= 0; i--) 
            {
                if (this.tableau[i] != null) { 
                    Barre encours = this.tableau[i];
                    while (this.Descendre(ref encours));
                }

            }
            }
    }

}
