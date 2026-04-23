using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Cech_dobrodruhů;

namespace Testujeme
{
    [TestClass]
    public class UnitTest1
    {
        Form1 form = new Form1();

        // Počáteční hodnoty člena
        [TestMethod]
        public void InitValuesOk()
        {
            ClenCechu clen = new ClenCechu();
            Assert.AreEqual(50, clen.energie, "Počáteční energie by měla být 50.");
            Assert.AreEqual(100, clen.health, "Počáteční zdraví by mělo být 100.");
            Assert.AreEqual(20, clen.dmg, "Počáteční poškození (dmg) by mělo být 20.");
            Assert.IsTrue(clen.JeAktivni, "Člen cechu by měl být na začátku aktivní.");
        }

        // Trénink s kladnou hodnotou
        [TestMethod]
        public void Trenuj_ZvysiEnergii()
        {
            ClenCechu clen = new ClenCechu();
            clen.energie = 50;
            clen.Trenuj(20);
            Assert.AreEqual(70, clen.energie);
        }

        // Trénink nepřesáhne 100
        [TestMethod]
        public void Trenuj_MaxLimit()
        {
            ClenCechu clen = new ClenCechu();
            clen.energie = 80;
            clen.Trenuj(500);
            Assert.AreEqual(100, clen.energie, "Energie nesmí přesáhnout 100.");
        }

        // Trénink s nulou
        [TestMethod]
        public void Trenuj_Nula_Nic()
        {
            ClenCechu clen = new ClenCechu();
            clen.energie = 50;
            clen.Trenuj(0);
            Assert.AreEqual(50, clen.energie);
        }

        // Trénink se zápornou hodnotou
        [TestMethod]
        public void Trenuj_Zaporne_Nic()
        {
            ClenCechu clen = new ClenCechu();
            clen.energie = 50;
            clen.Trenuj(-10);
            Assert.AreEqual(50, clen.energie, "Záporná hodnota tréninku by měla být ignorována.");
        }

        // Zranění sníží HP
        [TestMethod]
        public void Zraneni_SniziZdravi()
        {
            ClenCechu clen = new ClenCechu();
            clen.health = 100;
            clen.UtrzZraneni(30, 0);
            Assert.AreEqual(70, clen.health);
        }

        // HP neklesne pod 0
        [TestMethod]
        public void Zraneni_MinLimit()
        {
            ClenCechu clen = new ClenCechu();
            clen.health = 50;
            clen.UtrzZraneni(150, 0);
            Assert.AreEqual(0, clen.health, "Zdraví nesmí být záporné.");
        }

        // Záporné zranění je ignorováno
        [TestMethod]
        public void Zraneni_Zaporne_Nic()
        {
            ClenCechu clen = new ClenCechu();
            clen.health = 100;
            clen.UtrzZraneni(-20, 0);
            Assert.AreEqual(100, clen.health, "Záporné zranění se musí ignorovat.");
        }

        // Nulové zranění je ignorováno
        [TestMethod]
        public void Zraneni_Nula_Nic()
        {
            ClenCechu clen = new ClenCechu();
            clen.health = 100;
            clen.UtrzZraneni(0, 0);
            Assert.AreEqual(100, clen.health);
        }

        // Smrt deaktivuje člena
        [TestMethod]
        public void Zraneni_Smrt_Neaktivni()
        {
            ClenCechu clen = new ClenCechu();
            clen.health = 50;
            clen.UtrzZraneni(50, 0);
            Assert.IsFalse(clen.JeAktivni, "Při dosažení 0 zdraví musí JeAktivni přejít na false.");
        }

        // Odpočinek přidá HP a energii
        [TestMethod]
        public void Odpocivej_ZvysiStats()
        {
            ClenCechu clen = new ClenCechu();
            clen.health = 50;
            clen.energie = 50;
            clen.Odpocivej();
            Assert.AreEqual(60, clen.health);
            Assert.AreEqual(55, clen.energie);
        }

        // Odpočinek doplní HP maximálně do 100
        [TestMethod]
        public void Odpocivej_HpMaxLimit()
        {
            ClenCechu clen = new ClenCechu();
            clen.health = 95;
            clen.Odpocivej();
            Assert.AreEqual(100, clen.health, "Zdraví nesmí po odpočinku přesáhnout 100.");
        }

        // Odpočinek doplní energii maximálně do 100
        [TestMethod]
        public void Odpocivej_EngMaxLimit()
        {
            ClenCechu clen = new ClenCechu();
            clen.energie = 98;
            clen.Odpocivej();
            Assert.AreEqual(100, clen.energie, "Energie nesmí po odpočinku přesáhnout 100.");
        }

        // Prodej u obchodníka sníží počet předmětů
        [TestMethod]
        public void Obchodnik_Prodej()
        {
            Obchodnik obchodnik = new Obchodnik("Karel", Form1.TypObchodu.Jidlo);
            obchodnik.PocetPredmetu = 10;
            obchodnik.Prodej(3);
            Assert.AreEqual(7, obchodnik.PocetPredmetu);
        }

        // Počet předmětů obchodníka neklesne pod 0
        [TestMethod]
        public void Obchodnik_Prodej_MinLimit()
        {
            Obchodnik obchodnik = new Obchodnik("Karel", Form1.TypObchodu.Jidlo);
            obchodnik.PocetPredmetu = 5;
            obchodnik.Prodej(10);
            Assert.AreEqual(0, obchodnik.PocetPredmetu, "Počet předmětů nesmí být záporný.");
        }

        // Doplnění zboží zvýší počet předmětů
        [TestMethod]
        public void Obchodnik_DoplnZbozi()
        {
            Obchodnik obchodnik = new Obchodnik("Karel", Form1.TypObchodu.Jidlo);
            obchodnik.PocetPredmetu = 10;
            obchodnik.DoplnZbozi(5);
            Assert.AreEqual(15, obchodnik.PocetPredmetu);
        }

        // Odpočinek obchodníka přidá jen HP
        [TestMethod]
        public void Obchodnik_Odpocivej()
        {
            Obchodnik obchodnik = new Obchodnik("Karel", Form1.TypObchodu.Jidlo);
            obchodnik.health = 50;
            obchodnik.energie = 50;
            obchodnik.Odpocivej();
            Assert.AreEqual(55, obchodnik.health, "Obchodník si odpočinkem zvyšuje zdraví jen o 5.");
            Assert.AreEqual(50, obchodnik.energie, "Energie se obchodníkovi po odpočinku nesmí měnit.");
        }

        // Dobrodruh správně získá XP
        [TestMethod]
        public void Dobrodruh_ZiskXp()
        {
            Dobrodruh hrdina = new Dobrodruh("Arthur", "Válečník", Form1.TypZbrane.Mec, Form1.TypBrneni.Latove);
            hrdina.Zkusenosti = 0;
            hrdina.PridejZkusenosti(50);
            Assert.AreEqual(50, hrdina.Zkusenosti);
        }

        // Úspěšné použití schopnosti strhne energii
        [TestMethod]
        public void Dobrodruh_Schopnost_Ok()
        {
            Dobrodruh hrdina = new Dobrodruh("Arthur", "Válečník", Form1.TypZbrane.Mec, Form1.TypBrneni.Latove);
            hrdina.energie = 20;
            bool uspesne = hrdina.PouzijSchopnost();
            Assert.IsTrue(uspesne);
            Assert.AreEqual(10, hrdina.energie, "Použití schopnosti by mělo sebrat 10 energie.");
        }

        // Nedostatek energie zamezí použití schopnosti
        [TestMethod]
        public void Dobrodruh_Schopnost_Fail()
        {
            Dobrodruh hrdina = new Dobrodruh("Arthur", "Válečník", Form1.TypZbrane.Mec, Form1.TypBrneni.Latove);
            hrdina.energie = 5;
            bool uspesne = hrdina.PouzijSchopnost();
            Assert.IsFalse(uspesne, "Při energii pod 10 nesmí jít schopnost použít.");
            Assert.AreEqual(5, hrdina.energie, "Energie by se neměla odečíst, pokud použití selhalo.");
        }
    }
}
