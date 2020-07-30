using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// A very simplistic car driving on the x-z plane. 

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public GameObject fuel;

    void Start()
    {

    }

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);


        //Calculate the distance and print in the debug log
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
        }
    }

    private void CalculateDistance()
    {
        //Calculate using Phythgorean theorem
        //Sqrt(Square(x1 - x2) + Square(y1 - y2)
        Vector3 tP = transform.position; //tank position
        Vector3 fP = fuel.transform.position; //fuel position

        //You can put tp or fP first, it doesn't matter, the order should be same
        //float distance = Mathf.Sqrt(Mathf.Pow(tP.x - fP.x, 2) + Mathf.Pow(tP.y - fP.y, 2));

        //As Unity uses 3 dimensions so, with above code, there can be just a slight difference, to fix that, let's add third dimension also (Z)
        float distance = Mathf.Sqrt(Mathf.Pow(tP.x - fP.x, 2) + Mathf.Pow(tP.y - fP.y, 2) + Mathf.Pow(tP.z - fP.z, 2));

        //Now let's use the Unity's distance calculation method
        float unityDistance = Vector3.Distance(tP, fP);

        Debug.Log($"Distance: ${distance}");
        Debug.Log($"Unity Distance: ${unityDistance}");
    }
}