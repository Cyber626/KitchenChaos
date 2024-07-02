using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer, spawnPlateTimerMax = 4;
    private int platesSpawnAmount, platesSpawnAmountMax = 4;

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            spawnPlateTimer += Time.deltaTime;
            if (spawnPlateTimer > spawnPlateTimerMax)
            {
                spawnPlateTimer = 0;

                if (platesSpawnAmount < platesSpawnAmountMax)
                {
                    platesSpawnAmount++;
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() && platesSpawnAmount > 0)
        {
            platesSpawnAmount--;

            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
