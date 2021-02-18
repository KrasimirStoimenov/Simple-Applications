namespace ReinforcementIronCalculator.Models
{
    public class N14 : WeightCalculator
    {
        private const double weightPerMeter = 1.21;

        public N14(int count, double length)
            : base(count, length,weightPerMeter)
        {
        }
    }
}
