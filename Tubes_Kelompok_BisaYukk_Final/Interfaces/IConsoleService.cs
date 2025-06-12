using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_Kelompok_BisaYukk_Final.Interfaces
{
    public interface IConsoleService
    {
        string ReadLine();
        void WriteLine(string message);
        public void Clear()
        {
            try
            {
                if (!Console.IsOutputRedirected)
                {
                    Console.Clear();
                }
            }
            catch (IOException)
            {
                // Abaikan error saat dijalankan di lingkungan test
            }
        }


        void WaitForKey();
    }
}
