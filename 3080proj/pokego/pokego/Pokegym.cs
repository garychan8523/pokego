using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokego
{
    public class PokeGym
    {
        public int location{get; private set;}
        private Pokemon champ = new Pokemon(10);
        public Pokemon Champ { get { return champ; } }

        public PokeGym(int location)
        {
            this.location = location;
        }

        public void regenGymChamp()
        {
            champ = new Pokemon(10);
        }

        public void battle(Pokemon trainerPokemon, Pokemon gymPokemon, int turn)
        {
            if(turn==1)
            {
                trainerPokemon.attackCalculat(gymPokemon);
            }
            else if(turn==2)
            {
                gymPokemon.attackCalculat(trainerPokemon);
            }
        }

    }
}
