//Bölüm : Bilişim Sistemleri Mühendisliği
//Ad Soyad : İbrahim Çelen
//Numara : B201200014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PinPon
{
    class PinPon
    {
        int en;
        int boy;
        OyunAlanı oyunAlanı;
        Raket raket1;
        Raket raket2;
        ConsoleKeyInfo KeyInfo;
        ConsoleKey consoleKey;
        Top top;
        public PinPon(int en,int boy)
        {
            this.en = en;
            this.boy = boy;
            oyunAlanı = new OyunAlanı(en, boy);
            top = new Top(en / 2, boy / 2, boy, en);
        }
        public void Setup()
        {
            raket1 = new Raket(2, boy);
            raket2 = new Raket(en - 2, boy);
            KeyInfo = new ConsoleKeyInfo();
            consoleKey = new ConsoleKey();
            top.X = en / 2;
            top.Y = boy / 2;
            top.Hareket = 0;
        }
        void Input()
        {
            if (Console.KeyAvailable)
            {
                KeyInfo = Console.ReadKey(true);
                consoleKey = KeyInfo.Key;
            }
        }
        public void Basla()
        {
            while (true)
            {
                Console.Clear();
                Skor();
                Setup();
                oyunAlanı.Ciz();
                raket1.Ciz();
                raket2.Ciz();
                top.Ciz();
                while (top.X != 1 && top.X != en-1)
                {
                    Input();
                    switch (consoleKey)
                    {
                        case ConsoleKey.W:
                            raket1.Yukarı();
                            break;
                        case ConsoleKey.S:
                            raket1.Asagı();
                            break;
                        case ConsoleKey.UpArrow:
                            raket2.Yukarı();
                            break;
                        case ConsoleKey.DownArrow:
                            raket2.Asagı();
                            break;
                    }
                    consoleKey = ConsoleKey.A;
                    top.Kontrol(raket1, raket2);
                    top.Ciz();
                    Thread.Sleep(100);
                }
            }
            
        }
        public void Skor()
        {
            top.SkorHesap();
            if (top.skor1 == 3 || top.skor2 == 3)
            {
                Console.Clear();
                Console.WriteLine(top.skor1.ToString() + "        " + top.skor2.ToString());
            }
        }
    }
    class Top
    {
        public int X { set; get; }
        public int Y { set; get; }
        public int skor1 { set; get; }
        public int skor2 { set; get; }
        int yonX;
        int yonY;
        int i;
        int alanBoy;
        int alanEn;

        public int Hareket { set; get; }

        public Top(int x, int y, int alanBoy, int alanEn)
        {
            X = x;
            Y = y;
            this.alanBoy = alanBoy;
            this.alanEn = alanEn;
            yonX = -1;
            yonY = 1;
        }
        public void Kontrol(Raket raket1,Raket raket2)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("\0");
            if(Y<=1 || Y >= alanBoy)
            {
                yonY *= -1;
            }
            if ((X == 3 || X == alanEn-3) && (raket1.Y - (raket1.Uzunluk / 2)) <= Y && (raket1.Y + (raket1.Uzunluk / 2)) > Y)
            {
                yonX *= -1;
                if (Y == raket1.Y)
                {
                    Hareket = 0;
                }
                if ((Y >= (raket1.Y - (raket1.Uzunluk / 2)) && Y < raket1.Y || (Y > raket1.Y && Y < (raket1.Y + (raket1.Uzunluk / 2)))))
                {
                    Hareket = 1;
                }
            }
                switch (Hareket)
                {
                    case 0:
                        X += yonX;
                        break;
                    case 1:
                        X += yonX;
                        Y += yonY;
                        break;

                }
            
        }
        public void SkorHesap()
        {
            if(X == alanEn - 1)
            {
                skor1++;
            }
            if (X == 1)
            {
                skor2++;
            }
        }
        public void Ciz()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(X,Y);
            Console.Write("■");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Raket
    {
        public int X { set; get; }
        public int Y { set; get; }
        public int Uzunluk { set; get; }
        int alanBoy;

        public Raket(int x,int alanBoy)
        {
            this.alanBoy = alanBoy;
            X = x;
            Y = alanBoy / 2;
            Uzunluk = alanBoy / 3;
        }
        public void Yukarı()
        {
            if((Y - 1 - (Uzunluk/2)) != 0)
            {
                Console.SetCursorPosition(X, (Y + (Uzunluk / 2)) - 1);
                Console.Write("\0");
                Y--;
                Ciz();
            }
        }
        public void Asagı()
        {
            if ((Y + 1 + (Uzunluk / 2)) != alanBoy+2)
            {
                Console.SetCursorPosition(X, (Y - (Uzunluk / 2)));
                Console.Write("\0");
                Y++;
                Ciz();
            }
        }
        public void Ciz()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = (Y - (Uzunluk/2)); i < Y + (Uzunluk / 2); i++)
            {
                Console.SetCursorPosition(X, i);
                Console.Write("│");
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
    public class OyunAlanı
    {
        public int Boy { set; get; }
        public int En { set; get; }

        public OyunAlanı()
        {
            Boy = 20;
            En = 60;
        }
        public OyunAlanı(int en,int boy)
        {
            En = en;
            Boy = boy;
        }
        public void Ciz()
        {
            for (int i = 1; i <= En; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("─");
            }
            for (int i = 1; i <= En; i++)
            {
                Console.SetCursorPosition(i, (Boy + 1));
                Console.Write("─");
            }
            for (int i = 1; i <= Boy; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│");
            }
            for (int i = 1; i <= Boy; i++)
            {
                Console.SetCursorPosition(En + 1, i);
                Console.Write("│");
            }
            Console.SetCursorPosition(0, 0);
            Console.Write("┌");
            Console.SetCursorPosition(En + 1, 0);
            Console.Write("┐");
            Console.SetCursorPosition(0, Boy + 1);
            Console.Write("└");
            Console.SetCursorPosition(En + 1, Boy + 1);
            Console.Write("┘");
        }
    }

}
