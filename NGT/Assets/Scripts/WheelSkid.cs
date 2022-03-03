using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSkid : MonoBehaviour
{

    [SerializeField] float intensityModifier = 1.5f;
    [SerializeField] float intensity;

    Skidmarks skidMarkcontroller;
    PlayerCar playerCar;

    int lastSkidId = -1;


    void Start()
    {
        skidMarkcontroller = FindObjectOfType<Skidmarks>();
        playerCar = GetComponentInParent<PlayerCar>();
    }


    void LateUpdate()
    {
        intensity = playerCar.SideSlipAmount;
        if (intensity < 0)
            intensity = -intensity;

        if (intensity > 0.1f)
        {
            lastSkidId = skidMarkcontroller.AddSkidMark(transform.position, transform.up,
                                              intensity * intensityModifier, lastSkidId);
        }
        else
        {
            lastSkidId = -1;
        }
    }
}
