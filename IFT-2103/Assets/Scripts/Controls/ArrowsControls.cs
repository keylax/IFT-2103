using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    class ArrowsControls : ControlScheme
    {
        public float getHorizontalAxis()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return -1f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                return 1f;
            }
            return 0f;
        }

        public float getVerticalAxis()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                return 1f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                return -1f;
            }
            return 0f;
        }

        public bool getMenuButtonDown()
        {
            return Input.GetButtonDown("MenuWASD");
        }
    }
}
