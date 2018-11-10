using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

namespace DarkLordGame
{
    public static class PersistenceData
    {
        public static void SaveData<T>(string fileName, T data)
        {
            string path = BuildFilePath(fileName);
            BinaryFormatter binnaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);

            binnaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;
            binnaryFormatter = null;
            path = null;
        }

        public static T LoadData<T>(string fileName, T defaultData)
        {
            string path = BuildFilePath(fileName);

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(path, FileMode.Open);
                T data = (T)formatter.Deserialize(fileStream);
                fileStream.Close();
                fileStream.Dispose();
                fileStream = null;
                formatter = null;
                path = null;
                return data;
            }
            path = null;
            return defaultData;
        }

        public static void DeleteFile(string fileName)
        {
            string path = BuildFilePath(fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            path = null;
        }

        private static string BuildFilePath(string fileName)
        {
            StringBuilder path = new StringBuilder();
            path.Append(Application.persistentDataPath);
            path.Append("/");
            path.Append(fileName);
            string pathString = path.ToString();
            path = null;
            return pathString;
        }
    }
}
