using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text textNumberOfPickUpsCollected;
    public Text textWin;

    private Rigidbody2D rb2D;
    private int numberOfPickUpsCollected;
    private const int numberOfPickUpsToWin = 12;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        numberOfPickUpsCollected = 0;
        SetCountText();
    }

    private void FixedUpdate()
    {
        //Input //Press CMD+' to get more info after selecting the keyword e.g. Input

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb2D.AddForce(movement * speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision.gameObject.tag);
        if (collision.CompareTag("PickUp"))
        {
            collision.gameObject.SetActive(false);

            numberOfPickUpsCollected += 1;

            SetCountText();
        }
    }

    private void SetCountText()
    {
        textNumberOfPickUpsCollected.text = $"Count: {numberOfPickUpsCollected}";

        if (numberOfPickUpsCollected >= numberOfPickUpsToWin)
        {
            textWin.text = "You Win!";
            return;
        }

        textWin.text = string.Empty;
    }
}
