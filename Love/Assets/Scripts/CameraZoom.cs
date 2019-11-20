using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 2f;
    public float percentageAmount = 0.05f;

    private float _defaultSize;
    private float _amount;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _defaultSize = _camera.orthographicSize;
        _amount = _camera.orthographicSize * percentageAmount;
    }

    // returns true if there it should zoom again in the next frame
    public bool ZoomIn()
    {
        float amount = Mathf.Clamp(zoomSpeed * Time.deltaTime, 0f, _camera.orthographicSize - (_defaultSize - _amount));
        bool stopZooming = Mathf.Approximately(_camera.orthographicSize, _defaultSize * (1 - percentageAmount));

        _camera.orthographicSize -= amount;

        return !stopZooming;
    }

    // returns true if there it should zoom again in the next frame
    public bool ZoomOut()
    {
        float amount = Mathf.Clamp(zoomSpeed * Time.deltaTime, 0f, _defaultSize - _camera.orthographicSize);
        bool stopZooming = Mathf.Approximately(_camera.orthographicSize, _defaultSize);

        _camera.orthographicSize += amount;

        return !stopZooming;
    }
}