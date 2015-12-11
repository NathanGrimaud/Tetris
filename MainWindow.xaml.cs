using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Tetris
{
    
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Partie game;
        public static MainWindow main;

        public MainWindow()
        {
            
            InitializeComponent();
            
            Level level2 = new Level();

            main = this;            


            fenetre.Background = new SolidColorBrush(Colors.LightBlue);
 
            main = this;
            


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Je choppe l'évenement Key, si celui-ci envoie a droite, je balance a droite, si l'user envoie a gauche, je décale a gauche 
            if (e.Key == Key.Right)
            {
                game.test.Droite(ref game.grille);
            }
            else if (e.Key == Key.Left)
            {
                game.test.Gauche(ref game.grille);
            }
            else if (e.Key == Key.Down)
            {
                game.test.Accelerer(ref game.grille);
            }
            else if (e.Key == Key.Up)
            {
                game.test.Tourner(ref game.test, ref game.grille);
            }
            if(game != null)
                game.grille.fillGrid(game.tabRect);
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            Level level = new Level();
            if (level1.IsChecked == true) {
                level = new Level()
                {
                    backgroundimage = "",
                    backgroundcolor = Colors.LightBlue,
                    Couleurs = new List<Color>()
        { Colors.DarkSlateGray, Colors.DarkRed, Colors.Blue, Colors.Maroon, Colors.DeepSkyBlue,
        Colors.DarkMagenta, Colors.Goldenrod, Colors.MediumTurquoise,Colors.MediumSlateBlue,
        Colors.PowderBlue,Colors.SandyBrown,Colors.SaddleBrown,Colors.DeepSkyBlue},
                decrement = 0,
                    musique = @"",
                    timer = 500,
                };
            }
            if (level2.IsChecked == true)
            {
                level = new Level()
                {
                    backgroundimage = "",
                    backgroundcolor = Colors.LightBlue,
                    Couleurs = new List<Color>{ Colors.DarkSlateGray, Colors.DarkRed, Colors.Blue, Colors.Maroon, Colors.DeepSkyBlue,
        Colors.DarkMagenta, Colors.Goldenrod, Colors.MediumTurquoise,Colors.MediumSlateBlue,
        Colors.PowderBlue,Colors.SandyBrown,Colors.SaddleBrown,Colors.DeepSkyBlue},
                    decrement = 0,
                    musique = @"",
                    timer = 100,
                };
            }
            if (level3.IsChecked == true)
            {
                level = new Level()
                {
                    backgroundimage = "",
                    backgroundcolor = Colors.Black,
                    Couleurs = new List<Color>{ Colors.Green},
                    decrement = 0,
                    musique = @"",
                    timer = 200,
                };
            }
            menu.Visibility = Visibility.Collapsed;
            game = new Partie(main, level);
        }
    }
}
