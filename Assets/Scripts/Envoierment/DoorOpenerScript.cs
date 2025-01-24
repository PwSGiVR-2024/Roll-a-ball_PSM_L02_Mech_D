using UnityEngine;

public class DoorOpenerScript : MonoBehaviour
{

    public float Speed = 1f;
    public float MaxRange = 11f;
    public GameObject DoorDown;
    public GameObject DoorUp;

    public int OpenTaskId = 6;
    public int CloseTaskId = 7;

    /// when player enters boss area and reaches the checkpoint
    /// and it is fals, it closes the doors after 5s so player doesn't escape
    private bool _opened = false; 
    private bool _startChanging = false;
    private Vector3 _doorDownStartPos;

    void Start()
    {
        TaskScript_Scene3.e_TaskComplete += ChangeState;
        _doorDownStartPos = DoorDown.transform.position;
    }

    void Update()
    {
        if(_startChanging && !_opened)
        {
            if (DoorDown.transform.position.y > _doorDownStartPos.y - MaxRange)
            {
                DoorDown.transform.Translate(Vector3.down * Speed);
                DoorUp.transform.Translate(Vector3.up * Speed);
            }
            else
            {
                _startChanging = false;
                _opened = true;
            }
            
        }
        else if (_startChanging && _opened)
        {
            if(DoorDown.transform.position.y < _doorDownStartPos.y + MaxRange)
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

    public void ChangeState(object o, int taskId)
    {
        if (taskId == OpenTaskId || taskId == CloseTaskId)
        {
            _startChanging = !_startChanging;
        }
    }
}
