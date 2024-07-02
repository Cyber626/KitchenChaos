using System;
using UnityEngine;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private GameObject successUI, failUI;
    [SerializeField] private DeliveryBase deliveryBase;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        deliveryBase.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        deliveryBase.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        successUI.SetActive(false);
        failUI.SetActive(false);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        failUI.SetActive(false);
        successUI.SetActive(true);
        animator.SetTrigger(POPUP);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        successUI.SetActive(false);
        failUI.SetActive(true);
        animator.SetTrigger(POPUP);
    }

}
