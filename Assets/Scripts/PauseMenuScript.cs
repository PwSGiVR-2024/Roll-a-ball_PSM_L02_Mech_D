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
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void SettingsButton()
    {
        SettingsMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void QuitSettings()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
