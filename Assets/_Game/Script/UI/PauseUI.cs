using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : UICanvas
{
    public void BackMainMenu()
    {
        Time.timeScale = 1f;
        CloseDirectly();
        LevelManager.Instance.CloseLevel();
        GameManager.ChangeState(GameState.MainMenu);

    }

    public void Continues()
    {
        Time.timeScale = 1f;
        CloseDirectly();
        GameManager.ChangeState(GameState.GamePlay);

    }
}
