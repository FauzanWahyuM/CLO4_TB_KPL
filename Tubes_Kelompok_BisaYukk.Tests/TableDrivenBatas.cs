using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Models;
using Tubes_Kelompok_BisaYukk.Modules;

namespace Tubes_Kelompok_BisaYukk.Tests
{
    public class TableDrivenBatasTests
    {
        [Theory]
        [InlineData(StatusKaryawan.Intern, 2, 6)]
        [InlineData(StatusKaryawan.JuniorStaff, 4, 8)]
        [InlineData(StatusKaryawan.SeniorStaff, 6, 10)]
        public void GetBatasWaktu_StatusBerbeda_ReturnBatasYangBenar(
            StatusKaryawan status, int expectedMin, int expectedMax)
        {
            // Act
            var result = TableDrivenBatas.GetBatasWaktu(status);

            // Assert
            Assert.Equal(expectedMin, result.Item1);
            Assert.Equal(expectedMax, result.Item2);
        }
    }
}
