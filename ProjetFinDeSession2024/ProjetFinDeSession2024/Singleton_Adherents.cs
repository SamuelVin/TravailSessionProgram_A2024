using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Marco Petiquay / Créer le 2024-11-30 14:27 / Dernière modification: 

namespace ProjetFinDeSession2024
{
    class Singleton_Adherents
    {
        private MySqlConnection con;
        private MySqlCommand commande = new MySqlCommand();

        ObservableCollection<Adherents> liste;
        static Singleton_Adherents instance = null;

        Singleton_Adherents()
        {
            this.liste = new ObservableCollection<Adherents>();
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00001_2089642-marco-petiquay;Uid=2089642;Pwd=2089642;");
        }
        public static Singleton_Adherents getInstance()
        {
            if (instance is null)
                instance = new Singleton_Adherents();
            return instance;
        }

        public ObservableCollection<Adherents> Liste { get { return this.liste; } }

        public int Count { get { return this.liste.Count; } }

        public ObservableCollection<Adherents> getListe()
        {
            liste.Clear();

            try
            {
                commande.Connection = con;
                commande.CommandText = "Select * from Adherent";
                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string id = r.GetString(1);
                    string nom = r.GetString(2);
                    string prenom = r.GetString(3);
                    string adresse = r.GetString(4);
                    DateTime dateNaissance = r.GetDateTime(5);
                    int age = r.GetInt32(6);
                    liste.Add(new Adherents(id, nom, prenom, adresse, dateNaissance, age));
                }
                r.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine(ex.Message);
            }
            return liste;
        }

        public ObservableCollection<Adherents> Recherche(string recherche)
        {
            try
            {
                commande.Connection = con;
                commande.CommandText = "CALL Recherche(@recherche)";
                commande.Parameters.Clear();
                commande.Parameters.AddWithValue("@recherche", $"%{recherche}%");
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                liste.Clear();

                while (r.Read())
                {
                    string id = r.GetString(1);
                    string nom = r.GetString(2);
                    string prenom = r.GetString(3);
                    string adresse = r.GetString(4);
                    DateTime dateNaissance = r.GetDateTime(5);
                    int age = r.GetInt32(6);
                    liste.Add(new Adherents(id, nom, prenom, adresse, dateNaissance, age));
                }

                con.Close();
                r.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine(ex.Message);
            }
            return liste;
        }

        public void Ajouter(Adherents adherents)
        {
            try
            {
                commande.Connection = con;
                commande.CommandText = "CALL ps_ajouterAdherent(@nom, @prenom, @adresse, @dateNaissance)";
                commande.Parameters.AddWithValue("@nom", adherents.Nom);
                commande.Parameters.AddWithValue("@prenom", adherents.Prenom);
                commande.Parameters.AddWithValue("@adresse", adherents.Adresse);
                commande.Parameters.AddWithValue("@dateNaissance", adherents.DateNaissance);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public void Modifier(Adherents adherents)
        {
            try
            {
                commande.Connection = con;
                commande.CommandText = "CALL ps_modifierAdherent(@id, @nom, @prenom, @adresse, @dateNaissance)";
                commande.Parameters.AddWithValue("@id", adherents.Identifiant);
                commande.Parameters.AddWithValue("@nom", adherents.Nom);
                commande.Parameters.AddWithValue("@prenom", adherents.Prenom);
                commande.Parameters.AddWithValue("@adresse", adherents.Adresse);
                commande.Parameters.AddWithValue("@dateNaissance", adherents.DateNaissance);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public void Supprimer(Adherents adherents)
        {
            try
            {
                commande.Connection = con;
                commande.CommandText = "CALL ps_supprimerAdherent(@numeroId)";
                commande.Parameters.AddWithValue("@numeroId", adherents.Identifiant);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

    }
}
