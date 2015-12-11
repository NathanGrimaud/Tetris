using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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
        static public List<Thread> AllThreads;
        public MainWindow()
        {
            
            InitializeComponent();

            Closing += Window_Closing;
            AllThreads = new List<Thread>();
            Level level2 = new Level();


            main = this;            
            fenetre.Background = new SolidColorBrush(Colors.LightBlue);
            main = this;
            


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Je choppe l'évenement Key, si celui-ci envoie a droite, je balance a droite, si l'user envoie a gauche, je décale a gauche 
            if (e.Key == Key.Right && game != null)
            {
                game.test.Droite(ref game.grille);
            }
            else if (e.Key == Key.Left && game != null)
            {
                game.test.Gauche(ref game.grille);
            }
            else if (e.Key == Key.Down && game != null)
            {
                game.test.Accelerer(ref game.grille);
            }
            else if (e.Key == Key.Up && game != null)
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
                    numero = 1,
                    backgroundcolor = Colors.LightBlue,
                    Couleurs = new List<Color>()
        { Colors.DarkSlateGray, Colors.DarkRed, Colors.Blue, Colors.Maroon, Colors.DeepSkyBlue,
        Colors.DarkMagenta, Colors.Goldenrod, Colors.MediumTurquoise,Colors.MediumSlateBlue,
        Colors.PowderBlue,Colors.SandyBrown,Colors.SaddleBrown,Colors.DeepSkyBlue},
                decrement = 0,
                    musique = "level1.mp3",
                    timer = 500,
                    fill = new List<Color> { Colors.White }
                };
            }
            if (level2.IsChecked == true)
            {
                level = new Level()
                {
                    backgroundimage = "",
                    numero = 2,
                    backgroundcolor = Colors.LightBlue,
                    Couleurs = new List<Color>{ Colors.DarkSlateGray, Colors.DarkRed, Colors.Blue, Colors.Maroon, Colors.DeepSkyBlue,
        Colors.DarkMagenta, Colors.Goldenrod, Colors.MediumTurquoise,Colors.MediumSlateBlue,
        Colors.PowderBlue,Colors.SandyBrown,Colors.SaddleBrown,Colors.DeepSkyBlue},
                    decrement = 0,
                    musique = "",
                    timer = 100,
                    fill = new List<Color> { Colors.White }
                };
            }

            if (level3.IsChecked == true)
            {
                level = new Level()
                {
                    backgroundimage = "",
                    numero = 3,
                    backgroundcolor = Colors.LightBlue,
                    Couleurs = new List<Color>{ Colors.DarkSlateGray, Colors.DarkRed, Colors.Blue, Colors.Maroon, Colors.DeepSkyBlue,
        Colors.DarkMagenta, Colors.Goldenrod, Colors.MediumTurquoise,Colors.MediumSlateBlue,
        Colors.PowderBlue,Colors.SandyBrown,Colors.SaddleBrown,Colors.DeepSkyBlue},
                    decrement = 1,
                    musique = @"",
                    timer = 100,
                    fill = new List<Color> { Colors.White }
                };
            }

            if (level4.IsChecked == true)
            {
                level = new Level()
                {
                    backgroundimage = "",
                    numero = 4,
                    backgroundcolor = Colors.Black,
                    Couleurs = new List<Color>{ Colors.Red, Colors.Yellow, Colors.Orange, Colors.Magenta, Colors.Cyan, Colors.Purple, Colors.Green, Colors.Pink, Colors.Blue},
                    decrement = 0,

                    musique = "",

                    timer = 100,
                    fill = new List<Color> { Colors.White }
                };
            }

            if (level5.IsChecked == true)
            {
                level = new Level()
                {
                    backgroundimage = "",
                    numero = 5,
                    backgroundcolor = Colors.Black,
                    Couleurs = new List<Color> {Colors.White},
                    decrement = 1,
                    musique = @"",

                    timer = 200,
                    fill = new List<Color> { Colors.Red, Colors.Yellow, Colors.Orange, Colors.Magenta, Colors.Cyan, Colors.Purple, Colors.Green, Colors.Pink, Colors.Blue },
                };
            }
          

            menu.Visibility = Visibility.Collapsed;
            ScoreNom.Visibility = Visibility.Visible;
            ScoreScore.Visibility = Visibility.Visible;
            ScoreNiveau.Visibility = Visibility.Visible;
            labelScore.Visibility = Visibility.Visible;
            labelScoreLevel.Visibility = Visibility.Visible;
            labelScoreNom.Visibility = Visibility.Visible;
            game = new Partie(main, level);
        }
        ~MainWindow()
        {

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var t in AllThreads)// a appeler au restart partie
            {
                t.Abort();
            }
        }
    }
}
