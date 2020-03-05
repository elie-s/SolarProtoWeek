using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SolarProto
{
    public class OrbitsManager : MonoBehaviour
    {
        private List<OrbitMotion> orbitMotions;

        // Start is called before the first frame update
        void Start()
        {
            GetList();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) StopMotions();
            else if (Input.GetMouseButtonUp(0)) StartMotions();
        }

        private void GetList()
        {
            orbitMotions = FindObjectsOfType<OrbitMotion>().ToList();
        }

        
        public void StartMotions()
        {
            foreach (OrbitMotion orbit in orbitMotions)
            {
                orbit.SetMotion(true);
            }
        }

        public void StopMotions()
        {
            foreach (OrbitMotion orbit in orbitMotions)
            {
                orbit.SetMotion(false);
            }
        }
    }
}