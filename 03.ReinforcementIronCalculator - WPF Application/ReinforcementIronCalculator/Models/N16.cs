namespace ReinforcementIronCalculator.Models
{
    public class N16 : WeightCalculator
    {
        private const double weightPerMeter = 1.58;

        public N16(int count, double length)
            : base(count, length,weightPerMeter)
        {
        }
    }
}
