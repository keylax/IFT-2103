using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class WASDControls : ControlScheme
    {
        public float getHorizontalAxis()
        {
            return Input.GetAxis("HorizontalWASD");
        }

        public float getVerticalAxis()
        {
            return Input.GetAxis("VerticalWASD");
        }

        public bool getMenuButtonDown()
        {
            return Input.GetButtonDown("MenuWASD");
        }
    }
}
