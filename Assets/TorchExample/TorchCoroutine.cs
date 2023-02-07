using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCoroutine : Torch
{
    private float startTime;
    private IEnumerator BurnTorchCo;
    public override void InitializeTorch(float minfuel, float maxfuel)
    {
        fuel = Random.Range(minfuel, maxfuel);
        startTime = Time.time;
        BurnTorchCo = BurnTorch();
        StartCoroutine(BurnTorchCo);
    }

    public override void PauseTorch()
    {
        StopCoroutine(BurnTorchCo);
        float diff = Time.time - startTime;
        fuel -= diff;
    }

    public override void ResumeTorch()
    {
        if (fuel <= 0)
            return;
        startTime = Time.time;
        StartCoroutine(BurnTorchCo);
        
    }

    private IEnumerator BurnTorch()
    {
        yield return new WaitForSeconds(fuel);

        DeactivateTorch();

    }
}
