using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Torch : MonoBehaviour
{
    public float fuel;
    public Light Light;

    private void Awake()
    {
        Light = GetComponent<Light>();
    }

    public abstract void InitializeTorch(float minfuel, float maxfuel);

    public abstract void PauseTorch();
    public abstract void ResumeTorch();

    public void DeactivateTorch()
    {
        fuel = 0;
        Light.intensity = 0;
        gameObject.SetActive(false);
    }

}
