using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Text.RegularExpressions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinDeSession2024
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DialogueAdherentsConnexion : ContentDialog
    {
        bool fermerDialogue = true;

        public DialogueAdherentsConnexion()
        {
            this.InitializeComponent();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            if (string.IsNullOrEmpty(tbxIdentifiant.Text.Trim()))
            {
                tblIdentifiantErreur.Text = "Veuillez entrer votre identifiant";
                fermerDialogue = false;
            } else if (!(IdentifiantValidation(tbxIdentifiant.Text.Trim())))
            { 
                tblIdentifiantErreur.Text = "Veuillez entrer un identifiant valide";
                fermerDialogue = false;
            } else if (!(SingletonConnexionAdherant.getInstance().Connexion(tbxIdentifiant.Text.Trim())))
            {
                tblIdentifiantErreur.Text = "L'identifiant est introuvable. Veuillez réessayer.";
                fermerDialogue = false;
            }
            else
            {
                fermerDialogue = true;
            }

            if (fermerDialogue)
            {
                //this.Frame.Navigate(typeof(Page_InscriptionActivite));
                SingletonConnexionAdherant.getInstance().Connexion(tbxIdentifiant.Text.Trim());
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

        public bool IdentifiantValidation(string identifiant)
        {
            string identifiantFormat = @"^[A-Z]{2}-\d{4}-\d{3}$";
            return Regex.IsMatch(identifiant, identifiantFormat);
        }

    }
}
