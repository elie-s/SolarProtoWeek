using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace SolarProto
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private int totalScore;

        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] private float animDuration;

        public void SetScore()
        {
            totalScore = playerData.GetScore();
            StartCoroutine(IncreaseScoreText(totalScore));
        }

        [ContextMenu("Test")]
        public void LaunchCoroutine()
        {
            StartCoroutine(IncreaseScoreText(totalScore));
        }

        IEnumerator IncreaseScoreText(int totalScore)
        {
            float timer = 0f;

            while (timer < animDuration)
            {
                scoreText.text = Mathf.Round(Mathf.Lerp(0, totalScore, timer / animDuration)).ToString() + " points";

                yield return null;
                timer += Time.deltaTime;
            }

            scoreText.text = Mathf.Round(Mathf.Lerp(0, totalScore, 1)).ToString() + " points";
        }
    }
}

