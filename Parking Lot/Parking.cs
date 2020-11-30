using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Parking_Lot
{
    public class Parking
    {
        #region Atributi

        List<Korisnik> korisnici;
        List<Lokacija> lokacije;

        #endregion

        #region Properties

        public List<Korisnik> Korisnici
        {
            get => korisnici;
        }

        public List<Lokacija> Lokacije
        {
            get => lokacije;
        }

        #endregion

        #region Konstruktor

        public Parking()
        {
            korisnici = new List<Korisnik>();
            lokacije = new List<Lokacija>();
        }

        #endregion

        #region Metode

        public void RadSaLokacijom(Lokacija l, int opcija, List<string> podaci = null)
        {
            if (opcija == 0)
            {
                if (Lokacije.Find(lokacija => lokacija.Naziv == l.Naziv) != null)
                    throw new InvalidOperationException("Lokacija već postoji!");

                Lokacije.Add(l);
            }
            else if (opcija == 1)
            {
                Lokacija lokacija = Lokacije.Find(loc => loc.Naziv == l.Naziv);
                if (lokacija == null)
                    throw new InvalidOperationException("Lokacija ne postoji!");

                Lokacije.Remove(lokacija);
            }
            else if (opcija == 2)
            {
                Lokacija lokacija = Lokacije.Find(loc => loc.Naziv == l.Naziv);
                if (lokacija == null)
                    throw new InvalidOperationException("Lokacija ne postoji!");

                foreach (string ulica in podaci)
                    if (!lokacija.Ulice.Contains(ulica))
                        lokacija.Ulice.Add(ulica);
            }
        }

        public void DodajKorisnika(Korisnik k, bool clan)
        {
            Korisnik korisnik = Korisnici.Find(kor => kor.Username == k.Username);
            if (korisnik != null && !clan)
                throw new ArgumentException("Korisnik već postoji!");
            else if (korisnik != null)
            {
                Korisnici.Remove(korisnik);
                Korisnici.Add(k);
            }
            else
                Korisnici.Add(k);
        }

        public void RezervišiParking(Clan c, Lokacija l)
        {
            Tuple<int, Lokacija> pm = new Tuple<int, Lokacija>(l.DajTrenutniBrojSlobodnogMjesta(), l);
            c.RezervisanoParkingMjesto = pm;
        }

        public void OslobodiParkingMjesto(ITransakcija transakcija, Clan c)
        {
            if (transakcija.DajVrijemeDolaska(c.Vozilo).AddHours(24) < DateTime.Now)
            {
                c.RezervisanoParkingMjesto.Item2.OslobodiMjesto();
                c.RezervisanoParkingMjesto = null;
            }
            else
                throw new InvalidOperationException("Još uvijek nisu prošla 24 sata!");
        }

        /// <summary>
        /// Metoda u kojoj se vrši pretvaranje korisnika u člana.
        /// Prvo je potrebno pronaći postojećeg korisnika sa poslanim korisničkim imenom.
        /// Ukoliko korisnik ne postoji, baca se izuzetak.
        /// Podacima od pronađenog korisnika dodaje se nasumično generisani password sa
        /// ispravnim formatom prema postojećoj programskoj logici, a članarina mu se
        /// odobrava na narednih godinu dana. Zatim se password vraća kao rezultat metode.
        /// </summary>
        /// <param name="username"></param>
        public string DodavanjeČlana(string username)
        {
            Korisnik korisnik = korisnici.Find(k => k.Username == username);

            if(korisnik != null)
            {
                var rand = new Random();
                string karakteri = "abcdefghijklmnjopqrstuvwxyzABCDEFGHIJKLMNJOPQRSTUVWXYZ0123456789";
                int duzinaPassworda = rand.Next(10, 33);
                StringBuilder password = new StringBuilder(duzinaPassworda);
                for(int i = 0; i < duzinaPassworda; i++)
                {
                    int odaberi = rand.Next(63);
                    password.Append(karakteri.ElementAt(odaberi));
                }
                korisnici.Remove(korisnik);
                Clan c = new Clan(korisnik.Username, password.ToString(), korisnik.Adresa, korisnik.Vozilo, DateTime.Now.AddYears(1));

                korisnici.Add(c);

                return password.ToString();
            }

            throw new ArgumentException("Korisnik ne postoji!");
        }

        public double DajUkupnuMogućuDobitZaKorisnika(Clan c)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
