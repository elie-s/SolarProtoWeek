using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class ShipController : MonoBehaviour, INewtonian
    {
        [SerializeField] private GravityManager gravityManager = default;
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float orientationLerpForce = 0.25f;
        [SerializeField] private PrevisionLine prevision = default;
        [SerializeField] private float previsionDuration = 4.0f;
        [SerializeField] private int previsionFrequency = 5;

        private System.Action crash;
        private System.Action win;

        public float mass = 1.0f;

        private Vector3 direction;
        private bool stop = false;
        private bool getForces = false;

        private void OnEnable()
        {
            direction = transform.forward * speed;
            FindObjectOfType<GravityManager>().AddNewtonian(this);
            prevision = FindObjectOfType<PrevisionLine>();
        }

        void Update()
        {
            Rotate();
            if (Input.GetMouseButton(0) || Gesture.GettingTouch)
            {
                Stop();
                prevision.Simulation(previsionDuration, transform.position, mass, direction, previsionFrequency);
            }
            if (Input.GetMouseButtonUp(0) || Gesture.ReleasedMovementTouch) prevision.Reset();
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
            getForces = true;
            while (!stop)
            {
                Move();

                yield return new WaitForFixedUpdate();
            }

            getForces = false;
        }

        private void Move()
        {
            transform.position += direction * Time.fixedDeltaTime;
        }

        private void Rotate()
        {
            transform.forward = Vector3.Lerp(transform.forward, direction, orientationLerpForce).normalized;
        }

        public void ApplyForce(Vector3 _force)
        {
            if (!getForces) return;

            direction += _force;
        }

        public float GetMass()
        {
            return mass;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void OnDisable()
        {
            FindObjectOfType<GravityManager>()?.RemoveNewtonian(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision detected.");

            if(other.tag == "Crash")
            {
                crash();
            }
            else if(other.tag == "Portal")
            {
                win();
                getForces = false;
                SetDirection(other.transform.forward);
            }
        }

        public void SetCrash(System.Action _action)
        {
            crash = _action;
        }

        public void SetWin(System.Action _action)
        {
            win = _action;
        }
    }
}
