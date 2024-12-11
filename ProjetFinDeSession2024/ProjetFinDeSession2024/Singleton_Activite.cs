using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

// Samuel Vinette / Créer le 2024-12-27 13:40 / Dernière modification: 2024-12-27 13:57

namespace ProjetFinDeSession2024
{
    class SingletonListe_Activite
    {
        ObservableCollection<Activite> liste;
        static SingletonListe_Activite instance = null;
        private string messageSQL;

        public SingletonListe_Activite()
        {
            liste = new ObservableCollection<Activite>();

            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");
            MySqlCommand commande = con.CreateCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from activite";
            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                string nom = r["nom"].ToString();
                string categorie = r["categorie"].ToString();
                int cout_organisation = int.Parse(r["cout_organisation"].ToString());
                int cout_vente = int.Parse(r["cout_vente"].ToString());

                liste.Add(new Activite(nom, categorie, cout_organisation, cout_vente));
            }
            con.Close();
        }

        public static SingletonListe_Activite getInstance()
        {
            if (instance == null)
                instance = new SingletonListe_Activite();

            return instance;
        }

        // Retourner equipe
        public Activite getActivite(int position)
        {
            return liste[position];
        }

        // Retourner liste equipe
        public ObservableCollection<Activite> getListe()
        {
            liste.Clear();

            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");
            MySqlCommand commande = con.CreateCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from activite";
            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                string nom = r["nom"].ToString();
                string categorie = r["categorie"].ToString();
                int cout_organisation = int.Parse(r["cout_organisation"].ToString());
                int cout_vente = int.Parse(r["cout_vente"].ToString());

                liste.Add(new Activite(nom, categorie, cout_organisation, cout_vente));
            }

            con.Close();
            return liste;
        }

        public ObservableCollection<Activite> Recherche(string rechercheNom)
        {
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM activite WHERE categorie LIKE @recherche";
                commande.Parameters.Clear();
                commande.Parameters.AddWithValue("@recherche", $"%{rechercheNom}%");
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                liste.Clear();

                while (r.Read())
                {
                    string nom = r["nom"].ToString();
                    string categorie = r["categorie"].ToString();
                    int cout_organisation = int.Parse(r["cout_organisation"].ToString());
                    int cout_vente = int.Parse(r["cout_vente"].ToString());
                    liste.Add(new Activite(nom, categorie, cout_organisation, cout_vente));
                }

                con.Close();
                r.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                messageSQL = ex.Message;
            }
            return liste;
        }

        // Ajouter equiper
        public bool ajouterActivite(string nom, string categorie, int cout_organisation, int cout_vente)
        {
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO activite (nom, categorie, cout_organisation, cout_vente) VALUES (@nom, @categorie, @cout_organisation, @cout_vente)";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@categorie", categorie);
                commande.Parameters.AddWithValue("@cout_organisation", cout_organisation);
                commande.Parameters.AddWithValue("@cout_vente", cout_vente);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                liste.Add(new Activite(nom, categorie, cout_organisation, cout_vente));
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }

        }

        public void Modifier(Activite activite)
        {
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "UPDATE activite SET nom = @_nom, categorie = @_categorie, cout_organisation = @_cout_organisation, cout_vente = @_cout_vente WHERE categorie = @_categorie";
                commande.Parameters.AddWithValue("@_nom", activite.Nom);
                commande.Parameters.AddWithValue("@_categorie", activite.Categorie);
                commande.Parameters.AddWithValue("@_cout_organisation", activite.Cout_Organization);
                commande.Parameters.AddWithValue("@_cout_vente", activite.Cout_Vente);
                
                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                commande.Parameters.Clear();
            }
            catch (Exception ex)
            {
                messageSQL = ex.Message;
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();


                Debug.WriteLine($"MySQL Error: {messageSQL} TEST");
            }
        }

        // Supprimer equiper
        public bool supprimerActivite(int position)
        {
            liste.RemoveAt(position);
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

            try
            {
                MySqlCommand commande1 = new MySqlCommand();
                commande1.Connection = con;
                commande1.CommandText = "DELETE FROM activite WHERE id = @id";
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
