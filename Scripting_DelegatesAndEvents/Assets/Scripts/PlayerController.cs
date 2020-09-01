using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //declare a delegate and an event of type of this delegate
    public delegate void OnDamageReceived(int currentHealth);
    public static event OnDamageReceived onDamage;

    //delegate syntax onComplete
    public delegate string OnComplete();
    public static event OnComplete OnActionCompleted;

    //Instead of using the delegate/event syntax above, we can use the Action delegate syntax as below (it's simple and better)
    public static Action<int> OnDamageReceivedActionSyntax;

    //Func delegate syntax to return values
    public static Func<string> OnFuncActionCompleted;

    //Func delegate which takes string param and returns int
    public static Func<string, int> OnGetStringLength;

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

            Debug.Log(Completed());
            Debug.Log(FuncCompleted());

            Debug.Log(GetStringLength("This is a string"));
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

    public string Completed()
    {
        return OnActionCompleted?.Invoke();
    }

    public string FuncCompleted()
    {
        return OnFuncActionCompleted?.Invoke();
    }

    public static int? GetStringLength(string str)
    {
        return OnGetStringLength?.Invoke(str);
    }
}
