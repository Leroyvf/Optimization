using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS05InsectScript : MonoBehaviour
{
    public struct Insect
    {
        public Vector3 position;
        public Vector3 forward;
        public float ID;
    }

    public ComputeShader computeShader;
    public GameObject InsectPrefab;
    public Transform InsectHome;
    public int numberOfInsects;

    [Header("")]
    public float DeltaMod = 1f;
    public float weight = 1f;
    public float RepelWeight = 1f;
    public float AlignWeight = 1f;
    public float CohesionWeight = 1f;
    public float CenterWeight = 1f;
    public float MaxSpeed = 1f;
    public float RepelDist = 1f;
    public float AlignDist = 4f;
    public float CohesionDist = 1f;
    public float MaxForce = 1f;

    private ComputeBuffer insectBuffer;
    private uint[] ConsumeIDs;
    private uint[] OutIDs;

    private List<Transform> AllInsects = new List<Transform> ();
    private int insectCount;
    private int kernelHandle;


    // Start is called before the first frame update
    void Start()
    {
        CreateInsect();

        insectBuffer = new ComputeBuffer(insectCount, sizeof(float) * 7, ComputeBufferType.Append);
  
        OutIDs = new uint[insectCount];
        ConsumeIDs = new uint[insectCount];

        for (uint i = 0; i < insectCount; i++)
        {
            ConsumeIDs[i] = i; //filling the index with all indeces
        }

        Insect[] tempArray = new Insect[insectCount];
        for (int i = 0; i < insectCount; i++)
        {
            tempArray[i].position = AllInsects[i].position;
            tempArray[i].forward = AllInsects[i].forward;
            tempArray[i].ID = i;
        }

        insectBuffer.SetData(tempArray);
        insectBuffer.SetCounterValue((uint)insectCount);

        float InWeight = 1f / weight;
        computeShader.SetInt("numBoids", insectCount);
        computeShader.SetFloats("Parameters1", new float[4]
        {
            RepelWeight, AlignWeight, CohesionWeight, MaxSpeed
        });
        computeShader.SetFloats("Parameters2", new float[4]
        {
            RepelDist, AlignDist, CohesionDist, MaxForce
        });
        computeShader.SetFloats("Parameters3", new float[4]
        {
            Time.deltaTime * DeltaMod, InWeight, 0, CenterWeight
        });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateInsect()
    {
        insectCount = numberOfInsects;
        Transform insect = null;

        for (int i = 0; i < insectCount; i++)
        {
            insect = Instantiate(InsectPrefab, Vector3.zero, Quaternion.identity).transform;

            Vector2 ran = UnityEngine.Random.insideUnitCircle;
            insect.position = InsectHome.position + new Vector3(ran.x, ran.y, 0);
            insect.localRotation = Quaternion.LookRotation(UnityEngine.Random.insideUnitSphere, Vector3.up);
        
            AllInsects.Add(insect);
        }
    }
}
