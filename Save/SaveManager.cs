using System;
using System.IO;
using System.Text.Json;
using ConsoleApp129.Exceptions;

namespace ConsoleApp129.Save
{
    public static class SaveManager
    {
        private const string path = "save.json";

        public static void Save(GameData data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(path, json);
            }
            catch (IOException ioEx)
            {
                throw new SaveException("Ошибка записи файла сохранения.", ioEx);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                throw new SaveException("Доступ к файлу сохранения запрещён.", uaEx);
            }
            catch (Exception ex)
            {
                throw new SaveException("Неожиданная ошибка при сохранении.", ex);
            }
        }

        public static GameData Load()
        {
            try
            {
                if (!File.Exists(path))
                    return null;

                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<GameData>(json);
            }
            catch (JsonException jEx)
            {
                throw new LoadException("Файл сохранения повреждён или имеет неверный формат.", jEx);
            }
            catch (IOException ioEx)
            {
                throw new LoadException("Ошибка чтения файла сохранения.", ioEx);
            }
            catch (Exception ex)
            {
                throw new LoadException("Неожиданная ошибка при загрузке.", ex);
            }
        }
    }
}