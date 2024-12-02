using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

// Marco Petiquay / Créer le 2024-11-30 14:27 / Dernière modification:

namespace ProjetFinDeSession2024
{
    class Adherents : INotifyPropertyChanged
    {
        private string id, nom, prenom, adresse;
        private DateTime dateNaissance;
        private int age;

        public Adherents()
        {
            id = string.Empty;
            nom = string.Empty;
            prenom = string.Empty;
            adresse = string.Empty;
            dateNaissance = new DateTime(1979, 1, 1);
            age = 0;
        }

        public Adherents(string _id, string _nom, string _prenom, string _adresse, DateTime _dateNaissance, int _age)
        {
            this.id = _id;
            this.nom = _nom;
            this.prenom = _prenom;
            this.adresse = _adresse;
            this.dateNaissance = _dateNaissance;
            this.age = _age;
        }

        public string Identifiant
        {
            get => id; 
            set
            {
                id = value;
                this.OnPropertyChanged(nameof(id));
            }
        }

        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
                this.OnPropertyChanged(nameof(nom));
            }
        }

        public string Prenom
        {
            get => prenom;
            set
            {
                prenom = value;
                this.OnPropertyChanged(nameof(prenom));
            }
        }

        public string NomPrenom() => $"{prenom} {nom}";

        public string Adresse
        {
            get => adresse;
            set
            {
                adresse = value;
                this.OnPropertyChanged(nameof(adresse));
            }
        }

        public DateTime DateNaissance
        {
            get => dateNaissance;
            set
            { 
                dateNaissance = value;
                this.OnPropertyChanged(nameof(dateNaissance));
            }
        }

        public override string ToString()
        {
            return $"{Nom} {Prenom} - {id}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

        internal object ToLower()
        {
            throw new NotImplementedException();
        }
    }
}
