using UnityEngine;
using UnityEngine.SceneManagement;
namespace com.mohamedyahya.wordpuzzletool
{

    public class MainMenu : MonoBehaviour
    {

        void Start()
        {

        }


        void Update()
        {

        }

        public void PlayGame()
        {
            SceneManager.LoadScene("LevelScene");
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void retry()
        {
            SceneManager.LoadScene("LevelScene");
        }

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
