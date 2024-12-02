using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// Samuel Vinette / Créer le 2024-12-27 13:35 / Dernière modification: 2024-12-27 13:57

namespace ProjetFinDeSession2024
{
    internal class Activite : INotifyPropertyChanged
    {
        string nom;
        string categorie;
        int cout_organisation;
        int cout_vente;

        public Activite(string nom, string categorie, int cout_organisation, int cout_vente)
        {
            this.nom = nom;
            this.categorie = categorie;
            this.cout_organisation = cout_organisation;
            this.cout_vente = cout_vente;
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Categorie
        {
            get { return categorie; }
            set { categorie = value; }
        }

        public int Cout_Organization
        {
            get { return cout_organisation; }
            set { cout_organisation = value; }
        }

        public int Cout_Vente
        {
            get { return cout_vente; }
            set { cout_vente = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
