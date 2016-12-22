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

            updateTrainerInfo();
        }

        public void updateTrainerInfo()
        {
            txtTrainerInfo.Text = "You have " + currentTrainer.Pokecandy + " candy ";
            txtTrainerInfo.Text += "and " + currentTrainer.countPokemon().ToString() + " pokemon.";
        }

        private void cvclose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
