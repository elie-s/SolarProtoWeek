using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    [ExecuteAlways]
    public class OrbitMotion : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private float speedMultiplier = 1.0f;
        [SerializeField] private OrbitTransform orbitTransform = default;
        [SerializeField] private float time = 0.0f;

        private bool isOrbiting = false;

        private void Awake()
        {
            SetTime();
        }

        private void Update()
        {
            if (!Application.isPlaying) SetTime();
        }

        void FixedUpdate()
        {
            if(isOrbiting) Orbit();
        }

        private void Rotate()
        {
            transform.RotateAround(origin.position, Vector3.up, speedMultiplier * speed * Time.fixedDeltaTime / Mathf.Pow(Vector3.Distance(transform.position, origin.position), 2));
        }

        private void Orbit()
        {
            transform.position = orbitTransform.Evaluate(time);
            IncrementTime();
        }

        private void SetTime()
        {
            time = orbitTransform.Evaluate();
        }

        private void IncrementTime()
        {
            float ratio = 15.0f / Vector3.Distance(transform.position, origin.position);
            time += speedMultiplier * speed * Time.fixedDeltaTime * ratio;

            CycleTime();
        }

        private void CycleTime()
        {
            if (speed > 0 && time > 1.0f) time = time - 1.0f;
            else if (speed < 0 && time < 0) time = time + 1.0f;
        }

        public void SetMotion(bool _value)
        {
           isOrbiting = _value;
        }
    }
}