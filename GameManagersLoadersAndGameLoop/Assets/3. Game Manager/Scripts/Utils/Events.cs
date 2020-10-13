using UnityEngine.Events;

public class Events //just a container class to contain events
{
    //make it accessible in the Unity's Inspector by making it Serializable
    [System.Serializable]
    public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> //incoming and current game states
    {
    }

    [System.Serializable]
    public class EventFadeComplete : UnityEvent<bool> { }
}
