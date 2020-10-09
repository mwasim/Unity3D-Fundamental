using System.Collections;
using UnityEngine;

public delegate void EnemyDestroyedHandler(int pointValue);

public class EnemyController : MonoBehaviour
{
    #region Field Declarations

    [Header("Prefabs")]
    public GameObject explosion;
    public ProjectileController projectilePrefab;

    // Set by GameSceneController
    [HideInInspector] public float shotSpeed;
    [HideInInspector] public float shotdelayTime;
    [HideInInspector] public float angerdelayTime;
    [HideInInspector] public float speed;
    [HideInInspector] public int pointValue = 10;

    private WaitForSeconds shotDelay;
    private WaitForSeconds angerDelay;
    private float shotSpeedxN;

    private Vector2 currentTarget;
    private SpriteRenderer spriteRenderer;

    #endregion

    public event EnemyDestroyedHandler OnEnemyDestroyed;

    #region Startup

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentTarget = ScreenBounds.GetRandomPosition();
        shotDelay = new WaitForSeconds(shotdelayTime);
        angerDelay = new WaitForSeconds(angerdelayTime);
        shotSpeedxN = shotSpeed * 2.5f;

        StartCoroutine(AngerCountDown());
        StartCoroutine(OpenFire());
    }

    #endregion

    #region Movement

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, currentTarget) > 0.001f)
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, Time.deltaTime * speed);
        else
            currentTarget = ScreenBounds.GetRandomPosition();
    }

    #endregion

    #region Collisons

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("EnemyBullet")) return; //when enemy fires a projectile, this shouldn't destroy the enemy itself

        Destroy(collision.gameObject); //destroy projectile (collision game object)

        //The below code is commented because, the points are updated via the GameSceneController and HUDController using events
        //FindObjectOfType<PlayerController>().EnableProjectile();    
        //FindObjectOfType<HUDController>().UpdateScore(pointValue);

        OnEnemyDestroyed?.Invoke(pointValue); //if event subscribed raise it

        GameObject xPlosion = Instantiate(explosion, transform.position, Quaternion.identity);
        xPlosion.transform.localScale = new Vector2(2, 2);

        Destroy(gameObject); //destroy the enemy itself
    }

    #endregion

    #region Projectile control

    private void FireProjectile()
    {
        Vector2 spawnPosition = transform.position;

        ProjectileController projectile =
            Instantiate(projectilePrefab, spawnPosition, Quaternion.AngleAxis(90, Vector3.forward));

        projectile.gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
        projectile.projectileSpeed = shotSpeed;
        projectile.projectileDirection = Vector2.down;
    }

    IEnumerator OpenFire()
    {
        while (true)
        {
            FireProjectile();
            yield return shotDelay;
        }
    }

    #endregion

    #region Anger management

    IEnumerator AngerCountDown()
    {
        yield return angerDelay;
        GetAngry();
    }

    private void GetAngry()
    {
        spriteRenderer.color = Color.red;
        currentTarget = ScreenBounds.GetRandomPosition();
        shotDelay = new WaitForSeconds(shotdelayTime / 3);
        shotSpeed = shotSpeedxN;
    }

    #endregion
}
