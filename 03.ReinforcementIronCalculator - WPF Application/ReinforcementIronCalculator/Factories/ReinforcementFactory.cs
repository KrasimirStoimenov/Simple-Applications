using ReinforcementIronCalculator.Models;
using ReinforcementIronCalculator.Models.Contracts;
using ReinforcementIronCalculator.Factories.Contracts;

namespace ReinforcementIronCalculator.Factories
{
    public class ReinforcementFactory : IReinforcementFactory
    {
        public ICalculateWeight Create(int width, int count, double length)
        {
            ICalculateWeight weightForCalculation = null;

            switch (width)
            {
                case 6:
                    weightForCalculation = new F6(count, length);
                    break;
                case 8:
                    weightForCalculation = new N8(count, length);
                    break;
                case 10:
                    weightForCalculation = new N10(count, length);
                    break;
                case 12:
                    weightForCalculation = new N12(count, length);
                    break;
                case 14:
                    weightForCalculation = new N14(count, length);
                    break;
                case 16:
                    weightForCalculation = new N16(count, length);
                    break;
                case 18:
                    weightForCalculation = new N18(count, length);
                    break;
                case 20:
                    weightForCalculation = new N20(count, length);
                    break;
            }

            return weightForCalculation;
        }
    }
}
