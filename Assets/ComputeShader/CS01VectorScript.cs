using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS01VectorScript : MonoBehaviour
{
    public ComputeShader ComputeShader;
    public int dispatchSize;

    private Vector3[] vectors;
    private int vectorCount;

    private ComputeBuffer buffer;

    private int kernelHandle;

    // Start is called before the first frame update
    void Start()
    {
        kernelHandle = ComputeShader.FindKernel("CSMain");

        vectorCount = 8 * dispatchSize;
        vectors = new Vector3[vectorCount];

        buffer = new ComputeBuffer(vectorCount, sizeof(float) * 3);
        buffer.SetData(vectors);

        //this is expensive, communication with GPU
        ComputeShader.SetBuffer(kernelHandle, "buffer", buffer);
        ComputeShader.Dispatch(kernelHandle, dispatchSize, 1, 1);

        //script might have to wait until data is ready
        buffer.GetData(vectors);

        foreach (var v in vectors)
        {
            Debug.Log(v.x);
        }
        buffer.Release();
    }
}
    
