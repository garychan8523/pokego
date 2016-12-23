using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokego
{
        public class Pokemon
        {
            Random rnd = new Random();
            public enum pokesize { xs, s, m, l, xl };               /* pokesize = (int)size.l; */

            private string name;
            private string type;
            private int ap;
            private int cp;
            private int hp;
            private int maxhp;
            private pokesize size;
            private List<string> ListName;
            private List<string> ListType;
            private List<int> ListAp;
            private List<int> ListHp;

            public string Name { get { return name; } }
            public string Type { get { return type; } }
            public int Cp { get { cpCaluate(); return cp; } }
            public int Ap { get { return ap; } }
            public int Hp { get { return hp; } }
            public int Maxhp { get { return maxhp; } }
            public pokesize Size { get { return size; } }

            // constructor
            public Pokemon()
            {
                initialization();

                int numgen = RandGen();
                this.name = ListName[numgen];
                this.type = ListType[numgen];
                this.ap = ListAp[numgen] + rnd.Next(3);
                this.maxhp = ListHp[numgen] + rnd.Next(3);
                this.hp = this.maxhp;
                this.size = (pokesize)rnd.Next(5);
            }

            public Pokemon(int x) //For generating pre-PowerUp pokemon
            {
                if (ListName == null)
                {
                    loadPokeData();
                }
                int numgen = RandGen();
                this.name = ListName[numgen];
                this.type = ListType[numgen];
                this.ap = ListAp[numgen] + rnd.Next(3);
                this.maxhp = ListHp[numgen] + rnd.Next(3);
                this.hp = this.maxhp;
                this.size = (pokesize)rnd.Next(5);

                if (x >= 1)
                {
                    for (int i = 0; i < x; i++)
                    {
                        this.powerUp();
                    }
                }
            }

            private int RandGen()
            {
                return rnd.Next(this.ListName.Count());
            }

            public void initialization()
            {
                if (ListName == null)
                {
                    loadPokeData();
                }
            }
            public override string ToString()
            {
 	            return this.Name + "(CP: " + this.Cp + ")";
            }

            public void powerUp()
            {
                this.ap = (int)(this.ap * 1.1);
                this.hp = (int)(this.hp * 1.2);
                this.maxhp = (int)(this.maxhp * 1.2);
            }

            public void Heal()
            {
                this.hp = this.maxhp;
            }

            public void attackCalculat(Pokemon x)
            {
                int extra = rnd.Next((int)this.ap / 10);
                x.hp = x.hp - this.ap - extra;
                if (x.hp < 0) x.hp = 0;
            }

            private int cpCaluate()
            {
                cp = this.ap + this.hp;
                return cp;
            }

            public bool isDead()
            {
                if (this.hp <= 0)
                {

                    return true;
                }
                return false;
            }

            public void loadPokeData()
            {
                // instantiate each list for storing poke data
                ListName = new List<string>();
                ListType = new List<string>();
                ListAp = new List<int>();
                ListHp = new List<int>();

                // load poke name from resources and add to ListName
                string[] path = Properties.Resources.pokename.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string line in path) { ListName.Add(line); }

                // load poke type from resources and add to ListType
                path = Properties.Resources.poketype.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string line in path) { ListType.Add(line); }

                // load poke cp from resources and add to ListCp
                path = Properties.Resources.pokeap.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string line in path) { int temp = Int32.Parse(line);  ListAp.Add(temp); }

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

        }

}
