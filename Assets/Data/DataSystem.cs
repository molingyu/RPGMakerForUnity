using System;
using Sirenix.OdinInspector;

namespace Assets.Data
{
    [Serializable]
    public enum Scenes
    {
        TitleScene = 1,
        MapScene = 2,
        StaffScene = 3,
    }

    public class DataSystem : SerializedScriptableObject
    {
        public Scenes FirstScene;
    }
}
