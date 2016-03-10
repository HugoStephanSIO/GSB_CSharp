using System;
using System.ServiceProcess;
using mesBdd;
using mesDates;
using System.Data;

namespace AutoGestionEtatFiche
{
    /// <summary>
    /// Classe service, l'application tourne en arrière plan avec un timer défini pour vérifier toutes les 24 h l'état des fiches de frais et de le mettre à jour si besoin est.
    /// </summary>
    public partial class AutoGestionEtatFiche : ServiceBase
    {
        // config connexion bdd
        private string serveur = "localhost";
        private string bdd = "gsbv2";
        private string utilisateur = "root";
        private string mdp = "";
        // Autres variables
        private ConnexionSql connexion = null;
        private GestionDate aujourdHui = null;
        private System.Timers.Timer myTimer = null;
        private string key = null;


        /// <summary>
        /// Constructeur : initialise un journal d'événements pour les logs, vérifie une première fois l'état des frais, lance le timer
        /// </summary>
        public AutoGestionEtatFiche()
        {
            InitializeComponent();
            GSBLog = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("GSBAutoGestionEtatFiche"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "GSBAutoGestionEtatFiche", "GSBLog");
            }
            GSBLog.Source = "GSBAutoGestionEtatFiche";
            GSBLog.Log = "GSBLog";

            // Vérification de l'état des fiches au lancement 
            verifierLesFiches();

            // Ajout d'un timer qui vérifie chaque minute l'état des fiches 
            this.myTimer = new System.Timers.Timer();
            // 60 seconds ==== DEBUG
            // PROD => rajouter "*60*24"
            myTimer.Interval = 60000; 
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(verifierLesFiches);
            myTimer.Start();
        }


        /// <summary>
        /// Méthode appelée au démarrage de l'application, affiche un message confirmant le démarrage
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            GSBLog.WriteEntry("Démarrage du service de MAJ Auto des fiches de frais...");
        }


        /// <summary>
        /// Méthode appelée à la fermeture de l'application, ferme la connexion à la BDD et affiche un message confirmant l'arrêt
        /// </summary>
        protected override void OnStop()
        {
            connexion.CloseConnection();
            GSBLog.WriteEntry("Fermeture du service de MAJ Auto des fiches de frais...");
        }


        /// <summary>
        /// Vérifie que les fiches de frais sont à jours.
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArgs"></param>
        private void verifierLesFiches(Object myObject = null, EventArgs myEventArgs = null)
        {
            // On génére la clef en récupérant annéé courante et mois précédent
            this.aujourdHui = new GestionDate();
            this.key = this.aujourdHui.AnneeCourante + this.aujourdHui.MoisPrecedent;

            try
            {
                int numJour = Convert.ToInt32(aujourdHui.JourCourant);
                // Si on est avant le 20 du mois courant toutes les fiches du mois passée doivent être en état CL
                if (numJour < 20)
                {
                    GSBLog.WriteEntry("TimeOut : Vérification de l'existence de fiches du mois passé non cloturées...");

                    // S'il y a des fiches du mois dernier encore en état 'CR', on les ferme
                    if (this.getFichesCR())
                    {
                        GSBLog.WriteEntry("=> Il existe bien des fiches du mois passé non cloturées");
                        this.closeFichesCR();
                        GSBLog.WriteEntry("=> Fermeture de toutes les fiches du mois passé réussie");
                    }
                    else
                    {
                        GSBLog.WriteEntry("=> Toutes les fiches du mois passé sont bien closes");
                    }
                }
                // Si on est le 20 ou plus toutes les fiches doivent être en état 'VA' 
                else
                {
                    GSBLog.WriteEntry("TimeOut : Vérification de l'existence de fiches du mois passé non validées...");

                    // S'il y a des fiches du mois dernier en état 'CL', on les ferme
                    if (this.getFichesCL())
                    {
                        GSBLog.WriteEntry("=> Il existe bien des fiches du mois passé non validées");
                        this.mettreFichesEnPaiement();
                        GSBLog.WriteEntry("=> Validation de toutes les fiches du mois passé réussie");
                    }
                    else
                    {
                        GSBLog.WriteEntry("=> Toutes les fiches du mois passé sont bien validées");
                    }
                }
            }
            catch (Exception ex)
            {
                GSBLog.WriteEntry(ex.Message);
            }
        }


        /// <summary>
        /// Vérifie si il y a des fiches du mois passé qui ne sont pas fermées (état 'CR')
        /// </summary>
        /// <returns>true ou false selon s'il existe des fiches du mois passé encore en état 'CR' ou non</returns>
        private bool getFichesCR()
        {
            // Dans le doute on force la maj en retournant true
            bool ret = true;
            try
            {
                connexion = ConnexionSql.getInstance(serveur, bdd, utilisateur, mdp);
                // Récupére une DataTable contenant les fiches correspondant à la key et en état CR
                DataTable dt = connexion.getFichesMois(key, "CR");
                if(dt.Rows.Count == 0)
                {
                    ret = false;
                }
                connexion.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }


        /// <summary>
        /// Vérifie si il y a des fiches du mois passé qui ne sont pas mise en paiement (état 'CL')
        /// </summary>
        /// <returns>true ou false selon s'il existe des fiches du mois passé encore en état 'CL' ou non</returns>
        private bool getFichesCL()
        {
            // Dans le doute on force la maj en retournant true
            bool ret = true;
            try
            {
                connexion = ConnexionSql.getInstance(serveur, bdd, utilisateur, mdp);
                // Récupére une DataTable contenant les fiches correspondant à la key et en état CR
                DataTable dt = connexion.getFichesMois(key, "CL");
                if (dt.Rows.Count == 0)
                {
                    ret = false;
                }
                connexion.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }


        /// <summary>
        /// Ferme toutes les fiches du mois passé qui étaient en état 'CR', en les mettant en état 'CL'
        /// </summary>
        private void closeFichesCR()
        {
            try
            {
                connexion = ConnexionSql.getInstance(serveur, bdd, utilisateur, mdp);

                // Ferme l'état des fiches du mois key en état 'CR'
                connexion.closeFichesMois(key);

                connexion.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Passe toutes les fiches du mois passé en état validée et mises en paiement (état 'VA')
        /// </summary>
        private void mettreFichesEnPaiement()
        {
            try
            {
                connexion = ConnexionSql.getInstance(serveur, bdd, utilisateur, mdp);

                // Met les fiches du mois dernier en état 'VA'
                connexion.fichesMoisToVA(key);

                connexion.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
