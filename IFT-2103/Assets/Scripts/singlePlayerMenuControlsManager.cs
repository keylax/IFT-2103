using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Controls;
using UnityEngine.UI;

public class singlePlayerMenuControlsManager : MonoBehaviour
{
    public Toggle PlayerOneToggleArrows;
    public Toggle PlayerOneToggleWASD;
    public Toggle PlayerOneToggleZQSD;

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
}
