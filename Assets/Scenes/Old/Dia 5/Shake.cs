﻿using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour
{
    public bool Shaking;
    private float ShakeDecay;
    private float ShakeIntensity;

    private Vector3 OriginalPos;
    private Quaternion OriginalRot;

    void Start()
    {
        Shaking = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (ShakeIntensity > 0)
        {
            transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
            transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                            OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                            OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                            OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f);

            ShakeIntensity -= ShakeDecay * Time.fixedDeltaTime;

            if(ShakeIntensity <= 0)
            {
                transform.rotation = OriginalRot;
                transform.position = OriginalPos;
            }

        }
        else if (Shaking)
        {
            Shaking = false;
        }
    }

    public void DoShake()
    {
        if (!Shaking)
        {
            OriginalPos = transform.position;
            OriginalRot = transform.rotation;
        }

        ShakeIntensity = 0.3f;
        ShakeDecay = 2f;
        Shaking = true;
    }
}