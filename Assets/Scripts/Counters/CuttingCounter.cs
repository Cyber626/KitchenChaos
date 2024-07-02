using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{

    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData() { OnAnyCut = null; }

    public event EventHandler<IHasProgress.OnProgressChangedEvents> OnProgressChanged;

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
            if (cuttingRecipeSO != null)
            {

                cuttingProgress++;

                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEvents
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });

                if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    KitchenObjectSO outputKitchenObjectObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(outputKitchenObjectObjectSO, this);
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Put object to counter
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEvents
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //PlayerHoldingPlate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                }
            }
            else
            {
                //Take object from counter
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObject)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(inputKitchenObject);
        if (cuttingRecipeSO != null) return cuttingRecipeSO.output;
        else return null;
    }

    private CuttingRecipeSO GetCuttingRecipeSoWithInput(KitchenObjectSO inputKitchenObject)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObject)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
