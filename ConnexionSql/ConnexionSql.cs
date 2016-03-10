using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace mesBdd
{
    /// <summary>
    /// Outil de gestion de l'accés à la base de données de gestion de frais, avec un système de singleton (constructeur privé + variable static volatile) et une fonction d'éxécution de requête
    /// </summary>
    public class ConnexionSql
    {
        // PROPRIETES :
        // ------------
        private static string connString;
        private static volatile ConnexionSql connexion = null;
        private static MySqlConnection oleCn = null;
        private MySqlCommand reqSQL = null;


        /// <summary>
        /// Constructeur
        /// </summary>
        private ConnexionSql(string unProvider, string uneDataBase, string unUid, string unMdp)
        {
            try
            {
                connString = "SERVER=" + unProvider + ";" + "DATABASE=" +
                            uneDataBase + ";" + "UID=" + unUid + ";" + "PASSWORD=" + unMdp + ";";
                try
                {
                    oleCn = new MySqlConnection(connString);
                }
                catch (Exception ex)
                {
                    throw ex;// showError(emp.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;// Program.showError(emp.Message);
            }
        }


        /// <summary>
        /// Méthode permettant de renvoyer une connexion unique (Singleton)
        /// </summary>
        public static ConnexionSql getInstance(string unProvider, string uneDataBase, string unUid, string unMdp)
        {
            try
            {
                if (null == oleCn)
                { // Premier appel
                    connexion = new ConnexionSql(unProvider, uneDataBase, unUid, unMdp);
                    connexion.OpenConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return connexion;
        }


        /// <summary>
        /// Ouvre une connexion à la base de données
        /// </summary>
        private void OpenConnection()
        {
            try
            {
                oleCn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Ferme la connexion
        /// </summary>
        public void CloseConnection()
        {
            if (oleCn != null)
            {
                oleCn.Close();
                oleCn = null;
            }
        }


        /// <summary>
        /// Exécute une requête passée en argument
        /// </summary>
        /// <param name="req">Le texte de la requête</param>
        /// <returns>L'objet MySqlCommand contenant le résultat de la requête</returns>
        public MySqlCommand reqExec(string req)
        {
            MySqlCommand mysqlCom = new MySqlCommand(req, oleCn);
            return (mysqlCom);
        }


        /// <summary>
        /// Récupére toutes les fiches en bases du mois correspondant à la clef 
        /// </summary>
        /// <param name="key">La clef format : AAAAMM</param>
        /// <param name="etat">Ajoute condition à la requête pour ne récupérer que les fiches frais de cet état</param>
        /// <returns>La DataTable contenant les fiches</returns>
        public DataTable getFichesMois(string key, string etat = "")
        {
            DataTable dt = new DataTable();

            // Met en forme le datagrid view
            dt.Columns.Add("NOM");
            dt.Columns.Add("PRENOM");
            dt.Columns.Add("MOIS");
            dt.Columns.Add("NB JUSTIFICATIFS");
            dt.Columns.Add("MONTANT VALIDE");
            dt.Columns.Add("DATE MODIF");
            dt.Columns.Add("ETAT");

            // Tentative de récupérer les données en base
            try
            {

                // Exécute la requête de lecture de la table
                string query = 
                "SELECT nom, prenom, mois, nbJustificatifs, montantValide, dateModif, idEtat "
                + "FROM visiteur, fichefrais "
                + "WHERE visiteur.ID = fichefrais.idVisiteur "
                + "AND mois = " + key + " ";

                if(etat != "")
                {
                    query += "AND idEtat = '"+etat+"'";
                }

                reqSQL = this.reqExec(query);

                // Commence la lecture
                MySqlDataReader reader = reqSQL.ExecuteReader();
                while (reader.Read())
                {
                    // Remplissage de la table (DataTable) 
                    DataRow dr = dt.NewRow();
                    dr[0] = reader[0];
                    dr[1] = reader[1];
                    dr[2] = reader[2];
                    dr[3] = reader[3];
                    dr[4] = reader[4];
                    dr[5] = reader[5];
                    dr[6] = reader[6];
                    dt.Rows.Add(dr);
                }
                // Ferme la lecture
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        /// <summary>
        /// Ferme toutes les fiches du mois key en les mettant en état 'CL'
        /// </summary>
        /// <param name="key">Mois concerné, format : AAAAMM</param>
        public void closeFichesMois(string key)
        {
            string query =
                "UPDATE fichefrais " +
                "SET idEtat = 'CL' " +
                "WHERE mois = " + key + " " +
                "AND idEtat = 'CR'";

            reqSQL = this.reqExec(query);
            MySqlDataReader reader = reqSQL.ExecuteReader();
            while (reader.Read())
            {
            }
            reader.Close();
        }


        /// <summary>
        /// Passe toutes les fiches du mois key en état 'VA' validation attente paiement
        /// </summary>
        /// <param name="key">Mois concerné, format : AAAAMM</param>
        public void fichesMoisToVA(string key)
        {
            string query =
                "UPDATE fichefrais " +
                "SET idEtat = 'VA' " +
                "WHERE mois = " + key + " " +
                "AND idEtat = 'CL'";
            reqSQL = this.reqExec(query);
            MySqlDataReader reader = reqSQL.ExecuteReader();
            while (reader.Read())
            {
            }
            reader.Close();
        }
    }
}
