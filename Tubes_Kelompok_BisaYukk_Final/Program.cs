﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Modules;
using Tubes_Kelompok_BisaYukk.Models;
using Tubes_Kelompok_BisaYukk_Final.Modules;
using Tubes_Kelompok_BisaYukk_Final.Interfaces;

namespace Tubes_Kelompok_BisaYukk
{
    public static class Program
    {
        static Dictionary<string, List<string>> dataKaryawan;

        public static Func<string> ReadPasswordDelegate = () => ReadPassword();
        public static Action<IConsoleService> MenuAdminCallback = console => MenuAdmin(console);
        public static Action WaitForKeyDelegate = () => Console.ReadKey();
        public static Action<string> MenuKaryawanCallback = nama => MenuKaryawan(nama, new ConsoleService()); 


        static void Main()
        {

            try
            {
                dataKaryawan = FileHandler.Instance.MuatDariFile();

            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Gagal memuat data karyawan: " + ex.Message);
                dataKaryawan = new Dictionary<string, List<string>>();
            }

            bool exitProgram = false;

            while (!exitProgram)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("==== Sistem Penjadwalan Tugas Karyawan ====");
                    Console.WriteLine("1. Login sebagai Admin");
                    Console.WriteLine("2. Login sebagai Karyawan");
                    Console.WriteLine("3. Keluar");
                    Console.Write("Pilih menu: ");

                    string pilihanLogin = Console.ReadLine()?.Trim();

                    ILoginStrategy loginStrategy = null;
                    switch (pilihanLogin)
                    {
                        case "1":
                            loginStrategy = new AdminLoginStrategy(new ConsoleService());
                            break;
                        case "2":
                            loginStrategy = new EmployeeLoginStrategy();
                            break;
                        case "3":
                            exitProgram = true;
                            break;
                        default:
                            Console.WriteLine("Pilihan tidak valid! Tekan sembarang tombol untuk kembali.");
                            Console.ReadKey();
                            break;
                    }

                    loginStrategy?.Login(dataKaryawan);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[FATAL ERROR] Terjadi kesalahan tak terduga: " + ex.Message);
                    Console.WriteLine("Tekan sembarang tombol untuk melanjutkan...");
                    Console.ReadKey();
                }
            }
        }

        public static string ReadPassword(Func<ConsoleKeyInfo> readKeyFunc = null)
        {
            readKeyFunc ??= () => Console.ReadKey(true); // default: pakai console asli

            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = readKeyFunc();
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password.Length--;
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    Console.Write("*");
                    password.Append(key.KeyChar);
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password.ToString();
        }


        public static bool IsValidStatus(string input, out StatusKaryawan status)
        {
            status = StatusKaryawan.JuniorStaff;
            return !string.IsNullOrWhiteSpace(input) && Enum.TryParse(input.Trim(), true, out status);
        }

        static bool IsDuplicateTugas(string karyawan, string tugas, Dictionary<string, List<string>> data)
        {
            return data.ContainsKey(karyawan) && data[karyawan].Exists(t => t.Contains(tugas));
        }


        public static void MenuKaryawan(string karyawanAktif, IConsoleService console)
        {
            bool kembaliKeLogin = false;
            while (!kembaliKeLogin)
            {
                Console.Clear();
                Console.WriteLine("Karyawan: " + karyawanAktif);
                Console.WriteLine("1. Mendaftarkan tugas");
                Console.WriteLine("2. Tugas yang sudah disimpan");
                Console.WriteLine("3. Selesaikan tugas");
                Console.WriteLine("4. Statistik & Laporan Karyawan");
                Console.WriteLine("5. Logout");
                Console.Write("\nPilih menu: ");

                string pilihan = Console.ReadLine()?.Trim() ?? "";

                switch (pilihan)
                {
                    case "1":
                        DaftarTugas(karyawanAktif);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Tugas yang sudah disimpan:");
                        TableDrivenJadwal.TampilkanJadwal(karyawanAktif, dataKaryawan);
                        Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                        Console.ReadKey();
                        break;
                    case "3":
                        SelesaikanTugas(karyawanAktif);
                        break;
                    case "4":
                        Console.Clear();
                        StatistikKaryawan.TampilkanLaporan(dataKaryawan);
                        Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.WriteLine("Logout...");
                        kembaliKeLogin = true;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid! Tekan sembarang tombol untuk kembali.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void MenuAdmin(IConsoleService console)
        {
            bool keluar = false;
            while (!keluar)
            {
                console.Clear();
                console.WriteLine("=== Menu Admin ===");
                console.WriteLine("1. Lihat semua tugas");
                console.WriteLine("2. Tambah tugas");
                console.WriteLine("3. Hapus tugas");
                console.WriteLine("4. Lihat history tugas yang sudah diselesaikan");
                console.WriteLine("5. Kembali ke menu login");
                console.WriteLine("\nPilih menu: ");

                string pilihan = console.ReadLine()?.Trim();

                switch (pilihan)
                {
                    case "1":
                        console.WriteLine("[TampilkanSemuaTugas()]");
                        break;
                    case "2":
                        console.WriteLine("[TambahTugas()]");
                        break;
                    case "3":
                        console.WriteLine("[HapusTugas()]");
                        break;
                    case "4":
                        console.WriteLine("[TampilkanHistoryTugasSelesai()]");
                        break;
                    case "5":
                        keluar = true;
                        break;
                    default:
                        console.WriteLine("Pilihan tidak valid!");
                        break;
                }

                if (!keluar)
                {
                    console.WriteLine("\nTekan sembarang tombol untuk kembali...");
                    console.WaitForKey();
                }
            }
        }



        static void TampilkanSemuaTugas()
        {
            Console.WriteLine("\nDaftar semua tugas:");
            foreach (var item in AutomataPemesanan.GetSemuaTugas())
            {
                var status = AutomataStatus.GetStatusDariTugas(item.Key);
                string statusStr = status.HasValue ? status.Value.ToString() : "Tidak Diketahui";
                Console.WriteLine($"- {item.Key} (Status: {(item.Value ? "Tersedia" : "Sudah Diambil")} | {statusStr})");
            }
        }

        static void TambahTugas()
        {
            Console.Write("Masukkan nama tugas baru: ");
            string nama = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(nama))
            {
                Console.WriteLine("Nama tugas tidak boleh kosong.");
                return;
            }

            Console.WriteLine("\nPilih status karyawan untuk tugas ini:");
            Console.WriteLine("1. Intern");
            Console.WriteLine("2. Junior Staff");
            Console.WriteLine("3. Senior Staff");
            Console.Write("Pilihan: ");
            string statusInput = Console.ReadLine()?.Trim();

            StatusKaryawan status;
            switch (statusInput)
            {
                case "1": status = StatusKaryawan.Intern; break;
                case "2": status = StatusKaryawan.JuniorStaff; break;
                case "3": status = StatusKaryawan.SeniorStaff; break;
                default:
                    Console.WriteLine("Status tidak valid. Tugas tidak ditambahkan.");
                    return;
            }

            Console.WriteLine("\nPilih prioritas tugas:");
            Console.WriteLine("1. Rendah");
            Console.WriteLine("2. Sedang");
            Console.WriteLine("3. Tinggi");
            Console.Write("Pilihan: ");
            string prioritasInput = Console.ReadLine()?.Trim();

            string prioritas;
            switch (prioritasInput)
            {
                case "1": prioritas = "Rendah"; break;
                case "2": prioritas = "Sedang"; break;
                case "3": prioritas = "Tinggi"; break;
                default:
                    Console.WriteLine("Prioritas tidak valid. Tugas tidak ditambahkan.");
                    return;
            }

            if (AutomataPemesanan.TambahTugas(nama, dataKaryawan))
            {
                AutomataStatus.TambahTugasUntukStatus(status, nama);
                PrioritasTugas.AturPrioritas(nama, prioritas);
                Console.WriteLine("Tugas berhasil ditambahkan untuk status " + status);
            }
            else
            {
                Console.WriteLine("\nTugas sudah ada atau tidak valid.");
            }
        }

        static void HapusTugas()
        {
            Console.Write("Masukkan nama tugas yang ingin dihapus: ");
            string nama = Console.ReadLine()?.Trim();
            if (AutomataPemesanan.HapusTugas(nama))
                Console.WriteLine("Tugas berhasil dihapus.");
            else
                Console.WriteLine("Tugas tidak ditemukan.");
        }

        static void DaftarTugas(string karyawan)
        {
            Console.Clear();
            Console.WriteLine("Pilih status karyawan:");
            var statusList = Enum.GetValues(typeof(StatusKaryawan)).Cast<StatusKaryawan>().ToList();
            for (int i = 0; i < statusList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {statusList[i]}");
            }
            Console.Write("Pilihan: ");

            if (!int.TryParse(Console.ReadLine(), out int statusIndex) || statusIndex < 1 || statusIndex > statusList.Count)
            {
                Console.WriteLine("Status tidak valid! Tekan sembarang tombol untuk kembali ke menu utama...");
                Console.ReadKey(); return;
            }

            StatusKaryawan status = statusList[statusIndex - 1];
            List<string> tugas = AutomataStatus.GetTugas(status).Where(t => AutomataPemesanan.CekTugasTersedia(t)).ToList();

            if (tugas.Count == 0)
            {
                if (AutomataPemesanan.GetSemuaTugas().Count == 0)
                {
                    Console.WriteLine("Belum ada tugas yang tersedia. Silakan hubungi admin.");
                }
                else
                {
                    Console.WriteLine("Tidak ada tugas tersedia untuk status ini.");
                }
                Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                Console.ReadKey(); return;
            }

            Console.WriteLine("\nTugas yang tersedia:");
            for (int i = 0; i < tugas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tugas[i]} (Prioritas: {PrioritasTugas.GetPrioritas(tugas[i])})");
            }
            Console.Write("Pilih nomor tugas: ");

            if (!int.TryParse(Console.ReadLine(), out int tugasIndex) || tugasIndex < 1 || tugasIndex > tugas.Count)
            {
                Console.WriteLine("Tugas tidak valid! Tekan sembarang tombol untuk kembali ke menu utama...");
                Console.ReadKey(); return;
            }

            string tugasDipilih = tugas[tugasIndex - 1];
            if (IsDuplicateTugas(karyawan, tugasDipilih, dataKaryawan))
            {
                Console.WriteLine("Tugas ini sudah disimpan sebelumnya. Pilih tugas lain.");
                Console.ReadKey(); return;
            }

            var batas = TableDrivenBatas.GetBatasWaktu(status);
            Console.Write($"Masukkan durasi pengerjaan ({batas.Item1}-{batas.Item2} jam): ");
            if (!int.TryParse(Console.ReadLine(), out int durasi) || durasi < batas.Item1 || durasi > batas.Item2)
            {
                Console.WriteLine("Durasi tidak valid! Tekan sembarang tombol untuk kembali ke menu utama...");
                Console.ReadKey(); return;
            }

            if (AutomataPemesanan.AmbilTugas(tugasDipilih))
            {
                TableDrivenJadwal.SimpanJadwal(karyawan, tugasDipilih, durasi, dataKaryawan);
                FileHandler.Instance.SimpanKeFile(dataKaryawan);
                Console.WriteLine("Tugas berhasil disimpan dan diambil!");
            }
            else
            {
                Console.WriteLine("Tugas sudah diambil oleh karyawan lain!");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        static void SelesaikanTugas(string karyawan)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tugas yang sedang dikerjakan:");

                List<string> tugasKaryawan = TableDrivenJadwal.GetTugasKaryawan(karyawan, dataKaryawan);

                if (tugasKaryawan.Count == 0)
                {
                    Console.WriteLine("Tidak ada tugas yang sedang dikerjakan.");
                    Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                    Console.ReadKey();
                    return;
                }

                for (int i = 0; i < tugasKaryawan.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tugasKaryawan[i]}");
                }
                Console.WriteLine("0. Kembali ke menu utama");

                Console.Write("\nMasukkan nomor tugas yang ingin diselesaikan (atau 0 untuk keluar): ");
                if (int.TryParse(Console.ReadLine()?.Trim(), out int nomorTugas))
                {
                    if (nomorTugas == 0)
                    {
                        return;
                    }
                    if (nomorTugas > 0 && nomorTugas <= tugasKaryawan.Count)
                    {
                        string tugasSelesai = tugasKaryawan[nomorTugas - 1];
                        TableDrivenJadwal.SelesaikanTugas(karyawan, tugasSelesai, dataKaryawan);
                        FileHandler.Instance.SimpanKeFile(dataKaryawan);
                        Console.WriteLine($"Tugas \"{tugasSelesai}\" telah diselesaikan.");
                    }
                    else
                    {
                        Console.WriteLine("Nomor tugas tidak valid. Coba lagi.");
                    }
                }
                else
                {
                    Console.WriteLine("Input tidak valid. Masukkan angka tugas atau 0 untuk keluar.");
                }

                Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
                Console.ReadKey();
            }
        }

        static void TampilkanHistoryTugasSelesai()
        {
            TableDrivenJadwal.NormalisasiTugasSelesai(dataKaryawan);
            Console.WriteLine("===== History Tugas yang Sudah Selesai =====");

            var semuaSelesai = TableDrivenJadwal.GetTugasSelesai(dataKaryawan);
            if (semuaSelesai.Count == 0)
            {
                Console.WriteLine("Belum ada tugas yang diselesaikan.");
                return;
            }

            foreach (var karyawan in semuaSelesai)
            {
                Console.WriteLine($"\nKaryawan: {karyawan.Key}");
                foreach (var tugas in karyawan.Value)
                {
                    Console.WriteLine("- " + tugas);
                }
            }
        }

    }
}