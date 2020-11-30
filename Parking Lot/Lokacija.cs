using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Parking_Lot
{
    public class Lokacija
    {
        #region Atributi

        string naziv;
        List<string> ulice;
        double cijena;
        int kapacitet, brojač;

        #endregion
      
        #region Properties

        public string Naziv
        {
            get => naziv;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || !value.All(char.IsLetter))
                    throw new ArgumentException("Neispravan format naziva!");

                naziv = value;
            }
        }

        public List<string> Ulice
        {
            get => ulice;
        }

        public double Cijena
        {
            get => cijena;
            set
            {
                if (value < 0.0 || value > 50.00)
                    throw new ArgumentException("Neispravan format cijene!");

                cijena = value;
            }
        }

        public int Kapacitet
        {
            get => kapacitet;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Neispravan format kapaciteta!");

                kapacitet = value;
            }
        }

        #endregion

        #region Konstruktor

        public Lokacija(string name, List<string> streets, double price, int capacity)
        {
            Naziv = name;
            if (streets == null)
                throw new NullReferenceException("Morate specificirati barem jednu ulicu!");

            ulice = streets;
            Cijena = price;
            Kapacitet = capacity;
            brojač = 0;

        }

        #endregion

        #region Metode


       

        public int DajTrenutniBrojSlobodnogMjesta()
        {
            brojač++;
            if (brojač == kapacitet)
                throw new InvalidOperationException("Sva mjesta su zauzeta!");
            return brojač;
        }

        public void OslobodiMjesto()
        {
            brojač--;
        }

        /// <summary>
        /// Metoda u kojoj se vrši izmjena trenutnih ulica od kojih se lokacija sastoji.
        /// Ukoliko je opcija 0, vrši se dodavanje ulica koje su proslijeđene kao parametar
        /// u listu, pri čemu se baca izuzetak ukoliko je neka od ulica već u listi.
        /// Ukoliko je opcija 1, brišu se sve ulice od kojih se lokacija sastoji.
        /// </summary>
        /// <param name="opcija"></param>
        /// <param name="ulice"></param>
        public void RadSaUlicama(int opcija, List<string> ulice = null)
        {
            if(opcija==0)
            {
                int i = 0;   
              while(ulice[i]!=null)
                {
                    if (this.ulice.Contains(ulice[i])) throw new Exception();
                    else this.ulice.Add(ulice[i]);

                    i++;
                }
            }
            else if(opcija==1)
            {
                this.ulice.Clear();
            }
        }

        #endregion
    }
}
