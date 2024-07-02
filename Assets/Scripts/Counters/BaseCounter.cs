using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    public static void ResetStaticData() { OnAnyObjectPlacedHere = null; }

    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
    }
    public KitchenObject GetKitchenObject() { return kitchenObject; }
    public void ClearKitchenObject() { kitchenObject = null; }
    public bool HasKitchenObject() { return kitchenObject != null; }
    public Transform GetKitchenObjectFollowTransform() { return counterTopPoint; }

    public virtual void Interact(Player player) { Debug.LogError("BaseCounter.Interact() should not be triggered directly"); }
    public virtual void InteractAlternate(Player player) { }

}
