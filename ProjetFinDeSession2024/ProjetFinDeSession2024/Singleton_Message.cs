using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinDeSession2024
{
    class SingletonListe_Message
    {
        ObservableCollection<Message> liste;
        static SingletonListe_Message instance = null;

        public SingletonListe_Message()
        {
            liste = new ObservableCollection<Message>();

            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");
            MySqlCommand commande = con.CreateCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from message";
            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                int id = int.Parse(r["id"].ToString());
                int id_adherent = int.Parse(r["id_adherent"].ToString());
                int id_activite = int.Parse(r["id_activite"].ToString());
                string nom = "Nom Temporaire";
                string commentaire = r["commentaire"].ToString();
                int note_5 = int.Parse(r["note_5"].ToString());

                liste.Add(new Message(id, id_adherent, id_activite, nom, commentaire, note_5));
            }
            con.Close();
        }

        public static SingletonListe_Message getInstance()
        {
            if (instance == null)
                instance = new SingletonListe_Message();

            return instance;
        }

        // Retourner equipe
        public Message getMessage(int position)
        {
            return liste[position];
        }

        // Retourner liste equipe
        public ObservableCollection<Message> getListe()
        {
            liste.Clear();

            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");
            MySqlCommand commande = con.CreateCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from message";
            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                int id = int.Parse(r["id"].ToString());
                int id_adherent = int.Parse(r["id_adherent"].ToString());
                int id_activite = int.Parse(r["id_activite"].ToString());
                string nom = "Nom Temporaire";
                string commentaire = r["commentaire"].ToString();
                int note_5 = int.Parse(r["note_5"].ToString());

                liste.Add(new Message(id, id_adherent, id_activite, nom, commentaire, note_5));
            }

            con.Close();
            return liste;
        }

        // Ajouter equiper
        public bool ajouterMessage(int id_adherent, int id_activite, string commentaire, int note_5)
        {
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO message (id_adherent, id_activite, commentaire, note_5) VALUES (@id_adherent, @id_activite, @commentaire, @note_5)";
                commande.Parameters.AddWithValue("@id_adherent", id_adherent);
                commande.Parameters.AddWithValue("@id_activite", id_activite);
                commande.Parameters.AddWithValue("@commentaire", commentaire);
                commande.Parameters.AddWithValue("@note_5", note_5);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                liste.Add(new Message(-1, id_adherent, id_activite, "Nom Temporaire", commentaire, note_5));
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }

        }

        // Supprimer equiper
        public bool supprimerMessage(int position)
        {
            liste.RemoveAt(position);
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

            try
            {
                MySqlCommand commande1 = new MySqlCommand();
                commande1.Connection = con;
                commande1.CommandText = "DELETE FROM message WHERE id = @id";
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
