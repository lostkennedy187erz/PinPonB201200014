using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            PinPon Pong = new PinPon(60, 20);
            Pong.Basla();
            Console.ReadKey();
        }
    }
}
