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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

// Samuel Vinette / Créer le 2024-11-28 15:53 / Dernière modification: 2024-11-28 15:53

namespace ProjetFinDeSession2024
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_InscriptionSeance : Page
    {
        int id_activite = 0;
        ObservableCollection<Seance> liste = new ObservableCollection<Seance>() { };
        public Page_InscriptionSeance()
        {
            this.InitializeComponent();
            liste = SingletonListe_Seance.getInstance().getListe_DeActivite(0);

            if (SingletonConnexion.getInstance().isAdmin() == true)
            {
                stk_connecter.Visibility = Visibility.Visible;
                bt_ajouter.Visibility = Visibility.Visible;
                bt_supprimer.Visibility = Visibility.Visible;
            }
            if (SingletonConnexion.getInstance().isAdmin() == false && SingletonConnexion.getInstance().isConnected() == true)
            {
                bt_seance.Visibility = Visibility.Visible;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            liste = SingletonListe_Seance.getInstance().getListe_DeActivite(int.Parse(e.Parameter.ToString()));
            id_activite = int.Parse(e.Parameter.ToString());
            if (liste.Count==0)
            {
                txtblock_aucuneseance.Visibility = Visibility.Visible;
            }
        }

        private void bt_supprimer_Click(object sender, RoutedEventArgs e)
        {
            int selected_index = lv_list.SelectedIndex;
            bool operation_reussi = SingletonListe_Seance.getInstance().supprimerSeance(selected_index);
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
            this.Frame.Navigate(typeof(PageAdmin_AjoutSeance), id_activite);
        }

        private void bt_seance_Click(object sender, RoutedEventArgs e)
        {
            bool operation_reussi = SingletonConnexion.getInstance().inscriptionSeance5();
            Debug.WriteLine("op 1 - " + operation_reussi);
            if (operation_reussi == true && lv_list.SelectedIndex != -1)
            {
                // Find the GridViewItem corresponding to the selected index
                var selectedGridViewItem = lv_list.ContainerFromIndex(lv_list.SelectedIndex) as GridViewItem;

                // Check if the GridViewItem is valid
                if (selectedGridViewItem != null)
                {
                    // Find the StackPanel inside the GridViewItem (this is your item template)
                    var selectedStackPanel = selectedGridViewItem.ContentTemplateRoot as StackPanel;

                    if (selectedStackPanel != null)
                    {
                        // Now find the TextBlock named "the_id_of_seance" inside the StackPanel
                        var idTextBlock = selectedStackPanel?.FindName("the_id_of_seance") as TextBlock;

                        Debug.WriteLine("a " + selectedStackPanel);
                        Debug.WriteLine("b " + idTextBlock);

                        // Check if the TextBlock was found
                        if (idTextBlock != null)
                        {
                            string idText = idTextBlock.Text;
                            int seanceId = int.Parse(idText);  // Assuming the TextBlock contains an integer

                            // Perform your operation
                            operation_reussi = SingletonConnexion.getInstance().inscrire(seanceId);
                            Debug.WriteLine("op 2 - " + operation_reussi + " id=" + seanceId);
                        }
                        else
                        {
                            Debug.WriteLine("TextBlock not found.");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("StackPanel not found.");
                    }
                }
                Debug.WriteLine("op 3 - Done");

                /*
                var selectedStackPanel = lv_list.ContainerFromIndex(lv_list.SelectedIndex) as FrameworkElement;
                //var selectedStackPanel = lv_list.ContainerFromIndex(lv_list.SelectedIndex) as StackPanel;
                var idTextBlock = selectedStackPanel?.FindName("the_id_of_seance") as TextBlock;

                Debug.WriteLine("a " + selectedStackPanel);
                Debug.WriteLine("b " + idTextBlock);

                if (idTextBlock != null)
                {
                    string idText = idTextBlock.Text;
                    int seanceId = int.Parse(idText);
                    operation_reussi = SingletonConnexion.getInstance().inscrire(seanceId);
                    Debug.WriteLine("op 2 - " + operation_reussi);
                }
                Debug.WriteLine("op 3 - Done");
                */
            }
        }
    }
}
