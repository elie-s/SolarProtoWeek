using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SolarProto
{
    public class LoadSceneBehaviour : MonoBehaviour
    {
        [SerializeField] private int sceneIndex = 0;

        public void LoadScene()
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}