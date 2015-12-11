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
        public List<int> emplacement = new List<int>();
        public bool bloquer;
        public SolidColorBrush couleur;
        public static List<Color> Couleurs = new List<Color>()
        { Colors.DarkSlateGray, Colors.DarkRed, Colors.Blue, Colors.Maroon, Colors.DeepSkyBlue,
        Colors.DarkMagenta, Colors.Goldenrod, Colors.MediumTurquoise,Colors.MediumSlateBlue,
        Colors.PowderBlue,Colors.SandyBrown,Colors.SaddleBrown,Colors.DeepSkyBlue};
        protected static Random r = new Random();
        protected int position = 0;
        public bool posInitiale = true;
        public Barre() { }

        public static Barre Create()
        {
            var type = r.Next(7);
            

            if (type == 0)
            {
                return new BarreT();
            }
            else if (type == 1)
            {
                return new BarreLongue();
            }
            else if (type == 2)
            {
                return new BarreL();
            }
            else if (type == 3)
            {
                return new BarreLinv();
            }
            else if (type == 4)
            {
                return new BarreZ();
            }
            else if (type == 5)
            {
                return new BarreZinv();
            }
            else
            {
                return new BarreCarre();
            }

        }
        public abstract void Tourner(ref Barre barre, ref Table grille);
        //vérifier le débordement sur un coté

        public bool Descendre(Table grille) // Pour faire descendre une barre 
        {
            bool placementvalide = grille.EmplacementDispo(this);
            List<int> precemplacement = new List<int>();

            for (int i = 0; i < this.emplacement.Count; i++)
            {
                // Je copie la liste des emplacement dans la barre dans une liste afin de les garder en mémoire en cas de Fail
                precemplacement.Add(this.emplacement[i]);
                // J'effectue le deplacement sur la vrai liste
                this.emplacement[i] = this.emplacement[i] + 10;
            }
            // je verifie si tout est okay. Si oui, j'écris les nouveaux emplacement dans le tableau
            if (grille.EmplacementDispo(this))
            {

                grille.write(ref precemplacement, this);
                this.posInitiale = false;
                return true;

            }
            else
            {
                // Sinon, je remets tout comme avant grace a la liste copié auparavant 
                this.emplacement = precemplacement;
                this.bloquer = true;

                grille.checkLigne();
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
                bool placementvalide = grille.EmplacementDispo(this);
                List<int> precemplacement = new List<int>();

                for (int i = 0; i < this.emplacement.Count; i++)
                {
                    // Je copie la liste des emplacement dans la barre dans une liste afin de les garder en mémoire en cas de Fail
                    precemplacement.Add(this.emplacement[i]);
                    // J'effectue le deplacement sur la vrai liste
                    this.emplacement[i] = this.emplacement[i] + 1;
                }
                // je verifie si tout est okay. Si oui, j'écris les nouveaux emplacement dans le tableau
                if (grille.EmplacementDispo(this) && grille.DepacementLargeur(this, precemplacement))
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
                bool placementvalide = grille.EmplacementDispo(this);


                List<int> precemplacement = new List<int>();

                for (int i = 0; i < this.emplacement.Count; i++)
                {
                    // Je copie la liste des emplacement dans la barre dans une liste afin de les garder en mémoire en cas de Fail
                    precemplacement.Add(this.emplacement[i]);
                    // J'effectue le deplacement sur la vrai liste
                    this.emplacement[i] = this.emplacement[i] - 1;
                }

                // je verifie si tout est okay. Si oui, j'écris les nouveaux emplacement dans le tableau
                if (grille.EmplacementDispo(this) && grille.DepacementLargeur(this, precemplacement))
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
            this.Descendre(grille);
        }
        

    }
}
