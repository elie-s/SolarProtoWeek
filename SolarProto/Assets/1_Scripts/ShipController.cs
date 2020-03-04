using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField] private GravityManager gravityManager = default;
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float orientationLerpForce = 0.25f;

        public float mass = 1.0f;

        private Vector3 direction;
        private bool stop = false;

        private void Start()
        {
            direction = transform.forward * speed;
        }

        void Update()
        {
            Rotate();
        }

        public void Launch()
        {
            Debug.Log(direction);
            StartCoroutine(MoveRoutine());
        }

        public void Stop()
        {
            stop = true;
        }

        public void SetDirection(Vector3 _direction)
        {
            //Debug.Log("direction's magnitude: " + _direction.magnitude);
            direction = new Vector3(_direction.x, direction.y, _direction.y) * speed;
        }

        private IEnumerator MoveRoutine()
        {
            stop = false;
            gravityManager.StartSendForces();

            while (!stop)
            {
                Move();

                yield return new WaitForFixedUpdate();
            }

            gravityManager.StopSendForces();
        }

        private void Move()
        {
            transform.position += direction * Time.fixedDeltaTime;
        }

        private void Rotate()
        {
            transform.forward = Vector3.Lerp(transform.forward, direction, orientationLerpForce).normalized;
        }

        public void GetForces(Vector3 _forces)
        {
            direction += _forces;
        }
    }
}
