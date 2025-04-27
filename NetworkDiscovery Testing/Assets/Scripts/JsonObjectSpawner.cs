using Mirror;
using UnityEngine;

[RequireComponent(typeof(JsonFetchClient))]
public class JsonObjectSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject dummyDataHolderPrefab;
    [SerializeField] private int id;
    [SyncVar] private NetworkIdentity dummyDataHolderNetworkId;
    private JsonFetchClient jsonFetchClient;

    public void Awake()
    {
        jsonFetchClient = GetComponent<JsonFetchClient>();
        jsonFetchClient.DataFetched += spawnAndConfigureDummyDataHolder;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        jsonFetchClient.GetData(id);
    }

    private void spawnAndConfigureDummyDataHolder(DummyData fetchedDummyData)
    {
        GameObject dummyDataHolderObject = Instantiate(dummyDataHolderPrefab);
        configureDummyDataHolder(dummyDataHolderObject, fetchedDummyData);
        NetworkServer.Spawn(dummyDataHolderObject);
        dummyDataHolderNetworkId = dummyDataHolderObject.GetComponent<NetworkIdentity>();
    }

    private void configureDummyDataHolder(GameObject dummyDataObject, DummyData dummyDataToSet)
    {
        DummyDataHolder dummyDataHolder = dummyDataObject.GetComponent<DummyDataHolder>();
        dummyDataHolder.DummyData = dummyDataToSet;
    }
}
