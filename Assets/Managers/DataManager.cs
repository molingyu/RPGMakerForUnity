using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Data;
using Assets.Plugins.IceKori.Syntax.BaseType;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Assets.Managers
{
    [Serializable]
    public struct SaveFileHeadInfo
    {
        public bool IsAuto;
        public DateTime SaveTime;
        public List<Sprite> PartySprites;
        public TimeSpan PlayTime;
        public string SaveMap;
        public string Path;
    }

    [Serializable]
    public class GlobalSaveFile : SerializedScriptableObject
    {
        public List<IceKoriBaseType> GlobalSaveVariables;
        public List<SaveFileHeadInfo> SaveFileInfos;

        public GlobalSaveFile()
        {
            GlobalSaveVariables = new List<IceKoriBaseType>();
            SaveFileInfos = new List<SaveFileHeadInfo>();
        }
    }

    [Serializable]
    public class SaveFileData : SerializedScriptableObject
    {
        public DateTime SaveFileDate;

        public SaveFileData()
        {
            SaveFileDate = DateTime.Now;
        }

        public void ExtractSaveFileData()
        {
        }
    }

    public static class DataManager
    {
        public static string SaveFilePath;
        public static GlobalSaveFile GlobalSaveFile;
        public static Database Database;

        public static void Init()
        {
            SaveFilePath = Application.persistentDataPath;
            LoadDatabase();
        }

        public static void LoadDatabase()
        {
            var bundle = AssetBundle.LoadFromFile("Assets/AssetBundles/Database");
            Database = bundle.LoadAsset<Database>("Database");
        }

        public static void GlobalSaveFileGet()
        {
            if (_SaveFileExists(_MakeSaveFilePath("GlobalSaveFile")))
            {
                GlobalSaveFile = _LoadFile<GlobalSaveFile>(_MakeSaveFilePath("GlobalSaveFile"));
                Debug.Log(GlobalSaveFile.SaveFileInfos.Count);
            }
            else
            {
                GlobalSaveFile = ScriptableObject.CreateInstance<GlobalSaveFile>();
                SaveGlobalSaveFile();
            }
            GlobalSaveFile.SaveFileInfos = GlobalSaveFile.SaveFileInfos.Where(info => _SaveFileExists(info.Path)).ToList();
            SaveGlobalSaveFile();
        }

        public static void CreateGameObjects()
        {

        }

        public static void StartNewGame()
        {
            CreateGameObjects();
        }

        public static void LoadGame(string path)
        {
            SaveFileData file = _LoadFile<SaveFileData>(path);
            file.ExtractSaveFileData();
        }

        public static void SaveGame(bool auto = false)
        {

            var data = ScriptableObject.CreateInstance<SaveFileData>();
            string name = auto ? "AutoSave" : _MakeNewSaveFileName(data.SaveFileDate);
            int pos = auto ? 0 : GlobalSaveFile.SaveFileInfos.Count;
            _SaveFile(data, name);
            var saveFileInfo = new SaveFileHeadInfo()
            {
                IsAuto = auto,
                SaveTime = data.SaveFileDate
            };
            GlobalSaveFile.SaveFileInfos.Insert(pos, saveFileInfo);
            SaveGlobalSaveFile();
        }

        public static void SaveGlobalSaveFile()
        {
            _SaveFile(GlobalSaveFile, _MakeSaveFilePath("GlobalSaveFile"));
        }

        private static void _SaveFile<T>(T data, string fileName) where T : SerializedScriptableObject
        {
            Debug.Log("Save File to:" + _MakeSaveFilePath(fileName));
            byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.JSON);
            File.WriteAllBytes(_MakeSaveFilePath(fileName), bytes);
        }

        private static T _LoadFile<T>(string path) where T : SerializedScriptableObject
        {
            byte[] bytes = File.ReadAllBytes(path);
            return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.JSON);
        }

        private static bool _SaveFileExists(string path)
        {
            return File.Exists(path);
        }

        private static string _MakeSaveFilePath(string fileName)
        {
            return $"{SaveFilePath}/Save{fileName}.rmuSaveData";
        }

        private static string _MakeNewSaveFileName(DateTime data)
        {
            return data.ToString("s").Replace('-', '_').Replace(':', '_');
        }
    }
}
