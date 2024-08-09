using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI Text_Level;
    public override void Setup()
    {
        base.Setup();
        Text_Level.text = "Level " + (LevelManager.Instance.level + 1) ;
    }
    public void PauseGame()
    {
        CloseDirectly();
        GameManager.ChangeState(GameState.Pause);
        Time.timeScale = 0;
    }
}
