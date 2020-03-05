using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class Prevision : MonoBehaviour, INewtonian
    {
        private Vector3 direction;
        private float mass;

        private bool getForces = true;

        private void OnEnable()
        {
            FindObjectOfType<GravityManager>().AddNewtonian(this);
        }

        void Start()
        {

        }

        void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            transform.position += direction * Time.fixedDeltaTime;
        }

        public void ApplyForce(Vector3 _force)
        {
            if (!getForces) return;

            direction += _force;
        }

        public void Init(Vector3 _direction, float _mass)
        {
            direction = _direction;
            mass = _mass;
        }

        public float GetMass()
        {
            return mass;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public IEnumerator LifeSpanRoutine(float _lifeSpan)
        {
            yield return new WaitForSeconds(_lifeSpan);

            StartCoroutine(DestructionRoutine());
        }

        private IEnumerator DestructionRoutine()
        {
            Destroy(GetComponent<MeshRenderer>());
            getForces = false;
            direction = Vector3.zero;

            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }

        private void OnDisable()
        {
            FindObjectOfType<GravityManager>()?.RemoveNewtonian(this);
        }
    }
}