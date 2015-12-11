using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tetris
{
    class Partie
    {
        public MainWindow main;
        public double score;
        public Table grille;
        public Level level;
        public Barre test;
        public List<Rectangle> tabRect;

        public Partie(MainWindow main, Level level)
        {
            this.main = main;
            this.level = level;

            test = Barre.Create(); // Test est la barre qui sera en cours de placement 
            tabRect = new List<Rectangle>(180);
            main.banniere.Content = "GooOOoooo";
            this.grille = new Table(main, tabRect);
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, level.timer);
            messageTimer.Start();
            this.playSong("theme");

        }

        private void playSong(string nom)
        {
            /*
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation*/
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
       

    }
}
