using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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

            //Calculate Dot Product
            CalculateAngle();
        }
    }

    private void CalculateAngle()
    {
        //Facing position of a character is always Z direction (which is Vector3.up)
        var tF = transform.up; //facing position of the tank
        var fD = fuel.transform.position - transform.position; // target position - current position = direction

        //Draw rays for debugging
        //Debug.DrawRay(transform.position, tF, Color.green, 2); //green line is not visible
        Debug.DrawRay(transform.position, tF * 10, Color.green, 2); //green line becomes visible by multiplying tF with 10
        Debug.DrawRay(transform.position, fD, Color.red, 2);

        /*
            We know (normally used for camera of weapons)
            < 90 degrees => v.w > 0 (w object is infront of v object)
            = 90 degrees => v.w = 0 (w object is facing the v object)
            > 90 degrees => v.w < 0 (w object is behind the v object)

            Angle calculation
            Anlge(Theta) = cos-1(v.w/|v||w|) //cost inverse of (dot product of vectors V and W divided by product of lengths of V and W)
         */
        var dot = tF.x * fD.x + tF.y * fD.y; // Dot Product
        var angleInRadians = Mathf.Acos(dot / (tF.magnitude * fD.magnitude));
        var angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        //Debug.Log($"Angle: {angleInRadians}"); //Angle in radians
        Debug.Log($"Angle: {angleInDegrees}"); //Angle in degrees

        var unityAngle = Vector3.Angle(tF, fD);
        Debug.Log($"Angle: {unityAngle}"); //Angle in degrees
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