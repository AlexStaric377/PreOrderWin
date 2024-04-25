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
using System.Collections.ObjectModel;
using System.Threading;

namespace PreOrderWin
{

    public class OrederPreviewList
    {
        public int Id { get; set; }
        public string Quantity { get; set; }
        public string PriceTov { get; set; }
        public string SumTov { get; set; }
        public string Discount { get; set; }
        public string NameTov { get; set; }
    }


    /// <summary>
    /// Логика взаимодействия для ListZakazWin.xaml
    /// </summary>
    public partial class ListZakazWin : Window
    {
        public static int OnOffCash = 0, OnOffBank = 0;
        public static double MinusBonus = 0.0, PLusBonus = 0.0;
        ObservableCollection<OrederPreviewList> OrderPreviewList = new ObservableCollection<OrederPreviewList>();
       
        public ListZakazWin()
        {
            InitializeComponent();
            MainWindow.NameSetWindow = "ListZakazWin";




            //this.RowPayment.Height = new GridLength(2, GridUnitType.Pixel);
            //if (MainWindow.OnOffInvalid == 1) this.StackTake.VerticalAlignment = VerticalAlignment.Bottom;
            //else this.StackTake.VerticalAlignment = VerticalAlignment.Center;

            // закрыто на время отладки
            //Bar.Content = DiscountWin.DiscountBarCod;

            // 
            if (MainWindow.BankTerminalOnOff == false && MainWindow.RunTerminal == false) ButtonCart.Visibility = Visibility.Hidden;
            else ButtonCart.Visibility = Visibility.Visible;
    
            Bar.Content = DiscountWin.DiscountBarCod.ToString();
 
            if (MainWindow.BonusValue != 0)
            { 
                volumeSlider.Maximum = Convert.ToDouble(MainWindow.BonusValue);
                Bonus.Content = Convert.ToString(MainWindow.BonusValue);
                ValueMax.Content = Convert.ToString(MainWindow.BonusValue);
                
                ImageGrn.Visibility = Visibility.Visible;
                volumeSlider.IsEnabled = true;
                Minus.IsEnabled = true;
                Plus.IsEnabled = true;
                ItogoTxt.Visibility = Visibility.Visible;
                BonusItog.Visibility = Visibility.Visible;
                DiscTxt.Visibility = Visibility.Visible;
                SumPrice.Visibility = Visibility.Visible;
                SumBonus.Visibility = Visibility.Visible;
                
                GrnItog.Visibility = Visibility.Visible;
                GrnBonus.Visibility = Visibility.Visible;
                GrnIDisc.Visibility = Visibility.Visible;
                SpisBonus.Visibility = Visibility.Visible;
                Minus.Visibility = Visibility.Visible;
                Board.Visibility = Visibility.Visible;
                Plus.Visibility = Visibility.Visible;
                volumeSlider.Visibility = Visibility.Visible;
                ValueMin.Visibility = Visibility.Visible;
                ValueMax.Visibility = Visibility.Visible;
                Ok.Visibility = Visibility.Hidden;

            }
            else
            {
                volumeSlider.Maximum = 0;
                ValueMax.Visibility = Visibility.Hidden;
                Bonus.Visibility = Visibility.Hidden;
                ImageGrn.Visibility = Visibility.Hidden;
                volumeSlider.IsEnabled = false;
                Minus.IsEnabled = false;
                Plus.IsEnabled = false;
                ItogoTxt.Visibility = Visibility.Hidden;
                BonusItog.Visibility = Visibility.Hidden;
                DiscTxt.Visibility = Visibility.Hidden;
                SumPrice.Visibility = Visibility.Hidden;
                SumBonus.Visibility = Visibility.Hidden;
               
                GrnItog.Visibility = Visibility.Hidden;
                GrnBonus.Visibility = Visibility.Hidden;
                GrnIDisc.Visibility = Visibility.Hidden;
                SpisBonus.Visibility = Visibility.Hidden;
                Minus.Visibility = Visibility.Hidden;
                Board.Visibility = Visibility.Hidden;
                Plus.Visibility = Visibility.Hidden;
                volumeSlider.Visibility = Visibility.Hidden;
                ValueMin.Visibility = Visibility.Hidden;
                ValueMax.Visibility = Visibility.Hidden;
                Ok.Visibility = Visibility.Hidden;
            }
            

            if (MainWindow.RestHier == 2)
            { 
                TAKEOUT.Visibility = Visibility.Visible;
                TAKEOIN.Visibility = Visibility.Hidden;
            }

            if (MainWindow.RestHier == 1)
            {
                TAKEOUT.Visibility = Visibility.Hidden;
                TAKEOIN.Visibility = Visibility.Visible;
            }

            if (MainWindow.OnOffInvalid == 1)
            {

                Button4.Visibility = Visibility.Hidden;
                Button3.Visibility = Visibility.Hidden;
                GhangeOrder.Visibility = Visibility.Visible;
                DeletOrder.Visibility = Visibility.Visible;


            }
            else
            {

                GhangeOrder.Visibility = Visibility.Hidden;
                DeletOrder.Visibility = Visibility.Hidden;
                Button3.Visibility = Visibility.Visible;
                Button4.Visibility = Visibility.Visible;
            }

            if (MainWindow.language_Key == 1)
            {
       
                OrderDelete.Content = "Delete";
                OpderEdit.Content = "Edit";
                OstBonus.Content = "BONUS BALANCE:";
                BonusOst.Content = "MY ORDER";
                TakeOut.Content = "WITH A";
                TakeiN.Content = "HERE";
                ItogoTxt.Content = "Total:";
                DiscTxt.Content = "Discount:";
                Splata.Content = "To be paid";
                SpisBonus.Content = "WRITE BONUSES:";
                CashKassa.Content = "PAY AT THE CASH";
                CashcART.Content = "(cash or card)";
                BankTerminal.Content = "PAY HERE";
                CART.Content = "(only by card)";
                BackMain.Content = "BACK";
                BonusItog.Content = "Pay bonuses:";
                LabGhange.Content = "Edit";
                DeletGhange.Content = "Delete";
            }
            else
            {
                OrderDelete.Content = "Скасувати";
                OpderEdit.Content = "Редагувати";
                OstBonus.Content = "ЗАЛИШОК БОНУСІВ:";
                BonusOst.Content = "МОЄ ЗАМОВЛЕННЯ";
                TakeOut.Content = "З СОБОЮ";
                TakeiN.Content = "У ЗАКЛАДІ";
                ItogoTxt.Content = "Всього:";
                DiscTxt.Content = "Знижка:";
                Splata.Content = "До сплати";
                SpisBonus.Content = "СПИСАТИ БОНУСИ:";
                CashKassa.Content = "ПЛАТИТИ НА КАСІ";
                CashcART.Content = "(готівкою чи картою)";
                BankTerminal.Content = "ОППЛАТИТИ ТУТ";
                CART.Content = "(тільки картою)";
                BackMain.Content = "НАЗАД";
                BonusItog.Content = "Сплатити бонусами:";
                LabGhange.Content = "Редагувати";
                DeletGhange.Content = "Скасувати";

            }


            SumDisc.Content = MainWindow.SumSkidka > 0 ? Convert.ToString(MainWindow.SumSkidka) : "";
            SumDisc.Visibility = MainWindow.SumSkidka > 0 ? Visibility.Visible : Visibility.Hidden;
            SumPrice.Content = Convert.ToString(MainWindow.SumaOrder);
            SumBonus.Content = Convert.ToString(MainWindow.SumDiscountBasket);
            SumFinish.Content = Convert.ToString(MainWindow.SumaOrder- MainWindow.SumDiscountBasket- MainWindow.SumSkidka);
            MainWindow.SumaFinish = MainWindow.SumaOrder - MainWindow.SumDiscountBasket - MainWindow.SumSkidka;
            volumeSlider.Value = 0;
            ValueSlider.Text = "0";


            //---------------------------------
            //ApplicationContext db = new ApplicationContext();
            var ListOrderPreview = MainWindow.db.OrederPreviews.ToList<OrederPreview>();

            // Добавляем товар в тело заказа
            int indexstrok = 0;
            foreach (OrederPreview ListPreviewOrder in ListOrderPreview)
            {

                OrderPreviewList.Add(new OrederPreviewList
                {
                    Id = ListPreviewOrder.Id,
                    Quantity = Convert.ToString(ListPreviewOrder.Quantity) + " x ",
                    PriceTov = Convert.ToString(ListPreviewOrder.PriceTov),
                    SumTov = Convert.ToString(ListPreviewOrder.SumTov),
                    Discount = Convert.ToString(ListPreviewOrder.Discount),
                    NameTov = MainWindow.SumSkidka !=0 ? ListPreviewOrder.NameTov :ListPreviewOrder.NameTov.Substring(ListPreviewOrder.NameTov.IndexOf(" "), ListPreviewOrder.NameTov.Length- ListPreviewOrder.NameTov.IndexOf(" "))
                });

                indexstrok++;
            }
            StrokaOrderList.ItemsSource = OrderPreviewList;
            ControlWin.Setdbpgsql(nameof(ListZakazWin));
            Scrol.Visibility = Visibility.Hidden;
            if (indexstrok >= 6) Scrol.Visibility = Visibility.Visible;

            //if (MainWindow.CmdStrokaServer.Length != 0) CuponWin.LoadND2017();
        }

        //private void StrokaOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void Delete_Oder_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow.TimeClick++;
        //    ControlWin.DeleteOrderPreview();
        //    this.Close();
    
        //}

      
        private void ButtonZkaz_Back_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            if (MainWindow.ApiOrdersAdd == true) ControlWin.ReversOrderTurnCheck();
            CuponWin WinPayment = new CuponWin();
            WinPayment.Show();
            this.Close();
        }

     

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            if (Convert.ToDecimal(SumPrice.Content)-1 >= Convert.ToDecimal(volumeSlider.Value))
            { 
                string VolumeSlid = volumeSlider.Value.ToString();
                ValueSlider.Text = VolumeSlid.Contains(",") ? VolumeSlid.Substring(0, VolumeSlid.LastIndexOf(",")+2) : VolumeSlid;
                SumBonus.Content = ValueSlider.Text;
                decimal ValueBonusSlider = Convert.ToDecimal(ValueSlider.Text);

                if (ValueBonusSlider <= Convert.ToDecimal(SumPrice.Content))
                { 
                    SumFinish.Content = Convert.ToString(Convert.ToDecimal(SumPrice.Content) - Convert.ToDecimal(ValueSlider.Text) - (SumDisc.Content.ToString().Trim().Length >0 ? Convert.ToDecimal(SumDisc.Content):0.0m));
                    volumeSlider.IsEnabled = true;
                    Plus.IsEnabled = true;
                }            
            }
            else volumeSlider.IsEnabled = false;
        }

        private void ButtonMinus_Bonus_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MinusBonus = Convert.ToDouble(ValueSlider.Text);
            if (MinusBonus <= 0)
            {
                MinusBonus = 0;
                ValueSlider.Text = Convert.ToString(MinusBonus);
                return;
            }
                
            
            MinusBonus= MinusBonus-0.1;
            ValueSlider.Text = Convert.ToString(MinusBonus);
            volumeSlider.Value = MinusBonus;
            SumBonus.Content = ValueSlider.Text;
            decimal ValueBonusSlider = Convert.ToDecimal(ValueSlider.Text);
            if (ValueBonusSlider <= Convert.ToDecimal(MainWindow.BonusValue)) SumFinish.Content = Convert.ToString(Convert.ToDecimal(SumPrice.Content) - Convert.ToDecimal(ValueSlider.Text) - (SumDisc.Content.ToString().Trim().Length > 0 ? Convert.ToDecimal(SumDisc.Content) : 0.0m));
            else
            { 
                Plus.IsEnabled = true;
                volumeSlider.IsEnabled = true;          
            }
 
        }

        private void ButtonPlus_Bonus_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            PLusBonus = Convert.ToDouble(ValueSlider.Text);
            //if (Convert.ToDecimal(MainWindow.BonusValue) <= Convert.ToDecimal(SumPrice.Content))


            if ((Convert.ToDecimal(MainWindow.BonusValue)  >= Convert.ToDecimal(PLusBonus + 0.1)) && (Convert.ToDecimal(SumPrice.Content) - 1 >= Convert.ToDecimal(PLusBonus + 0.1)))
            {
                PLusBonus+= 0.1;
                ValueSlider.Text = Convert.ToString(PLusBonus);
                volumeSlider.Value = PLusBonus;
                SumBonus.Content = ValueSlider.Text;
                decimal ValueBonusSlider = Convert.ToDecimal(ValueSlider.Text);
                if (ValueBonusSlider <= Convert.ToDecimal(SumPrice.Content)) SumFinish.Content = Convert.ToString(Convert.ToDecimal(SumPrice.Content) - Convert.ToDecimal(ValueSlider.Text) - (SumDisc.Content.ToString().Trim().Length > 0 ? Convert.ToDecimal(SumDisc.Content) : 0.0m));
    
                   
            
            }
            else Plus.IsEnabled = false;
        }

        private void ButtonZkaz_GhangeOrder_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.ChangeOrder = 1;
            MainWindow.TimeClick++;
            // отменить заказ перед корректировкой
            if (MainWindow.ApiOrdersAdd == true) ControlWin.ReversOrderTurnCheck();
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

        private void ButtonZkaz_DeletOrder_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            // Удаление заказ из очереди отложеных чеков кассы
            if (MainWindow.ApiOrdersAdd == true) ControlWin.ReversOrderTurnCheck();
            ControlWin.DeleteOrderPreview();
            MainWindow.SumZakazBasket = 0;
            MainWindow.SumaOrder = 0;
            MainWindow.SumDiscountBasket = 0;
            MainWindow.NumberHdZakaz = 0;
            MainWindow.TextContent = "";
            MainWindow.TimeClick++;
            MainWindow.Kol = 0;
            ListPriceWin.StrokaKodTov = "";
            this.Close();
        }

        private void TakeOutOnOff_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (MainWindow.RestHier == 2)
            {
                TAKEOUT.Visibility = Visibility.Hidden;
                TAKEOIN.Visibility = Visibility.Visible;
                MainWindow.RestHier = 1;
            }
            else
            {
                TAKEOUT.Visibility = Visibility.Visible;
                TAKEOIN.Visibility = Visibility.Hidden;
                MainWindow.RestHier = 2;
            }
        }

        private void ButtonOk_PreviewTouchDown(object sender, TouchEventArgs e)
        {

            if (MainWindow.ApiOrdersAdd == true) PaymentOkBonus();

        }

        private void ButtonOk_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.ApiOrdersAdd == true) PaymentOkBonus();
        }

        // Подтверждение оплаты бонусами.
        public void PaymentOkBonus()
        {
            PreOrderConection.ErorDebag("Запрос к веб сереверу " + Environment.NewLine + " Здійснюється звернення до системи лояльності для списання бонусів.", 2);
            string TextWindows = "Шановний замовник! Зачекайте. " + Environment.NewLine + " Здійснюється звернення до системи лояльності для списання бонусів.";
            MessageError NewOrder = new MessageError(TextWindows, 0);
            NewOrder.Show();

            string LineError = "";
            bool DiscountTrue = true;
            
            //MainWindow.SumDiscountBasket = 5.0m;
            //MainWindow.SumDiscountBasket = Convert.ToDecimal(ValueSlider.Text);
            MainWindow.UrlAdresServer = MainWindow.TcpUrlServer + ":" + MainWindow.PortUrlServer + "/api/klo/use_bonuses"; //"http://192.168.1.11:8082/api/orders/reverse";
            string SumBonusItog = MainWindow.SumDiscountBasket.ToString().Replace(",", ".");
            MainWindow.CmdStrokaServer = "{\"cmd\":\"use_bonuses\",\"data\":{\"order_id\":" + MainWindow.IdOrderND2017.ToString() + ",\"loyalty\":{\"bonuses\":" + SumBonusItog + "}}}";
            ControlWin.LoadUrlServer(MainWindow.UrlAdresServer, MainWindow.CmdStrokaServer);
            PreOrderConection.ErorDebag("UrlServerReturnCode = " + MainWindow.UrlServerReturnCode.ToString() + Environment.NewLine + " Строка: " + MainWindow.ResponseFromServer, 2);
            NewOrder.Close();
            if (MainWindow.ReturnCodeServer == 0)
            {
                if (MainWindow.ResponseFromServer.Contains("code\":0") == false)
                { 
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":8") == true) LineError = "Помилка виконання системи лояльності";
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":9") == true) LineError = "Запит до системі лояльності вже здійснено.";
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":10") == true) LineError = "Система лояльності зайнята іншим запитом, треба здійснити повторний запит";
                    DiscountTrue = false;
                    TextWindows = "Шановний замовник! Вибачте. " + Environment.NewLine + LineError + Environment.NewLine + " Карта лояльності не ідентивікована.  "
                    + Environment.NewLine + "Оплату замовлення можливо здійснити без урахування бонусів та знижок. ";
                    DiscountTrue = false;
            
                }

            }
            else
            {
                if (MainWindow.ExMessage.Contains("500") == true || MainWindow.ExMessage.Contains("400") == true || MainWindow.ResponseFromServer == "")
                    TextWindows = "Шановний замовник! Вибачте. " + Environment.NewLine + " Помилка в системі лояльності при списанні бонусів  "
                    + Environment.NewLine + "Оплата замовлення буде здійснена без урахування бонусів та знижок. ";
                DiscountTrue = false;
            }
            if (DiscountTrue == false)
            {

                PreOrderConection.ErorDebag("Ошибка в запросе к веб сереверу " + Environment.NewLine + MainWindow.ResponseFromServer, 2);
                NewOrder = new MessageError(TextWindows, 2, 7);
                NewOrder.ShowDialog();
                MainWindow.CmdStrokaServer = "";
                MainWindow.SumaFinish = MainWindow.SumaOrder;
                ValueSlider.Text = "";
                SumBonus.Content = "";
                SumFinish.Content = Convert.ToString(Convert.ToDecimal(SumPrice.Content));
            }
        }

        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void ButtonZkaz_Nal_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.CashBank = 1; // оплата через касу наличными или картой
            MainWindow.SumDiscountBasket = Convert.ToDecimal(SumBonus.Content);
            MainWindow.SumaFinish = MainWindow.SumaOrder - MainWindow.SumDiscountBasket- MainWindow.SumSkidka;
            // подтверждение списания бонусов
            if (MainWindow.RunServer == true && MainWindow.SumDiscountBasket != 0) PaymentOkBonus();

            // тестовый запрос проверено
            //ControlWin.PaymentBankOnOff("cash");
            //ControlWin.ReversOrderTurnCheck();
            //
            MainWindow.ResultPayment = true;
            PaymentOrderWin WinTake = new PaymentOrderWin();
            WinTake.Show();
            this.Close();
        } 

        private void ButtonZkaz_Cart_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.SumDiscountBasket = Convert.ToDecimal(SumBonus.Content);
            MainWindow.SumaFinish = MainWindow.SumaOrder - MainWindow.SumDiscountBasket - MainWindow.SumSkidka;
            if (MainWindow.RunServer == true && MainWindow.SumDiscountBasket !=0) PaymentOkBonus();
            MainWindow.CashBank = 2; // оплата  картой
            PaymentBankWin WinTake = new PaymentBankWin();
            WinTake.Show();
            this.Close();

        }
    }
}
