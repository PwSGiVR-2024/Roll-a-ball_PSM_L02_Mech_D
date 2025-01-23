using System;
using UnityEngine;

public class DoorOpenerScript : MonoBehaviour
{

    public float Speed = 1f;
    public float MaxRange = 11f;
    public GameObject DoorDown;
    public GameObject DoorUp;

    public int OpenTaskId = 4;
    public int CloseTaskId = 5;

    /// when player enters boss area and reaches the checkpoint
    /// and it is fals, it closes the doors after 5s so player doesn't escape
    private bool _opened = false; 
    private bool _startChanging = true;
    private Vector3 _doorDownStartPos;
    private Vector3 _doorUpStartPos;

    void Start()
    {
        TaskScript_Scene3.e_TaskComplete += ChangeState;
        _doorDownStartPos = DoorDown.transform.position;
        _doorUpStartPos = DoorUp.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_startChanging && !_opened)
        {
            if (DoorDown.transform.position.y < _doorDownStartPos.y - MaxRange)
            {
                DoorDown.transform.Translate(Vector3.down * Speed);
                DoorUp.transform.Translate(Vector3.up * Speed);
            }
            else
            {
                _startChanging = false;
            }
            
        }
        else if (_startChanging && _opened)
        {
            if(DoorDown.transform.position.y < _doorDownStartPos.y - MaxRange)
            {
                DoorDown.transform.Translate(Vector3.up * Speed);
                DoorUp.transform.Translate(Vector3.down * Speed);
            }
            else
            {
                _startChanging=false;
            }
        }
    }

    private void ChangeState(object o, int taskId)
    {
        print(taskId);
        _startChanging = !_startChanging;
    }
}
