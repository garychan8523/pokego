using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokego
{
    public class PokeTrainer
    {
        public List<Pokemon> OwnPokemon { get { return POwnPokemon; } }
        private List<Pokemon> POwnPokemon = new List<Pokemon>();
        private int pokecandy;
        public int Pokecandy { get { return pokecandy; } }
        private int gymbadge;
        public int GYMbadge { get { return gymbadge; } }

        public PokeTrainer()
        {
            this.pokecandy = 10;
            this.gymbadge = 0;
            //Pokemon newpokemon = new Pokemon(9);
            //POwnPokemon.Add(newpokemon);
        }

        public void powerUpPokemon(int x)
        {
            this.POwnPokemon[x].powerUp();
            this.pokecandy--;
        }

        public void sellPokemon(int x)
        {
            this.POwnPokemon.RemoveAt(x);
            this.pokecandy += 3;
        }

        public void healPokemon(int x)
        {
            this.POwnPokemon[x].Heal();
            this.pokecandy -= 3;
        }

        public void getOneCandy()
        {
            pokecandy++;
        }

        public void useOneCandy()
        {
            pokecandy++;
        }

        public void winGYM()
        {
            this.pokecandy += 10;
            this.gymbadge++;

        }

        public bool havePokemon()
        {
            if (this.POwnPokemon.Count > 0)
                return true;
            else return false;
        }

        public bool allPokemonAlive()
        {
            bool status = false;
            foreach (Pokemon x in this.OwnPokemon)
            {
                if (x.Hp > 0) status = true;
            }
            return status;
        }

        public int countPokemon()
        {
            return this.POwnPokemon.Count;
        }
    }
}
