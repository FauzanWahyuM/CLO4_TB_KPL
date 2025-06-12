using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tubes_Kelompok_BisaYukk.Modules
{
    public class FileHandler
    {
        private static FileHandler? _instance;
        private static readonly object _lock = new object(); // thread-safe

        private readonly string filePath = "data.json";

        private FileHandler() { }

        public static FileHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new FileHandler();
                        }
                    }
                }
                return _instance;
            }
        }

        public void SimpanKeFile(Dictionary<string, List<string>> data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan data: {ex.Message}");
            }
        }

        public Dictionary<string, List<string>> MuatDariFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal membaca data: {ex.Message}");
            }

            return new Dictionary<string, List<string>>();
        }
    }
}
