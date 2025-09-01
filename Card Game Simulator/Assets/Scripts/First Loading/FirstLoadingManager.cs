using Extensions.Unity.ImageLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadingManager : MonoBehaviour
{
    [SerializeField] private string postLoadSceneName;

    void Awake()
    {
        initializeImageLoader();
    }

    void Update()
    {
        if (SimulatorNetworkManager.singleton.IsReady && DataDirectoryManager.Instance.IsReady
                                                      && PrefabReferences.Instance.IsReady
                                                      && GameTemplateEditor.Instance.IsReady
                                                      && ContentServerAPIManager.Instance.IsReady
                                                      && InGameDebugWindowManager.Instance.IsReady
                                                      && PlayerPrefsManager.Instance.IsReady
                                                      && SimulatorGlobalData.Instance.IsReady
                                                      && LocalContentServer.Instance.IsReady)
        {
            loadNextScene();
        }
    }

    private void initializeImageLoader()
    {
        ImageLoader.Init();
    }

    private void loadNextScene()
    {
        SceneManager.LoadScene(postLoadSceneName);
    }
}
