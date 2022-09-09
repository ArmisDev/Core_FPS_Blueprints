using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    [SerializeField] private float swayMult;
    [SerializeField] private float swaySmooth;

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMult;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMult;

        Quaternion rotationY = Quaternion.AngleAxis(-mouseY, Vector3.left);
        Quaternion rotationX = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, swaySmooth * Time.deltaTime);
    }
}
