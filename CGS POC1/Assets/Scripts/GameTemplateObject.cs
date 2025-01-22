using System.Collections.Generic;
using UnityEngine;

public class GameTemplateObject : MonoBehaviour
{
    private readonly GameTemplateInfo gameTemplateInfo = new GameTemplateInfo();
    [SerializeField] private int testLength;
    [SerializeField] private int testWidth;
    [SerializeField] private Vector2 firstDeckLocation;
    [SerializeField] private Vector2 secondDeckLocation;

    public int TableLength => gameTemplateInfo.TableInfo.Length;

    public int TableWidth => gameTemplateInfo.TableInfo.Width;

    public List<DeckInfo> DeckInfoList => gameTemplateInfo.DeckInfoList;

    public CardBank CardBank => gameTemplateInfo.CardBank;

    private void Awake()
    {
        //createTestGameTemplateObject();
    }

    private void createTestGameTemplateObject()
    {
        gameTemplateInfo.TableInfo.Length = testLength;
        gameTemplateInfo.TableInfo.Width = testWidth;
        gameTemplateInfo.CardBank = getTestCardBank();
        gameTemplateInfo.DeckInfoList = getTestDeckInfoList();
    }

    private CardBank getTestCardBank()
    {
        const string backSideFileName = "blue_back";
        CardBank testCardBank = new CardBank();

        testCardBank.AddCardInfo(new CardInfo("2C", "2C", backSideFileName));

        return testCardBank;
    }

    private List<DeckInfo> getTestDeckInfoList()
    {
        List<DeckInfo> testDeckInfoList = new List<DeckInfo>();

        testDeckInfoList.Add(getTestDeckInfo());
        testDeckInfoList.Add(getTestDeckInfo());
        testDeckInfoList[0].Location = firstDeckLocation;
        testDeckInfoList[1].Location = secondDeckLocation;

        return testDeckInfoList;
    }

    private DeckInfo getTestDeckInfo()
    {
        List<string> testCardIdList = new List<string>();
        testCardIdList.Add("2C");
        DeckInfo testDeckInfo = new DeckInfo();
        testDeckInfo.CardIdList = testCardIdList;
        return testDeckInfo;
    }
}
