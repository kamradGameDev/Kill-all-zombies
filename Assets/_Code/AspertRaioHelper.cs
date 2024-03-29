using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspertRaioHelper : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private const float DefaultAspectRatio = 1.77f; // iPhone 5 landscape ratio
    private const float DefaultOrthographicSize = 5f;

    private void Start()
    {
        _camera.orthographicSize = DefaultOrthographicSize;

        _camera.projectionMatrix = Matrix4x4.Ortho(
        -DefaultOrthographicSize * DefaultAspectRatio,
        DefaultOrthographicSize * DefaultAspectRatio,
        -DefaultOrthographicSize, DefaultOrthographicSize,
        _camera.nearClipPlane, _camera.farClipPlane);
    }
}
