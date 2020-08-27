using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static Queue<GameObject> patients;

    static GWorld()
    {
        world = new WorldStates();

        patients = new Queue<GameObject>();
    }

    public GWorld()
    {

    }

    public void AddPatient(GameObject p)
    {
        patients.Enqueue(p);
    }

    public GameObject RemovePatient()
    {
        if (patients.Count == 0) return null;

        return patients.Dequeue();
    }

    public static GWorld Instance => instance;
    public WorldStates World => world;
}
