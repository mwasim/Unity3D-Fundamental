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

    private const string PlayerPrefsKey = "JsonTest";

    private void OnEnable()
    {
        //load player prefs
        JsonUtility.FromJsonOverwrite(PlayerPrefsKey, this);

        Debug.Log("Player prefs are loaded.");
    }

    private void OnDisable()
    {
        //save player prefs

        //We can save this object also to the PlayerPrefs
        PlayerPrefs.SetString(PlayerPrefsKey, JsonUtility.ToJson(this, true));
        PlayerPrefs.Save();

        Debug.Log("Player prefs are saved.");
    }

    //This method runs when a change is made to the serialized information in the editor
    private void OnValidate()
    {
        //PlayerPrefs - Store and access Player preferences between game sessions
        //On Windows this info is saved in the Registery, and on Mac, this info is saved in the Library preferences folder
        //On Mac, this info got saved on Library/Preferences/unity.GameDev.Game saves and serialization.plist (e.g. TestInt = 34)
        //PlayerPrefs.SetInt("TestInt", 34);
        //PlayerPrefs.Save();

        

        Debug.Log("OnValidate method called..");
    }    
}
