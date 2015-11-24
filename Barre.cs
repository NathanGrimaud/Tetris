using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris
{
    
    public class Barre
    {        
        public List<int> emplacement = new List<int>() ;
        public bool bloquer;
        public SolidColorBrush couleur;
        private static List<Color> Couleurs = new List<Color>()
        { Colors.DarkSlateGray, Colors.DarkRed, Colors.Blue, Colors.Maroon, Colors.DeepSkyBlue,
        Colors.DarkMagenta, Colors.Goldenrod, Colors.MediumTurquoise,Colors.MediumSlateBlue,
        Colors.PowderBlue,Colors.SandyBrown,Colors.SaddleBrown,Colors.DeepSkyBlue};
        static Random r = new Random();

        public Barre()
        {
            this.emplacement = new List<int>() { 4,5};
            this.couleur = new SolidColorBrush(Couleurs[r.Next(Couleurs.Count)]);
        }
    }
}
