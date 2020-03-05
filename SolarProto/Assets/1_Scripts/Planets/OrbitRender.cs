using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    [ExecuteAlways]
    public class OrbitRender : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer = default;
        [SerializeField] private OrbitTransform orbitTransform = default;
        [SerializeField, Range(3, 359)] private int segments = 120;

        Vector3[] points;

        private void Start()
        {
            CalculateEllipse();
        }

        private void Update()
        {
            CalculateEllipse();
        }

        private void CalculateEllipse()
        {
            points = new Vector3[segments + 1];

            for (int i = 0; i < segments; i++)
            {
                points[i] = orbitTransform.Evaluate((float)i / (float)segments);
            }

            points[segments] = points[0];

            DrawEllipse();
        }

        private void DrawEllipse()
        {
            lineRenderer.positionCount = segments + 1;
            Vector3[] positions = new Vector3[points.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = points[i];
            }

            lineRenderer.SetPositions(positions);
        }

        private void OnValidate()
        {
            CalculateEllipse();
        }
    }
}