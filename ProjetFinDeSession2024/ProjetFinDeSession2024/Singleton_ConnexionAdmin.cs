using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Samuel Vinette / Créer le 2024-12-27 13:10 / Dernière modification: 2024-12-27 13:32

namespace ProjetFinDeSession2024
{
    internal class SingletonConnexion
    {
        bool connected;
        static SingletonConnexion instance = null;

        public SingletonConnexion()
        {
            connected = new bool();
            connected = false;
        }

        public static SingletonConnexion getInstance()
        {
            if (instance == null)
                instance = new SingletonConnexion();

            return instance;
        }

        // Retourner la connexion
        public bool isConnected()
        {
            return connected;
        }

        // Connexion
        public bool seConnecter(string utilisateur, string motdepasse)
        {
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"select * from admin";

                con.Open();
                commande.Prepare();

                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    if (utilisateur == r["utilisateur"].ToString() && motdepasse == r["motdepasse"].ToString())
                    {
                        con.Close();
                        connected = true;

                        // Deconnecte le compte Adherent si connecté
                        SingletonConnexionAdherant.getInstance().Deconnexion();
                        return true;
                    }
                }
                con.Close();
                connected = false;
                return false;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }

        }

        // Déconnexion
        public bool seDeconnecter()
        {
            connected = false;
            return true;
        }
    }
}
