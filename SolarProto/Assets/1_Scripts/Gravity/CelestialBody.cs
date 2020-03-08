using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class CelestialBody : MonoBehaviour
    {
        [SerializeField] private float massReference = 1.0f;
        [SerializeField] private float massMultiplier = 1.0f;
        [SerializeField] private float maxDistance = 0.0f;
        [SerializeField] private bool scaleMass = true;
        [Header("Force debug")]
        [SerializeField] private float forceDebug = 5.0f;
        [SerializeField] private float shipMass = 1000.0f;

        public float Mass => massReference * (scaleMass ? 4.0f / 3.0f * Mathf.PI * transform.localScale.x : 1.0f) * massMultiplier;

        public Vector3 GravitationalForce(Vector3 _pos, float _mass, float _grav)
        {
            if (maxDistance > 0.0f && Vector3.Distance(_pos, transform.position) > maxDistance) return Vector3.zero;

            Vector3 direction = transform.position - _pos;
            Vector3 result = direction.normalized * (_grav * (Mass * _mass) / Mathf.Pow(direction.magnitude, 2));

            return result;
        }

        public float Force(float _distance)
        {
            return GravityManager.GravitationalForce * shipMass * Mass / Mathf.Pow(_distance, 1.0f);
        }

        public float DistanceFor(float _force)
        {
            return Mathf.Sqrt(GravityManager.GravitationalForce * Mass * shipMass / _force);
        }

        public void SetValues(float _massRef, float _massMultiplier, float _maxDistance)
        {
            massReference = _massRef;
            massMultiplier = _massMultiplier;
            maxDistance = _maxDistance;
        }

        public float[] GetValues()
        {
            return new float[3] { massReference, massMultiplier, maxDistance };
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red ;
            Gizmos.DrawWireSphere(transform.position, DistanceFor(forceDebug));
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, DistanceFor(forceDebug / 10));
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, DistanceFor(forceDebug / 50));
        }
    }
}