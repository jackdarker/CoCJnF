using System;
using System.IO; // Required fro reading/writing to files.
using UnityEngine;
using System.Collections.Generic;

// Instance of this class can be created as assets.
// Each instance contains collections of data from
// the Saver monobehaviours they have been referenced
// by.  Since assets exist outside of the scene, the
// data will persist ready to be reloaded next time
// the scene is loaded.  Note that these assets
// DO NOT persist between loads of a build and can
// therefore NOT be used for saving the gamestate to
// disk.
[CreateAssetMenu]
public class SaveData : ResettableScriptableObject {
    // This nested class is a lighter replacement for
    // Dictionaries.  This is required because Dictionaries
    // are not serializable.  It has a single generic type
    // that represents the type of data to be stored in it.
    [Serializable]
    public class KeyValuePairLists<T> {
        public List<string> keys = new List<string>();      // The keys are unique identifiers for each element of data. 
        public List<T> values = new List<T>();              // The values are the elements of data.


        public void Clear() {
            keys.Clear();
            values.Clear();
        }


        public void TrySetValue(string key, T value) {
            // Find the index of the keys and values based on the given key.
            int index = keys.FindIndex(x => x == key);

            // If the index is positive...
            if (index > -1) {
                // ... set the value at that index to the given value.
                values[index] = value;
            } else {
                // Otherwise add a new key and a new value to the collection.
                keys.Add(key);
                values.Add(value);
            }
        }


        public bool TryGetValue(string key, ref T value) {
            // Find the index of the keys and values based on the given key.
            int index = keys.FindIndex(x => x == key);

            // If the index is positive...
            if (index > -1) {
                // ... set the reference value to the value at that index and return that the value was found.
                value = values[index];
                return true;
            }

            // Otherwise, return that the value was not found.
            return false;
        }
    }


    // These are collections for various different data types.
    public KeyValuePairLists<bool> boolKeyValuePairLists = new KeyValuePairLists<bool>();
    public KeyValuePairLists<int> intKeyValuePairLists = new KeyValuePairLists<int>();
    public KeyValuePairLists<string> stringKeyValuePairLists = new KeyValuePairLists<string>();
    public KeyValuePairLists<Vector3> vector3KeyValuePairLists = new KeyValuePairLists<Vector3>();
    public KeyValuePairLists<Quaternion> quaternionKeyValuePairLists = new KeyValuePairLists<Quaternion>();
    #region Defaults
    public const string DEFAULT_LEVEL = "level1";
    private const int DEFAULT_COINS = 0;
    private const int DEFAULT_HEALTH = 100;
    private const int DEFAULT_LIVES = 3;
    #endregion

    // We initialize all of the stats to be default values.
    public int coins = DEFAULT_COINS;
    public int health = DEFAULT_HEALTH;
    public int lives = DEFAULT_LIVES;
    public string lastLevel = DEFAULT_LEVEL;

    const bool DEBUG_ON = true;
    
    /// <summary>
    /// Writes the instance of this class to the specified file in JSON format.
    /// </summary>
    /// <param name="filePath">The file name and full path to write to.</param>
    public void WriteToFile(string filePath) {
        // Convert the instance ('this') of this class to a JSON string with "pretty print" (nice indenting).
        string json = JsonUtility.ToJson(this, true);
        Save("AppVersion", 100);    //Todo  the version of the game; use it for detecting load of old data-verson
        string json2 = JsonUtility.ToJson(intKeyValuePairLists, true);
        // Write that JSON string to the specified file.
        File.WriteAllText(filePath, json);
        
        // Tell us what we just wrote if DEBUG_ON is on.
        if (DEBUG_ON)
            Debug.LogFormat("WriteToFile({0}) -- data:\n{1}", filePath, json);
    }

    /// <summary>
    /// Returns a new SaveData object read from the data in the specified file.
    /// </summary>
    /// <param name="filePath">The file to attempt to read from.</param>
    public static SaveData ReadFromFile(string filePath) {
        // If the file doesn't exist then just return the default object.
        if (!File.Exists(filePath)) {
            Debug.LogErrorFormat("ReadFromFile({0}) -- file not found, returning new object", filePath);
            return new SaveData();
        } else {
            // If the file does exist then read the entire file to a string.
            string contents = File.ReadAllText(filePath);

            // If debug is on then tell us the file we read and its contents.
            if (DEBUG_ON)
                Debug.LogFormat("ReadFromFile({0})\ncontents:\n{1}", filePath, contents);

            // If it happens that the file is somehow empty then tell us and return a new SaveData object.
            if (string.IsNullOrEmpty(contents)) {
                Debug.LogErrorFormat("File: '{0}' is empty. Returning default SaveData");
                return new SaveData();
            }

            // Otherwise we can just use JsonUtility to convert the string to a new SaveData object.
            return JsonUtility.FromJson<SaveData>(contents);
        }
    }

    /// <summary>
    /// This is used to check if the SaveData object is the same as the default.
    /// i.e. it hasn't been written to yet.
    /// </summary>
    public bool IsDefault() {
        return (
            coins == DEFAULT_COINS &&
            health == DEFAULT_HEALTH &&
            lives == DEFAULT_LIVES &&
            lastLevel == DEFAULT_LEVEL);
    }

    /// <summary>
    /// A friendly string representation of this object.
    /// </summary>
    public override string ToString() {

        return string.Format(
            "coins: {0}\nhealth: {1}\nlives: {2}\npowerUps: {3}\nlastLevel: {4}",
            coins,
            health,
            lives,
            lastLevel
            );
    }

    public override void Reset() {
        boolKeyValuePairLists.Clear();
        intKeyValuePairLists.Clear();
        stringKeyValuePairLists.Clear();
        vector3KeyValuePairLists.Clear();
        quaternionKeyValuePairLists.Clear();
    }


    // This is the generic version of the Save function which takes a
    // collection and value of the same type and then tries to set a value.
    private void Save<T>(KeyValuePairLists<T> lists, string key, T value) {
        lists.TrySetValue(key, value);
    }


    // This is similar to the generic Save function, it tries to get a value.
    private bool Load<T>(KeyValuePairLists<T> lists, string key, ref T value) {
        return lists.TryGetValue(key, ref value);
    }


    // This is a public overload for the Save function that specifically
    // chooses the generic type and calls the generic version.
    public void Save(string key, bool value) {
        Save(boolKeyValuePairLists, key, value);
    }


    public void Save(string key, int value) {
        Save(intKeyValuePairLists, key, value);
    }


    public void Save(string key, string value) {
        Save(stringKeyValuePairLists, key, value);
    }


    public void Save(string key, Vector3 value) {
        Save(vector3KeyValuePairLists, key, value);
    }


    public void Save(string key, Quaternion value) {
        Save(quaternionKeyValuePairLists, key, value);
    }


    // This works the same as the public Save overloads except
    // it calls the generic Load function.
    public bool Load(string key, ref bool value) {
        return Load(boolKeyValuePairLists, key, ref value);
    }


    public bool Load(string key, ref int value) {
        return Load(intKeyValuePairLists, key, ref value);
    }


    public bool Load(string key, ref string value) {
        return Load(stringKeyValuePairLists, key, ref value);
    }


    public bool Load(string key, ref Vector3 value) {
        return Load(vector3KeyValuePairLists, key, ref value);
    }


    public bool Load(string key, ref Quaternion value) {
        return Load(quaternionKeyValuePairLists, key, ref value);
    }
}
