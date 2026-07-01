using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
   public GameObject MainMenu;
   public GameObject Settingsmenu;

   public void play()
    {
        SceneManager.LoadScene("OverWorld");
    }

    public void settings()
    {
        MainMenu.SetActive(false);
        Settingsmenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }



    void Start()
    {
        MainMenu.SetActive(true);
        Settingsmenu.SetActive(false);
    }

    
    void Update()
    {
        play();
        settings();
        Quit();
    }
}
