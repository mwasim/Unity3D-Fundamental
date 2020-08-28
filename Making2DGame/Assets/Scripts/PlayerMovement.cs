using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D _controller;
    private float _horizontalMove;
    private bool _jump;
    private bool _crouch;

    [SerializeField]
    private float _movementSpeed = 40.0f;
    public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * MovementSpeed;
        //Debug.Log(horizontalInput);

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            _crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            _crouch = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move the character
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump);
        _jump = false;
    }
}
