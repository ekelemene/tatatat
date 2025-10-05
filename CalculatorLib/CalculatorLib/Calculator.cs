using System;

namespace CalculatorLib
{
    public class Calculator
    {
        public double Add(double a, double b)
        {
            // ❌ Ошибка специально, чтобы тест показал проблему:
            return a * b; // a + b
        }
    }
}