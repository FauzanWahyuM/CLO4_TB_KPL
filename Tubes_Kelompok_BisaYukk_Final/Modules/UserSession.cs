using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tubes_Kelompok_BisaYukk_Final.Modules.namespace_Security;
using Tubes_Kelompok_BisaYukk_Final.Modules;

namespace Tubes_Kelompok_BisaYukk.Modules
{
    public class UserSession
    {
        private static string _karyawanAktif;

        public static string GetKaryawanAktif() => _karyawanAktif;

        public static void SetKaryawanAktif(string nama) => _karyawanAktif = nama;
    }
}
