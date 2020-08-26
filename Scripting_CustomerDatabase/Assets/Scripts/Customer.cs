/*
    A class object appears in the Unity's inspector only if it's serializable
 */
[System.Serializable]
public class Customer
{
    public string firstName;
    public string lastName;
    public int age;
    public Gender gender;
    public string occupation;

    public Customer(string fName, string lName, int customerAge, Gender customerGender, string customerOccupation)
    {
        firstName = fName;
        lastName = lName;
        age = customerAge;
        gender = customerGender;
        occupation = customerOccupation;
    }
}

public enum Gender
{
    Male = 1,
    Female = 2
}
