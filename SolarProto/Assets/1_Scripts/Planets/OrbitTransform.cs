using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    [ExecuteAlways]
    public class OrbitTransform : MonoBehaviour
    {
        public Transform origin;
        public float distance;

        public Vector3 originPos => origin ? origin.position : Vector3.zero;

        private void Update()
        {
            if (!Application.isPlaying) SetDistance();
        }

        private void Awake()
        {
            SetDistance();
        }

        public Vector3 Evaluate(float t)
        {
            float angle = Mathf.Deg2Rad * 360.0f * t;
            Vector3 result = new Vector3(Mathf.Sin(angle) * distance, 0.0f, Mathf.Cos(angle) * distance);

            return result + originPos;
        }

        public float Evaluate()
        {
            Vector3 vector = originPos - transform.position;
            float result = 1.0f - (Mathf.Atan2(vector.z, vector.x) + Mathf.PI) / (2 * Mathf.PI) + 0.25f;

            if (result > 1.0f) result -= 1.0f;

            return result;
        }

        private void SetDistance()
        {
            distance = Vector3.Distance(transform.position, originPos);
        }
    }
}