using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadinScreen : ScreenElement
{
    public override void Show()
    {
        base.Show();

        Invoke(nameof(changeScreen), Random.Range(1, 2));
    }

    private void changeScreen()
    {
        GameStateHandler.StateHandler.ChangeState(GameStates.Start);
    }
}
