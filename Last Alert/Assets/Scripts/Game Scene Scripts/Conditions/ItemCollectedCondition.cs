using UnityEngine;

public class ItemCollectedCondition : Condition {

    public bool resetItemToLocation = true;

    //Allow multiple types of the same object if needed
    public GameObject[] itemWanted;
    private Vector3[] itemStartPos;
    private Quaternion[] itemStartRot;

    void Start() {
        //Save start location if resetting to start location is wanted
        if (resetItemToLocation == true) {
            itemStartPos = new Vector3[itemWanted.Length];
            itemStartRot = new Quaternion[itemWanted.Length];
            for (int i = 0; i < itemWanted.Length; i++) {
                itemStartPos[i] = itemWanted[i].transform.position;
                itemStartRot[i] = itemWanted[i].transform.rotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        foreach (GameObject item in itemWanted) {
            if (item == other.gameObject) {
                SetCompleted(true);
            }
        }
    }

    public override void ResetCondition() {
        SetCompleted(false);
        //Put items back in start location if true
        if (resetItemToLocation == true) {
            for (int i = 0; i < itemWanted.Length; i++) {
                //Stop physics
                Rigidbody rigidbody = itemWanted[i].GetComponent<Rigidbody>();
                if (rigidbody != null) {
                    rigidbody.isKinematic = true;
                }
                //Move object
                itemWanted[i].transform.position = itemStartPos[i];
                itemWanted[i].transform.rotation = itemStartRot[i];
                //Start physics
                if (rigidbody != null) {
                    rigidbody.isKinematic = false;
                }
            }
        }
    }
}