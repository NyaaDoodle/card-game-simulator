using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    [Header("View Containers")]
    [SerializeField] private GameObject tableViewContainer;
    [SerializeField] private GameObject handViewContainer;

    [Header("View Switch Buttons")]
    [SerializeField] private Button switchToHandButton;   // Shows in Table View
    [SerializeField] private Button switchToTableButton; // Shows in Hand View

    [Header("Settings")]
    [SerializeField] private SimulatorView startingView = SimulatorView.TableView;

    public SimulatorView CurrentView { get; private set; }

    // Events
    public event Action<SimulatorView, SimulatorView> ViewChanged; // oldView, newView
    public event Action<SimulatorView> ViewActivated;

    public bool IsInTableView => CurrentView == SimulatorView.TableView;
    public bool IsInHandView => CurrentView == SimulatorView.HandView;

    void Start()
    {
        setupButtons();
        SwitchToView(startingView);
    }

    private void setupButtons()
    {
        if (switchToHandButton != null)
        {
            switchToHandButton.onClick.AddListener(() => SwitchToView(SimulatorView.HandView));
        }
        if (switchToTableButton != null)
        {
            switchToTableButton.onClick.AddListener(() => SwitchToView(SimulatorView.TableView));
        }
    }

    public void SwitchToView(SimulatorView targetView)
    {
        if (CurrentView == targetView) return;

        SimulatorView previousView = CurrentView;
        CurrentView = targetView;

        updateViewContainersVisibility();
        updateButtonVisibility();

        OnViewChanged(previousView);
        OnViewActivated();

        Debug.Log($"Switched from {previousView} to {CurrentView}");
    }

    protected virtual void OnViewChanged(SimulatorView previousView)
    {
        ViewChanged?.Invoke(previousView, CurrentView);
    }

    protected virtual void OnViewActivated()
    {
        ViewActivated?.Invoke(CurrentView);
    }

    private void updateViewContainersVisibility()
    {
        if (tableViewContainer != null)
        {
            tableViewContainer.SetActive(CurrentView == SimulatorView.TableView);
        }
        if (handViewContainer != null)
        {
            handViewContainer.SetActive(CurrentView == SimulatorView.HandView);
        }
    }

    private void updateButtonVisibility()
    {
        if (switchToHandButton != null)
        {
            switchToHandButton.gameObject.SetActive(CurrentView == SimulatorView.TableView);
        }
        if (switchToTableButton != null)
        {
            switchToTableButton.gameObject.SetActive(CurrentView == SimulatorView.HandView);
        }
    }
}
