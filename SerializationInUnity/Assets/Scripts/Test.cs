using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [System.Serializable] //we use Serializable attribute to serialize and be shown in the Unity's inspector
    public class TestClass {
        public int i;
        public float f;
    }

    [System.Serializable] //we use Serializable attribute to serialize and be shown in the Unity's inspector
    public struct TestStruct
    {
        public int i;
        public float f;
    }

    //Beow all these fields are serialized by the unity editor and shown in the unity's inspector
    public int intVariable;
    public float floatVariable;
    public string stringVariable;
    public Vector3 v;
    public Color c;
    public LayerMask l;
    public AnimationCurve a;
    public GameObject g;
    public Rigidbody r;
    public Transform t;
    public int[] ints;
    public List<float> floats;
    public TestClass cl;
    public TestStruct st;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
