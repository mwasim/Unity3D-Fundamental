using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health = 100;

    [SerializeField]
    private GameObject _enemyDeathEffect;

    [SerializeField]
    private float _movementSpeed = 5.0f;

    [SerializeField]
    private float _ceilingLimit = 7.5f;
    [SerializeField]
    private float _groundLimit = 2.5f;

    int movementMultiplier = 1;
    bool moveUp = true;
    private void FixedUpdate()
    {
        var currentY = transform.position.y;
        
        if (currentY > _ceilingLimit && moveUp)
        {
            movementMultiplier = -1;
            moveUp = false;
        }
        else if (currentY < _groundLimit && !moveUp)
        {
            movementMultiplier = 1;
            moveUp = true;
        }

        Debug.Log("Multiplier: " + movementMultiplier);

        var translation = movementMultiplier * Vector2.up * Time.fixedDeltaTime * _movementSpeed;
        Debug.Log(translation);

        transform.Translate(translation);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        var enemyDeathEffect = Instantiate(_enemyDeathEffect, transform.position, Quaternion.identity);

        Destroy(enemyDeathEffect, 1.5f);

        Destroy(gameObject);
    }
}
