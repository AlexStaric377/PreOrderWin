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
using System.Threading;
using System.Diagnostics;



namespace PreOrderWin
{
    /// <summary>
    /// Логика взаимодействия для TakeWin.xaml
    /// </summary>
    public partial class TakeWin : Window
    {
        public static bool ButtonHere = true, ButtonOut = true, ButtonUkraine=true, ButtonEnglish=true, BackButton=true, InvalidTake=true;
        public TakeWin()
        {
            InitializeComponent();
            MainWindow.NameSetWindow = "TakeWin";

            if (MainWindow.OnOffInvalid == 1) this.StackTake.VerticalAlignment = VerticalAlignment.Bottom;
            else this.StackTake.VerticalAlignment = VerticalAlignment.Center;


           
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
            PreOrderConection.ErorDebag("TakeWinSetdbpgsql ", 2);
            ControlWin.Setdbpgsql(nameof(TakeWin));
        }

        //private void ButtonInvalid_TouchDown(object sender, TouchEventArgs e)
        //{
        //    InvalidTake = false;
        //    Invalid_Click();
        //}

        //private void ButtonInvalid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    Invalid_Click();
        //}
        private void ButtonTakeInvOn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.OnOffInvalid = 1;
            MainWindow.RunListPrice = 1;
            TakeInvalid WinTake = new TakeInvalid();
            WinTake.Show();
            this.Close();
        }
        //private void Invalid_Click()
        //{
    

        //}

        //private void ButtonBack_TouchDown(object sender, TouchEventArgs e)
        //{
        //    //MessageBox.Show("OutTouchDown");
        //    BackButton = false;
        //    MainWindow.TimeClick++;
        //    this.Close();
        //}
        private void ButtonBackMain_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.ClickButtonOnOff = true;
            MainWindow.TimeClick++; this.Close(); 
        }
        //private void ButtonBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
            
        //}
 
       
        // заказ на вынос
        //private void ButtonOut_TouchDown(object sender, TouchEventArgs e)
        //{
        //    //MessageBox.Show("OutTouchDown");
        //    ButtonOut = false;
        //    ButtonOutClick();
 
        //}
        private void ButtonOutTake_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.RestHier = 2; // заказ на вынос
            ControlLoadBD();  
        }

        // заказ здесь
        //private void ButtonHere_TouchDown(object sender, TouchEventArgs e)
        //{
        //    //MessageBox.Show("HereTouchDown");
        //    ButtonHere = false;
        //    ButtonHereClick();
        //}

        //private void ButtonHere_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //   ButtonHereClick();
        //}
        private void ButtonHereTake_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.RestHier = 1;
            ControlLoadBD();  
        }

        public void ControlLoadBD()
        {
            MainWindow.RunListPrice = 1;
            string Text = " не завантажено. " + Environment.NewLine + "Завантажте дані з БД 1С ";
            
            if (MainWindow.db.TradingFloors.Count() == 0)
            {
                Text = "Довідник торгових підрозділів " + Text;
                ShowErrorMessage(Text);
                return;
            }
            if (MainWindow.db.Sprtovs.Count() == 0)
            {
                Text = "Довідник  товарів" + Text;
                ShowErrorMessage(Text);
                return;
            }
            if (MainWindow.db.Sprgrups.Count() == 0)
            {
                Text = "Довідник  груп товарів " + Text;
                ShowErrorMessage(Text);
                return;
            }

            if (MainWindow.LoadTovGrup == false) ControlTovGrup();
            if (MainWindow.LoadTovGrup == false) return;

            if (MainWindow.db.TradingFloors.Count() != 0 && MainWindow.db.Sprtovs.Count() != 0 && MainWindow.db.Sprgrups.Count() != 0)
            {
                ListPriceWin WinPayment = new ListPriceWin();
                WinPayment.Show();
                this.Close();
            }
        }
        
        public void ShowErrorMessage(string Text = "БД не завантажена")
        {
            PreOrderConection.ErorDebag(Text, 3, 0, (PreOrderConection.StruErrorDebag)null);
            MessageError NewOrder = new MessageError(Text, 2);
            NewOrder.Show();
            Thread.Sleep(8000);
            this.Close();
        }

        public void ControlTovGrup()
        {
            
            string Text = "";
            var TovGrup = MainWindow.db.Sprtovs.ToList();
            foreach (Sprtov pole in TovGrup)
            {
                int KodGrup = pole.KodGrup;
                int TovKod = pole.KodTov;
                string TovName = pole.NameTovEN;
                if (MainWindow.db.Sprgrups.Where(p => p.KodGrup == KodGrup).FirstOrDefault() == null)
                {
                    Text = "У товарі: "+ TovName+ "Код: " + TovKod.ToString()+ " нема группи в довіднику. Код  "+ KodGrup.ToString()+  Environment.NewLine + "Завантажте дані з БД 1С ";
                    ShowErrorMessage(Text);
                    return;
                }

            }
            MainWindow.LoadTovGrup = true;

        }

        //private void ButtonUkraine_TouchDown(object sender, TouchEventArgs e)
        //{
        //    e.Handled = true;
        //    ButtonUkraine = false;
        //    Ukraine_Click();
        //}

 

        private void ButtonUkraine_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Ukraine_Click();
        }

        

        private void Ukraine_Click()
        {
            //e.Handled = true;
            MainWindow.TimeClick++;
            Vibor.Content = "БУДЬ ЛАСКА ОБЕРІТЬ";
            language.Content = "ОБЕРІТЬ МОВУ";
            Label.Content = "Замовлення" + Environment.NewLine + " в закладі";
            Label2.Content = "Замовлення" + Environment.NewLine + " з собою";
            BackMain.Content = "НАЗАД";
            MainWindow.language_Key = 0;
        }

        

        //private void ButtonEnglish_TouchDown(object sender, TouchEventArgs e)
        //{
        //    ButtonEnglish = false;
        //    English_Click();
        //}

        private void ButtonEnglish_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            English_Click();
        }

        private void English_Click()
        {
            
            MainWindow.TimeClick++;
            Vibor.Content = "PLEASE CHOOSE";
            language.Content = "CHOOSE LANGUAGE";
            Label.Content = "TAKE IN";
            Label2.Content = "TAKE OUT";
            BackMain.Content = "BACK";
            MainWindow.language_Key = 1;
        }

       
    }
}


