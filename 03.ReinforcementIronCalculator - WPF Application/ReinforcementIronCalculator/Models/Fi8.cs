namespace ReinforcementIronCalculator.Models
{
    public class Fi8 : WeightCalculator
    {
        private const double weightPerMeter = 0.395;

        public Fi8(int count, double length)
            : base(count, length, weightPerMeter)
        {
        }
    }
}
