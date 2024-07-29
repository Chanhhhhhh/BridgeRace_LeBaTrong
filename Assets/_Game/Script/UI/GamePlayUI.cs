using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : UICanvas
{
    public void PauseGame()
    {
        CloseDirectly();
        GameManager.ChangeState(GameState.Pause);
        Time.timeScale = 0;
    }
}
