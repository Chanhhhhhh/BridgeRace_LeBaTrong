using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultUI : UICanvas
{
    public void BackMainMenu()
    {
        CloseDirectly();
        LevelManager.Instance.CloseLevel();
        GameManager.ChangeState(GameState.MainMenu);
    }

    public void OnRetry()
    {
        CloseDirectly();
        LevelManager.Instance.CloseLevel();
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Instance.CreateLevel(LevelManager.Instance.level);
    }
}
