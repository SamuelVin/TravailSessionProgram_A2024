using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

// Marco Petiquay / Créée le 2024-11-30 14:27 / Dernière modification:

namespace ProjetFinDeSession2024
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Admin_DialogueAjoutAdherent : ContentDialog
    {
        bool fermerDialogue = true;
        public Admin_DialogueAjoutAdherent()
        {
            this.InitializeComponent();
        }
        private void DpDateNaissance_Loaded(object sender, RoutedEventArgs e)
        {
            dpDateNaissance.SelectedDate = DateTimeOffset.Now;
            dpDateNaissance.MinYear = new DateTimeOffset(new DateTime(1960, 1, 1));
            dpDateNaissance.MaxYear = DateTimeOffset.Now;
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ResetErreurs();

            int annee = int.Parse(dpDateNaissance.SelectedDate.Value.Year.ToString());
            int mois = int.Parse(dpDateNaissance.SelectedDate.Value.Month.ToString());
            int jours = int.Parse(dpDateNaissance.SelectedDate.Value.Day.ToString());

            if (string.IsNullOrEmpty(tbxNom.Text.Trim()))
            {
                tblNomErreur.Text = "Veuillez entrer un nom";
                fermerDialogue = false;
            } else if (tbxNom.Text.Length > 30)
            {
                tblNomErreur.Text = "Le nombre de caractères ne doit pas dépasser 30";
                fermerDialogue = false;
            } else
            {
                fermerDialogue = true;
            }


            if (string.IsNullOrEmpty(tbxPrenom.Text.Trim()))
            {
                tblPrenomErreur.Text = "Veuillez entrer un prénom";
                fermerDialogue = false;
            }
            else if (tbxPrenom.Text.Length > 30)
            {
                tblPrenomErreur.Text = "Le nombre de caractères ne doit pas dépasser 30";
                fermerDialogue = false;
            } else
            {
                fermerDialogue = true;
            }

            if (DateValidation() is true)
            {
                fermerDialogue = true;
            } else
            {
                fermerDialogue = false;
            }

            if (string.IsNullOrEmpty(tbxAdresse.Text.Trim()))
            {
                tblAdresseErreur.Text = "Veuillez entrer une adresse";
                fermerDialogue = false;
            }
            else if (tbxAdresse.Text.Length > 150)
            {
                tblAdresseErreur.Text = "Le nombre de caractères ne doit pas dépasser 150";
                fermerDialogue = false;
            }
            else
            {
                fermerDialogue = true;
            }

            if (fermerDialogue is true) 
            { 
                Singleton_Adherents.getInstance().Ajouter(new Adherents(tbxNom.Text, tbxPrenom.Text, tbxAdresse.Text, new DateTime(annee, mois, jours)));
                
            }
                /*Singleton_Adherents.getInstance().Ajouter(new Adherents(tbxNom.Text, tbxPrenom.Text, tbxAdresse.Text, new DateTime(dpDateNaissance.SelectedDate.Value.Year,
                                                                                                                                   dpDateNaissance.SelectedDate.Value.Month,
                                                                                                                                   dpDateNaissance.SelectedDate.Value.Day)));
                */
        }

        public bool DateValidation()
        {
            DateTimeOffset date = DateTimeOffset.Now.AddYears(-18);
            if (dpDateNaissance.Date > date)
            {
                tblDateNaissanceErreur.Text = "L'adhérant doit avoir plus que 18 ans";
                return false;
            } else
            {
                return true;
            }
        }

        public void ResetErreurs()
        {
            tblNomErreur.Text = string.Empty;
            tblPrenomErreur.Text = string.Empty;
            tblAdresseErreur.Text = string.Empty;
            tblDateNaissanceErreur.Text = string.Empty;
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                if (fermerDialogue == false)
                    args.Cancel = true;
            }
            else
                args.Cancel = false;
        }
    }
}
