using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    [System.Serializable]
    public class Orbits
    {
        public float xAxis;
        public float yAxis;
        public Vector3 origin;

        public Orbits(Transform _body, Transform _origin)
        {
            float distance = Vector3.Distance(_origin.position, _body.position);

            origin = _origin.position;

            xAxis = distance;
            yAxis = distance;
        }

        public Vector3 Evaluate(float t)
        {
            float angle = Mathf.Deg2Rad * 360.0f * t;
            Vector3 result = new Vector3(Mathf.Sin(angle) * xAxis, 0.0f, Mathf.Cos(angle) * yAxis);

            return result+origin;
        }
    }
}