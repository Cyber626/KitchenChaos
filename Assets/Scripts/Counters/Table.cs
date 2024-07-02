using System.Collections.Generic;
using UnityEngine;

public class Table : DeliveryBase
{
    private enum State
    {
        WaitingGuests,
        Ordering,
        WaitingOrders,
        Eating,
        WaitingCleanUp,
    }

    private State state = State.WaitingGuests;
    private float orderingTimer = 0, eatingTimer = 0;
    [SerializeField] private float orderingTimerMax = 10, eatingTimerMax = 25;
    [SerializeField] private KitchenObjectSO tempKitchenObjectSO;


    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Start()
    {
        DeliveryManager.Instance.AddToAvailableList(this);
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.WaitingGuests:
                break;
            case State.Ordering:
                OrderingStateHandler();
                break;
            case State.WaitingOrders:
                break;
            case State.Eating:
                EatingStateHandler();
                break;
            case State.WaitingCleanUp:
                break;
        }
    }

    protected override void SuccessfulDelivered()
    {
        if (waitingRecipeSOList.Count == 0)
        {
            state = State.Eating;
        }
    }

    protected override void PutPlateToTable(Player player)
    {
        player.GetKitchenObject().SetKitchenObjectParent(this);
    }

    private void EatingStateHandler()
    {
        eatingTimer += Time.deltaTime;
        if (eatingTimer >= eatingTimerMax)
        {
            //finished eating
            GetKitchenObject().DestroySelf();
            //create unwashed dishes
            SetKitchenObject(KitchenObject.SpawnKitchenObject(tempKitchenObjectSO, this));
            state = State.WaitingCleanUp;
        }
    }

    public override void Interact(Player player)
    {
        switch (state)
        {
            case State.WaitingOrders: base.Interact(player); break;
            case State.WaitingCleanUp: CleanupInteract(player); break;
            default: break;
        }
    }

    private void CleanupInteract(Player player)
    {
        if (!player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
            state = State.WaitingGuests;
            DeliveryManager.Instance.AddToAvailableList(this);
        }
    }

    private void OrderingStateHandler()
    {
        orderingTimer += Time.deltaTime;
        if (orderingTimer >= orderingTimerMax)
        {
            AddNewRecipeToWaitingList();
        }
    }

    public void GuestSat()
    {
        state = State.Ordering;
    }
}
