using UnityEngine;

public class MoveFotward : MonoBehaviour
{
    public float speed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(Vector3.forward.normalized * Time.deltaTime * speed);
    }
}
