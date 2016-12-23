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
    /// Interaction logic for inventoryview.xaml
    /// </summary>
    public partial class inventoryview : Window
    {
        private PokeTrainer currentTrainer;

        public inventoryview(PokeTrainer currentTrainer)
        {
            InitializeComponent();
            this.currentTrainer = currentTrainer;

            Initialize();

            lbPokemon.SelectionChanged += new SelectionChangedEventHandler(OnSelect);
        }

        private void Initialize()
        {
            reloadPokemonData();
            updateCenterInfo();
            hideOptionControl();
        }

        private void updateCenterInfo()
        {
            updateTrainerInfo();

            txtOptionInfo.Text = "Welcome to PokeCenter !\n\n";

            if (currentTrainer.OwnPokemon.Count <= 0)
            {
                txtOptionInfo.Text += "Oh... seems you dont't \n";
                txtOptionInfo.Text += "have any pokemon.";
            }
            else
            {
                txtOptionInfo.Text += "You can power-up, heal or \n";
                txtOptionInfo.Text += "sell your pokemon here.\n\n";
                txtOptionInfo.Text += "Select a pokemon to start.";
            }
        }

        private void updateTrainerInfo()
        {
            txtTrainerInfo.Text = "You have " + currentTrainer.Pokecandy + " candy ";
            txtTrainerInfo.Text += "and " + currentTrainer.countPokemon().ToString() + " pokemon.";
        }

        private void reloadPokemonData()
        {
            lbPokemon.Items.Clear();
            foreach(Pokemon item in currentTrainer.OwnPokemon)
            {
                lbPokemon.Items.Add(item);
            }
        }

        public void OnSelect(Object sender, RoutedEventArgs e)
        {
            Pokemon target = (Pokemon)lbPokemon.SelectedItem;
            if(target != null)
            {
                txtOptionInfo.Text = target.Name + "\n\n\n";
                txtOptionInfo.Text += "CP  : " + target.Cp.ToString() + "\n";
                txtOptionInfo.Text += "HP  : " + target.Hp.ToString() + "/" + target.Maxhp.ToString() + "\n";
                txtOptionInfo.Text += "SIZE: " + target.Size.ToString().ToUpper() + "\n";
                txtOptionInfo.Text += "TYPE: " + target.Type + "\n";
                showOptionControl();
            }
            else
            {
                txtOptionInfo.Text = "";
            }
            
        }

        private void cvclose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void txtOptionPowerup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTrainer.Pokecandy < 1)
            {}
            else
            {
                int targetIndex = (int)lbPokemon.SelectedIndex;
                currentTrainer.powerUpPokemon((int)lbPokemon.SelectedIndex);
                updateTrainerInfo();
                reloadPokemonData();
                lbPokemon.SelectedIndex = targetIndex;
                showOptionControl();
            }
            
        }

        private void txtOptionHeal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pokemon target = (Pokemon)lbPokemon.SelectedItem;
            if(currentTrainer.Pokecandy>=3 && target.Hp < target.Maxhp)
            {
                int targetIndex = (int)lbPokemon.SelectedIndex;
                currentTrainer.healPokemon((int)lbPokemon.SelectedIndex);
                updateTrainerInfo();
                reloadPokemonData();
                lbPokemon.SelectedIndex = targetIndex;
                showOptionControl();
            }
        }

        private void txtOptionSell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentTrainer.sellPokemon((int)lbPokemon.SelectedIndex);
            lbPokemon.Items.Remove(lbPokemon.SelectedItem);
            updateCenterInfo();
            reloadPokemonData();
            hideOptionControl();
        }

        private void showOptionControl()
        {
            if (currentTrainer.Pokecandy < 1) { txtOptionPowerup.Opacity = 0.3; }
            if (currentTrainer.Pokecandy < 3) { txtOptionHeal.Opacity = 0.3; }
            txtOptionPowerup.Visibility = Visibility.Visible;
            txtOptionHeal.Visibility = Visibility.Visible;
            txtOptionSell.Visibility = Visibility.Visible;
        }

        private void hideOptionControl()
        {
            txtOptionPowerup.Visibility = Visibility.Collapsed;
            txtOptionHeal.Visibility = Visibility.Collapsed;
            txtOptionSell.Visibility = Visibility.Collapsed;
        }
    }
}
