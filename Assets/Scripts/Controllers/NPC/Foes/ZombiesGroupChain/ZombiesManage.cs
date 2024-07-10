public class ZombiesManage : IChainPart
{
    IZombiesChainHandler _handler;

    public ZombiesManage(IZombiesChainHandler handler)
    {
        _handler = handler;
    } 

    public void Update(double delta)
    {
        if (_handler.GetZombies().Count - _handler.GetKilled() > 0)
        {
            foreach(var z in _handler.GetZombies()) {
                if(z.Value)
                {
                    z.Key.Update(delta);
                }
            }
        } else
        {
            _handler.MoveToNext();
        }
    }
}
