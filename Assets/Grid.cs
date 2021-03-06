﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;



namespace Assets
{
    public class Grid
    {
        public static readonly int GridUnit = 16;
        public static readonly int GridXOffset = 7;
        public static readonly Vector3 GridOffset = new Vector3(16, 16, 0);

        public static readonly int gridWidth = 16;
        public static readonly int gridHeight = 30;
        public int[,] Cells;

        public Grid()
        {
            Cells = new int[gridWidth, gridHeight];
            for(int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x ++)
                {
                    if(x == 3 && y == 1)
                    {
                        Cells[x,y] = 1;
                    }

                    if (x == 7 && y == 10)
                    {
                        Cells[x, y] = 1;
                    }

                    if (x == 4 && y == 12)
                    {
                        Cells[x, y] = 1;
                    }

                    if (x == 5 && y == 4 )
                    {
                        Cells[x, y] = 1;
                    }

                    if (x == 8 &&  y == 20)
                    {
                        Cells[x, y] = 1;
                    }

                    if (x == 4 && y == 10)
                    {
                        Cells[x, y] = 1;
                    }
                }
            }
        }



        public bool IsInGrid(Vector3 currentPos)
        {           
            int xPosOnGrid = (int)GridTools.GridPosition(currentPos).x;
            int yPosOnGrid = (int)GridTools.GridPosition(currentPos).y;

            if(xPosOnGrid > gridWidth || xPosOnGrid < 0)
            {
                return false;
            }
            if (yPosOnGrid > gridHeight || yPosOnGrid < 0)
            {
                return false;
            }
            return true;
        }

    }
}