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
        
        Table grille = new Table();
        Barre test = Barre.Create(); // Test est la barre qui sera en cours de placement 
        List<Rectangle> tabRect = new List<Rectangle>(180);
        public MainWindow()
        {

            InitializeComponent();
            // Création du timer 
            DispatcherTimer  messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            messageTimer.Start();
            this.loadGrid();
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
            // Tout les ticks, je fait descendre la barre en cours "test"         
            grille.Descendre(ref test);           
            Timer.Content = DateTime.Now.Second;     
                             
            if (test.bloquer)
            {
                // Si test est bloqué, car il a touché un autre objet, alors on recréer une barre 
                test = Barre.Create();
            }

            this.fillGrid();
        }
        // NathanGrimaud sera aussi prié de commenté son code :p
        public void loadGrid()
        {
            var left = 1;
            var top = 1;

            for (int i = 1; i < 181; i++)
            {
                var rect = new Rectangle();
                rect.Height = 20;
                rect.Width = 20;
                rect.Stroke = new SolidColorBrush(Colors.LightGray);
                rect.Tag = i-1;
                Canvas.SetLeft(rect, left*20);
                Canvas.SetTop(rect, top*20);
                gameGrid.Children.Add(rect);
                tabRect.Add(rect);

                if (i%10==0 && i!=0)
                {
                    top++;
                    left = 1;
                }
                else
                {
                    left ++;
                }
            }
        }        
        public  void fillGrid()
        {

            for (int i = 0; i < tabRect.Count; i++)
            {
                if (grille.tableau[i] != null)
                    tabRect[i].Fill = grille.tableau[i].couleur;
                else
                    tabRect[i].Fill = new SolidColorBrush(Colors.White);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Je choppe l'évenement Key, si celui-ci envoie a droite, je balance a droite, si l'user envoie a gauche, je décale a gauche 
            if(e.Key == Key.Right)
            {             
                grille.Droite(ref test);

            }
            else if (e.Key == Key.Left)
            {               
                grille.Gauche(ref test);
            }
            else if (e.Key == Key.Down)
            {
                grille.Accelerer(ref test);
            }
            else if (e.Key == Key.Up)
            {
                test.Tourner(ref test,ref grille);
            }
            this.fillGrid();
        }
}
}
