using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ExampleController : MonoBehaviour
{
    Camera camera;
    Transform myTransForm;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        myTransForm = transform;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10000; i++)
        {
            TransformExample();
            // CameraExample();
        }
    }

    void TransformExample()
    {
        Profiler.BeginSample("TRANSFORM EXAMPLE");

        myTransForm.position = myTransForm.position;

        Profiler.EndSample();
    }

    void CameraExample()
    {
       Profiler.BeginSample("CAMERA.MAIN EXAMPLE");

       camera.backgroundColor = Color.black;

       Profiler.EndSample();
    }

}
