using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
    }
    public void Main_menu_button()
    {
        SceneManager.LoadScene(0);
    }

    public void Settings_button()
    {
        SettingsMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void Quit_settings()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void Resume_button()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void Quit_button()
    {
        Application.Quit();
    }
}
