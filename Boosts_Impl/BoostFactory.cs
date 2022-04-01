using HellWheels.Boosts.Oil;
using HellWheels.Boosts.Rocket;
using HellWheels.Boosts.Saw;
using HellWheels.Boosts.SpeedUp;
using HellWheels.Car;
using System.Collections.Generic;
using UnityEngine;

namespace HellWheels.Boosts
{
    public class BoostFactory : IBoostFactory
    {

        private const string OIL_PREFAB_PATH = "Boosts/Oil/OilPrefab";
        private const string ROCKET_PREFAB_PATH = "Boosts/Rocket/RocketPrefab";
        private const string SAW_PREFAB_PATH = "Boosts/Saw/SawPrefab";
        private const string SPEEDUP_PREFAB_PATH = "Boosts/SpeedUp/SpeedUpPrefab";

        public IBoost CreateOil(Vector3 localPos, Transform parentTransform, ICar selfCar)
        {
            GameObject prefab = Resources.Load<GameObject>(OIL_PREFAB_PATH);
            GameObject obj = UnityEngine.Object.Instantiate(prefab, parentTransform.position, parentTransform.rotation, parentTransform);
            obj.transform.localPosition = localPos;
            OilBoost oilBoost = obj.GetComponent<OilBoost>();
            oilBoost.Initialize(parentTransform, selfCar);
            return oilBoost;
        }

        public IBoost CreateRocket(Vector3 localPos, Transform parentTransform, ICar selfCar, List<ICar> allCars, IAudioManager audioManager)
        {
            GameObject prefab = Resources.Load<GameObject>(ROCKET_PREFAB_PATH);
            GameObject obj = UnityEngine.Object.Instantiate(prefab, parentTransform.position, parentTransform.rotation, parentTransform);
            obj.transform.localPosition = localPos;
            RocketBoost rocketBoost = obj.GetComponent<RocketBoost>();
            rocketBoost.Initialize(audioManager, selfCar, allCars);
            return rocketBoost;
        }

        public IBoost CreateSaw(Vector3 localPos, Transform parentTransform, ICar selfCar, List<ICar> allCars)
        {
            GameObject prefab = Resources.Load<GameObject>(SAW_PREFAB_PATH);
            GameObject obj = UnityEngine.Object.Instantiate(prefab, parentTransform.position, parentTransform.rotation, parentTransform);
            obj.transform.localPosition = localPos;
            SawBoost sawPrefab = obj.GetComponent<SawBoost>();
            sawPrefab.Initialize(selfCar, allCars);
            return sawPrefab;
        }

        public IBoost CreateSpeedUp(Vector3 localPos, Transform parentTransform, ICar selfCar, IRacingCamera racingCamera)
        {
            GameObject prefab = Resources.Load<GameObject>(SPEEDUP_PREFAB_PATH);
            GameObject obj = UnityEngine.Object.Instantiate(prefab, parentTransform.position, parentTransform.rotation, parentTransform);
            obj.transform.localPosition = localPos;
            SpeedUpBoost speedUpPrefab = obj.GetComponent<SpeedUpBoost>();
            speedUpPrefab.Initialize(selfCar, racingCamera);
            return speedUpPrefab;
        }
    }
}
