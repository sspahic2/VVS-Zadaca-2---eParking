using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parking_Lot;
using System;
using System.Collections.Generic;

namespace Unit_Testovi
{
    [TestClass]
    public class NoviTestovi
    {
       

        #region Zamjenski Objekti


        [TestMethod]
        public void TestZamjenskiObjekat()
        {
            Lokacija l = new Lokacija("Kampus", new List<string>() { "Zmaja od Bosne" }, 2.50, 50);
            Clan c = new Clan(DateTime.Now.AddYears(1));
            Parking p = new Parking();
            p.DodajKorisnika(c, true);
            p.RadSaLokacijom(l, 0);

            p.RezervišiParking(c, l);

            ITransakcija transakcija = new Transakcija();

            p.OslobodiParkingMjesto(transakcija, c);

            Assert.AreEqual(p.Lokacije[0].DajTrenutniBrojSlobodnogMjesta(), 1);
        }

        #endregion

        #region TDD

        [TestMethod]
        public void TestDobitAutomobil()
        {
            Lokacija l1 = new Lokacija("Kampus", new List<string>() { "Zmaja od Bosne" }, 2.50, 50);
            Lokacija l2 = new Lokacija("Ilidža", new List<string>() { "Željeznička" }, 1.50, 100);
            Clan c = new Clan(DateTime.Now.AddYears(1));
            c.Vozilo = new Vozilo("Automobil", "111-A-111", 5);
            Parking p = new Parking();
            p.RadSaLokacijom(l1, 0);
            p.RadSaLokacijom(l2, 0);

            double zarada = p.DajUkupnuMogućuDobitZaKorisnika(c);

            Assert.AreEqual(zarada, (2.50 + 1.50) / 5);
        }

        [TestMethod]
        public void TestDobitAutobus()
        {
            Lokacija l1 = new Lokacija("Kampus", new List<string>() { "Zmaja od Bosne" }, 2.50, 50);
            Lokacija l2 = new Lokacija("Ilidža", new List<string>() { "Željeznička", "Sarajevska" }, 1.50, 100);
            Clan c = new Clan(DateTime.Now.AddYears(1));
            c.Vozilo = new Vozilo("Autobus", "111-A-111", 50);
            Parking p = new Parking();
            p.RadSaLokacijom(l1, 0);
            p.RadSaLokacijom(l2, 0);

            double zarada = p.DajUkupnuMogućuDobitZaKorisnika(c);

            Assert.AreEqual(zarada, (2.50 + 1.50 * 2) / 50);
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDobitIzuzetak()
        {
            Clan c = new Clan(DateTime.Now.AddYears(1));
            c.Vozilo = new Vozilo("Kamionet", "111-A-111", 2);
            Parking p = new Parking();

            double zarada = p.DajUkupnuMogućuDobitZaKorisnika(c);
        }


        #endregion


    }


}
