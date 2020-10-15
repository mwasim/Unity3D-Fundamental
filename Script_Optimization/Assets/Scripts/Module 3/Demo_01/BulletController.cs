using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 3; 

    private Transform myTransform;
    private float maxVerticalPosition;
    private Vector2 endPosition;

    private string[] makeMeHeavy = new string[750000];
    
    // Start is called before the first frame update
    private void Start()
    {
        myTransform = transform;

        maxVerticalPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, 1)).y;
        endPosition = new Vector2(myTransform.position.x, maxVerticalPosition + 1);

        StartCoroutine(Move(speed));
    }

    private void Update()
    {
        if (myTransform.position.y > maxVerticalPosition)
        {
            Destroy(gameObject);
        }
    }
 
    IEnumerator Move(float duration)
    {
        float elapsedTime = 0;
        Vector2 startingPos = myTransform.position;

        while (elapsedTime < duration)
        {
            myTransform.position = Vector3.Lerp(startingPos, endPosition, (elapsedTime / duration));
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
    }     
}
