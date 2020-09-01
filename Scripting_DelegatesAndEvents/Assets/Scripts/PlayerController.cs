using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //declare a delegate and an event of type of this delegate
    public delegate void OnDamageReceived(int currentHealth);
    public static event OnDamageReceived onDamage;

    //Instead of using the delegate/event syntax above, we can use the Action syntax as below (it's simple and better)
    public static Action<int> OnDamageReceivedActionSyntax;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Health = 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage();
        }
    }

    public void Damage()
    {
        Health--;

        //if this even was subscribed, invoke it
        onDamage?.Invoke(Health);

        //The syntax for invoking subscribed Action is also similar
        OnDamageReceivedActionSyntax?.Invoke(Health);

        //It can be written as below also..
        //if (onDamage != null)
        //{
        //    onDamage(Health);
    }
}
