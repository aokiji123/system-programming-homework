using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedNumberGenerator
{
    internal class FibonacciGenerator
    {
        private int _upperBound;
        private volatile bool _stopRequested;
        private List<int> _numbers;

        public FibonacciGenerator(int upperBound) { _upperBound = upperBound; _stopRequested = false; _numbers = new List<int>(); }

        public void GenerateFibonacci()
        {
            int a = 0, b = 1, c;
            while (!_stopRequested && (_upperBound == 0 || a <= _upperBound))
            {
                _numbers.Add(a);
                c = a + b; a = b; b = c;
            }
        }
        public List<int> GetNumbers() { return _numbers; }
        public void Stop() { _stopRequested = true; }
    }
}