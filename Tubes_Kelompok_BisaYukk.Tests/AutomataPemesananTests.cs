using Tubes_Kelompok_BisaYukk.Modules;
using Xunit;
using System.Collections.Generic;
using System.IO;

namespace Tubes_Kelompok_BisaYukk.Tests
{
    public class AutomataPemesananTests
    {

        [Fact]
        public void TambahTugas_ShouldAddNewTugas()
        {
            string namaUnik = "TugasUnik_" + Guid.NewGuid();

            var dataKaryawan = new Dictionary<string, List<string>>();
            var result = AutomataPemesanan.TambahTugas(namaUnik, dataKaryawan);

            Assert.True(result);
            Assert.True(AutomataPemesanan.CekTugasTersedia(namaUnik));
        }

        [Fact]
        public void AmbilTugas_ShouldReturnTrueAndSetTugasToFalse()
        {
            var dataKaryawan = new Dictionary<string, List<string>>();
            AutomataPemesanan.TambahTugas("Coding", dataKaryawan);
            var result = AutomataPemesanan.AmbilTugas("Coding");
            Assert.True(result);
            Assert.False(AutomataPemesanan.CekTugasTersedia("Coding"));
        }

        [Fact]
        public void HapusTugas_ShouldRemoveTugas()
        {
            var dataKaryawan = new Dictionary<string, List<string>>();
            AutomataPemesanan.TambahTugas("Testing", dataKaryawan);
            var result = AutomataPemesanan.HapusTugas("Testing");
            Assert.True(result);
        }

        [Fact]
        public void TambahTugas_Duplikat_ShouldReturnFalse()
        {
            var dataKaryawan = new Dictionary<string, List<string>>();
            AutomataPemesanan.TambahTugas("Laporan", dataKaryawan);
            var result = AutomataPemesanan.TambahTugas("Laporan", dataKaryawan);
            Assert.False(result);
        }

        [Fact]
        public void AmbilTugas_TugasTidakTersedia_ShouldReturnFalse()
        {
            var result = AutomataPemesanan.AmbilTugas("TidakAda");
            Assert.False(result);
        }

        [Fact]
        public void HapusTugas_TugasTidakAda_ShouldReturnFalse()
        {
            var result = AutomataPemesanan.HapusTugas("TidakAda");
            Assert.False(result);
        }

        [Fact]
        public void GetSemuaTugas_ShouldReturnDictionaryCopy()
        {
            AutomataPemesanan.TambahTugas("BackupData", new Dictionary<string, List<string>>());
            var result = AutomataPemesanan.GetSemuaTugas();
            Assert.Contains("BackupData", result.Keys);
        }

    }
}