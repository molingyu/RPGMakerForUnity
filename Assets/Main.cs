using Assets.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class Main : MonoBehaviour
    {

        void Start()
        {
            DataManager.Init();
            SceneManager.LoadScene((int)DataManager.Database.System.FirstScene);
        }

    }
}
