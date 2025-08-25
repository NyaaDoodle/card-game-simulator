using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceSelectionEntity : MonoBehaviour
{
    [SerializeField] private Button selectionButton;
    [SerializeField] private TMP_Text spaceName;

    public void Setup(SpaceData spaceData, Action<SpaceData> onSelectAction)
    {
        setupSelectionButton(spaceData, onSelectAction);
        setupDeckName(spaceData);
    }

    private void OnDestroy()
    {
        removeSelectionButtonListeners();
    }

    private void setupSelectionButton(SpaceData spaceData, Action<SpaceData> onSelectAction)
    {
        selectionButton.onClick.AddListener(() => onSelectAction(spaceData));
    }

    private void removeSelectionButtonListeners()
    {
        selectionButton.onClick.RemoveAllListeners();
    }

    private void setupDeckName(SpaceData spaceData)
    {
        spaceName.text = spaceData.Name;
    }
}
