using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CommandManager : MonoSingleton<CommandManager>
{
    public List<ICommand> CommandBuffer { get; private set; } = new List<ICommand>();

    public override void Init()
    {
        base.Init();


    }

    /*
        //Add a method to add a command
        //Play routine triggered by the Play method to execute all the command
        //Rewind routine triggered by the Rewind method to undo or play in reverse

        //Done - Finish changing colors. Turn them all white.
        //Reset - Clear the commands buffer
     */

    public void AddCommand(ICommand command)
    {
        CommandBuffer.Add(command);
    }

    public void Play()
    {
        //When play is trigger, execute PlayRoutine

        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        Debug.Log("Playing...");
        foreach (var command in CommandBuffer)
        {
            command.Execute();

            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Finished Playing...");
    }

    public void Rewind()
    {
        //When Rewind is trigger, execute RewindRoutine

        StartCoroutine(RewindCoroutine());
    }

    private IEnumerator RewindCoroutine()
    {
        Debug.Log("Rewinding...");
        foreach (var command in Enumerable.Reverse(CommandBuffer)) //rewind in the reverse order
        {
            command.Undo();

            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Finished Rewinding...");
    }

    public void Done()
    {
        var cubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach (var item in cubes)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        CommandBuffer.Clear();
    }

}
