using UnityEngine;

public class LaserActivatorScript : MonoBehaviour
{
    public int TaskId;
    public float SwitchTime = 5f; // time between active laser switch

    private bool _start;
    private float _lastActivationTime = 0;
    private int _inactiveLaser = 0;
    private int _childrenNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TaskScript_Scene3.e_TaskComplete += OnLaserStart;
        _childrenNumber = gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_start)
        {
            return;
        }

        if (Time.time > _lastActivationTime + SwitchTime)
        {
            gameObject.transform.GetChild(_inactiveLaser).gameObject.SetActive(true);

            if (_inactiveLaser != _childrenNumber - 1)
            {
                _inactiveLaser++;
            }
            else
            {
                _inactiveLaser = 0;
            }

            gameObject.transform.GetChild(_inactiveLaser).gameObject.SetActive(false);
            _lastActivationTime = Time.time;
        }
    }

    private void OnDestroy()
    {
        TaskScript_Scene3.e_TaskComplete -= OnLaserStart;
    }

    void OnLaserStart(object o, int taskId)
    {
        if (taskId == TaskId)
        {
            _start = true;
        }
    }
}
