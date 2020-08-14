using UnityEngine;

public class WorkerController : MonoBehaviour
{
    public Material happyMaterial;
    public Material sadMaterial;

    private float sadSpeed = 2.5f;
    private float happySpeed = 6.0f;
    public bool isHappy = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = sadMaterial;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move the object
        var theSpeed = isHappy ? happySpeed : sadSpeed;

        transform.Translate(Vector3.right * theSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            MakeHappy();
            Destroy(other.gameObject);
        }
    }

    private void MakeHappy()
    {
        isHappy = true;

        GetComponent<MeshRenderer>().material = happyMaterial;
    }
}
