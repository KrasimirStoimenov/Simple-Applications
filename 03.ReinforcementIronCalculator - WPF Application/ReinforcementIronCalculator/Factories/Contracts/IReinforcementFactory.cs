using ReinforcementIronCalculator.Models.Contracts;

namespace ReinforcementIronCalculator.Factories.Contracts
{
    public interface IReinforcementFactory
    {
        public ICalculateWeight Create(int width,int count,double length);
    }
}
