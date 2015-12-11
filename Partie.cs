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
using System.Threading;

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
        bool read;
        MediaPlayer mp;
        public Partie(MainWindow main, Level level)

        {
           
            ScoresPartie = new Score();
            ScoresPartie.Read();
            this.enregistré = false;
            this.main = main;
            this.level = level;

            Barre.Couleurs = level.Couleurs;
            main.fenetre.Background = new SolidColorBrush(level.backgroundcolor);

            if (level.backgroundcolor == Colors.Black)
            {
                main.Termine.Foreground = new SolidColorBrush(Colors.White);
                main.ScoreNiveau.Foreground = new SolidColorBrush(Colors.White);
                main.ScoreNom.Foreground = new SolidColorBrush(Colors.White);
                main.ScoreScore.Foreground = new SolidColorBrush(Colors.White);
            }                
            else
            {
                main.Termine.Foreground = new SolidColorBrush(Colors.Black);
                main.ScoreNiveau.Foreground = new SolidColorBrush(Colors.Black);
                main.ScoreNom.Foreground = new SolidColorBrush(Colors.Black);
                main.ScoreScore.Foreground = new SolidColorBrush(Colors.Black);
            }
                
                

            test = Barre.Create(); // Test est la barre qui sera en cours de placement 
            tabRect = new List<Rectangle>(180);
            main.banniere.Content = "GooOOoooo";
            this.grille = new Table(main, tabRect);
            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);

            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, level.timer);

            messageTimer.Start();

            Thread myThread = new Thread(() => playSong());
            MainWindow.AllThreads.Add(myThread);
            myThread.Start();
            partie = this;
        }


        public void playSong()
        {
            
            MediaPlayer mp = new MediaPlayer();
            this.read = true;
            var source = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString() + "/Assets/" + level.musique;
            mp.MediaEnded += this.close;
            mp.Open(new Uri(source));
            while (read)
            {
                mp.Play();
            }
            mp.Close();
        }

        private void close(object sender, EventArgs e)
        {
            this.read = false;
        }

        private void messageTimer_Tick(object sender, EventArgs e)
        {
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
        ~Partie()
        {
            this.read = false;
        }

    }
}
