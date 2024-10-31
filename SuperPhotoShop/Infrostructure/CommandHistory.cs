using System.Collections.Generic;
using System.Linq;

namespace SuperPhotoShop.Infrostructure
{
    public class CommandHistory
    {
        private Stack<Command> commandsStack;

        public void ExecuteCommand(Command command)
        {
            command.Execute();
            commandsStack.Push(command);
        }
        public void UndoCommand()
        {
            Command command = commandsStack.Pop();
            command.Undo();
            
        }
        public void RedoCommand()
        {
            Command command = commandsStack.Last();
            command.Redo();
        }
    }
}
