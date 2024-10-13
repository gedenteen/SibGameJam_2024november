using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class JsonAsyncStorageService : IStorageService
{
    public async void Save(string key, object data, Action<bool> callback = null)
    {
        string path = BuildPath(key);

        await Task.Run(() =>
        {
            string json = JsonConvert.SerializeObject(data);

            using (StreamWriter fileStream = new StreamWriter(path))
            {
                fileStream.Write(json);
            }
        });

        callback?.Invoke(true);
    }

    public async void Load<T>(string key, Action<T> callback)
    {
        string path = BuildPath(key);

        T data = await Task.Run<T>(() =>
        {
            using (StreamReader fileStream = new StreamReader(path))
            {
                string json = fileStream.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        });

        callback?.Invoke(data);
    }
    
    private string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}
