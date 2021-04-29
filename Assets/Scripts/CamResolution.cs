using System.Collections;
using UnityEngine;

public class CamResolution : MonoBehaviour
{
    Camera cam;
    float camStartSize;
    float targetAspect;
    float targetAspectWidth = 42;
    float targetAspectHeight = 17;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        targetAspect = targetAspectWidth / targetAspectHeight;
        camStartSize = cam.orthographicSize;
    }
    private void Start()
    {
        StartCoroutine(GetScreenRatio());
    }

    IEnumerator GetScreenRatio()
    {
        while (enabled)
        {
            cam.orthographicSize = camStartSize * targetAspect / cam.aspect;
            yield return null;
        }
    }
}
