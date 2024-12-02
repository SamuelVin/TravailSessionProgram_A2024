using Microsoft.UI.Xaml.Controls.Primitives;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace ProjetFinDeSession2024
{

    class SingletonConnexionAdherant
    {
        private static SingletonConnexionAdherant instance;
        private MySqlConnection con;
        private MySqlCommand commande = new MySqlCommand();
        private bool connected;

        ObservableCollection<Adherents> liste;
        private Adherents adherent;

        SingletonConnexionAdherant()
        {
            adherent = new Adherents();
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00001_2089642-marco-petiquay;Uid=2089642;Pwd=2089642;");
        }

        public static SingletonConnexionAdherant getInstance()
        {
            if (instance == null)
                instance = new SingletonConnexionAdherant();

            return instance;
        }

        public bool GetConnexion()
        {
            return connected;
        }

        public bool Connexion(string _identifiant)
        {
            try
            {
                commande.Connection = con;
                commande.CommandText = "Select * from adherents;";
                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();

                // Cherche dans la base de donnee l'identifiant
                while (r.Read())
                {
                    if (_identifiant == r.GetString(0))
                    {
                        string nom = r.GetString(1);
                        string prenom = r.GetString(2);
                        string adresse = r.GetString(3);
                        DateTime dateNaissance = r.GetDateTime(4);
                        int age = r.GetInt32(5);

                        con.Close();
                        connected = true;

                        adherent = new Adherents(_identifiant, nom, prenom, adresse, dateNaissance, age);
                        // Deconnecte le compte Administrateur si connecté
                        SingletonConnexion.getInstance().seDeconnecter();
                        return true;
                    }
                    return false;
                }
                r.Close();
                con.Close();
                return false;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        // Deconnexion de l'adherant
        public bool Deconnexion()
        {
            connected = false;
            return true;
        }

        public Adherents GetAdherent()
        {
            return adherent;
        }


    }
}
