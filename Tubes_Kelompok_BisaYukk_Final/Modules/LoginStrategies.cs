using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Modules;
using Tubes_Kelompok_BisaYukk;
using Tubes_Kelompok_BisaYukk_Final.Interfaces;

namespace Tubes_Kelompok_BisaYukk_Final.Modules
{
    public interface ILoginStrategy
    {
        void Login(Dictionary<string, List<string>> dataKaryawan);
    }

    public class AdminLoginStrategy : ILoginStrategy
    {
        private readonly IConsoleService _console;

        public AdminLoginStrategy(IConsoleService console)
        {
            _console = console;
        }

        public void Login(Dictionary<string, List<string>> dataKaryawan)
        {
            _console.WriteLine("Masukkan password admin: ");
            string password = Program.ReadPasswordDelegate();

            if (password == "admin123")
            {
                Program.MenuAdminCallback(_console);
            }
            else
            {
                _console.WriteLine("Password salah!");
                Program.WaitForKeyDelegate();
            }
        }
    }

    public class EmployeeLoginStrategy : ILoginStrategy
    {
        public void Login(Dictionary<string, List<string>> dataKaryawan)
        {
            UserSession.Login();
            string karyawanAktif = UserSession.GetKaryawanAktif();
            if (string.IsNullOrEmpty(karyawanAktif)) return;

            if (!dataKaryawan.ContainsKey(karyawanAktif))
                dataKaryawan[karyawanAktif] = new List<string>();

            Program.MenuKaryawan(karyawanAktif, new ConsoleService());
        }
    }
}
