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

namespace ProjetFinDeSession2024
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_Commentaire : Page
    {
        ObservableCollection<Message> liste = new ObservableCollection<Message>() { };
        public Page_Commentaire()
        {
            this.InitializeComponent();
            liste = SingletonListe_Message.getInstance().getListe();

            if (SingletonConnexion.getInstance().isAdmin() == true)
            {
                stk_connecter.Visibility = Visibility.Visible;
                bt_supprimer.Visibility = Visibility.Visible;
            }
            else if (SingletonConnexion.getInstance().isConnected() == true) 
            {
                bt_ajouter.Visibility = Visibility.Visible;
            }
        }

        private void bt_supprimer_Click(object sender, RoutedEventArgs e)
        {
            int selected_index = lv_list.SelectedIndex;
            bool operation_reussi = SingletonListe_Message.getInstance().supprimerMessage(selected_index);
            if (operation_reussi == true)
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
            this.Frame.Navigate(typeof(Page_AjoutCommentaire));
        }
    }
}
