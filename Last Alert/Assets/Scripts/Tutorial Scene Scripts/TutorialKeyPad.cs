using UnityEngine;

public class TutorialKeyPad : MonoBehaviour {

    public ItemCollectedCondition condition;
    public Vector3 newPos;
    public Vector3 newRot;

    public Animator animatorRef;
    public string animatorBoolName;

    void Update() {
        if (condition.GetCompleted() == true) {
            //Get reference
            GameObject wantedItem = condition.itemWanted[0];

            //Check if of type pickup and if so drop if the player is holding it
            if (wantedItem.GetComponent<PickUp>() != null) {
                if (wantedItem.GetComponent<PickUp>().held == true) {
                    GameReferenceGetter.pickUpControllerRef.DropItem(false);
                }
            }

            //Stop physics
            Rigidbody rigidbody = wantedItem.GetComponent<Rigidbody>();
            if (rigidbody != null) {
                rigidbody.isKinematic = true;
            }
            //Move object
            wantedItem.transform.position = newPos;
            wantedItem.transform.eulerAngles = newRot;
            //Start physics
            if (rigidbody != null) {
                rigidbody.isKinematic = false;
            }

            //Call animator variable
            if (animatorRef != null) {
                animatorRef.SetBool(animatorBoolName, true);
            }
        }
    }
}