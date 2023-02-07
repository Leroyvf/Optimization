using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum TorchMode
{
    Update,                 //each light has its own update
    Coroutine,              // ""           ""       Coroutine
    ManagerCoroutine
}
public class TorchManager : MonoBehaviour
{
    public GameObject TorchPrefab;
    public int TorchCount;
    public TorchMode TorchMode;
    public Vector2 fuelRangeValues;

    private Torch[] Torches;

    // Start is called before the first frame update
    void Start()
    {
        Torches = new Torch[TorchCount];

        GameObject torch = null;
        Torch t = null;

        for (int i = 0; i < TorchCount; i++)
        {
            torch = Instantiate(TorchPrefab, transform);
            torch.transform.position = new Vector3(Random.Range(-5,5),1, Random.Range(-5,5));

            if (TorchMode == TorchMode.Update)
            {
                t = torch.AddComponent<TorchUpdate>();
            }
            else if (TorchMode == TorchMode.Coroutine)
            {
                t = torch.AddComponent<TorchCoroutine>();
            }
            else if (TorchMode == TorchMode.ManagerCoroutine)
            {
                t = torch.AddComponent<TorchManagerCoroutine>();
            }

                
            t.InitializeTorch(fuelRangeValues.x, fuelRangeValues.y);

            Torches[i] = t;
        }

        // Sorts the array by fuel lowest to highest
        if (TorchMode == TorchMode.ManagerCoroutine)
        {
            Torches = Torches.OrderBy(a => a.fuel).ToArray();

            StartCoroutine(BurnTorches());
        }
        //

    }
    private IEnumerator BurnTorches()
    {
        float waitTime = Torches[0].fuel;

        for (int i = 0; i < TorchCount; i++)
        {
            yield return new WaitForSeconds(waitTime);
            float timeStep = Time.deltaTime;

            do
            {
                if (i < TorchCount - 1)
                {
                    waitTime = Torches[i + 1].fuel - Torches[i].fuel;
                    timeStep -= waitTime;

                }
                Torches[i].DeactivateTorch();

            } while (timeStep > 0 && (i++ < TorchCount - 1));


            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
