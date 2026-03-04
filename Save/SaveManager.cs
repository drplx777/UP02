using System.IO;
using System.Text.Json;

namespace ConsoleApp129.Save
{
    public static class SaveManager
    {
        private const string path = "save.json";

        public static void Save(GameData data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }

        public static GameData Load()
        {
            if (!File.Exists(path))
                return null;

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<GameData>(json);
        }
    }
}