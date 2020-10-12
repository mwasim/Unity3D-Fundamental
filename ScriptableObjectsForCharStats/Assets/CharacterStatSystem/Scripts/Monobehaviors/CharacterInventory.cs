using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    //VARIABLE DECLERATIONS
    public static CharacterInventory Instance;

    private void Awake()
    {
        Instance = this;
    }
}
