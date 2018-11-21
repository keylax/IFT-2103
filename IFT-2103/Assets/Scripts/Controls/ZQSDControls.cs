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
            return Input.GetAxis("HorizontalZQSD");
        }

        public float getVerticalAxis()
        {
            return Input.GetAxis("VerticalZQSD");
        }

        public bool getMenuButtonDown()
        {
            return Input.GetButtonDown("MenuZQSD");
        }
    }
}
