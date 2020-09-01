using UnityEngine;

public class UIManager : MonoBehaviour
{
    //When it's activated or enabled
    private void OnEnable()
    {
        //subscribe to the event
        PlayerController.onDamage += UpdatePlayerHealth;

        //subcribe to the Action
        PlayerController.OnDamageReceivedActionSyntax += UpdatePlayerHealth;
    }

    //When it's deactivated or diabled, ensure to unsubscribe the event
    private void OnDisable()
    {
        //PlayerController.onDamage -= UpdatePlayerHealth;

        PlayerController.OnDamageReceivedActionSyntax -= UpdatePlayerHealth;
    }    

    private void UpdatePlayerHealth(int currentHealth)
    {
        Debug.Log("Current Health: " + currentHealth);
    }
}
