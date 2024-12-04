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

namespace ProjetFinDeSession2024
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAdmin_AjoutSeance : Page
    {
        int id_activite = 0;

        public PageAdmin_AjoutSeance()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            id_activite = int.Parse(e.Parameter.ToString());
        }

        private void bt_ajouter_Click(object sender, RoutedEventArgs e)
        {
            bool essayer = true;
            string par1 = paramete1_date.Date.ToString().Substring(0,10);
            string par1_1 = paramete1_date.Date.ToString().Substring(0, 4);
            string par1_2 = paramete1_date.Date.ToString().Substring(5, 2);
            string par1_3 = paramete1_date.Date.ToString().Substring(8, 2);
            string par2 = paramete2_heure.Time.ToString().Substring(0, 8);
            string par2_1 = paramete2_heure.Time.ToString().Substring(0, 2);
            string par2_2 = paramete2_heure.Time.ToString().Substring(3, 2);

            string dNow_1 = DateOnly.FromDateTime(DateTime.Now).ToString().Substring(0, 10);
            string dNow_1_1 = DateOnly.FromDateTime(DateTime.Now).ToString().Substring(0, 4);
            string dNow_1_2 = DateOnly.FromDateTime(DateTime.Now).ToString().Substring(5, 2);
            string dNow_1_3 = DateOnly.FromDateTime(DateTime.Now).ToString().Substring(8, 2);
            string tNow_2 = TimeOnly.FromDateTime(DateTime.Now).ToString().Substring(0, 5);
            string tNow_2_1 = TimeOnly.FromDateTime(DateTime.Now).ToString().Substring(0, 2);
            string tNow_2_2 = TimeOnly.FromDateTime(DateTime.Now).ToString().Substring(3, 2);

            /*
            Debug.WriteLine(" = - = - = - = - = - = - = - = - = - = - = - = - = - = - = ");
            Debug.WriteLine(par1);
            Debug.WriteLine(par1_1);
            Debug.WriteLine(par1_2);
            Debug.WriteLine(par1_3);
            Debug.WriteLine(par2);
            Debug.WriteLine(par2_1);
            Debug.WriteLine(par2_2);
            Debug.WriteLine(" = - = - = - = - = - = - = - = - = - = - = - = - = - = - = ");
            Debug.WriteLine(dNow_1);
            Debug.WriteLine(dNow_1_1);
            Debug.WriteLine(dNow_1_2);
            Debug.WriteLine(dNow_1_3);
            Debug.WriteLine(tNow_2);
            Debug.WriteLine(tNow_2_1);
            Debug.WriteLine(tNow_2_2);
            Debug.WriteLine(" = - = - = - = - = - = - = - = - = - = - = - = - = - = - = ");
            */

            txt_Erreur1.Text = "";
            txt_Erreur2.Text = "";
            txt_Erreur5.Text = "";
            txt_Reussi5.Text = "";

            // Champ - Date Seance
            if (par1_1=="1600")
            {
                txt_Erreur1.Text = "Ce champ doit être remplit";
                essayer = false;
            }

            // Champ - Heure Seance
            if (paramete2_heure.Time.ToString().Substring(0, 1)=="-")
            {
                txt_Erreur2.Text = "Ce champ doit être remplit";
                essayer = false;
            }

            // Si DATE + HEURE > DATE + HEURE ACTUEL
            if (essayer==true)
            {
                int CanBeLower = 0; // 2 = false
                if (int.Parse(par1_1)>int.Parse(dNow_1_1))
                { CanBeLower = 1; }
                else if (int.Parse(par1_1)==int.Parse(dNow_1_1))
                { CanBeLower = 2; }
                else if (int.Parse(par1_1) <= int.Parse(dNow_1_1))
                { essayer = false; txt_Erreur1.Text = "La date sélectionnée doit être dans le futur"; }

                // Si la date est plus base que la date présente (mois)
                if (CanBeLower==2 && int.Parse(par1_2) < int.Parse(dNow_1_2))
                { essayer = false; txt_Erreur1.Text = "La date sélectionnée doit être dans le futur"; }
                // Si la date est plus base que la date présente (jour)
                if (essayer==true && CanBeLower==2 && int.Parse(par1_3) < int.Parse(dNow_1_3))
                { essayer = false; txt_Erreur1.Text = "La date sélectionnée doit être dans le futur"; }
                // Si l'heure est plus bas que l'heure présent (heure)
                if (essayer==true && CanBeLower==2 && int.Parse(par2_1) < int.Parse(tNow_2_1))
                { essayer = false; txt_Erreur2.Text = "La date sélectionnée doit être dans le futur"; }
                // Si l'heure est plus bas que l'heure présent (minute)
                if (essayer == true && CanBeLower == 2 && int.Parse(par2_2) < int.Parse(tNow_2_2))
                { essayer = false; txt_Erreur2.Text = "La date sélectionnée doit être dans le futur"; }
            }

            if (essayer == true)
            {
                DateOnly par_DateOnly = DateOnly.Parse(par1);
                TimeOnly par_TimeOnly = TimeOnly.Parse(par2);
                bool operation_reussi = SingletonListe_Seance.getInstance().ajouterSeance(par_DateOnly, par_TimeOnly);

                if (operation_reussi == true)
                {
                    txt_Erreur1.Text = "";
                    txt_Erreur2.Text = "";
                    txt_Erreur5.Text = "";
                    txt_Reussi5.Text = "Opération réussi";

                    //paramete1_date;
                    //paramete2_heure;
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
