using HellWheels.Car;
using System.Collections.Generic;
using UnityEngine;

namespace HellWheels.Boosts
{
    public interface IBoostFactory
    {
        IBoost CreateRocket(Vector3 localPos, Transform parentTransform, ICar selfCar, List<ICar> allCars, IAudioManager audioManager);

        IBoost CreateSaw(Vector3 localPos, Transform parentTransform, ICar selfCar, List<ICar> allCars);

        IBoost CreateOil(Vector3 localPos, Transform parentTransform, ICar selfRacer);

        IBoost CreateSpeedUp(Vector3 localPos, Transform parentTransform, ICar selfCar, IRacingCamera racingCamera);
    }
}
