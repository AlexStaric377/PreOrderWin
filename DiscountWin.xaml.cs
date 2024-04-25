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
using System.Windows.Shapes;

namespace PreOrderWin
{
    /// <summary>
    /// Логика взаимодействия для DiscountWin.xaml
    /// </summary>
    public partial class DiscountWin : Window
    {
        public static string DiscountBarCod = "";
        
        public DiscountWin()
        {
            InitializeComponent();
            MainWindow.NameSetWindow = "DiscountWin";

            BarcodeDiscaunt.Focus();

            if (MainWindow.language_Key == 0)
            {
                TextScan.Content = "ПРОСКАНУЙТЕ КАРТКУ ЛОЯЛЬНОСТІ";
                TextQR.Content = "KLO АБО QR КОД З ДОДАТКУ";
                TextBar.Content = "Піднесіть картку" + Environment.NewLine + "штрихкодом  до сканера" + Environment.NewLine + "на відстані 15-20 см.";
                TextNo.Content = "НЕМАЄ КАРТКИ?";
                TextAddKlo.Content = "        Завантажте" + Environment.NewLine + "      додаток KLO" + Environment.NewLine + "на свій телефон";
                TextKaca.Content = "або отримайте картку на касі" + Environment.NewLine + "              безкоштовно ";
                BackMain.Content = "НАЗАД";
                NextText.Content = "ПРОДОВЖИТИ БЕЗ КАРТКИ";
            }
            else
            {
                TextScan.Content = "LOOK FOR A LOYALTY CARD";
                TextQR.Content = "KLO OR QR CODE FROM THE APPENDIX";
                TextBar.Content = "Bring the card" + Environment.NewLine + "with the barcode to the scanner" + Environment.NewLine + "at a distance of 15-20 cm.";
                TextNo.Content = "NO CARD?";
                TextAddKlo.Content = "                    Download" + Environment.NewLine + "    the KLO application" + Environment.NewLine + "             to your phone";
                TextKaca.Content = "or get a card at the cash" + Environment.NewLine + "              for free";
                BackMain.Content = "BACK";
                NextText.Content = "CONTINUE WITHOUT THE CARD";
            }
        }

        private void Button_Back_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.ProgramProc = "";
            MainWindow.TimeClick++;
            if (MainWindow.OnOffInvalid == 1)
            {
                PriceInvalid WinTake = new PriceInvalid();
                WinTake.Show();
            }
            else
            {
                ListPriceWin WinTake = new ListPriceWin();
                WinTake.Show();
                
            }
            this.Close();
        }


        private void Button_Next_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            // тестовы запрос
            //CuponWin.LoadND2017();

            //ListZakazWin WinTake = new ListZakazWin();
            CuponWin WinTake = new CuponWin();
            WinTake.Show();
            this.Close();
        }

        private void Button_Next_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            CuponWin WinTake = new CuponWin();
            WinTake.Show();
            this.Close();
        }

        private void BarCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DiscountBarCod = BarcodeDiscaunt.Text;
                MainWindow.TimeClick++;
                CuponWin WinTake = new CuponWin();
                WinTake.Show();
                this.Close();
            }

            //this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

        }

        private void BarCod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DiscountBarCod = BarcodeDiscaunt.Text;
                MainWindow.TimeClick++;
            }

        }

       
    }
}
