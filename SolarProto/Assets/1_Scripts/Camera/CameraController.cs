using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform positionController = default;
    [SerializeField] private Transform cameraTransform = default;
    [SerializeField] private Transform sunTransform = default;
    [SerializeField] private Transform gateTransform = default;
    [SerializeField] private Transform shipTransform = default;
    [SerializeField] private float sunShipLerpValue = 0.20f;
    [SerializeField] private float cameraLerpValue = 0.15f;
    [SerializeField] private float minDistance = 5.0f;
    [SerializeField] private float maxdistance = 50.0f;
    [SerializeField] private float minZoomDistance = 5.0f;
    [SerializeField] private float maxZoomDistance = 130.0f;
    [SerializeField] private bool startAboveSpawnPoint = false;
    [SerializeField] private Transform spawnPoint = default;


    private Vector2 positionController2DProjection => new Vector2(positionController.position.x, positionController.position.z);
    private Vector2 sun2DProjection => new Vector2(sunTransform.position.x, sunTransform.position.z);
    private Vector2 gate2DProjection => new Vector2(gateTransform.position.x, gateTransform.position.z);
    private Vector2 ship2DProjection => shipTransform ? new Vector2(shipTransform.position.x, shipTransform.position.z) : Vector2.zero;

    private float lerpZoom => (Mathf.Clamp(Vector3.Distance(sunTransform.position, shipTransform ? shipTransform.position : Vector3.zero), minDistance, maxdistance) - minDistance) / (maxdistance - minDistance);

    void Start()
    {
        if (startAboveSpawnPoint) SetAboveSpawnPoint(15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        PlaneProjection();
        Zoom();
        MoveCamera();
    }

    public void SetShip(Transform _ship)
    {
        shipTransform = _ship;
    }

    private void PlaneProjection()
    {
        Vector2 planeProjection = Vector2.Lerp(sun2DProjection, ship2DProjection, sunShipLerpValue);
        if (Vector3.Distance(sunTransform.position, shipTransform ? shipTransform.position : Vector3.zero) > maxdistance) positionController.position = new Vector3(sun2DProjection.x, positionController.position.y, sun2DProjection.y);
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

    private void SetAboveSpawnPoint(float _yValue)
    {
        cameraTransform.position = spawnPoint.position + Vector3.up * _yValue;

        StartCoroutine(SetLerpValue(cameraLerpValue, 5.0f));
    }

    private IEnumerator SetLerpValue(float _value, float _duration)
    {
        float time = 0.0f;

        yield return null;

        while (time < _duration)
        {
            cameraLerpValue = Mathf.Lerp(0.0f, _value, time / _duration);

            yield return null;
            time += Time.deltaTime;
        }

        cameraLerpValue = _value;
    }
}
