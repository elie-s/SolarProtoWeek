using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    [ExecuteAlways]
    public class OrbitMotion : MonoBehaviour
    {
        [SerializeField] private Transform origin = default;
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private float speedMultiplier = 1.0f;
        [SerializeField] private OrbitTransform orbitTransform = default;
        [SerializeField] private float previsionTime = 3.0f;
        [SerializeField] private float time = 0.0f;
        [SerializeField] private LineRenderer previsionLine = default;
        [SerializeField] private bool startRandom = false;
        [SerializeField] private bool facingOrigin = false;

        private bool isOrbiting = false;
        private float startTime;
        private Vector3 lastPos;
        private float lastSpeed;

        private bool moved => lastPos != transform.position;
        private bool changedSpeed => lastSpeed != speed;

        private void Awake()
        {
            lastPos = transform.position;
            SetTime();
            startTime = time;
            
        }

        private void Start()
        {
            if (Application.isPlaying && startRandom) RandomStart();

            SetPrevision();
            PrevisionSetActive(true);
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                SetTime();
                if(moved || changedSpeed)
                {
                    SetPrevision();
                    PrevisionSetActive(true);
                }
            }

            lastPos = transform.position;
            lastSpeed = speed;

            if(origin && facingOrigin) transform.LookAt(origin);
        }

        private void LateUpdate()
        {
            
        }

        void FixedUpdate()
        {
            if(isOrbiting) Orbit();
        }

        private void RandomStart()
        {
            transform.localScale *= Random.value * 5 + 1;
            speed = Random.value * 2 - 1.0f;
            time = Random.value;
            startTime = time;
            transform.position = orbitTransform.Evaluate(time);
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

        private float IncrementTime(float _timeRef, Vector3 _position)
        {
            float value = _timeRef;
            float ratio = 15.0f / Vector3.Distance(_position, origin.position);
            value += speedMultiplier * speed * Time.fixedDeltaTime * ratio;

            return CycleTime(value);
        }

        private Vector3[] PrevisionPositions(float _duration)
        {
            float tmpTime = time;
            int iterations = Mathf.RoundToInt(_duration / Time.fixedDeltaTime);
            Vector3 position = transform.position;
            List<Vector3> positions = new List<Vector3>();

            for (int i = 0; i < iterations; i++)
            {
                positions.Add(position);
                tmpTime = IncrementTime(tmpTime, position);
                position = orbitTransform.Evaluate(tmpTime);
            }

            return positions.ToArray();
        }

        private void SetPrevision()
        {
            Vector3[] positions = PrevisionPositions(previsionTime);

            previsionLine.positionCount = positions.Length;
            previsionLine.SetPositions(positions);
        }

        private void PrevisionSetActive(bool _value)
        {
            previsionLine.enabled = _value;
        }

        private void CycleTime()
        {
            if (speed > 0 && time > 1.0f) time = time - 1.0f;
            else if (speed < 0 && time < 0) time = time + 1.0f;
        }

        private float CycleTime(float _refTime)
        {
            float value = _refTime;    

            if (speed > 0 && value > 1.0f) value = value - 1.0f;
            else if (speed < 0 && value < 0) value = value + 1.0f;

            return value;
        }

        public void SetMotion(bool _value)
        {
            isOrbiting = _value;
            PrevisionSetActive(!_value);

            if (!_value) SetPrevision();
        }

        public void ResetPosition()
        {
            time = startTime;
            transform.position = orbitTransform.Evaluate(time);
        }
    }
}