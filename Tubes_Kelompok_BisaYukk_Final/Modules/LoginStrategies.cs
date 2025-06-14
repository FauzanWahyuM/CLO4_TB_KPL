using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Modules;
using Tubes_Kelompok_BisaYukk;
using Tubes_Kelompok_BisaYukk_Final.Interfaces;
using static Tubes_Kelompok_BisaYukk_Final.Modules.namespace_Security;

namespace Tubes_Kelompok_BisaYukk_Final.Modules
{
    public interface ILoginStrategy
    {
        void Login(Dictionary<string, List<string>> dataKaryawan);
    }

    public class AdminLoginStrategy : ILoginStrategy
    {
        private readonly IConsoleService _console;
        private const int MaxAttempts = 3;

        public AdminLoginStrategy(IConsoleService console)
        {
            _console = console;
        }

        public void Login(Dictionary<string, List<string>> dataKaryawan)
        {
            string passPath = Path.Combine(
                AppContext.BaseDirectory, "Modules", "admin_pass.txt");

            if (!File.Exists(passPath))
            {
                _console.WriteLine("admin_pass.txt belum disalin ke direktori output!");
                Program.WaitForKeyDelegate();
                return;
            }

            string correctPassword = File.ReadAllText(passPath).Trim();

            for (int attempt = 1; attempt <= MaxAttempts; attempt++)
            {
                _console.WriteLine("Masukkan password admin: ");     
                string inputPassword = Program.ReadPasswordDelegate();

                if (inputPassword == correctPassword)
                {
                    Program.MenuAdminCallback(_console);
                    return;
                }

                _console.WriteLine("Password salah!");
                if (attempt < MaxAttempts)
                    _console.WriteLine($"Percobaan {attempt} dari {MaxAttempts}. Silakan coba lagi.\n");
            }

            _console.WriteLine("Anda telah melebihi batas percobaan login. Akses ditolak.");
            Program.WaitForKeyDelegate();
        }
    }


    public class EmployeeLoginStrategy : ILoginStrategy
    {
        public void Login(Dictionary<string, List<string>> dataKaryawan)
        {
            Console.Write("Masukkan nama karyawan: ");
            string rawName = Console.ReadLine()?.Trim() ?? "";

            // Validasi format
            if (!UsernameSecurity.IsValid(rawName))
            {
                Console.WriteLine(
                    "Format nama tidak valid (huruf, spasi, titik, 3‑40 karakter).");
                Program.WaitForKeyDelegate();
                return;
            }

            // Pseudonym jadi key dictionary
            string key = UsernameSecurity.Pseudonym(rawName);

            if (!dataKaryawan.ContainsKey(key))
                dataKaryawan[key] = new List<string>();

            // Simpan nama asli di sesi (bukan di dictionary/file)
            UserSession.SetKaryawanAktif(rawName);

            // Lanjut ke menu
            Program.MenuKaryawan(rawName, new ConsoleService());
        }
    }
}
