using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medris_Vlad_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        private DoughnutMachine myDoughnutMachine;
        private int mRaisedGlazed;
        private int mRaisedSugar;
        private int mFilledLemon;
        private int mFilledChocolate;
        private int mFilledVanilla;

        public MainWindow(){
            InitializeComponent();
        }

        private void FilledItemsShow_Click(object sender, RoutedEventArgs e){
            string mesaj;
            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            mesaj = SelectedItem.Header.ToString() + " doughnuts are being cooked right now";
            this.Title = mesaj;
        }

        private void FilledItems_Click(object sender, RoutedEventArgs e){
            string DoughnutFlavour;

            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            DoughnutFlavour = SelectedItem.Header.ToString();
            Enum.TryParse(DoughnutFlavour, out DoughnutType myFlavour);
            myDoughnutMachine.MakeDoughnuts(myFlavour);
        }

        private void txtQuantity_KeyPress(object sender, System.Windows.Input.KeyEventArgs e){
            if (!(e.Key >= System.Windows.Input.Key.D0 && e.Key <= System.Windows.Input.Key.D9))
            {
                MessageBox.Show("Numai cifre se pot introduce", "Input Error", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, RoutedEventArgs e){
            this.Close();
        }

        private void stopToolStripMenuItem_Click(object sender, RoutedEventArgs e){
            myDoughnutMachine.Enabled = false;
        }

        private void DoughnutCompleteHandler(){
            switch (myDoughnutMachine.Flavor){
                case DoughnutType.Glazed:
                    mRaisedGlazed++;
                    txtGlazedRaised.Text = mRaisedGlazed.ToString();
                    break;
                case DoughnutType.Sugar:
                    mRaisedSugar++;
                    txtSugarRaised.Text = mRaisedSugar.ToString();
                    break;
                case DoughnutType.Lemon:
                    mFilledLemon++;
                    txtLemonFilled.Text = mFilledLemon.ToString();
                    break;
                case DoughnutType.Chocolate:
                    mFilledChocolate++;
                    txtChocolateFilled.Text = mFilledChocolate.ToString();
                    break;
                case DoughnutType.Vanilla:
                    mFilledVanilla++;
                    txtVanillaFilled.Text = mFilledVanilla.ToString();
                    break;
            }
            myDoughnutMachine.DoughnutComplete += new DoughnutMachine.DoughnutCompleteDelegate(DoughnutCompleteHandler);
        }

        private void glazedToolStripMenuItem_Click(object sender, RoutedEventArgs e){
            glazedToolStripMenuItem.IsChecked = true;
            sugarToolStripMenuItem.IsChecked = false;
            myDoughnutMachine.MakeDoughnuts(DoughnutType.Glazed);
        }

        private void sugarToolStripMenuItem_Click(object sender, RoutedEventArgs e){
            glazedToolStripMenuItem.IsChecked = false;
            sugarToolStripMenuItem.IsChecked = true;
            myDoughnutMachine.MakeDoughnuts(DoughnutType.Sugar);
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e){
            myDoughnutMachine = new DoughnutMachine();
        }
    }
}
