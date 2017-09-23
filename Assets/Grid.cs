﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public static class Grid
    {
        public static Vector3 GridPosition(this Vector3 currentPosition)
        {
            float xBorderWidth = 7f;
            float xPos = currentPosition.x;
            float yPos = currentPosition.y;
            
            var xGridPos = Mathf.Round((xPos - xBorderWidth) / 16f);
            var yGridPos = Mathf.Round(yPos / 16f);
            return new Vector3(xGridPos, yGridPos, 0f);


        }

        public static Vector3 QuantizePosition(this Vector3 currentPosition)
        {
            int xGridPos = (int)currentPosition.GridPosition().x;
            int yGridPos = (int)currentPosition.GridPosition().y;

            int xPosQuantized = 7 + 16 * xGridPos;
            var yPosQuantized = 16 * yGridPos;
            return new Vector3(xPosQuantized, yPosQuantized, 0f);
        }


    }
}