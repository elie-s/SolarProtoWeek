using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform positionController = default;
    [SerializeField] private Transform cameraTransform = default;
    [SerializeField] private Transform sunTransform = default;
    [SerializeField] private Transform shipTransform = default;
    [SerializeField] private float sunShipLerpValue = 0.20f;
    [SerializeField] private float cameraLerpValue = 0.15f;
    [SerializeField] private float minDistance = 5.0f;
    [SerializeField] private float maxdistance = 50.0f;
    [SerializeField] private float minZoomDistance = 5.0f;
    [SerializeField] private float maxZoomDistance = 130.0f;


    private Vector2 positionController2DProjection => new Vector2(positionController.position.x, positionController.position.z);
    private Vector2 sun2DProjection => new Vector2(sunTransform.position.x, sunTransform.position.z);
    private Vector2 ship2DProjection => new Vector2(shipTransform.position.x, shipTransform.position.z);

    private float lerpZoom => (Mathf.Clamp(Vector3.Distance(sunTransform.position, shipTransform.position), minDistance, maxdistance) - minDistance) / (maxdistance - minDistance);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlaneProjection();
        Zoom();
        MoveCamera();
    }

    private void PlaneProjection()
    {
        Vector2 planeProjection = Vector2.Lerp(sun2DProjection, ship2DProjection, sunShipLerpValue);
        if (Vector3.Distance(sunTransform.position, shipTransform.position) > maxdistance) positionController.position = new Vector3(sun2DProjection.x, positionController.position.y, sun2DProjection.y);
        else positionController.position = new Vector3(planeProjection.x, positionController.position.y, planeProjection.y);
    }

    private void Zoom()
    {
        positionController.position = new Vector3(positionController.position.x, Mathf.Lerp(minZoomDistance, maxZoomDistance, lerpZoom), positionController.position.z);
    }

    private void MoveCamera()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, positionController.position, cameraLerpValue);
    }
}
