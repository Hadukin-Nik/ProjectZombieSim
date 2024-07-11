using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class ZombiesWait : IChainPart
{
    private IZombiesChainHandler _handler;

    private float _coolDown;
    private float _time;
    private float _elapsed = 1f;
    public static event Action<float> OnTimerTick;

    public ZombiesWait(IZombiesChainHandler handler, float coolDown)
    {
        _handler = handler;
        _coolDown = coolDown;
        _time = coolDown;
        OnTimerTick?.Invoke(_elapsed);
    }

    public void Update(double delta)
    {
        _time -= (float) delta;
        _elapsed += (float) delta;

        if(_elapsed >= 1f)
        {
            OnTimerTick?.Invoke(_time);
            _elapsed = 0f;
        }

        if (_time < 0)
        {
            _time = _coolDown;
            var saveables = new List<IHordAction>();
            var rootObjs = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var root in rootObjs)
            {
                // Pass in "true" to include inactive and disabled children
                IHordAction action = root.GetComponent<IHordAction>();
                if (action != null)
                {
                    saveables.Add(action);
                }
            }
            foreach (var obj in saveables)
            {
                obj.OnStart();
            }
            _handler.MoveToNext();
        }
    }
    
}
