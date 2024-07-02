using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private List<Table> availableTablesList;

    // private float spawnRecipeTimer, spawnRecipeTimerMax = 4f;
    // private readonly int waitingRecipesMax = 4;
    private int successfulRecipesAmount;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
        availableTablesList = new List<Table>();
    }

    private void Update()
    {
        // if (GameManager.Instance.IsGamePlaying() && waitingRecipesMax > waitingRecipeSOList.Count)
        // {
        //     spawnRecipeTimer -= Time.deltaTime;
        //     if (spawnRecipeTimer <= 0)
        //     {
        //         spawnRecipeTimer = spawnRecipeTimerMax;

        //         RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
        //         waitingRecipeSOList.Add(waitingRecipeSO);

        //         OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        //     }
        // }
    }

    // public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    // {
    //     for (int i = 0; i < waitingRecipeSOList.Count; i++)
    //     {
    //         RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

    //         if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
    //         {
    //             bool plateContentsMatchesRecipe = true;
    //             foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
    //             {
    //                 if (!plateKitchenObject.GetKitchenObjectSOList().Contains(recipeKitchenObjectSO))
    //                 {
    //                     plateContentsMatchesRecipe = false;
    //                 }
    //             }
    //             if (plateContentsMatchesRecipe)
    //             {
    //                 //Recipe delivered
    //                 successfulRecipesAmount++;
    //                 waitingRecipeSOList.RemoveAt(i);
    //                 OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
    //                 OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
    //                 return;
    //             }
    //         }
    //     }
    //     //Incorrect delivery
    // }

    public void SuccessfulDelivered()
    {
        successfulRecipesAmount++;
        // check next line
        // waitingRecipeSOList.RemoveAt(0);
        // OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        // OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingListSO() { return waitingRecipeSOList; }
    public int GetSuccessfulRecipesAmount() { return successfulRecipesAmount; }
    public void AddToWaitingListSO(RecipeSO recipeSO)
    {
        waitingRecipeSOList.Add(recipeSO);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveFromWaitingListSO(RecipeSO recipeSO)
    {
        waitingRecipeSOList.Remove(recipeSO);
        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
    }

    public void AddToAvailableList(Table table)
    {
        availableTablesList.Add(table);
    }
}