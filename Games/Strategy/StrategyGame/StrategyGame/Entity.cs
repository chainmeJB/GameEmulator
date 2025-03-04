using System;

namespace StrategyGame
{
    public class Entity
    {
        public int Gold { get; private set; }
        public int Army { get; private set; }

        public Entity(int gold, int army)
        {
            Gold = gold;
            Army = army;
        }

        public void SetGold(int value) => Gold = value;

        public void SetArmy(int value) => Army = value;

        public void HireSoldiers()
        {
            if (Gold >= 20)
            {
                Gold -= 20;
                Army += 3;
            }
        }

        public void Attack(Entity opponent, Random rand)
        {
            int attack = rand.Next(1, Army + 1);
            opponent.Army -= attack;
        }

        public void Defend()
        {
            Army += 2;
        }

        public bool IsDefeated() => Army <= 0;
    }
}
