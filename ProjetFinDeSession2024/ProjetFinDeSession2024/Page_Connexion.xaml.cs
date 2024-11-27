using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
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
    /// 

    // Samuel Vinette / Créer le 2024-12-27 14:00 / Dernière modification: 2024-12-27 14:32
    public sealed partial class Page_Connexion : Page
    {
        public Page_Connexion()
        {
            this.InitializeComponent();
        }

        private void bt_connexion_Click(object sender, RoutedEventArgs e)
        {
            bool operation_reussi = SingletonConnexion.getInstance().seConnecter(txtbox_utilisateur.Text, txtbox_motdepasse.Password);
            if (operation_reussi == true)
            {
                txtbloc_reussi.Visibility = Visibility.Visible;
                txtbloc_echouer.Visibility = Visibility.Collapsed;
                txtbox_motdepasse.Password = "";
                txtbox_utilisateur.Text = "";
            }
            else
            {
                txtbloc_reussi.Visibility = Visibility.Collapsed;
                txtbloc_echouer.Visibility = Visibility.Visible;
                txtbox_motdepasse.Password = "";
            }
        }

        private void bt_testcon_Click(object sender, RoutedEventArgs e)
        {
            bool operation_reussi = SingletonConnexion.getInstance().isConnected();
            if (operation_reussi == true)
            {
                txtbloc_reussi.Visibility = Visibility.Visible;
                txtbloc_echouer.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtbloc_reussi.Visibility = Visibility.Collapsed;
                txtbloc_echouer.Visibility = Visibility.Visible;
            }
        }

        private void bt_deconnecter_Click(object sender, RoutedEventArgs e)
        {
            bool operation_reussi = SingletonConnexion.getInstance().seDeconnecter();
            if (operation_reussi == false)
            {
                txtbloc_reussi.Visibility = Visibility.Visible;
                txtbloc_echouer.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtbloc_reussi.Visibility = Visibility.Collapsed;
                txtbloc_echouer.Visibility = Visibility.Visible;
            }
        }
    }
}
