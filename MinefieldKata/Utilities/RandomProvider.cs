using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinefieldKata.Utilities
{
    public interface IRandomProvider
    {
        int Next(int min, int max);
    }

    public class RandomProvider : IRandomProvider
    {
        private readonly Random rng;

        public RandomProvider()
        {
            rng = new Random();
        }

        public int Next(int min, int max)
        {
            return rng.Next(min, max);
        }
    }
}
