using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    [CreateAssetMenu()]
    public class PlayerData : ScriptableObject
    {
        public List<LevelData> levelData { get; private set; }

        public void Init()
        {
            levelData = new List<LevelData>();
        }

        public void AddLevelData(LevelData _data)
        {
            levelData.Add(_data);
        }

        public float GetTotalIPT()
        {
            float result = 0.0f;

            foreach (LevelData data in levelData)
            {
                result += data.initialPlanificationTime;
            }

            return result;
        }

        public float GetTotalSPT()
        {
            float result = 0.0f;

            foreach (LevelData data in levelData)
            {
                result += data.subsequentPlanificationTime;
            }

            return result;
        }
    }
}