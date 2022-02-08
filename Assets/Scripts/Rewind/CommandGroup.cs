using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CommandGroup
{
    public CommandGroup(int MAX_COMMAND_SIZE)
    {
        this.MAX_COMMAND_SIZE = MAX_COMMAND_SIZE;
    }

    [SerializeField] private readonly Stack<Command> commands = new Stack<Command>();
    private int MAX_COMMAND_SIZE = -1;

    public void Add(Command command)
    {
        commands.Push(command);
    }

    public Command GetLastCommand()
    {
        return commands.Pop();
    }

    public void ClearCommands()
    {
        commands.Clear();
    }

    public bool IsFull()
    {
        return commands.Count == MAX_COMMAND_SIZE ? true : false;
    }
    
    public bool IsEmpty()
    {
        return commands.Count <= 0 ? true : false;
    }

    public int Lenght()
    {
        return commands.Count;
    }
}
