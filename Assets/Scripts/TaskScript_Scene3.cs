using NUnit.Framework;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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

    private string[] _taskText = new string[8]
    {
        "Get through asteroid field! ",
        "Destroy all the defending turrets! ",
        "Get through the laser field! ",
        "Get through the hangar! ",
        "Reach helipad! ",
        "Destroy the datacenter transmitters! ", // made up thing
        "Defeat the BOSS! ",
        "Congratulation!!",
    };
    private int _taskId = 0;
    private bool _doors = false;

    void Start()
    {
        DIalogueScript.e_ChangeTask += OnNewTask;
        BossScript.e_BossKilled += OnNewTask;
    }

    private void Update()
    {
        switch (_taskId)
        {
            case 1:
                TaskProgress.text = "Turrets left: " + Turrets.transform.childCount.ToString();
                if (Turrets.transform.childCount == 0){
                    e_TaskComplete?.Invoke(this, _taskId+1);
                    OnNewTask(null, _taskId + 1);
                }
                break;
            case 5:
                Buttons.SetActive(true); // they don't exist before so palyer can't skip
                TaskProgress.text = "Buttons left: " + Buttons.transform.childCount.ToString();
                if (Buttons.transform.childCount == 0 && !_doors)
                {
                    e_TaskComplete?.Invoke(this, _taskId+1);
                    _doors = true;
                }
                break;
                case 6:
                if (_doors)
                {
                    e_TaskComplete(this, _taskId + 1);
                    _doors = false;
                }
                    
                    break;

            case 7: // fired after explosion of boss
                StartCoroutine(BossDefeatSequence());
                break;
            default:
                break;
        }
    }

    // this one is triggered by checkpoint
    private void OnNewTask(object sender, int id)
    {
        CurrentTask.text = _taskText[id];
        TaskProgress.text = "";
        _taskId = id;
    }

    private IEnumerator BossDefeatSequence()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
