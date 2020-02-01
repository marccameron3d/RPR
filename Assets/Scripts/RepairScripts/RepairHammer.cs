using UnityEngine;

public class RepairHammer : RepairPoint
{
    protected override void SetUp()
    {
        base.SetUp();
        neededToolType = GameData.ToolType.HAMMER;
    }
}
