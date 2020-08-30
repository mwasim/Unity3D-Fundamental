using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20.0f;

    [Header("Enemy Settings")]
    [SerializeField]
    private int _damageLevel = 10;

    [SerializeField]
    private GameObject _shotImpactEffect;

    //private 
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = transform.right * _speed; //instruct rigid body to move right with at _speed

    }

    //As the bullet hits an enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(_damageLevel);
        }

        var shotImpact = Instantiate(_shotImpactEffect, transform.position, transform.rotation);

        Destroy(shotImpact, 0.2f);

        Destroy(gameObject); //destroy the bullet
    }    
}
