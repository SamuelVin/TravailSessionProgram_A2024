using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Samuel Vinette / Créer le 2024-11-28 15:36 / Dernière modification: 2024-11-28 15:53

namespace ProjetFinDeSession2024
{
    class SingletonListe_Seance
    {
        ObservableCollection<Seance> liste;
        static SingletonListe_Seance instance = null;

        public SingletonListe_Seance()
        {
            liste = new ObservableCollection<Seance>();

            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");
            MySqlCommand commande = con.CreateCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from seance";
            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                DateOnly date = DateOnly.Parse(r["date"].ToString());
                TimeOnly heure = TimeOnly.Parse(r["heure"].ToString());

                liste.Add(new Seance(date, heure));
            }
            con.Close();
        }

        public static SingletonListe_Seance getInstance()
        {
            if (instance == null)
                instance = new SingletonListe_Seance();

            return instance;
        }

        // Retourner equipe
        public Seance getSeance(int position)
        {
            return liste[position];
        }

        // Retourner liste equipe
        public ObservableCollection<Seance> getListe()
        {
            liste.Clear();

            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");
            MySqlCommand commande = con.CreateCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from seance";
            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                DateOnly date = DateOnly.Parse(r["date"].ToString());
                TimeOnly heure = TimeOnly.Parse(r["heure"].ToString());

                liste.Add(new Seance(date, heure));
            }
            con.Close();

            return liste;
        }

        public ObservableCollection<Seance> getListe_DeActivite(int id_activite)
        {
            liste.Clear();

            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");
            MySqlCommand commande = con.CreateCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from seance where id_activite = @id_activite";
            commande.Parameters.AddWithValue("@id_activite", id_activite);
            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                DateOnly date = DateOnly.Parse(r["date"].ToString());
                TimeOnly heure = TimeOnly.Parse(r["heure"].ToString());

                liste.Add(new Seance(date, heure));
            }
            con.Close();

            return liste;
        }

        // Ajouter equiper
        public bool ajouterSeance(DateOnly date, TimeOnly heure)
        {
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO seance (date, heure) VALUES (@date, @heure)";
                commande.Parameters.AddWithValue("@date", date);
                commande.Parameters.AddWithValue("@heure", heure);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                liste.Add(new Seance(date, heure));
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }

        }

        // Supprimer equiper
        public bool supprimerSeance(int position)
        {
            liste.RemoveAt(position);
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

            try
            {
                MySqlCommand commande1 = new MySqlCommand();
                commande1.Connection = con;
                commande1.CommandText = "DELETE FROM seance WHERE id = @id";
                commande1.Parameters.AddWithValue("@id", position + 1);

                con.Open();
                commande1.Prepare();
                commande1.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }
    }
}
