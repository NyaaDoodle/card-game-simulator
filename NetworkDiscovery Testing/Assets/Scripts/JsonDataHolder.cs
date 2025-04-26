using UnityEngine;

public class JsonDataHolder : MonoBehaviour
{
    [SerializeField] private GameTemplateFetchClient gameTemplateFetchClient;
    private DummyTemplateData dummyTemplateData;

    public int Id => dummyTemplateData.Id;
    public string Info => dummyTemplateData.Info;

    public void Start()
    {
        gameTemplateFetchClient.GameTemplateFetched += gameTemplateFetchClient_OnGameTemplateFetched;
    }

    private void gameTemplateFetchClient_OnGameTemplateFetched(DummyTemplateData fetchedTemplateData)
    {
        dummyTemplateData = fetchedTemplateData;
    }
}
