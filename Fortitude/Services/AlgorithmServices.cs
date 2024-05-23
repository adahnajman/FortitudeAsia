namespace Fortitude.Services
{
    public class AlgorithmServices
    {
        public int CalculateSpecialDigit(string value)
        {
            int[] multipliers = { 10, 8, 6, 4, 2 };
            int[] sums = new int[5];

            for (int i = 0; i < value.Length; i++)
            {
                int digit = int.Parse(value[i].ToString());
                sums[i % 5] += digit * multipliers[i % 5];
            }

            int totalSum = sums.Sum();
            while (totalSum > 9)
            {
                totalSum = totalSum.ToString().Select(c => int.Parse(c.ToString())).Sum();
            }

            return totalSum;
        }

        public string CalculateSpecialDigitDistribution(int start, int end)
        {
            Dictionary<int, int> frequency = new Dictionary<int, int>();

            for (int i = start; i <= end; i++)
            {
                int specialDigit = CalculateSpecialDigit(i.ToString());
                if (frequency.ContainsKey(specialDigit))
                {
                    frequency[specialDigit]++;
                }
                else
                {
                    frequency[specialDigit] = 1;
                }
            }

            var HighFrequency = frequency.OrderByDescending(x => x.Value).First();
            var lowFrequency = frequency.OrderBy(x => x.Value).First();

            string result = "Special Digit:\n";
            foreach (var item in frequency)
            {
                result += $"Digit {item.Key}: {item.Value}\n";
            }
            result += $"\nMost High digit: {HighFrequency.Key} : {HighFrequency.Value} occurrences\n";
            result += $"Least digit: {lowFrequency.Key} : {lowFrequency.Value} occurrences\n";

            return result;
        }


    }
}
