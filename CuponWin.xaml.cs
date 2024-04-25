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
using System.IO;
using System.Threading;
using System.Text.Json;
using System.Net;
using System.Data;
using System.Web;


namespace PreOrderWin
{
    /// <summary>
    /// Логика взаимодействия для CuponWin.xaml
    /// </summary>
    public partial class CuponWin : Window
    {

        public static int NumberPozition = 0;
        public static string StrokaKoda = "", BarCodCupon = "";
        public static  bool OrderTrue = true;
        //public static List<ListTovar> Goods = new List<ListTovar>(1);

        public CuponWin()
        {
            InitializeComponent();
            MainWindow.NameSetWindow = "CuponWin";
            ReadConfDiscount();
            Bar.Content = DiscountWin.DiscountBarCod;

            BarcodeCupon.Focus();

            // Отладка штрихкода. Удалить в рабочей версии
            //------------------------------------------------------
            if (MainWindow.BarCod.Length != 0) Bar.Content = MainWindow.BarCod;



            //if (MainWindow.BonusValue != 0) Bonus.Content = Convert.ToString(MainWindow.BonusValue);
            ErrorKod.Visibility = Visibility.Hidden;
            NumberPozition = 0;

            if (MainWindow.language_Key == 0)
            {
                //OstBonus.Content = "ЗАЛИШОК БОНУСІВ:";
                Cuponscan.Content = "ПРОСКАНУЙТЕ";
                Cupon.Content = "КУПОН";
                ErrorKod.Content = "КУПОН НЕ ДІЙСНИЙ";
                KodCupon.Content = "АБО ВВЕДІТЬ КОД КУПОНА";
                BackMain.Content = "РЕДАГУВАТИ";
                Next1.Content = "ПРОДОВЖИТИ З КУПОНАМИ";
                NextText.Content = "ПРОДОВЖИТИ БЕЗ КУПОНА";
                TextBar.Content = "Піднесіть купон" + Environment.NewLine + "штрихкодом  до сканера" + Environment.NewLine + "на відстані 15-20 см.";

            }
            else
            {
                //OstBonus.Content = "BONUS BALANCE:";
                Cuponscan.Content = "SCAN";
                Cupon.Content = "COUPON";
                ErrorKod.Content = "THE COUPON IS NOT VALID";
                KodCupon.Content = "OR ENTER THE COUPON CODE";
                BackMain.Content = "EDIT ORDERS";
                Next1.Content = "CONTINUE WITH COUPONS";
                NextText.Content = "CONTINUE WITHOUT COUPON";
                TextBar.Content = "Bring the coupon" + Environment.NewLine + "with the barcode to the scanner" + Environment.NewLine + "at a distance of 15-20 cm.";


            }
            Headerlist.Visibility = Visibility.Hidden;
            Dele_0.Visibility = Visibility.Hidden;
            Dele_1.Visibility = Visibility.Hidden;
            Dele_2.Visibility = Visibility.Hidden;
            Dele_3.Visibility = Visibility.Hidden;
            Dele_4.Visibility = Visibility.Hidden;
            Dele_5.Visibility = Visibility.Hidden;
            Dele_6.Visibility = Visibility.Hidden;
            Dele_7.Visibility = Visibility.Hidden;
            Dele_8.Visibility = Visibility.Hidden;
            Dele_9.Visibility = Visibility.Hidden;
           
        }

        private void ButtonCupon_Next_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.CountListCupon = 0;
            CuponNextZakaz();
        }

        private void ButtonCupon_Next_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            MainWindow.CountListCupon = 0;
            CuponNextZakaz();

        }

        private void ButtonCupon_List_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            CuponNextZakaz();
        }


        public void CuponNextZakaz()
        {
            MainWindow.TimeClick++;
            if (MainWindow.RunServer == true)  LoadND2017(DiscountWin.DiscountBarCod, "");
            if (OrderTrue == true)
            {
                ListZakazWin WinTake = new ListZakazWin();
                WinTake.Show();
            }
            this.Close();
        }

        private void ButtonCupon_Back_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
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

        private void One_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("1");
        }

        private void To_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("2");
        }

        private void Three_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("3");
        }

        private void Four_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("4");
        }

        private void Five_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("5");
        }

        private void Six_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("6");
        }

        private void Seven_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("7");
        }

        private void Eight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("8");
        }

        private void Nine_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("9");
        }

        private void Zero_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            ValueWrite("0");
        }

        private void Del_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            DeleteValueCod();
            ErrorKod.Visibility = Visibility.Hidden;

        }

        public void DeleteValueCod()
        {
            ValueOne.Content = "";
            ValueTo.Content = "";
            ValueThree.Content = "";
            ValueFour.Content = "";
            ValueFive.Content = "";
            ValueSix.Content = "";
            ValueSeven.Content = "";
            ValueEight.Content = "";
            ValueNine.Content = "";
            ValueZero.Content = "";
            Value11.Content = "";
            Value12.Content = "";
            Value13.Content = "";
            NumberPozition = 0;
            StrokaKoda = "";
        }

        private void DelCupValue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            ErrorKod.Visibility = Visibility.Hidden;
 
            if (NumberPozition-1 == 0) ValueOne.Content = "";
            if (NumberPozition - 1 == 1) ValueTo.Content = "";
            if (NumberPozition - 1 == 2) ValueThree.Content = "";
            if (NumberPozition - 1 == 3) ValueFour.Content = "";
            if (NumberPozition - 1 == 4) ValueFive.Content = "";
            if (NumberPozition - 1 == 5) ValueSix.Content = "";
            if (NumberPozition - 1 == 6) ValueSeven.Content = "";
            if (NumberPozition - 1 == 7) ValueEight.Content = "";
            if (NumberPozition - 1 == 8) ValueNine.Content = "";
            if (NumberPozition - 1 == 9) ValueZero.Content = "";
            if (NumberPozition - 1 == 10) Value11.Content = "";
            if (NumberPozition - 1 == 11) Value12.Content = "";
            if (NumberPozition - 1 == 12) Value13.Content = "";
            StrokaKoda = StrokaKoda.Substring(0,NumberPozition);
            NumberPozition--;
            MainWindow.TimeClick++;
 
        }

        public void CheckKod()
        {

            int Chet =  Convert.ToInt16(StrokaKoda.Substring(10, 1)) + Convert.ToInt16(StrokaKoda.Substring(8, 1))+ Convert.ToInt16(StrokaKoda.Substring(6, 1)) 
                 + Convert.ToInt16(StrokaKoda.Substring(4, 1)) + Convert.ToInt16(StrokaKoda.Substring(2, 1)) + Convert.ToInt16(StrokaKoda.Substring(0, 1));

            int Nechet  = Convert.ToInt16(StrokaKoda.Substring(11, 1)) + Convert.ToInt16(StrokaKoda.Substring(9, 1)) + Convert.ToInt16(StrokaKoda.Substring(7, 1))
                             + Convert.ToInt16(StrokaKoda.Substring(5, 1)) + Convert.ToInt16(StrokaKoda.Substring(3, 1)) + Convert.ToInt16(StrokaKoda.Substring(1, 1));
            string  Digit10 = Convert.ToString(Nechet * 3 + Chet);
            string CheckDigit = Convert.ToString(10-Convert.ToInt16(Digit10.Substring(Digit10.Length-1, 1)));
            if (Convert.ToInt16(Digit10.Substring(Digit10.Length - 1, 1)) == 0) CheckDigit = "0";
            NumberPozition = 13;
            if (StrokaKoda.Substring(12, 1) != CheckDigit)
            { 
                ErrorKod.Visibility = Visibility.Visible;
                return;
            }

            if (MainWindow.CountListCupon < 10)
            {
                for (int i = 0; i <= MainWindow.CountListCupon; i++)
                {
                    if (MainWindow.ListCuponLoylti[i] == StrokaKoda)
                    {
                        string TextWindows = "Шановний замовник! " + Environment.NewLine + " Купон з таким кодом вже введено: " + StrokaKoda;
                        MessageError NewOrder = new MessageError(TextWindows, 0, 300);
                        NewOrder.Show();
                        Thread.Sleep(7000);
                        NewOrder.Close();
                        BarcodeCupon.Text = "";
                        NumberPozition = 0;
                        StrokaKoda = "";
                        return;
                    }
                }

                MainWindow.ListCuponLoylti[MainWindow.CountListCupon] = BarCodCupon = StrokaKoda; // e.Key.ToString();

                if (MainWindow.CountListCupon == 0) { cupon0.Content = StrokaKoda; Dele_0.Visibility = Visibility.Visible; } 
                if (MainWindow.CountListCupon == 1) { cupon1.Content = StrokaKoda; Dele_1.Visibility = Visibility.Visible; }
                if (MainWindow.CountListCupon == 2) { cupon2.Content = StrokaKoda; Dele_2.Visibility = Visibility.Visible; }
                if (MainWindow.CountListCupon == 3) { cupon3.Content = StrokaKoda; Dele_3.Visibility = Visibility.Visible; }
                if (MainWindow.CountListCupon == 4) { cupon4.Content = StrokaKoda; Dele_4.Visibility = Visibility.Visible; }
                if (MainWindow.CountListCupon == 5) { cupon5.Content = StrokaKoda; Dele_5.Visibility = Visibility.Visible; }
                if (MainWindow.CountListCupon == 6) { cupon6.Content = StrokaKoda; Dele_6.Visibility = Visibility.Visible; }
                if (MainWindow.CountListCupon == 7) { cupon7.Content = StrokaKoda; Dele_7.Visibility = Visibility.Visible; }
                if (MainWindow.CountListCupon == 8) { cupon8.Content = StrokaKoda; Dele_8.Visibility = Visibility.Visible; }
                if (MainWindow.CountListCupon == 9) { cupon9.Content = StrokaKoda; Dele_9.Visibility = Visibility.Visible; }
                MainWindow.CountListCupon++;
                DeleteValueCod();
            }
            if(MainWindow.CountListCupon>0) Headerlist.Visibility = Visibility.Visible;
            //LoadND2017(DiscountWin.DiscountBarCod, StrokaKoda);
            //ListZakazWin WinTake = new ListZakazWin();
            //WinTake.Show();
            //this.Close();
        }

        public void ValueWrite(string SetValue)
        {
            if (NumberPozition == 13) return;
            ErrorKod.Visibility = Visibility.Hidden;
            if (NumberPozition == 0) ValueOne.Content = SetValue;
            if (NumberPozition == 1) ValueTo.Content = SetValue;
            if (NumberPozition == 2) ValueThree.Content = SetValue;
            if (NumberPozition == 3) ValueFour.Content = SetValue;
            if (NumberPozition == 4) ValueFive.Content = SetValue;
            if (NumberPozition == 5) ValueSix.Content = SetValue;
            if (NumberPozition == 6) ValueSeven.Content = SetValue;
            if (NumberPozition == 7) ValueEight.Content = SetValue;
            if (NumberPozition == 8) ValueNine.Content = SetValue;
            if (NumberPozition == 9) ValueZero.Content = SetValue;
            if (NumberPozition == 10) Value11.Content = SetValue;
            if (NumberPozition == 11) Value12.Content = SetValue;
            if (NumberPozition == 12) Value13.Content = SetValue;
            NumberPozition++;
            StrokaKoda += SetValue;
            if (NumberPozition == 13) CheckKod();

        }


        private void BarCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (MainWindow.CountListCupon < 10)
                {
                    for (int i = 0; i <= MainWindow.CountListCupon; i++)
                    {
                        if (MainWindow.ListCuponLoylti[i] == BarcodeCupon.Text)
                        {
                            string TextWindows = "Шановний замовник! " + Environment.NewLine + " Купон з таким кодом вже введено: "+ BarcodeCupon.Text;
                            MessageError NewOrder = new MessageError(TextWindows,0, 300);
                            NewOrder.Show();
                            Thread.Sleep(7000);
                            NewOrder.Close();
                            BarcodeCupon.Text = "";
                            NumberPozition = 0;
                            StrokaKoda = "";
                            return;
                        } 
                    }
                    MainWindow.ListCuponLoylti[MainWindow.CountListCupon] = BarCodCupon= BarcodeCupon.Text; // e.Key.ToString();

                    if (MainWindow.CountListCupon == 0) { cupon0.Content = BarcodeCupon.Text; Dele_0.Visibility = Visibility.Visible; }
                    if (MainWindow.CountListCupon == 1) { cupon1.Content = BarcodeCupon.Text; Dele_1.Visibility = Visibility.Visible; }
                    if (MainWindow.CountListCupon == 2) { cupon2.Content = BarcodeCupon.Text; Dele_2.Visibility = Visibility.Visible; }
                    if (MainWindow.CountListCupon == 3) { cupon3.Content = BarcodeCupon.Text; Dele_3.Visibility = Visibility.Visible; }
                    if (MainWindow.CountListCupon == 4) { cupon4.Content = BarcodeCupon.Text; Dele_4.Visibility = Visibility.Visible; }
                    if (MainWindow.CountListCupon == 5) { cupon5.Content = BarcodeCupon.Text; Dele_5.Visibility = Visibility.Visible; }
                    if (MainWindow.CountListCupon == 6) { cupon6.Content = BarcodeCupon.Text; Dele_6.Visibility = Visibility.Visible; }
                    if (MainWindow.CountListCupon == 7) { cupon7.Content = BarcodeCupon.Text; Dele_7.Visibility = Visibility.Visible; }
                    if (MainWindow.CountListCupon == 8) { cupon8.Content = BarcodeCupon.Text; Dele_8.Visibility = Visibility.Visible; }
                    if (MainWindow.CountListCupon == 9) { cupon9.Content = BarcodeCupon.Text; Dele_9.Visibility = Visibility.Visible; }
                    MainWindow.CountListCupon++;
                    BarcodeCupon.Text = "";
                    DeleteValueCod();
                }

                if (MainWindow.CountListCupon > 0) Headerlist.Visibility = Visibility.Visible;


                //// Обработка ШК в программе лояльности.
                //LoadND2017(DiscountWin.DiscountBarCod, BarCodCupon);
                //CuponWin CuponWin_Link = ControlWin.LinkMainWindow("CuponWin");
                //MainWindow.TimeClick++;
                //ListZakazWin WinTake = new ListZakazWin();
                //WinTake.Show();
                //CuponWin_Link.Close();
            }
  


        }

        private void ButtonClose0_TouchDown(object sender, TouchEventArgs e)
        {
            cupon0.Content = "";
            Dele_0.Visibility = Visibility.Hidden;
            DeletCupon(0);
        }
        private void ButtonClose1_TouchDown(object sender, TouchEventArgs e)
        {
            cupon1.Content = "";
            Dele_1.Visibility = Visibility.Hidden;
            DeletCupon(1);
        }
        private void ButtonClose2_TouchDown(object sender, TouchEventArgs e)
        {
            cupon2.Content = "";
            Dele_2.Visibility = Visibility.Hidden;
            DeletCupon(2);
        }

        private void ButtonClose3_TouchDown(object sender, TouchEventArgs e)
        {
            cupon3.Content = "";
            Dele_3.Visibility = Visibility.Hidden;
            DeletCupon(3);
        }

        private void ButtonClose4_TouchDown(object sender, TouchEventArgs e)
        {
            cupon4.Content = "";
            Dele_4.Visibility = Visibility.Hidden;
            DeletCupon(4);
        }

        private void ButtonClose5_TouchDown(object sender, TouchEventArgs e)
        {
            cupon5.Content = "";
            Dele_5.Visibility = Visibility.Hidden;
            DeletCupon(5);
        }

        private void ButtonClose6_TouchDown(object sender, TouchEventArgs e)
        {
            cupon6.Content = "";
            Dele_6.Visibility = Visibility.Hidden;
            DeletCupon(6);
        }

        private void ButtonClose7_TouchDown(object sender, TouchEventArgs e)
        {
            cupon7.Content = "";
            Dele_7.Visibility = Visibility.Hidden;
            DeletCupon(7);
        }

        private void ButtonClose8_TouchDown(object sender, TouchEventArgs e)
        {
            cupon8.Content = "";
            Dele_8.Visibility = Visibility.Hidden;
            DeletCupon(8);
        }

        private void ButtonClose9_TouchDown(object sender, TouchEventArgs e)
        {
            cupon9.Content = "";
            Dele_9.Visibility = Visibility.Hidden;
            DeletCupon(9);
        }

        

        public void DeletCupon(int index = 0)
        {
            for (int i = index; i <= MainWindow.CountListCupon; i++)
            { 
                MainWindow.ListCuponLoylti[i] = MainWindow.ListCuponLoylti[i + 1];
                if (i == 0 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon0.Content = MainWindow.ListCuponLoylti[i]; Dele_0.Visibility = Visibility.Visible; }
                if (i == 1 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon1.Content = MainWindow.ListCuponLoylti[i]; Dele_1.Visibility = Visibility.Visible; }
                if (i == 2 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon2.Content = MainWindow.ListCuponLoylti[i]; Dele_2.Visibility = Visibility.Visible; }
                if (i == 3 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon3.Content = MainWindow.ListCuponLoylti[i]; Dele_3.Visibility = Visibility.Visible; }
                if (i == 4 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon4.Content = MainWindow.ListCuponLoylti[i]; Dele_4.Visibility = Visibility.Visible; }
                if (i == 5 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon5.Content = MainWindow.ListCuponLoylti[i]; Dele_5.Visibility = Visibility.Visible; }
                if (i == 6 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon6.Content = MainWindow.ListCuponLoylti[i]; Dele_6.Visibility = Visibility.Visible; }
                if (i == 7 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon7.Content = MainWindow.ListCuponLoylti[i]; Dele_7.Visibility = Visibility.Visible; }
                if (i == 8 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon8.Content = MainWindow.ListCuponLoylti[i]; Dele_8.Visibility = Visibility.Visible; }
                if (i == 9 && MainWindow.ListCuponLoylti[i].Length != 0) { cupon9.Content = MainWindow.ListCuponLoylti[i]; Dele_9.Visibility = Visibility.Visible; }
            }
            if (MainWindow.CountListCupon > 0)
            { 
                for (int ind = MainWindow.CountListCupon-1; ind < 10; ind++)
                { 
                    MainWindow.ListCuponLoylti[ind] = "";
                    if (ind == 0 ) { cupon0.Content = ""; Dele_0.Visibility = Visibility.Hidden; }
                    if (ind == 1 ) { cupon1.Content = ""; Dele_1.Visibility = Visibility.Hidden; }
                    if (ind == 2 ) { cupon2.Content = ""; Dele_2.Visibility = Visibility.Hidden; }
                    if (ind == 3 ) { cupon3.Content = ""; Dele_3.Visibility = Visibility.Hidden; }
                    if (ind == 4 ) { cupon4.Content = ""; Dele_4.Visibility = Visibility.Hidden; }
                    if (ind == 5 ) { cupon5.Content = ""; Dele_5.Visibility = Visibility.Hidden; }
                    if (ind == 6 ) { cupon6.Content = ""; Dele_6.Visibility = Visibility.Hidden; }
                    if (ind == 7 ) { cupon7.Content = ""; Dele_7.Visibility = Visibility.Hidden; }
                    if (ind == 8 ) { cupon8.Content = ""; Dele_8.Visibility = Visibility.Hidden; }
                    if (ind == 9 ) { cupon9.Content = ""; Dele_9.Visibility = Visibility.Hidden; }

                }
            }
            
            MainWindow.CountListCupon--;
            if (MainWindow.CountListCupon == 0) Headerlist.Visibility = Visibility.Hidden;
        }

        private void ButtonClose1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon1.Content = "";
            Dele_1.Visibility = Visibility.Hidden;
            DeletCupon(1);
        }

        private void ButtonClose2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon2.Content = "";
            Dele_2.Visibility = Visibility.Hidden;
            DeletCupon(2);
        }

        private void ButtonClose3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon3.Content = "";
            Dele_3.Visibility = Visibility.Hidden;
            DeletCupon(3);
        }

        private void ButtonClose4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon4.Content = "";
            Dele_4.Visibility = Visibility.Hidden;
            DeletCupon(4);
        }

        private void ButtonClose5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon5.Content = "";
            Dele_5.Visibility = Visibility.Hidden;
            DeletCupon(5);
        }

        private void ButtonClose6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon6.Content = "";
            Dele_6.Visibility = Visibility.Hidden;
            DeletCupon(6);
        }

        private void ButtonClose7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon7.Content = "";
            Dele_7.Visibility = Visibility.Hidden;
            DeletCupon(7);
        }

        private void ButtonClose8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon8.Content = "";
            Dele_8.Visibility = Visibility.Hidden;
            DeletCupon(8);
        }

        private void ButtonClose9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon9.Content = "";
            Dele_9.Visibility = Visibility.Hidden;
            DeletCupon(9);
        }

        private void ButtonClose0_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cupon0.Content = "";
            Dele_0.Visibility = Visibility.Hidden;
            DeletCupon(0);
        }

        public static void LoadND2017(string BarCodDiscaount = "", string BarCodCupon = "")
        {

            
            PreOrderConection.ErorDebag("Запрос к веб сереверу " + Environment.NewLine + " Создание заказа и запрос о наличии бонусов", 2);
            string TextWindows = "Шановний замовник! Зачекайте. " + Environment.NewLine + " Здійснюється запис Вашого замовлення до черги відкладених замовлень.";
            if (BarCodDiscaount.Trim().Length != 0) TextWindows += Environment.NewLine + " а також зверненя до системы лояльності." + Environment.NewLine + "Перевіряється нявність бонусів та знижок за Вашою карткою. ";
            MessageError NewOrder = new MessageError(TextWindows, 0);
            NewOrder.Show();
            //Thread.Sleep(10000);
            MainWindow.ResponseFromServer = "";
            string json = "",jsonadd="",jsonprice = "",jsonamount="",ordersuma ="", ErrorText="",TextCode = "";
            OrderTrue = true;
            int CurrentIdTov = 0,IndexTov =0, IndexTovErr = 0; ;
            MainWindow.SumSkidka = 0.0m;
            MainWindow.UrlAdresServer = MainWindow.TcpUrlServer + ":" + MainWindow.PortUrlServer + "/api/orders/add"; //"http://192.168.1.11:8082/api/orders/add";
            int CountOrder = MainWindow.db.OrederPreviews.Count();
            int[] IndKodTov = new int[CountOrder];
            foreach (OrederPreview ListPreviewOrder in MainWindow.db.OrederPreviews.ToList<OrederPreview>())
            {

                if (json.Length>0) json += ",";
                ListTovar StrokaOrder = new ListTovar { code = Convert.ToString(ListPreviewOrder.Discount), qty = ListPreviewOrder.Quantity, price = ListPreviewOrder.PriceTov, amount = ListPreviewOrder.SumTov };
                CurrentIdTov = ListPreviewOrder.Idtov;
                IndKodTov[IndexTov] = ListPreviewOrder.Idtov;
                jsonadd = JsonSerializer.Serialize<ListTovar>(StrokaOrder);
                //decimal Drob= StrokaOrder.price - Math.Truncate(StrokaOrder.price);
                jsonprice =  jsonadd.Substring(jsonadd.IndexOf("price\":")+7, jsonadd.IndexOf(",\"amount") -(jsonadd.IndexOf("price\":") + 7))+((StrokaOrder.price - Math.Truncate(StrokaOrder.price)) != 0? "" :".0");
                jsonadd = jsonadd.Replace(Convert.ToString(StrokaOrder.price), jsonprice); 
                if (StrokaOrder.qty > 1)
                {
                    jsonamount = jsonadd.Substring(jsonadd.IndexOf("amount\":") + 8, jsonadd.IndexOf("}") - (jsonadd.IndexOf("amount\":") + 8)) + ".0";
                    jsonadd = jsonadd.Replace(Convert.ToString(StrokaOrder.amount), jsonamount);
                }
                json += jsonadd;
                IndexTov++;
            }
            ordersuma = Convert.ToString(MainWindow.SumaOrder).Replace(",",".");
            MainWindow.CmdStrokaServer = "{\"cmd\":\"add_order\",\"data\":{\"total\":" + ordersuma + ",\"goods\":[" + json + "]}} ";
            ControlWin.LoadUrlServer(MainWindow.UrlAdresServer, MainWindow.CmdStrokaServer);
            if (MainWindow.ReturnCodeServer == 0)
            {
                // Ошибка в строке 4-  Не найден товар из запроса,  5- Не совпадает цена товара из запроса, 3,2,1
                if (MainWindow.ResponseFromServer.Contains("{\"code\":0") == false )
                {
                    NewOrder.Close();

                    //   4-  Не найден товар из запроса , 5- Не совпадает цена товара из запроса
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":4") == true || MainWindow.ResponseFromServer.Contains("{\"code\":5") == true
                        || MainWindow.ResponseFromServer.Contains("{\"code\":6") == true)
                    {
                        // Один Товар  заказ удаляется.                            
                        if (CountOrder == 1) ControlWin.DeleteOrderPreview();
                        else 
                        {
                       
                            IndexTovErr = Convert.ToInt16(MainWindow.ResponseFromServer.Substring(MainWindow.ResponseFromServer.IndexOf("{\"e_code\":")+10, 
                                (MainWindow.ResponseFromServer.IndexOf(",\"e_msg")- (MainWindow.ResponseFromServer.IndexOf("{\"e_code\":")+10))));

                            MainWindow.db.OrederPreviews.RemoveRange(MainWindow.db.OrederPreviews.Where(c => c.Idtov == IndKodTov[IndexTovErr]));
                            MainWindow.db.SaveChanges();
                          
                        }
                        // Закрыть товар для продажи с невенным кодом.
                        if (MainWindow.ResponseFromServer.Contains("{\"code\":4") == true || MainWindow.ResponseFromServer.Contains("{\"code\":5") == true)
                        { 
                            var ErrorTovar = MainWindow.db.Sprtovs.Where(p => p.KodTov == IndKodTov[IndexTovErr]).FirstOrDefault();
                            ErrorTovar.SaleStop = true;
                            MainWindow.db.SaveChanges();                       
                        }

                        
                        if (MainWindow.ResponseFromServer.Contains("{\"code\":4") == true) TextCode = " Замовлений товар не знайдено у довіднику каси.";
                        if (MainWindow.ResponseFromServer.Contains("{\"code\":5") == true) TextCode = " Ціна замовленого товару не співпадає з ціною у довіднику каси.";
                        if (MainWindow.ResponseFromServer.Contains("{\"code\":6") == true) TextCode = " Зміна на касі не відкрита. Зверніться до Адміністратора.";

                        ErrorText = "Шановний замовник! Вибачте. " + Environment.NewLine + "Додати замовлення до черги відкладених замовлень на касі неможливо. "
                            + TextCode +Environment.NewLine + " Товар вилучено з замовлення.";
                        OrderTrue = false;
                        PreOrderConection.ErorDebag(TextCode + Environment.NewLine+ MainWindow.CmdStrokaServer + Environment.NewLine + MainWindow.ResponseFromServer, 2);
                    }

                   
                    // 1- Ошибка выполнения 3- Ошибка в формате запроса (нет обязательного поля данных или ошибочный формат)
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":1") == true || MainWindow.ResponseFromServer.Contains("{\"code\":3") == true)
                    {
                        ErrorText = "Шановний замовник! Вибачте." + Environment.NewLine + "Помилка виконання запиту до каси."
                        + Environment.NewLine + "Бадь ласка натисніть ще раз на кнопку ГОТОВО.";
                        OrderTrue = false;
                        PreOrderConection.ErorDebag(TextCode + Environment.NewLine + MainWindow.CmdStrokaServer + Environment.NewLine + MainWindow.ResponseFromServer, 2);

                    }


                }
                else
                {
                    //{ "result":{ "code":0,"msg":"ok"},"data":{ "order_id":11,"order_code":11,"goods":[{ "code":"27757","qty":1.00,"price":40.00,"amount":40.00,"discount":0.00}],"total":40.00,"payment":{ "type":"","total":0.00,"PAN":"","RRN":"","auth_code":"","slip_no":"","terminal_id":"","merchant_id":"","card_holder":"","payment_system":""} } }

                    // Успех добавления заказа в очередь отложенных чеков. Запрос на наличие бонусов и скидок
                    MainWindow.IdOrderND2017 = Convert.ToUInt16(MainWindow.ResponseFromServer.Substring(MainWindow.ResponseFromServer.IndexOf("order_id\":") + 10, MainWindow.ResponseFromServer.IndexOf(",\"order_code\"") - (MainWindow.ResponseFromServer.IndexOf("order_id\":") + 10)));
                    MainWindow.ApiOrdersAdd = true;
                    MainWindow.CmdStrokaServer = "";
                    int Time = 7000;
                    //MainWindow.SumaFinish = MainWindow.SumaOrder;
                    //ControlWin.PaymentBankOnOff("cash");
                    //ControlWin.ReversOrderTurnCheck();

                    string CuponArry = "[]";
                    bool DiscountTrue = true;
                    
                    BarCodDiscaount = BarCodDiscaount.Trim().Length != 0 ? BarCodDiscaount : MainWindow.CodTexnologLoyalnost;
                    //BarCodCupon = "3333333333333";
                    if (BarCodDiscaount.Trim().Length != 0)
                    {
                        string LineError = "";
                        // Проверка наличия купонов 	КИОСК КЛО применить лояльную систему 
                        MainWindow.UrlAdresServer = MainWindow.TcpUrlServer + ":" + MainWindow.PortUrlServer + "/api/klo/use_loyalty"; // "http://192.168.1.11:8082/api/klo/use_loyalty";

                        // Строка без купонов если MainWindow.CountListCupon = 0 иначе формирование списка введенных купонов
                        if (MainWindow.CountListCupon > 0)
                        {
                            CuponArry = "[";
                            for (int i = 0; i < MainWindow.CountListCupon; i++)CuponArry += "\""+ MainWindow.ListCuponLoylti[i]+ "\",";
                            CuponArry += "]";
                        }
                        string AddCupon =  "\",\"coupons\":"+ CuponArry;
                        MainWindow.CmdStrokaServer = "{\"cmd\":\"use_loyalty\",\"data\": {\"order_id\":" + MainWindow.IdOrderND2017.ToString() + ",\"loyalty\": {\"card\":\"" + BarCodDiscaount + AddCupon +"}}}";
                        ControlWin.LoadUrlServer(MainWindow.UrlAdresServer, MainWindow.CmdStrokaServer);
 
                        if (MainWindow.ReturnCodeServer == 0)
                        {
                            if (MainWindow.ResponseFromServer.Contains("{\"code\":0") == true)
                            {
                                NewOrder.Close();
                                // Анализ наличия бонусов.
                                //{ "result":{ "code":0,"msg":"ok"},"data":{ "order_id":31,"order_code":31,"goods":[{ "code":"27757","qty":1.00,"price":40.00,"amount":40.00,"discount":0.00}],"total":40.00,"loyalty":{ "card":"9010009026414","coupons":[],"server_msg":"","bonuses":0.00},"payment":{ "type":"","total":0.00,"PAN":"","RRN":"","auth_code":"","slip_no":"","terminal_id":"","merchant_id":"","card_holder":"","payment_system":""} } }
                                // обработка скидки и бонусов.  и перерасчет стоимости заказа. вывод сообщения системы лояльности.
                                // выделяем бонусы 
                                string a1 = MainWindow.ResponseFromServer.Substring(MainWindow.ResponseFromServer.IndexOf("bonuses\":") + 9, (MainWindow.ResponseFromServer.IndexOf("},\"payment")) - (MainWindow.ResponseFromServer.IndexOf("bonuses\":") + 9));
                                a1 = a1.Replace(".", ",");
                                MainWindow.BonusValue = Convert.ToDecimal(a1);// MainWindow.ResponseFromServer.Substring(MainWindow.ResponseFromServer.IndexOf("bonuses\":")+9,(MainWindow.ResponseFromServer.IndexOf(",\"server_msg")) -(MainWindow.ResponseFromServer.IndexOf("bonuses\":") + 9))+ );
                                //"server_msg":"server message" Сообщение системы лояльности.
                                MainWindow.Loyaltymsg = MainWindow.ResponseFromServer.Substring(MainWindow.ResponseFromServer.IndexOf("server_msg\":\"") + 13, (MainWindow.ResponseFromServer.IndexOf("\",\"bonuses")) - (MainWindow.ResponseFromServer.IndexOf("server_msg\":") + 13));
 
                                
                                if (MainWindow.Loyaltymsg.Trim().Length != 0 && MainWindow.MsgLoyaltyOnOff == true)
                                {
                                    TextWindows = MainWindow.Loyaltymsg.Replace("\\r\\n","");
                                    int Point = TextWindows.Contains("!") == true ? TextWindows.IndexOf("!") : (TextWindows.Contains(".") == true ? TextWindows.IndexOf("."):0) ;
                                    TextWindows = TextWindows.Substring(0, Point) + Environment.NewLine + TextWindows.Substring(Point + 1, TextWindows.Length - (Point+1));
                                    NewOrder = new MessageError(TextWindows, 2, 7);
                                    NewOrder.ShowDialog();

                                }
                                // Товар
                                //string test = "{ \"code\":\"27757\",\"qty\":1.00,\"price\":44.00,\"amount\":30.00,\"discount\":10.00}";

                                string CurrentTovar = "";
                                json = MainWindow.ResponseFromServer.Substring((MainWindow.ResponseFromServer.IndexOf("\"goods\":[") + 9), (MainWindow.ResponseFromServer.IndexOf("}]") + 1) - (MainWindow.ResponseFromServer.IndexOf("\"goods\":[") + 9));
                                foreach (OrederPreview ListPreviewOrder in MainWindow.db.OrederPreviews.ToList<OrederPreview>())
                                {
                                    if (json.Length > 0)
                                    {
                                        CurrentTovar =  json.Substring(0, json.IndexOf("}") + 1); // test;
                                        Tovardiscountbonus restoredjson = JsonSerializer.Deserialize<Tovardiscountbonus>(CurrentTovar);
                                        if (restoredjson.discount != 0)
                                        {
                                            if (ListPreviewOrder.SumTov != restoredjson.amount)
                                            {
                                                decimal Procdicount = Math.Round((1 - (restoredjson.amount / ListPreviewOrder.SumTov)) * 100, MidpointRounding.ToEven);
                                                string Tov = ListPreviewOrder.NameTov.Substring(ListPreviewOrder.NameTov.IndexOf(" "), ListPreviewOrder.NameTov.Length - ListPreviewOrder.NameTov.IndexOf(" "));

                                                ListPreviewOrder.NameTov = Tov + "  Знижка -" + Convert.ToString(Procdicount) + "%";
                                                ListPreviewOrder.SumTov = restoredjson.amount;
                                                MainWindow.db.SaveChanges();
                                                MainWindow.SumSkidka += restoredjson.discount;
                                            }
                                        }
                                        if (json.IndexOf("}") + 1 < json.Length) json = json.Substring(json.IndexOf("}") + 2, json.Length - (json.IndexOf("}") + 2));
                                        else json = "";
                                    }

                                }
                                //MainWindow.CmdStrokaServer = "";
                            }
                            else
                            {
                               
                                if (MainWindow.ResponseFromServer.Contains("{\"code\":8") == true) LineError = "Помилка виконання системи лояльності";
                                if (MainWindow.ResponseFromServer.Contains("{\"code\":9") == true) LineError = "Запит до системі лояльності вже здійснено.";
                                if (MainWindow.ResponseFromServer.Contains("{\"code\":10") == true) LineError = "Система лояльності зайнята іншим запитом, треба здійснити повторний запит";
                                DiscountTrue = false;
                                TextWindows = "Шановний замовник! Вибачте. " + Environment.NewLine+LineError + Environment.NewLine + " Карта лояльності не ідентивікована.  "
                                + Environment.NewLine + "Оплату замовлення можливо здійснити без урахування бонусів та знижок. "
                                + Environment.NewLine + "Ви можете натиснути на кнопку <РЕДАГУВАТИ> замовлення, ще раз на кнопку <ГОТОВО> та повторити сканування карти лояльності. ";
                            }
                            
                        }
                        else
                        {
                            if(MainWindow.ExMessage.Contains("500")== true || MainWindow.ResponseFromServer =="")
                                TextWindows = "Шановний замовник! Вибачте. " + Environment.NewLine + " Карта лояльності не ідентивікована. "
                                + Environment.NewLine + "Оплату замовлення можливо здійснити без урахування бонусів та знижок. ";
                            DiscountTrue = false;
                        } 

                        if (DiscountTrue == false)
                        {
                            NewOrder.Close();
                            PreOrderConection.ErorDebag("Ошибка в запросе к веб сереверу " + Environment.NewLine + MainWindow.ResponseFromServer, 2);
                            NewOrder = new MessageError(TextWindows,2,12);
                            NewOrder.ShowDialog();
                            
 
                        }

                    }
                }

            }
            else
            {
                ErrorText = "Шановний замовник! Вибачте." + Environment.NewLine + "Не встановлено зв'язок з сервером." 
                +Environment.NewLine + "Додати замовлення до черги відкладених замовлень на касі неможливо." +
                Environment.NewLine + "Зверніться до Адміністратора.";
                OrderTrue = false;
            } 
            MainWindow.CmdStrokaServer = "";
            if (OrderTrue == false)
            { 
              
                 // Обработка ошибки заказа. Результат анализа завершения в коде "code\":0"
                //MainWindow.ExMessage.Contains("- 400")          
                NewOrder.Close();
                TextWindows = ErrorText;
                PreOrderConection.ErorDebag("Ошибка в запросе к веб сереверу " + Environment.NewLine + MainWindow.ResponseFromServer + Environment.NewLine + TextWindows, 2);
                NewOrder = new MessageError(TextWindows, 2, 7);
                NewOrder.ShowDialog();

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
                if (MainWindow.NameSetWindow != "ListPriceWin")
                {
                    CuponWin CuponWin_Link = ControlWin.LinkMainWindow("CuponWin");
                    if(CuponWin_Link !=null ) CuponWin_Link.Close();
                }
               
            }
        }
        public void ReadConfDiscount()
        {
            string PuthServer = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(PuthServer + "PreOrder.conf"))
            {
                string[] fileLines = File.ReadAllLines(PuthServer + "PreOrder.conf");
                foreach (string x in fileLines)
                {
                    if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Length != 0)
                    {
                        if (x.Trim().Contains("BarCodDiscount"))
                        {
                            string[] data = x.Split('=');
                            DiscountWin.DiscountBarCod = data[1].Trim();
                        }
                    }

                }
            }
        }

    }

   
}
