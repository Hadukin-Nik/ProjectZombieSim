using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombiesWait : IChainPart
{
    private IZombiesChainHandler _handler;

    private float _coolDown;
    private float _time;

    public ZombiesWait(IZombiesChainHandler handler, float coolDown)
    {
        _handler = handler;
        _coolDown = coolDown;
        _time = coolDown;
    }

    public void Update(double delta)
    {
        if(_time == _coolDown) { }
        _time -= (float) delta;

        if (_time < 0)
        {
            _time = _coolDown;

            var saveables = new List<IHordAction>();
            var rootObjs = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var root in rootObjs)
            {
                // Pass in "true" to include inactive and disabled children
                IHordAction action = root.GetComponent<IHordAction>();
                if(action != null)
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
