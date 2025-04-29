using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class Language {
        // Common recognizers for use by all
        private Map<String, Recognizer> recognizersByName;

        public String Name { get; private set; }

        public Language(String name, params Recognizer[] recognizers) {
            Name = name;
            recognizersByName = Map<String, Recognizer>.From(recognizers, r => r.Name);
        }

        public Recognizer Recognizer(String name) {
            return recognizersByName[name];
        }
    }
}
