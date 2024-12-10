using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinDeSession2024
{
    internal class Message : INotifyPropertyChanged
    {
        int id;
        int id_adherent;
        int id_activite;
        string nom;
        string commentaire;
        int note_5;

        public Message(int id, int id_adherent, int id_activite, string nom, string commentaire, int note_5)
        {
            this.id = id;
            this.id_adherent = id_adherent;
            this.id_activite = id_activite;
            this.nom = nom;
            this.commentaire = commentaire;
            this.note_5 = note_5;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Id_adherent
        {
            get { return id_adherent; }
            set { id_adherent = value; }
        }
        public int Id_activite
        {
            get { return id_activite; }
            set { id_activite = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Commentaire
        {
            get { return commentaire; }
            set { commentaire = value; }
        }
        public int Note_5
        {
            get { return note_5; }
            set { note_5 = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
