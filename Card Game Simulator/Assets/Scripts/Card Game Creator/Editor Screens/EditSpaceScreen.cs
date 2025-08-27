using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditSpaceScreen : GameTemplateEditorScreenBase
{
    [SerializeField] private TMP_InputField spaceNameInputField;
    [SerializeField] private TMP_InputField spaceXCoordinateInputField;
    [SerializeField] private TMP_InputField spaceYCoordinateInputField;
    [SerializeField] private TMP_InputField spaceRotationInputField;
    [SerializeField] private Button deleteSpaceButton;

    private string currentSpaceId;
    private SpaceData currentSpaceData => WorkingGameTemplate.SpacesData[currentSpaceId];

    public void Show(SpaceData spaceData, Action onBackButtonSelect)
    {
        gameObject.SetActive(true);
        SetupBaseButtons(onBackButtonSelect);
        currentSpaceId = spaceData.Id;
        setupInputFields();
        setupDeleteButton();
    }

    public override void Hide()
    {
        currentSpaceId = null;
        unsetInputFields();
        unsetDeleteButton();
        base.Hide();
    }

    private void setupInputFields()
    {
        setupInputFieldsText();
        setupInputFieldsEvents();
    }

    private void setupInputFieldsText()
    {
        spaceNameInputField.text = currentSpaceData.Name;
        spaceXCoordinateInputField.text = currentSpaceData.TableXCoordinate.ToString();
        spaceYCoordinateInputField.text = currentSpaceData.TableYCoordinate.ToString();
        spaceRotationInputField.text = currentSpaceData.Rotation.ToString();
    }

    private void setupInputFieldsEvents()
    {
        spaceNameInputField.onEndEdit.AddListener((text) => WorkingGameTemplate.SetSpaceName(currentSpaceData, text));
        spaceXCoordinateInputField.onEndEdit.AddListener((text) =>
            {
                bool success = float.TryParse(text, out float x);
                if (success)
                {
                    WorkingGameTemplate.SetSpaceXCoordinate(currentSpaceData, x);
                }
            });
        spaceYCoordinateInputField.onEndEdit.AddListener((text) =>
            {
                bool success = float.TryParse(text, out float y);
                if (success)
                {
                    WorkingGameTemplate.SetSpaceYCoordinate(currentSpaceData, y);
                }
            });
        spaceRotationInputField.onEndEdit.AddListener((text) =>
            {
                bool success = float.TryParse(text, out float rotation);
                if (success)
                {
                    WorkingGameTemplate.SetSpaceRotation(currentSpaceData, rotation);
                }
            });
    }

    private void setupDeleteButton()
    {
        deleteSpaceButton.onClick.AddListener(() =>
            {
                WorkingGameTemplate.DeleteSpaceData(currentSpaceData);
                GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen();
            });
    }
    
    private void unsetInputFields()
    {
        spaceNameInputField.onEndEdit.RemoveAllListeners();
        spaceXCoordinateInputField.onEndEdit.RemoveAllListeners();
        spaceYCoordinateInputField.onEndEdit.RemoveAllListeners();
        spaceRotationInputField.onEndEdit.RemoveAllListeners();
    }

    private void unsetDeleteButton()
    {
        deleteSpaceButton.onClick.RemoveAllListeners();
    }
}