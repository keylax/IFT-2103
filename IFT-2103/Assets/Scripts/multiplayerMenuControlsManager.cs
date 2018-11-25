using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Controls;
using UnityEngine.UI;

public class multiplayerMenuControlsManager : MonoBehaviour {


    public Toggle PlayerOneToggleArrows;
    public Toggle PlayerOneToggleWASD;
    public Toggle PlayerOneToggleZQSD;
    public Toggle PlayerTwoToggleArrows;
    public Toggle PlayerTwoToggleWASD;
    public Toggle PlayerTwoToggleZQSD;


    public void setControlsPlayerOneArrows()
    {
        if (PlayerOneToggleArrows.isOn)
        {
            GameParameters.setPlayerOneControls(new ArrowsControls());
            if (PlayerTwoToggleArrows.isOn)
            {
                PlayerTwoToggleWASD.isOn = true;
            }
        }
    }

    public void setControlsPlayerOneWASD()
    {
        if (PlayerOneToggleWASD.isOn)
        {
            GameParameters.setPlayerOneControls(new WASDControls());
            if (PlayerTwoToggleWASD.isOn)
            {
                PlayerTwoToggleZQSD.isOn = true;
            }
        }
    }

    public void setControlsPlayerOneZQSD()
    {
        if (PlayerOneToggleZQSD.isOn)
        {
            GameParameters.setPlayerOneControls(new ZQSDControls());
            if (PlayerTwoToggleZQSD.isOn)
            {
                PlayerTwoToggleArrows.isOn = true;
            }
        }
    }

    public void setControlsPlayerTwoArrows()
    {
        if (PlayerTwoToggleArrows.isOn)
        {
            GameParameters.setPlayerTwoControls(new ArrowsControls());
            if (PlayerOneToggleArrows.isOn)
            {
                PlayerOneToggleWASD.isOn = true;
            }
        }

    }

    public void setControlsPlayerTwoWASD()
    {
        if (PlayerTwoToggleWASD.isOn)
        {
            GameParameters.setPlayerTwoControls(new WASDControls());
            if (PlayerOneToggleWASD.isOn)
            {
                PlayerOneToggleZQSD.isOn = true;
            }
        }

    }

    public void setControlsPlayerTwoZQSD()
    {
        if (PlayerTwoToggleZQSD.isOn)
        {
            GameParameters.setPlayerTwoControls(new ZQSDControls());
            if (PlayerOneToggleZQSD.isOn)
            {
                PlayerOneToggleArrows.isOn = true;
            }
        }
    }


}
