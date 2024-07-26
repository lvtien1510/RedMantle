using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround2 : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        Vector3 newPosition = transform.position + new Vector3(deltaMovement.x * parallaxEffectMultiplier, 0);
        transform.position = new Vector3(newPosition.x, cameraTransform.position.y, transform.position.z);
        lastCameraPosition = cameraTransform.position;
    }

}
