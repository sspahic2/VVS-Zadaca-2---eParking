using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Parking_Lot
{
    public class Vozilo
    {

        #region Atributi

        string vrsta, tablice;
        int brojSjedišta;

        #endregion

        #region Properties

        public string Vrsta
        {
            get => vrsta;
            set
            {
                List<string> vrste = new List<string>() { "Automobil", "Motor", "Autobus", "Kamionet", "Minibus" };
                if (vrste.Find(element => element == value) == null)
                    throw new ArgumentException("Nepostojeća vrsta vozila!");

                vrsta = value;
            }
        }

        public string Tablice
        {
            get => tablice;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length != 9
                    || !value.Substring(0, 3).All(char.IsDigit)
                    || !(value.Substring(3, 1) == "-")
                    || !value.Substring(4, 1).All(char.IsUpper)
                    || !(value.Substring(5, 1) == "-")
                    || !value.Substring(6).All(char.IsDigit))
                    throw new FormatException("Neispravan format tablica!");

                tablice = value;
            }
        }

        public int BrojSjedišta
        {
            get => brojSjedišta;
            set
            {
                if (value < 0)
                    throw new FormatException("Neispravan broj sjedišta!");

                brojSjedišta = value;
            }
        }

        #endregion

        #region Konstruktor

        public Vozilo(string type, string plates, int noSeats)
        {
            Vrsta = type;
            Tablice = plates;
            BrojSjedišta = noSeats;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda u kojoj se provjerava ispravnost podataka za vozilo.
        /// Ovisno o vrsti, dozvoljen je različit broj sjedišta:
        /// Automobil - 4 do 7 sjedišta
        /// Motor - 1 do 2 sjedišta
        /// Autobus - 20 do 100 sjedišta
        /// Kamionet - 2 do 3 sjedišta
        /// Minibus - 5 do 20 sjedišta
        /// Sve ostale kombinacije dovode do pojave izuzetka.
        /// </summary>
        /// <returns></returns>
        public bool ProvjeriIspravnostVozila()
        {
            bool result = false;
            switch (new List<string>() { "Automobil", "Motor", "Autobus", "Kamionet", "Minibus" }.FindIndex(element => element.Equals(vrsta)))
            {
                case 0:
                    result = (brojSjedišta >= 4 && brojSjedišta <= 7);
                    break;
                case 1:
                    result =  brojSjedišta >= 1 && brojSjedišta <= 2;
                    break;
                case 2:
                    result =  brojSjedišta >= 20 && brojSjedišta <= 100;
                    break;
                case 3:
                    result =  brojSjedišta >= 2 && brojSjedišta <= 3;
                    break;
                case 4:
                    result = brojSjedišta >= 5 && brojSjedišta <= 20;
                    break;
                default:
                    result = false;
                    break;
            }
            if(result)
            {
                return result;
            }
            else
            {
                throw new FormatException("Neispravan broj sjedišta!");
            }
        }

        #endregion
    }
}
