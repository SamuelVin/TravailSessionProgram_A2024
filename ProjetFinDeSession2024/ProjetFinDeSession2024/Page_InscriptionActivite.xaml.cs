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

// Samuel Vinette / Créer le 2024-12-27 13:10 / Dernière modification: 2024-12-27 13:32 -->

namespace ProjetFinDeSession2024
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_InscriptionActivite : Page
    {
        ObservableCollection<Activite> liste = new ObservableCollection<Activite>() { };
        public Page_InscriptionActivite()
        {
            this.InitializeComponent();
            liste = SingletonListe_Activite.getInstance().getListe();

            if (SingletonConnexion.getInstance().isAdmin() == true)
            {
                stk_connecter.Visibility = Visibility.Visible;
                bt_ajouter.Visibility = Visibility.Visible;
                bt_supprimer.Visibility = Visibility.Visible;
            }
        }

        private void bt_supprimer_Click(object sender, RoutedEventArgs e)
        {
            int selected_index = lv_list.SelectedIndex;
            bool operation_reussi = SingletonListe_Activite.getInstance().supprimerActivite(selected_index);
            if(operation_reussi == true)
            {
                txtblock_erreur.Visibility = Visibility.Collapsed;
                txtblock_reussi.Visibility = Visibility.Visible;
            }
            else
            {
                txtblock_erreur.Visibility = Visibility.Visible;
                txtblock_reussi.Visibility = Visibility.Collapsed;
            }
        }

        private void bt_ajouter_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageAdmin_AjoutActivite));
        }

        private void bt_seance_Click(object sender, RoutedEventArgs e)
        {
            if(lv_list.SelectedIndex != -1) 
            {
                this.Frame.Navigate(typeof(Page_InscriptionSeance), lv_list.SelectedIndex+1);
            }
        }
    }
}
