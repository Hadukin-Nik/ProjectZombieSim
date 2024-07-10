using UnityEngine;

public class SoldierTargetLooker : IChainPart
{
    private ISoldierChainHandler _handler;
    private Transform _body;

    private int _radius;

    public SoldierTargetLooker(ISoldierChainHandler handler, Transform body, int radius)
    {
        _handler = handler;
        _body = body;
        _radius = radius;
    }

    public void Update(double delta)
    {
        //Check for Zombies are near
        RaycastHit[] gameObjects = Physics.SphereCastAll(_body.position, _radius, _body.forward, 0.1f);
        foreach (RaycastHit gameObject in gameObjects)
        {
            if (gameObject.transform.tag == "Foe" && gameObject.transform.GetComponent<IDestroyable>() != null)
            {
                _handler.SetTarget(gameObject.transform.gameObject);
                _handler.MoveToNext();
                return;
            }
        }

        _handler.MoveToNext();
        return;
    }
}
