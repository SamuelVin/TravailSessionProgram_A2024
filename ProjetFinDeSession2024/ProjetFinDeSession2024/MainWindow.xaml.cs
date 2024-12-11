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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    // Samuel Vinette / Créer le 2024-12-27 14:00 / Dernière modification: 2024-12-27 14:32
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            mainFrame.Navigate(typeof(Page_InscriptionActivite));
            Admin();
        }

        private async void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;
            Console.WriteLine(item.Name);
            switch (item.Name)
            {
                case "nvAffichage":
                    mainFrame.Navigate(typeof(Page_InscriptionActivite));
                    break;
                case "nvConnexion":

                    // La connexion de l'adherent/l'utilisateur fonctionne, voici le mot de passe: BG-2005-473
                    // Pour l'instant, le programme se connecte sur ma base de donnée dans la classe SingletonConnexionAdherant()
                    // Si tu regarde dans Partie Publique, on aura besoin que l'Adhérent puisse pouvoir se connecter
                    // pour s'inscrire dans une activité/séance. Par contre, il faudra checker plus-tard comme quoi il ne peut pas
                    // s'inscrire dans la même activité/séance deux fois.

                    // Si il n'y a aucun adhérent de connectee, la boite de dialogue DialogueAdherentsConnexion() va s'afficher.
                    if (!(SingletonConnexionAdherant.getInstance().GetConnexion()))
                    {

                        DialogueAdherentsConnexion dialog = new DialogueAdherentsConnexion();

                        dialog.XamlRoot = this.Content.XamlRoot;
                        dialog.PrimaryButtonText = "Confirmer";
                        dialog.CloseButtonText = "Annuler";
                        dialog.Title = "Connexion";
                        dialog.DefaultButton = ContentDialogButton.Close;

                        ContentDialogResult resultat = await dialog.ShowAsync();

                        if (SingletonConnexionAdherant.getInstance().GetConnexion())
                        {
                            navItemHeaderFooter.Content = "Bienvenu " + SingletonConnexionAdherant.getInstance().GetAdherent().NomPrenom;
                            navItemSeparatorUser.Visibility = Visibility.Visible;
                            nvConnexion.Content = "Se déconnecter";
                            nvAdmin.Content = "Administration";
                            SingletonConnexion.getInstance().seDeconnecter();
                            Admin();
                            mainFrame.Navigate(typeof(Page_InscriptionActivite));
                        }
                    }

                    // Si il y a un adhérent de connecter, une boite de dialogue de deconnexion va s'afficher.
                    else
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.XamlRoot = this.Content.XamlRoot;
                        dialog.Title = "Déconnexion";
                        dialog.PrimaryButtonText = "Oui";
                        dialog.CloseButtonText = "Non";
                        dialog.DefaultButton = ContentDialogButton.Primary;
                        dialog.Content = "Voulez-vous vraiment vous déconnecter?";

                        ContentDialogResult resultat = await dialog.ShowAsync();

                        if (resultat == ContentDialogResult.Primary)
                        {
                            navItemHeaderFooter.Content = null;
                            navItemSeparatorUser.Visibility = Visibility.Collapsed;
                            nvConnexion.Content = "Adhérant";
                            SingletonConnexionAdherant.getInstance().Deconnexion();
                        }

                    }

                    break;
                case "nvGestionAdherent":
                    mainFrame.Navigate(typeof(PageAdmin_GestionAdherent));
                    break;
                    ;
                case "nvAdmin":
                    /*
                    mainFrame.Navigate(typeof(Page_Connexion));
                    */
                    if (!SingletonConnexion.getInstance().isConnected())
                    {
                        Admin_DialogueConnexion dialog1 = new Admin_DialogueConnexion();

                        dialog1.XamlRoot = this.Content.XamlRoot;
                        dialog1.PrimaryButtonText = "Confirmer";
                        dialog1.CloseButtonText = "Annuler";
                        dialog1.Title = "Connexion";
                        dialog1.DefaultButton = ContentDialogButton.Close;

                        ContentDialogResult resultat1 = await dialog1.ShowAsync();

                        if (SingletonConnexion.getInstance().isConnected())
                        {
                            navItemHeaderFooter.Content = "Administrateur";
                            navItemSeparatorUser.Visibility = Visibility.Visible;
                            nvAdmin.Content = "Se déconnecter";
                            Admin();
                        }
                    }
                    else
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.XamlRoot = this.Content.XamlRoot;
                        dialog.Title = "Déconnexion";
                        dialog.PrimaryButtonText = "Oui";
                        dialog.CloseButtonText = "Non";
                        dialog.DefaultButton = ContentDialogButton.Primary;
                        dialog.Content = "Voulez-vous vraiment vous déconnecter?";

                        ContentDialogResult resultat = await dialog.ShowAsync();

                        if (resultat == ContentDialogResult.Primary)
                        {
                            navItemHeaderFooter.Content = null;
                            navItemSeparatorUser.Visibility = Visibility.Collapsed;
                            nvAdmin.Content = "Administration";
                            SingletonConnexion.getInstance().seDeconnecter();
                            Admin();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void Admin()
        {
            if (SingletonConnexion.getInstance().isConnected() is true)
            {
                nvGestionAdherent.Visibility = Visibility.Visible;
            }
            else
            {
                nvGestionAdherent.Visibility = Visibility.Collapsed;
            }
        }
    }
}
