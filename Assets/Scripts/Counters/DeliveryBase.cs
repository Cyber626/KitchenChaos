using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBase : BaseCounter
{
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    protected List<RecipeSO> waitingRecipeSOList;
    [SerializeField] protected RecipeListSO recipeListSO;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() && !HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (DeliverRecipe(plateKitchenObject))
                {
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }

    protected virtual void PutPlateToTable(Player player)
    {
        Debug.Log("PutPlateToTable: Should put plate to counter");
    }

    protected void AddNewRecipeToWaitingList()
    {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
        waitingRecipeSOList.Add(waitingRecipeSO);
        DeliveryManager.Instance.AddToWaitingListSO(waitingRecipeSO);
    }

    private bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    if (!plateKitchenObject.GetKitchenObjectSOList().Contains(recipeKitchenObjectSO))
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    //Recipe delivered
                    waitingRecipeSOList.RemoveAt(i);
                    DeliveryManager.Instance.RemoveFromWaitingListSO(waitingRecipeSO);

                    SoundManager.Instance.OnRecipeSuccess(this);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    SuccessfulDelivered();

                    //Todo: if waitingList is empty then start eating
                    return true;
                }
            }
        }
        //Incorrect delivery

        SoundManager.Instance.OnRecipeFailed(this);
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        return false;
    }

    protected virtual void IncorrectDelivered() { }

    protected virtual void SuccessfulDelivered()
    {
        Debug.Log("Base: Successfully delivered!");
    }
}
