using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Tapecorps.GameDevelopmentKit
{
    public static class GDK
    {
        public static Transform GetChildByName(Transform parent, string name)
        {
            Transform[] children = parent.GetComponentsInChildren<Transform>();

            foreach (Transform c in children)
                if (c.name.Trim() == name.Trim()) return c;

            return null;
        }

        public static Transform[] GetChildrenByName(Transform parent, string name)
        {
            Transform[] children = parent.GetComponentsInChildren<Transform>();
            List<Transform> found = new List<Transform>();

            foreach (Transform c in children)
                if (c.name.Trim() == name.Trim()) found.Add(c);

            return found.ToArray();
        }

        public static Vector3 LerpAngle(Vector3 from, Vector3 to, float time)
        {
            return new Vector3(Mathf.LerpAngle(from.x, to.x, time), Mathf.LerpAngle(from.y, to.y, time), Mathf.LerpAngle(from.z, to.z, time));
        }

        public static Vector2 LerpAngle(Vector2 from, Vector2 to, float time)
        {
            return new Vector2(Mathf.LerpAngle(from.x, to.x, time), Mathf.LerpAngle(from.y, to.y, time));
        }
    }

    public class SaveFile : IDisposable
    {
        private List<string> keys;
        private List<object> items;
        private List<Type> types;

        private int readedLine = 0;

        private bool loaded = false;

        /// <summary>Creates a new save file.</summary>
        public SaveFile()
        {
            keys = new List<string>();
            items = new List<object>();
            types = new List<Type>();
            readedLine = 0;
            loaded = false;
        }


        /// <summary>Clears the save file.</summary>
        public void Clear()
        {
            keys.Clear();
            items.Clear();
            types.Clear();
            readedLine = 0;
        }

        /// <summary>Saves the contents of save file.</summary>
        public void Save(string path)
        {
            if (!(keys.Count == items.Count && types.Count == items.Count)) throw new Exception("There's an error. Please clear the save file.");

            List<string> lines = new List<string>();

            for(int i = 0; i < keys.Count; i++)
            {
                lines.Add(GenerateSaveLine(i));
            }

            FileInfo fi = new FileInfo(path);

            if (!fi.Directory.Exists) throw new Exception("Path directory doesn't exist");

            File.WriteAllLines(path,lines.ToArray());
        }

        /// <summary>Loads a save file from path.</summary>
        public void Load(string path)
        {
            if(!File.Exists(path)) throw new Exception("Save file doesn't exist");

            string[] lines = File.ReadAllLines(path);

            foreach(string line in lines)
                EvaulateSaveLine(line);

            loaded = true;
        }

        #region Private Methods
        private bool CheckIllegalKeys(string key)
        {
            return (key.Contains("System.Single") || key.Contains("System.Int32") || key.Contains("System.String"));
        }

        private void Write(string key, object _item)
        {
            if (CheckIllegalKeys(key)) throw new Exception("Key contains illegal content.");
;
            Type type = _item.GetType();

            if (type != typeof(int) && type != typeof(string) && type != typeof(float) && type != typeof(bool)) throw new Exception($"Unsupported data type {type}.");

            keys.Add(key);
            items.Add(_item);
            types.Add(_item.GetType());
        }

        private T Read<T>(string key)
        {
            if (!loaded) throw new Exception("Save file isn't loaded yet.");
            if (string.IsNullOrEmpty(key)) throw new Exception("Key cannot be empty!");

            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i].Trim() == key.Trim())
                {
                    readedLine++;
                    return (T)items[i];
                }
            }

            throw new Exception($"Value related to key \"{key}\" couldn't found");
        }

        private string GenerateSaveLine(int index)
        {
            return $"{keys[index]}{types[index]}{items[index]}";
        }

        private void EvaulateSaveLine(string line)
        {
            string[] parts = line.Split(new string[] { "System.String", "System.Int32", "System.Single", "System.Boolean" }, StringSplitOptions.RemoveEmptyEntries);

            keys.Add(parts[0]);

            if (line.Contains("System.Int32"))
                items.Add(System.Convert.ToInt32(parts[1]));
            else if (line.Contains("System.Single"))
                items.Add(System.Convert.ToSingle(parts[1]));
            else if (line.Contains("System.Boolean"))
                items.Add(System.Convert.ToBoolean(parts[1]));
            else
                items.Add(parts[1]);
        }

        #endregion

        /// <summary>Disposal of the save file.</summary>
        public void Dispose()
        {
            keys = null;
            items = null;
            types = null;
            readedLine = -1;
            loaded = false;
        }

        #region Writers
        /// <summary>Writes a boolean value to save file.</summary>
        public void WriteBoolean(string key, bool value)
        {
            Write(key, value);
        }

        /// <summary>Writes a integer value to save file.</summary>
        public void WriteInteger(string key, int value)
        {
            Write(key, value);
        }

        /// <summary>Writes a string value to save file.</summary>
        public void WriteString(string key, string value)
        {
            Write(key, value);
        }

        /// <summary>Writes a float value to save file.</summary>
        public void WriteFloat(string key, float value)
        {
            Write(key, value);
        }

        /// <summary>Writes a Vector2 value to save file.</summary>
        public void WriteVector2(string key, Vector2 value)
        {
            Write(key + "_x", value.x);
            Write(key + "_y", value.y);
        }

        /// <summary>Writes a Vector3 value to save file.</summary>
        public void WriteVector3(string key, Vector3 value)
        {
            Write(key + "_x", value.x);
            Write(key + "_y", value.y);
            Write(key + "_z", value.z);
        }

        /// <summary>Writes a Quaternion value to save file.</summary>
        public void WriteQuaternion(string key, Quaternion value)
        {
            Write(key + "_x", value.x);
            Write(key + "_y", value.y);
            Write(key + "_z", value.z);
            Write(key + "_w", value.w);
        }

        #endregion

        #region Readers
        /// <summary>Searches and reads a boolean value by the key from save file.</summary>
        public bool ReadBoolean(string key)
        {
            return Read<bool>(key);
        }

        /// <summary>Searches and reads a integer value by the key from save file.</summary>
        public int ReadInteger(string key)
        {
            return Read<int>(key);
        }

        /// <summary>Searches and reads a string value by the key from save file.</summary>
        public string ReadString(string key)
        {
            return Read<string>(key);
        }

        /// <summary>Searches and reads a float value by the key from save file.</summary>
        public float ReadFloat(string key)
        {
            return Read<float>(key);
        }

        /// <summary>Searches and reads a Vector2 value by the key from save file.</summary>
        public Vector2 ReadVector2(string key)
        {
            return new Vector2(Read<float>(key+"_x"), Read<float>(key + "_y"));
        }

        /// <summary>Searches and reads a Vector3 value by the key from save file.</summary>
        public Vector3 ReadVector3(string key)
        {
            return new Vector3(Read<float>(key + "_x"), Read<float>(key + "_y"), Read<float>(key + "_z"));
        }

        /// <summary>Searches and reads a Quaternion value by the key from save file.</summary>
        public Quaternion ReadQuaternion(string key)
        {
            return new Quaternion(Read<float>(key + "_x"), Read<float>(key + "_y"), Read<float>(key + "_z"), Read<float>(key + "_w"));
        }

        #endregion

    }
}
