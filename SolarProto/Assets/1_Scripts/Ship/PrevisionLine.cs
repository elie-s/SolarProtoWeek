using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class PrevisionLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer = default;
        [SerializeField] private GravityManager gravityManager = default;
        private SimulationObject simulation;

        public void Simulation(float _duration, Vector3 _position, float _mass, Vector3 _directionInitial, int _frequency)
        {
            simulation = new SimulationObject(_position, _mass, _directionInitial);

            int iterations = Mathf.RoundToInt(_duration / Time.fixedDeltaTime);
            List<Vector3> positions = new List<Vector3>();

            int frequency = 0;

            for (int i = 0; i < iterations; i++)
            {
                if(frequency == 0) positions.Add(simulation.GetPosition());
                gravityManager.SetForcesToShip(simulation);
                simulation.Move();

                frequency++;
                if (frequency == _frequency) frequency = 0;
            }

            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }

        public void Reset()
        {
            lineRenderer.positionCount = 0;
        }
    }

    public class SimulationObject : INewtonian
    {
        public Vector3 position;
        public float mass;
        public Vector3 direction;

        public SimulationObject(Vector3 _position, float _mass, Vector3 _directionInitial)
        {
            position = _position;
            mass = _mass;
            direction = _directionInitial;
        }

        public void Move()
        {
            position += direction * Time.fixedDeltaTime;
        }

        public void ApplyForce(Vector3 _force)
        {
            direction += _force;
        }

        public float GetMass()
        {
            return mass;
        }

        public Vector3 GetPosition()
        {
            return position;
        }
    }
}