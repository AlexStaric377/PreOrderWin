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
using Microsoft.EntityFrameworkCore;
using System.Management;
// Управление вводом-выводом
using System.IO;
using System.Threading;
using Microsoft.Win32;



namespace PreOrderWin
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

 
        public static double ScreenHeight = 0.0;
        public static double SetHeigtCurent = 0.0;
        public static double ScreenWidth = 0.0;
        public static Decimal SumZakazBasket = new Decimal(), SumaOrder = new Decimal(), SumaFinish = new Decimal(), BonusValue = 0M;
        public static Decimal SumPaymentBasket = new Decimal(),SumSkidka= new Decimal();
        public static Decimal SumDiscountBasket = new Decimal();
        public static int RestHier = 0;
        public static int CashBank = 0;
        public static int Card_Loyal = 0;
        public static int language_Key = 0;
        public static int OneLoadAdmin = 0;
        public static string TextContent = "", CodTexnologLoyalnost="", ProgramProc = "";
        public static int Kol = 0;
        public static decimal Cuma = 0M;
        public static int IdStart = 0, OrderListItem=0, CheckTerminal =0;
        public static int IndexActivProces = -1;
        public static int NumberHdZakaz = 0, IdTradFloor =1, UrlServerReturnCode = 0;
        public static int HeightWin = 100, RunListPrice = 0;
        public static int OnOffInvalid = 0, ActivRelated=0, ActivHotDog = 0, IdCol =0, IdPaper =0, IdMayonez =0,IDOrder =0, ReturnCodeServer=0; 
        public static int InitLoadedHeigt = 0, ChangeOrder=0, IndexKodTov=-1, IndexHotDog = -1, RelatedKodTov =0, HotDogKodTov = 0, Idnewtov = 0, CountOrder=0;
        public static byte USBPortTerminal = 0;
        public static string FonBord = "", PortTerminal = "", ResponseFromServer ="", TcpUrlServer = "", PortUrlServer = "", CmdStrokaServer = "", UrlAdresServer="", Loyaltymsg="";
        public static bool LoadPrimer = false, LoadTovGrup = false, BankTerminalOnOff=true, ClickButtonOnOff=true, ClickMouseOnOff = true, WaitStopTime=true, ApiOrdersAdd = false, UpdateLoadOpis=true;
        public static bool RunTerminal = false, ResultPayment = true, EndRegOrder = false, RunServer = true, LoadPriceItems= false, LoadSetdbpgsql = false, MsgLoyaltyOnOff = false;
        public static string TMBord = "", BarCod = "", NameSetWindow = "", NameSetRelated = "", NameMapTov ="", NameError = "", ExMessage="", TimeEndSmena="", TimeEndRegOrder=""; 
        public static int ColOnOff=1,PapricOnOff=1,MayonezOnOff=1, InfoWindow=0, TimeOut=0, TimeOutRun = 0, TimeClick =0,SetClick=0,TakClick=0;
        public static int IdGrupTov = 0, KodVidTov = 0, KodPvidTov = 0,IdOrderND2017,CountListCupon=0, HotDogColOnOff=0, HotDogPaprikOnOff = 0, HotDogMayonezOnOff = 0;
        public static int[] LiskodTov = new int[100], HotDogLiskodTov = new int[100];
        public static string[] NameRunWindow = new string[100], NameBackWindow = new string[100],ListCuponLoylti = new string[10];
        public static ApplicationContext db = new ApplicationContext();



        /// Структура данных для многопотоковой среды (передача аргументов)
        /// </summary>
        public struct RenderInfo
        {
            public string argument1 { get; set; }
            public string argument2 { get; set; }
            public string argument3 { get; set; }
            public string argument4 { get; set; }
            public string argument5 { get; set; }
        }


        public MainWindow()
        {
            InitializeComponent();
            // 1920х1080
            ScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            ScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;



            // Загрузка тестовых переменных
            string PuthServer = AppDomain.CurrentDomain.BaseDirectory; // @"d:\PreorderWin\";
            if (File.Exists(PuthServer + "PreOrder.conf"))
            {

                string[] fileLines = File.ReadAllLines(PuthServer + "PreOrder.conf");
                foreach (string x in fileLines)
                {

                    if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Length != 0)
                    {
                        
                        
                        string[] data = x.Split('=');
                        if (data[0].Trim() == "DataTest" && data[1].Trim() == "true") LoadPrimer = true;
                        if (data[0].Trim() == "BarCod") BarCod = data[1];
                        if (data[0].Trim() == "BonusValue") BonusValue = Convert.ToDecimal(data[1]);
                        if (data[0].Trim() == "TradFloor") IdTradFloor = Convert.ToInt16(data[1]);
                    }


                }
            }
 


        }

        private void ButtonClickMain_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            //if(ClickButtonOnOff == true) 
                ClickButton();
        }

        //private void ButtonClickMain_TouchDown(object sender, TouchEventArgs e)
        //{
        //    e.Handled = true;
        //    ClickButtonOnOff = false;
        //    if (WaitStopTime == true)ClickButton();
        //}
        private void ClickButton()
        {

            //ControlWin.AddBaseFoto();
            //MessageBox.Show("DeleteOrderPreview");
            ControlWin.DeleteOrderPreview();
            //PreOrderConection.ErorDebag("Setdbpgsql ", 3);
            ControlWin.Setdbpgsql(nameof(MainWindow));
            if (MainWindow.ReturnCodeServer == 1) return;
            MainWindow.ApiOrdersAdd = false;
            MainWindow.ResultPayment = false;
            DiscountWin.DiscountBarCod = MainWindow.CodTexnologLoyalnost;
            //ClickButtonOnOff = false;
            SumZakazBasket = 0;
            SumaOrder = 0;
            SumDiscountBasket = 0;
            ProgramProc = "";
            MainWindow.TimeOut = 0;
            MainWindow.NumberHdZakaz = 0;
            MainWindow.ChangeOrder = 0;
            MainWindow.KodPvidTov = 0;
            MainWindow.KodVidTov = 0;
            MainWindow.IdGrupTov = 0;
            //Загрузка контрольного примера.

            if (LoadPrimer == true) { ControlWin.DataTest(); LoadPrimer = false; UpdateDataTest(); }
                
            for (int i = 0; i < 100; i++) LiskodTov[i] = 0;
            for (int i = 0; i < 10; i++) ListCuponLoylti[i] = "";
            CountListCupon = 0;
            MainWindow.NameSetRelated = "";
            if(TimeOutRun == 0) ControlWin.RunTimeOut();
            //ControlWin.RunBankTerminal();
            //MainWindow.CashBank = 2;
            //ControlWin.PaymentBankOnOff("bank");
            //PaymentBankWin.PaymentBankOk();
            //CuponWin.LoadND2017();
            MainWindow.TimeClick ++;
            TakeWin WinTake = new TakeWin();
            WinTake.Show();
        }

        private void ExitPreOrder(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (ClickMouseOnOff == true) Environment.Exit(0);
        }

        private void ExitTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            ClickMouseOnOff = false;
            Environment.Exit(0);
        }

        public void UpdateDataTest()
        {
            string PuthServer = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(PuthServer + "PreOrder.conf"))
            {
                int Idcount = 0, Idcountout = 0;
                string[] fileLines = File.ReadAllLines(PuthServer + "PreOrder.conf");
                string[] fileoutLines = new string[fileLines.Count()];
                foreach (string x in fileLines)
                {
                    if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Length != 0)
                    {
                        if (x.Trim().Contains("DataTest"))
                        {
                            fileLines[Idcount] = "DataTest = false";
                        }
                        fileoutLines[Idcountout] = fileLines[Idcount];
                        Idcountout++;
                    }
                    Idcount++;
                }
                File.WriteAllLines(PuthServer + "PreOrder.conf", fileoutLines);
            }
        }
    }
}
