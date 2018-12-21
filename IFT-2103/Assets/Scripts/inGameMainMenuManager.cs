using UnityEngine;

namespace Invector.CharacterController
{
    public class inGameMainMenuManager : MonoBehaviour
    {

        public GameObject mainMenu;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MenuInput();
        }


        private void MenuInput()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                SetUpMainMenu();
            }
        }

        public void ResumeGame()
        {
            GameObject game = GameObject.FindGameObjectWithTag("game");
            game.SetActive(true);
            mainMenu.SetActive(false);
        }

        public void SetUpMainMenu()
        {
            mainMenu.SetActive(true);
            Cursor.visible = true;

            GameObject game = GameObject.FindGameObjectWithTag("game");
            game.SetActive(false);  
        }
    }
}
