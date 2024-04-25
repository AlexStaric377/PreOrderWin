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
using System.Drawing;

namespace PreOrderWin
{
    /// <summary>
    /// Логика взаимодействия для PaymentOrderWin.xaml
    /// </summary>
    public partial class PaymentOrderWin : Window
    {
       
        public static string TextKvitancii = "", TextKvitancii0 = "", TextKvitancii1 = "", TextKvitancii11 = "", TextKvitancii12 = "", TextKvitancii2 = "", TextKvitancii3 = "", TextKvitancii4 = "";
        public static int SizeFont = 11, IdHeadZakaz = 0;
        public static bool OrderPrintTouch = true, Oder_Ghage = true, Delete_Oder = true, BackButton = true, OrderTrue = true;
        public static float x = 0.0F, y = 0.0F, StrAddZagolov = 0.0F, StrAdd = 0.0F, StrAddItg = 0.0F, StrAddDisc = 0.0F, StrAddBank = 0.0F;

       


        //private void Test(object sender, MouseButtonEventArgs e)
        //{
        //    MainWindow.TimeClick++;
        //    // вызов процедуры печати чека
        //    PrintOrder();

        //    MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
        //    Arguments01.argument1 = "1";
        //    Arguments01.argument2 = "";
        //    Thread thread = new Thread(ControlWin.RunWinDykuy);
        //    thread.SetApartmentState(ApartmentState.STA);
        //    thread.IsBackground = true; // Фоновый поток
        //    thread.Start(Arguments01);

        //    Dykuy WinTake = new Dykuy();
        //    WinTake.Show();
        //    Close();
        //}

        public PaymentOrderWin()
        {
            InitializeComponent();
            MainWindow.NameSetWindow = "OrderWinPayment";
            ButtonBack.Visibility = Visibility.Visible;
            Button3.Visibility = Visibility.Visible;
            Button4.Visibility = Visibility.Visible;

            this.RowPayment.Height = new GridLength(2, GridUnitType.Pixel);
            if (MainWindow.OnOffInvalid == 1) this.StackTake.VerticalAlignment = VerticalAlignment.Bottom;
            else this.StackTake.VerticalAlignment = VerticalAlignment.Center;


            if (MainWindow.language_Key == 0)
            {
                Bank.Content = "                              БУДЬ ЛАСКА, " + Environment.NewLine + " НАТИСНІТЬ НА КНОПКУ 'Друквати квтанцію', " + Environment.NewLine +
                    "                      ВІЗЬМІТЬ КВИТАНЦІЮ " + Environment.NewLine +
                    "                     ТА ПРОЙДІТЬ ДО КАСИ " + Environment.NewLine + "              ДЛЯ ОПЛАТИ ТА ОТРИМАННЯ   " + Environment.NewLine + "                        ФІСКАЛЬНОГО ЧЕКА";
                CashOrder.Content = " ДОЧЕКАЙТЕСЬ ВИКОНАННЯ ЗАМОВЛЕННЯ, " + Environment.NewLine + "    ЙОГО НОМЕР ТА СТАН ГОТОВНОСТІ " + Environment.NewLine +
                    "        ВІДОБРАЖЕНІ НА МОНІТОРІ ЧЕРГИ";

                BackMain.Content = "НАЗАД";
                OrderDelete.Content = "  Скасувати" + Environment.NewLine + "замовлення";
                OpderEdit.Content = " Редагувати " + Environment.NewLine + "замовлення";
                OrderPrint.Content = "Друкувати " + Environment.NewLine + " квітанцію";

            }

            else
            {
                Bank.Content = "                  PLEASE " + Environment.NewLine + "        TAKE THE RECEIPT " + Environment.NewLine +
                    "  AND GO TO THE CASH DESK " + Environment.NewLine + "     TO PAY AND RECEIVE" + Environment.NewLine + "           A FISCAL CHECK";
                CashOrder.Content = " WAIT FOR THE ORDER TO BE FULFILLED, " + Environment.NewLine + "    ITS NUMBER AND READYNESS STATE " + Environment.NewLine +
                    "        DISPLAYED ON THE MONITOR";

                BackMain.Content = "BACK";
                OrderDelete.Content = "  Cancel" + Environment.NewLine + "  order";
                OpderEdit.Content = " Edit " + Environment.NewLine + " order";
                OrderPrint.Content = " Print " + Environment.NewLine + "a receipt";
            }

        }



        //private void ButtonDelete_Oder_TouchDown(object sender, TouchEventArgs e)
        //{
        //    Delete_Oder = false;
        //    Delete_Oder_Click();
        //}

        private void ButtonDelete_Oder_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            e.Handled = true;
            Delete_Oder_Click();
        }
        private void Delete_Oder_Click()
        {
            MainWindow.TimeClick++;
            // Удаление заказ из очереди отложеных чеков кассы
            ControlWin.ReversOrderTurnCheck();
            ControlWin.DeleteOrderPreview();
            
            MainWindow.SumZakazBasket = 0;
            MainWindow.SumaOrder = 0;
            MainWindow.SumDiscountBasket = 0;
            MainWindow.NumberHdZakaz = 0;
            MainWindow.TextContent = "";
            MainWindow.Kol = 0;
            this.Close();
        }

        private void ButtonEdit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            e.Handled = true;
            Ghage_Oder_Click();
        }

        //private void ButtonEdit_TouchDown(object sender, TouchEventArgs e)
        //{
        //    Oder_Ghage = false;
        //    Ghage_Oder_Click();
        //}


        private void Ghage_Oder_Click()
        {
            MainWindow.ChangeOrder = 1;
            MainWindow.TimeClick++;
            // Удаление заказ из очереди отложеных чеков кассы
            ControlWin.ReversOrderTurnCheck();
            MainWindow.IdGrupTov = 0;
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

        private void ButtonPrint_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if(OrderPrintTouch == true)
            e.Handled = true;
            ButtonBack.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
            Button4.Visibility = Visibility.Hidden;
            Button5.Visibility = Visibility.Hidden;
            Print_Order_Click();
        }

        private void ButtonPrint_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            ButtonBack.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
            Button4.Visibility = Visibility.Hidden;
            Print_Order_Click();
        }
    

        private  void Print_Order_Click()
        {
            MainWindow.TimeClick++;
            //   Подтверждение оплаты через nal в кассе.
            if (MainWindow.RunServer == true) ControlWin.PaymentBankOnOff("cash");
            if (MainWindow.ResultPayment == true)
            {
                // вызов процедуры печати чека
                PrintOrder();

                MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
                Arguments01.argument1 = "1";
                Arguments01.argument2 = "";
                Thread thread = new Thread(ControlWin.RunWinDykuy);
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true; // Фоновый поток
                thread.Start(Arguments01);
                Dykuy WinTake = new Dykuy();
                WinTake.Show();
            }
            else
            {
 
                // возврат на корректировку заказа для повторного выполнения действий
                Ghage_Oder_Click();
            }

            this.Close();
        }

        // печать квитанции по заказу.
        public static void PrintOrder()
        {
            
            TextKvitancii = "";
            TextKvitancii0 = "";
            TextKvitancii1 = "";
            TextKvitancii11 = "";
            TextKvitancii2 = "";
            TextKvitancii12 = "";
            TextKvitancii3 = "";
            TextKvitancii4 = "";
            StrAddZagolov = 0.0F;
            StrAdd = 0.0F;
            StrAddItg = 0.0F;
            StrAddBank = 0.0F;
            StrAddDisc = 0.0F;

            ControlWin.AddOreder();
          
            // создаем объект для печати.
            PrintDocument printDocument = new PrintDocument();
            //// Устанавливаем обработчик собтия печати
            printDocument.PrintPage += PrintPageHandler;
            //// Объект диалога настройки печати
            //// количество страниц, размер бумаги ...
            ////PrintDialog printDialog = new PrintDialog();
            //// печать документа
            printDocument.Print();

           

        }


        public static void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            float x = 0.0F;
            float y = 0.0F;
            SizeFont = 10;
            e.Graphics.DrawString(TextKvitancii, new Font("serif", SizeFont), System.Drawing.Brushes.Black, x, y);
            float y0 = 115.0F + StrAddZagolov;
            SizeFont = 18;
            e.Graphics.DrawString(TextKvitancii0, new Font("Arial", SizeFont), System.Drawing.Brushes.Black, x, y0);
            float y01 = 145.0F+ StrAddZagolov;
            SizeFont = 10;
            e.Graphics.DrawString(TextKvitancii1, new Font("Lucida Console", SizeFont), System.Drawing.Brushes.Black, x, y01);
            float y4 = 145.0F + StrAddZagolov + StrAdd;
            e.Graphics.DrawString(TextKvitancii11, new Font("Lucida Console", SizeFont), System.Drawing.Brushes.Black, x, y4);
            float y5 = 145.0F + StrAddZagolov + StrAdd + StrAddItg;
            if (MainWindow.CashBank == 2)
            { 
             e.Graphics.DrawString(TextKvitancii12, new Font("Lucida Console", SizeFont), System.Drawing.Brushes.Black, x, y5);          
            }
            float y1 = 155.0F + StrAddZagolov + StrAdd+ StrAddItg+ StrAddBank; 
            SizeFont = 18;
            e.Graphics.DrawString(TextKvitancii2, new Font("Arial", SizeFont), System.Drawing.Brushes.Black, x, y1);
            if (MainWindow.CashBank == 2) StrAddBank -= 15.0F;
            float y2 = 200.0F + StrAddZagolov + StrAdd + StrAddItg + StrAddBank; 
            SizeFont = 12;
            e.Graphics.DrawString(TextKvitancii3, new Font("seriffe", SizeFont), System.Drawing.Brushes.Black, x, y2);
            float y3 = 270.0F + StrAddZagolov + StrAdd + StrAddItg + StrAddDisc + StrAddBank;
            SizeFont = 12;
            e.Graphics.DrawString(TextKvitancii4, new Font("Arial Black", SizeFont), System.Drawing.Brushes.Black, x, y3);
        }



        //private void ButtonBack_TouchDown(object sender, TouchEventArgs e)
        //{
        //    BackButton = false;
        //    BackListZakaz();
        //}

        private void ButtonBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (BackButton == true)
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
     



        //// ОТладка наполнение базы данных заказами
        //private void print_check(object sender, MouseButtonEventArgs e)
        //{
        //    MainWindow.TimeClick++;

        //    // вызов процедуры печати чека
        //    ControlWin.AddOreder();

        //    MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
        //    Arguments01.argument1 = "1";
        //    Arguments01.argument2 = "";
        //    Thread thread = new Thread(ControlWin.RunWinDykuy);
        //    thread.SetApartmentState(ApartmentState.STA);
        //    thread.IsBackground = true; // Фоновый поток
        //    thread.Start(Arguments01);

        //    Dykuy WinTake = new Dykuy();
        //    WinTake.Show();
        //    Close();
        //}
    }

    // Подтверждение оплаты бонусами.
   

}
