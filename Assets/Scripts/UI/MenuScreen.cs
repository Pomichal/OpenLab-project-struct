using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : ScreenBase
{

    public void StartButtonClicked()
    {
        App.gameManager.LoadScene("Level1", true, new ShowScreenCommand<InGameScreen>());
        Hide();
    }
}
