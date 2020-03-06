using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public struct LevelData
    {
        public int levelId;
        public int playsAmount;
        public float initialPlanificationTime;
        public float subsequentPlanificationTime;

        public LevelData(int _id, int _plays, float _ipt, float _spt)
        {
            levelId = _id;
            playsAmount = _plays;
            initialPlanificationTime = _ipt;
            subsequentPlanificationTime = _spt;
        }

        public override string ToString()
        {
            return "id: " + levelId + " | " + "plays: " + playsAmount + " | " + "IPT: " + initialPlanificationTime + " | " + "SPT: " + subsequentPlanificationTime; 
        }
    }
}