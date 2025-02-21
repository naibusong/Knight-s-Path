

public abstract class BaseState
{
    protected Enemy currentEnemy;
    public abstract void OnEnter(Enemy enemy);
    public abstract void LogicUpdate();//Âß¼­
    public abstract void PhysicsUpdate();//ÎïÀí
    public abstract void OnExit();
}
