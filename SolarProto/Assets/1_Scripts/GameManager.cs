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
            ResetShip();
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
            else if (Input.GetMouseButton(0) || Gesture.GettingTouch) orbits.StopMotions();
        }

        private void ResetShip()
        {
            DestroyShip();

            ship = Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation);
            ShipController shipController = ship.GetComponent<ShipController>();
            gravityManager.AddNewtonian(shipController);
            inputManager.SetShip(shipController);
            camController.SetShip(ship.transform);
            shipController.SetWin(Win);
            shipController.SetCrash(Crash);
        }

        public void DestroyShip()
        {
            gravityManager.RemoveNewtonian(ship.GetComponent<ShipController>());
            Destroy(ship);
        }

        public void Crash()
        {
            DestroyShip();
            Debug.Log("Boum ! <3");
        }

        public void Win()
        {
            Debug.Log("Win !");
        }
    }
}