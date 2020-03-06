using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData = default;
        [SerializeField] private int levelID = 0;
        [SerializeField] private Phase phase = Phase.Initial;

        private int plays = 0;
        private float ipt = 0.0f;
        private float spt = 0.0f;
        private bool stop;

        // Start is called before the first frame update
        void Start()
        {
            if (levelID == 0) playerData.Init();
            StartCoroutine(PhaseRoutine());
        }

        private IEnumerator PhaseRoutine()
        {
            phase = Phase.Initial;

            while (!Input.GetMouseButtonUp(0) && !Gesture.ReleasedMovementTouch)
            {
                yield return null;
                ipt += Time.deltaTime;
            }

            plays++;

            while (!stop)
            {
                phase = Phase.Play;

                if (Input.GetMouseButton(0) || Gesture.GettingTouch) plays++;

                while (Input.GetMouseButton(0) || Gesture.GettingTouch)
                {
                    phase = Phase.Subsequent;

                    yield return null;
                    spt += Time.deltaTime;
                }

                yield return null;
            }
        }

        public void SaveData()
        {
            stop = true;
            playerData.AddLevelData(new LevelData(levelID, plays, ipt, spt));
        }

        public enum Phase { Initial, Subsequent, Play}
    }
}