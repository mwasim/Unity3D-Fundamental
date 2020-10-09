using System;

public class EventBroker
{
    //The difference from other events is the inclusion of the STATIC keyword to make it accessible via class instead of instance
    public static event Action ProjectileOutOfBounds;

    public static void CallProjectileOutOfBounds()
    {
        ProjectileOutOfBounds?.Invoke();
    }
}
