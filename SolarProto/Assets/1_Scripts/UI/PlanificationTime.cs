using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SolarProto
{
    public class PlanificationTime : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [SerializeField] private float pti;
        [SerializeField] private float ptu;
        [SerializeField] private float timeCount;

        [SerializeField] private Scrollbar slider;
        [SerializeField] private float animDuration;

        [SerializeField] private TextMeshProUGUI percentage;


        [ContextMenu("Test")]
        private void TestPTI()
        {
            SetTimeCount();
        }

        private void Start()
        {
            SetTimeCount();
        }

        public void SetTimeCount()
        {
            pti = playerData.GetTotalIPT();
            ptu = playerData.GetTotalSPT(); ;
            timeCount = pti + ptu;
            StartCoroutine(MoveScrollBar(timeCount, pti, ptu));
        }

        IEnumerator MoveScrollBar(float timeCount, float pti, float ptu)
        {

            float timer = 0f;

            while (timer < animDuration)
            {
                slider.size = Mathf.Lerp(0, pti / timeCount, timer / animDuration);
                percentage.text = (Mathf.Round(slider.size * 100)).ToString() + " %";

                yield return null;
                timer += Time.deltaTime;
            }
            slider.size = Mathf.Lerp(0, pti / timeCount, 1);


        }
    }
}

