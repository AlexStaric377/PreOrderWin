using System;
using System.Windows;
// Управление сетью
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing.Printing;
using System.Threading;
using System.Windows.Threading;
using System.Data;
// Управление Xml
using System.Xml;

// Управление вводом-выводом
using System.IO;
using System.IO.Compression;
using System.Management;
using System.ServiceProcess;
using System.Collections.ObjectModel;


using System.Drawing;
// Ссылка в проекте MSV2010 добовляется ...
using System.Drawing.Text;

using System.Globalization;

using System.Data.SqlClient;

// Управление сетью

//--- для Проверки Сборок
using System.Reflection;
// Импорт библиотек Windows DllImport (управление питанием ОС, ...
using System.Runtime.InteropServices;


using System.Security.Permissions;

/// Многопоточность

using System.Timers;


using System.Xml.Linq;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;

using System.Runtime.CompilerServices;







namespace PreOrderWin
{



    public partial class ControlWin
    {



        public static string FonBord = "";
        public static string TMBord = "";
        public static string Uri_Zakaz_Hier = "";
        public static string Uri_Zakaz_Houm = "";
        public static string Uri_FlagUk = "";
        public static string Uri_FlagEn = "";
        public static string Uri_Invalid = "";
        public static string TradingNameFloor = "";
        public static string Uri_KLODyakuyou = "";
        public static string Uri_Inval = "";
        public static string Uri_Card = "";
        public static string Uri_CardNo = "";
        public static string Uri_PaymentCash = "";
        public static string Uri_PaymentHoum = "";
        public static string Uri_QrImage = "";
        public static string Uri_ButtonCash = "";
        public static string Uri_ButtonCart = "", Uri_Reclama = "";
        public static string[] ListNameTov = new string[43], ListNameEn = new string[43];
        public static int[] ListKodTov = new int[43];
        public static string[] OpisTov = new string[43];
        public static string[] OpisTovEN = new string[43];
        public static decimal[] ListPriceTov = new decimal[43];
        public static string[] ListTov = new string[22];
        public static int ConectionTov = 1, CurrentLastResultTerminal = 0;
        public static int[] ListConectionGrup = new int[12];
        public static int[] ListConectionVid = new int[12];
        public static int[] ListConectionPvid = new int[12], ListConectionTov = new int[12];
        public static int[] ListCompositionTov = new int[12];
        public static int[] ListCompositionCalc = new int[12];
        public static int OnOffConectionGrup = 0, OnOffConectionVid = 0, OnOffConectionPvid = 0, OnOffConectionTov=0, OnOffCompositionTov = 0, CompositionTovIdtovBaz = 0,
                            CompositionTovIdtovReplace = 0;
        public static string[] ListStatus = new string[10];
        public static byte[] ImageFoto;
        public static int LastResult = 0, KodVozvarata = 0, CardSignVerif = 0;
        public static string ErrorMessageTerminal = "", CardPAN = "", CardHolder = "", CardIssuerName = "", CardRRN = "", TextWindows = "";
        public static string NumberBankKart = "", NumberTranzakcii = "", KodAvtor = "", TerminalID = "", InvoiceNum = "", MerchantKasir = "", PaymentSystem = "", UserNameCard = "", UserNameCardAvtor = "";
        public static ECRCommXLib.BPOS1Lib Terminal = new ECRCommXLib.BPOS1Lib();

        public static WebResponse result = null;
        public static WebRequest reqPOST = null;
        public static Stream sendStream = null;
        public static Stream ReceiveStream = null;
        public static StreamReader StrRead = null;


        #region Вернуть ссылку на главное окно по запросу WPF C# {LinkMainWindow}
        /// <summary>
        /// Вернуть ссылку на окно по запросу WPF C# {ListWindowMain}MainWindow
        /// </summary>
        /// <param name="NameWindow"> Имя главного окна Рабочий стол или Панель</param>
        /// <returns></returns>
        public static dynamic LinkMainWindow(string NameWindow)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Title == NameWindow)
                    return (dynamic)window;
            }
            return null;
        }
        #endregion

        // Процедура обнаружения активных серверов 
        public static int ScanActivPg()
        {



            string Pg = "postgresql-x64-12";
            var RezChek = 1;

            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName == Pg) { MainWindow.IndexActivProces++; break; }
            }

            if (MainWindow.IndexActivProces < 0) RezChek = 0;
            return RezChek;
        }


        public static void Setdbpgsql(string NameWindow)
        {
            if (MainWindow.LoadSetdbpgsql == false)
            { 
                try
                {
                    var LoadAdmin = MainWindow.db.Admins.ToList<Admin>();
                    foreach (Admin interfeysWindows in LoadAdmin)
                    {
                        if (interfeysWindows.Name == "LabelOneWindow")
                            ControlWin.FonBord = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "Logotip")
                            ControlWin.TMBord = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "Zakaz_Hier")
                            ControlWin.Uri_Zakaz_Hier = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "Zakaz_Houm")
                            ControlWin.Uri_Zakaz_Houm = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "FlagUk")
                            ControlWin.Uri_FlagUk = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "FlagEn")
                            ControlWin.Uri_FlagEn = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "Invalid")
                            ControlWin.Uri_Invalid = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "KLODyakuyou")
                            ControlWin.Uri_KLODyakuyou = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "Invalid")
                            ControlWin.Uri_Inval = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "Card")
                            ControlWin.Uri_Card = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "CardNo")
                            ControlWin.Uri_CardNo = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "Card")
                            ControlWin.Uri_Card = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "CardNo")
                            ControlWin.Uri_CardNo = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "PaymentCash")
                            ControlWin.Uri_PaymentCash = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "PaymentHoum")
                            ControlWin.Uri_PaymentHoum = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "QrImage")
                            ControlWin.Uri_QrImage = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "ButtonCash")
                            ControlWin.Uri_ButtonCash = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "ButtonCart")
                            ControlWin.Uri_ButtonCart = interfeysWindows.Puth;
                        if (interfeysWindows.Name == "BordReclama")
                            ControlWin.Uri_Reclama = interfeysWindows.Puth;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MainWindow.ExMessage = ex.Message;
                    PreOrderConection.ErorDebag("Ошибка: " + MainWindow.ExMessage, 3);

                }

           
                //}
                PreOrderConection.ErorDebag("MainWindow ",3);
                Admin entity = new Admin();
                if (NameWindow == "MainWindow")
                {

                    MainWindow WindowUri = ControlWin.LinkMainWindow(NameWindow);
                    if (ControlWin.FonBord.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.FonBord, UriKind.Relative)));
                        WindowUri.FonWindow.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {
                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "LabelOneWindow";
                        entity.Puth = "d:\\PreOrderWin\\Images\\screen10.jpg";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }
                    if (ControlWin.TMBord.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.TMBord, UriKind.Relative)));
                        WindowUri.TmKLO.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {
                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "Logotip";
                        entity.Puth = "d:\\PreOrderWin\\Images\\logo.png";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }

                    foreach (TradingFloor tradingFloor in MainWindow.db.TradingFloors.ToList<TradingFloor>())
                    {
                        if (tradingFloor.NaNumberFloor == 1)
                            ControlWin.TradingNameFloor = tradingFloor.NameFloor;
                    }
                    if (ControlWin.TradingNameFloor == "")
                    {
                        TradingFloor Floor = new TradingFloor();
                        Floor.NaNumberFloor = 1;
                        Floor.NameFloor = "ТОВ \"KLO\" kiosk";
                        Floor.AdrFloor = "м. Київ, вул. Трублаїні, 1б";
                        Floor.IPN = "123456789012";
                        Floor.Tel = "+380 93 046 6920";
                        Floor.TelFair = "0 800 301 555";

                        MainWindow.db.TradingFloors.Add(Floor);
                        MainWindow.db.SaveChanges();
                    }
                }
                if (NameWindow == "TakeWin")
                {

                    TakeWin takeWin = ControlWin.LinkMainWindow(NameWindow);
                    if (ControlWin.Uri_Zakaz_Hier.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.Uri_Zakaz_Hier, UriKind.Relative)));
                        takeWin.Zakaz_Hier.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {
                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "Zakaz_Hier";
                        entity.Puth = "d:\\PreOrderWin\\Images\\Order_Hier.jpg";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }
                    if (ControlWin.Uri_Zakaz_Houm.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.Uri_Zakaz_Houm, UriKind.Relative)));
                        takeWin.Zakaz_Houm.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {
                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "Zakaz_Houm";
                        entity.Puth = "d:\\PreOrderWin\\Images\\Order_Houm.jpg";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }
                    if (ControlWin.Uri_FlagUk.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.Uri_FlagUk, UriKind.Relative)));
                        takeWin.FlagUk.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {
                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "FlagUk";
                        entity.Puth = "d:\\PreOrderWin\\Images\\Ukraine.png";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }
                    if (ControlWin.Uri_FlagEn.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.Uri_FlagEn, UriKind.Relative)));
                        takeWin.FlagEn.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {
                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "FlagEn";
                        entity.Puth = "d:\\PreOrderWin\\Images\\English.jpg";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }
                    if (ControlWin.Uri_Invalid.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.Uri_Invalid, UriKind.Relative)));
                        takeWin.Invalid.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {
                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "Invalid";
                        entity.Puth = "d:\\PreOrderWin\\Images\\Invalid_Klo.jpg";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }
                }
                if (NameWindow == "ListPriceWin")
                {

                    ListPriceWin listPriceWin = ControlWin.LinkMainWindow(NameWindow);

                    if (ControlWin.Uri_Inval.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.Uri_Inval, UriKind.Relative)));
                        listPriceWin.Invalid.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {
                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "Invalid";
                        entity.Puth = "d:\\PreOrderWin\\Images\\Invalid_Klo.jpg";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }
                    if (ControlWin.Uri_Reclama.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.Uri_Reclama, UriKind.Relative)));
                        listPriceWin.ReclamaTov.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {
                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "BordReclama";
                        entity.Puth = "d:\\PreOrderWin\\Images\\ReclamaAvi.jpg";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }
                }

                if (NameWindow == "ListZakazWin")
                {

                    ListZakazWin listZakazWin = ControlWin.LinkMainWindow(NameWindow);
                    if (ControlWin.Uri_ButtonCash.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.Uri_ButtonCash, UriKind.Relative)));
                        listZakazWin.ButtonCash.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {

                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "ButtonCash";
                        entity.Puth = "d:\\PreOrderWin\\Images\\CashGrn.jpg";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();
                    }
                    if (ControlWin.Uri_ButtonCart.Trim().Length > 0)
                    {
                        ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.Uri_ButtonCart, UriKind.Relative)));
                        listZakazWin.ButtonCart.Background = (System.Windows.Media.Brush)imageBrush;
                    }
                    else
                    {

                        entity.Id = MainWindow.db.Admins.Count() + 1;
                        entity.Name = "ButtonCart";
                        entity.Puth = "d:\\PreOrderWin\\Images\\screen93cart.jpg";

                        MainWindow.db.Admins.Add(entity);
                        MainWindow.db.SaveChanges();

                    }
                }
                PreOrderConection.ErorDebag("SprStatuss ", 3);
                if (MainWindow.db.SprStatuss.Count() == 0)
                {
                    ListStatus[0] = "нове замовлення"; ListStatus[1] = "готується"; ListStatus[2] = "виготовлено"; ListStatus[3] = "отримано"; ListStatus[4] = "відмінено";
                    SprStatus sprStatus = new SprStatus();
                    for (int i = 0; i < 6; i++)
                    {
                        sprStatus.Id = i + 1;
                        sprStatus.KodStatus = i + 1;
                        sprStatus.NameStatusUk = ListStatus[i];
                        sprStatus.NameStatusEn = "";

                        MainWindow.db.SprStatuss.Add(sprStatus);
                        MainWindow.db.SaveChanges();
                    }

                }
                PreOrderConection.ErorDebag("SprPayments ", 2);
                if (MainWindow.db.SprPayments.Count() == 0)
                {
                    ListStatus[0] = "готівка"; ListStatus[1] = "карта";
                    SprPayment strPayment = new SprPayment();
                    for (int i = 0; i < 2; i++)
                    {
                        strPayment.Id = i + 1;
                        strPayment.KodPayment = i + 1;
                        strPayment.NamePaymentUk = ListStatus[i];
                        strPayment.NamePaymentEn = "";

                        MainWindow.db.SprPayments.Add(strPayment);
                        MainWindow.db.SaveChanges();
                    }
                }
                if (MainWindow.db.SprVidGiveOuts.Count() == 0)
                {
                    ListStatus[0] = "в закладі"; ListStatus[1] = "з собою";
                    SprVidGiveOut sprStatus = new SprVidGiveOut();
                    for (int i = 0; i < 2; i++)
                    {
                        sprStatus.Id = i + 1;
                        sprStatus.KodGiveOut = i + 1;
                        sprStatus.NameGiveOutUk = ListStatus[i];
                        sprStatus.NameGiveOutEn = "";

                        MainWindow.db.SprVidGiveOuts.Add(sprStatus);
                        MainWindow.db.SaveChanges();
                    }

                }

                if (MainWindow.db.HotDogUnits.Count()== 0)
                {
                    ListStatus[0] = "світла булочка"; ListStatus[1] = "темна булочка"; ListStatus[2] = "light bun"; ListStatus[3] = "dark bun";
                    ListKodTov[0] = 44559; ListKodTov[1] = 50444; ListKodTov[2] = 50446; ListKodTov[3] = 50448; ListKodTov[4] = 50450;
                    HotDogUnit sprStatus = new HotDogUnit();
                    for (int i = 0; i < 5; i++)
                    {
                        for (int part = 0; part < 2; part++)
                        { 
                            sprStatus.Id = MainWindow.db.HotDogUnits.Count() == 0 ? 1 : MainWindow.db.HotDogUnits.Max(p => p.Id) + 1;
                            sprStatus.Kodtov = ListKodTov[i];
                            sprStatus.KodUnit = part + 1;
                            sprStatus.NameUnitUk = ListStatus[part];
                            sprStatus.NameUnitEn = "";
                            MainWindow.db.HotDogUnits.Add(sprStatus);
                            MainWindow.db.SaveChanges(); 
                        }
                    }

                }
            }
            PreOrderConection.ErorDebag("VewMainWindow ", 3);
            if (NameWindow == "MainWindow")
            {
                if (MainWindow.LoadSetdbpgsql == false)
                { 
                    if (MainWindow.RunTerminal == false && MainWindow.RunServer == false)
                    {
                        MainWindow.BankTerminalOnOff = false;
                        string TextWindows = "Шановний замовник! " + Environment.NewLine + "Програмний комплекс налаштовано без взаємодії з кассою ND2017 та банковським терміналом.";
                        MessageError NewOrder = new MessageError(TextWindows, 2,3);
                        NewOrder.ShowDialog();
                        //Thread.Sleep(7000);
                        //NewOrder.Close();
                    }

                    if (MainWindow.db.AdminSetValues.Count() == 0)
                    {
 
                        AdminSetValue StrAdminSetValue = new AdminSetValue();
                        StrAdminSetValue.Id = 1;
                        StrAdminSetValue.TcpApiHttpServer = MainWindow.TcpUrlServer;
                        StrAdminSetValue.PortServer = MainWindow.PortUrlServer;
                        StrAdminSetValue.ValueCmdStroka = "";
                        StrAdminSetValue.StatusOrder = 0;
                        StrAdminSetValue.CountOrder = 0;
                        StrAdminSetValue.PortTerminal = Convert.ToString(MainWindow.USBPortTerminal);
                        StrAdminSetValue.NumberOrder = 1;
                        StrAdminSetValue.TimeEndSmena = MainWindow.TimeEndSmena.Trim().Length == 0 ? "24:00" : MainWindow.TimeEndSmena;
                        StrAdminSetValue.TimeEndRegOrder = MainWindow.TimeEndRegOrder.Trim().Length == 0 ? "23:30" : MainWindow.TimeEndRegOrder;
                        MainWindow.db.AdminSetValues.Add(StrAdminSetValue);
                        MainWindow.db.SaveChanges();

                        if (MainWindow.TcpUrlServer.Trim().Length == 0)
                        {
                            PreOrderConection.ErorDebag("возникло исключение:" + Environment.NewLine + " Не указан TCP/AP адрес сервера для связи с кассой ", 2);
                            string TextWindows = "Не вказано TCP/AP адресу для взаємозв'язку з касою " + Environment.NewLine + " Вкажіть TCP/AP адресу у файлі PreOrder.conf, параметр: TcpUrlServer = ."
                                    + Environment.NewLine + " Відсутність комунікації з касою не забезпечує режим оплати замовлень через банк.";
                            MessageError NewOrder = new MessageError(TextWindows, 2,5);
                            NewOrder.ShowDialog();
                            return;

                        }
                        if (MainWindow.PortUrlServer.Trim().Length == 0)
                        {
                            PreOrderConection.ErorDebag("возникло исключение:" + Environment.NewLine + " Не указан порт сервера для связи с кассой ", 2);
                            string TextWindows = "Не вказано порт для взаємозв'язку з касою " + Environment.NewLine + " Вкажіть порт у файлі PreOrder.conf, параметр: PortUrlServer = ."
                                + Environment.NewLine + " Відсутність комунікації з касою не забезпечує режим оплати замовлень через банк.";
                            MessageError NewOrder = new MessageError(TextWindows, 2, 7);
                            NewOrder.ShowDialog();
                            return;
                        }

                    }
                    else
                    {
 
                        var ListAdmin = MainWindow.db.AdminSetValues.Where(p => p.Id == 1).FirstOrDefault();

                        if (MainWindow.RunServer == true)
                        {
                            // загрузка состояния системных настроек и проверка актуальности парпаметров.
                            // Контроль времени окончания регистрации смены
                            if (MainWindow.TimeEndRegOrder.Trim().Length != 0 && MainWindow.TimeEndRegOrder != ListAdmin.TimeEndRegOrder)
                            {
                                ListAdmin.TimeEndRegOrder = MainWindow.TimeEndRegOrder;
                                MainWindow.db.SaveChanges();
                            }
                            else MainWindow.TimeEndRegOrder = ListAdmin.TimeEndRegOrder;

                            if (MainWindow.TimeEndSmena.Trim().Length != 0 && MainWindow.TimeEndSmena != ListAdmin.TimeEndSmena)
                            {
                                ListAdmin.TimeEndSmena = MainWindow.TimeEndSmena;
                                MainWindow.db.SaveChanges();
                            }
                            else MainWindow.TimeEndSmena = ListAdmin.TimeEndSmena;

                            // DateTime.Now.ToString("MMddHHmmsstt")

                            MainWindow.EndRegOrder = false;
                            if (MainWindow.TimeEndRegOrder != null)
                            {
                                if (MainWindow.TimeEndRegOrder.Trim().Length != 0)
                                {
                                    if (Convert.ToInt16(MainWindow.TimeEndRegOrder.Substring(0, 2)) < Convert.ToInt16(DateTime.Now.ToString("HH"))) MainWindow.EndRegOrder = true;
                                    if (Convert.ToInt16(MainWindow.TimeEndRegOrder.Substring(0, 2)) == Convert.ToInt16(DateTime.Now.ToString("HH"))
                                        && Convert.ToInt16(MainWindow.TimeEndRegOrder.Substring(3, 2)) < Convert.ToInt16(DateTime.Now.ToString("mm"))) MainWindow.EndRegOrder = true;

                                    if (MainWindow.EndRegOrder == true)
                                    {
                                        MainWindow.ReturnCodeServer = 1;
                                        PreOrderConection.ErorDebag("Закончилось время регистрации заказов : " + MainWindow.TimeEndRegOrder, 2);
                                        string TextWindows = "Шановний замовник! Вибачте. " + Environment.NewLine + " Закінчився робочий час для реестрації замовлень.  "
                                               + Environment.NewLine + "Формування замовлень неможливо. ";
                                        MessageError NewOrder = new MessageError(TextWindows, 2, 4);
                                        NewOrder.ShowDialog();
                                        return;
                                    }

                                }

                            }

                            //  проверка TCP/ AP адрес и порта
                            if (MainWindow.TcpUrlServer.Trim().Length != 0 && MainWindow.TcpUrlServer != ListAdmin.TcpApiHttpServer)
                            {
                                ListAdmin.TcpApiHttpServer = MainWindow.TcpUrlServer;
                                MainWindow.db.SaveChanges();
                            }
                            else MainWindow.TcpUrlServer = ListAdmin.TcpApiHttpServer;
                            if (MainWindow.PortUrlServer.Trim().Length != 0 && MainWindow.PortUrlServer != ListAdmin.PortServer)
                            {
                                ListAdmin.PortServer = MainWindow.PortUrlServer;
                                MainWindow.db.SaveChanges();
                            }
                            else MainWindow.PortUrlServer = ListAdmin.PortServer;


                            // проверка соединения с сервером кассы

                            if (MainWindow.TcpUrlServer != null && MainWindow.PortUrlServer != null)
                            {
                                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                                {
                                    MainWindow LinkMainWindow = ControlWin.LinkMainWindow("MainWindow");
                                    LinkMainWindow.IsEnabled = false;
                                    PingNd2017();
                                    LinkMainWindow.IsEnabled = true;
                                }));
                            }
                            else
                            {
                                string TCPPort = MainWindow.TcpUrlServer == null ? "TCP/AP адресу" : "порт";
                                TCPPort = MainWindow.TcpUrlServer == null && MainWindow.PortUrlServer == null ? "TCP/AP адресу та порт" : TCPPort;
                                PreOrderConection.ErorDebag("возникло исключение:" + Environment.NewLine + " Не указан " + TCPPort + " сервера для связи с кассой ", 2);
                                string TextWindows = "Не вказано " + TCPPort + " для взаємозв'язку з касою " + Environment.NewLine + " Вкажіть " + TCPPort + " у файлі PreOrder.conf, параметр: TcpUrlServer = ."
                                        + Environment.NewLine + " Відсутність комунікації з касою не забезпечує режим оплати замовлень. Зверніться до Адміністратора. Робота припинена";
                                MessageError NewOrder = new MessageError(TextWindows, 2, 5);
                                NewOrder.ShowDialog();
                                MainWindow.ReturnCodeServer = 1;
                                return;
                            }

                        }

                        if (MainWindow.RunTerminal == true)
                        {
                            // проверка готовности работы с терминалом.
                            if (MainWindow.USBPortTerminal != 0 && MainWindow.USBPortTerminal != Convert.ToByte(ListAdmin.PortTerminal))
                            {
                                ListAdmin.PortTerminal = Convert.ToString(MainWindow.USBPortTerminal);
                                MainWindow.db.SaveChanges();
                            }
                            else
                            { MainWindow.USBPortTerminal = Convert.ToByte(ListAdmin.PortTerminal); }

                            string EcrDll = @"c:\Program Files (x86)\Ingenico Group\ECR ActiveX Library\ECRcommX.dll";
                            if (MainWindow.CheckTerminal == 0)
                            {
                                if (File.Exists(EcrDll) == false)
                                {
                                    PreOrderConection.ErorDebag("возникло исключение:" + Environment.NewLine + " Відсутня бібліотека ECRcommX.dll х32 ", 2);
                                    string TextWindows = "Відсутня бібліотека ECRcommX.dll х32" + Environment.NewLine + " Зареєструйте бібліотеку для рооботи з банк.терминалом. "
                                            + Environment.NewLine + " Відсутність комунікації з терміналом забезпечує режим оплати тільки готівкою. ";
                                    MessageError NewOrder = new MessageError(TextWindows, 2, 5);
                                    NewOrder.ShowDialog();
                                    MainWindow.BankTerminalOnOff = false;


                                }
                                if ((MainWindow.PortTerminal == "" || MainWindow.USBPortTerminal == 0) && MainWindow.BankTerminalOnOff == true)
                                {

                                    PreOrderConection.ErorDebag("возникло исключение:" + Environment.NewLine + " Не задан порт для связи с банковским терминалом ", 2);
                                    string TextWindows = "Не задано порт для зв'язку з банківським  терміналом." + Environment.NewLine + " Вкажіть № порта у файлі PreOrder.conf, параметр: PortTerminal = ."
                                            + Environment.NewLine + " Відсутність комунікації з терміналом забезпечує режим оплати тільки готівкою. ";
                                    MessageError NewOrder = new MessageError(TextWindows, 2, 5);
                                    NewOrder.ShowDialog();
                                    MainWindow.BankTerminalOnOff = false;
                                }
                                Terminal.CommOpen(MainWindow.USBPortTerminal, 115200);
                                if (Convert.ToInt16(Terminal.LastResult) == 1 && MainWindow.BankTerminalOnOff == true)
                                {
                                    ControlWin.StatusError(Convert.ToInt16(ControlWin.Terminal.LastErrorCode), ControlWin.Terminal.LastErrorDescription, Convert.ToInt16(ControlWin.Terminal.ResponseCode),
                                    Convert.ToInt16(ControlWin.Terminal.LastStatMsgCode), ControlWin.Terminal.LastStatMsgDescription, Convert.ToInt16(ControlWin.Terminal.SignVerif));
                                    ControlWin.Terminal.CommClose();
                                    MainWindow.BankTerminalOnOff = false;
                                }
                                MainWindow.CheckTerminal = 1;
                            }                   
                        }
  


                        // Проверка состояния строки небыло ли перегрузки во время работы с сервером и не завис ли сервер.
                        if (MainWindow.RunServer == true)
                        {
                            bool ConectionUrlServer = true;
                            MainWindow.CmdStrokaServer = ListAdmin.ValueCmdStroka;
                            if (MainWindow.CmdStrokaServer != null)
                            {
                                if (MainWindow.CmdStrokaServer.Trim().Length != 0)
                                {
                                    // работа с серевером прервана, необходимо запросить информацию о строке.
                                    if ((MainWindow.CmdStrokaServer.Contains("add_order") == true ||
                                        MainWindow.CmdStrokaServer.Contains("use_loyalty") == true ||
                                        MainWindow.CmdStrokaServer.Contains("use_bonuses") == true ||
                                        MainWindow.CmdStrokaServer.Contains("get_order") == true ||
                                        MainWindow.CmdStrokaServer.Contains("reverse_order") == true ||
                                        (MainWindow.CmdStrokaServer.Contains("commit_order") && MainWindow.CmdStrokaServer.Contains("cash"))) && MainWindow.BankTerminalOnOff == true)

                                    {
                                        MainWindow.UrlAdresServer = MainWindow.TcpUrlServer + ":" + MainWindow.PortUrlServer + "/api/orders/reverse";
                                        ControlWin.LoadUrlServer(MainWindow.UrlAdresServer, MainWindow.CmdStrokaServer);
                                        PreOrderConection.ErorDebag("UrlServerReturnCode = " + MainWindow.UrlServerReturnCode.ToString() + Environment.NewLine + " Строка: " + MainWindow.ResponseFromServer, 2);
                                    }
                                    else ConectionUrlServer = false;
                                    //
                                    if (MainWindow.CmdStrokaServer.Contains("commit_order") == true &&
                                        MainWindow.CmdStrokaServer.Contains("bank") == true && MainWindow.BankTerminalOnOff == true)
                                    {
                                        MainWindow.UrlServerReturnCode = 0;
                                        MainWindow.UrlAdresServer = MainWindow.TcpUrlServer + ":" + MainWindow.PortUrlServer + "/api/orders/commit";
                                        ControlWin.LoadUrlServer(MainWindow.UrlAdresServer, MainWindow.CmdStrokaServer);
                                        PreOrderConection.ErorDebag("UrlServerReturnCode = " + MainWindow.UrlServerReturnCode.ToString() + Environment.NewLine + " Строка: " + MainWindow.ResponseFromServer, 2);
                                        if (MainWindow.ReturnCodeServer == 0)
                                        {// печать квитанции по заказу.
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
                                        }
                                        else ConectionUrlServer = false;
                                    }
                                    else ConectionUrlServer = false;
                                    if (ConectionUrlServer == false)
                                    {
                                        PreOrderConection.ErorDebag("возникло исключение:" + Environment.NewLine + " Невозможно оплатить банковскую оплату, нет связи с кассой ", 2);
                                        string TextWindows = "Неможливо підтвердити оплату через банк у зв'язку з відсутністю взаємодії з сервером каси"
                                             + Environment.NewLine + " Відсутність комунікації з сервером каси забезпечує режим оплати тільки готівкою. ";
                                        MessageError NewOrder = new MessageError(TextWindows, 2, 5);
                                        NewOrder.ShowDialog();
                                    }
                                    MainWindow.ApiOrdersAdd = false;
                                    MainWindow.CmdStrokaServer = "";
                                }
                            }


                        }


                    }
                    MainWindow.LoadSetdbpgsql = true;
                }
 


            }


        }
        // Проверка связи с сервером ND2017.
        public static void PingNd2017()
        {

            MainWindow.IdOrderND2017 = 23;
            MainWindow.UrlAdresServer = MainWindow.TcpUrlServer + ":" + MainWindow.PortUrlServer + "/api/sys/ping";  // "/api/orders/get"; //"http://192.168.1.11:8082/api/orders/reverse";
            MainWindow.CmdStrokaServer = "{\"cmd\":\"ping\"}"; // "{\"cmd\":\"get_order\",\"data\":{\"order_id\":" + MainWindow.IdOrderND2017.ToString() + "}}"; // 
            //"{\"cmd\":\"get_order\",\"data\":{\"order_id\":" + MainWindow.IdOrderND2017.ToString() + "}}";
            // Url запрашиваемого методом POST скрипта
            MainWindow.ReturnCodeServer = 0;
            LoadUrlServer(MainWindow.UrlAdresServer, MainWindow.CmdStrokaServer);
            if (MainWindow.ReturnCodeServer == 1)
            {
                string TextWindows = "Шановний замовник! Вибачте. " + Environment.NewLine + " Не встановлено зв'язок с касою."
                       + Environment.NewLine + "Відсутність комунікації з касою не забезпечує режим оплати замовлень. Зверніться до Адміністратора. Робота припинена";
                MessageError NewOrder = new MessageError(TextWindows, 2, 4);
                NewOrder.ShowDialog();

            }
            MainWindow.CmdStrokaServer = "";
        }


        //   Процедура записи выбраного товара во временную таблицу (корзину) тела заказа     
        public static void Addbasket()
        {
            decimal ChangeSum = 0m;
            if (Convert.ToInt32(ListTov[7]) != 0) //    Код калькуляции, которую он возглавляет и которая содержит перечень товаров его составляющих   && MainWindow.db.OrederPreviews.Where(p => p.Idtov == Convert.ToInt32(ListTov[4])).FirstOrDefault() != null
            {

                if ( ListPriceWin.MapOrderList == 1 && MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).Count() !=0) //ListPriceWin.SelectHotdog == 2 &&
                {
                    var OpisTov = MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).FirstOrDefault();
                    MainWindow.db.OrederPreviews.RemoveRange(MainWindow.db.OrederPreviews.Where(c => c.Idtov == OpisTov.Idtov));
                    MainWindow.db.SaveChanges();
                    var Calc = MainWindow.db.Sprcalculattovs.Where(p => p.KodCalc == Convert.ToInt32(ListTov[7])).FirstOrDefault();
                    if (Calc != null)LoadOpisTov(Calc.CalcTov);
  
                }
                //if (ListPriceWin.MapOrderList == 0 && MainWindow.db.OrederPreviews.Where(p => p.Idtov == Convert.ToInt32(ListTov[4])).Count() != 0) //ListPriceWin.SelectHotdog == 2 &&
                //{
                //    var Calc = MainWindow.db.Sprcalculattovs.Where(p => p.KodCalc == Convert.ToInt32(ListTov[7])).FirstOrDefault();
                //    if (Calc != null)LoadOpisTov(Calc.CalcTov);

                //}
            }
            if (Convert.ToBoolean(ListTov[21]) == true) // Товар входит в состав калькуляции
            {
                if (ListPriceWin.MapOrderList == 1 && MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).Count() != 0) 
                {
                    var OpisTov = MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).FirstOrDefault();
                    MainWindow.db.OrederPreviews.RemoveRange(MainWindow.db.OrederPreviews.Where(c => c.Idtov == OpisTov.Idtov));
                    MainWindow.db.SaveChanges();
                    var Calc = MainWindow.db.Sprcalculattovs.Where(p => p.CalcTov == Convert.ToInt32(ListTov[4])).FirstOrDefault();
                    if (Calc != null)LoadOpisTov(Calc.KodTov);
  
                }
                //if (ListPriceWin.MapOrderList == 0 && MainWindow.db.OrederPreviews.Where(p => p.Idtov == Convert.ToInt32(ListTov[4])).Count() != 0)
                //{
                   
                //    var Calc = MainWindow.db.Sprcalculattovs.Where(p => p.CalcTov == Convert.ToInt32(ListTov[4])).FirstOrDefault();
                //    if (Calc != null)LoadOpisTov(Calc.KodTov);
                //}
            }
            //ApplicationContext dbAdd = new ApplicationContext();
            //  Проверяем наличие товара в заказе

            //if (ListPriceWin.MapOrderList == 1 )
           

            var PrevTovChange = ListPriceWin.MapOrderList == 0 ? MainWindow.db.OrederPreviews.Where(p => p.Idtov == Convert.ToInt32(ListTov[4])).FirstOrDefault() :
                MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).FirstOrDefault();
            if (PrevTovChange == null || ListPriceWin.MapOrderList == 0)
            //Товара нет дополняем.
            {

                OrederPreview StrTovAdd = new OrederPreview();
                int AddId = MainWindow.db.OrederPreviews.Count() == 0 ? 1 : MainWindow.db.OrederPreviews.Max(p => p.Id) + 1;
                StrTovAdd.Id = AddId;
                StrTovAdd.Number = 0;
                StrTovAdd.Idtov = Convert.ToInt32(ListTov[4]);
                StrTovAdd.Quantity = Convert.ToDecimal(MainWindow.Kol);
                StrTovAdd.PriceTov = Convert.ToDecimal(ListTov[10]);
                StrTovAdd.SumTov = MainWindow.Cuma;
                StrTovAdd.Discount = Convert.ToDecimal(ListTov[6]); ;
                StrTovAdd.DeleteTov = "D:\\PreOrderWin\\Images\\screen43del.jpg";
                StrTovAdd.ImagePath = ImageFoto;
                string TovName = Convert.ToString(MainWindow.Kol).Trim() + " " + (MainWindow.language_Key == 1 ? ListTov[9] : ListTov[8]) + "  " + (ListPriceWin.SelectHotdog != 0 ? ListPriceWin.NameDog : "")  ;

                // Опции кетчуп майонез перец
                MainWindow.IdCol = MainWindow.IdPaper = MainWindow.IdMayonez = 0;
                var OptionsStr = MainWindow.db.OptionsTovs.Where(p => p.Kodtov == Convert.ToInt32(ListTov[4])).FirstOrDefault();
                if (OptionsStr != null && ListPriceWin.MapOrderList == 0 )
                {
                    var OptionsTov = MainWindow.db.OptionsTovs.Where(p => p.Kodtov == Convert.ToInt32(ListTov[4]));
                    foreach (OptionsTov Stroka in OptionsTov)
                    {
                        if (MainWindow.ColOnOff == 1 && Stroka.KodOpcii == 1)
                        { MainWindow.IdCol = Stroka.Id; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }

                        if (MainWindow.PapricOnOff == 1 && Stroka.KodOpcii == 2)
                        { MainWindow.IdPaper = Stroka.Id; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }
                        if (MainWindow.MayonezOnOff == 1 && Stroka.KodOpcii == 3)
                        { MainWindow.IdMayonez = Stroka.Id; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; } 
                    
                    }

                }
                if (OptionsStr != null && ListPriceWin.MapOrderList == 1)
                {

                    var OptionsTov = MainWindow.db.OptionsTovs.Where(p => p.Kodtov == Convert.ToInt32(ListTov[4]));
                    foreach (OptionsTov Stroka in OptionsTov)
                    {
                        if (MainWindow.ColOnOff == 1 && Stroka.KodOpcii == 1)
                        { MainWindow.IdCol = MainWindow.ColOnOff == 1 ? Stroka.Id : 0; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }

                        if (MainWindow.PapricOnOff == 1 && Stroka.KodOpcii == 2)
                        { MainWindow.IdPaper = MainWindow.PapricOnOff == 1 ? Stroka.Id : 0;  TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }
                        if (MainWindow.MayonezOnOff == 1 && Stroka.KodOpcii == 3)
                        { MainWindow.IdMayonez = MainWindow.MayonezOnOff == 1 ? Stroka.Id : 0; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }

                    }
                    
                   
                }

                StrTovAdd.NameTov = TovName;

                StrTovAdd.Opcii = MainWindow.IdCol.ToString() + ";" + MainWindow.IdPaper.ToString() + ";" + MainWindow.IdMayonez.ToString() + ";"+ (ListPriceWin.SelectHotdog != 0 ? ":"+ ListPriceWin.NameDog : "");

                MainWindow.db.OrederPreviews.Add(StrTovAdd);

                // фиксируем номер заказа и формируем новый для последующих заказов.
                var OrderNumb = MainWindow.db.AdminSetValues.Where(p => p.Id == 1).FirstOrDefault();
                MainWindow.IDOrder = OrderNumb.NumberOrder;

                OrderNumb.NumberOrder = MainWindow.IDOrder + 1;
                MainWindow.db.SaveChanges();

            }
            else
            //  Товар есть корректируем количество и сумму.
            {
                ChangeSum = PrevTovChange.SumTov;
                PrevTovChange.Quantity = (Decimal)MainWindow.Kol;
                PrevTovChange.SumTov = MainWindow.Cuma;
                string TovName = Convert.ToString(MainWindow.Kol) + " " + (MainWindow.language_Key == 1 ? ListTov[9] : ListTov[8]) + "  " + (ListPriceWin.SelectHotdog != 0 ? ListPriceWin.NameDog : "");

                // Опции кетчуп майонез перец
                MainWindow.IdCol = MainWindow.IdPaper = MainWindow.IdMayonez = 0;
                var OptionsStr = MainWindow.db.OptionsTovs.Where(p => p.Kodtov == Convert.ToInt32(ListTov[4])).FirstOrDefault();
                if (OptionsStr != null && ListPriceWin.MapOrderList == 0)
                {
                    var OptionsTov = MainWindow.db.OptionsTovs.Where(p => p.Kodtov == Convert.ToInt32(ListTov[4]));
                    foreach (OptionsTov Stroka in OptionsTov)
                    {
                        if (MainWindow.ColOnOff == 1 && Stroka.KodOpcii == 1)
                        { MainWindow.IdCol = Stroka.Id; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }

                        if (MainWindow.PapricOnOff == 1 && Stroka.KodOpcii == 2)
                        { MainWindow.IdPaper = Stroka.Id; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }
                        if (MainWindow.MayonezOnOff == 1 && Stroka.KodOpcii == 3)
                        { MainWindow.IdMayonez = Stroka.Id; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }

                    }

                }
                if (OptionsStr != null && ListPriceWin.MapOrderList == 1)
                {

                    var OptionsTov = MainWindow.db.OptionsTovs.Where(p => p.Kodtov == Convert.ToInt32(ListTov[4]));
                    foreach (OptionsTov Stroka in OptionsTov)
                    {
                        if (MainWindow.ColOnOff == 1 && Stroka.KodOpcii == 1)
                        { MainWindow.IdCol = Stroka.Id; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }

                        if (MainWindow.PapricOnOff == 1 && Stroka.KodOpcii == 2)
                        { MainWindow.IdPaper = Stroka.Id; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }
                        if (MainWindow.MayonezOnOff == 1 && Stroka.KodOpcii == 3)
                        { MainWindow.IdMayonez = Stroka.Id; TovName += " -" + (MainWindow.language_Key == 1 ? Stroka.NameOptionsEn : Stroka.NameOptionsUk) + ";"; }

                    }


                }

                PrevTovChange.NameTov = TovName;
                PrevTovChange.Opcii = MainWindow.IdCol.ToString() + ";" + MainWindow.IdPaper.ToString() + ";" + MainWindow.IdMayonez.ToString() + ";" + (ListPriceWin.SelectHotdog != 0 ? ":" + ListPriceWin.NameDog : "");

                //записываем изменения
                MainWindow.db.SaveChanges();

            }
            

            MainWindow.SumZakazBasket -= ChangeSum;
            MainWindow.SumZakazBasket += MainWindow.Cuma;
            MainWindow.SumPaymentBasket -= ChangeSum;
            MainWindow.SumPaymentBasket += MainWindow.Cuma;
            MainWindow.SumaOrder -= ChangeSum;
            MainWindow.SumaOrder += MainWindow.Cuma;

        }

        // Запись готового заказа в таблицу заголовка заказов и тело заказа
        public static void AddOreder()
        {
            string StrKvitancii = "";
            //ApplicationContext db = new ApplicationContext();
            int IdTradingFloor = 0;
            // загрузка описания точки торговли
            var TradFloor = MainWindow.db.TradingFloors.Where(p => p.Id == MainWindow.IdTradFloor); // Связать с кодом торговой точки которая работает в этой базе при настройках БД БекОфисе
            foreach (TradingFloor Stroka in TradFloor)
            {
                IdTradingFloor = Stroka.Id;
                string StrokaNameFloor = Stroka.NameFloor.Trim();
                int len = ((32 - StrokaNameFloor.Length) / 2) + StrokaNameFloor.Length;
                if (StrokaNameFloor.Trim().Length <= 32) PaymentOrderWin.TextKvitancii = StrokaNameFloor.Trim() + " \n";
                else
                {
                    StrKvitancii = StrokaNameFloor.Substring(0, 32);
                    int LastSpace = StrKvitancii.LastIndexOf(" ");
                    PaymentOrderWin.TextKvitancii =  StrokaNameFloor.Substring(0, LastSpace) + " \n";
                    string OstStroka = StrokaNameFloor.Substring(LastSpace+1, StrokaNameFloor.Length - (LastSpace + 1)) ;
                    len = ((32 - OstStroka.Length) / 2) + OstStroka.Length;
                    PaymentOrderWin.TextKvitancii += OstStroka.PadLeft(len).PadRight(32) + " \n";
                    PaymentOrderWin.StrAddZagolov += 15.0F;
                }

              
                len = ((32 - ("ПН " + Stroka.IPN).Length) / 2) + ("ПН " + Stroka.IPN).Length;
                PaymentOrderWin.TextKvitancii += ("ПН " + Stroka.IPN).PadLeft(len).PadRight(32) + "\n";

                len = ((32 - Stroka.AdrFloor.Length) / 2) + Stroka.AdrFloor.Length;
                if (Stroka.AdrFloor.Length <= 32) PaymentOrderWin.TextKvitancii += Stroka.AdrFloor.PadLeft(len).PadRight(32) + "\n ";
                else 
                {
                    StrKvitancii = Stroka.AdrFloor.Substring(0, 32);
                    int LastSpace = StrKvitancii.LastIndexOf(" ");
                    PaymentOrderWin.TextKvitancii += Stroka.AdrFloor.Substring(0, LastSpace) + " \n";
                    string OstStroka = Stroka.AdrFloor.Substring(LastSpace+1, Stroka.AdrFloor.Length - (LastSpace+1)) ;
                    len = ((32 - OstStroka.Length) / 2) + OstStroka.Length;
                    PaymentOrderWin.TextKvitancii += OstStroka.PadLeft(len).PadRight(32) + " \n";
                    PaymentOrderWin.StrAddZagolov += 15.0F;
                }
                len = ((32 - ("Телефон закладу:" + Stroka.Tel).Length) / 2) + ("Телефон закладу:" + Stroka.Tel).Length;
                if (("Телефон закладу:" + Stroka.Tel).Length <=32) PaymentOrderWin.TextKvitancii += ("Телефон закладу:" + Stroka.Tel).PadLeft(len).PadRight(32) + "\n";
                else
                {
                    PaymentOrderWin.TextKvitancii +=  "Телефон закладу:"+Stroka.Tel.Substring(0, (32-("Телефон закладу:").Length)) + " \n";
                    PaymentOrderWin.TextKvitancii += Stroka.Tel.Substring(("Телефон закладу:").Length+1,Stroka.Tel.Length- (("Телефон закладу:").Length+1)) + " \n";
                    PaymentOrderWin.StrAddZagolov += 15.0F;
                }
                len = ((32 - ("Телефон гарячої лінії:" ).Length) / 2) + ("Телефон гарячої лінії:" ).Length;
                PaymentOrderWin.TextKvitancii += ("Телефон гарячої лінії:").PadLeft(len).PadRight(32) + "\n";
                if (Stroka.TelFair.Trim().Length > 0)
                { 
                    PaymentOrderWin.TextKvitancii += Stroka.TelFair.PadLeft((32 - Stroka.TelFair.Length) / 2).PadRight((32 - Stroka.TelFair.Length) / 2) + "\n";
                    PaymentOrderWin.StrAddZagolov += 15.0F;
                } 
                //else PaymentOrderWin.StrAddZagolov -= 15.0F;
                PaymentOrderWin.TextKvitancii += "Грошова одиниця: грн. ";
                PaymentOrderWin.TextKvitancii0 += "  Замовлення: № " + MainWindow.IdOrderND2017.ToString() + "\n ";

            }

            if (MainWindow.RunServer == false) MainWindow.IdOrderND2017 = MainWindow.IDOrder;
                //  Определяем номер нового заказа          
                //if (MainWindow.db.HeadZakazs.Count() == 0 && MainWindow.db.ContentZakazs.Count() == 0) MainWindow.NumberHdZakaz = 1;
            if (((MainWindow.db.ContentZakazs.Count() != 0 && MainWindow.db.HeadZakazs.Count() == 0) || (MainWindow.db.ContentZakazs.Count() == 0 && MainWindow.db.HeadZakazs.Count() != 0)))
            {
                // Удаление тела заказа
                MainWindow.db.HeadZakazs.RemoveRange(MainWindow.db.HeadZakazs.ToList());
                MainWindow.db.ContentZakazs.RemoveRange(MainWindow.db.ContentZakazs.ToList());
                //  Сохранить изменения
                MainWindow.db.SaveChanges();

            }
          
            // Добавляем товар в тело заказа
            int PozitionTov = 0;
            int CountContent = MainWindow.db.ContentZakazs.Count();
            string TovName = "";
            ContentZakaz StrTovAdd = new ContentZakaz();
            foreach (OrederPreview ListPreviewOrder in MainWindow.db.OrederPreviews.ToList<OrederPreview>())
            {
                // Разбиваем название товара на несколько строк
                LoadOpisTov(ListPreviewOrder.Idtov);
                StrKvitancii = ""; 
                TovName = ListPreviewOrder.NameTov;
                if (TovName.Length < 20)
                {
                    PaymentOrderWin.TextKvitancii1 += TovName.PadRight(20) + ListPreviewOrder.PriceTov.ToString().PadLeft(6) + ListPreviewOrder.SumTov.ToString().PadLeft(6) + "\n";
                    PaymentOrderWin.StrAdd += 15.0F;
                }
                else
                {
                    int LengthName = TovName.Length, ind = 0, StartSimvol = 0, LengthSimvol = 20;

                    for (ind = 1; ind <= 4; ind++)
                    {
                        StrKvitancii += TovName.Substring(StartSimvol, LengthSimvol) + ((LengthSimvol == 20 && ind < 4) ? "\n" : "");
                        if (LengthSimvol == 20 && ind < 4)
                        {
                            PaymentOrderWin.TextKvitancii1 += StrKvitancii;
                            StrKvitancii = "";
                            PaymentOrderWin.StrAdd += 15.0F;
                        }

                        if (LengthName - 20 <= 0)
                        {
                            LengthSimvol = 0;
                            break;
                        }

                        else
                        {
                            LengthName -= 20;
                            StartSimvol += 20;
                            LengthSimvol = ((20 - LengthName) <= 20 && (20 - LengthName) > 0) ? LengthName : 20;

                        }
                    }

                    PaymentOrderWin.TextKvitancii1 += StrKvitancii.PadRight(20) + ListPreviewOrder.PriceTov.ToString().PadLeft(6) + ListPreviewOrder.SumTov.ToString().PadLeft(6) + "\n";
                    PaymentOrderWin.StrAdd += 15.0F;

                }

                int AddId = CountContent == 0 ? 1 : MainWindow.db.ContentZakazs.Max(p => p.Id) + 1;

                try
                {
                    StrTovAdd.Id = AddId;
                    StrTovAdd.Number = MainWindow.IdOrderND2017;
                    StrTovAdd.Kodtov = Convert.ToInt32(ListTov[6]); // ArtTov для кассы ListPreviewOrder.Idtov;
                    StrTovAdd.TovCalc = Convert.ToInt32(ListTov[7]);
                    StrTovAdd.Quantity = ListPreviewOrder.Quantity;
                    StrTovAdd.Opcii = ListPreviewOrder.Opcii;
                    StrTovAdd.PriceTov = ListPreviewOrder.PriceTov;
                    StrTovAdd.SumTov = ListPreviewOrder.SumTov;
                    StrTovAdd.Timeissue = Convert.ToInt16(ListTov[19]) * 60;
                    StrTovAdd.NameTovUK = ListTov[8]+" -"+(ListPreviewOrder.Opcii.Contains(";:") ? ListPreviewOrder.Opcii.Substring(ListPreviewOrder.Opcii.IndexOf(";:")+2, ListPreviewOrder.Opcii.Length-(ListPreviewOrder.Opcii.IndexOf(";:") + 2)) : "" ); // ListPreviewOrder.NameTov;
                    StrTovAdd.Status = 1;
                    MainWindow.db.ContentZakazs.Add(StrTovAdd);
                    MainWindow.db.SaveChanges();
                }
                catch (Exception ex)
                {

                    MainWindow.ExMessage = ex.Message;
                    PreOrderConection.ErorDebag("Ошибка: " + MainWindow.ExMessage, 2);

                }
                try
                {
                    ApplicationContext dbCalc = new ApplicationContext();
                    int CountQueue = dbCalc.Queuereserves.Count();
                    int CountCalc = MainWindow.db.CompositionTovs.Where(p => p.KodCalc == Convert.ToInt16(ListTov[7])).Count();
                    if (CountCalc != 0)
                    {

                        var ActCalcul = dbCalc.Sprcalculattovs.Where(p => p.KodCalc == Convert.ToInt16(ListTov[7]));
                        foreach (Sprcalculattov strzap in ActCalcul)
                        {
                            ApplicationContext dbtovBaz = new ApplicationContext();
                            int CountTovCalc = dbtovBaz.CompositionTovs.Where(p => p.IdtovBaz == strzap.CalcTov).Count();
                            if (CountTovCalc != 0)
                            {
                                Queuereserve StrQueuereserv = new Queuereserve();
                                var ActTovCalc = dbtovBaz.CompositionTovs.Where(p => p.IdtovBaz == strzap.CalcTov).FirstOrDefault();
                                StrQueuereserv.Id = CountQueue == 0 ? 1 : dbtovBaz.Queuereserves.Max(p => p.Id) + 1;
                                StrQueuereserv.Number = MainWindow.IdOrderND2017;
                                StrQueuereserv.KodCalc = Convert.ToInt16(ListTov[7]);
                                StrQueuereserv.IdtovBaz = ActTovCalc.IdtovBaz; //strzap.CalcTov;
                                StrQueuereserv.IdtovReplace = HotDog.PointBunWait == 0 ? ActTovCalc.IdtovReplace : 0;
                                dbtovBaz.Queuereserves.Add(StrQueuereserv);
                                dbtovBaz.SaveChanges();
                                PreOrderConection.ErorDebag("Запись " + ActTovCalc.IdtovReplace.ToString() + " " + ActTovCalc.IdtovBaz.ToString(), 3);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MainWindow.ExMessage = ex.Message;
                    PreOrderConection.ErorDebag("Ошибка: " + MainWindow.ExMessage, 2);

                }
                //MessageBox.Show("Content Ok");
                PozitionTov++;
                CountContent++;

                var opistov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ListPreviewOrder.Idtov).FirstOrDefault();
                opistov.OstTov -= ListPreviewOrder.Quantity;
                MainWindow.db.SaveChanges();
            }

            PaymentOrderWin.TextKvitancii11 += "Разом:".PadRight(26) + MainWindow.SumaOrder.ToString().PadLeft(6) + "\n";
            PaymentOrderWin.StrAddItg += 15.0F;
            if (MainWindow.SumDiscountBasket != 0 || MainWindow.SumSkidka != 0)
            {

                PaymentOrderWin.TextKvitancii11 += "Сплатити бонусами:".PadRight(26) + MainWindow.SumDiscountBasket.ToString().PadLeft(6) + "\n";
                PaymentOrderWin.TextKvitancii11 += "Знижка:".PadRight(26) + MainWindow.SumSkidka.ToString().PadLeft(6) + "\n";
                PaymentOrderWin.TextKvitancii11 += "До сплати:".PadRight(26) + MainWindow.SumaFinish.ToString().PadLeft(6) + "\n";
                PaymentOrderWin.StrAddItg += 45.0F;
            }

            // Добавляем заголовок заказа
            
            var StrContentOrder = MainWindow.db.ContentZakazs.Where(p => p.Number == MainWindow.IdOrderND2017);
            var HeadUpOrder = MainWindow.db.HeadZakazs.Where(p => p.IdOrder == MainWindow.IdOrderND2017).FirstOrDefault();
            //MessageBox.Show("StrContentOrder =" + StrContentOrder.ToString());
            if (MainWindow.db.HeadZakazs.Where(p => p.IdOrder == MainWindow.IdOrderND2017).FirstOrDefault() == null)
            {
                HeadZakaz StrHead = new HeadZakaz();
                //  заказа нет, дополняем.
                PreOrderConection.ErorDebag("Номер заказа."+ MainWindow.IDOrder.ToString()+ "Товары "+ PozitionTov.ToString(), 3);

                int MemNumberOrder = MainWindow.db.HeadZakazs.Count() == 0 ? 1 : MainWindow.db.HeadZakazs.Max(p => p.Id) + 1;
                //MessageBox.Show(MemNumberOrder.ToString()+"  "+ MainWindow.IdOrderND2017.ToString());
                StrHead.Id = MemNumberOrder;
                StrHead.IdOrder = MainWindow.IdOrderND2017;
                StrHead.IdFloor = IdTradingFloor;
                StrHead.Number = MainWindow.IDOrder;
                StrHead.SumZakaz = MainWindow.SumZakazBasket;
                StrHead.SumPayment = MainWindow.SumaFinish;
                StrHead.SumDiscount = MainWindow.SumDiscountBasket;
                StrHead.SumBonusLoyalti = MainWindow.SumSkidka;
                StrHead.PlaceService = MainWindow.RestHier;
                StrHead.Status = 1;
                StrHead.Payment = MainWindow.CashBank;
                StrHead.BankCart = NumberBankKart.ToString();
                StrHead.NumbTRN = NumberTranzakcii.ToString();
                StrHead.TerminalID = TerminalID;
                StrHead.NumbCheck = InvoiceNum.ToString();
                StrHead.KodAvtoriz = KodAvtor.ToString();
                StrHead.ShtrihKod = "";
                StrHead.DataTimeBegin = DateTime.Today;
                StrHead.DataTimePrint = DateTime.Now;
                StrHead.DataTimeEnd = DateTime.Now;

               
                StrHead.IdKartLoyalty = DiscountWin.DiscountBarCod;
                StrHead.QuantityPozitionTov = PozitionTov;
                MainWindow.db.HeadZakazs.Add(StrHead);
            }
            else
            {
                //  Заказ есть корректируем количество и сумму.
                foreach (ContentZakaz ListOrder in MainWindow.db.ContentZakazs.Where(p => p.Number == MainWindow.NumberHdZakaz).ToList<ContentZakaz>())
                {
                    MainWindow.SumZakazBasket += ListOrder.SumTov;
                }

                MainWindow.SumPaymentBasket = MainWindow.SumZakazBasket - MainWindow.SumDiscountBasket;

                HeadUpOrder.SumZakaz = MainWindow.SumZakazBasket;
                HeadUpOrder.SumPayment = MainWindow.SumPaymentBasket;
                HeadUpOrder.SumDiscount = MainWindow.SumDiscountBasket;

            }
            // записываем изменения
            MainWindow.db.SaveChanges();

            PaymentOrderWin.StrAddBank = 0;
            if (MainWindow.CashBank == 1)
            {
                    

                PaymentOrderWin.TextKvitancii2 += "     Заплатити на касі\n";
                PaymentOrderWin.TextKvitancii2 += "    готівкою чи карткою.\n";
                //PaymentOrderWin.TextKvitancii2 += "         Замовлення:\n";
                //PaymentOrderWin.TextKvitancii2 += "               № " + MainWindow.IdOrderND2017.ToString();
  
            }
            else
            {

                PaymentOrderWin.TextKvitancii12 += "№ Б.карти: "+ NumberBankKart.ToString()+"\n";
                PaymentOrderWin.TextKvitancii12 += "№ Транзакції RRN:" + NumberTranzakcii.ToString()+ "\n";
                PaymentOrderWin.TextKvitancii12 += "№ чека у терміналі:"+ InvoiceNum + "\n";
                PaymentOrderWin.TextKvitancii12 += "№ Код авторизації:" + KodAvtor + "\n";
                PaymentOrderWin.TextKvitancii12 += "Термінал:" + TerminalID + "\n";
                PaymentOrderWin.StrAddBank = 75.0F;

                PaymentOrderWin.TextKvitancii2 += "        Чек сплачено.\n";
                
            }
 

            PaymentOrderWin.IdHeadZakaz = MainWindow.db.HeadZakazs.Count() + 1;

            PaymentOrderWin.TextKvitancii3 += "_______________________________\n";
            string IdHead = PaymentOrderWin.IdHeadZakaz.ToString();
            char pad = '0';
            PaymentOrderWin.TextKvitancii3 += IdHead.PadLeft(6, pad) + "                    " + DateTime.Now.ToString("dd.MM.yyyy") + " " + DateTime.Now.ToString("HH.mm") + "\n ";

               
            if (DiscountWin.DiscountBarCod.Length != 0 || MainWindow.SumDiscountBasket != 0)

            {
                PaymentOrderWin.TextKvitancii3 += "       Номер карти лояльності: \n ";
                PaymentOrderWin.TextKvitancii3 += "                 " + DiscountWin.DiscountBarCod + " \n ";
                PaymentOrderWin.TextKvitancii3 += "       Кількість бонусів: "+MainWindow.BonusValue.ToString()+"\n ";
                PaymentOrderWin.StrAddDisc = 45.0F;
            }

               
            PaymentOrderWin.TextKvitancii3 += "               Дякуємо за візит\n ";
            PaymentOrderWin.TextKvitancii4 += "       Службова Квітанція\n ";

       

            // Удаление тела заказа
            MainWindow.db.OrederPreviews.RemoveRange(MainWindow.db.OrederPreviews.ToList());
            //  Сохранить изменения
            MainWindow.db.SaveChanges();
            MainWindow.SumZakazBasket = 0;
            MainWindow.NumberHdZakaz = 0;

            //MessageBox.Show(MainWindow.SumZakazBasket.ToString());


        }

        // Загрузка описания товара в массив для отображения в окне выбора
        public static void LoadOpisTov(int KodOpisTov =0, bool UpdateLoad = true)
        {
            OnOffConectionGrup = 0;
            OnOffConectionVid = 0;
            OnOffConectionPvid = 0;
            CompositionTovIdtovBaz = 0;
            OnOffCompositionTov = 0;
            CompositionTovIdtovReplace = 0;
            if (UpdateLoad == true)
            { 
                  for (int i = 0; i < 12; i++) { ListConectionGrup[i]=ListConectionVid[i]= ListConectionPvid[i] = 0;}          
            }

 

            var opistov = MainWindow.db.Sprtovs.Where(p => p.KodTov == KodOpisTov);

            foreach (Sprtov pole in opistov)
            {
                ListTov[0] = pole.Id.ToString();
                ListTov[1] = pole.KodGrup.ToString();
                ListTov[2] = pole.Kodvid.ToString();
                ListTov[3] = pole.Kodpvid.ToString();
                ListTov[4] = pole.KodTov.ToString();
                ListTov[5] = pole.Uktved.ToString();
                ListTov[6] = pole.ArtTov;
                ListTov[7] = pole.KodCalc.ToString();
                ListTov[8] = pole.NameTovUK;
                ListTov[9] = pole.NameTovEN;
                ListTov[10] = pole.PriceTov.ToString();
                ListTov[11] = pole.OstTov.ToString();
                ListTov[12] = ""; //pole.IDFotoTov.ToString();
                ListTov[13] = pole.IdConcomtov.ToString();
                ListTov[14] = pole.IdOptions.ToString();
                ListTov[15] = pole.IdInfo.ToString();
                ListTov[16] = pole.Opis;
                ListTov[17] = pole.ImagePath;
                ListTov[18] = pole.SaleStop.ToString();
                ListTov[19] = pole.Timeissue.ToString();
                ListTov[20] = pole.OpisEn.ToString();
                ListTov[21] = pole.PartCalc.ToString();
                ImageFoto = pole.IDFotoTov;

            }
            int ind = 0;

            if (UpdateLoad == true)
            { 
                // Определяем есть ли Сопутствующие товары
                
                var Conection = MainWindow.db.Sprconcomtovs.Where(p => p.Concomtov == KodOpisTov);
                foreach (Sprconcomtov pole in Conection)
                {
                    ListConectionGrup[ind] = pole.KodGrup;
                    ListConectionVid[ind] = pole.Kodvid;
                    ListConectionPvid[ind] = pole.Kodpvid;
                    ListConectionTov[ind] = pole.KodTov;

                    if (pole.KodGrup != 0) OnOffConectionGrup++;
                    if (pole.Kodvid != 0) OnOffConectionVid++;
                    if (pole.Kodpvid != 0) OnOffConectionPvid++;
                    if (pole.KodTov != 0) OnOffConectionTov++;
                    ind++;
                }          
            
            }

            // Определяем входит ли товар в калькуляцию и есть ли у него взаимозаменяемые товары.
            ind = -1;
            if (Convert.ToInt16(ListTov[7]) != 0)
            {
                var Calculat = MainWindow.db.Sprcalculattovs.Where(p => p.KodTov == KodOpisTov);

                foreach (Sprcalculattov pole in Calculat)
                {
                    ind++;
                    ListCompositionCalc[ind] = pole.KodCalc;
                    ListCompositionTov[ind] = pole.CalcTov;

                    OnOffCompositionTov++;
                    
                }

                if (OnOffCompositionTov != 0)
                {

                    for (int i = 0; i <= ind; i++)
                    { 
                        var CalculatTov = MainWindow.db.Sprcalculattovs.Where(p => p.KodCalc == ListCompositionCalc[i]);

                        foreach (Sprcalculattov pole in CalculatTov)
                        {
                            CompositionTovIdtovBaz = pole.KodTov;
                            CompositionTovIdtovReplace = pole.CalcTov;
                            break;

                        }
                    }

                }


            }
            if (Convert.ToBoolean(ListTov[21]) == true)
            {
                var Calculat = MainWindow.db.Sprcalculattovs.Where(p => p.CalcTov == KodOpisTov);

                foreach (Sprcalculattov pole in Calculat)
                {
                    ind++;
                    ListCompositionCalc[ind] = pole.KodCalc;
                    ListCompositionTov[ind] = pole.KodTov;

                    OnOffCompositionTov++;

                }

                if (OnOffCompositionTov != 0)
                {

                    for (int i = 0; i <= ind; i++)
                    {
                        var CalculatTov = MainWindow.db.Sprcalculattovs.Where(p => p.KodCalc == ListCompositionCalc[i]);

                        foreach (Sprcalculattov pole in CalculatTov)
                        {
                            CompositionTovIdtovBaz = pole.KodTov;
                            CompositionTovIdtovReplace = pole.CalcTov;
                            break;

                        }
                    }

                }


            }
            // Настройка на выбор составляющих хот дога.  из таблицы взаимозаменяемых составляющих или по умолчанию из текстовых литералов
            HotDog.NameBunWait = MainWindow.language_Key == 1 ? "light bun" : "світла булочка";
            HotDog.NameBunBlack = MainWindow.language_Key == 1 ? "dark bun" : "темна булочка";
            // Список талиц БД
            int CountTable = MainWindow.db.Model.GetEntityTypes().Select(t => t.GetTableName()).Distinct().Count();
            var tableNames = MainWindow.db.Model.GetEntityTypes().Select(t => t.GetTableName()).Distinct().ToList();
            for (int indtab = 0; ind < CountTable-1; indtab++)
            {
                if (tableNames[indtab] == "HotDogUnits")
                {
                    var Unit = MainWindow.db.HotDogUnits.Where(p => p.Kodtov == KodOpisTov).ToList();
                    if (Unit.Count != 0)
                    {
                        CompositionTovIdtovBaz = KodOpisTov;
                        foreach (HotDogUnit pole in Unit)
                        {
                            if (pole.KodUnit == 1) HotDog.NameBunWait = MainWindow.language_Key == 1 ? pole.NameUnitEn : pole.NameUnitUk;
                            if (pole.KodUnit == 2) HotDog.NameBunBlack = MainWindow.language_Key == 1 ? pole.NameUnitEn : pole.NameUnitUk;
                        }
                    }
                    break;
                }
            }
            // счтитвание описания схемы Бд в виде таблицы данных. 
            //using (var ctx = new ApplicationContext())
            //{
            //    DataTable dbSchemaWithTables = ctx.Database.GetDbConnection().GetSchema("Tables");
            //    int table = dbSchemaWithTables.Rows.Count;
            //    for (int index = 0; ind < table; index++)
            //    {
            //        string NameT = dbSchemaWithTables.Rows[index]["table_name"].ToString();
            //        if (NameT == "HotDogUnits") return;
            //    }
                
            //}

        
        }
        // Удаление (очистка) корзины.
        public static void DeleteOrderPreview()
        {
            PreOrderConection.ErorDebag("RemoveRange", 3);
            //MessageBox.Show("RemoveRange");
            // Удаление тела заказа
            try
            {
                MainWindow.db.OrederPreviews.RemoveRange(MainWindow.db.OrederPreviews.Where(c => c.Number == 0));
                //  Сохранить изменения
                MainWindow.db.SaveChanges();

            }
            catch (Exception ex)
                {
                MessageBox.Show(ex.Message);
                MainWindow.ExMessage = ex.Message;
                    PreOrderConection.ErorDebag("Ошибка: " + MainWindow.ExMessage, 2);

                }
    
            PreOrderConection.ErorDebag("Удаление предзаказа (очистка) корзины.", 3);


        }


        // Удаление заказа из очереди заказов
        public static void DeleteOrder()
        {
            //ApplicationContext db = new ApplicationContext();
            // Удаление заголовка заказа
            var Strhead = MainWindow.db.HeadZakazs.Where(p => p.Number == MainWindow.NumberHdZakaz).FirstOrDefault();
            MainWindow.db.HeadZakazs.Remove((HeadZakaz)Strhead);

            // Удаление тела заказа
            MainWindow.db.ContentZakazs.RemoveRange(MainWindow.db.ContentZakazs.Where(c => c.Number == MainWindow.NumberHdZakaz));

            //  Сохранить изменения
            MainWindow.db.SaveChanges();
            PreOrderConection.ErorDebag("Удаление заказа из очереди заказов, отказ от оплаты", 3);
        }








        // загрузка справочника товаров для тестирования
        public static void DataTest()
        {
            
            // Справочник товаров
            int[] Listgrup = new int[43];
            string[] ListFoto = new string[43];

            //if (MainWindow.db.Sprtovs.Count() == 0)
            //{

            //    for (int i = 0; i < 11; i++)
            //    {
            //        ListKodTov[i] = i + 1;

            //    }


            //    ListKodTov[1] = 0; ListKodTov[2] = 0; ListKodTov[3] = 0; ListKodTov[4] = 0; ListKodTov[5] = 0; ListKodTov[6] = 0; ListKodTov[7] = 0; ListKodTov[8] = 0;
            //    ListKodTov[9] = 0; ListKodTov[10] = 0; ListKodTov[11] = 0; ListKodTov[12] = 0; ListKodTov[13] = 0; ListKodTov[14] = 0; ListKodTov[15] = 0;


            //    ListNameEn[0] = "Hod-dog Mr.G Chicken"; ListNameEn[1] = "Hod-dog Mr.G Beef"; ListNameEn[2] = "Hod-dog Mr.G Jaeger with cheese"; ListNameEn[3] = "Hot Dog Mr.G Premier L.SIZE";
            //    ListNameEn[4] = "Mr.G Ukrainian hot dog with paprika"; ListNameEn[5] = "Hod-dog Mr.G Chicken"; ListNameEn[6] = "Hot dog in a tortilla";
            //    ListNameEn[7] = "Hod-dog Mr.G Beef"; ListNameEn[8] = "Hod-dog Mr.G Jaeger with cheese";
            //    ListNameEn[9] = "Hot Dog Mr.G Premier L.SIZE"; ListNameEn[10] = "Mr.G Ukrainian hot dog with paprika";


            //    ListNameEn[11] = "Hot Dog";
            //    ListNameEn[12] = "Burger"; ListNameEn[13] = "Burger bif"; ListNameEn[14] = "Burger chiken"; ListNameEn[15] = "Hot Dog";

            //    //ListNameEn[16] = "Burger"; ListNameEn[17] = "Burger bif"; ListNameEn[18] = "Burger chiken"; ListNameEn[19] = "Hot Dog";
            //    //ListNameEn[20] = "Burger"; ListNameEn[21] = "Burger bif"; ListNameEn[22] = "Burger chiken"; ListNameEn[23] = "Hot Dog";
            //    //ListNameEn[24] = "Burger"; ListNameEn[25] = "Burger bif"; ListNameEn[26] = "Burger chiken"; ListNameEn[27] = "Hot Dog";
            //    //ListNameEn[28] = "Ays Late"; ListNameEn[29] = "Ays Americano"; ListNameEn[30] = "Mors Fruct"; ListNameEn[31] = "Limonad";
            //    //ListNameEn[32] = "Ays Late"; ListNameTov[33] = "Ays Americano";
            //    //ListNameEn[34] = "Cartoplya pyure"; ListNameEn[35] = "Cartoplya zapechena"; ListNameEn[36] = "Cartoplya fri";
            //    //ListNameEn[37] = "Pirog ovochi";
            //    //ListNameEn[38] = "Ovochi myaso"; ListNameEn[39] = "Solyanka"; ListNameTov[40] = "DARK BUN";
            //    //ListNameEn[41] = "ketchup"; ListNameEn[42] = "Mayonnaise";


            //    ListNameTov[0] = "Ход - дог Mr.G Курячий"; ListNameTov[1] = "Ход - дог Mr.G Яловичий"; ListNameTov[2] = "Ход - дог Mr.G Єгерський з сиром";
            //    ListNameTov[3] = "Хот - дог Mr.G Прем'єрський L.SIZE";
            //    ListNameTov[4] = "Хот - дог Mr.G Український з паприкою"; ListNameTov[5] = "Хот - дог в тортильї"; ListNameTov[6] = "Ход - дог Mr.G Курячий";

            //    ListNameTov[7] = "Ход - дог Mr.G Яловичий"; ListNameTov[8] = "Ход - дог Mr.G Єгерський з сиром";
            //    ListNameTov[9] = "Хот - дог Mr.G Прем'єрський L.SIZE"; ListNameTov[10] = "Хот - дог Mr.G Український з паприкою";



            //    ListNameTov[11] = "Бургер"; ListNameTov[12] = "Бургер Біф"; ListNameTov[13] = "Бургер чікен"; ListNameTov[14] = "Хот дог";
            //    //ListNameTov[15] = "Бургер"; ListNameTov[16] = "Бургер Біф"; ListNameTov[17] = "Бургер чікен"; ListNameTov[18] = "Бургер Біф";

            //    //ListNameTov[19] = "Бургер"; ListNameTov[20] = "Бургер Біф"; ListNameTov[21] = "Бургер чікен"; ListNameTov[22] = "Хот дог";
            //    //ListNameTov[23] = "Бургер";

            //    //ListNameTov[24] = "Бургер"; ListNameTov[25] = "Бургер Біф"; ListNameTov[26] = "Бургер чікен"; ListNameTov[27] = "Хот дог";

            //    //ListNameTov[28] = "Айс Лате"; ListNameTov[29] = "Айс Американо"; ListNameTov[30] = "Морс Фруктовый"; ListNameTov[31] = "Лимонад";
            //    //ListNameTov[32] = "Айс Лате"; ListNameTov[33] = "Айс Американо";

            //    //ListNameTov[34] = "Картопляне пюре"; ListNameTov[35] = "Картопля запечена із зеленню"; ListNameTov[36] = "Картопля фрі";
            //    //ListNameTov[37] = "Пирог з овочами";

            //    //ListNameTov[38] = "Овочі з м'ясом в соусі"; ListNameTov[39] = "Солянка"; ListNameTov[40] = "темна булочка";

            //    //ListNameTov[41] = "кетчуп"; ListNameTov[42] = "майонез"; 



            //    ListPriceTov[0] = 44M; ListPriceTov[1] = 44M; ListPriceTov[2] = 44M; ListPriceTov[3] = 39M;
            //    ListPriceTov[4] = 44M; ListPriceTov[5] = 55M; ListPriceTov[6] = 44M; ListPriceTov[7] = 44M; ListPriceTov[8] = 44M;
            //    ListPriceTov[9] = 39M; ListPriceTov[10] = 44M;


            //    ListPriceTov[11] = 65M; ListPriceTov[12] = 58M; ListPriceTov[13] = 60.0M; ListPriceTov[14] = 65M; ListPriceTov[15] = 65M;
            //    //ListPriceTov[16] = 60M; ListPriceTov[17] = 65M; ListPriceTov[18] = 65M; ListPriceTov[19] = 35M;
            //    //ListPriceTov[20] = 31.5M; ListPriceTov[21] = 21.0M; ListPriceTov[22] = 18.0M; ListPriceTov[23] = 28.0M; ListPriceTov[24] = 32.0M; ListPriceTov[25] = 13.0M;
            //    //ListPriceTov[26] = 65M; ListPriceTov[27] = 65M; ListPriceTov[28] = 58M; ListPriceTov[29] = 60.0M; ListPriceTov[30] = 65M; ListPriceTov[31] = 65M;
            //    //ListPriceTov[32] = 60M; ListPriceTov[33] = 65M; ListPriceTov[34] = 65M; ListPriceTov[35] = 35M;
            //    //ListPriceTov[36] = 31.5M; ListPriceTov[37] = 21.0M; ListPriceTov[38] = 18.0M; ListPriceTov[39] = 28.0M;
            //    //ListPriceTov[40] = 21.0M; ListPriceTov[41] = 18.0M; ListPriceTov[42] = 28.0M;


            //    Listgrup[0] = 1; Listgrup[1] = 1; Listgrup[2] = 1; Listgrup[3] = 1; Listgrup[4] = 1; Listgrup[5] = 1;

            //    Listgrup[6] = 1; Listgrup[7] = 1; Listgrup[8] = 1; Listgrup[9] = 1; Listgrup[10] = 1;


            //    Listgrup[11] = 2; Listgrup[12] = 2; Listgrup[13] = 2; Listgrup[14] = 2; Listgrup[15] = 2;
            //    //Listgrup[16] = 2; Listgrup[17] = 2; Listgrup[18] = 2;

            //    //Listgrup[19] = 3; Listgrup[20] = 3; Listgrup[21] = 3; Listgrup[22] = 3; Listgrup[23] = 3;

            //    //Listgrup[24] = 4; Listgrup[25] = 4; Listgrup[26] = 4; Listgrup[27] = 4;

            //    //Listgrup[28] = 5; Listgrup[29] = 5; Listgrup[30] = 5; Listgrup[31] = 5; Listgrup[32] = 5; Listgrup[33] = 5;

            //    //Listgrup[34] = 6; Listgrup[35] = 6; Listgrup[36] = 6; Listgrup[37] = 6;

            //    //Listgrup[38] = 7; Listgrup[39] = 7;

            //    //Listgrup[40] = 6; Listgrup[41] = 6; Listgrup[42] = 6;


            //    ListFoto[0] = "D:\\PreOrderWin\\Images\\HotDogNorma.png"; ListFoto[1] = "D:\\PreOrderWin\\Images\\HotDogNorma.png";
            //    ListFoto[2] = "D:\\PreOrderWin\\Images\\HotDogNorma.png"; ListFoto[3] = "D:\\PreOrderWin\\Images\\HotDogNorma.png";
            //    ListFoto[4] = "D:\\PreOrderWin\\Images\\HotDogNorma.png"; ListFoto[5] = "D:\\PreOrderWin\\Images\\vlavashe-min.png";
            //    ListFoto[6] = "D:\\PreOrderWin\\Images\\HotDogNorma.png";
            //    ListFoto[7] = "D:\\PreOrderWin\\Images\\HotDogNorma.png";
            //    ListFoto[8] = "D:\\PreOrderWin\\Images\\HotDogNorma.png";
            //    ListFoto[9] = "D:\\PreOrderWin\\Images\\HotDogNorma.png"; ListFoto[10] = "D:\\PreOrderWin\\Images\\HotDogNorma.png";

            //    ListFoto[11] = "D:\\PreOrderWin\\Images\\HotDog.jpg";
            //    ListFoto[12] = "D:\\PreOrderWin\\Images\\Burger.jpg"; ListFoto[13] = "D:\\PreOrderWin\\Images\\BurgerBif.jpg";
            //    ListFoto[14] = "D:\\PreOrderWin\\Images\\BurgerChiken.jpg"; ListFoto[15] = "D:\\PreOrderWin\\Images\\HotDog.jpg";
            //    //ListFoto[16] = "D:\\PreOrderWin\\Images\\Burger.jpg"; ListFoto[17] = "D:\\PreOrderWin\\Images\\BurgerBif.jpg";
            //    //ListFoto[18] = "D:\\PreOrderWin\\Images\\BurgerChiken.jpg"; ListFoto[19] = "D:\\PreOrderWin\\Images\\HotDog.jpg";
            //    //ListFoto[20] = "D:\\PreOrderWin\\Images\\Burger.jpg"; ListFoto[21] = "D:\\PreOrderWin\\Images\\BurgerBif.jpg";
            //    //ListFoto[22] = "D:\\PreOrderWin\\Images\\BurgerChiken.jpg"; ListFoto[23] = "D:\\PreOrderWin\\Images\\HotDog.jpg";
            //    //ListFoto[24] = "D:\\PreOrderWin\\Images\\Burger.jpg"; ListFoto[25] = "D:\\PreOrderWin\\Images\\BurgerBif.jpg";
            //    //ListFoto[26] = "D:\\PreOrderWin\\Images\\BurgerChiken.jpg"; ListFoto[27] = "D:\\PreOrderWin\\Images\\HotDog.jpg";
            //    //ListFoto[28] = "D:\\PreOrderWin\\Images\\AycLate.jpg"; ListFoto[29] = "D:\\PreOrderWin\\Images\\AycAmerikano.jpg";
            //    //ListFoto[30] = "D:\\PreOrderWin\\Images\\MorsFrukt.jpg"; ListFoto[31] = "D:\\PreOrderWin\\Images\\Limonad.jpg";
            //    //ListFoto[32] = "D:\\PreOrderWin\\Images\\AycLate.jpg"; ListFoto[33] = "D:\\PreOrderWin\\Images\\AycAmerikano.jpg";
            //    //ListFoto[34] = "D:\\PreOrderWin\\Images\\4.jpg"; ListFoto[35] = "D:\\PreOrderWin\\Images\\5.jpg";
            //    //ListFoto[36] = "D:\\PreOrderWin\\Images\\6.jpg"; ListFoto[37] = "D:\\PreOrderWin\\Images\\7.jpg";
            //    //ListFoto[38] = "D:\\PreOrderWin\\Images\\8.jpg"; ListFoto[39] = "D:\\PreOrderWin\\Images\\9.jpg";

            //    //ListFoto[40] = "D:\\PreOrderWin\\Images\\8.jpg"; ListFoto[41] = "D:\\PreOrderWin\\Images\\9.jpg"; ListFoto[42] = "D:\\PreOrderWin\\Images\\9.jpg";

            //    OpisTov[0] = "Ковбаски варені в/с Зальцбурзькі з м'яса птиці :філе куряче, жир-сирець яловичий,вода питна, стабілізатори Е450, Е451, Е452, імбирь,  перець чорний, часник сушений,  Е316, суміш прянощів, екстракт чорного перцю, стабілізатор   Е250 Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції, цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські сухі, молоко сухе знежирене, пшеничне борошно, солодово - пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота, суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти, кунжут       Кетчуп: вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота, камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця: вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця ";
            //    OpisTov[1] = "Ковбаски варені в/с Бірвурст (яловичі) :яловичина 1с, жир-сирець яловичий, яловичина в/с, вода питна, сіль кухонна, стабілізатори Е450, Е451, Е452, суміш комплексна функціональна, цукор, імбирь, аромат, перець чорний, часник сушений, антиоксидант Е316, суміш прянощів, екстракт чорного перцю, стабілізатор кольру Е250         Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції,цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські  сухі, молоко сухе знежирене, пшеничне борошно, солодово-пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота,суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти,кунжут       Кетчуп:вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист    Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота,  камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця :  вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця ";
            //    OpisTov[2] = "Ковбаски варені в/с Єгерські з сиром :яловичина 1с, яловичина в/с, сало бокове, свинина напівжирна, філе куряче, сир твердий, сіль кухонна, перець чорний мелений, часник сушений, антиоксидант Е316, стабілізатори Е450, Е451, Е452, добавка смакова, суміш ароматична, стабілізатор кольору Е250       Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції,цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські  сухі, молоко сухе знежирене, пшеничне борошно, солодово-пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота,суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти,кунжут       Кетчуп:вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист    Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота,  камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця :  вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця ";
            //    OpisTov[3] = "Сосиски вищого сорту Прем'єра: яловичина 1с, сало бокове, яловичина в / с, філе куряче, вода питна, сіль кухонна, стабілізатори Е450, Е451, Е452, цукор, суміш  ";
            //    OpisTov[4] = "Ковбаски н/к 1с з паприкою  :свинина знежилована напівжирна, яловичина 1с, м'ясо куряче, сало бокове, емульсія жилки(жилка яловича, вода питна) сироватка молочна суха, сіль кухонна, паприка, пряно-ароматична суміш, цукор, часник свіжий, барвник натуральний - ферментований рис, перець чорний мелений, стабілізатор кольору Е250 Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції, цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські сухі, молоко сухе знежирене, пшеничне борошно, солодово - пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота, суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти, кунжут       Кетчуп: вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота, камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця: вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця";
            //    OpisTov[5] = "Тортілья пшенична (борошно пшеничне, вода, пальмова олія, стабілізатори: загущувач Е422, карбоксіметилцелюлози натрієва сіль Е466, гуарова камедь Е412;  ";

            //    OpisTov[6] = "Ковбаски варені в/с Зальцбурзькі з м'яса птиці :філе куряче, жир-сирець яловичий,вода питна, стабілізатори Е450, Е451, Е452, імбирь,  перець чорний, часник сушений,  Е316, суміш прянощів, екстракт чорного перцю, стабілізатор   Е250 Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції, цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські сухі, молоко сухе знежирене, пшеничне борошно, солодово - пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота, суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти, кунжут       Кетчуп: вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота, камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця: вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця ";
            //    OpisTov[7] = "Ковбаски варені в/с Бірвурст (яловичі) :яловичина 1с, жир-сирець яловичий, яловичина в/с, вода питна, сіль кухонна, стабілізатори Е450, Е451, Е452, суміш комплексна функціональна, цукор, імбирь, аромат, перець чорний, часник сушений, антиоксидант Е316, суміш прянощів, екстракт чорного перцю, стабілізатор кольру Е250         Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції,цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські  сухі, молоко сухе знежирене, пшеничне борошно, солодово-пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота,суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти,кунжут       Кетчуп:вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист    Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота,  камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця :  вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця ";
            //    OpisTov[8] = "Ковбаски варені в/с Єгерські з сиром :яловичина 1с, яловичина в/с, сало бокове, свинина напівжирна, філе куряче, сир твердий, сіль кухонна, перець чорний мелений, часник сушений, антиоксидант Е316, стабілізатори Е450, Е451, Е452, добавка смакова, суміш ароматична, стабілізатор кольору Е250       Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції,цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські  сухі, молоко сухе знежирене, пшеничне борошно, солодово-пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота,суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти,кунжут       Кетчуп:вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист    Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота,  камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця :  вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця ";
            //    OpisTov[9] = "Сосиски вищого сорту Прем'єра: яловичина 1с, сало бокове, яловичина в / с, філе куряче, вода питна, сіль кухонна, стабілізатори Е450, Е451, Е452, цукор, суміш  ";
            //    OpisTov[10] = "Ковбаски н/к 1с з паприкою  :свинина знежилована напівжирна, яловичина 1с, м'ясо куряче, сало бокове, емульсія жилки(жилка яловича, вода питна) сироватка молочна суха, сіль кухонна, паприка, пряно-ароматична суміш, цукор, часник свіжий, барвник натуральний - ферментований рис, перець чорний мелений, стабілізатор кольору Е250 Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції, цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські сухі, молоко сухе знежирене, пшеничне борошно, солодово - пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота, суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти, кунжут       Кетчуп: вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота, камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця: вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця";

            //    OpisTov[11] = "Ковбаски варені в/с Зальцбурзькі з м'яса птиці :філе куряче, жир-сирець яловичий,вода питна, стабілізатори Е450, Е451, Е452, імбирь,  перець чорний, часник сушений,  Е316, суміш прянощів, екстракт чорного перцю, стабілізатор   Е250 Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції, цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські сухі, молоко сухе знежирене, пшеничне борошно, солодово - пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота, суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти, кунжут       Кетчуп: вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота, камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця: вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця ";
            //    OpisTov[12] = "Ковбаски варені в/с Бірвурст (яловичі) :яловичина 1с, жир-сирець яловичий, яловичина в/с, вода питна, сіль кухонна, стабілізатори Е450, Е451, Е452, суміш комплексна функціональна, цукор, імбирь, аромат, перець чорний, часник сушений, антиоксидант Е316, суміш прянощів, екстракт чорного перцю, стабілізатор кольру Е250         Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції,цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські  сухі, молоко сухе знежирене, пшеничне борошно, солодово-пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота,суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти,кунжут       Кетчуп:вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист    Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота,  камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця :  вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця ";
            //    OpisTov[13] = "Ковбаски варені в/с Єгерські з сиром :яловичина 1с, яловичина в/с, сало бокове, свинина напівжирна, філе куряче, сир твердий, сіль кухонна, перець чорний мелений, часник сушений, антиоксидант Е316, стабілізатори Е450, Е451, Е452, добавка смакова, суміш ароматична, стабілізатор кольору Е250       Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції,цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські  сухі, молоко сухе знежирене, пшеничне борошно, солодово-пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота,суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти,кунжут       Кетчуп:вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист    Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота,  камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця :  вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця ";
            //    OpisTov[14] = "Сосиски вищого сорту Прем'єра: яловичина 1с, сало бокове, яловичина в / с, філе куряче, вода питна, сіль кухонна, стабілізатори Е450, Е451, Е452, цукор, суміш  ";
            //    OpisTov[15] = "Ковбаски н/к 1с з паприкою  :свинина знежилована напівжирна, яловичина 1с, м'ясо куряче, сало бокове, емульсія жилки(жилка яловича, вода питна) сироватка молочна суха, сіль кухонна, паприка, пряно-ароматична суміш, цукор, часник свіжий, барвник натуральний - ферментований рис, перець чорний мелений, стабілізатор кольору Е250 Булочка бутербродна з кунжутом: борошно пшеничне вищого ґатунку, вода питна,пюре картопляне сухе, крохмаль картопляний, молоко сухе,  сіль кухонна, глюкоза, мальтодекстин, спеції, цукор білий, олія соняшникова рафінована дезодорована, дріжджі хлібопекарські сухі, молоко сухе знежирене, пшеничне борошно, солодово - пшеничне борошно, ферменти ензими, антиоксидант аскорбінова кислота, суха пшенична клейковина, борошно пшеничне, емульгатор лецитин соєвий, антиоксидант ,аскорбінова кислота, ферменти, кунжут       Кетчуп: вода питна, паста томатна, цукор білий, крохмаль модифікований кукурудзяний та картопляний, сіль кухонна, кислота оцтова харчова, сорбат калію, мускатний горіх, перець чорний, часник, кориця, гвоздика, лавровий лист Майонез:олія соняшникова, вода птна, цукор білий, жовток яєчний ферметнований рідкий, модифікований крохмаль кукурудзяний, оцет спиртовий, сіль кухонна, молочна кислота, камідь ксантова та гуаранова, сорбат калію, ароматизатор Гірчиця, антиоксидант Е385, натуральний барвник Бета-каротин    Гірчиця: вода питна, цукор білий, порошок гірчичний, олія соняшникова, сіль, кислота оцтова харчова, мед натуральний, куркума, ароматизатор Мед, камідь ксантова, кориця";


            //    OpisTovEN[0] = "Boiled sausages Salzburg from poultry meat: chicken fillet, raw beef fat, drinking water, stabilizers E450, E451, E452, ginger, black pepper, dried garlic, E316, a mixture of spices, black pepper extract, stabilizer E250  Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[1] = "Boiled sausages Birwurst (beef): beef 1c, raw beef fat,, water, salt, stabilizers E450, E451, E452, a mixture of complex functional, sugar, ginger, black pepper, dried garlic, antioxidant E316, a mixture of spices pepper, color stabilizer E250     Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[2] = "Boiled Jaeger sausages with cheese: beef 1c, lard, semi-fat pork, chicken fillet, hard cheese, salt, ground black pepper, dried garlic, antioxidant E316, stabilizers E450, E451, E452, flavoring, aromatic mixture, color stabilizer     Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[3] = "Sausages of the highest grade Premier beef 1s, lateral lard,, chicken fillet, water, salt stabilizers E450, E451, E452, sugar, mix complex functional, antioxidant E316, black pepper, cardamom, stabilizer of color E250   Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[4] = "Sausages with paprika: low-fat lean pork, 1c beef, chicken, lard, dry whey, salt, paprika, spicy-aromatic mixture, sugar, garlic, natural dye - fermented, rice, ground black pepper, color stabilizer E250   Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[5] = "Wheat tortilla (wheat flour, water, palm oil, stabilizers: thickener E422, carboxymethylcellulose sodium salt E466, guar gum E412; acidity regulators: E500, E450; salt, malic acid E296, emulsifier: E472, E282; dextrose) , boiled sausage of the first grade Tender with ketchup (pork skimmed semi-fat, chicken fillet, lard),ketchup - 20% (dried vegetables: tomato, garlic; sugar, pork gelatin, salt, flavor enhancer E621, acidity regulator E262, thickener E407a, antioxidant E330, mustard oil, spice extracts: black pepper, chili), water, whey milk powder, salt, stabilizer (milk protein, lactose), nutmeg, antioxidant E316, color stabilizer E250.cheddar cheese (pasteurized cow's milk, salt, calcium chloride stabilizer, bacterial enzyme cultures, carotene dye).";

            //    OpisTovEN[6] = "Boiled sausages Salzburg from poultry meat: chicken fillet, raw beef fat, drinking water, stabilizers E450, E451, E452, ginger, black pepper, dried garlic, E316, a mixture of spices, black pepper extract, stabilizer E250  Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[7] = "Boiled sausages Salzburg from poultry meat: chicken fillet, raw beef fat, drinking water, stabilizers E450, E451, E452, ginger, black pepper, dried garlic, E316, a mixture of spices, black pepper extract, stabilizer E250  Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[8] = "Boiled sausages Salzburg from poultry meat: chicken fillet, raw beef fat, drinking water, stabilizers E450, E451, E452, ginger, black pepper, dried garlic, E316, a mixture of spices, black pepper extract, stabilizer E250  Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[9] = "Sausages of the highest grade Premier beef 1s, lateral lard,, chicken fillet, water, salt stabilizers E450, E451, E452, sugar, mix complex functional, antioxidant E316, black pepper, cardamom, stabilizer of color E250   Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[10] = "Sausages with paprika: low-fat lean pork, 1c beef, chicken, lard, dry whey, salt, paprika, spicy-aromatic mixture, sugar, garlic, natural dye - fermented, rice, ground black pepper, color stabilizer E250   Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";

            //    OpisTovEN[11] = "Boiled sausages Salzburg from poultry meat: chicken fillet, raw beef fat, drinking water, stabilizers E450, E451, E452, ginger, black pepper, dried garlic, E316, a mixture of spices, black pepper extract, stabilizer E250  Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[12] = "Boiled sausages Salzburg from poultry meat: chicken fillet, raw beef fat, drinking water, stabilizers E450, E451, E452, ginger, black pepper, dried garlic, E316, a mixture of spices, black pepper extract, stabilizer E250  Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[13] = "Boiled sausages Salzburg from poultry meat: chicken fillet, raw beef fat, drinking water, stabilizers E450, E451, E452, ginger, black pepper, dried garlic, E316, a mixture of spices, black pepper extract, stabilizer E250  Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[14] = "Sausages of the highest grade Premier beef 1s, lateral lard,, chicken fillet, water, salt stabilizers E450, E451, E452, sugar, mix complex functional, antioxidant E316, black pepper, cardamom, stabilizer of color E250   Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";
            //    OpisTovEN[15] = "Sausages with paprika: low-fat lean pork, 1c beef, chicken, lard, dry whey, salt, paprika, spicy-aromatic mixture, sugar, garlic, natural dye - fermented, rice, ground black pepper, color stabilizer E250   Bun with sesame: wheat flour, water, mashed potatoes, starch, milk powder, salt, glucose, sugar, oil, yeast, milk powder, wheat flour, malt-wheat flour, enzymes, antioxidant ascorbic acid, soy,  sesame     Ketchup: water, tomato paste, sugar, starch, salt, food acetic acid, potassium sorbate, nutmeg, black pepper, garlic, cinnamon, cloves, bay leaf. Mayonnaise: oil, water, sugar, egg yolk, starch, vinegar, salt, lactic acid, potassium sorbate, flavor Mustard, antioxidant E385, Beta-carotene Mustard: water, sugar, mustard powder, oil, salt, acetic acid, honey , turmeric, cinnamon";

            //    bool CalcOnOffv = false;
            //    byte[] image;
            //    int CountFoto = MainWindow.db.ImagesTovs.Count();
            //    Sprtov strSprtov = new Sprtov();
            //    for (int i = 0; i < 11; i++)
            //    {

            //        CalcOnOffv = ((i + 1) > 6 && (i + 1) < 12) ? true : false;
            //        strSprtov.Id = i + 1;
            //        strSprtov.KodGrup = Listgrup[i]; ;
            //        strSprtov.KodTov = ListKodTov[i];
            //        strSprtov.ArtTov = "";
            //        strSprtov.NameTovUK = ListNameTov[i];
            //        strSprtov.NameTovEN = ListNameEn[i];
            //        strSprtov.KodCalc =  (i+1)<6 ? i + 1 :0 ;
            //        strSprtov.PartCalc = CalcOnOffv;
            //        strSprtov.PriceTov = ListPriceTov[i];
            //        strSprtov.OstTov = 10;
            //        image = File.ReadAllBytes(ListFoto[i]);
            //        strSprtov.IDFotoTov = image;
            //        strSprtov.IdConcomtov = 0;
            //        strSprtov.IdOptions = 0;
            //        strSprtov.IdInfo = 0;
            //        strSprtov.Opis = OpisTov[i];
            //        strSprtov.OpisEn = OpisTovEN[i];

                   
            //        strSprtov.ImagePath = ListFoto[i];
            //        strSprtov.SaleStop = false;
            //        strSprtov.Timeissue = 10;


            //        MainWindow.db.Sprtovs.Add(strSprtov);
            //        MainWindow.db.SaveChanges();

            //    }




            //    ImagesTov FotoTov = new ImagesTov();
            //    for (int i = 0; i < 11; i++)
            //    {
            //        FotoTov.Id = CountFoto + i + 1;
            //        FotoTov.KodGrup = Listgrup[i];
            //        FotoTov.Kodvid = 0;
            //        FotoTov.Kodpvid = 0;
            //        FotoTov.Kodtov = ListKodTov[i];
            //        image = File.ReadAllBytes(ListFoto[i]);
            //        FotoTov.FotoGruopTov = image;
            //        FotoTov.ImagePath = ListFoto[i];
            //        MainWindow.db.ImagesTovs.Add(FotoTov);
            //        MainWindow.db.SaveChanges();
            //    }


            //}

          

            //if (MainWindow.db.Sprgrups.Count() == 0)
            //{


            //    ListNameTov[0] = "ХОТ ДОГИ"; ListNameTov[1] = "БУРГЕРИ БІФ";
                
                
            //    // ListNameTov[2] = "БУРГЕРИ ЧІКЕН"; ListNameTov[3] = "ХОТ ДОГИ";
            //    //ListNameTov[4] = "НАПОЇ"; ListNameTov[5] = "Гарніри"; ListNameTov[6] = "СОЛЯНКА";



            //    ListNameEn[0] = "Hot Dog"; ListNameEn[1] = "BURGERS BIF";
                
            //    // ListNameEn[2] = "BURGERS chiken"; ListNameEn[3] = "Hot Dog";
            //    //ListNameEn[4] = "NAPOI"; ListNameEn[5] = "GARNIRS"; ListNameEn[6] = "SOLYANKA";

            //    ListFoto[0] = "D:\\PreOrderWin\\Images\\HotDogNorma.png";ListFoto[1] = "D:\\PreOrderWin\\Images\\BurgerBif.jpg";
                
            //    //
            //    //ListFoto[2] = "D:\\PreOrderWin\\Images\\BurgerChiken.jpg"; ListFoto[3] = "D:\\PreOrderWin\\Images\\HotDog.jpg";
            //    //ListFoto[4] = "D:\\PreOrderWin\\Images\\AycLate.jpg"; ListFoto[5] = "D:\\PreOrderWin\\Images\\5.jpg";
            //    //ListFoto[6] = "D:\\PreOrderWin\\Images\\9.jpg";
            //    int CountFoto = MainWindow.db.ImagesTovs.Count();

            //    Sprgrup strSprgrup = new Sprgrup();

            //    for (int i = 0; i < 1; i++)
            //    {

            //        strSprgrup.Id = i + 1;
            //        strSprgrup.KodGrup = i + 1;
            //        strSprgrup.NameUK = ListNameTov[i];
            //        strSprgrup.NameEN = ListNameEn[i];
            //        strSprgrup.IDFotoGrup = CountFoto + i + 1;
            //        strSprgrup.ImagePath = ListFoto[i];

            //        MainWindow.db.Sprgrups.Add(strSprgrup);
            //        MainWindow.db.SaveChanges();
            //    }
            //    ImagesTov strSprtov = new ImagesTov();
                
            //    for (int i = 0; i < 1; i++)
            //    {

            //        strSprtov.Id = CountFoto + i + 1;
            //        strSprtov.KodGrup = i + 1;
            //        strSprtov.Kodtov = 0;
            //        byte[] image = File.ReadAllBytes(ListFoto[i]);
            //        strSprtov.FotoGruopTov = image;
            //        strSprtov.ImagePath = ListFoto[i];

            //        MainWindow.db.ImagesTovs.Add(strSprtov);
            //        MainWindow.db.SaveChanges();

            //    }


            //}

          
            //if (MainWindow.db.Sprconcomtovs.Count() == 0)
            //{
            //    ListKodTov[0] = 11; ListKodTov[1] = 12; ListKodTov[2] = 13; ListKodTov[3] = 14; ListKodTov[4] = 15;
            //    //ListKodTov[5] = 5; ListKodTov[6] = 6;
            //    Listgrup[0] = 2; Listgrup[1] = 2; Listgrup[2] = 2; Listgrup[3] = 2; Listgrup[4] = 2;
            //    //Listgrup[5] = 2; Listgrup[6] = 2;
            //    Sprconcomtov Listconcom = new Sprconcomtov();

            //    for (int i = 0; i < 6; i++)
            //    {
            //        Listconcom.Id = i + 1;
            //        Listconcom.Concomtov = ListKodTov[i];
            //        Listconcom.KodGrup = Listgrup[i];
            //        MainWindow.db.Sprconcomtovs.Add(Listconcom);

            //        var opistov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ListKodTov[i]).FirstOrDefault();
            //        if (opistov != null) opistov.IdConcomtov = Listconcom.Id;

            //        MainWindow.db.SaveChanges();


            //    }


            //}

            // Справочник взаимозаменяемых составляющих товаров в калькуляции  
            //if (MainWindow.db.CompositionTovs.Count() == 0)
            //{
            //    ListKodTov[0] = 1;  ListKodTov[1] = 2; ListKodTov[2] = 3; ListKodTov[3] = 4; ListKodTov[4] = 5;
            //    //ListKodTov[3] = 3; ListKodTov[4] = 4; ListKodTov[5] = 5; ListKodTov[6] = 6;
            //    Listgrup[0] = 7; Listgrup[1] = 8; Listgrup[2] = 9; Listgrup[3] = 10; Listgrup[4] = 11;
            //    //Listgrup[3] = 3; Listgrup[4] = 5; Listgrup[5] = 6; Listgrup[6] = 7;
            //    CompositionTov Listconcom = new CompositionTov();

            //    for (int i = 0; i < 6; i++)
            //    {
            //        Listconcom.Id = i + 1;
            //        Listconcom.KodCalc = i + 1;
            //        Listconcom.IdtovBaz = ListKodTov[i];
            //        Listconcom.IdtovReplace = Listgrup[i];

            //        MainWindow.db.CompositionTovs.Add(Listconcom);
            //        MainWindow.db.SaveChanges();
            //    }


            //}


            // Справочник калькуляций товаров
            //if (MainWindow.db.Sprcalculattovs.Count() == 0)
            //{
            //    ListKodTov[0] = 1; ListKodTov[1] = 2; ListKodTov[2] = 3; ListKodTov[3] = 4; ListKodTov[4] = 5;

            //    //ListKodTov[3] = 4; ListKodTov[4] = 4; ListKodTov[5] = 5; ListKodTov[6] = 6;
            //    Listgrup[0] = 7; Listgrup[1] = 8; Listgrup[2] = 9; Listgrup[3] = 10; Listgrup[4] = 11;
            //    //Listgrup[3] = 3; Listgrup[4] = 5; Listgrup[5] = 6; Listgrup[6] = 7;

            //    Sprcalculattov Listconcom = new Sprcalculattov();

            //    for (int i = 0; i < 5; i++)
            //    {
            //        Listconcom.Id = i + 1;
            //        Listconcom.KodTov = ListKodTov[i];
            //        Listconcom.KodCalc = i + 1;
            //        Listconcom.CalcTov = Listgrup[i];
            //        Listconcom.QuantityTov = .5M;
            //        Listconcom.Portion = 100;
            //        Listconcom.NameCalc = "Хот дог";

            //        MainWindow.db.Sprcalculattovs.Add(Listconcom);

            //        var opistov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ListKodTov[i]).FirstOrDefault();
            //        if (opistov != null) opistov.KodCalc = Listconcom.KodCalc;

            //        MainWindow.db.SaveChanges();
            //    }


            //}
           

            //if (MainWindow.db.InfoTovs.Count() == 0)
            //{
            //    for (int i = 0; i < 11; i++)
            //    {
            //        ListKodTov[i] = i + 1;

            //    }
            //    InfoTovSpr ListInfoTov = new InfoTovSpr();
            //    for (int i = 0; i < 16; i++)
            //    {
            //        ListInfoTov.Id = i + 1;
            //        ListInfoTov.Kodtov = ListKodTov[i];
            //        ListInfoTov.NameInfoUk = "";
            //        ListInfoTov.NameInfoEn = "";
            //        ListInfoTov.Weight = "180 ";
            //        ListInfoTov.Composition = "Булка, Ковбаски варені в/с Зальцбурзькі з м'яса птиці, Кетчуп, Майонез, Гірчиця";
            //        ListInfoTov.CompositEn = "bride, Sausages boiled 'Salzburg from poultry', Ketchup, Mayonnaise, Mustard";

            //        ListInfoTov.Alergen = "лактоза, глютен, кунжут, яйця, гірчиця, можливі залишки сої та селери";
            //        ListInfoTov.AlergenEn = "lactose, gluten, sesame, eggs, mustard, possible residues of soy and celery";
            //        ListInfoTov.Alarm = " Не рекомендовано до вживання особам, що мають чутливість до складників виробу";
            //        ListInfoTov.AlarmEn = " Not recommended for use by persons with sensitivity to the components of the product.";
            //        ListInfoTov.Belki = 16;
            //        ListInfoTov.Giri = 37;
            //        ListInfoTov.Vuglevody = 33;
            //        ListInfoTov.Kkal = 1914;
            //        MainWindow.db.InfoTovs.Add(ListInfoTov);

            //        var opistov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ListKodTov[i]).FirstOrDefault();
            //        if (opistov != null) opistov.IdInfo = ListInfoTov.Id;

            //        MainWindow.db.SaveChanges();



            //    }
            //}

            //if (MainWindow.db.OptionsTovs.Count() == 0)
            //{
            //    for (int i = 0; i < 11; i++)
            //    {
            //        ListKodTov[i] = i + 1;

            //    }
            //    OptionsTov ListOptionsTov = new OptionsTov();
            //    int ind = 1;
            //    for (int i = 0; i < 11; i++)
            //    {
            //        if (i != 5)
            //        { 
            //            ListOptionsTov.Id =  ind;
            //            ListOptionsTov.KodOpcii = 1;
            //            ListOptionsTov.Kodtov = ListKodTov[i];
            //            ListOptionsTov.NameOptionsUk = "Кетчуп";
            //            ListOptionsTov.NameOptionsEn = "Ketchup";
            //            MainWindow.db.OptionsTovs.Add(ListOptionsTov);
            //            MainWindow.db.SaveChanges();
            //            ind++;
            //            ListOptionsTov.Id =  ind;
            //            ListOptionsTov.KodOpcii = 2;
            //            ListOptionsTov.Kodtov = ListKodTov[i];
            //            ListOptionsTov.NameOptionsUk = "Майонез";
            //            ListOptionsTov.NameOptionsEn = "Mayonnaise";
            //            MainWindow.db.OptionsTovs.Add(ListOptionsTov);
            //            MainWindow.db.SaveChanges();
            //            ind++;
            //            ListOptionsTov.Id =  ind ;
            //            ListOptionsTov.KodOpcii = 3;
            //            ListOptionsTov.Kodtov = ListKodTov[i];
            //            ListOptionsTov.NameOptionsUk = "Горчиця";
            //            ListOptionsTov.NameOptionsEn = "Mustard";
            //            MainWindow.db.OptionsTovs.Add(ListOptionsTov);

            //            var opistov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ListKodTov[i]).FirstOrDefault();
            //            if (opistov != null) opistov.IdOptions = ListOptionsTov.Id;
            //            MainWindow.db.SaveChanges();                   
            //        }
 
            //        ind ++;
            //    }
            //}
        }

        public static void AddBaseFoto()
        {
            int[] Listgrup = new int[43];

            if (MainWindow.db.Sprconcomtovs.Count() == 0)
            {
                ListKodTov[0] = 50444; ListKodTov[1] = 50446; ListKodTov[2] = 13; ListKodTov[3] = 14; ListKodTov[4] = 15;
                //ListKodTov[5] = 5; ListKodTov[6] = 6;
                Listgrup[0] = 1; Listgrup[1] = 1; Listgrup[2] = 2; Listgrup[3] = 2; Listgrup[4] = 2;
                //Listgrup[5] = 2; Listgrup[6] = 2;
                Sprconcomtov Listconcom = new Sprconcomtov();

                for (int i = 0; i < 2; i++)
                {
                    Listconcom.Id = i + 1;
                    Listconcom.Concomtov = ListKodTov[i];
                    Listconcom.KodGrup = Listgrup[i];
                    MainWindow.db.Sprconcomtovs.Add(Listconcom);

                    var opistov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ListKodTov[i]).FirstOrDefault();
                    if (opistov != null) opistov.IdConcomtov = Listconcom.Id;

                    MainWindow.db.SaveChanges();


                }

            }
                //if (MainWindow.db.Sprconcomtovs.Count() == 0)
                //{
                //    ListKodTov[0] = 44559; ListKodTov[1] = 50443; ListKodTov[2] = 50444; ListKodTov[3] = 504446; ListKodTov[4] = 504448;
                //    ListKodTov[5] = 50450; // ListKodTov[6] = 6;
                //    Listgrup[0] = 1; Listgrup[1] = 1; Listgrup[2] = 1; Listgrup[3] = 1; Listgrup[4] = 1; Listgrup[5] = 1;
                //    //Listgrup[5] = 2; Listgrup[6] = 2;
                //    Sprconcomtov Listconcom = new Sprconcomtov();

                //    for (int i = 0; i < 6; i++)
                //    {
                //        Listconcom.Id = i + 1;
                //        Listconcom.Concomtov = ListKodTov[i];
                //        Listconcom.KodGrup = Listgrup[i];
                //        MainWindow.db.Sprconcomtovs.Add(Listconcom);

                //        var opistov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ListKodTov[i]).FirstOrDefault();
                //        if (opistov != null) opistov.IdConcomtov = Listconcom.Id;

                //        MainWindow.db.SaveChanges();


                //    }

                //}

                //Справочник взаимозаменяемых составляющих товаров в калькуляции
                //if (MainWindow.db.CompositionTovs.Count() == 0)
                //{
                //    ListKodTov[0] = 44559; ListKodTov[1] = 50443; ListKodTov[2] = 50444; ListKodTov[3] = 504446; ListKodTov[4] = 504448;
                //    ListKodTov[5] = 50450;
                //    Listgrup[0] = 1; Listgrup[1] = 1; Listgrup[2] = 1; Listgrup[3] = 1; Listgrup[4] = 1; Listgrup[5] = 1;
                //    Listgrup[3] = 3; Listgrup[4] = 5; Listgrup[5] = 6; Listgrup[6] = 7;
                //    CompositionTov Listconcom = new CompositionTov();

                //    for (int i = 0; i < 6; i++)
                //    {
                //        Listconcom.Id = i + 1;
                //        Listconcom.KodCalc = i + 1;
                //        Listconcom.IdtovBaz = ListKodTov[i];
                //        Listconcom.IdtovReplace = Listgrup[i];

                //        MainWindow.db.CompositionTovs.Add(Listconcom);
                //        MainWindow.db.SaveChanges();
                //    }


                //}


                //// Справочник калькуляций товаров
                //if (MainWindow.db.Sprcalculattovs.Count() == 0)
                //{
                //    ListKodTov[0] = 44559; ListKodTov[1] = 50443; ListKodTov[2] = 50444; ListKodTov[3] = 50446; ListKodTov[4] = 50448;
                //    ListKodTov[5] = 50450; // ListKodTov[6] = 6;
                //    Listgrup[0] = 50451; Listgrup[1] = 50452; Listgrup[2] = 50453; Listgrup[3] = 50454; Listgrup[4] = 50455; Listgrup[5] = 50456;

                //    Sprcalculattov Listconcom = new Sprcalculattov();

                //    for (int i = 0; i < 5; i++)
                //    {
                //        Listconcom.Id = i + 1;
                //        Listconcom.KodTov = ListKodTov[i];
                //        Listconcom.KodCalc = i + 1;
                //        Listconcom.CalcTov = Listgrup[i];
                //        Listconcom.QuantityTov = .5M;
                //        Listconcom.Portion = 100;
                //        Listconcom.NameCalc = "Хот дог";

                //        MainWindow.db.Sprcalculattovs.Add(Listconcom);

                //        var opistov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ListKodTov[i]).FirstOrDefault();
                //        if (opistov != null) opistov.KodCalc = Listconcom.KodCalc;

                //        MainWindow.db.SaveChanges();
                //    }


                //}




                //string[] ListFoto = new string[43];
            //ListFoto[0] = "D:\\PreOrderWin\\Images\\kofe.jpg"; ListFoto[1] = "D:\\PreOrderWin\\Images\\napitki.jpg"; 
            //ListFoto[2] = "D:\\PreOrderWin\\Images\\vidkofe.jpg"; ListFoto[3] = "D:\\PreOrderWin\\Images\\vidsoki.jpg";

            //int[] ind = new int[7];
            //ind[0] = 3; ind[1] = 5; ind[2] = 4; ind[3] = 6;
            //for (int i=0; i<4; i++)
            //{

            //    var UpdateImagesFoto = MainWindow.db.ImagesTovs.Where(p => p.Id == ind[i]).FirstOrDefault();
            //    byte[] image = File.ReadAllBytes(ListFoto[i]);
            //    UpdateImagesFoto.FotoGruopTov = image;
            //    UpdateImagesFoto.ImagePath = ListFoto[i];
            //    MainWindow.db.SaveChanges();         
            //}
            //ListFoto[0] = "D:\\PreOrderWin\\Images\\kapuchino.jpg"; ListFoto[1] = "D:\\PreOrderWin\\Images\\sokappel.jpg";
            //ListFoto[2] = "D:\\PreOrderWin\\Images\\late.jpg";

            //ind[0] = 28186; ind[1] = 33924; ind[2] = 51209; 
            //for (int i = 0; i < 3; i++)
            //{

            //    var UpdateImagesFoto = MainWindow.db.Sprtovs.Where(p => p.Id == ind[i]).FirstOrDefault();
            //    byte[] image = File.ReadAllBytes(ListFoto[i]);
            //    UpdateImagesFoto.IDFotoTov= image;
            //    UpdateImagesFoto.ImagePath = ListFoto[i];
            //    MainWindow.db.SaveChanges();
            //}




            //ImagesTov FotoTov = new ImagesTov();
            //ListFoto[0] = "D:\\PreOrderWin\\Images\\HotDogNorma.png"; ListFoto[1] = "D:\\PreOrderWin\\Images\\HotDog.jpg";
            //ListFoto[2] = "D:\\PreOrderWin\\Images\\hotdog3.jpg"; ListFoto[3] = "D:\\PreOrderWin\\Images\\hotdog4.jpg";
            //ListFoto[4] = "D:\\PreOrderWin\\Images\\HotDogNorma.png"; ListFoto[5] = "D:\\PreOrderWin\\Images\\HotDog.jpg";
            //ListFoto[6] = "D:\\PreOrderWin\\Images\\hotdog3.jpg";
            //ListFoto[7] = "D:\\PreOrderWin\\Images\\hotdog4.jpg";
            //ListFoto[8] = "D:\\PreOrderWin\\Images\\HotDogNorma.png";
            //ListFoto[9] = "D:\\PreOrderWin\\Images\\HotDog.jpg"; ListFoto[10] = "D:\\PreOrderWin\\Images\\HotDogNorma.png";

            //ListFoto[11] = "D:\\PreOrderWin\\Images\\HotDog.jpg";
            //ListFoto[12] = "D:\\PreOrderWin\\Images\\vlavashe-min.png"; ListFoto[13] = "D:\\PreOrderWin\\Images\\vlavashe-min.png";
            //ListFoto[14] = "D:\\PreOrderWin\\Images\\vlavashe-min.png"; ListFoto[15] = "D:\\PreOrderWin\\Images\\HotDog.jpg";

            //byte[] image;
            //var UpdateImagesSprtov = MainWindow.db.Sprtovs.Where(p => p.KodGrup == 1).OrderBy(p => p.KodTov).ToList();
            //int i = 0;

            //foreach (Sprtov pole in UpdateImagesSprtov)
            //{

            //    image = File.ReadAllBytes(ListFoto[i]);
            //    pole.IDFotoTov = image;
            //    pole.ImagePath = ListFoto[i];
            //    MainWindow.db.SaveChanges();
            //    i++;
            //}
            //i = 0;
            //ListFoto[0] = "D:\\PreOrderWin\\Images\\HotDogNorma.png"; ListFoto[1] = "D:\\PreOrderWin\\Images\\AycLate.jpg";
            //ListFoto[2] = "D:\\PreOrderWin\\Images\\BurgerChiken.jpg"; ListFoto[3] = "D:\\PreOrderWin\\Images\\vlavashe-min.png";
            //ListFoto[4] = "D:\\PreOrderWin\\Images\\BurgerChiken.jpg";
            //ListFoto[5] = "D:\\PreOrderWin\\Images\\Burger.jpg";
            //ListFoto[6] = "D:\\PreOrderWin\\Images\\BurgerBif.jpg";
            //ListFoto[7] = "D:\\PreOrderWin\\Images\\BurgerBif.jpg";
            //var UpdateImagesFoto = MainWindow.db.ImagesTovs.ToList();
            ////ImagesTov FotoTov = new ImagesTov();
            //foreach (ImagesTov pole in UpdateImagesFoto)
            //{

            //    image = File.ReadAllBytes(ListFoto[i]);
            //    pole.FotoGruopTov = image;
            //    pole.ImagePath = ListFoto[i];
            //    MainWindow.db.SaveChanges();
            //    i++;
            //}


        }

        public static void RunWinDykuy(object ThreadObj)
        {

            Thread.Sleep(3000);
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                Dykuy Win_Off = ControlWin.LinkMainWindow("Dykuy");
                //MainWindow WinMain = new MainWindow();
                //WinMain.Show();
                Win_Off.Close();
            }));
 
 
        }

        //public static void InListPriceWin(string NameWin)
        //{
        //    Window WinRun = ControlWin.LinkMainWindow(NameWin);
        //    if (MainWindow.db.Sprtovs.Count() == 0)
        //    {
        //        PreOrderConection.ErorDebag("Справочник товаров не загружен " + Environment.NewLine + "Загрузите данные из БД 1С ", 2, 0, (PreOrderConection.StruErrorDebag)null);
        //        string TextWindows = "Справочник товаров не загружен " + Environment.NewLine + "Загрузите данные из БД 1С ";
        //        MessageError NewOrder = new MessageError(TextWindows, 2);
        //        NewOrder.Show();
        //        Thread.Sleep(8000);

        //        MainWindow WinTake = new MainWindow();
        //        WinTake.Show();
        //        WinRun.Close();
        //    }
        //    else
        //    {
        //        MainWindow.TimeClick++;
        //        PriceInvalid WinPayment = new PriceInvalid();
        //        WinPayment.Show();
                
        //    }
            

        //}

        // запуск потока слежения за пасивностью клиента
        public static void RunTimeOut()
        {
                MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
                Thread thread = new Thread(RunWinTimeOut);
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true; // Фоновый поток
                thread.Start(Arguments01);
            MainWindow.TimeOutRun = 1;
        }

        // открытие окна Close
        public static void RunWinTimeOut(object ThreadObj)
        {

            while(MainWindow.TimeOut == 0) 
            { 
                MainWindow.SetClick = MainWindow.TimeClick;             
                Thread.Sleep(60000);

                if (MainWindow.SetClick == MainWindow.TimeClick)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        int indwin = 0;
                        if (MainWindow.NameError != "")
                        {
                            Window WinError = ControlWin.LinkMainWindow(MainWindow.NameError);
                            if (WinError != null) indwin++;
 
                        }
                        if (MainWindow.NameMapTov != "")
                        {
                            Window WinMapTov = ControlWin.LinkMainWindow(MainWindow.NameMapTov);
                            if (WinMapTov != null) indwin++;
                        }
                        if (MainWindow.NameSetRelated != "")
                        {
                            Window WinRelated = ControlWin.LinkMainWindow(MainWindow.NameSetRelated);
                            if (WinRelated != null) indwin++;
                        }

                        if (MainWindow.NameSetWindow != "")
                        {
                            Window WinSet = ControlWin.LinkMainWindow(MainWindow.NameSetWindow);
                            if (WinSet != null) indwin++;
                        }
 
                        if (indwin > 0)
                        {
                            WaitClose WinWait = new WaitClose();
                            WinWait.Show();
 
                        }
                        else MainWindow.TimeOutRun = 0;
                        MainWindow.TimeOut = 1;
                    }));
                }           
            }

        }


        // запуск потока слежения за пасивностью клиента
        public static void RunBankTerminal()
        {
            //    MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
            //    Thread thread = new Thread(ThreadBankTerminal);
            //    thread.SetApartmentState(ApartmentState.STA);
            //    thread.IsBackground = true; // Фоновый поток
            //    thread.Start(Arguments01);
            //    MainWindow.TimeOutRun = 1;
            //}

           
            KodVozvarata = 0;
            TextWindows = "";
            decimal TotalOrder = MainWindow.SumaFinish;
            //MainWindow.SumaFinish = 0.01m;
            double Oplata = Convert.ToDouble(MainWindow.SumaFinish*100);
            bool CurrentProcessValue = true;
            PaymentBankWin.NextPrintCheck = true;



            // Язык сообщений библиотеки Может принимать следующие значения: 0 – английский, 1 – украинский, 2 – русский
            Terminal.CommClose();
            Thread.Sleep(500);
            Terminal.SetErrorLang(1);
            // Установка связи с терминалом
            Terminal.CommOpen(MainWindow.USBPortTerminal, 115200);
            Thread.Sleep(500);
            // Анализ подтверждения установки связи с терминалом
            if (Convert.ToInt16(Terminal.LastResult) == 1 || Convert.ToInt16(Terminal.LastErrorCode) == 3 || Convert.ToInt16(Terminal.LastErrorCode) == 4)
            {
                StatusErrorTerminal(Convert.ToInt16(Terminal.LastErrorCode), Terminal.LastErrorDescription, Convert.ToInt16(Terminal.ResponseCode),
                                   Convert.ToInt16(Terminal.LastStatMsgCode), Terminal.LastStatMsgDescription, Convert.ToInt16(Terminal.SignVerif));
                PaymentBankWin.NextPrintCheck = false;
                return;
            }

 
            // Запрос на оплату и подтвержление.
            if (Convert.ToInt16(Terminal.LastResult) == 0 || Convert.ToInt16(Terminal.LastResult) == 2)
            {

                while (CurrentProcessValue == true)
                {

                    if (Convert.ToInt16(Terminal.LastResult) == 2)
                    {
                        WaitPOSRespone(Convert.ToInt16(Terminal.LastStatMsgCode), Terminal.LastStatMsgDescription, Convert.ToInt16(Terminal.SignVerif));
                        if (KodVozvarata != 0)
                        {
                           CurrentProcessValue = false;
                           break;
                        }
                       
                    }
                    if (Convert.ToInt16(Terminal.LastResult) == 1 || Convert.ToInt16(Terminal.LastErrorCode) == 3 || Convert.ToInt16(Terminal.LastErrorCode) == 4)
                    {
                        StatusErrorTerminal(Convert.ToInt16(Terminal.LastErrorCode), Terminal.LastErrorDescription, Convert.ToInt16(Terminal.ResponseCode),
                                           Convert.ToInt16(Terminal.LastStatMsgCode), Terminal.LastStatMsgDescription, Convert.ToInt16(Terminal.SignVerif));
                        CurrentProcessValue = false;
                        break;
                    }
                    // Терминал готов к работе. сумма для оплаты в копейках отправить в терминал
                    if (Convert.ToInt16(Terminal.LastResult) == 0 )
                    {
                        Terminal.Purchase(Convert.ToUInt32(Oplata), 0, 0);
                        Thread.Sleep(1000);
                        if (Convert.ToInt16(Terminal.LastResult) == 2)
                        {
                            WaitPOSRespone(Convert.ToInt16(Terminal.LastStatMsgCode), Terminal.LastStatMsgDescription, Convert.ToInt16(Terminal.SignVerif));
                            //MessageBox.Show("WaitPOSRespone "+ KodVozvarata.ToString() + " " + Convert.ToString(Terminal.LastResult) + " " + Convert.ToString(Terminal.LastErrorCode) + " " + Convert.ToString(Terminal.ResponseCode));


                            if (KodVozvarata != 0)
                            {
                                CurrentProcessValue = false;
                                break;
                            } 
                        }
                        //MessageBox.Show(Convert.ToString(Terminal.LastResult) + " " + Convert.ToString(Terminal.LastErrorCode) + " " + Convert.ToString(Terminal.ResponseCode));
                        if (Convert.ToInt16(Terminal.LastResult) == 1 || Convert.ToInt16(Terminal.LastErrorCode) == 3 || Convert.ToInt16(Terminal.LastErrorCode) == 4 )
                        {
                            StatusErrorTerminal(Convert.ToInt16(Terminal.LastErrorCode), Terminal.LastErrorDescription, Convert.ToInt16(Terminal.ResponseCode),
                                               Convert.ToInt16(Terminal.LastStatMsgCode), Terminal.LastStatMsgDescription, Convert.ToInt16(Terminal.SignVerif));
                            CurrentProcessValue = false;
                            break;
                        }

                       
                        while (Convert.ToInt16(Terminal.LastResult) != 0)
                        {
                            Thread.Sleep(1000);
                        }

                        NumberBankKart = CardPAN = Terminal.PAN; // номера карты в формате 1234хххххх5678
                        NumberTranzakcii = CardRRN = Terminal.RRN; // получить идентификатор транзакции RRN
                        KodAvtor = Terminal.AuthCode; // код авторизации
                        TerminalID = Terminal.TerminalID.ToString(); // ID терминала
                        InvoiceNum = Convert.ToString(Terminal.InvoiceNum); // номер транзакции номер квитанции 
                        UserNameCard= Terminal.IssuerName; // імя емітента картки master privat
                        PaymentSystem = Terminal.IssuerName;
                        //Terminal.CompletionInvoiceNum
                        UserNameCardAvtor = Terminal.CardHolder;
                        MerchantKasir = Terminal.MerchantID.ToString(); //  ідентифікатор продавця.
                        Terminal.Confirm();
                        Thread.Sleep(3000);
 
                        CurrentProcessValue = false;
                        PaymentBankWin.NextPrintCheck = true;
                        MainWindow.SumaFinish = TotalOrder;

                    }

                }


            }
            else PaymentBankWin.NextPrintCheck = false;
            Terminal.CommClose();
        }


        // Процедура обработки ошибок терминала
        public static void StatusError(int LastErrorCode, string LastErrorDescription, int ResponseCode, int LastStatMsgCode, string LastStatMsgDescription, int SignVerif)
        {

            if (LastErrorCode == 1 || LastErrorCode == 2 || LastErrorCode == 3)
            {
                //1-"ошибка открытия СОМ-порта, например неправильный номер порта.";2-"необходимо открыть СОМ-порт 3-"ошибка связи с терминалом
                ErrorMessageTerminal = LastErrorDescription;
                PreOrderConection.ErorDebag(LastErrorDescription, 2);
                TextWindows = "Відсутнє з'єднання з терміналом" + Environment.NewLine + " Перевірте з'єднання  по порту та стан терміналу."
                     + Environment.NewLine + " Відсутність комунікації з терміналом забезпечує режим оплати тільки готівкою.";
                MessageError NewOrder = new MessageError(TextWindows, 2,7);
                NewOrder.ShowDialog();
                //Thread.Sleep(7000);
                //NewOrder.Close();
                return;
            }
        }
        public static void StatusErrorTerminal(int LastErrorCode, string LastErrorDescription, int ResponseCode, int LastStatMsgCode, string LastStatMsgDescription, int SignVerif)
        {
            ErrorMessageTerminal = LastErrorDescription;
            PreOrderConection.ErorDebag(LastErrorDescription, 2);
            if (LastErrorCode == 1 || LastErrorCode == 2 || LastErrorCode == 3)
            {
                //1-"ошибка открытия СОМ-порта, например неправильный номер порта.";2-"необходимо открыть СОМ-порт 3-"ошибка связи с терминалом
                
                TextWindows = "Відсутнє з'єднання з терміналом";
            }
            if (LastErrorCode == 4)
            {
                //"терминал вернул ошибку, можно обработать ResponseCode
                if (ResponseCode == 1000)
                {
                    TextWindows = "Общая ошибка" + Environment.NewLine + " Проверте соединение и состояние терминала.";
                    //общая ошибка;
                }

                if (ResponseCode == 1003)
                { // журнал транзакций заполнен, нужно произвести обнуление (Z-отчет);
                    TextWindows = "Журнал транзакций заполнен." + Environment.NewLine + " Нужно произвести обнуление (Z-отчет)";
                }
                if (ResponseCode == 1004)
                {
                    //  нет связи с хостом/банком;
                    TextWindows = "Нет связи с банком" + Environment.NewLine + " Проверте соединение и состояние терминала.";
                }
                if (ResponseCode == 1005)
                {// закончилась бумага;
                    TextWindows = "Нет бумаги в принтере" + Environment.NewLine + " Необходимо вставить бумагу в принтер печати чеков.";
                }
                if (ResponseCode == 1006)
                { // ошибка ключей;
                    TextWindows = "Відсутнє з'єднання з терміналом" + Environment.NewLine + "Перевірте з'єднання  по порту та стан терміналу.";
                }
                if (ResponseCode == 1001)
                { // ошибка ключей;
                    TextWindows = "Транзакція відмінена користувачем" + Environment.NewLine + " Оплату скасовано.";
                }
 

                //        //  Используется в случае утвержденного / не утвержденного разрешения.
                //        //Коды ниже 1000 принимаются от хоста.
                //        //1000 - Общая ошибка(следует использовать в исключительном случае)
                //        //1001 - Транзакция отменена пользователем
                //        //1002 - Снижение EMV
                //        //1003 - Журнал транзакций заполнен. Нужна закрытая партия
                //        //1004 - Нет связи с хостом
                //        //1005 - Нет бумаги в принтере
                //        //1006 - Ошибка криптографических ключей
                //        //1007 - Картридер не подключен
                //        //1008 - Транзакция уже завершена

            }
            KodVozvarata = 1;
            Terminal.Cancel();
            Thread.Sleep(1000);
            while (Convert.ToInt16(Terminal.LastResult) != 0)Thread.Sleep(1000);
 
            Terminal.CommClose();
            Thread.Sleep(1000);
            while (Convert.ToInt16(Terminal.LastResult) != 0) Thread.Sleep(1000);
            PaymentBankWin.NextPrintCheck = false;

        }
        public static void WaitPOSRespone(int LastStatMsgCode, string LastStatMsgDescription, int SignVerif)
        {
            KodVozvarata = 0;
            int IndWait = 0;
            //"на терминале выполняются операции, дальнейшая работа в таком режиме невозможна
            while (Convert.ToInt16(Terminal.LastResult) == 2)
            {
                PreOrderConection.ErorDebag(LastStatMsgDescription, 3);
                if (IndWait > 240)
                {
                    KodVozvarata = 1;
                    PreOrderConection.ErorDebag(ErrorMessageTerminal, 2);
                    Thread.Sleep(1000);
                    while (Convert.ToInt16(Terminal.LastResult) != 0) Thread.Sleep(1000);
                    Terminal.CommClose();
                    Thread.Sleep(1000);
                    while (Convert.ToInt16(Terminal.LastResult) != 0) Thread.Sleep(1000);
                    PaymentBankWin.NextPrintCheck = false;
                    break;
                }
                IndWait++;
                Thread.Sleep(500);
                if ( Convert.ToInt16(Terminal.ResponseCode) == 1000)
                { 
                    KodVozvarata = 1;
                    PaymentBankWin.NextPrintCheck = false;
                    break;
                }

            }
    
           
                
            //    //            Проверить состояние подключения терминала в операции, если LastResult возвращает 2 и
            //    //            LastStatMsgCode долго не меняется.
            //    //            get_LastStatMsgCode(BYTE * pVal), свойство LastStatMsgCode
            //    //           [OUT] pVal - возвращает один из следующих кодов статуса:
            //    //            0 - код состояния недоступен.
            //    //1 - карточка прочитана
            //    //2 - использовали чип - карту
            //    //3 - авторизация в процессе
            //    //4 - ожидание действия кассира
            //    //5 - распечатка чека
            //    //6 - необходим контактный ввод
            //    //7 - карта удалена
            //    //8 - Многофункциональные устройства EMV
            //    //9 - ожидание карты
            //    // 10 - в процессе
            //    // 11 - правильная транзакция
            //    // 12 - Клавиша ожидания ввода пина enter
            //    // 13 - Нажата клавиша возврата ввода
            //    // 14 - Нажата клавиша ввода пина


        }


                    // Процедура копирования однодневного пакета заказов в таблицу истории заказов.

                    // запуск потока слежения за изменением даты и создания копии однодневных заказов
        public static void RunCopyHistoryOrder()
        {
            MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
            Thread thread = new Thread(CopyToHistoryOrder);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true; // Фоновый поток
            thread.Start(Arguments01);
            
        }
        public static void CopyToHistoryOrder(object ThreadObj)
        {
            
      
            
            
            while (0 == 0)
            {
                Thread.Sleep(600000);
                using (ApplicationContext dbhistory = new ApplicationContext())
                { 
                    if (dbhistory.HeadZakazs.Count() > 0)
                    {
                        DateTime MemDataTimePrint = dbhistory.HeadZakazs.Max(p => p.DataTimePrint);
                        if (DateTime.Now.Date > MemDataTimePrint.Date)
                        {
                        var HeadUpOrder = dbhistory.HeadZakazs.ToList();
                        HeadZakazHistory StrHead = new HeadZakazHistory();
                        foreach (HeadZakaz pole in HeadUpOrder)
                        {

                            if (pole.Status == 4 || pole.Status == 5)
                            { 
                                StrHead.Id = pole.Id;
                                StrHead.IdOrder = pole.IdOrder;
                                StrHead.IdFloor = pole.IdFloor;
                                StrHead.Number = pole.Number;
                                StrHead.SumZakaz = pole.SumZakaz;
                                StrHead.SumPayment = pole.SumPayment;
                                StrHead.SumDiscount = pole.SumDiscount;
                                StrHead.SumBonusLoyalti = pole.SumBonusLoyalti;
                                StrHead.PlaceService = pole.PlaceService;
                                StrHead.Payment = pole.Payment;
                                StrHead.Status = pole.Status;
                                StrHead.BankCart = pole.BankCart;
                                StrHead.NumbTRN = pole.NumbTRN;
                                StrHead.TerminalID = pole.TerminalID;
                                StrHead.NumbCheck = pole.NumbCheck;
                                StrHead.KodAvtoriz = pole.KodAvtoriz;
                                StrHead.ShtrihKod = pole.ShtrihKod;
                                StrHead.DataTimeBegin = pole.DataTimeBegin;
                                StrHead.DataTimePrint = pole.DataTimePrint;
                                StrHead.DataTimeEnd = pole.DataTimeEnd;
                                StrHead.IdKartLoyalty = pole.IdKartLoyalty;

                                // Дозапись заказ сновым статусом
                                dbhistory.HeadZakazHistorys.Add(StrHead);

                                var ContentUpOrder = dbhistory.ContentZakazs.Where(c => c.Number == pole.Number);
                                ContentZakazHistory StrContent = new ContentZakazHistory();
                                foreach (ContentZakaz zap in ContentUpOrder)
                                {

                                    StrContent.Id = zap.Id;
                                    StrContent.Number = zap.Number;
                                    StrContent.Kodtov = zap.Kodtov;
                                    StrContent.TovCalc = zap.TovCalc;
                                    StrContent.Quantity = zap.Quantity;
                                    StrContent.Opcii = zap.Opcii;
                                    StrContent.PriceTov = zap.PriceTov;
                                    StrContent.SumTov = zap.SumTov;
                                    StrContent.Timeissue = zap.Timeissue;
                                    StrContent.NameTovUK = zap.NameTovUK;

                                    // Дозапись заказ сновым статусом
                                    dbhistory.ContentZakazHistorys.Add(StrContent);
                                }

                            }

                        }
                        dbhistory.SaveChanges();
                        // Удаление тела заказа
                        dbhistory.HeadZakazs.RemoveRange(MainWindow.db.HeadZakazs.ToList());
                        dbhistory.SaveChanges();
                        dbhistory.ContentZakazs.RemoveRange(MainWindow.db.ContentZakazs.ToList());
                        //  Сохранить изменения
                        dbhistory.SaveChanges();

                    }

   
                    }
                   
  

                }
   
            }
 
        }
        // Процедура обмена сообщением с кассовым сервером. 
        public static void LoadUrlServer( string UrlAdres ="", string CmdStroka ="")
        {
            for (int indshag = 0 ; indshag < 3; indshag++)
            {
                try
                {
                    // Url запрашиваемого методом POST скрипта
                    MainWindow.ResponseFromServer = "";
                    MainWindow.ReturnCodeServer = 0;
                    reqPOST = WebRequest.Create(UrlAdres);
                    reqPOST.Method = "POST";
                    reqPOST.Timeout = 60000;
                    //reqPOST.ContentType = "application/x-www-form-urlencoded";

                    // передаем список пар параметров / значений для запрашиваемого скрипта методом POST
                    // в случае нескольких параметров необходимо использовать символ & для разделения параметров
                    // в данном случае используется кодировка UTF8 для Url кодирования спец. символов значения параметров


                    byte[] sentData = Encoding.UTF8.GetBytes(CmdStroka); //"message=" + System.Web.HttpUtility.UrlEncode("отправляемые данные", Encoding.UTF8)
                    reqPOST.ContentLength = sentData.Length;
                    sendStream = reqPOST.GetRequestStream();
                    sendStream.Write(sentData, 0, sentData.Length);
                    sendStream.Close();

                    // считываем результат работы
                    result = reqPOST.GetResponse();
                    ReceiveStream = result.GetResponseStream();
                    StrRead = new StreamReader(ReceiveStream, Encoding.UTF8);
                    MainWindow.ResponseFromServer = StrRead.ReadToEnd();

                }
                catch (Exception ex)
                {

                    MainWindow.ExMessage = ex.Message;
                    PreOrderConection.ErorDebag("Ошибка: " + MainWindow.ExMessage, 2);
                    MainWindow.ReturnCodeServer = 1;
                }
                finally
                {
                    indshag = 3;
                    if (ReceiveStream != null) ReceiveStream.Close();
                    if (StrRead != null) StrRead.Close();
                    if (result != null) result.Close();
                }
            
            }
           

        }

        // Удаление заказ из очереди отложеных чеков кассы
        public static void ReversOrderTurnCheck()
        {
            
            if (MainWindow.ApiOrdersAdd == true)
            {
                MainWindow.UrlAdresServer = MainWindow.TcpUrlServer + ":" + MainWindow.PortUrlServer + "/api/orders/reverse"; //"http://192.168.1.11:8082/api/orders/reverse";
                MainWindow.CmdStrokaServer = "{\"cmd\":\"reverse_order\",\"data\":{\"order_id\":" + MainWindow.IdOrderND2017.ToString() + "}}";
                ControlWin.LoadUrlServer(MainWindow.UrlAdresServer, MainWindow.CmdStrokaServer);
                PreOrderConection.ErorDebag("UrlServerReturnCode = " + MainWindow.UrlServerReturnCode.ToString() + Environment.NewLine + " Строка: " + MainWindow.ResponseFromServer, 2);
                MainWindow.ApiOrdersAdd = false;
            }

        }

        // Подтверждение оплаты заказ наличными или через банк в кассе.
        public static void PaymentBankOnOff(string PaymentType ="")
        {
            if (MainWindow.CashBank == 2)
            {
                //ControlWin.NumberBankKart = "3333XXXXXXXX3333"; //CardPAN = Terminal.pan; // номера карты в формате 1234хххххх5678
                //ControlWin.NumberTranzakcii = "0000000001"; // CardRRN = Terminal.rrn; // получить идентификатор транзакции RRN
                //ControlWin.KodAvtor = "123456"; //Terminal.AuthCode; // код авторизации
                //ControlWin.TerminalID = "TERM_001"; //Terminal.TerminalID.ToString(); // ID терминала
                //ControlWin.InvoiceNum = "000001"; //Convert.ToString(Terminal.InvoiceNum); // номер транзакции  
                //ControlWin.PaymentSystem = "master"; // Terminal.IssuerName; // імя емітента картки master privat
                //ControlWin.UserNameCardAvtor = "card holder";
                //ControlWin.MerchantKasir = "merch_1"; //Terminal.MerchantID.ToString(); //  ідентифікатор продавця.           
            }
            else
            {
                NumberBankKart = NumberTranzakcii = KodAvtor = TerminalID = InvoiceNum = PaymentSystem = UserNameCardAvtor = MerchantKasir = "";
            }
            MainWindow.ResultPayment = true;
            string TextCode = "", ErrorText ="";
            string BankCard = "\"PAN\":\"" + ControlWin.NumberBankKart + "\",\"RRN\":\"" + ControlWin.NumberTranzakcii + "\",\"auth_code\":\"" +
                ControlWin.KodAvtor + "\",\"slip_no\":\"" + ControlWin.InvoiceNum + "\",\"terminal_id\":\"" + ControlWin.TerminalID + "\",\"merchant_id\":\"" + ControlWin.MerchantKasir + "\",\"card_holder\":\"" + UserNameCardAvtor + "\",\"payment_system\":\"" + ControlWin.PaymentSystem + "\"";

            MainWindow.UrlAdresServer = MainWindow.TcpUrlServer + ":" + MainWindow.PortUrlServer + "/api/orders/commit"; //"http://192.168.1.11:8082/api/orders/commit";
            string ordersuma = Convert.ToString(MainWindow.SumaFinish).Replace(",", ".");
           
            MainWindow.CmdStrokaServer = "{\"cmd\":\"commit_order\",\"data\":{\"order_id\":" + MainWindow.IdOrderND2017.ToString() + ",\"total\":"+ ordersuma + ",\"payment\":{\"type\":\"" + PaymentType + "\",\"total\":" + ordersuma + "," + BankCard + "}}}";
            PreOrderConection.ErorDebag(MainWindow.CmdStrokaServer, 2); //MainWindow.IdOrderND2017
            //MessageBox.Show(MainWindow.CmdStrokaServer);
            ControlWin.LoadUrlServer(MainWindow.UrlAdresServer, MainWindow.CmdStrokaServer);
            PreOrderConection.ErorDebag("UrlServerReturnCode = " + MainWindow.UrlServerReturnCode.ToString() + Environment.NewLine + " Строка: " + MainWindow.ResponseFromServer, 2);
            //MessageBox.Show(MainWindow.ResponseFromServer);
            if (MainWindow.ReturnCodeServer == 0)
            {

                if (MainWindow.ResponseFromServer.Contains("{\"code\":0") == false)
                {

                    if (MainWindow.ResponseFromServer.Contains("{\"code\":1") == true) TextCode = " Помилка виконання підтвердження оплати.";
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":2") == true) TextCode = " Немає замовлення з таким номером.";
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":3") == true) TextCode = " Помилка у форматі запросу на підтвердження.";
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":6") == true) TextCode = " Зміна закрита або перебільшено залишок товару, продати неможливо.";
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":7") == true) TextCode = " Перебільшена максимально можлива кількість відкладених замавлень.";
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":11") == true) TextCode = "Замовлення вже підтверджено.";
                    if (MainWindow.ResponseFromServer.Contains("{\"code\":12") == true) TextCode = "Сумма підтвердження замовлення меньше суми відкладеного замовлення .";
                    ErrorText = "Шановний замовник! Вибачте. " + Environment.NewLine 
                        + TextCode + Environment.NewLine + " Будь ласка натисніть повторно на кнопку \"ГОТОВО\" та виконайте подальші дії по підтвердженю замовлення.";
                    PreOrderConection.ErorDebag(TextCode + Environment.NewLine + MainWindow.CmdStrokaServer + Environment.NewLine + MainWindow.ResponseFromServer, 2);
                    MainWindow.ResultPayment =  false;
                    MessageError NewOrder = new MessageError(ErrorText, 2, 7);
                    NewOrder.ShowDialog();
                    // Разбор подтверждения оплаты.
                }
                else
                {


                    MainWindow.ResultPayment = true;
                    MainWindow.CmdStrokaServer = "";
                    // фиксируем состояние подтверждения оплаты.
                    var OrderNumb = MainWindow.db.AdminSetValues.Where(p => p.Id == 1).FirstOrDefault();
                    OrderNumb.ValueCmdStroka = "";
                    OrderNumb.CountOrder = 0;
                    MainWindow.db.SaveChanges();
 
                  
                }


            }
            else
            {
                // Обработка ошибки сервера. Отвалилась связь.
                //MainWindow.ExMessage.Contains("- 400")
                //MainWindow.ResultPayment = false;
                PreOrderConection.ErorDebag("Ошибка в запросе к веб сереверу " + Environment.NewLine + MainWindow.ResponseFromServer, 2);
                string TextWindows = "Шановний замовник! Вибачте. " + Environment.NewLine + " Помилка сервера при підтвердженні оплати Вашого замовлення.";
                MessageError NewOrder = new MessageError(TextWindows, 2, 7);
                NewOrder.ShowDialog();
            }

        }


    }
}
