using UnityEngine;

public class ActivateGameObjects : MonoBehaviour
{
    private bool isActive = true; //private variable can be seen in the inspector only when Debug Mode is enabled

    [Tooltip("To specify range")]
    [Header("Range")] //adds a header in the inspector
    [Range(1, 10)] //Use Range attribute, it allows us to select a value using a slider in the Unity inspector
    public int numberInRange = 2;
    public int number2InRange = 2;
    public int number3InRange = 2;
    
    [Space()] //adds spaces between variables in the inspector
    public int number = 2;
    [HideInInspector] //this variable will be hidden in the inspector
    public int number2 = 2;
    [Space()]
    public int number3 = 2;

    //we make the variables public only if it should be accessible from any other class
    //Otherwise, we can simply mark private variable as SerializeField to see in the inpsector
    [SerializeField]
    private int startValue = 1;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update called");

        if (Input.GetKeyDown(KeyCode.P))
        { 
            //Debug.Log(isActiveToggle);
            //Debug.Log(gameObject.activeInHierarchy);


            gameObject.SetActive(false);
        }   
    }
}
