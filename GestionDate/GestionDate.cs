using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mesDates
{
    /// <summary>
    /// Outil de gestion des dates, permet entre autre de récupére les mois courant, précédent et suivant ainsi que l'année courante.
    /// On créé l'objet en choisissant un DateTime comme référence pour le jour actuel puis on peut accéder aux différents informations via les Properties.
    /// </summary>
    public class GestionDate
    {
        // Propriétés :
        // ------------
        string moisCourant;
        string moisPrecedent;
        string moisSuivant;
        string anneeCourante;
        string jourCourant;
        DateTime now ;
        /// <summary>
        /// Contient le mois en cours à partir de la date donnée au constructeur de l'objet
        /// Format : MM
        /// </summary>
        public string MoisCourant { get { return moisCourant; } }
        /// <summary>
        /// Contient le mois précédent à partir du mois en cours
        /// Format : MM
        /// </summary>
        public string MoisPrecedent { get { return moisPrecedent; } }
        /// <summary>
        /// Contient le mois suivant à partir du mois en cours
        /// Format : MM
        /// </summary>
        public string MoisSuivant { get { return moisSuivant; } }
        /// <summary>
        /// Contient l'année en cours à partir de la date donnée au constructeur de l'objet
        /// Format : AAAA
        /// </summary>
        public string AnneeCourante { get { return anneeCourante; } }
        /// <summary>
        /// Contient le jour en cours à partir de la date donnée au constructeur de l'objet
        /// Format : JJ
        /// </summary>
        public string JourCourant { get { return jourCourant; } }


        // Constructeurs :
        // ---------------
        /// <summary>
        /// Surcharge, permet d'initier la gestion des dates à partir du moment actuel si on ne précise rien
        /// </summary>
        public GestionDate() : this(DateTime.Now) { }
        /// <summary>
        /// Constructeur, initie les variables à partir du DateTime en argument considéré comme moment présent
        /// </summary>
        /// <param name="n"></param>
        public GestionDate(DateTime n)
        {
            now = n;
            getMoisCourant();
            getMoisPrecedent();
            getMoisSuivant();
            getAnneeCourante();
            getJourCourant();
        }


        /// <summary>
        /// Récupére le mois en cours
        /// </summary>
        private void getMoisCourant()
        {
            moisCourant = now.Month.ToString();
            // Par défaut le mois est donné sur un chiffre (pour les mois de 1 à 9)
            // On rajoute donc le 0 initial si on voit que la longueur du mois est de 1
            if (MoisCourant.Length == 1)
            {
                moisCourant = "0" + MoisCourant;
            }
        }


        /// <summary>
        /// Récupére l'année actuelle
        /// </summary>
        private void getAnneeCourante()
        {
            anneeCourante = now.Year.ToString();
        }


        /// <summary>
        /// Récupére le mois précédent le mois actuel
        /// </summary>
        private void getMoisPrecedent()
        {
            // Cas particulier du mois de janvier
            if (MoisCourant == "01")
            {
                moisPrecedent = "12";
            }
            // Sinon on fait juste getMoisCourant() - 1
            else
            {
                int mois = Convert.ToInt32(MoisCourant);
                mois--;
                moisPrecedent = Convert.ToString(mois);

                // Par défaut le mois est donné sur un chiffre (pour les mois de 1 à 9)
                // On rajoute donc le 0 initial si on voit que la longueur du mois est de 1
                if (MoisPrecedent.Length == 1)
                {
                    moisPrecedent = "0" + MoisPrecedent;
                }
            }
        }


        /// <summary>
        /// Récupére le mois suivant le mois actuel
        /// </summary>
        private void getMoisSuivant()
        {
            // Cas particulier du mois de décembre
            if (MoisCourant == "12")
            {
                moisSuivant = "01";
            }
            // Sinon on fait juste getMoisCourant() + 1
            else
            {
                int mois = Convert.ToInt32(MoisCourant);
                mois++;
                moisSuivant = Convert.ToString(mois);

                // Normalisation de l'affichage du mois sur 2 chiffres
                if (MoisSuivant.Length == 1)
                {
                    moisSuivant = "0" + MoisSuivant;
                }
            }
        }


        /// <summary>
        /// Recupére le jour actuel du mois actuel
        /// </summary>
        private void getJourCourant()
        {
            this.jourCourant = now.Day.ToString();
        }
    }
}
