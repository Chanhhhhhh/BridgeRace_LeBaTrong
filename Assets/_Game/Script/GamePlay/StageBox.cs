using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBox : MonoBehaviour
{
    [SerializeField] private Stage stage;
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (!stage.colorList.Contains(character.colorType))
        {
            stage.OnInitBrick(character.colorType);
        }
        if (character.stage!=null)
        {
            character.stage.colorList.Remove(character.colorType);

        }
        character.stage = this.stage;
        stage.colorList.Add(character.colorType);

        if (character.CompareTag(Constants.TAG_BOT))
        {
            Bot bot = other.GetComponent<Bot>();
            bot.changState(bot.seekState);
        }

    }
}
