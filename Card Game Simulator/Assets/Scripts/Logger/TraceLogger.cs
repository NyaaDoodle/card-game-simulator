using System;
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
        if (list != null)
        {
            foreach (T member in list)
            {
                stringBuilder.AppendLine(valueOrElseNullString(member));
                stringBuilder.AppendLine();
            }
        }
        else
        {
            stringBuilder.AppendLine("null");
        }
        Debug.Log(stringBuilder.ToString());
    }

    public void LogDictionary<K, V>(string dictionaryName, IDictionary<K, V> dictionary)
    {
        if (!IsEnabled) return;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{dictionaryName}:");
        if (dictionary != null)
        {
            foreach (KeyValuePair<K, V> pair in dictionary)
            {
                stringBuilder.AppendLine($"{valueOrElseNullString(pair.Key)} => {valueOrElseNullString(pair.Value)}");
                stringBuilder.AppendLine();
            }
        }
        else
        {
            stringBuilder.AppendLine("null");
        }
        Debug.Log(stringBuilder.ToString());
    }

    public void LogEnumerable<T>(string enumerableName, IEnumerable<T> enumerable)
    {
        if (!IsEnabled) return;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{enumerableName}:");
        if (enumerable != null)
        {
            foreach (T member in enumerable)
            {
                stringBuilder.AppendLine(valueOrElseNullString(member));
                stringBuilder.AppendLine();
            }
        }
        else
        {
            stringBuilder.AppendLine("null");
        }
        Debug.Log(stringBuilder.ToString());
    }

    public void Log(string message)
    {
        if (!IsEnabled) return;
        Debug.Log(message);
    }

    public void LogException(Exception e)
    {
        if (!IsEnabled) return;
        Debug.LogException(e);
    }

    private static string valueOrElseNullString<T>(T value)
    {
        return value != null ? value.ToString() : "null";
    }
}