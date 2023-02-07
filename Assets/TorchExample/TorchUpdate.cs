using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchUpdate : Torch
{

    private bool isTorchActive = false;

    // Update is called once per frame
    void Update()
    {
        if (isTorchActive)
        {
            fuel -= Time.deltaTime;
            if (fuel < 0)
            {
                isTorchActive = false;
                DeactivateTorch();
            }
        }
            
    }


    public override void InitializeTorch(float minfuel, float maxfuel)
    {
        fuel = Random.Range(minfuel, maxfuel);
        isTorchActive = true;
    }

    public override void PauseTorch()
    {
        isTorchActive = false;
    }

    public override void ResumeTorch()
    {
        isTorchActive = true;
    }


}
