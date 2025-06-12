using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk_Final.Modules;

namespace Tubes_Kelompok_BisaYukk.Tests
{
    public class AdminLoginStrategyTests
    {
        [Fact]
        public void Login_ShouldCallMenuAdmin_WhenPasswordIsCorrect()
        {
            // Arrange
            var mockInput = new List<string> { "admin123", "5" };
            var mockConsole = new MockConsoleService(mockInput);
            var strategy = new AdminLoginStrategy(mockConsole);

            bool menuDipanggil = false;

            Program.ReadPasswordDelegate = () => mockConsole.ReadLine(); // mock password input
            Program.MenuAdminCallback = (console) =>
            {
                menuDipanggil = true;
                Program.MenuAdmin(console); // jalankan menu pakai input "5" untuk keluar
            };

            // Act
            strategy.Login(new Dictionary<string, List<string>>());

            // Assert
            Assert.True(menuDipanggil);
            Assert.Contains("=== Menu Admin ===", mockConsole.Outputs);
        }

        [Fact]
        public void Login_ShouldPrintError_WhenPasswordIsWrong()
        {
            var inputs = new[] { "wrongpass" };
            var mockConsole = new MockConsoleService(inputs);
            var strategy = new AdminLoginStrategy(mockConsole); // inject mock

            Program.ReadPasswordDelegate = () => mockConsole.ReadLine();
            Program.WaitForKeyDelegate = () => mockConsole.WriteLine("[WaitForKey]");

            strategy.Login(new Dictionary<string, List<string>>());

            Assert.Contains("Password salah!", mockConsole.Outputs);
            Assert.Contains("[WaitForKey]", mockConsole.Outputs);
        }

    }
}
