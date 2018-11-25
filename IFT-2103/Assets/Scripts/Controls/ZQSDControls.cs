using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    class ZQSDControls : ControlScheme
    {
        public float getHorizontalAxis()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                return -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                return 1f;
            }
            return 0f;
        }

        public float getVerticalAxis()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                return 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                return -1f;
            }
            return 0f;
        }
    }
}
