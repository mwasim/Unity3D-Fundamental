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

        //subscribe to Func which returns string and takes no params
        PlayerController.OnFuncActionCompleted += OnTaskCompleted;

        //subscribe to Fun which takes string param and returns int
        PlayerController.OnGetStringLength += GetStringLength;
    }

    //When it's deactivated or diabled, ensure to unsubscribe the event
    private void OnDisable()
    {
        PlayerController.onDamage -= UpdatePlayerHealth;

        PlayerController.OnDamageReceivedActionSyntax -= UpdatePlayerHealth;

        PlayerController.OnFuncActionCompleted -= OnTaskCompleted;

        PlayerController.OnGetStringLength -= GetStringLength;
    }

    private void UpdatePlayerHealth(int currentHealth)
    {
        Debug.Log("Current Health: " + currentHealth);
    }


    public string OnTaskCompleted()
    {
        return "Task is completed now";
    }

    public int GetStringLength(string str)
    {
        return str.Length;
    }
}
