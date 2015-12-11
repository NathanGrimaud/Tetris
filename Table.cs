using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tetris
{
    public class Table
    {
        public Barre[] tableau = new Barre[180];
        public MainWindow windows;
        bool bzz = true;
        Level lvl;
        protected static Random r = new Random();


        public Table(MainWindow main, List<Rectangle> tabRect, Level lvl) {

            this.lvl = lvl;
            var left = 1;
            var top = 1;

            this.windows = main;

            for (int i = 1; i < 181; i++)
            {
                var rect = new Rectangle();
                rect.Height = 20;
                rect.Width = 20;
                rect.Stroke = new SolidColorBrush(Colors.LightGray);
                rect.Tag = i - 1;
                Canvas.SetLeft(rect, left * 20);
                Canvas.SetTop(rect, top * 20);
                main.gameGrid.Children.Add(rect);
                tabRect.Add(rect);

                if (i % 10 == 0 && i != 0)
                {
                    top++;
                    left = 1;
                }
                else
                {
                    left++;
                }
            }

            
        } // Constructeur         
    
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

        public void fillGrid(List<Rectangle> tabRect)
        {
            
            for (int i = 0; i < tabRect.Count; i++)
            {
                if (this.tableau[i] != null)
                    tabRect[i].Fill = this.tableau[i].couleur;
                // SI le niveau est a 5, il faut alors attribuer une couleur aléatoire à chaque endroit laissé vide précédement
                else if(lvl.numero != 5)
                    tabRect[i].Fill = new SolidColorBrush(lvl.fill[r.Next(lvl.fill.Count)]);
                else if(lvl.numero == 5 && tabRect[i].Fill.ToString() == Colors.Black.ToString() || tabRect[i].Fill.ToString() == Colors.White.ToString())
                    tabRect[i].Fill = new SolidColorBrush(lvl.fill[r.Next(lvl.fill.Count)]);
                    
            }
        }

        public bool DepacementLargeur(Barre atest, List<int> precemplacement)
        {
            for (int i = 0; i < atest.emplacement.Count; i++)
            {
                // Je vérifie que les déplacement en largeur reste bien sur la même ligne
                if (atest.emplacement[i] / 10 != precemplacement[i] / 10)
                    return false;
            }

            return true;

        }

        public bool EmplacementDispo(Barre test)
        {
            // Je tourne chaque emplacement de la barre a check 
            List<int> precemplacement = new List<int>();

            foreach (int emplacement in test.emplacement)
            {
                // Si l'emplacement est en dehors des limites du tableau, je return false

                if (emplacement >= 180)
                    return false;
                if (emplacement < 0)
                    return false;


                // Sinon si l'emplacement du tableau est vide ou corresponds déja a la même barre, alors je continue le foreach 
                else if (this.tableau[emplacement] == null || this.tableau[emplacement] == test)
                {
                    precemplacement.Add(emplacement);
                }

                // Sinon, je retourne faux 
                else
                    return false;
            }

            precemplacement = precemplacement.OrderByDescending(k => k).ToList();

            foreach (int emplacement in test.emplacement)
            {
                if (precemplacement.Contains(emplacement + 1))
                {
                    if ((emplacement + 1) / 10 != emplacement / 10)
                        return false;
                }
            }
            return true;
        }

        public void supprimerLigne(int j)
        {
            // Une barre désagrégé est une barre qui a perdu une partie de soit pendant la suppression
            List<Barre> BarreDesagrégé = new List<Barre>();

            // Cette boucle for passe en revu toute la ligne qui doit être supprimé
            //Pour supprimer une ligne, je passe par chaque index du tableau correspondant à cette ligne, et je les mets à null 

            for (int i = 0; i <= 9; i++)
            {

                if (this.tableau[j + i] != null)
                {
                    if (this.tableau[j + i].emplacement.Contains(j + i))
                    {
                        // J'ajoute la barre qui perd une case dans le tableau de barre desagragé
                        BarreDesagrégé.Add(this.tableau[j + i]);

                        // Je supprime l'emplacement de la liste
                        this.tableau[j + i].emplacement.Remove(j + i);

                        // Je supprime l'emplacement du tableau 
                        this.tableau[j + i] = null;

                    }

                }


            }
            // Je fait tourner toutes les barres desagrégé, pour les faires descendre carré par carré 
            foreach (var encours in BarreDesagrégé)
            {
                List<int> newlist = new List<int>();
                // Je copie ma liste d'emplacement dans une nouvelle
                foreach (var emp in encours.emplacement)
                {
                    newlist.Add(emp);
                    this.tableau[emp] = null;
                }

                // Je la tri par ordre descendant 
                newlist = newlist.OrderByDescending(k => k).ToList();

                // Puis j'écris grâce à cette nouvelle liste dans le tableau et dans la barre concerné 
                foreach (var emp in newlist)
                {
                    encours.emplacement.Remove(emp);
                    int newemp = descendreCut(encours, emp);
                    this.tableau[newemp] = encours;
                    encours.emplacement.Add(newemp);
                }
            }
            // Je fait tourner le reste du tableau du bas vers le haut, a partir de la ligne supprime (j étant la coordonnée de la première case de la ligne supprimé) 
            for (int i = j; i >= 0; i--)
            {
                if (this.tableau[i] != null)
                {
                    Barre encours = this.tableau[i];


                    // Puis, tant que les barre du dessus peuvent descendre, elle descende 
                    while (encours.Descendre(this)) ;

                }
            }

        }

        // Cette fonction me permet de descendre emplacement par emplacement une barre. Necessaire lorsque une barre est désagrégé. 
        public int descendreCut(Barre desc, int emp)
        {
            while ((emp + 10) < 180 && (this.tableau[emp + 10] == null))
            {
                emp = emp + 10;
            }
            return emp;
        }

        public void checkLigne()
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
                    if (this.tableau[j] == null)
                    {
                        entier = false;
                    }

                    j++;
                }

                // Si il n'y a pas eu de trou dans la ligne, je la supprime
                if (entier)
                {
                    Partie.partie.score += 100;
                    MainWindow.main.ScoreActuel.Text = Partie.partie.score.ToString();
                    this.supprimerLigne(i * 10);
                    this.checkLigne();
                }
            }
        }

        public void vibrate()
        {
            if (bzz)
            {
                windows.gameGrid.Margin = new Thickness {
                   Left = 50,
                   Top = 30
                };
                bzz = false;
            }
            else
            {
                windows.gameGrid.Margin = new Thickness
                {
                    Left = 55,
                    Top = 35
                };
                bzz = true;
            }

        }



    }

}
