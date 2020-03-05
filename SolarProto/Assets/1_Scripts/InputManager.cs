using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera gameCamera = default;
        [SerializeField] private ShipController ship = default;
        [SerializeField] private float magnitudeMax = 1.0f;

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) StartCoroutine(GetVectorInput());
        }

        private IEnumerator GetVectorInput()
        {
            Vector2 startPos = AdjustedViewportRatioPosition(Input.mousePosition);
            Vector3 startPosRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCamera.nearClipPlane) ;
            Vector2 direction = Vector2.zero;

            while (Input.GetMouseButton(0))
            {
                Debug.DrawLine(gameCamera.ScreenToWorldPoint(startPosRaw), gameCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCamera.nearClipPlane)), Color.green);
                direction = -(AdjustedViewportRatioPosition(Input.mousePosition) - startPos);
                direction = direction.normalized * Mathf.Clamp01(direction.magnitude / magnitudeMax);

                ship.SetDirection(direction);

                yield return null;
            }

            Debug.Log(direction.magnitude);
            ship.SetDirection(direction);

            ship.Launch();
        }

        private Vector2 AdjustedViewportRatioPosition(Vector2 _OnScreenPosition)
        {
            Vector2 result = gameCamera.ScreenToViewportPoint(_OnScreenPosition);
            float screenWidthHeightRatio = (float)Screen.height / (float)Screen.width;

            //Debug.Log(new Vector2(result.x, result.y * screenWidthHeightRatio));

            return new Vector2(result.x, result.y * screenWidthHeightRatio);
        }
    }
}