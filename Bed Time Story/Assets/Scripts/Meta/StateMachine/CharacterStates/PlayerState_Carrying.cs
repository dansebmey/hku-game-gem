public class PlayerState_Carrying : PlayerState
{
    public override void OnEnter()
    {
        character.moveSpeed = GameConstants.moveSpeedWhenCarrying;
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}