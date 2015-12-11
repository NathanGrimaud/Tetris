using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows;

namespace Tetris
{
    class Partie
    {
        public MainWindow main;
        public Table grille;
        public Level level;
        public Barre test;
        public static DispatcherTimer messageTimer;
        public List<Rectangle> tabRect;

        public double score;
        public static Partie partie;
        public bool enregistré;
        public Score ScoresPartie;


        public Partie(MainWindow main, Level level)

        {
            ScoresPartie = new Score();
            ScoresPartie.Read();
            this.enregistré = false;
            this.main = main;
            this.level = level;

            Barre.Couleurs = level.Couleurs;
            main.fenetre.Background = new SolidColorBrush(level.backgroundcolor);

            if (level.numero == 4) { 
                main.nyan.Visibility = System.Windows.Visibility.Visible;
                main.image.Visibility = System.Windows.Visibility.Visible;
            }

            test = Barre.Create(); // Test est la barre qui sera en cours de placement 
            tabRect = new List<Rectangle>(180);
            main.banniere.Content = "GooOOoooo";
            this.grille = new Table(main, tabRect, level);
            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, level.timer);
            messageTimer.Start();
            this.playSong("theme");
            partie = this;

            if(level.numero == 3)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 360;
                da.Duration = new Duration(TimeSpan.FromSeconds(3));
                da.RepeatBehavior = RepeatBehavior.Forever;
                RotateTransform rt = new RotateTransform(360, main.gameGrid.Width / 2, main.gameGrid.Height / 2);
                main.gameGrid.RenderTransform = rt;
                rt.BeginAnimation(RotateTransform.AngleProperty, da);

            }
            if(level.numero == 5)
            {
                main.gameGrid.RenderTransform = new RotateTransform(180,main.gameGrid.Width /2, main.gameGrid.Height / 2);

                for (int i = 0; i < tabRect.Count; i++)
                {                  
                        tabRect[i].Fill = new SolidColorBrush(Colors.Black);
                }
            }
        }

       

        private void playSong(string nom)
        {
           
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();
           
        }

        private void messageTimer_Tick(object sender, EventArgs e)
        {
            if (level.numero == 4)
                grille.vibrate();
            main.banniere.Content = "";
            test.Descendre(grille);
            main.Timer.Content = DateTime.Now.Second;

            if (test.bloquer)
            {
                // Si test est bloqué, car il a touché un autre objet, alors on recréer une barre 
                test = Barre.Create();
            }

            grille.fillGrid(tabRect);

        }
       public static void stop()
        {
            if(messageTimer.IsEnabled)
            {
                messageTimer.Stop();
            }
            var score = partie.getScore();
            if (!partie.enregistré)
            {
                partie.enregistré = true;
                score.Write();

            }                
        }
        public Score getScore()
        {
            ScoresPartie.Enregistrer(this.level.numero,this.score, main.NomScore.Text);
            return ScoresPartie;
        }

    }
}
