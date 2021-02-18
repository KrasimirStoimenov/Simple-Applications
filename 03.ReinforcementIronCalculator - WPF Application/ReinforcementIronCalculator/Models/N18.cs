namespace ReinforcementIronCalculator.Models
{
    public class N18 : WeightCalculator
    {
        private const double weightPerMeter = 2.00;

        public N18(int count, double length)
            : base(count, length,weightPerMeter)
        {
        }
    }
}
