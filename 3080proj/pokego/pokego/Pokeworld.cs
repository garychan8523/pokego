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
            public List<object> currentItem = new List<object>();
            public List<Rectangle> currentItemImage = new List<Rectangle>();

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
            
            public Pokemon addPokemon()
            {
                Pokemon newbornpokemon = new Pokemon();
                itemcounter++;
                currentItem.Add((object)newbornpokemon);
                return newbornpokemon;
            }

            public void stepIncrement()
            {
                stepcounter++;
            }

        }
}
