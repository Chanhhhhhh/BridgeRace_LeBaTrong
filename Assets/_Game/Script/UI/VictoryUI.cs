using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryUI : ResultUI
{  
    public void OnNext()
    {
        CloseDirectly();
        LevelManager.Instance.CloseLevel();
        LevelManager.Instance.CreateLevel(LevelManager.Instance.level + 1);
        GameManager.ChangeState(GameState.GamePlay);
    }
}
