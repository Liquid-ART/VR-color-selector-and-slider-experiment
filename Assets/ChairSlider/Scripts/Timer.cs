public class Timer
{

    bool _timerIsRunning;

    public bool TimerIsRunning
    {
        get
        {
            return _timerIsRunning;
        }
    }

    float _timeRemain;

    public float TimeRemain
    {
        get
        {
            return _timeRemain;
        }
    }

    float _time;

    public float Time
    {
        get
        {
            return _time;
        }
    }

    public Timer()

    {
        UpdateCaller.OnUpdate += Update;
    }

    ~Timer()
    {
        UpdateCaller.OnUpdate -= Update;
    }


    //functions------------------------------------------
    void Update()
    {
        if (_timerIsRunning)
            _timeRemain -= UnityEngine.Time.deltaTime;
    }


    public void SetTimer(float _time)
    {
        this._time = _time;
        _timeRemain = _time;
        _timerIsRunning = true;
    }

    public void RestartTimer()
    {
        _timeRemain = _time;
        _timerIsRunning = true;
    }
    
    public void ResetTimer()
    {
        _timerIsRunning = false;
    }


    public bool OnceTimerIsComplete() // returns true once
    {  

        if (_timeRemain > 0 && _timerIsRunning == true)
        {
            return false;
        }
        else if(_timerIsRunning == true)
        {
            _timerIsRunning = false;
            return true;
        }
        else
        {
            return false;
        }
 
    }

    public bool TimerComplete() // returns true until manually resetted
    {

        if (_timeRemain > 0 && _timerIsRunning == true)
        {
            return false;
        }
        else
        {
            _timerIsRunning = false;
            return true;
        }

    }
}
