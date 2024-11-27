using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Samuel Vinette / Créer le 2024-12-27 13:40 / Dernière modification: 2024-12-27 13:57

namespace ProjetFinDeSession2024
{
    class SingletonListe_Activite
    {
        ObservableCollection<Activite> liste;
        static SingletonListe_Activite instance = null;

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

            return liste;
        }

        // Ajouter equiper
        public void ajouterActivite(string nom, string categorie, int cout_organisation, int cout_vente)
        {
            // Pour ce connecter
            MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00002_2260734-samuel-vinette;Uid=2260734;Pwd=2260734;");

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"insert into activite values('{nom}','{categorie}','{cout_organisation}','{cout_vente}')";

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                liste.Add(new Activite(nom, categorie, cout_organisation, cout_vente));
            }
            catch (Exception ex)
            {
                con.Close();
            }

        }

        // Supprimer equiper
        public void supprimerActivite(int position)
        {
            liste.RemoveAt(position);
        }
    }
}
