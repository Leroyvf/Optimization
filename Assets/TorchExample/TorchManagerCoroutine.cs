using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManagerCoroutine : Torch
{
    public override void InitializeTorch(float minfuel, float maxfuel)
    {
        fuel = Random.Range(minfuel, maxfuel);
    }

    public override void PauseTorch()
    {
        
    }

    public override void ResumeTorch()
    {
        
    }
}
