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
    /// Interaction logic for gymview.xaml
    /// </summary>
    public partial class gymview : Window
    {
        private PokeTrainer currentTrainer;
        private PokeGym currentGym;
        private Canvas cvspawnarea;
        private Pokeworld currentWorld;
        private Rectangle targetImage;

        public gymview(PokeTrainer currentTrainer, PokeGym currentGym, Pokeworld currentWorld, Canvas cvspawnarea, Rectangle targetImage)
        {
            InitializeComponent();
            this.currentTrainer = currentTrainer;
            this.currentGym = currentGym;
            this.cvspawnarea = cvspawnarea;
            this.currentWorld = currentWorld;
            this.targetImage = targetImage;
            this.Closing += new System.ComponentModel.CancelEventHandler(gymview_Closing);

            Initialize();

            BattleTurn(currentGym.Champ);
            lbPokemon.SelectionChanged += new SelectionChangedEventHandler(OnSelect);
        }

        void gymview_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            currentGym.regenGymChamp();
        }

        private void Initialize()
        {
            reloadPokemonData();
            updateCenterInfo();
            hideOptionControl();
        }

        private void reloadPokemonData()
        {
            lbPokemon.Items.Clear();
            foreach (Pokemon item in currentTrainer.OwnPokemon)
            {
                lbPokemon.Items.Add(item);
            }
        }

        public void OnSelect(Object sender, RoutedEventArgs e)
        {
            Pokemon target = (Pokemon)lbPokemon.SelectedItem;
            if (target != null)
            {
                txtOptionInfo.Text = target.Name + "\n\n\n";
                txtOptionInfo.Text += "CP  : " + target.Cp.ToString() + "\n";
                txtOptionInfo.Text += "HP  : " + target.Hp.ToString() + "/" + target.Maxhp.ToString() + "\n";
                txtOptionInfo.Text += "SIZE: " + target.Size.ToString().ToUpper() + "\n";
                txtOptionInfo.Text += "TYPE: " + target.Type + "\n";
                if(!target.isDead())
                {
                    txtOptionComfirm.Visibility = Visibility.Visible;
                }
            }
            else
            {
                txtOptionInfo.Text = "";
            }

        }

        private void BattleTurn(Pokemon target)
        {
            renderGymPokemon();
        }

        private void renderGymPokemon()
        {
            Rectangle gymchampImage = new Rectangle();
            gymchampImage.Width = 64;
            gymchampImage.Height = 64;
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/" + currentGym.Champ.Name + ".png"));
            gymchampImage.Fill = ib;
            Canvas.SetZIndex(gymchampImage, (int)150);
            cvchamp.Children.Add(gymchampImage);
        }

        private void renderTrainerPokemon()
        {
            Rectangle trainerchampImage = new Rectangle();
            trainerchampImage.Width = 64;
            trainerchampImage.Height = 64;
            ImageBrush ib2 = new ImageBrush();
            ib2.ImageSource = new BitmapImage(new Uri("pack://application:,,/Resources/" + trainerPokemon.Name + "bk.png"));
            trainerchampImage.Fill = ib2;
            Canvas.SetZIndex(trainerchampImage, (int)150);
            cvtrainerchamp.Children.Add(trainerchampImage);
        }

        private void updateCenterInfo()
        {
            updateTurnInfo();

            txtOptionInfo.Text = "Welcome to PokeGym !\n\n";

            if (currentTrainer.OwnPokemon.Count <= 0)
            {
                // just for safety.
                txtOptionInfo.Text += "Oh... seems you dont't \n";
                txtOptionInfo.Text += "have any pokemon.";
            }
            else
            {
                txtOptionInfo.Text += "Start a fight to \n";
                txtOptionInfo.Text += "win badges and candy.\n\n";
                txtOptionInfo.Text += "Run or fight until die.";
            }
        }

        private void updateTurnInfo()
        {
            txtTurnInfo.Text = currentGym.Champ.Name + "(CP: " + currentGym.Champ.Cp.ToString() + ") spotted.";
        }

        private void hideOptionControl()
        {
            txtOptionFight.Visibility = Visibility.Collapsed;
        }

        private void txtDecisionRun_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void txtDecisionFight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtDecisionFight.Visibility = Visibility.Collapsed;
            txtDecisionRun.Visibility = Visibility.Collapsed;
            lbPokemon.Visibility = Visibility.Visible;
            txtOptionInfo.Text = "Select a pokemon to fight.";
        }

        Pokemon trainerPokemon;
        private void txtOptionComfirm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            trainerPokemon = (Pokemon)lbPokemon.SelectedItem;
            if (trainerPokemon.isDead() || trainerPokemon == null) { }
            else
            {
                renderTrainerPokemon();
                updateBattleInfo();
                InitiateBattleTimer();
                txtOptionFight.Visibility = Visibility.Visible;
            }
        }

        int oldTrainerHP;
        int oldGymHP;
        private void updateBattleInfo()
        {
            if (trainerPokemon != null && currentGym.Champ != null)
            {
                lbPokemon.Visibility = Visibility.Collapsed;
                txtOptionInfo.Visibility = Visibility.Collapsed;
                txtOptionComfirm.Visibility = Visibility.Collapsed;
                oldTrainerHP = trainerPokemon.Hp;
                oldGymHP = currentGym.Champ.Hp;
                txtTrainerChamp.Text = "YOU: " + trainerPokemon.Name + "\n\n\n";
                txtTrainerChamp.Text += "CP  : " + trainerPokemon.Cp.ToString() + "\n";
                txtTrainerChamp.Text += "HP  : " + trainerPokemon.Hp.ToString() + "/" + trainerPokemon.Maxhp.ToString() + "\n";
                txtTrainerChamp.Text += "SIZE: " + trainerPokemon.Size.ToString().ToUpper() + "\n";
                txtTrainerChamp.Text += "TYPE: " + trainerPokemon.Type + "\n";
                txtTrainerChamp.Visibility = Visibility.Visible;
                txtGymChamp.Text = "";
                txtGymChamp.Text = "GYM CHAMP: " + currentGym.Champ.Name + "\n\n\n";
                txtGymChamp.Text += "CP  : " + currentGym.Champ.Cp.ToString() + "\n";
                txtGymChamp.Text += "HP  : " + currentGym.Champ.Hp.ToString() + "/" + currentGym.Champ.Maxhp.ToString() + "\n";
                txtGymChamp.Text += "SIZE: " + currentGym.Champ.Size.ToString().ToUpper() + "\n";
                txtGymChamp.Text += "TYPE: " + currentGym.Champ.Type + "\n";
                txtGymChamp.Visibility = Visibility.Visible;

                if (!trainerPokemon.isDead() && !currentGym.Champ.isDead())
                {
                    if (turnsCounter >= 1)
                    {
                        if (oldTrainerHP - trainerPokemon.Hp > 0)
                        {
                            txtTurnInfo.Text = currentGym.Champ.Name + " attacked " + trainerPokemon.Name + " (" + (trainerPokemon.Hp - oldTrainerHP).ToString() + ")";
                        }
                        else if (oldGymHP - currentGym.Champ.Hp > 0)
                        {
                            txtTurnInfo.Text = trainerPokemon.Name + " attacked " + currentGym.Champ.Name + " (" + (currentGym.Champ.Hp - oldGymHP).ToString() + ")";
                        }
                        else if ((oldTrainerHP - trainerPokemon.Hp) == 0 && (oldGymHP - currentGym.Champ.Hp) == 0)
                        {
                            txtTurnInfo.Text = "It's your turn, click fight to attack.";
                        }
                    }
                }
                else
                {
                    txtOptionFight.Visibility = Visibility.Collapsed;
                    txtReturn.Visibility = Visibility.Visible;
                    if (trainerPokemon.isDead())
                    {
                        txtTurnInfo.Text = "You pokemon is dead, you lose.";
                        txtTrainerChamp.Opacity = 0.4;
                        cvtrainerchamp.Opacity = 0.4;
                    }
                    else if (currentGym.Champ.isDead())
                    {
                        txtTurnInfo.Text = "Gym's pokemon has dead, you win.";
                        currentTrainer.winGYM();
                        txtGymChamp.Opacity = 0.4;
                        cvchamp.Opacity = 0.4;
                    }
                }
            }
            else
            {
                txtTrainerChamp.Text = "";
                txtGymChamp.Text = "";
            }
        }

        private void InitiateBattleTimer()
        {
            System.Windows.Threading.DispatcherTimer battleTimer = new System.Windows.Threading.DispatcherTimer();
            battleTimer.Tick += battleTimer_Tick;
            battleTimer.Interval = TimeSpan.FromSeconds(0.5);
            battleTimer.Start();
        }

        int playermove;
        int turnsCounter = 0;
        private void battleTimer_Tick(object sender, EventArgs e)
        {
            turnsCounter++;
            if (turnsCounter % 2 == 1)
            {
                currentGym.battle(trainerPokemon, currentGym.Champ, 2);
            }
            else if(playermove==0)
            {
                turnsCounter++;
            }
            else if(playermove==1)
            {
                playermove = 0;
                currentGym.battle(trainerPokemon, currentGym.Champ, 1);
            }

            updateBattleInfo();
        }

        private void txtOptionFight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            playermove = 1;
        }

        private void txtReturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
