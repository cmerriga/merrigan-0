////using System;
////using System.Collections.Generic;
////using System.IO;
////using System.Text;
////using Merrigan0.Internal.DotNet.Extensions;
////using Merrigan0.Internal.Nodes;

////namespace Merrigan0.Csv {
    ////[Untested]
////    public static class Csv {
////        public static void AppendLine(MutableString text, Array<String> valueStrings) {
////            bool first = true;
////            foreach (String valueString in valueStrings) {
////                if (first) {
////                    first = false;
////                } else {
////                    text.Append(",");
////                }
////                String csvValueString = ToValueString(valueString);
////                text.Append(csvValueString);
////            }
////            text.Append("\n");
////        }

////        [WhatItDoes("Creates an array of hashes, where each represents a line in a CSV file")]
////        [return: NotNull]
////        public static Node ReadFile(String path, Array<String> headers = null, Array<Type> types = null) {
////            using (StreamReader reader = new StreamReader(path)) {
////                return Read(reader, headers, types);
////            }
////        }

////        [WhatItDoes("Creates an array of hashes, where each represents a line CSV-formatted text")]
////        public static Node Read(TextReader reader, Array<String> headers = null, Array<Type> types = null) {
////            if (headers == null) {
////                headers = ReadLine(reader);
////                if (headers == null) {
////                    return new EmptyArrayNode();
////                }
////            }
////            MutableArray<Node> nodesSoFar = new MutableArray<Node>();
////            while (true) {
////                Array<String> values = ReadLine(reader);
////                if (values == null) {
////                    break;
////                }

////                if (values.Length != headers.Length) {
////                    throw new Exception("BLAH");
////                }

////                MutableNode nodeSoFar = new MutableNode();
////                for (int i = 0; i < values.Length; ++i) {
////                    if (types != null) {
////                        nodeSoFar.Set(headers[i], Convert.ChangeType(values[i], types[i]));
////                    } else {
////                        nodeSoFar.Set(headers[i], values[i]);
////                    }
////                }
////                nodesSoFar.Append(nodeSoFar);
////            }
////            return new NodeArrayArrayNode(nodesSoFar);
////        }

////        [WhatItDoes("Reads the next line, if there is one")]
////        [return: NotNull]
////        public static Array<String> ReadLine(TextReader reader, int? expectedValues = null) {
////            String line = reader.ReadLine();
////            if (line == null) {
////                return null;
////            }
////            MutableArray<String> valueStringsSoFar = new MutableArray<String>();
////            String stringValue;
////            int i = 0;
////            while (true) {
////                stringValue = ReadStringValue(line, ref i);
////                if (stringValue == null)
////                    break;

////                valueStringsSoFar.Append(stringValue);
////            }

////            if (expectedValues.HasValue && valueStringsSoFar.Length != expectedValues.Value) {
////                throw new Exception("BLAH");
////            }
////            if (valueStringsSoFar.Length == 0) {
////                return null;
////            }
////            return valueStringsSoFar.Current;
////        }

////        public static String ReadStringValue(String line, [NotNegative] ref int i) {
////            char ch;
////            while (true) {
////                if (i >= line.Length) {
////                    return null;
////                }
////                ch = line[i];
////                if (!(ch == ' ' || ch == '\t')) {
////                    break;
////                }
////                ++i;
////            }
////            MutableArray<char> charactersSoFar = new MutableArray<char>();
////            bool inQuote = false;
////            while (true) {
////                if (i >= line.Length) {
////                    break;
////                }
////                ch = line[i];

////                if (ch == '\\') {
////                    ++i;
////                    if (i >= line.Length) {
////                        break;
////                    }
////                    ch = line[i];
////                    charactersSoFar.Append(ch);
////                    continue;
////                }

////                if (!inQuote && (ch == ',')) {
////                    ++i;
////                    break;
////                }

////                if (ch == '"') {
////                    if (inQuote) {
////                        ++i;
////                        while (true) {
////                            if (i >= line.Length) {
////                                return null;
////                            }
////                            ch = line[i];
////                            if (!(ch == ' ' || ch == '\t')) {
////                                break;
////                            }
////                        }
////                        if (line[i] == ',') {
////                            ++i;
////                        }
////                        break;
////                    } else {
////                        if (charactersSoFar.Length == 0) {
////                            inQuote = true;
////                            ++i;
////                            continue;
////                        } else {
////                            throw new Exception("BLAH");
////                        }
////                    }
////                }
////                charactersSoFar.Append((char)ch);
////                ++i;
////            }

////            String stringValue = new CharacterArrayString(charactersSoFar);
////            return stringValue;
////        }

////        [return: NotNull]
////        public static String ToCsv(ArrayNode nodes) {
////            if (nodes.Length == 0) {
////                return String.Empty;
////            }
////            Array<String> headers = nodes[0].Transform(p => p.Key);
////            MutableString csvSoFar = new MutableString();
////            AppendLine(csvSoFar, headers);
////            foreach (Node node in nodes) {
////                MutableArray<String> valueStringsSoFar = new MutableArray<String>();
////                foreach (String header in headers) {
////                    object value = node[header];
////                    valueStringsSoFar.Append(value == null ? String.Empty : (String)value.ToString());
////                }
////                AppendLine(csvSoFar, valueStringsSoFar);
////            }
////            return csvSoFar.Current;
////        }

////        [return: NotNull]
////        public static String ToValueString(String s) {
////            if (s.Contains(" ") ||
////                s.Contains("\t") ||
////                s.Contains("\r") ||
////                s.Contains("\n") ||
////                s.Contains("\"") ||
////                s.Contains(",") ||
////                s.Contains("\\")) {
////                    MutableString valueStringSoFar = new MutableString();
////                    valueStringSoFar.Append("\"");
////                    valueStringSoFar.Replace("\\", "\\\\");
////                    valueStringSoFar.Replace("\"", "\\\"");
////                    valueStringSoFar.Replace(",", "\\,");
////                    valueStringSoFar.Append("\"");
////                    return valueStringSoFar.Current;
////            } else {
////                return s;
////            }
////        }

////        public static void WriteFile(String path, ArrayNode nodes) {
////            String csv = ToCsv(nodes);
////            File.WriteAllText(path, csv);
////        }
////    }
////}
