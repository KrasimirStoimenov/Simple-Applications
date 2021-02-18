namespace ReinforcementIronCalculator.Models
{
    public class F6 : WeightCalculator
    {
        private const double weightPerMeter = 0.222;

        public F6(int count, double length)
            : base(count, length,weightPerMeter)
        {
        }
    }
}
