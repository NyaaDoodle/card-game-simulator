using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

[System.Serializable]
public class TraceLogger
{
    public bool IsEnabled;

    public void LogMethod(
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        if (!IsEnabled) return;
        Debug.Log($"{memberName} ; {Path.GetFileName(filePath)} at line {lineNumber}");
    }

    public void LogVariable<T>(string variableName, T value)
    {
        if (!IsEnabled) return;
        Debug.Log($"{variableName}: {(value != null ? value.ToString() : "null")}");
    }

    public void LogList<T>(string listName, IList<T> list)
    {
        if (!IsEnabled) return;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{listName}:");
        foreach (T member in list)
        {
            stringBuilder.AppendLine(valueOrElseNullString(member));
            stringBuilder.AppendLine();
        }
        Debug.Log(stringBuilder.ToString());
    }

    public void LogDictionary<K, V>(string dictionaryName, IDictionary<K, V> dictionary)
    {
        if (!IsEnabled) return;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{dictionaryName}:");
        foreach (KeyValuePair<K, V> pair in dictionary)
        {
            stringBuilder.AppendLine($"{valueOrElseNullString(pair.Key)} => {valueOrElseNullString(pair.Value)}");
            stringBuilder.AppendLine();
        }
        Debug.Log(stringBuilder.ToString());
    }

    public void Log(string message)
    {
        if (!IsEnabled) return;
        Debug.Log(message);
    }

    private static string valueOrElseNullString<T>(T value)
    {
        return value != null ? value.ToString() : "null";
    }
}