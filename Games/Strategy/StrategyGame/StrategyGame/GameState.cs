
namespace StrategyGame
{
    public class GameState
    {
        public int PlayerGold { get; private set; }
        public int PlayerArmy { get; private set; }
        public int EnemyGold { get; private set; }
        public int EnemyArmy { get; private set; }

        public GameState(int playerGold, int playerArmy, int enemyGold, int enemyArmy)
        {
            PlayerGold = playerGold;
            PlayerArmy = playerArmy;
            EnemyGold = enemyGold;
            EnemyArmy = enemyArmy;
        }
    }
}
