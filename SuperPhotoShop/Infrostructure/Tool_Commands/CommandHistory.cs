﻿using SuperPhotoShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public class CommandHistory
    {
        private Stack<Command> commandsStack = new Stack<Command>();

        private void CommandAdd(Command command)
        {
            commandsStack.Push(command);
        }

        public bool CanUndoCommand
        {
            get
            {
                if(commandsStack.Count!=0)return true;
                else return false;
            }
        }

        public Stack<Command> Commands
        {
            get
            {
                return commandsStack;
            }
        }

        public CommandHistory()
        {
        }

        public CommandHistory(Stack<Command> commandsStack)
        {
            this.commandsStack = commandsStack;
        }

        public void ExecuteCommand(Command command, ImageModel imageModel)
        {
            command.Execute(imageModel);
            CommandAdd(command);
        }
        public ImageModel UndoCommand()
        {
            Command command = commandsStack.Pop();
            return new ImageModel(command.Undo());
            
        }

    }
}
