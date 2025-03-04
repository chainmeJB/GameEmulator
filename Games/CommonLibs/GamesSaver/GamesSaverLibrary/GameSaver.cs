using System;
using System.IO;
using Newtonsoft.Json;

namespace GamesSaverLibrary
{
    public class GameSaver<T>
    {
        public void SaveGame(T data, string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public T LoadGame(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        return serializer.Deserialize<T>(reader);
                    }
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return default;
            }
        }
    }
}