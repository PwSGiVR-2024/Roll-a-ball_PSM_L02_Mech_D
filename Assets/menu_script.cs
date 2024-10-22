using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu_script : MonoBehaviour
{
    public GameObject settings_panel;
    public GameObject menu_panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start_game()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit_game()
    {
        Application.Quit(0);
    }

    public void Change_settings()
    {
        menu_panel.SetActive(false);
        settings_panel.SetActive(true);
    }

    public void Quit_settings()
    {
        menu_panel.SetActive(true);
        settings_panel.SetActive(false);
    }
}
