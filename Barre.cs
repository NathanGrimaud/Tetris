using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    
    public abstract class Barre 
    {        
        public List<int> emplacement = new List<int>() ;
        public bool bloquer;
        public SolidColorBrush couleur;
        protected static List<Color> Couleurs = new List<Color>()
        { Colors.DarkSlateGray, Colors.DarkRed, Colors.Blue, Colors.Maroon, Colors.DeepSkyBlue,
        Colors.DarkMagenta, Colors.Goldenrod, Colors.MediumTurquoise,Colors.MediumSlateBlue,
        Colors.PowderBlue,Colors.SandyBrown,Colors.SaddleBrown,Colors.DeepSkyBlue};
        protected static Random r = new Random();
        protected int position = 0;
        public bool posInitiale = true;
        public Barre() { }

        public static Barre Create()
        {
            //var type = r.Next(7);
            return new BarreLongue();

            //if (type == 0)
            //{
            //    return new BarreT();
            //}
            //else if (type == 1)
            //{
            //    return new BarreLongue();
            //}
            //else if (type == 2)
            //{
            //    return new BarreL();
            //}
            //else if (type == 3)
            //{
            //    return new BarreLinv();
            //}
            //else if (type == 4)
            //{
            //    return new BarreZ();
            //}
            //else if (type == 5)
            //{
            //    return new BarreZinv();
            //}
            //else
            //{
            //    return new BarreCarre();
            //}

        }
        public abstract void Tourner(ref Barre barre, ref Table grille);
        //vérifier le débordement sur un coté

        public bool Descendre(ref Table grille) // Pour faire descendre une barre 
        {
            bool placementvalide = EmplacementDispo(ref grille);
            List<int> precemplacement = new List<int>();

            for (int i = 0; i < this.emplacement.Count; i++)
            {
                // Je copie la liste des emplacement dans la barre dans une liste afin de les garder en mémoire en cas de Fail
                precemplacement.Add(this.emplacement[i]);
                // J'effectue le deplacement sur la vrai liste
                this.emplacement[i] = this.emplacement[i] + 10;
            }
            // je verifie si tout est okay. Si oui, j'écris les nouveaux emplacement dans le tableau
            if (EmplacementDispo(ref grille))
            {

                grille.write(ref precemplacement, this);
                this.posInitiale = false;          
                return true;

            }
            else
            {
                // Sinon, je remets tout comme avant grace a la liste copié auparavant 
                this.checkLigne(ref grille);
                this.emplacement = precemplacement;
                this.bloquer = true;
                if (this.posInitiale)
                {
                   //fin du jeu
                }
                return false;
            }

        }


        public bool Droite(ref Table grille)
        {
            // Je vérifie qu'on essaye pas de deplacer un objet non bloqué 
            if (!this.bloquer)
            {
                bool placementvalide = EmplacementDispo(ref grille);
                List<int> precemplacement = new List<int>();

                for (int i = 0; i < this.emplacement.Count; i++)
                {
                    // Je copie la liste des emplacement dans la barre dans une liste afin de les garder en mémoire en cas de Fail
                    precemplacement.Add(this.emplacement[i]);
                    // J'effectue le deplacement sur la vrai liste
                    this.emplacement[i] = this.emplacement[i] + 1;
                }
                // je verifie si tout est okay. Si oui, j'écris les nouveaux emplacement dans le tableau
                if (EmplacementDispo(ref grille) && DepacementLargeur(ref grille, precemplacement))
                {

                    grille.write(ref precemplacement, this);

                    return true;

                }
                else
                {
                    // Sinon, je remets tout comme avant grace a la liste copié auparavant 
                    this.emplacement = precemplacement;
                    return false;
                }

            }
            return false;
        }

        public bool Gauche(ref Table grille)
        {
            // Je vérifie qu'on essaye pas de deplacer un objet non bloqué 
            if (!this.bloquer)
            {
                bool placementvalide = EmplacementDispo(ref grille);


                List<int> precemplacement = new List<int>();

                for (int i = 0; i < this.emplacement.Count; i++)
                {
                    // Je copie la liste des emplacement dans la barre dans une liste afin de les garder en mémoire en cas de Fail
                    precemplacement.Add(this.emplacement[i]);
                    // J'effectue le deplacement sur la vrai liste
                    this.emplacement[i] = this.emplacement[i] - 1;
                }

                // je verifie si tout est okay. Si oui, j'écris les nouveaux emplacement dans le tableau
                if (EmplacementDispo(ref grille) && DepacementLargeur(ref grille, precemplacement))
                {

                    grille.write(ref precemplacement, this);


                    return true;

                }
                else
                {
                    // Sinon, je remets tout comme avant grace a la liste copié auparavant 
                    this.emplacement = precemplacement;
                    return false;
                }

            }
            return false;
        }
        public void Accelerer(ref Table grille)
        {
            this.Descendre(ref grille);
        }

        public bool EmplacementDispo(ref Table grille)
        {
            // Je tourne chaque emplacement de la barre a check 
            foreach (int emplacement in this.emplacement)
            {
                // Si l'emplacement est en dehors des limites du tableau, je return false

                if (emplacement >= 180)
                    return false;
                if (emplacement < 0)
                    return false;
                // Sinon si l'emplacement du tableau est vide ou corresponds déja a la même barre, alors je continue le foreach 
                else if (grille.tableau[emplacement] == null || grille.tableau[emplacement] == this) { }

                // Sinon, je retourne faux 
                else
                    return false;
            }

            return true;
        }

        public bool DepacementLargeur(ref Table grille, List<int> precemplacement)
        {
            for (int i = 0; i < this.emplacement.Count; i++)
            {
                // Je vérifie que les déplacement en largeur reste bien sur la même ligne
                if (this.emplacement[i] / 10 != precemplacement[i] / 10)
                    return false;
            }

            return true;

        }

        public void checkLigne(ref Table grille)
        {
            // Je tourne les 18 lignes du tableau
            for (int i = 17; i > 0; i--)
            {
                // pour chaque ligne, je multiplie par 10 afin de transformer j en index du tableau
                int j = i * 10;
                bool entier = true;
                // Tant que j n'est pas arrivé au bout de la ligne et qu'il n'y a pas de trou dans la ligne je boucle 
                while (j != i * 10 + 10 && entier) 
                {
                    if (grille.tableau[j] == null)
                    {
                        entier = false;
                    }

                    j++;
                } 

                // Si il n'y a pas eu de trou dans la ligne, je la supprime
                if (entier)
                {
                    this.supprimerLigne(i * 10, ref grille);
                    this.checkLigne(ref grille);
                }
            }
        }

        public void supprimerLigne(int j, ref Table grille)
        {
            //Pour supprimer une ligne, je passe par chaque index du tableau correspondant à cette ligne, et je les mets à null 
            for (int i = 0; i <= 9; i++)
            {

                if(grille.tableau[j + i] != null)
                {
                    if (grille.tableau[j + i].emplacement.Contains(j + i))
                        grille.tableau[j + i].emplacement.Remove(j + i);
                }              
                grille.tableau[j + i] = null;
            }
            for (int i = j; i >= 0; i--)
            {
                if (grille.tableau[i] != null)
                {
                    Barre encours = grille.tableau[i];
                    // Puis, tant que les barre du dessus peuvent descendre, elle descende 
                    // ne marche pas quand la barre n'est pas complète, 
                    //car les emplacements ne sont pas supprimés
                    List<int> newlist = new List<int>();
                    List<int> preclist = new List<int>();

                    foreach (var emp in encours.emplacement)
                    {
                        grille.tableau[emp] = null;
                        grille.tableau[descendreCut(ref grille, emp)] = encours;
                        newlist.Add(descendreCut(ref grille, emp));
                    }
                    
                    encours.emplacement = newlist;


                }
            }
            foreach (var emp in grille.tableau)
            {
                //if (emp != null)
                   // while(emp.Descendre(ref grille));
            }
        }
        public int descendreCut(ref Table grille, int emp)
        {
            while (emp + 10 < 180 && grille.tableau[emp + 10] != null)
            {
                emp = emp + 10;
            }
            return emp;                                                                                                                                                                                                                                                                  
        }

    }
}
