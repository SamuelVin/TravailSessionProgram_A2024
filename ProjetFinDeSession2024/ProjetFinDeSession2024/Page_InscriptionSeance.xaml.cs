using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

// Samuel Vinette / Créer le 2024-11-28 15:53 / Dernière modification: 2024-11-28 15:53

namespace ProjetFinDeSession2024
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_InscriptionSeance : Page
    {
        ObservableCollection<Seance> liste = new ObservableCollection<Seance>() { };
        public Page_InscriptionSeance()
        {
            this.InitializeComponent();
            liste = SingletonListe_Seance.getInstance().getListe();

            if (SingletonConnexion.getInstance().isConnected() == true)
            {
                stk_connecter.Visibility = Visibility.Visible;
            }
        }
    }
}
