using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace pokego
{
        public class Pokeworld
        {
            Random rnd = new Random();
            public int stepcounter = 0;
            public int itemcounter = 0;
            public int gymcounter = 0;
            public List<object> currentItem = new List<object>();
            public List<PokeGym> currentGym = new List<PokeGym>();
            public List<Rectangle> currentItemImage = new List<Rectangle>();

            //getter setter to-be fixed
            //public int stepcounter { get; private set; }
            //public int itemcounter { get; private set; }
            //public List<object> currentItem { get; private set; }
            //public List<Rectangle> currentItemImage { get; private set; }

            // constructor
            public Pokeworld()
            {
                initialize();
            }

            private void initialize()
            {
            }

            // spawn a single new gym.
            public PokeGym spawnPokegym(int location)
            {
                PokeGym newgym = new PokeGym(location);
                gymcounter++;
                currentGym.Add(newgym);
                return newgym;
            }

            // create a new pokemon.
            public Pokemon spawnPokemon()
            {
                 // control total no. of item
                if(itemcounter<5)
                {
                    Pokemon newpokemon = new Pokemon();
                    itemcounter++;
                    currentItem.Add((object)newpokemon);
                    return newpokemon;
                }
                else
                {
                    return null;
                }
                
            }

            public void removeItem(int index)
            {
                currentItemImage.RemoveAt(index);
                currentItem.RemoveAt(index);
                itemcounter--;
            }

            public void removeItem(Pokemon pokemon)
            {
                int index = currentItem.IndexOf(pokemon);
                currentItemImage.RemoveAt(index);
                currentItem.RemoveAt(index);
                itemcounter--;
            }

            public void stepIncrement()
            {
                stepcounter++;
            }

        }
}
