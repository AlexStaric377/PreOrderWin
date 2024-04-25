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
/// Многопоточность
using System.Threading;
using System.Drawing.Printing;

namespace PreOrderWin
{
    /// <summary>
    /// Логика взаимодействия для PaymentBankWin.xaml
    /// </summary>
    public partial class PaymentBankWin : Window
    {
        public static long NumberChecka = 0;
        public static int ConectBank = 0, ErrorKodVoid = 0;
        public static bool NextPrintCheck = true, Payment=true, ButtonPayment=true, Oder_Ghage = true, Delete_Oder=true, BackButton=true;
        
        public PaymentBankWin()
        {
            InitializeComponent();
            MainWindow.NameSetWindow = "PaymentBankWin";
            ButtonBack.Visibility  = Visibility.Visible;
            Button3.Visibility = Visibility.Visible;
            Button4.Visibility = Visibility.Visible;
            Button5.Visibility = Visibility.Visible;
            NextPrintCheck = true; Payment = true;
            this.RowPayment.Height = new GridLength(2, GridUnitType.Pixel);
            if (MainWindow.OnOffInvalid == 1) this.StackTake.VerticalAlignment = VerticalAlignment.Bottom;
            else this.StackTake.VerticalAlignment = VerticalAlignment.Center;
            
            if (MainWindow.language_Key == 0)
            {
                Bank.Content = "                         БУДЬ ЛАСКА, " + Environment.NewLine + " НАТИСНІТЬ НА КНОПКУ 'СПЛАТИТИ' ТА " + Environment.NewLine +
                    "             СЛІДУЙТЕ ІНСТРУКЦІЯМ " + Environment.NewLine +
                    "      НА БАНКІВСЬКОМУ ТЕРМІНАЛІ " + Environment.NewLine + "                 ВІЗЬМІТЬ КВИТАНЦІЮ " + Environment.NewLine +
                    "               ТА ПРОЙДІТЬ ДО КАСИ " + Environment.NewLine + " ДЛЯ ОТРИМАННЯ ФІСКАЛЬНОГО ЧЕКА";
                CashOrder.Content = " ДОЧЕКАЙТЕСЬ ВИКОНАННЯ ЗАМОВЛЕННЯ, " + Environment.NewLine + "    ЙОГО НОМЕР ТА СТАН ГОТОВНОСТІ " + Environment.NewLine + 
                    "        ВІДОБРАЖЕНІ НА МОНІТОРІ ЧЕРГИ";

                BackMain.Content = "НАЗАД";
                OrderDelete.Content = "  Скасувати" + Environment.NewLine + "замовлення";
                OpderEdit.Content = " Редагувати " + Environment.NewLine + "замовлення";
                OrderPrint.Content = "Сплатити" ;

            }

            else
            { 
                 Bank.Content = "                         PLEASE " + Environment.NewLine + " CLICK ON THE 'PAY' BUTTON AND " + Environment.NewLine +
                    "      FOLLOW THE INSTRUCTIONS  " + Environment.NewLine +
                      "      AT THE BANKING TERMINAL. " + Environment.NewLine + "                 GET A RECEIPT " + Environment.NewLine +
                      "        AND GO TO THE CASH REGISTER " + Environment.NewLine + " TO RECEIVE A FISCAL CHECK";
                CashOrder.Content = " WAIT FOR THE ORDER TO BE FULFILLED, " + Environment.NewLine + "    ITS NUMBER AND READYNESS STATE " + Environment.NewLine +
                    "        DISPLAYED ON THE MONITOR";

                BackMain.Content = "BACK";
                OrderDelete.Content = "  Cancel" + Environment.NewLine + "  order";
                OpderEdit.Content = " Edit " + Environment.NewLine + " order";
                OrderPrint.Content = " Print " + Environment.NewLine + "a receipt";          
            }

 

        }

 
        private void ButtonDelete_Oder_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
          
            e.Handled = true;
            Delete_Oder_Click();
        }
        private void Delete_Oder_Click()
        {
            MainWindow.TimeClick++;
            // Удаление заказ из очереди отложеных чеков кассы
            if (MainWindow.RunServer == true) ControlWin.ReversOrderTurnCheck();
            ControlWin.DeleteOrderPreview();
            MainWindow.CountOrder = 0;
            MainWindow.SumZakazBasket = 0;
            MainWindow.SumaOrder = 0;
            MainWindow.SumDiscountBasket = 0;
            MainWindow.NumberHdZakaz = 0;
            MainWindow.TextContent = "";
            MainWindow.TimeClick++;
            MainWindow.Kol = 0;
            this.Close();
        }

 

        private void ButtonEdit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
 
            Ghage_Oder_Click();
        }


        private void Ghage_Oder_Click()
        {
            MainWindow.ChangeOrder = 1;
            MainWindow.TimeClick++;
            // Удаление заказ из очереди отложеных чеков кассы
            MainWindow.IdGrupTov = 0;
            if (MainWindow.RunServer == true) ControlWin.ReversOrderTurnCheck();
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

  

        private void ButtonPayment_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

 
            e.Handled = true;
            Payment = true;
            MainWindow.ResultPayment = true;
            Button5.Visibility = Visibility.Hidden;
            ButtonBack.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
            Button4.Visibility = Visibility.Hidden;
            string TextWindows = "Шановний замовник! " + Environment.NewLine + "Будь ласка зачекайте. Здійснюється звернення до банківсьго терміналу для підтвердження оплати.";
            MessageError NewOrder = new MessageError(TextWindows, 2, 7);
            NewOrder.ShowDialog();
            PaymentBank();

        }

        // вызов процедуры работы с банкоматом
        public void PaymentBank()
        {
            MainWindow.TimeClick++;
            PaymentOrderWin.TextKvitancii = "";
            PaymentOrderWin.TextKvitancii0 = "";
            PaymentOrderWin.TextKvitancii1 = "";
            PaymentOrderWin.TextKvitancii11 = "";
            PaymentOrderWin.TextKvitancii2 = "";
            PaymentOrderWin.TextKvitancii12 = "";
            PaymentOrderWin.TextKvitancii3 = "";
            PaymentOrderWin.TextKvitancii4 = "";
            PaymentOrderWin.StrAdd = 0.0F;
            PaymentOrderWin.StrAddItg = 0.0F;
            PaymentOrderWin.StrAddBank = 0.0F;
            PaymentOrderWin.StrAddDisc = 0.0F;

            while (Payment == true)
            {
                if (MainWindow.RunTerminal == true) ControlWin.RunBankTerminal();
                if (NextPrintCheck == true)
                {
                    if (MainWindow.BankTerminalOnOff == true && MainWindow.RunServer == true && MainWindow.RunTerminal == true) ControlWin.PaymentBankOnOff("bank");
                    if (MainWindow.ResultPayment == true)
                    { 
                        // печать квитанции по заказу.
                        ControlWin.AddOreder();
                        // создаем объект для печати.
                        PrintDocument printDocument = new PrintDocument();
                        // Устанавливаем обработчик собтия печати
                        printDocument.PrintPage += PaymentOrderWin.PrintPageHandler;
                        // Объект диалога настройки печати
                        // количество страниц, размер бумаги ...
                        //PrintDialog printDialog = new PrintDialog();

                        // печать документа
                        printDocument.Print();
                        RunDykuy();
                        Payment = false;
                    
                    }
                }
                else
                {
                    Payment = true;
                    ErrorMessage WinError = new ErrorMessage();
                    //WinError.Owner = this;
                    WinError.ShowDialog();
                    // скасувати замовлення
                    if (ErrorKodVoid == 0)
                    {
                        Payment = false;
                        //MainWindow.ChangeOrder = 1;
                        MainWindow.TimeClick++;
                        Delete_Oder_Click();
                        //if (MainWindow.OnOffInvalid == 1)
                        //{
                        //    PriceInvalid WinTake = new PriceInvalid();
                        //    WinTake.Show();
                        //}
                        //else
                        //{
                        //    ListPriceWin WinTake = new ListPriceWin();
                        //    WinTake.Show();

                        //}
                        this.Close();
                    }
                    // " Платити готівкою";
                    if (ErrorKodVoid == 2)
                    {
                        PaymentOrderWin.PrintOrder();
                        Payment = false;
                        RunDykuy();
                       
                    }
                    if (ErrorKodVoid == 3)
                    {
                        Payment = false;
                        this.Close();
                    }
                    if (ErrorKodVoid == 1)
                    {
                        Payment = false;
                        ButtonBack.Visibility = Visibility.Visible;
                        Button3.Visibility = Visibility.Visible;
                        Button4.Visibility = Visibility.Visible;
                        Button5.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        public void RunDykuy()
        {
            MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
            Arguments01.argument1 = "1";
            Arguments01.argument2 = "";
            Thread thread = new Thread(ControlWin.RunWinDykuy);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true; // Фоновый поток
            thread.Start(Arguments01);
            Dykuy WinTake = new Dykuy();
            WinTake.Show();
            this.Close();
        }

  
        private void ButtonBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            BackListZakaz();
        }

        private void BackListZakaz()
        {
            MainWindow.TimeClick++;
            ListZakazWin WinTake = new ListZakazWin();
            WinTake.Show();
            this.Close();
        }

    }
}
