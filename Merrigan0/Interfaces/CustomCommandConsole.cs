using System;
using Merrigan0.Internal.DotNet.Polyfills.System;

namespace Merrigan0 {
    public class CustomCommandConsole : CommandConsole {
        private Func<String> getCommandAction;
        private Func<String, bool> tryProcessCommand;
        private Action writePromptAction;

        public CustomCommandConsole(
            Array<Command> commands, 
            Action writePromptAction = null,
            Func<String> getCommandAction = null,
            Func<String, bool> tryProcessCommand = null) : 
            base(commands) 
        {
            this.getCommandAction = getCommandAction;
            this.tryProcessCommand = tryProcessCommand;
            this.writePromptAction = writePromptAction;
        }

        protected override String GetCommandLine() {
            if (getCommandAction == null) {
                return base.GetCommandLine();
            }
            return getCommandAction();
        }

        protected override bool TryProcessCommandLine(String command) {
            if (tryProcessCommand == null) {
                return base.TryProcessCommandLine(command);
            }
            return tryProcessCommand(command);
        }

        protected override void WritePrompt() {
            if (writePromptAction == null) {
                base.WritePrompt();
                return;
            }
            writePromptAction();
        }

        //public static void Main(string[] args) {
        //    CommandConsole commandConsole = new CustomCommandConsole(
        //        new Command("another", Another, "Starts another random problem of the same type of the previous."),
        //        new Command("binary", Binary, "Dumps the bytes of a file in ."),
        //        new Command("help", Help, "Prints help on every command."),
        //        new Command("new", New, "Starts a random problem of a random type."));
        //    commandConsole.Run(((Array<string>)args).Cast<String>());
        //}
    }
}
