using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public enum Direction
    {
        up = 0,
        left = 1,
        down = 2,
        right = 3
    }

    public static class DirectionMethods
    {
        public static Direction TurnLeft(this Direction direction)
        {
            return (Direction)(((int)direction + 1) % 4); 
        }

        public static Direction TurnRight(this Direction direction)
        {
            return (Direction)(((int)direction + 3) % 4);
        }

        public static Direction Opposite(this Direction direction)
        {
            return (Direction)(((int)direction + 2) % 4);
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
