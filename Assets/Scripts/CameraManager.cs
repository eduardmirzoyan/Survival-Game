using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField, ReadOnly] private Camera cam;

    public static CameraManager instance;
    private void Awake()
    {
        // Singleton Logic
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        cam = GetComponent<Camera>();
    }

    public Vector3 GetCameraWorldPosition()
    {
        Vector3 worldPosition = transform.position;
        worldPosition.z = 0f;

        return worldPosition;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;

        return worldPosition;
    }
}
