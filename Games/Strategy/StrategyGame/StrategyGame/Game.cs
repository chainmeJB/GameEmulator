using System;

namespace StrategyGame
{
    public class Game
    {
        public Entity Player { get; private set; }
        public Entity Enemy { get; private set; }
        public Random rand = new Random();

        public Game(int playerGold, int playerArmy, int enemyGold, int enemyArmy)
        {
            Player = new Entity(playerGold, playerArmy);
            Enemy = new Entity(enemyGold, enemyArmy);
        }

        public bool IsGameOver() => Player.IsDefeated() || Enemy.IsDefeated();
        public bool PlayerWon() => !Player.IsDefeated();

        public void EnemyTurn()
        {
            int choice = rand.Next(1, 4);
            if (choice == 1) Enemy.HireSoldiers();
            else if (choice == 2) Enemy.Attack(Player, rand);
            else Enemy.Defend();
        }
    }
}
