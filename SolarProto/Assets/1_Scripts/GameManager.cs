using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject shipPrefab = default;
        [SerializeField] private Transform spawnPoint = default;
        [SerializeField] private GameObject ship = default;
        [SerializeField] private GravityManager gravityManager = default;
        [SerializeField] private CameraController camController = default;
        [SerializeField] private OrbitsManager orbits = default;
        [SerializeField] private InputManager inputManager = default;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) || Gesture.DoubleTouch)
            {
                ResetShip();
                orbits.StopMotions();
                orbits.ResetMotions();
            }
        }

        private void ResetShip()
        {
            DestroyShip();

            ship = Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation);
            gravityManager.AddNewtonian(ship.GetComponent<ShipController>());
            inputManager.SetShip(ship.GetComponent<ShipController>());
            camController.SetShip(ship.transform);
        }

        public void DestroyShip()
        {
            gravityManager.RemoveNewtonian(ship.GetComponent<ShipController>());
            Destroy(ship);
        }
    }
}