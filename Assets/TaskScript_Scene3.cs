using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskScript_Scene3 : MonoBehaviour
{
    /// <summary>
    /// This is the only viable way to manage multiple tasks in one scene that I can think of.
    /// It does mainly the same thing as a game manager, but flows through tasks,
    /// by using Checkpoint events and scripted counters
    /// </summary>

    public static event EventHandler<int> e_TaskComplete; // mainly used for starting sequences (laser walls)

    public Text CurrentTask;
    public Text TaskProgress;

    public GameObject Turrets;
    public GameObject Buttons;
    [SerializeField]
    private string[] taskText =
    {
        "Get through asteroid field! ",
        "Destroy all the defending turrets! ",
        "Get through the laser field! ",
        "Get through the hangar! ",
        "Destroy the datacenter transmitters! ", // made up thing
        "Defeat the BOSS! ",
    };
    private int _taskId = 0;

    void Start()
    {
        //DIalogueScript.e_ChangeTask += OnNewTask;
    }

    private void Update()
    {
        switch (_taskId)
        {
            case 1:
                TaskProgress.text = "Turrets left: " + Turrets.transform.childCount.ToString();
                e_TaskComplete(this, _taskId);
                break;
            case 4:
                TaskProgress.text = "Buttons left: " + Buttons.transform.childCount.ToString();
                e_TaskComplete(this, _taskId);
                break;
            case 6: // fired after explosion of boss
                SceneManager.LoadScene(SceneManager.sceneCount-1);
                break;
            default:
                break;
        }
    }

    // this one is triggered by checkpoint
    private void OnNewTask(object sender, int id)
    {
        CurrentTask.text = taskText[id];
    }
}
