using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float distance = 5f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float minVerticalAngle = -45f;
    [SerializeField] float maxVerticalAngle = 45f;
    [SerializeField] float minHorizontalAngle = -45f;
    [SerializeField] float maxHorizontalAngle = 45f;
    [SerializeField] Vector2 framingOffset;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;
    float rotationY;
    float rotationX;
    float invertXVal;
    float invertYVal;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;
        rotationX += Input.GetAxis("Camera Y") * invertYVal * rotationSpeed;
        rotationY += Input.GetAxis("Camera X") * invertXVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        //rotationY = Mathf.Clamp(rotationY, minHorizontalAngle, maxHorizontalAngle);
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y, 0) ;
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, 5);
        transform.rotation = targetRotation;
    }

    public Quaternion PlannarRotation => Quaternion.Euler(0, rotationY, 0);
}
