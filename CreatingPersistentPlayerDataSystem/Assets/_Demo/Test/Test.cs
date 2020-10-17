using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public int i;
    public float f;
    public string s;
    public Vector3 v;
    public Color c;
    public LayerMask l;
    public AnimationCurve a;
    public GameObject g;
    public Rigidbody r;
    public Transform t;
    public int[] ints;
    public List<float> floats;
    public C cl;

    [System.Serializable]
    public struct C
    {
        public int i;
        public float f;
    }

    void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("json test"), this);
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("json test", JsonUtility.ToJson(this, true));
        PlayerPrefs.Save();
    }

    void OnValidate()
    {

    }
}
