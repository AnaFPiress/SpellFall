using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject Menu, Settings;
    private float timeScale;
    private bool isPaused = false;


    public void ButtonResume()
    {
        Menu.SetActive(false);
        Settings.SetActive(true);
    }

    public void ButtonMainMenu()
    {
        Time.timeScale = timeScale;
        SceneManager.LoadScene("Menu");
    }










    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
