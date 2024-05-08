using UnityEngine;
[ExecuteAlways]
public class CameraResizer : MonoBehaviour
{
    [SerializeField] Vector2 targetResolution;
    [SerializeField] float targetOrthographicSize = 5;
    Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        ResizeCameraOrthographicSize();
    }

    void ResizeCameraOrthographicSize()
    {
        var currentResolution = new Vector2(Screen.width, Screen.height);
        var targetAspect = targetResolution.x / targetResolution.y;
        var currentAspect = currentResolution.x / currentResolution.y;

        if (currentAspect >= targetAspect)
        {
            _camera.orthographicSize = targetOrthographicSize;
            return;
        }

        var orthoGraphicSize = targetOrthographicSize * (targetAspect / currentAspect);
        _camera.orthographicSize = orthoGraphicSize;
    }
}

