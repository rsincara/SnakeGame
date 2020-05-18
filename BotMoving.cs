using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfApp1.GameClasses;

namespace WpfApp1
{
    public class BotMoving
    {
        int width;
        int height;
        int moveSize;
        Food food;
        SnakeElement enemyHead;
        List<SnakeElement> snakeBody;
        List<SnakeElement> snakeBodyEnemy;
        Directions enemyCurrentDirection;

        public BotMoving(Food food, List<SnakeElement> player, List<SnakeElement> enemy,
                         Directions enemyDirection, int width, int height, int moveSize)
        {
            this.width = width;
            this.height = height;
            this.moveSize = moveSize;
            this.food = food;
            snakeBody = player;
            snakeBodyEnemy = enemy;
            enemyHead = enemy[0];
            enemyCurrentDirection = enemyDirection;
        }
        

        public Directions GetNextDirection()
        {
            if (food.point.X < enemyHead.point.X && enemyCurrentDirection != Directions.Right && IsSideFree(Directions.Left)) return Directions.Left;
            if (food.point.Y < enemyHead.point.Y && enemyCurrentDirection != Directions.Down && enemyHead.point.Y - moveSize >= 0 && IsSideFree(Directions.Up)) return Directions.Up;
            if (food.point.X > enemyHead.point.X && enemyCurrentDirection != Directions.Left && IsSideFree(Directions.Right)) return Directions.Right;
            if (food.point.Y > enemyHead.point.Y && enemyCurrentDirection != Directions.Up && IsSideFree(Directions.Down)) return Directions.Down;

            if (food.point.Y > enemyHead.point.Y && enemyCurrentDirection == Directions.Up && enemyHead.point.X - moveSize > 0 && IsSideFree(Directions.Left)) return Directions.Left;
            if (food.point.Y > enemyHead.point.Y && enemyCurrentDirection == Directions.Up && enemyHead.point.X + moveSize < width+moveSize && IsSideFree(Directions.Right)) return Directions.Right;
            if (food.point.Y < enemyHead.point.Y && enemyCurrentDirection == Directions.Down && enemyHead.point.X - moveSize >= 0 && IsSideFree(Directions.Left)) return Directions.Left;
            if (food.point.Y < enemyHead.point.Y && enemyCurrentDirection == Directions.Down && enemyHead.point.X + moveSize < width+moveSize && IsSideFree(Directions.Right)) return Directions.Right;
            if(food.point.X < enemyHead.point.X && enemyCurrentDirection ==  Directions.Right && enemyHead.point.Y + moveSize < height-moveSize && IsSideFree(Directions.Down)) return Directions.Down;
            if(food.point.X < enemyHead.point.X && enemyCurrentDirection ==  Directions.Right && enemyHead.point.Y + moveSize > height - moveSize && IsSideFree(Directions.Up)) return Directions.Up;

            if (enemyCurrentDirection != Directions.Up && IsSideFree(Directions.Down) ) return Directions.Down;
            if (enemyCurrentDirection != Directions.Down && enemyHead.point.Y - moveSize >= 0 && IsSideFree(Directions.Up)) return Directions.Up;
            if ( enemyCurrentDirection != Directions.Right && IsSideFree(Directions.Left) ) return Directions.Left;
            if ( enemyCurrentDirection != Directions.Left && IsSideFree(Directions.Right) ) return Directions.Right;

            if (enemyCurrentDirection != Directions.Left) return Directions.Right;
            if (enemyCurrentDirection != Directions.Right) return Directions.Left;
            if (enemyCurrentDirection != Directions.Up) return Directions.Down;
            if (enemyCurrentDirection != Directions.Down) return Directions.Up;
            return Directions.Stay;
        }

        bool IsSideFree(Directions direction)
        {
            switch (direction)
            {
                case Directions.Right:
                    return snakeBodyEnemy.Skip(1).All(x => x.point != new Point(enemyHead.point.X + moveSize, enemyHead.point.Y)) &&
                           snakeBody.All(x => x.point != new Point(enemyHead.point.X + moveSize, enemyHead.point.Y) ) ;
                case Directions.Up:
                    return snakeBodyEnemy.Skip(1).All(x => x.point != new Point(enemyHead.point.X, enemyHead.point.Y-moveSize)) &&
                           snakeBody.All(x => x.point != new Point(enemyHead.point.X, enemyHead.point.Y-moveSize) ) ;
                case Directions.Left:
                    return snakeBodyEnemy.Skip(1).All(x => x.point != new Point(enemyHead.point.X - moveSize, enemyHead.point.Y)) &&
                           snakeBody.All(x => x.point != new Point(enemyHead.point.X - moveSize, enemyHead.point.Y) ) ;
                case Directions.Down:
                    return snakeBodyEnemy.Skip(1).All(x => x.point != new Point(enemyHead.point.X, enemyHead.point.Y+moveSize)) &&
                           snakeBody.All(x => x.point != new Point(enemyHead.point.X, enemyHead.point.Y+moveSize) ) ;
            }
            return false;
        }
    }
}
