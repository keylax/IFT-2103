using UnityEngine;

namespace Assets.Scripts
{
    class WASDControls : ControlScheme
    {
        public float getHorizontalAxis()
        {
            if (Input.GetKey(KeyCode.A))
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
            if (Input.GetKey(KeyCode.W))
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
