using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using ProjetFinDeSession2024;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private string messageSQL;

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
                commande.CommandText = "CALL ps_afficherAdherant()";
                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string id = r.GetString(0);
                    string nom = r.GetString(1);
                    string prenom = r.GetString(2);
                    string adresse = r.GetString(3);
                    DateTime dateNaissance = r.GetDateTime(4);
                    int age = r.GetInt32(5);
                    liste.Add(new Adherents(id, nom, prenom, adresse, dateNaissance, age));
                }
                r.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                messageSQL = ex.Message;
            }
            return liste;
        }

        public ObservableCollection<Adherents> Recherche(string rechercheNom)
        {
            try
            {
                commande.Connection = con;
                commande.CommandText = "CALL ps_rechercheAdherent(@recherche)";
                commande.Parameters.Clear();
                commande.Parameters.AddWithValue("@recherche", $"%{rechercheNom}%");
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                liste.Clear();

                while (r.Read())
                {
                    string id = r.GetString(0);
                    string nom = r.GetString(1);
                    string prenom = r.GetString(2);
                    string adresse = r.GetString(3);
                    DateTime dateNaissance = r.GetDateTime(4);
                    int age = r.GetInt32(5);
                    liste.Add(new Adherents(id, nom, prenom, adresse, dateNaissance, age));
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

        public void Ajouter(Adherents adherents)
        {
            try
            {
                commande.Parameters.Clear();
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
            }/*
            catch (MySqlException ex)
            {
                if (ex.Number == 1644)
                    messageSQL = ex.Message;
            }*/
            catch (Exception ex)
            {
                messageSQL = ex.Message;
                Debug.WriteLine($"MySQL Error: {messageSQL} TEST");
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public void Modifier(string identifiant, Adherents adherents)
        {
            try
            {
                commande.Connection = con;
                commande.CommandText = "CALL ps_modifierAdherent(@id, @nom, @prenom, @adresse, @dateNaissance)";
                commande.Parameters.AddWithValue("@id", identifiant);
                commande.Parameters.AddWithValue("@nom", adherents.Nom);
                commande.Parameters.AddWithValue("@prenom", adherents.Prenom);
                commande.Parameters.AddWithValue("@adresse", adherents.Adresse);
                commande.Parameters.AddWithValue("@dateNaissance", adherents.DateNaissance);

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
                commande.Parameters.Clear();
            }/*
            catch (MySqlException ex)
            {
                if (ex.Number == 1644)
                    messageSQL = ex.Message;
            }*/
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public string Message()
        {
            return messageSQL;
        }

        public void RefreshBD()
        {
            liste.Clear();
            getListe();
        }
    }
}
