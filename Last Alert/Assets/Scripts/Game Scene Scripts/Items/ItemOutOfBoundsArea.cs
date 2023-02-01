using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemOutOfBoundsArea : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        //Check if object is an item
        Item itemRef = other.GetComponent<Item>();
        if (itemRef != null) {
            itemRef.ResetItem();
            return;
        }

        //Check if the object is the player
        PlayerController playerControllerRef = other.GetComponent<PlayerController>();
        if (playerControllerRef != null) {
            playerControllerRef.ResetPlayer();
            return;
        }

        //Other type of object
        Destroy(other.gameObject);
    }
}