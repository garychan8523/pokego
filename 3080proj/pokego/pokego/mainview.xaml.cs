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
using System.Windows.Shapes;

namespace pokego
{
    /// <summary>
    /// Interaction logic for mainview.xaml
    /// </summary>
    public partial class mainview : Window
    {
        Random rnd = new Random();
        private Pokeworld currentWorld;
        private PokeTrainer currentPlayer;

        public mainview(Pokeworld currentWorld, PokeTrainer currentPlayer)
        {
            InitializeComponent();
            InitiateRealTimer();
            InitiateSpawnTimer();
            this.currentWorld = currentWorld;
            this.currentPlayer = currentPlayer;
        }

        private void InitiateRealTimer()
        {
            System.Windows.Threading.DispatcherTimer realTimer = new System.Windows.Threading.DispatcherTimer();
            realTimer.Tick += realTimer_Tick;
            realTimer.Interval = TimeSpan.FromSeconds(1);
            realTimer.Start();
        }

        int secondsCounter = 0;
        private void realTimer_Tick(object sender, EventArgs e)
        {
            secondsCounter++;
            txtTime.Text = "  Elapsed time: " + secondsCounter.ToString() + "  ";
        }

        private void InitiateSpawnTimer()
        {
            System.Windows.Threading.DispatcherTimer generationTimer = new System.Windows.Threading.DispatcherTimer();
            generationTimer.Tick += generationTimer_Tick;
            generationTimer.Interval = TimeSpan.FromSeconds(1);        // 10 for ten seconds, 1 for one second
            generationTimer.Start();
        }

        private void generationTimer_Tick(object sender, EventArgs e)
        {
            int random = rnd.Next(1, 11);
            System.Diagnostics.Debug.WriteLine("randon number: " + random);
            System.Diagnostics.Debug.Write("10 seconds passed, ");

            for (int i = 0; i < currentWorld.currentItemImage.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("item get left: " + Canvas.GetLeft(currentWorld.currentItemImage[i]));
                if (Canvas.GetLeft(currentWorld.currentItemImage[i]) <= -67 || Canvas.GetLeft(currentWorld.currentItemImage[i]) >= 970)
                {
                    cvspawnarea.Children.Remove(currentWorld.currentItemImage[i]);
                    currentWorld.removeItem(i);
                }
            }

            if (random % 2 == 0 && currentWorld.itemcounter < 5)                    // 2 for 50%; 3 for 33%
            {
                spawnPokemon();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("nothing happened.");
            }
        }

        private void spawnPokemon()
        {
            System.Diagnostics.Debug.WriteLine("new pokemon appear!");

            Pokemon target = currentWorld.addPokemon();
            DrawPokemon(target);

            txtPokeStatus.Text = target.Name + " appear ! Click to catch it !";
            System.Diagnostics.Debug.WriteLine("Time: " + secondsCounter.ToString());
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // when keydown is left
            if (e.Key == Key.Left && cvpokeworldDrawing.Margin.Left < 5900)
                {
                    currentWorld.stepIncrement();
                    // shift background and item image to right
                    double position = cvpokeworldDrawing.Margin.Left + 15;
                    txtPokeStatus.Text = "Heading left, PostionX: " + position.ToString();
                    cvpokeworldDrawing.Margin = new Thickness(position, 0, 0, 0);
                    foreach (Rectangle item in currentWorld.currentItemImage)
                    {
                        Canvas.SetLeft(item, Canvas.GetLeft(item) + 15);
                    }

                    //change character picture
                    if (currentWorld.stepcounter % 3 == 0)
                    {
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/poketrainer_left.png"));
                        cvtrainer.Background = ib;
                    }
                    else if (currentWorld.stepcounter % 3 == 1)
                    {
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/poketrainer_left1.png"));
                        cvtrainer.Background = ib;
                    }
                    else if (currentWorld.stepcounter % 3 == 2)
                    {
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/poketrainer_left2.png"));
                        cvtrainer.Background = ib;
                    }
                }
            // when keydown is right
            if (e.Key == Key.Right && cvpokeworldDrawing.Margin.Left > -11215)
                {
                    currentWorld.stepIncrement();
                    // shift background to left
                    double position = cvpokeworldDrawing.Margin.Left - 15;
                    txtPokeStatus.Text = "Heading right, PostionX: " + position.ToString();
                    cvpokeworldDrawing.Margin = new Thickness(position, 0, 0, 0);

                    foreach (Rectangle item in currentWorld.currentItemImage)
                    {
                        Canvas.SetLeft(item, Canvas.GetLeft(item) - 15);
                    }

                    // set character animation
                    if (currentWorld.stepcounter % 3 == 0)
                    {
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/poketrainer_right.png"));
                        cvtrainer.Background = ib;
                    }
                    else if (currentWorld.stepcounter % 3 == 1)
                    {
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/poketrainer_right1.png"));
                        cvtrainer.Background = ib;
                    }
                    else if (currentWorld.stepcounter % 3 == 2)
                    {
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/poketrainer_right2.png"));
                        cvtrainer.Background = ib;
                    }
                }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
            {
                txtPokeStatus.Text = "Resting, PostionX: " + cvpokeworldDrawing.Margin.Left.ToString();
                // set character animation
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/poketrainer_rest.png"));
                cvtrainer.Background = ib;
            }
        }

        private void DrawPokemon(Pokemon pokemon)
        {
            Rectangle pokeimage = new Rectangle();
            pokeimage.MouseLeftButtonDown += pokeimage_MouseLeftButtonDown;
            pokeimage.Width = 64;
            pokeimage.Height = 64;
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/" + pokemon.Name + ".png"));
            pokeimage.Fill = ib;

            Canvas.SetLeft(pokeimage, rnd.Next(916));
            Canvas.SetTop(pokeimage, 310);
            Canvas.SetZIndex(pokeimage, (int)110);

            currentWorld.currentItemImage.Add(pokeimage);
            cvspawnarea.Children.Add(pokeimage);
        }

        private void pokeimage_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Rectangle targetImage = (Rectangle)sender;
            Window screen_catch = new catchview(currentPlayer, (Pokemon)currentWorld.currentItem[cvspawnarea.Children.IndexOf(targetImage)], currentWorld, cvspawnarea, targetImage);
            screen_catch.Show();
        }

        // when main view is closing, we show back the start screen
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
                e.Cancel = true;
                this.Visibility = Visibility.Collapsed;

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Visibility = Visibility.Visible;
                    }
                }
        }

        private void cvtrainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window screen_inventory = new inventoryview(currentPlayer);
            screen_inventory.Show();
        }

    }
}
