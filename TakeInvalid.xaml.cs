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
    /// Логика взаимодействия для TakeInvalid.xaml
    /// </summary>
    public partial class TakeInvalid : Window
    {
        public TakeInvalid()
        {
            InitializeComponent();
            MainWindow.NameSetWindow = "TakeWin";

            /*if (MainWindow.OnOffInvalid == 1)*/ StackTake.VerticalAlignment = VerticalAlignment.Bottom;
            //else this.StackTake.VerticalAlignment = VerticalAlignment.Center;



            if (MainWindow.language_Key == 0)
            {
                BackMain.Content = "НАЗАД";
                Vibor.Content = "БУДЬ ЛАСКА, ОБЕРІТЬ";
                language.Content = "ОБЕРІТЬ МОВУ";
                Label.Content = "Замовлення" + Environment.NewLine + " в закладі";
                Label2.Content = "Замовлення" + Environment.NewLine + " з собою";
            }
            else
            {

                Vibor.Content = "PLEASE CHOOSE";
                language.Content = "CHOOSE LANGUAGE";
                Label.Content = "TAKE IN";
                Label2.Content = "TAKE OUT";
                BackMain.Content = "BACK";
            }

            //ControlWin.Setdbpgsql(nameof(TakeWin));
        }
        private void ButtonInvalidOff_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.OnOffInvalid = 0;
            MainWindow.TimeClick++;
            TakeWin WinTake = new TakeWin();
            WinTake.Show();
            this.Close();
        }

        private void BackButtonMainInvalid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.OnOffInvalid = 0;
            this.Close();
        }
 

        // заказ на вынос
        //private void Button_Away_PreviewTouchDown(object sender, TouchEventArgs e)
        //{
        //    MainWindow.RestHier = 2; // заказ на вынос
        //    PriceInvalid WinTake = new PriceInvalid();
        //    WinTake.Show();
        //}

        private void ButtonAwayInvalid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.RestHier = 2; // заказ на вынос
            PriceInvalid WinTake = new PriceInvalid();
            WinTake.Show();
            this.Close();
        }
        //private void Button_Away_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
 
        //}

        // заказ здесь    
        //private void Button_Here_PreviewTouchDown(object sender, TouchEventArgs e)
        //{
        //    MainWindow.TimeClick++;
        //    MainWindow.RestHier = 1;
        //    PriceInvalid WinTake = new PriceInvalid();
        //    WinTake.Show();
        //}
        private void ButtonInvalid_Here_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.RestHier = 1;
            PriceInvalid WinTake = new PriceInvalid();
            WinTake.Show();
            this.Close();
        }

  


        private void Button_English_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            Vibor.Content = "PLEASE CHOOSE";
            language.Content = "CHOOSE LANGUAGE";
            Label.Content = "TAKE IN";
            Label2.Content = "TAKE OUT";
            BackMain.Content = "BACK";
            MainWindow.language_Key = 1;
        }

        private void Button_Ukraine_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            Vibor.Content = "БУДЬ ЛАСКА ОБЕРІТЬ";
            language.Content = "ОБЕРІТЬ МОВУ";
            Label.Content = "Замовлення" + Environment.NewLine + " в закладі";
            Label2.Content = "Замовлення" + Environment.NewLine + " з собою";
            BackMain.Content = "НАЗАД";
            MainWindow.language_Key = 0;
        }

   
    }
}
