

public abstract class Command
{
    public abstract void Execute(Actor obj);
}

public class MoveLeft : Command
{
    public override void Execute(Actor actor)
    {
        actor.MoveLeft();
    }
}

public class MoveRight : Command
{
    public override void Execute(Actor actor)
    {
        actor.MoveRight();
    }
}

public class Jump : Command
{
    public override void Execute(Actor actor)
    {
        actor.Jump();
    }
}

