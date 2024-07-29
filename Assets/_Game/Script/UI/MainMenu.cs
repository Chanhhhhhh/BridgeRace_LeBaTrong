using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas 
{
    public void OpenLevelUI()
    {
        CloseDirectly();
        GameManager.ChangeState(GameState.Level);
    }
}
