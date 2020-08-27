public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;

    static GWorld()
    {
        world = new WorldStates();
    }

    public GWorld()
    {

    }

    public static GWorld Instance => instance;
    public WorldStates World => world;
}
