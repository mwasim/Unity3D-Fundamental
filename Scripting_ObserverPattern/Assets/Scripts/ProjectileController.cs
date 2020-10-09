using UnityEngine;

public delegate void OutOfBoundsHandler();

public class ProjectileController : MonoBehaviour
{
    #region Field Declarations

    public Vector2 projectileDirection;
    public float projectileSpeed;
    public bool isPlayers;    

    #endregion    

    #region Movement

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.Translate(projectileDirection * Time.deltaTime * projectileSpeed, Space.World);

        if (ScreenBounds.OutOfBounds(transform.position))
        {
            //ProjectileOutBounds?.Invoke(); //if event is subscribed, invoke it (avoid coupling between objects)

            //now we're using the Publisher/subscriber pattenr (while using the EventBroker), let's use that
            EventBroker.CallProjectileOutOfBounds();

            //Instead of using the below code, now we're using events to communicate between objects
            //if (isPlayers)
            //    FindObjectOfType<PlayerController>().EnableProjectile();

            Destroy(gameObject);            
        }
    }

    #endregion
}
