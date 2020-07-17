using UnityEngine;
using UnityEngine.UI;

public class ChangeColorBehavior : MonoBehaviour
{
    /*
        TIP:

        Press Command+SHIFT+M to see the dialog box to see which methods you can use in this script.
     */


    public Text cubeColorText; //use of variables

    //Awake is called first even if the script components are not enabled
    //It's best to use any references between scripts and initializations
    //Called only once in the life-time of the object
    //Example assign Amno to the enemy
    private void Awake()
    {
        //print("Awakee is called.");
    }

    //start is called after AWAKE immediatley before the first update
    //And only if the Script component is enabled (And start cannot be called again by enabling or disabling the script)
    //Called only once in the life-time of the object
    //Example: As the script is enabled, the enemy will be able to shoot
    private void Start()
    {
        //Debug.Log("Start is called");

        GetComponent<Renderer>().material.color = Color.white; //default color

        UpdateColorText();
    }

    /*
    // Update is called once per frame for every script that uses it
        Almost anything that needs to be changed or adjusted regularly happens here e.g. the movement of physics objects, simple timers, and the detection of input
        
    NOTE: Update is not called on a regular timeline e.g. if one frame takes longer than the other to process, then the update calls will be different
    */
    void Update()
    {
        //Debug.Log("Update is called");
        //Debug.Log($"Update time = {Time.deltaTime}"); //we can see deltaTime in Update varies

        //Behavior scripting examples
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }

        UpdateColorText();
    }

    /*
        FixedUpdate is a similar function to Update with few important differences

        NOTE:
        1. Fixed update is called on a regular time-lines and have the same time between calls
        - Immediately after FixedUpdate any physics calculations are done e.g. anything that affects the Rigid body should be executed in the FixedUpdate rather than Update
        - It's good practice to used Forces when using physics objects in the FixedUpdate
     */
    private void FixedUpdate()
    {
        //Debug.Log($"FixedUpdate time = {Time.deltaTime}"); //we can see deltaTime in FixedUpdate stays same
    }

    public void UpdateColorText()
    {
        PrintColorName(GetComponent<Renderer>().material.color);
    }


    //Use of functions
    void PrintColorName(Color color)
    {
        System.Drawing.Color theColor = System.Drawing.Color.FromArgb((int)color.a, (int)color.r, (int)color.g, (int)color.b);

        cubeColorText.text = theColor.ToString();
    }

   
}
