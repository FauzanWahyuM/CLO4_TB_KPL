﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_Kelompok_BisaYukk.Modules
{
    public class UserSession
    {
        private static string karyawanAktif;

        public static void Login()
        {
            Console.Write("Masukkan nama karyawan: ");
            karyawanAktif = Console.ReadLine();
        }

        public static string GetKaryawanAktif()
        {
            return karyawanAktif;
        }

        public static void SetKaryawanAktif(string nama)
        {
            karyawanAktif = nama;
        }

    }
}
