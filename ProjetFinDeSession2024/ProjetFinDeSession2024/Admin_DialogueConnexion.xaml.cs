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
    public sealed partial class Admin_DialogueConnexion : ContentDialog
    {
        bool fermerDialogue = true;
        public Admin_DialogueConnexion()
        {
            this.InitializeComponent();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            bool operation_reussi = SingletonConnexion.getInstance().seConnecter(txtbox_utilisateur.Text, txtbox_motdepasse.Password);
            if (operation_reussi is not true)
            {
                txtbox_motdepasse.Password = "";
                fermerDialogue = false;
            }

            if (string.IsNullOrEmpty(txtbox_utilisateur.Text.Trim()))
            {
                tbl_UtilisateurErreur.Text = "Veuillez entrez votre nom d'utilisateur";
                fermerDialogue = false;
            } else
            {
                tbl_UtilisateurErreur.Text = string.Empty;
                fermerDialogue = true;
            }

            if (string.IsNullOrEmpty(txtbox_motdepasse.Password.Trim()))
            {
                tbl_MotdepasseErreur.Text = "Veuillez entrez votre mot de passe";
                fermerDialogue = false;
            } else
            {
                tbl_MotdepasseErreur.Text = string.Empty;
                fermerDialogue = true;
            }
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
