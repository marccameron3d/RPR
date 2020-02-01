public static class GameData {
    public static float playerSpeed = 10.0f;
    public static float shipHealth = 1.0f;
    public static bool gravityIsWorking = true;
    public static float repairPointValue = 0.1f;
    public static float destoryedModifier = 0.75f;
    public static bool player1Alive = true;
    public static bool player2Alive = true;
    public static bool player3Alive = true;
    public static float shipSpeed = 1.0f;
    public static float shipSpeedModifier = 1.0f;

    public enum ToolType {
        SCREWDRIVER,
        HAMMER,
        SAW,
        DRILL,
        WRENCH,
        NONE
    }

    public enum PlayerNumber {
        NULL,
        PLAYER_1,
        PLAYER_2,
        PLAYER_3
    }

}