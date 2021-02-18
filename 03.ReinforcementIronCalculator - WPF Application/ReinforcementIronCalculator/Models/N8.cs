namespace ReinforcementIronCalculator.Models
{
    public class N8 : WeightCalculator
    {
        private const double weightPerMeter = 0.40;

        public N8(int count, double length)
            : base(count, length, weightPerMeter)
        {
        }
    }
}
