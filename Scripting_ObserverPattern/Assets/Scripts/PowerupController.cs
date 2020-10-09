using UnityEngine;

public class PowerupController : MonoBehaviour, IEndGameObserver
{
    #region Field Declarations

    public GameObject explosion;

    [SerializeField]
    private PowerType powerType;

    #endregion

    #region Movement

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 3, Space.World);

        if (ScreenBounds.OutOfBounds(transform.position))
        {
            RemoveAndDestroy();
        }
    }

    #endregion

    #region Collisons

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (powerType == PowerType.Shield)
        {
            var playerShip = collision.gameObject.GetComponent<PlayerController>();
            playerShip?.EnableShield();
        }

        RemoveAndDestroy();
    }

    #endregion

    public void Notify()
    {
        //destroy the power up / removes the instance of the Power Up
        RemoveAndDestroy();
    }

    //on destroying the power up, the observer should also be removed to avoid MissingReferenceException
    private void RemoveAndDestroy()
    {
        //remove observer
        var gameSceneController = FindObjectOfType<GameSceneController>();
        gameSceneController.RemoveObserver(this);

        Destroy(gameObject); //after removing observer destroy the game object
    }
}

public enum PowerType
{
    Shield,
    X2
};