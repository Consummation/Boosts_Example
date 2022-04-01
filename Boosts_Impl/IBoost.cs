using HellWheels.Car;
using System;

namespace HellWheels.Boosts
{
    public interface IBoost
    {
        /// <summary>
        /// Use the boost. Some boost usage can be continious, so use <paramref name="onUsed"/> callback to execute
        /// code after the boost usage
        /// </summary>
        /// <param name="onUsed">Callback on boost have being used</param>
        void Use(Action onUsed);

        void Dispose();
        BoostType BoostType { get; }
        ICar Target { get; }
    }
}
