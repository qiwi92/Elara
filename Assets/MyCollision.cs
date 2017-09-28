using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public enum CollisionType
    {
        None = 0,
        GridEdge = 1,
        Stone = 2
    }

    public class MyCollision
    {
        public static CollisionType Check(GameObject cube, Direction direction,Grid grid)
        {
            int xPos = (int)GridTools.GridPosition(cube.transform.position).x;
            int yPos = (int)GridTools.GridPosition(cube.transform.position).y;
            int nextX = (int)direction.DirectionToVector().x;
            int nextY = (int)direction.DirectionToVector().y;

            if (xPos + nextX < Grid.gridWidth && xPos + nextX >= 0 && yPos + nextY < Grid.gridHeight && yPos + nextY >= 0)
            {
                for (int y = 0; y < Grid.gridHeight; y++)
                {
                    for (int x = 0; x < Grid.gridWidth; x++)
                    {
                        if (grid.Cells[xPos + nextX, yPos + nextY] == 1)
                        {
                            return CollisionType.Stone;
                        }
                    }
                }
            }
            else
            {
                return CollisionType.GridEdge;
            }
            return CollisionType.None;
        }
    }

}