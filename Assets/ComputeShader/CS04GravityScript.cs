using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS04GravityScript : MonoBehaviour
{

    private struct Ball
    {
        public uint ID;
        public Vector3 Position;
        public Vector3 Velocity;
    }

    public ComputeShader computeShader;
    [Range(1, 10000)] public int ballCount;
    public float[] Gravity = new float[3];

    private Ball[] balls;
    private ComputeBuffer ballBuffer;
    private int kernelHandle;



    // Start is called before the first frame update
    void Start()
    {
        balls = new Ball[ballCount];

        CreateBalls();

        kernelHandle = computeShader.FindKernel("CSMain");

        ballBuffer = new ComputeBuffer(ballCount, sizeof(uint) + sizeof(float) * 6);
        ballBuffer.SetData(balls);
        computeShader.SetBuffer(kernelHandle, "ballBuffer", ballBuffer);
    }


    // Update is called once per frame
    void Update()
    {
        computeShader.SetFloats("Gravity", Gravity);
        computeShader.SetFloat("deltaTime", Time.deltaTime);

        computeShader.Dispatch(kernelHandle, Mathf.CeilToInt(ballCount / 32f), 1, 1);

        ballBuffer.GetData(balls);

        for (int i = 0; i < ballCount; i++)
        {
            transform.GetChild(i).position = balls[i].Position;
        }
    }

    private void OnDestroy()
    {
        ballBuffer.Release();
    }
    private void CreateBalls()
    {
        for (int i = 0; i < ballCount; i++)
        {
            GameObject b = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            b.transform.parent = transform;
            b.transform.position = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(0f, 100f), UnityEngine.Random.Range(-50f, 50f));
            Destroy(b.GetComponent<SphereCollider>());

            balls[i].Position = b.transform.position;
            balls[i].Velocity = new Vector3(0, UnityEngine.Random.Range(0f, 20f), 0);
            balls[i].ID = (uint)i;
        
        
        }
    }
}
