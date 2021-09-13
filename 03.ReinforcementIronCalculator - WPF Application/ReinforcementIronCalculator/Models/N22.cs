namespace ReinforcementIronCalculator.Models
{
    public class N22 : WeightCalculator
    {
        private const double weightPerMeter = 2.98;

        public N22(int count, double length)
            : base(count, length, weightPerMeter)
        {
        }
    }
}
