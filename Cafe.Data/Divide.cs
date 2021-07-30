using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Data
{
    public static class Divide
    {
        public static double DivPositive(double a, double b)
        {
            if (b == 0) return -1;
            return a / b;
        }
    }
}
