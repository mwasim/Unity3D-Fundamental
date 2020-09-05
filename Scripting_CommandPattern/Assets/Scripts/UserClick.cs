using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //left click
        //cast a ray
        //detect a cube
        //assign a random color to the cube

        if (Input.GetMouseButtonDown(0))
        {
            var rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayOrigin, out RaycastHit hitInfo))
            {
                var collider = hitInfo.collider;
                if (collider.CompareTag("Cube"))
                {
                    var randomColor = new Color(Random.value, Random.value, Random.value);
                    ICommand command = new ClickCommand(collider.gameObject, randomColor);

                    //Instead of below commented code, we're executing command now
                    command.Execute();

                    //Add to CommandManager's command buffer
                    CommandManager.Instance.AddCommand(command);

                    //collider.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
                }
            }
        }
    }
}
