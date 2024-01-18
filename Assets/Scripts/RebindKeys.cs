using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class RebindKeys : MonoBehaviour
{
    [SerializeField] InputActionReference RebindBrake, RebindRocket, RebindShoot;
    [SerializeField] GameObject RebindRocketText, WaitingRocketText, RebindShootingText, WaitingShootingText, RebindBrakeText, WaitingBrakeText;
    public InputActionRebindingExtensions.RebindingOperation rebindOp;
    [SerializeField] Button RocketButton, ShootButton, BrakeButton;

    public void RebindingRocket()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RebindRocket.action.Disable();
        RebindRocketText.SetActive(false);
        WaitingRocketText.SetActive(true);
        rebindOp = RebindRocket.action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => CompleteRebindRocket())
            .Start();
            /*operation => CompleteRebindForRocket();
            {
                RebindMovementText.SetActive(true);
                rebindOp.Dispose();
                RebindRocket.action.Enable();
            }
            )
            .Start();   */     
    }
    public void RebindingShoot()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RebindShoot.action.Disable();
        RebindShootingText.SetActive(false);
        WaitingShootingText.SetActive(true);
        rebindOp = RebindShoot.action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => CompleteRebindShoot())
            .Start();
    }
    public void RebindingBrake()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RebindBrake.action.Disable();
        RebindBrakeText.SetActive(false);
        WaitingBrakeText.SetActive(true);
        rebindOp = RebindBrake.action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => CompleteRebindBrake())
            .Start();
    }
    private void CompleteRebindRocket()
    {
        WaitingRocketText.SetActive(false);
        RebindRocketText.SetActive(true);
        rebindOp.Dispose();
        RebindRocket.action.Enable();
        RocketButton.Select();
    }

    private void CompleteRebindShoot()
    {
        WaitingShootingText.SetActive(false);
        RebindShootingText.SetActive(true);
        rebindOp.Dispose();
        RebindShoot.action.Enable();
        ShootButton.Select();
    }
    private void CompleteRebindBrake()
    {
        WaitingBrakeText.SetActive(false);
        RebindBrakeText.SetActive(true);
        rebindOp.Dispose();
        RebindBrake.action.Enable();
        BrakeButton.Select();
    }
}
