using System.Collections.Generic;
using UnityEngine;

public class CustomerDatabase : MonoBehaviour
{
    public List<Customer> customers;

    // Start is called before the first frame update
    void Start()
    {
        customers = new List<Customer> {
            new Customer("John", "Corner", 25, Gender.Male, "Designer"),
            new Customer("James", "Rob", 25, Gender.Male, "Programmer"),
            new Customer("Jenny", "Cooper", 25, Gender.Female, "QA Tester")
        };


        foreach (var customer in customers)
        {
            Debug.Log($"Customer:\tFirst Name: {customer.firstName}, Last Name: {customer.lastName}, Age: {customer.age}, Gender: {customer.gender}, Occupation: {customer.occupation}\n");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
