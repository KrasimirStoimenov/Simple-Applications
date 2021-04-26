namespace ReinforcementIronCalculator.Models
{
    public class Fi6 : WeightCalculator
    {
        private const double weightPerMeter = 0.222;

        public Fi6(int count, double length)
            : base(count, length,weightPerMeter)
        {
        }
    }
}
