using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk_Final.Interfaces;

namespace Tubes_Kelompok_BisaYukk.Tests
{
    public class MockConsoleService : IConsoleService
    {
        private readonly Queue<string> inputs;
        public List<string> Outputs { get; } = new();
        public List<string> InputLog { get; } = new();

        public MockConsoleService(IEnumerable<string> inputSequence)
        {
            inputs = new Queue<string>(inputSequence);
        }

        public string ReadLine()
        {
            if (inputs.Count == 0)
            {
                Outputs.Add("[INPUT EMPTY - returning '5']");
                InputLog.Add("5");       // fallback untuk keluar dari loop
                return "5";
            }

            var input = inputs.Dequeue();
            InputLog.Add(input);
            return input;
        }

        public void WriteLine(string message) => Outputs.Add(message);
        public void Clear() => Outputs.Add("[Clear]");
        public void WaitForKey() => Outputs.Add("[WaitForKey]");
    }

}
