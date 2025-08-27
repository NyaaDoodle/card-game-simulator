using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceSelectionGrid : MonoBehaviour
{
    [SerializeField] private RectTransform contentContainer;
    [SerializeField] private Button newSpaceButton;

    private readonly List<SpaceSelectionEntity> selectionEntities = new List<SpaceSelectionEntity>();

    public void Show(IEnumerable<SpaceData> spacesData, Action<SpaceData> onSelectSpace, Action onSelectAddButton = null)
    {
        gameObject.SetActive(true);
        setupAddButton(onSelectAddButton);
        spawnSpaceSelectionEntities(spacesData, onSelectSpace);
    }

    public void Hide()
    {
        despawnSpaceSelectionEntities();
        unsetAddButton();
        gameObject.SetActive(false);
    }

    private void setupAddButton(Action onSelectAddButton)
    {
        bool showAddButton = onSelectAddButton != null;
        newSpaceButton.gameObject.SetActive(showAddButton);
        newSpaceButton.onClick.AddListener(() => onSelectAddButton?.Invoke());
    }

    private void unsetAddButton()
    {
        newSpaceButton.onClick.RemoveAllListeners();
    }

    private void spawnSpaceSelectionEntities(IEnumerable<SpaceData> spacesData, Action<SpaceData> onSelectSpace)
    {
        foreach (SpaceData spaceData in spacesData)
        {
            spawnSpaceSelectionEntity(spaceData, onSelectSpace);    
        }
        setAddButtonAsLastContentSibling();
    }

    private void despawnSpaceSelectionEntities()
    {
        foreach (SpaceSelectionEntity selectionEntity in selectionEntities)
        {
            Destroy(selectionEntity.gameObject);
        }
        selectionEntities.Clear();
    }

    private void spawnSpaceSelectionEntity(SpaceData spaceData, Action<SpaceData> onSelectSpace)
    {
        SpaceSelectionEntity spaceSelectionEntity =
            PrefabExtensions.InstantiateSpaceSelectionEntity(spaceData, onSelectSpace, contentContainer);
        selectionEntities.Add(spaceSelectionEntity);
    }

    private void setAddButtonAsLastContentSibling()
    {
        newSpaceButton.transform.SetAsLastSibling();
    }
}
