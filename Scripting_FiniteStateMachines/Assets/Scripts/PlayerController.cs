using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Player Variables

    public float jumpForce;
    public Transform head;
    public Transform weapon01;
    public Transform weapon02;

    public Sprite idleSprite;
    public Sprite duckingSprite;
    public Sprite jumpingSprite;
    public Sprite spinningSprite;

    private SpriteRenderer face;
    private Rigidbody rbody;

    #endregion

    private bool _isJumping;
    private bool _isDucking;
    private bool _isSpinning;

    private float _rotation;

    private void Awake()
    {
        face = GetComponentInChildren<SpriteRenderer>();
        rbody = GetComponent<Rigidbody>();
        SetExpression(idleSprite);
    }

    // Update is called once per frame
    void Update()
    {
        /*
            NON-FINITE-STATE MACHINE APPROACH
         */
        if (Input.GetButtonDown("Jump"))
        {
            if (!_isJumping && !_isDucking) //jump if not already jumping or ducking
            {
                _isJumping = true;

                SetExpression(jumpingSprite);

                rbody.AddForce(Vector3.up * jumpForce);
            }
            else
            {
                _isSpinning = true;
                SetExpression(spinningSprite);
            }
        }
        else if (Input.GetButtonDown("Duck") && !_isJumping) //duck if not already jumping
        {
            _isDucking = true;

            head.localPosition = new Vector3(head.localPosition.x, .5f, head.localPosition.z);
            SetExpression(duckingSprite);
        }
        else if (Input.GetButtonUp("Duck") && !_isJumping) //on button release, reset duck
        {
            _isDucking = false;
            head.localPosition = new Vector3(head.localPosition.x, 0.8f, head.localPosition.z);
            SetExpression(idleSprite);
        }
        else if (Input.GetButtonDown("SwapWeapon"))
        {
            if (!_isJumping && !_isDucking && !_isSpinning) //jump if not already jumping or ducking
            {
                var usingWeapon01 = weapon01.gameObject.activeInHierarchy;

                weapon01.gameObject.SetActive(usingWeapon01 == false);
                weapon02.gameObject.SetActive(usingWeapon01);
            }
        }

        if (_isSpinning)
        {
            Spin();
        }
    }

    private void Spin()
    {
        var amountToRotate = 900 * Time.deltaTime;
        _rotation += amountToRotate;

        if (_rotation < 360)
        {
            transform.Rotate(Vector3.up, _rotation);
        }
        else
        {
            transform.rotation = Quaternion.identity;

            //reset spinning & rotation
            _isSpinning = false;
            _rotation = 0;
            SetExpression(jumpingSprite);
        }
    }

    //As the player collides the ground
    private void OnCollisionEnter(Collision collision)
    {
        _isJumping = false;
        SetExpression(idleSprite); //reset to idle sprite
    }

    public void SetExpression(Sprite newExpression)
    {
        face.sprite = newExpression;
    }
}
