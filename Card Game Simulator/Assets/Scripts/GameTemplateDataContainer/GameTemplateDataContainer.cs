using UnityEngine;

public class GameTemplateDataContainer : MonoBehaviour
{
    private GameTemplate currentGameTemplate;

    public string Name => currentGameTemplate.Name;

    public TableData TableData => currentGameTemplate.TableData;

    public CardPool CardPool => currentGameTemplate.CardPool;

    void Awake()
    {
        currentGameTemplate = null;
    }

    public void ChangeCurrentGameTemplate(GameTemplate newGameTemplate)
    {
        currentGameTemplate = newGameTemplate;
    }
}
