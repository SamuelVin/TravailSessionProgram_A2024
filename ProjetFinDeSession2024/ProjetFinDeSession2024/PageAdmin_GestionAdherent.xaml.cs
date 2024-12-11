using Google.Protobuf.WellKnownTypes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Microsoft;
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
    public sealed partial class PageAdmin_GestionAdherent : Page
    {
        private string adherantId;

        public PageAdmin_GestionAdherent()
        {
            this.InitializeComponent();
            lvListe.ItemsSource = Singleton_Adherents.getInstance().getListe();
            Singleton_Adherents.getInstance().RefreshBD();
        }

        private void DpDateNaissance_Loaded(object sender, RoutedEventArgs e)
        {
            dpDateNaissance.SelectedDate = DateTimeOffset.Now;
            dpDateNaissance.MinYear = new DateTimeOffset(new DateTime(1960, 1, 1));
            dpDateNaissance.MaxYear = DateTimeOffset.Now;
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
                Singleton_Adherents.getInstance().RefreshBD();
            //}

        }

        public async void btn_Supprimer_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;

            //DataContext représente l'élément parent
            Adherents _adherant = button.DataContext as Adherents;

            //permet de s'assurer que nous avons un élément sélectionné
            lvListe.SelectedItem = _adherant;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Suppression d'un adhérant";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = $"Voulez vous supprimer l'adherant: {_adherant.NomPrenom}?";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                Singleton_Adherents.getInstance().Supprimer(_adherant);
                Singleton_Adherents.getInstance().RefreshBD();
            }
            else
                lvListe.SelectedItem = null;
        }

        public void lvListe_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ResetErreurs();
            if (lvListe.SelectedItem is Adherents _adherant)
            {
                adherantId = _adherant.Identifiant;
                tbxNom.Text = _adherant.Nom;
                tbxPrenom.Text = _adherant.Prenom;
                tbxAdresse.Text = _adherant.Adresse;
                dpDateNaissance.Date = _adherant.DateNaissance;
            }
        }

        public void tbx_Recherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_Recherche.Text.Trim() != "")
                lvListe.ItemsSource = Singleton_Adherents.getInstance().Recherche(tbx_Recherche.Text);
            else
                Singleton_Adherents.getInstance().RefreshBD();
        }

        public void btn_ValiderModification_Click(object sender, RoutedEventArgs e)
        {
            ResetErreurs();
            int annee = int.Parse(dpDateNaissance.SelectedDate.Value.Year.ToString());
            int mois = int.Parse(dpDateNaissance.SelectedDate.Value.Month.ToString());
            int jours = int.Parse(dpDateNaissance.SelectedDate.Value.Day.ToString());

            if (Verification())
            {
                Singleton_Adherents.getInstance().Modifier(adherantId, new Adherents(tbxNom.Text, tbxPrenom.Text, tbxAdresse.Text, new DateTime(annee, mois, jours)));
                Singleton_Adherents.getInstance().RefreshBD();
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

            if (string.IsNullOrEmpty(tbxPrenom.Text.Trim()))
            {
                valide = false;
                tblPrenomErreur.Text = "Veuillez entrer un prénom";
            }
            else if (tbxPrenom.Text.Length > 30)
            {
                valide = false;
                tblPrenomErreur.Text = "Le nombre de caractères ne doit pas dépasser 30";
            }
            else
            {
                tblPrenomErreur.Text = string.Empty;
            }

            DateTimeOffset date = DateTimeOffset.Now.AddYears(-18);
            if (dpDateNaissance.Date > date)
            {
                tblDateNaissanceErreur.Text = "L'adhérant doit avoir plus que 18 ans";
                valide = false;
            }
            else
            {
                tblDateNaissanceErreur.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(tbxAdresse.Text.Trim()))
            {
                valide = false;
                tblAdresseErreur.Text = "Veuillez entrer une adresse";
            }
            else if (tbxAdresse.Text.Length > 150)
            {
                tblAdresseErreur.Text = "Le nombre de caractères ne doit pas dépasser 150";
                valide = false;
            }
            else
            {
                tblAdresseErreur.Text = string.Empty;
            }

            return valide;
        }
        public void ResetErreurs()
        {
            tblNomErreur.Text = string.Empty;
            tblPrenomErreur.Text = string.Empty;
            tblAdresseErreur.Text = string.Empty;
            tblDateNaissanceErreur.Text = string.Empty;
        }
    }
}
