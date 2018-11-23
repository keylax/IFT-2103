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
            return Input.GetAxis("HorizontalArrows");
        }

        public float getVerticalAxis()
        {
            return Input.GetAxis("VerticalArrows");
        }

        public bool getMenuButtonDown()
        {
            return Input.GetButtonDown("MenuWASD");
        }
    }
}
