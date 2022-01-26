using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CommandGroup
{
    public CommandGroup (int MAX_COMMAND_SIZE)
    {
        this.MAX_COMMAND_SIZE = MAX_COMMAND_SIZE;
    }

    [SerializeField]private List<Command> commands = new List<Command>();
    private int MAX_COMMAND_SIZE = -1;

    public void Add(Command command)
    {
        commands.Add(command);
    }
    
    public void RemoveAt(int index)
    {
        commands.RemoveAt(index);
    }

    public void Remove(Command command)
    {
        commands.Remove(command);
    }

    public Command Get(int index){
        return commands[index];
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

    public Command GetLastCommand()
    {
        return commands[commands.Count-1];
    }

    public List<Command> Reverse()
    {
        commands.Reverse();
        return commands;
    }
}
