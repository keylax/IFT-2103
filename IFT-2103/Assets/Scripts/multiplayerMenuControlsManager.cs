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
        GameParameters.setPlayerOneControls(new ArrowsControls());
    }

    public void setControlsPlayerOneWASD()
    {
        GameParameters.setPlayerOneControls(new WASDControls());
    }

    public void setControlsPlayerOneZQSD()
    {
        GameParameters.setPlayerOneControls(new ZQSDControls());
    }

    public void setControlsPlayerTwoArrows()
    {
        GameParameters.setPlayerTwoControls(new ArrowsControls());
    }

    public void setControlsPlayerTwoWASD()
    {
        GameParameters.setPlayerTwoControls(new WASDControls());
    }

    public void setControlsPlayerTwoZQSD()
    {
        GameParameters.setPlayerTwoControls(new ZQSDControls());
    }


}
