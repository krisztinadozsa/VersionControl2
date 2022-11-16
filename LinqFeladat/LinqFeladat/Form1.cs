using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqFeladat
{
    public partial class Form1 : Form
    {
        private List<Country> countries = new List<Country>();

        private List<Ramen> ramens = new List<Ramen>();
        public Form1()
        {
            InitializeComponent();
            LoadData("ramen.csv");
        }

        private void LoadData(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(',');
                    var countryName = line[2];
                    var country = AddCountry(countryName);

                    var ramen = new Ramen()
                    {
                        ID = ramens.Count + 1,
                        Brand = line[0],
                        Name = line[1],
                        CountryFK = country.ID,
                        Country = country,
                        Stars = Convert.ToDouble(line[3])
                    };

                    ramens.Add(ramen);




                }
            }
        }

        private Country AddCountry(string countryName)
        {
            var currentCountry = (from x in countries
                                  where x.Name.Equals(countryName)
                                  select x).FirstOrDefault();

            if (currentCountry == null)
            {
                currentCountry = new Country()
                {
                    ID = countries.Count + 1,
                    Name = countryName

                };

                countries.Add(currentCountry);
            }

            return currentCountry;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
