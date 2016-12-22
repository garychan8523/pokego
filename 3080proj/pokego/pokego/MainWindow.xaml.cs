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

namespace pokego
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Threading.DispatcherTimer animationTimer = new System.Windows.Threading.DispatcherTimer();
            animationTimer.Tick += animationTimer_Tick;
            animationTimer.Interval = TimeSpan.FromSeconds(0.08);
            animationTimer.Start();
        }
        int animationCounter = 0;
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            
            if (animationCounter < 10)
            {
                lblStartGame.Opacity -= 0.1;
            }
            if (animationCounter >= 10)
            {
                lblStartGame.Opacity += 0.2;
            }
            animationCounter++;
            if (animationCounter == 15) { animationCounter = 0; }
        }

        Pokeworld currentWorld;
        PokeTrainer currentPlayer;

        // click screen to start a new game
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if(currentWorld == null)
            {
                this.Visibility = Visibility.Collapsed;
                currentWorld = new Pokeworld();
                currentPlayer = new PokeTrainer();
                Window screen_main = new mainview(currentWorld, currentPlayer);
                screen_main.Show();
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(mainview))
                    {
                        (window as mainview).Visibility = Visibility.Visible;
                    }
                }
                //Window screen_main = mainview();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
