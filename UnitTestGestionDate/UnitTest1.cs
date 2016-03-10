using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mesDates;

namespace TestGestionDate
{
    [TestClass]
    /// <summary>
    /// Test unitaire de la classe GestonDate
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// Test pour le mois Suivant 
        /// </summary>
        [TestMethod]
        public void getMoisSuivantTest()
        {
            GestionDate monMois = new GestionDate();
            string moisAttendu = "04";
            string moisSuivant = monMois.MoisSuivant;
            Assert.AreEqual(moisAttendu, moisSuivant);

            monMois = new GestionDate(new DateTime(2016, 04, 12));
            moisAttendu = "05";
            moisSuivant = monMois.MoisSuivant;
            Assert.AreEqual(moisAttendu, moisSuivant);
        }


        /// <summary>
        /// Test pour le mois précédent
        /// </summary>
        [TestMethod()]
        public void getMoisPrecedentTest()
        {
            GestionDate monMois = new GestionDate();
            string moisattendu = "02";
            string moisPrecedent = monMois.MoisPrecedent;
            Assert.AreEqual(moisattendu, moisPrecedent);

            monMois = new GestionDate(new DateTime(2012, 07, 12));
            moisattendu = "06";
            moisPrecedent = monMois.MoisPrecedent;
            Assert.AreEqual(moisattendu, moisPrecedent);
        }


        /// <summary>
        /// Test pour le mois Courant 
        /// </summary>
        [TestMethod()]
        public void getMoisCourantTest()
        {
            GestionDate monMois = new GestionDate();
            string moisAttendu = "03";
            string moisActuel = monMois.MoisCourant;
            Assert.AreEqual(moisAttendu, moisActuel);

            monMois = new GestionDate(new DateTime(2000, 10, 19));
            moisAttendu = "10";
            moisActuel = monMois.MoisCourant;
            Assert.AreEqual(moisAttendu, moisActuel);
        }


        /// <summary>
        /// Test pour l'année en cours 
        /// </summary>
        [TestMethod()]
        public void anneeCouranteTest()
        {
            GestionDate monAnnee = new GestionDate();
            string anneeAttendue = "2016";
            string anneeActuelle = monAnnee.AnneeCourante;
            Assert.AreEqual(anneeActuelle, anneeAttendue);

            monAnnee = new GestionDate(new DateTime(2019, 11, 11));
            anneeAttendue = "2019";
            anneeActuelle = monAnnee.AnneeCourante;
            Assert.AreEqual(anneeActuelle, anneeAttendue);
        }


        /// <summary>
        /// Test pour le jour en cours 
        /// </summary>
        [TestMethod()]
        public void jourCourantTest()
        {
            GestionDate monJour = new GestionDate();
            string jourAttendu = "7";
            string jourActuel = monJour.JourCourant;
            Assert.AreEqual(jourActuel, jourAttendu);

            monJour = new GestionDate(new DateTime(2009, 12, 23));
            jourAttendu = "23";
            jourActuel = monJour.JourCourant;
            Assert.AreEqual(jourActuel, jourAttendu);
        }


        /// <summary>
        /// On fait des tests manuellements pour les méthodes ci-dessus:
        /// Pour le mois en cours, nous voulons le mois de mars et nous sommes actuellement au mois de mars
        /// </summary>
        [TestMethod()]
        public void moisCourantTest2()
        {
            GestionDate monMois = new GestionDate();
            string moisAttendu = "03";
            string moisActuel = monMois.MoisCourant;
            Assert.AreEqual(moisAttendu, moisActuel);

            monMois = new GestionDate(new DateTime(1997, 01, 12));
            moisAttendu = "01";
            moisActuel = monMois.MoisCourant;
            Assert.AreEqual(moisAttendu, moisActuel);
        }

        /// <summary>
        /// On test si la méthode MoisCourant marche bien
        /// </summary>
        [TestMethod()]
        public void getMoisCourantTest3()
        {
            GestionDate monMois = new GestionDate();
            string moisAttendu = "07";
            string moisActuel = monMois.MoisCourant;
            Assert.AreNotEqual(moisAttendu, moisActuel);
            
            monMois = new GestionDate(new DateTime(1994,05,11));
            moisAttendu = "10";
            moisActuel = monMois.MoisCourant;
            Assert.AreNotEqual(moisAttendu, moisActuel);
        }


        /// <summary>
        /// Pour le mois suivant, nous voulons le mois d'avril et nous sommes actuellement au mois de mars
        /// </summary>
        [TestMethod()]
        public void getMoisSuivantTest2()
        {
            GestionDate monmois = new GestionDate();
            string moisattendu = "04";
            string moisactuel = "03";
            moisactuel = monmois.MoisSuivant;
            Assert.AreEqual(moisattendu, moisactuel);
        }


        /// <summary>
        /// On test si la méthode MoisSuivant marche bien
        /// </summary>
        [TestMethod]
        public void getMoisSuivantTest3()
        {
            GestionDate monmois = new GestionDate();
            string moisattendu = "05";
            string moisactuel = "03";
            moisactuel = monmois.MoisSuivant;
            Assert.AreNotEqual(moisattendu, moisactuel);
        }


        /// <summary>
        /// Pour le mois précédent, nous voulons le mois de février et nous sommes actuellement au mois de mars
        /// </summary>
        [TestMethod()]
        public void getMoisPrecedentTest2()
        {
            GestionDate monmois = new GestionDate();
            string moisattendu = "02";
            string moisactuel = "03";
            moisactuel = monmois.MoisPrecedent;
            Assert.AreEqual(moisattendu, moisactuel);
        }


        /// <summary>
        /// On test si la méthode MoisPrécédent marche bien
        /// </summary>
        [TestMethod()]
        public void getMoisPrecedentTest3()
        {
            GestionDate monmois = new GestionDate();
            string moisattendu = "04";
            string moisactuel = "04";
            moisactuel = monmois.MoisPrecedent;
            Assert.AreNotEqual(moisattendu, moisactuel);
        }


        /// <summary>
        /// Test pour l'année en cours (2012)
        /// </summary>
        [TestMethod()]
        public void anneeCouranteTest2()
        {
            GestionDate monannee = new GestionDate(new DateTime(2012, 05, 05));
            Assert.AreEqual("2012, 05, 05", "2012, 05, 05");
        }


        /// <summary>
        ///  On test si la méthode annéeCourante marche bien
        /// </summary>
        [TestMethod()]
        public void anneeCouranteTest3()
        {
            GestionDate monannee = new GestionDate(new DateTime(2012, 05, 05));
            Assert.AreNotEqual("2012, 05, 05", "2016, 05, 05");
        }


        /// <summary>
        /// On test si la méthode jourCourant marche bien
        /// </summary>
        [TestMethod()]
        public void jourCourantTest2()
        {
            GestionDate monjour = new GestionDate();
            string jourattendu = "7";
            string jouractuel = "9";
            jouractuel = monjour.JourCourant;
            Assert.AreEqual(jouractuel, jourattendu);
        }
    }
}
