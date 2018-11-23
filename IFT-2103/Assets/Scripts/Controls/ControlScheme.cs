using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface ControlScheme
    {
        float getHorizontalAxis();
        float getVerticalAxis();
        bool getMenuButtonDown();
    }
}
