using System;
using UnityEngine;

namespace UISystem.Saving
{
    public interface ISaver
    {

        void Save(string sectionName, string keyName, float value);
        void Save(string sectionName, string keyName, int value);
        void Save(string sectionName, string keyName, string value);

        float Load(string sectionName, string keyName, float defaultValue);
        int Load(string sectionName, string keyName, int defaultValue);
        string Load(string sectionName, string keyName, string defaultValue);
        Vector2Int Load(string sectionName, string keyName, Vector2Int defaultValue, Func<Vector2Int, string> parserToString,
            Func<string, Vector2Int> parserFromString);

    }
}
