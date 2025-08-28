using System;
using System.Collections.Generic;
using UnityEngine;

public static class ModalWindowManager
{
    private static ModalWindowBase currentlyShownWindow;

    public static void OpenCardSelectionModalWindow(
        string titleText,
        IEnumerable<CardData> cardsData,
        Action<CardData> onSelectCard,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        currentlyShownWindow = PrefabExtensions.InstantiateCardSelectionModalWindow(
            titleText,
            cardsData,
            onSelectCard,
            onAddButtonSelect,
            onBackButtonSelect);
    }
    
    public static void OpenCardSelectionModalWindow(
        string titleText,
        IEnumerable<Card> cards,
        Action<Card> onSelectCard,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        currentlyShownWindow = PrefabExtensions.InstantiateCardSelectionModalWindow(
            titleText,
            cards,
            onSelectCard,
            onAddButtonSelect,
            onBackButtonSelect);
    }
    
    public static void OpenGameTemplateSelectionModalWindow(
        string titleText,
        Action<GameTemplate> onSelectGameTemplate,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        currentlyShownWindow = PrefabExtensions.InstantiateGameTemplateSelectionModalWindow(
            titleText,
            onSelectGameTemplate,
            onAddButtonSelect,
            onBackButtonSelect);
    }
    
    public static void OpenDeckSelectionModalWindow(
        string titleText,
        IEnumerable<DeckData> decksData,
        Action<DeckData> onSelectDeck,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        currentlyShownWindow = PrefabExtensions.InstantiateDeckSelectionModalWindow(
            titleText,
            decksData,
            onSelectDeck,
            onAddButtonSelect,
            onBackButtonSelect);
    }
    
    public static void OpenSpaceSelectionModalWindow(
        string titleText,
        IEnumerable<SpaceData> spacesData,
        Action<SpaceData> onSelectSpace,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        currentlyShownWindow = PrefabExtensions.InstantiateSpaceSelectionModalWindow(
            titleText,
            spacesData,
            onSelectSpace,
            onAddButtonSelect,
            onBackButtonSelect);
    }

    public static void OpenMobileImageMethodModalWindow(Action<Texture2D> onImageLoaded, Action onCancel, Action onBackButtonSelect)
    {
        currentlyShownWindow =
            PrefabExtensions.InstantiateMobileImageMethodModalWindow(onImageLoaded, onCancel, onBackButtonSelect);
    }

    public static void CloseCurrentWindow()
    {
        if (currentlyShownWindow != null && currentlyShownWindow.gameObject != null)
        {
            GameObject.Destroy(currentlyShownWindow.gameObject);
        }
        currentlyShownWindow = null;
    }
}
