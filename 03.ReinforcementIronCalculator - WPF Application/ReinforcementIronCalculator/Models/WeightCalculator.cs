using ReinforcementIronCalculator.Models.Contracts;
using System;

namespace ReinforcementIronCalculator.Models
{
    public abstract class WeightCalculator : ICalculateWeight
    {
        private double weightPerMeter;
        protected WeightCalculator(int count, double length, double weightPerMeter)
        {
            this.Count = count;
            this.Length = length;
            this.weightPerMeter = weightPerMeter;
        }
        protected int Count { get; }

        protected double Length { get; }

        public double CalculateWeight()
        {
            return this.Count * this.Length * this.weightPerMeter;
        }
    }
}
