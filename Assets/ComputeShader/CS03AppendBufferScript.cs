using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS03AppendBufferScript : MonoBehaviour
{

    struct Ball
    {
        public Vector3 pos;
        public uint ID;
    }

    public ComputeShader computeShader;
    public int dispatchSize;

    private ComputeBuffer ballBuffer;
    private Ball[] balls;
    private int kernelHandle;

    // Start is called before the first frame update
    void Start()
    {
        kernelHandle = computeShader.FindKernel("CSMain");

        ballBuffer = new ComputeBuffer(8 * dispatchSize, sizeof(float) * 3 + sizeof(uint), ComputeBufferType.Append);
        ballBuffer.SetCounterValue(0);

        computeShader.SetBuffer(kernelHandle, "ballBuffer", ballBuffer);
        computeShader.Dispatch(kernelHandle, dispatchSize, 1, 1);

        uint[] countBufferData = new uint[1];
        ComputeBuffer countBuffer = new ComputeBuffer(1, sizeof(uint), ComputeBufferType.IndirectArguments);
        ComputeBuffer.CopyCount(ballBuffer, countBuffer, 0);
        countBuffer.GetData(countBufferData);

        balls = new Ball[countBufferData[0]];

        ballBuffer.GetData(balls);

        foreach (var b in balls)
        {
            Debug.Log(b.ID);
        }

        ballBuffer.Release();
        countBuffer.Release();

    }


}
