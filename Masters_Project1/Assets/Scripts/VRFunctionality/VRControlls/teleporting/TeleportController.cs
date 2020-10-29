using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : PortalTraveller
{
    public float yaw;
    float smoothYaw;
    Vector3 velocity;
    public float mouseSensitivity = 10;
    public float rotationSmoothTime = 0.1f;
    float yawSmoothV;


    void Start()
    {
        yaw = transform.eulerAngles.y;
        smoothYaw = yaw;
    }

    // Update is called once per frame
    void Update()
    {
        float mX = Input.GetAxisRaw("Mouse X");

        yaw += mX * mouseSensitivity;
        smoothYaw = Mathf.SmoothDampAngle(smoothYaw, yaw, ref yawSmoothV, rotationSmoothTime);
    }

    public override void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        Vector3 eulerRot = rot.eulerAngles;
        float delta = Mathf.DeltaAngle(smoothYaw, eulerRot.y);
        yaw += delta;
        smoothYaw += delta;
        transform.eulerAngles = Vector3.up * smoothYaw;
        velocity = toPortal.TransformVector(fromPortal.InverseTransformVector(velocity));
        Physics.SyncTransforms();
    }
}
