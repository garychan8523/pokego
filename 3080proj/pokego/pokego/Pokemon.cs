using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokego
{
        public class Pokemon
        {
            public enum pokesize { xs, s, m, l, xl };               /* pokesize = (int)size.l; */

            private string name;
            private string type;
            private int cp;
            private int hp;
            private pokesize size;
            private List<string> ListName;
            private List<string> ListType;
            private List<int> ListCp;
            private List<int> ListHp;

            public string Name { get { return name; } }
            public string Type { get { return type; } }
            public int Cp { get { return cp; } }
            public int Hp { get { return hp; } }

            // constructor
            public Pokemon()
            {
                initialization(); 
                genPokemon();
            }

            public void initialization()
            {
                if (ListName == null)
                {
                    loadPokeData();
                }
            }

            public void loadPokeData()
            {
                // instantiate each list for storing poke data
                ListName = new List<string>();
                ListType = new List<string>();
                ListCp = new List<int>();
                ListHp = new List<int>();

                // load poke name from resources and add to ListName
                string[] path = Properties.Resources.pokename.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string line in path) { ListName.Add(line); }

                // load poke type from resources and add to ListType
                path = Properties.Resources.poketype.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string line in path) { ListType.Add(line); }

                // load poke cp from resources and add to ListCp
                path = Properties.Resources.pokecp.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string line in path) { int temp = Int32.Parse(line);  ListCp.Add(temp); }

                // load poke hp from resources and add to ListHp
                path = Properties.Resources.pokehp.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string line in path) { int temp = Int32.Parse(line); ListHp.Add(temp); }
            }

            private pokesize genSize()
            {
                Random rnd = new Random();
                int numgen = rnd.Next(5);
                return (pokesize)numgen;
            }

            public void genPokemon()
            {
                Random rnd = new Random();
                int numgen = rnd.Next(6);

                this.name = ListName[numgen];
                this.type = ListType[numgen];
                this.cp = ListCp[numgen];
                this.hp = ListHp[numgen];
                this.size = genSize();

                System.Diagnostics.Debug.WriteLine("New pokemon generated,");
                System.Diagnostics.Debug.WriteLine("Name: " + this.Name);
                System.Diagnostics.Debug.WriteLine("Type: " + this.Type);
                System.Diagnostics.Debug.WriteLine("CP: " + this.Cp);
                System.Diagnostics.Debug.WriteLine("HP: " + this.Hp);
            }
        }

}
