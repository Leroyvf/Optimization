using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS02TextureScript : MonoBehaviour
{

    public ComputeShader computeShader;

    public Vector2Int Resolution;

    private RenderTexture texture;

    private int[] res = new int[2];

    private int kernelHandle;
    private float[] numThreads;


    // Start is called before the first frame update
    void Start()
    {
        texture = new RenderTexture(Resolution.x, Resolution.y, 24);
        texture.enableRandomWrite = true;
        texture.Create();

        kernelHandle = computeShader.FindKernel("CSMain");

        res[0] = Resolution.x;
        res[1] = Resolution.y;

        computeShader.GetKernelThreadGroupSizes(kernelHandle, out uint x, out uint y, out uint z);
        numThreads = new float[] {(float)x, (float)y, (float)z};

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        computeShader.SetTexture(kernelHandle, "Result", texture);
        computeShader.SetInts("Resolution", res);

        computeShader.Dispatch(kernelHandle, Mathf.CeilToInt((Resolution.x / numThreads[0])), Mathf.CeilToInt((Resolution.y / numThreads[1])), 1);
        Graphics.Blit(texture, destination);
    }
}
