namespace ReinforcementIronCalculator.Models
{
    public class N20 : WeightCalculator
    {
        private const double weightPerMeter = 2.47;

        public N20(int count, double length)
            : base(count, length, weightPerMeter)
        {
        }
    }
}
