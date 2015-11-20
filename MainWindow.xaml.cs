using System;
using System.Collections.Generic;
using System.Linq;
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
        Barre test = new Barre();
        List<Rectangle> tabRect = new List<Rectangle>();
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer  messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            messageTimer.Start();
            this.loadGrid();
        }

        private void messageTimer_Tick(object sender, EventArgs e)
        {            
            grille.Descendre(ref test);           
            //E1.Content = test.emplacement[0];
            //E2.Content = test.emplacement[1];
            //E3.Content = test.emplacement[2];
            Timer.Content = DateTime.Now.Second;                      
            if (test.bloquer)
            {
                test = new Barre();
            }
        }
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
            tabRect.ForEach(delegate (Rectangle rect) {
                var id = int.Parse(rect.Tag.ToString());

                rect.Fill = new SolidColorBrush(Colors.White);
                if (test.emplacement.Contains(id))
                {
                    rect.Fill = new SolidColorBrush(Colors.Maroon);
                }

            });
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Right)
            {             
                grille.Droite(ref test);
            }
            else if (e.Key == Key.Left)
            {               
                grille.Gauche(ref test);
            }
        }
}
}
