namespace ReinforcementIronCalculator.Models
{
    public class N10 : WeightCalculator
    {
        private const double weightPerMeter = 0.62;

        public N10(int count, double length)
            : base(count, length,weightPerMeter)
        {
        }
    }
}
