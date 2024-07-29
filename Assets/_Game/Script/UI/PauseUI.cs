using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : UICanvas
{
    public void BackMainMenu()
    {
        CloseDirectly();
        LevelManager.Instance.CloseLevel();
        GameManager.ChangeState(GameState.MainMenu);
    }

    public void Continues()
    {
        CloseDirectly();
        GameManager.ChangeState(GameState.GamePlay);
        Time.timeScale = 1f;
    }
}
