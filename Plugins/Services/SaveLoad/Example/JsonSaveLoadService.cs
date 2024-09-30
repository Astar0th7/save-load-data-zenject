using System.IO;
using Services.SaveLoad;
using UnityEngine;

namespace SaveLoad.Example
{
    public class JsonSaveLoadService : ISaveLoadService
    {
        private const string DATA = nameof(DATA);

        private readonly SaveLoadListener _listener;
        private ProgressData _data;

        public JsonSaveLoadService(SaveLoadListener listener)
        {
            _listener = listener;
            _data = new ProgressData();
        }

        public void Save()
        {
            _listener.Write(_data);
            string json = JsonUtility.ToJson(_data);
            using StreamWriter fileStream = new(BuildPath());
            fileStream.Write(json);
        }

        public void Load()
        {
            string path = BuildPath();
            if (File.Exists(path) == false)
            {
                _listener.Read(_data);
                return;
            }
            
            using StreamReader fileStream = new(path);
            string json = fileStream.ReadToEnd();
            
            _data = JsonUtility.FromJson<ProgressData>(json);
            _listener.Read(_data);
        }

        private string BuildPath()
        {
            return Path.Combine(Application.persistentDataPath, DATA);
        }
    }
}