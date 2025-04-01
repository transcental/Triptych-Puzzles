using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button creditsButton;
    
    void Start()
    {
        var displays = Display.displays.Length;
        for (var i = 0; i < displays && i < 3; i++)
        {
            if (!Display.displays[i].active)
            {
                Display.displays[i].Activate();
            }
        }
        
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        creditsButton.onClick.AddListener(ShowCredits);
    }
    
    void StartGame()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("Map");
    }
    
    void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
    
    void ShowCredits()
    {
        Debug.Log("Showing credits");
    }
}
