using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu_script : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject MenuPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }

    public void ChangeSettings()
    {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void QuitSettings()
    {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
}
