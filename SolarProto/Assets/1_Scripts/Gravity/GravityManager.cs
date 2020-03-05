using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class GravityManager : MonoBehaviour
    {
        [SerializeField] private List<INewtonian> newtonians = new List<INewtonian>();
        [SerializeField] private float gravitationnalForceModifier = 1.0f;
        [SerializeField] private float gravitationalForce = (float)(6.67430d * System.Math.Pow(10, -11));

        private CelestialBody[] celestialBodies;

        // Start is called before the first frame update
        void Start()
        {
            InitBodies();
        }

        private void FixedUpdate()
        {
            SetForcesToShips();
        }

        public void AddNewtonian(INewtonian _newtonian)
        {
            newtonians.Add(_newtonian);
        }

        public void RemoveNewtonian(INewtonian _newtonian)
        {
            newtonians.Remove(_newtonian);
        }

        private void InitBodies()
        {
            celestialBodies = FindObjectsOfType<CelestialBody>();
        }

        private void SetForcesToShips()
        {
            foreach (INewtonian ship in newtonians)
            {
                SetForcesToShip(ship);
            }
        }

        public void SetForcesToShip(INewtonian _ship)
        {
            Vector3 force = default;

            foreach (CelestialBody body in celestialBodies)
            {
                if(body) force += body.GravitationalForce(_ship.GetPosition(), _ship.GetMass(), gravitationalForce * gravitationnalForceModifier);
            }

            Debug.Log(celestialBodies.Length);

            _ship.ApplyForce(force);
        }
    }
}