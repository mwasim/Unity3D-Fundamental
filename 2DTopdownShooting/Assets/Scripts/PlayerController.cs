using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 5.0f;

    [SerializeField]
    private Camera _camera;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Vector2 _mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    ////NOTE: We use the UPDATE function to get the input
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        //Convert from pixel coordinate to world units
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    ////NOTE: We use the FixedUpdate function to actually move our player based on the input got in the Update
    private void FixedUpdate()
    {
        //Debug.Log(_movement);

        _rb.MovePosition(_rb.position + _movement * _movementSpeed * Time.fixedDeltaTime);

        //As we've the mouse position in _mousePosition, we can rotate the player to face in that direction
        //step-1: get direction
        var lookDirection = _mousePosition - _rb.position;

        //step-2: We don't just need direction, we also need an angle, we use the funcation atan2
        //We need to multiply the Atan2 with Mathf.Rad2Deg to convert to degrees, and -90 is the correct to point the player in the right direction
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f; //returns the angle between the X-Axis and the 2D vector starting at ZERO and ending at (x, y)

        _rb.rotation = angle;

    }
}
