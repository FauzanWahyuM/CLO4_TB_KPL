using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk_Final.Interfaces;

namespace Tubes_Kelompok_BisaYukk_Final.Modules
{
    public class ConsoleService : IConsoleService
    {
        public string ReadLine() => Console.ReadLine();
        public void WriteLine(string message) => Console.WriteLine(message);
        public void Clear() => Console.Clear();
        public void WaitForKey() => Console.ReadKey();
    }
}
