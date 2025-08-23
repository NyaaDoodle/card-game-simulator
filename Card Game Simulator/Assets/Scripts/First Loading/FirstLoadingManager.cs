using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadingManager : MonoBehaviour
{
    [SerializeField] private string postLoadSceneName;
    void Start()
    {
        SceneManager.LoadScene(postLoadSceneName);
    }
}
