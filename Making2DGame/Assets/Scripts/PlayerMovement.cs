using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D _controller;
    private Animator _animator;
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
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * MovementSpeed;
        //Debug.Log(horizontalInput);

        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
            _animator.SetBool("IsJumping", true);
            Debug.Log("JUMP ANIMATION SET.");
        }

        if (Input.GetButtonDown("Crouch"))
        {
            _crouch = true;
            _animator.SetBool("IsCrouching", _crouch);
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

    public void OnLanding()
    {
        _animator.SetBool("IsJumping", false);
        Debug.Log("JUMP ANIMATION REMOVED.");
    }

    public void OnCrouching(bool isCrouching)
    {
        _animator.SetBool("IsCrouching", isCrouching);
    }
}
