using System;

namespace Merrigan0 {
    /*
     * Text-only interface that approximates the capabilities of most OS's standard terminal programs.
     * Initialized with all commands.
     * 
     * Example:
     *      CommandConsole commandConsole = new CommandConsole(
     *          new Command("another", Another, "Starts another random problem of the same type of the previous."),
     *          new Command("binary", Binary, "Dumps the bytes of a file in ."),
     *          new Command("help", Help, "Prints help on every command."),
     *          new Command("new", New, "Starts a random problem of a random type."));
     *      commandConsole.Run(((Array<string>)args).Cast<String>());
     */
    public abstract class CommandConsole {
        protected Array<Command> commands;
        //protected object masterObject;
        protected bool quitRequested;

        public CommandConsole(/*object masterObject, */params Command[] commands) : this(/*masterObject, */(Array<Command>)commands) { }

        public CommandConsole(/*object masterObject, */Array<Command> commands) {
            this.commands = commands;
            //this.masterObject = masterObject;
        }

        public void Run(Array<String> args) {
            while (true) {
                WritePrompt();
                String commandLine = GetCommandLine();
                if (!TryProcessCommandLine(commandLine)) {
                    Console.WriteLine("What?");
                }
                if (quitRequested) {
                    break;
                }
                Console.WriteLine();
            }
        }

        public void RequestQuit() {
            quitRequested = true;
        }

        protected virtual String GetCommandLine() {
            return Console.ReadLine();
        }

        protected virtual bool TryProcessCommandLine(String commandLine) {
            foreach (Command commandToTry in commands) {
                if (commandToTry.Name == commandLine) {
                    commandToTry.Action(commandLine);
                    return true;
                }
            }
            return false;
        }

        protected virtual void WritePrompt() {
            Console.Write(">");
        }
    }
}
