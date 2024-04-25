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
    /// Логика взаимодействия для ErrorMessage.xaml
    /// </summary>
    public partial class ErrorMessage : Window
    {
        public static int RepeatOnOff = 0, Cancel_Oder=0;
        public static bool TouchButton = true, RepeatButton = true;
        public ErrorMessage()
        {
            InitializeComponent();
            MainWindow.NameError = "ErrorMessage";
            Top = 600;
            Left = 200;
            if (MainWindow.OnOffInvalid == 1) Top = 1050;
            TextConect.Text = ControlWin.TextWindows.Trim().Length != 0 ? ControlWin.TextWindows : TextConect.Text;
            Cancel_Oder = 0;
            RepeatOnOff = 0;
            TouchButton = true; RepeatButton = true;
        }

      
        private void ButtonClose_TouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            CloseWinError();
        }
        private void ButtonClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            CloseWinError();
        }
        

        private void CloseWinError()
        {
            MainWindow.TimeClick++;
            PaymentBankWin.ErrorKodVoid = 3;
            ControlWin.DeleteOrderPreview();
            this.Close();

        }

        private void ButtonVoid_TouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            TouchButton = false;
            ButtonVoid();
        }

        //private void ButtonVoid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if(TouchButton == true)ButtonVoid();
        //}

        private void ButtonVoid()
        {
            MainWindow.TimeClick++;
            if (Cancel_Oder == 0)
            {
                //Ok_But.FontSize = 16;
                //Ok_But.Height = 46;
                Ok_But.Content = MainWindow.language_Key == 1 ? " Pay " + Environment.NewLine + "in cash" : " Платити " + Environment.NewLine + "готівкою";
                //Close_But.FontSize = 16;
                //Close_But.Height = 46;
                Close_But.Content = MainWindow.language_Key == 1 ? " Delete " + Environment.NewLine + "order" : " Скасувати " + Environment.NewLine + "замовлення";
                Cancel_Oder = 1;
                RepeatOnOff = 1;
                PaymentBankWin.ErrorKodVoid = 0;
            }
            else
            {
                PaymentBankWin.ErrorKodVoid = 0;
                //ControlWin.DeleteOrderPreview();
                this.Close();
            }
        }

        private void ButtonVoid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            TouchButton = false;
            ButtonVoid();
        }

        private void ButtonRepeat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            RepeatButton = false;
            ButtonRepeat();
        }

       

        private void ButtonRepeat_TouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            RepeatButton = false;
            ButtonRepeat();
        }

        //private void ButtonRepeat_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if(RepeatButton == true) ButtonRepeat();
        //}

        private void ButtonRepeat()
        {
            MainWindow.TimeClick++;
            Cancel_Oder = 0;
            // повторить
            if (RepeatOnOff == 0)
            {
                
                PaymentBankWin.ErrorKodVoid = 1;
                this.Close();
                return;
            }
            // " Платити готівкою";
            if (RepeatOnOff == 1)
            {

                MainWindow.CashBank = 1;
                PaymentBankWin.ErrorKodVoid = 2;
                this.Close();
                return;

            }

        }
       

    }
}
