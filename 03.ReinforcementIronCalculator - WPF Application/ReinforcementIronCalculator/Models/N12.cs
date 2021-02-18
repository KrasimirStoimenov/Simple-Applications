namespace ReinforcementIronCalculator.Models
{
    public class N12 : WeightCalculator
    {
        private const double weightPerMeter = 0.89;

        public N12(int count, double length)
            : base(count, length,weightPerMeter)
        {
        }
    }
}
