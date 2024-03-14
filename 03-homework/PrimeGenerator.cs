using System;
using System.Collections.Generic;

namespace ThreadedNumberGenerator
{
    internal class PrimeGenerator
    {
        private int _upperBound;
        private bool _stopRequested;
        private List<int> _primeNumbers;

        public PrimeGenerator(int upperBound)
        {
            _upperBound = upperBound;
            _stopRequested = false;
            _primeNumbers = new List<int>();
        }

        public void GeneratePrimes()
        {
            int currentNumber = 2;
            while (!_stopRequested && (_upperBound == 0 || currentNumber <= _upperBound))
            {
                if (IsPrime(currentNumber))
                {
                    _primeNumbers.Add(currentNumber);
                }
                currentNumber++;
            }
        }

        public void Stop()
        {
            _stopRequested = true;
        }

        public List<int> GetNumbers()
        {
            return _primeNumbers;
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            int boundary = (int)Math.Sqrt(number);

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
