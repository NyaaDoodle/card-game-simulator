using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditDeckScreen : GameTemplateEditorScreenBase
{
    [SerializeField] private TMP_InputField deckNameInputField;
    [SerializeField] private TMP_InputField deckXCoordinateInputField;
    [SerializeField] private TMP_InputField deckYCoordinateInputField;
    [SerializeField] private TMP_InputField deckRotationInputField;
    [SerializeField] private Button deleteDeckButton;
    [SerializeField] private CardSelectionGrid cardsInDeckSelectionGrid;
    [SerializeField] private Button removeCardFromDeckButton;

    private string currentDeckId;
    private DeckData currentDeckData => WorkingGameTemplate.DecksData[currentDeckId];

    public void Show(DeckData deckData, Action onBackButtonSelect)
    {
        gameObject.SetActive(true);
        currentDeckId = deckData.Id;
        SetupBaseButtons(onBackButtonSelect);
        setupInputFields();
        setupDeleteDeckButton();
        setupCardsInDeckSelectionGrid();
    }

    public override void Hide()
    {
        currentDeckId = null;
        hideRemoveCardButton();
        unsetInputFields();
        unsetDeleteDeckButton();
        unsetCardsInDeckSelectionGrid();
        base.Hide();
    }

    private void setupInputFields()
    {
        setupInputFieldsText();
        setupInputFieldsEvents();
    }

    private void setupInputFieldsText()
    {
        deckNameInputField.text = currentDeckData.Name;
        deckXCoordinateInputField.text = currentDeckData.TableXCoordinate.ToString();
        deckYCoordinateInputField.text = currentDeckData.TableYCoordinate.ToString();
        deckRotationInputField.text = currentDeckData.Rotation.ToString();
    }

    private void setupInputFieldsEvents()
    {
        deckNameInputField.onEndEdit.AddListener((text) => WorkingGameTemplate.SetDeckName(currentDeckData, text));
        deckXCoordinateInputField.onEndEdit.AddListener((text) =>
            {
                hideRemoveCardButton();
                bool success = float.TryParse(text, out float x);
                if (success)
                {
                    WorkingGameTemplate.SetDeckXCoordinate(currentDeckData, x);
                }
            });
        deckYCoordinateInputField.onEndEdit.AddListener((text) =>
            {
                hideRemoveCardButton();
                bool success = float.TryParse(text, out float y);
                if (success)
                {
                    WorkingGameTemplate.SetDeckYCoordinate(currentDeckData, y);
                }
            });
        deckRotationInputField.onEndEdit.AddListener((text) =>
            {
                hideRemoveCardButton();
                bool success = float.TryParse(text, out float rotation);
                if (success)
                {
                    WorkingGameTemplate.SetDeckRotation(currentDeckData, rotation);
                }
            });
    }

    private void setupDeleteDeckButton()
    {
        deleteDeckButton.onClick.AddListener(() =>
            {
                WorkingGameTemplate.DeleteDeckData(currentDeckData);
                BackButton.onClick.Invoke();
            });
    }

    private void setupCardsInDeckSelectionGrid()
    {
        cardsInDeckSelectionGrid.Show(currentDeckData.StartingCardIds, WorkingGameTemplate,
            showRemoveCardButton,
            () =>
                {
                    hideRemoveCardButton();
                    ModalWindowManager.OpenCardSelectionModalWindow(
                        "Select a Card to Add to Deck",
                        WorkingGameTemplate.CardPool.Values,
                        (cardData) =>
                            {
                                WorkingGameTemplate.AddCardDataToDeckStartingCards(currentDeckData, cardData);
                                ModalWindowManager.CloseCurrentWindow();
                                cardsInDeckSelectionGrid.UpdateCards(
                                    currentDeckData.StartingCardIds,
                                    WorkingGameTemplate);
                            },
                        null,
                        ModalWindowManager.CloseCurrentWindow,
                        null);
                });
    }

    private void showRemoveCardButton(CardData cardData)
    {
        removeCardFromDeckButton.gameObject.SetActive(true);
        removeCardFromDeckButton.onClick.RemoveAllListeners();
        removeCardFromDeckButton.onClick.AddListener(() =>
            {
                WorkingGameTemplate.RemoveCardDataFromDeckStartingCards(currentDeckData, cardData);
                hideRemoveCardButton();
                cardsInDeckSelectionGrid.UpdateCards(currentDeckData.StartingCardIds, WorkingGameTemplate);
            });
    }

    private void hideRemoveCardButton()
    {
        removeCardFromDeckButton.onClick.RemoveAllListeners();
        removeCardFromDeckButton.gameObject.SetActive(false);
    }
    
    private void unsetInputFields()
    {
        deckNameInputField.onEndEdit.RemoveAllListeners();
        deckXCoordinateInputField.onEndEdit.RemoveAllListeners();
        deckYCoordinateInputField.onEndEdit.RemoveAllListeners();
        deckRotationInputField.onEndEdit.RemoveAllListeners();
    }

    private void unsetDeleteDeckButton()
    {
        deleteDeckButton.onClick.RemoveAllListeners();
    }

    private void unsetCardsInDeckSelectionGrid()
    {
        cardsInDeckSelectionGrid.Hide();
    }
}