using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private float targetAspect = 9f / 16f;
    private float defaultVerticalFOV = 60f;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        float currentAspect = (float)Screen.width / Screen.height;
        float horizontalFOV = 2f * Mathf.Atan(Mathf.Tan(defaultVerticalFOV * Mathf.Deg2Rad / 2f) * targetAspect);

        float adjustedVerticalFOV = 2f * Mathf.Atan(Mathf.Tan(horizontalFOV / 2f) / currentAspect) * Mathf.Rad2Deg;
        cam.fieldOfView = adjustedVerticalFOV;
    }
}
