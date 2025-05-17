using System;
using UnityEngine;

public class GameTemplateDataContainer : MonoBehaviour
{
    public GameTemplate CurrentGameTemplate { get; private set; } = null;
    public event Action<GameTemplate> NewGameTemplateLoaded;

    public void ChangeCurrentGameTemplate(GameTemplate newGameTemplate)
    {
        CurrentGameTemplate = newGameTemplate;
        OnNewGameTemplateLoaded();
    }

    protected virtual void OnNewGameTemplateLoaded()
    {
        NewGameTemplateLoaded?.Invoke(CurrentGameTemplate);
    }
}
