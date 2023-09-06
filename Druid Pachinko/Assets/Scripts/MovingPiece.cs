using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPiece : MonoBehaviour
{
    [SerializeField, Tooltip("a list of all goal positions this object will move between, in order")]List<Transform> goalPositions = new List<Transform>();
    private Transform currentMoveGoal = null;
    private int moveGoalIndex = 0;
    [SerializeField, Tooltip("how fast this object should move")]private float moveSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        if(goalPositions.Count < 0){
            Debug.LogWarning("OBJECT NOT GIVEN ANY POSITIONS TO MOVE TO");
            moveGoalIndex = -1;
        }
        else{
            currentMoveGoal = goalPositions[0];
        }
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if(Vector3.Distance(transform.position, currentMoveGoal.position) < .01f){
            if(moveGoalIndex >= goalPositions.Count - 1){
                currentMoveGoal = goalPositions[0];
                moveGoalIndex = 0;
            }
            else{
                moveGoalIndex++;
                currentMoveGoal = goalPositions[moveGoalIndex];
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, currentMoveGoal.position, moveSpeed * Time.fixedDeltaTime);
        

    }
}
