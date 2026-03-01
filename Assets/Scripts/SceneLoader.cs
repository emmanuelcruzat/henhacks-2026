using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Luke's Scene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Health == MaxHealth){
            SceneManager.LoadScene("EndingScene");
        }*/
    }
}
