using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{

    Vector2 targetPosition;
    int numStop;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
    }

    // Update is called once per frame
    void Update()
    {

        if (numStop < 3)
        {

            float distance = Vector2.Distance(transform.position, targetPosition);


            if (distance > 0.001f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * 4);
            }
            else
            {
                numStop += 1;
                targetPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

            }
        }
        else if (transform.position.y > Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5);
        }
        else
        {
            Destroy(gameObject);

        }
    }
}
