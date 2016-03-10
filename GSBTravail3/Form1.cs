using System;
using System.Data;
using System.Windows.Forms;
using mesDates;
using mesBdd;

namespace GSBTravail3
{
    /// <summary>
    /// Gère l'interface graphique de gestion de l'état des fiches de frais
    /// </summary>
    public partial class Form1 : Form
    {
        // Propriétés :
        // ------------
        // configuration accés base de données
        private string serveur = "localhost";
        private string bdd = "gsbv2";
        private string utilisateur = "root";
        private string mdp = "";
        // Objets
        private ConnexionSql connexion = null;
        private GestionDate aujourdHui = null;


        /// <summary>
        /// Constructeur : initialise les composants graphique, la connexion, récupére les fiches frais et vérifie leur état.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            aujourdHui = new GestionDate();

            // Connexion à la base de donnée, récupération des fiches et vérification de leur états.
            try
            {
                getLesDonnees();
                verifierLesFiches();
            }
            catch (Exception ex)
            {
                Program.showError(ex.Message);
                this.displayMessage("Erreur, impossible de se connecter à la base de données.", true);
            }
        }


        /// <summary>
        /// Recupére les fiches de frais du mois précédent et affichage dans le dataGridView
        /// </summary>
        public void getLesDonnees()
        {
            // Récupére mois précédent et année pour key : AAAAMM
            string key = aujourdHui.AnneeCourante + aujourdHui.MoisPrecedent;

            // Tentative de récupérer les données en base
            try
            {
                connexion = ConnexionSql.getInstance(serveur, bdd, utilisateur, mdp);

                // Récupére une DataTable contenant les fiches correspondant à la key
                DataTable dt =  connexion.getFichesMois(key);

                // Lie le tableau à la DataTable
                dataGridView.DataSource = dt;

                connexion.CloseConnection();
            }
            catch (Exception ex)
            {
                Program.showError(ex.Message);
            }
        }
        

        /// <summary>
        /// Vérifie si les fiches de frais du mois précédent sont bien à jour (dans le bonne état)
        /// </summary>
        /// <returns>true ou false selon si les fiches sont dans le bon état</returns>
        public bool verifierLesFiches()
        {
            bool ret = false;
            string key = aujourdHui.AnneeCourante + aujourdHui.MoisPrecedent;

            try
            {
                connexion = ConnexionSql.getInstance(serveur, bdd, utilisateur, mdp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Si on est avant le 20 du mois courant, les fiches du mois passé doivent toutes être en état CL
            if (Convert.ToInt32(aujourdHui.JourCourant) < 20)
            {
                DataTable dt = connexion.getFichesMois(key, "CR"); 

                if (dt.Rows.Count == 0)
                {
                    this.displayMessage("Les fiches de frais sont toutes à jour.");
                    ret = true;
                }
                else
                {
                    this.displayMessage("Il reste des fiches du mois dernier à clôturer.", true);
                    ret = false;
                }
            }
            // Sinon les fiches doivent toutes êtres en état VA
            else
            {
                DataTable dt = connexion.getFichesMois(key, "CL");

                if (dt.Rows.Count == 0)
                {
                    this.displayMessage("Les fiches frais sont toutes à jour.");
                    ret = true;
                }
                else
                {
                    this.displayMessage("Il reste des fiches du mois dernier à valider.", true);
                    ret = false;
                }

            }

            // Dans tous les cas on ferme la connexion et on retourne true ou false
            connexion.CloseConnection();
            return ret;
        }


        /// <summary>
        /// Fonction événementielle : lorsque l'utilisateur clique sur le bouton, on met les fiches de frais à jour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void majButton_Click(object sender, EventArgs e)
        {
            if (!verifierLesFiches())
            {
                string key = aujourdHui.AnneeCourante + aujourdHui.MoisPrecedent;
                try
                {
                    connexion = ConnexionSql.getInstance(serveur, bdd, utilisateur, mdp);
                }
                catch (Exception ex)
                {
                    Program.showError(ex.Message);
                    this.displayMessage("Erreur, impossible de se connecter à la base de données.", true);
                }

                if(Convert.ToInt32(aujourdHui.JourCourant) < 20)
                {
                    connexion.closeFichesMois(key);
                }
                else
                {
                    connexion.fichesMoisToVA(key);
                }

                // Dans tous les cas on actualise l'affichage du gridView et on ferme la connexion
                this.verifierLesFiches();
                this.getLesDonnees();
                connexion.CloseConnection();
            }
        }


        /// <summary>
        /// Fonction utilitaire : affiche un message dans le label de la fenêtre
        /// </summary>
        /// <param name="text"></param>
        /// <param name="error"></param>
        private void displayMessage(string text, bool error = false)
        {
            etatLabel.Text = text;
            if (error)
            {
                etatLabel.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                etatLabel.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}
