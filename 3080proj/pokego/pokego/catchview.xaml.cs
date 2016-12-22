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
    /// Interaction logic for catchview.xaml
    /// </summary>
    public partial class catchview : Window
    {
        private Pokemon target;
        private Pokeworld currentWorld;
        private PokeTrainer currentPlayer;
        private Canvas cvspawnarea;
        private String targetText;
        private String inputText = "";
        private Rectangle targetImage;

        public catchview(PokeTrainer currentPlayer, Pokemon target, Pokeworld currentWorld, Canvas cvspawnarea, Rectangle targetImage)
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(catchview_Closing);
            this.target = target;
            this.currentWorld = currentWorld;
            this.currentPlayer = currentPlayer;
            this.cvspawnarea = cvspawnarea;
            this.targetImage = targetImage;
            targetText = target.Name.ToUpper();
            CatchTurn(target);
        }

        // for whatever reason this window close, it's routed here
        // no matter player caught the pokemon or run away, we clear the target pokemon
        void catchview_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cvspawnarea.Children.Remove(targetImage);
            currentWorld.removeItem(target);
        }

        private void DrawPokemon(Pokemon pokemon)
        {
            Rectangle catchTargetImage = new Rectangle();
            catchTargetImage.Width = 64;
            catchTargetImage.Height = 64;
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/" + pokemon.Name + ".png"));
            catchTargetImage.Fill = ib;
            Canvas.SetZIndex(catchTargetImage, (int)150);
            cvcatchtarget.Children.Add(catchTargetImage);
        }

        private void CatchTurn(Pokemon target)
        {
            DrawPokemon(target);
            txtDescriptionIntro.Text = "Wild " + target.Name + " appeared! CP: " + target.Cp;
        }

        private void txtOptionRun_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        int TPGcounter = -1;
        private void txtOptionCatch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TPGcounter = 0;
            txtDescriptionIntro.Text = "Type \"" + target.Name + "\" on your keyboard.";
            txtOptionCatch.Visibility = Visibility.Collapsed;
            txtOptionRun.Visibility = Visibility.Collapsed;

            InitiateTPGTimer();
            txtTPGinput.Text = "";
            txtTPGinput.Visibility = Visibility.Visible;
            txtTPGtimer.Visibility = Visibility.Visible;
        }
        
        private bool typinggame()
        {
            if (targetText == inputText)
                return true;
            else
                return false;
        }

        private void InitiateTPGTimer()
        {
            System.Windows.Threading.DispatcherTimer tpgTimer = new System.Windows.Threading.DispatcherTimer();
            tpgTimer.Tick += tpgTimer_Tick;
            tpgTimer.Interval = TimeSpan.FromSeconds(1);       // 5 for five seconds
            tpgTimer.Start();
        }
        int addFlag = 0;
        private void tpgTimer_Tick(object sender, EventArgs e)
        {
            if (typinggame() && addFlag == 0)
            {
                currentPlayer.OwnPokemon.Add(target);
                addFlag = 1;
                foreach(Pokemon item in currentPlayer.OwnPokemon)
                {
                    System.Diagnostics.Debug.WriteLine(item.Name);
                }
                txtTPGtimer.Visibility = Visibility.Collapsed;
                txtTPGinput.Visibility = Visibility.Collapsed;
                txtDescriptionIntro.TextAlignment = TextAlignment.Center;
                txtDescriptionIntro.Text = "GotCha, ";
                txtDescriptionIntro.Text += "You captured " + target.Name + " !";
                txtTPGreturn.Visibility = Visibility.Visible;
                txtTPGreturn.Text = "[Return]";
            }
            else if (TPGcounter < 5 && !typinggame())
            {
                TPGcounter++;
                txtTPGtimer.Text = (5 - TPGcounter).ToString();
                txtTPGtimer.Text += " seconds left";
            }
            else if (TPGcounter >= 5 && !typinggame())
            {
                cvcatchtarget.Visibility = Visibility.Collapsed;
                txtTPGtimer.Visibility = Visibility.Collapsed;
                txtTPGinput.Visibility = Visibility.Collapsed;
                txtDescriptionIntro.TextAlignment = TextAlignment.Center;
                txtDescriptionIntro.Text = "SoSad, ";
                txtDescriptionIntro.Text += target.Name + " ran away...";
                txtTPGreturn.Visibility = Visibility.Visible;
                txtTPGreturn.Text = "[Return]";
            }
        }

        char pos = ' ';
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(inputText.Length<targetText.Length && TPGcounter<5)
            {
                pos = targetText[inputText.Length];

                if (e.Key.ToString() == pos.ToString() && TPGcounter>=0)
                {
                    txtTPGinput.Text += pos.ToString();
                    inputText += pos.ToString();
                }
            }
            //txtTPGinput.Text = e.Key.ToString() + ", " + pos.ToString();
        }

        private void txtTPGreturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
