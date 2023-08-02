

public class Level 
{
    public int levelNo;
    public LevelStatus levelStatus;
    public int enemyCount = 10;
    private int increaseEnemy = 5;

    public Level(int _levelNo)
    {
        levelStatus = LevelStatus.Level_Locked;
        levelNo = _levelNo + 1;
        enemyCount += _levelNo + increaseEnemy; 
    }

}

public enum LevelStatus
{
    Level_Locked,
    Level_Unlocked,
    Level_Completed,
};