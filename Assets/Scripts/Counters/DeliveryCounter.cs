using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : DeliveryBase
{

    private float spawnRecipeTimer = 0;
    [SerializeField] private float spawnRecipeTimerMax = 15;
    [SerializeField] private int waitingRecipesMax = 4;


    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying() && waitingRecipesMax > waitingRecipeSOList.Count)
        {
            spawnRecipeTimer += Time.deltaTime;
            if (spawnRecipeTimer >= spawnRecipeTimerMax)
            {
                spawnRecipeTimer = 0;
                AddNewRecipeToWaitingList();
            }
        }
    }
    protected override void SuccessfulDelivered() { }
    protected override void PutPlateToTable(Player player)
    {
        player.GetKitchenObject().DestroySelf();
    }
}
