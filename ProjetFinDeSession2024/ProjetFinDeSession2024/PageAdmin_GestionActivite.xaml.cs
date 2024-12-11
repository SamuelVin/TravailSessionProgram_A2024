using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinDeSession2024
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAdmin_GestionActivite : Page
    {
        private string adherantId;

        public PageAdmin_GestionActivite()
        {
            this.InitializeComponent();
            lvListe.ItemsSource = SingletonListe_Activite.getInstance().getListe();
        }

        public async void btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            //if (SingletonConnexion.getInstance().isConnected() is true)
            //{
            Admin_DialogueAjoutAdherent dialog1 = new Admin_DialogueAjoutAdherent();

            dialog1.XamlRoot = this.Content.XamlRoot;
            dialog1.PrimaryButtonText = "Valider";
            dialog1.CloseButtonText = "Annuler";
            dialog1.Title = "Ajout d'un adhérant";
            dialog1.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat1 = await dialog1.ShowAsync();
            //}

        }

        public async void btn_Supprimer_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;

            //DataContext représente l'élément parent
            Activite _activite = button.DataContext as Activite;

            //permet de s'assurer que nous avons un élément sélectionné
            lvListe.SelectedItem = _activite;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Suppression d'un adhérant";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = $"Voulez vous supprimer l'activite: {_activite.Nom}?";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                SingletonListe_Activite.getInstance().supprimerActivite(lvListe.SelectedIndex);
            }
            else
                lvListe.SelectedItem = null;
        }

        public void lvListe_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ResetErreurs();
            if (lvListe.SelectedItem is Activite _activite)
            {
                tbxNom.Text = _activite.Nom;
                tbxCategorie.Text = _activite.Categorie;
                tbxOrganisation.Text = _activite.Cout_Organization.ToString();
                tbxVente.Text = _activite.Cout_Vente.ToString();
            }
        }

        public void tbx_Recherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_Recherche.Text.Trim() != "")
                lvListe.ItemsSource = SingletonListe_Activite.getInstance().Recherche(tbx_Recherche.Text);
            else
                lvListe.ItemsSource = SingletonListe_Activite.getInstance().getListe();
        }

        public void btn_ValiderModification_Click(object sender, RoutedEventArgs e)
        {
            ResetErreurs();

            if (Verification())
            {
                SingletonListe_Activite.getInstance().Modifier(new Activite(tbxNom.Text, tbxCategorie.Text, int.Parse(tbxOrganisation.Text), int.Parse(tbxVente.Text)));
            }
        }

        public async void btn_Export_Click(object sender, RoutedEventArgs e)
        {

            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(MainWindow.Current);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);
            picker.SuggestedFileName = "AdherantsListe";
            picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".csv" });

            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            List<Adherents> liste = new List<Adherents>();

            await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.ToString()), Windows.Storage.Streams.UnicodeEncoding.Utf8);

        }

        public bool Verification()
        {
            bool valide = true;

            if (string.IsNullOrEmpty(tbxNom.Text.Trim()))
            {
                valide = false;
                tblNomErreur.Text = "Veuillez entrer un nom";
            }
            else if (tbxNom.Text.Length > 30)
            {
                valide = false;
                tblNomErreur.Text = "Le nombre de caractères ne doit pas dépasser 30";
            }
            else
            {
                tblNomErreur.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(tbxCategorie.Text.Trim()))
            {
                valide = false;
                tblCategorieErreur.Text = "Veuillez entrer un prénom";
            }
            else if (tbxCategorie.Text.Length > 30)
            {
                valide = false;
                tblCategorieErreur.Text = "Le nombre de caractères ne doit pas dépasser 30";
            }
            else
            {
                tblCategorieErreur.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(tbxOrganisation.Text.Trim()))
            {
                valide = false;
                tblCoutOrgErreur.Text = "Veuillez entrer un nombre";
            }
            else if (int.TryParse(tbxOrganisation.Text, out _) == false)
            {
                tblCoutOrgErreur.Text = "Ce champ n'est pas un nombre";
                valide = false;
            }
            else
            {
                tblCoutOrgErreur.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(tbxVente.Text.Trim()))
            {
                valide = false;
                tblCoutVenteErreur.Text = "Veuillez entrer un nombre";
            }
            else if (int.TryParse(tbxVente.Text, out _) == false)
            {
                tblCoutVenteErreur.Text = "Ce champ n'est pas un nombre";
                valide = false;
            }
            else
            {
                tblCoutVenteErreur.Text = string.Empty;
            }

            return valide;
        }
        public void ResetErreurs()
        {
            tblNomErreur.Text = string.Empty;
            tblCategorieErreur.Text = string.Empty;
            tblCoutOrgErreur.Text = string.Empty;
            tblCoutVenteErreur.Text = string.Empty;
        }
    }
}
