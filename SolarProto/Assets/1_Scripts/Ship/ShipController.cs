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
        [SerializeField] private CelestialBody body = default;
        [SerializeField] private GameObject previsionPrefab = default;
        [SerializeField] private PrevisionLine prevision = default;
        [SerializeField] private float previsionDuration = 4.0f;
        [SerializeField] private int previsionFrequency = 5;

        public float mass = 1.0f;

        private Vector3 direction;
        private bool stop = false;
        private bool getForces = false;

        private void OnEnable()
        {
            direction = transform.forward * speed;
            FindObjectOfType<GravityManager>().AddNewtonian(this);
        }

        void Update()
        {
            Rotate();
            if (Input.GetMouseButton(0)) prevision.Simulation(previsionDuration, transform.position, mass, direction, previsionFrequency);
            if (Input.GetMouseButtonUp(0)) prevision.Reset();
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

        private IEnumerator PrevisionRoutine()
        {
            yield return new WaitForSeconds(0.5f);

            LaunchPrevision();

            yield return new WaitForSeconds(1.5f);

            if (Input.GetMouseButton(0)) StartCoroutine(PrevisionRoutine());
        }

        private void LaunchPrevision()
        {
            Prevision prevision = Instantiate(previsionPrefab, transform.position, Quaternion.identity).GetComponent<Prevision>();
            prevision.Init(direction, mass);
            StartCoroutine(prevision.LifeSpanRoutine(2.0f));
        }

        private void OnDisable()
        {
            FindObjectOfType<GravityManager>()?.RemoveNewtonian(this);
        }
    }
}
