public class PlayerState_Default : PlayerState
{
    public override void OnEnter()
    {
        character.moveSpeed = GameConstants.defaultMoveSpeed;
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}