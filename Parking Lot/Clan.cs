using System;
using System.Linq;

namespace Parking_Lot
{
    public enum Status
    {
        Aktivna,
        Neaktivna
    }

    public class Clan : Korisnik
    {
        #region Atributi

        string password;
        Status status;
        DateTime aktivnaDo;
        Tuple<int, Lokacija> rezervisanoParkingMjesto;

        #endregion

        #region Properties

        public string Password
        {
            get => password;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length < 10
                    || !value.All(char.IsLetterOrDigit))
                    throw new FormatException("Neispravan format passworda!");

                password = value;
            }
        }

        public Status Status
        {
            get => status;
        }

        public DateTime AktivnaDo
        {
            get => aktivnaDo;
        }

        public Tuple<int, Lokacija> RezervisanoParkingMjesto
        {
            get => rezervisanoParkingMjesto;
            set => rezervisanoParkingMjesto = value;
        }

        #endregion

        #region Konstruktor

        public Clan(string user, string pass, string address, Vozilo vehicle, DateTime endDate)
            : base(user, address, vehicle)
        {
            Password = pass;
            aktivnaDo = endDate;
            status = Status.Aktivna;
        }

        public Clan(DateTime endDate)
        {
            aktivnaDo = endDate;
            status = Status.Aktivna;
        }

        #endregion

        #region Metode

        public void ProvjeriJeLiČlanarinaIstekla()
        {
            if (DateTime.Now > aktivnaDo)
                status = Status.Neaktivna;
        }

        /// <summary>
        /// Metoda u kojoj se vrši produživanje važenja članarine.
        /// Ukoliko članarina još uvijek nije istekla, potrebno je baciti izuzetak,
        /// kao i u slučaju da je članarina istekla prije više od godinu dana.
        /// U suprotnom, odobrava se produženje članarine.
        /// </summary>
        public void ProdužiČlanarinu(DateTime noviRok)
        {
            int razlikaGodina = DateTime.Now.Year - aktivnaDo.Year;
            if (razlikaGodina > 1 || (DateTime.Now.Month - aktivnaDo.Month<=0 && razlikaGodina == 1))
            {
                throw new Exception("Clanarina je istekla prije više od godinu dana");
            } 
            this.ProvjeriJeLiČlanarinaIstekla();
            if(status == Status.Aktivna)
            {
                throw new Exception("Clanarina je jos uvijek aktivna");
            }
            aktivnaDo = noviRok;

        }

        #endregion
    }
}
