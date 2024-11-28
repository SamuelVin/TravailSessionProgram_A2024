using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Org.BouncyCastle.Bcpg;
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
    public sealed partial class PageAdmin_AjoutActivite : Page
    {
        public PageAdmin_AjoutActivite()
        {
            this.InitializeComponent();
        }

        private void bt_ajouter_Click(object sender, RoutedEventArgs e)
        {
            bool essayer = true;

            txt_Erreur1.Text = "";
            txt_Erreur2.Text = "";
            txt_Erreur3.Text = "";
            txt_Erreur4.Text = "";
            txt_Erreur5.Text = "";
            txt_Reussi5.Text = "";

            // Champ - Nom activité
            if (string.IsNullOrWhiteSpace(txtbox_parametre1.Text)==true)
            {
                txt_Erreur1.Text = "Ce champ doit être remplit";
                essayer = false;
            }

            // Champ - Coût Organisation activité
            if (string.IsNullOrWhiteSpace(txtbox_parametre2.Text) == true)
            {
                txt_Erreur2.Text = "Ce champ doit être remplit";
                essayer = false;
            }
            else if (int.TryParse(txtbox_parametre2.Text, out _)==false)
            {
                txt_Erreur2.Text = "Ce champ doit être un nombre";
                essayer = false;
            }

            // Champ - Coût Vente activité
            if (string.IsNullOrWhiteSpace(txtbox_parametre3.Text) == true)
            {
                txt_Erreur3.Text = "Ce champ doit être remplit";
                essayer = false;
            }
            else if (int.TryParse(txtbox_parametre3.Text, out _)==false)
            {
                txt_Erreur3.Text = "Ce champ doit être un nombre";
                essayer = false;
            }

            // Champ - Catégorie activité
            if (string.IsNullOrWhiteSpace(txtbox_parametre4.Text) == true)
            {
                txt_Erreur4.Text = "Ce champ doit être remplit";
                essayer = false;
            }

            if (essayer == true)
            {
                bool operation_reussi = SingletonListe_Activite.getInstance().ajouterActivite(txtbox_parametre1.Text, txtbox_parametre4.Text, int.Parse(txtbox_parametre2.Text), int.Parse(txtbox_parametre3.Text));

                if (operation_reussi == true) 
                {
                    txt_Erreur1.Text = "";
                    txt_Erreur2.Text = "";
                    txt_Erreur3.Text = "";
                    txt_Erreur4.Text = "";
                    txt_Erreur5.Text = "";
                    txt_Reussi5.Text = "Opération réussi";

                    txtbox_parametre1.Text = "";
                    txtbox_parametre2.Text = "";
                    txtbox_parametre3.Text = "";
                    txtbox_parametre4.Text = "";
                }
                else
                {
                    txt_Erreur5.Text = "Erreur du système";
                    txt_Reussi5.Text = "";
                }
            }
            else
            {
                txt_Erreur5.Text = "Il y a une ou plusieurs erreurs";
            }
        }
    }
}
