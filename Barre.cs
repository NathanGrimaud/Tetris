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

        public Barre() { }

        public static Barre Create()
        {
            var type = r.Next(7);
            return new BarreZinv();
            /*
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
            */
        }
        public abstract void Tourner(ref Barre barre);

    }
}
