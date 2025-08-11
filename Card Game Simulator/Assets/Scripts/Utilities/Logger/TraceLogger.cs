using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public static class TraceLogger
{
    public static bool IsLogMethodEnabled { get; set; }
    public static bool IsLogVariableEnabled { get; set; }
    public static bool IsLogListEnabled { get; set; }
    public static bool IsLogDictionaryEnabled { get; set; }

    public static void LogMethod(
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        if (!IsLogMethodEnabled) return;
        Debug.Log($"{memberName} ; {Path.GetFileName(filePath)} at line {lineNumber}");
    }

    public static void LogVariable<T>(string variableName, T value)
    {
        if (!IsLogVariableEnabled) return;
        Debug.Log($"{variableName}: {(value != null ? value.ToString() : "null")}");
    }

    public static void LogList<T>(string listName, IList<T> list)
    {
        if (!IsLogListEnabled) return;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{listName}:");
        foreach (T member in list)
        {
            stringBuilder.AppendLine(member.ToString());
            stringBuilder.AppendLine();
        }
        Debug.Log(stringBuilder.ToString());
    }

    public static void LogDictionary<K, V>(string dictionaryName, IDictionary<K, V> dictionary)
    {
        if (!IsLogDictionaryEnabled) return;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{dictionaryName}:");
        foreach (KeyValuePair<K, V> pair in dictionary)
        {
            stringBuilder.AppendLine($"{pair.Key.ToString()} => {pair.Value.ToString()}");
            stringBuilder.AppendLine();
        }
        Debug.Log(stringBuilder.ToString());
    }
}