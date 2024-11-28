using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// Samuel Vinette / Créer le 2024-11-28 15:36 / Dernière modification: 2024-11-28 15:53

namespace ProjetFinDeSession2024
{
    internal class Seance : INotifyPropertyChanged
    {
        DateOnly date;
        TimeOnly heure;

        public Seance(DateOnly date, TimeOnly heure)
        {
            this.date = date;
            this.heure = heure;
        }

        public DateOnly Date
        {
            get { return date; }
            set { date = value; }
        }

        public TimeOnly Heure
        {
            get { return heure; }
            set { heure = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
