using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static Queue<GameObject> patients;
    private static Queue<GameObject> cubicles;

    static GWorld()
    {
        world = new WorldStates();

        patients = new Queue<GameObject>();
       
        SetupCubicles();
    }

    private static void SetupCubicles()
    {
        cubicles = new Queue<GameObject>();

        var list = GameObject.FindGameObjectsWithTag("Cubicle");
        foreach (var c in list)
        {
            cubicles.Enqueue(c);
        }

        if (list.Length > 0)
            world.ModifyState("FreeCubicle", list.Length);
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

    public void AddCubicle(GameObject p)
    {
        cubicles.Enqueue(p);
    }

    public GameObject RemoveCubicle()
    {
        if (cubicles.Count == 0) return null;

        return cubicles.Dequeue();
    }

    public static GWorld Instance => instance;
    public WorldStates World => world;
}
