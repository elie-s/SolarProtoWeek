using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public interface INewtonian
    {
        void ApplyForce(Vector3 _force);
        Vector3 GetPosition();
        float GetMass();
    }
}