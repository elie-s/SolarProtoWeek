using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class GravityManager : MonoBehaviour
    {
        [SerializeField] private ShipController ship = default;
        [SerializeField] private float gravitationnalForceModifier = 1.0f;
        [SerializeField] private float gravitationalForce = (float)(6.67430d * System.Math.Pow(10, -11));

        private CelestialBody[] celestialBodies;
        private IEnumerator sendingForcesRoutine;
        private bool stop = false;

        // Start is called before the first frame update
        void Start()
        {
            InitBodies();
        }

        [ContextMenu("Start")]
        public void StartSendForces()
        {
            StopSendForces();

            sendingForcesRoutine = SendForcesRoutine();
            StartCoroutine(sendingForcesRoutine);
        }

        [ContextMenu("Stop")]
        public void StopSendForces()
        {
            stop = true;
            if(sendingForcesRoutine != null)StopCoroutine(sendingForcesRoutine);
        }

        private void InitBodies()
        {
            celestialBodies = FindObjectsOfType<CelestialBody>();
        }

        private void SetForcesToShip()
        {
            Vector3 force = default;

            foreach (CelestialBody body in celestialBodies)
            {
                force += body.GravitationalForce(ship.transform.position, ship.mass, gravitationalForce * gravitationnalForceModifier);
            }

            Debug.Log(celestialBodies.Length);

            ship.GetForces(force);
        }

        private IEnumerator SendForcesRoutine()
        {
            stop = false;

            while (!stop)
            {
                SetForcesToShip();

                yield return new WaitForFixedUpdate();
            }
        }
    }
}