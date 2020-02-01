public static class GameData
{
    public static float playerSpeed = 10.0f;
    public static float shipHealth = 1.0f;
    public static bool gravityIsWorking = true;
    public static float repairPointValue = 0.1f;
    public static float destoryedModifier = 0.75f;

    public enum ToolType
    {
        HAMMER,
        WRENCH,
        NONE
    }
}
