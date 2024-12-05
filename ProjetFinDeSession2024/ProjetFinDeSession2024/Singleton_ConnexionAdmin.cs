using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Samuel Vinette / Créer le 2024-12-27 13:10 / Dernière modification: 2024-12-27 13:32

namespace ProjetFinDeSession2024
{
    internal class SingletonConnexion
    {
        int connectedId;
        bool connected;
        bool administrateur;
        static SingletonConnexion instance = null;

        public SingletonConnexion()
        {
            connectedId = 0;
            connected = new bool();
            connected = false;
            administrateur = false;
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

        public bool isAdmin()
        {
            return administrateur;
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
                    if(utilisateur == r["utilisateur"].ToString() && motdepasse == r["motdepasse"].ToString())
                    {
                        if (bool.Parse(r["administrateur"].ToString()) == true)
                        {
                            administrateur=true;
                        }
                        connectedId = int.Parse(r["id"].ToString());
                        con.Close();
                        connected = true;
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
                Debug.WriteLine("Erreur: " + ex.Message);
                return false;
            }

        }

        // Déconnexion
        public bool seDeconnecter()
        {
            connectedId = 0;
            administrateur = false;
            connected= false;
            return true;
        }

        public bool inscriptionSeance5()
        {
            if(connected == true && connectedId > 0)
            {
                // Pour ce connecter
                MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

                try
                {
                    MySqlCommand commande = new MySqlCommand();
                    commande.Connection = con;
                    commande.CommandText = $"select id_adherent, count(*) as nbseance from adherentseance group by id_adherent";

                    con.Open();
                    commande.Prepare();

                    MySqlDataReader r = commande.ExecuteReader();

                    while (r.Read())
                    {
                        if (int.Parse(r["id_adherent"].ToString())==connectedId)
                        {
                            if (int.Parse(r["nbseance"].ToString()) < 5)
                            {
                                con.Close();
                                return true;
                            }
                            else
                            {
                                con.Close();
                                return false;
                            }
                            
                        }
                    }
                    con.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    con.Close();
                    Debug.WriteLine("Erreur: " + ex.Message);
                    return false;
                }
            }
            return false;
        }

        public bool inscrire(int id_seance)
        {
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO adherentseance (id_adherent, id_seance) VALUES (@id_adherent, @id_seance)";
                commande.Parameters.AddWithValue("@id_adherent", connectedId);
                commande.Parameters.AddWithValue("@id_seance", id_seance);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                Debug.WriteLine("Erreur: " + ex.Message);
                return false;
            }
        }
    }
}
