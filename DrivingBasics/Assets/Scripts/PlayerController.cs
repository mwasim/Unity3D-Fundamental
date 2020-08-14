using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] float horsePower;
    [SerializeField] float turnSpeed = 5;


    [SerializeField] GameObject centerOfMass;

    [SerializeField] TextMeshProUGUI textSpeedoMeter;
    [SerializeField] TextMeshProUGUI textGear;

    [SerializeField] List<WheelCollider> carWheels;
    private int wheelsOnGround;

    private float horizontalInput;
    private float forwardInput;
    private const int GearInterval = 5;

    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (CheckWheelsOnGround())
        {
            //Move the vehicle forward
            //transform.Translate(0, 0, 0.1f);
            //above line can be written as, Vector3.forward = (0, 0, 1), Z = 1
            //transform.Translate(Vector3.forward * Time.deltaTime * topSpeed * forwardInput);

            /*
             * Instead of transform.Translate, the better way is to use rb.AddForce
             * We don't need to use Time.deltaTime in the AddForce
             * And instead of speed, we should be using the horsePower
            */
            playerRb.AddRelativeForce(Vector3.forward * horsePower * forwardInput);

            //This code moves the vehicle left or right
            //Vector3.up = (1, 0, 0), X = 1
            //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

            //We need to rotate the vehicle left or right
            //Vector3.up = (0, 1, 0), Y = 1
            transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
        }       

        UpdateSpeed(playerRb.velocity.magnitude);
    }

    private void UpdateSpeed(float speed)
    {
        var kilometersPerHour = (int)speed * 3.6;
        textSpeedoMeter.text = $"Speed: {kilometersPerHour} km/h";

        if (kilometersPerHour % GearInterval == 0)
        {
            var gear = kilometersPerHour / GearInterval + 1;

            textGear.text = $"Gear: {gear}";
        }
    }

    private bool CheckWheelsOnGround()
    {
        wheelsOnGround = 0;
        foreach (var item in carWheels)
        {
            if (item.isGrounded)
                wheelsOnGround += 1;
        }

        return wheelsOnGround == carWheels.Count; //all wheels on the ground?
    }
}
