public class RepairHammer : RepairPoint
{
    protected override void SetUp()
    {
        base.SetUp();
        neededToolType = GameData.ToolType.HAMMER;
    }

    public override bool DoingAction()
    {
        return false;
    }
}
