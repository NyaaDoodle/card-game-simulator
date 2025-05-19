using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceState : MonoBehaviour
{
    // Game Template related properties
    public GameTemplate GameTemplate { get; private set; } = null;
    public bool IsGameTemplateLoaded => GameTemplate != null;

    // Objects in use by the game instance
    public GameObject TableObject { get; set; } = null;

    public List<GameObject> CardObjects { get; set; } = new List<GameObject>();
    public List<GameObject> CardDeckObjects { get; set; } = new List<GameObject>();
    public List<GameObject> CardSpaceObjects { get; set; } = new List<GameObject>();
    public List<GameObject> PlayerHandObjects { get; set; } = new List<GameObject>();

    public event Action<GameTemplate> NewGameTemplateLoaded;

    public void ChangeGameTemplate(GameTemplate newGameTemplate)
    {
        GameTemplate = newGameTemplate;
        OnNewGameTemplateLoaded();
    }

    protected virtual void OnNewGameTemplateLoaded()
    {
        NewGameTemplateLoaded?.Invoke(GameTemplate);
    }
}
