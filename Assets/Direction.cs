using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public enum Direction
    {
        up = 1,
        left = 2,
        down = 3,
        right = 4
    }

    public static class DirectionMethods
    {
        public static Direction TurnLeft(this Direction direction)
        {
            if ((int)direction == 4)
            {
                return Direction.up;
            }
            else
            {
                return (Direction)((int)direction + 1);
            }
        }

        public static Direction TurnRight(this Direction direction)
        {
            if ((int)direction == 1)
            {
                return Direction.right;
            }
            else
            {
                return (Direction)((int)direction - 1);
            }
        }

        public static List<int> FrontMoveGroup(this Direction direction)
        {
            switch (direction)
            {
                case Direction.up:
                    return new List<int> { 0, 1 };

                case Direction.down:
                    return new List<int> { 2, 3 };

                case Direction.left:
                    return new List<int> { 0, 2 };

                case Direction.right:
                    return new List<int> { 1, 3 };

                default:
                    throw new InvalidOperationException();
            }
        }


        public static List<int> BackMoveGroup(this Direction direction)
        {
            switch (direction)
            {
                case Direction.down:
                    return new List<int> { 0, 1 };

                case Direction.up:
                    return new List<int> { 2, 3 };

                case Direction.right:
                    return new List<int> { 0, 2 };

                case Direction.left:
                    return new List<int> { 1, 3 };

                default:
                    throw new InvalidOperationException();
            }
        }

        public static Vector3 DirectionToVector(this Direction direction)
        {
            if (direction == Direction.up)
            {
                return new Vector3(0, 1, 0);
            }
            if (direction == Direction.left)
            {
                return new Vector3(-1, 0, 0);
            }
            if (direction == Direction.down)
            {
                return new Vector3(0, -1, 0);
            }
            if (direction == Direction.right)
            {
                return new Vector3(1, 0, 0);
            }
            throw new InvalidOperationException();
        }
    }

}
