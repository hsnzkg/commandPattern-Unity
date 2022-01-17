using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandGroup
{
    private List<Command> commands = new List<Command>();

    public void Add(Command command){
        commands.Add(command);
    }

    public void Remove(int index)
    {
        commands.RemoveAt(index);
    }

    public Command Get(int index){
        return commands[index];
    }

    public int Lenght(){
        return commands.Count;
    }

    public List<Command> Reverse(){
        commands.Reverse();
        return commands;
    }
}
