using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Models;
using Tubes_Kelompok_BisaYukk.Modules;
using Tubes_Kelompok_BisaYukk_Final.Modules;


namespace Tubes_Kelompok_BisaYukk.Tests
{
   public class ProgramTests
    {

        [Fact]
        public void MenuAdmin_ShouldHandleLihatTugasAndExit()
        {
            // Arrange
            var mockInput = new List<string> { "1", "5" };
            var console = new MockConsoleService(mockInput);

            // Act
            Program.MenuAdmin(console);

            // Assert
            Assert.Contains("[TampilkanSemuaTugas()]", console.Outputs);
            Assert.Contains("=== Menu Admin ===", console.Outputs);
        }


        [Fact]
        public void ReadPassword_ShouldReturnTypedPassword()
        {
            // Arrange
            var inputs = new Queue<ConsoleKeyInfo>(new[]
            {
        new ConsoleKeyInfo('h', ConsoleKey.H, false, false, false),
        new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false),
        new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false),
        new ConsoleKeyInfo('o', ConsoleKey.O, false, false, false),
        new ConsoleKeyInfo('\n', ConsoleKey.Enter, false, false, false)
    });

            string result = Program.ReadPassword(() => inputs.Dequeue());

            // Assert
            Assert.Equal("halo", result);
        }

        [Fact]
        public void IsValidStatus_ShouldReturnTrue_ForValidInput()
        {
            // Arrange
            string input = "SeniorStaff";

            // Act
            bool result = Program.IsValidStatus(input, out StatusKaryawan status);

            // Assert
            Assert.True(result);
            Assert.Equal(StatusKaryawan.SeniorStaff, status);
        }

        [Fact]
        public void IsValidStatus_ShouldReturnFalse_ForInvalidInput()
        {
            // Arrange
            string input = "CEO"; // invalid

            // Act
            bool result = Program.IsValidStatus(input, out StatusKaryawan status);

            // Assert
            Assert.False(result);
            Assert.Equal(default(StatusKaryawan), status); // atau StatusKaryawan.Intern
        }

        [Fact]
        public void IsDuplicateTugas_ShouldReturnTrue_IfTugasExists()
        {
            // Arrange
            var data = new Dictionary<string, List<string>>
            {
                { "fauzan", new List<string> { "Buat Laporan Harian" } }
            };

            // Act
            bool result = InvokeIsDuplicateTugas("fauzan", "Laporan", data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDuplicateTugas_ShouldReturnFalse_IfTugasNotExists()
        {
            // Arrange
            var data = new Dictionary<string, List<string>>
            {
                { "fauzan", new List<string> { "Meeting" } }
            };

            // Act
            bool result = InvokeIsDuplicateTugas("fauzan", "Laporan", data);

            // Assert
            Assert.False(result);
        }

        private bool InvokeIsDuplicateTugas(string karyawan, string tugas, Dictionary<string, List<string>> data)
        {
            var method = typeof(Program).GetMethod(
                "IsDuplicateTugas",
                BindingFlags.NonPublic | BindingFlags.Static);

            return (bool)method.Invoke(null, new object[] { karyawan, tugas, data });
        }
    }
}
