using System;
using Merrigan0.RecognizersInternal;

namespace Merrigan0 {
    public delegate void CommandAction(object data);

    public class Command {
        public CommandAction Action { get; private set; }
        public String Help { get; private set; }
        public String Name { get; private set; }
        public Recognizer Recognizer { get; private set; }

        public Command(String name, CommandAction action, String help, Recognizer recognizer = null) {
            Action = action;
            Help = help;
            Name = name;
            Recognizer = recognizer;
            if (Recognizer == null) {
                Recognizer = new SpecificStringRecognizer(name);
            }
        }
    }
}
