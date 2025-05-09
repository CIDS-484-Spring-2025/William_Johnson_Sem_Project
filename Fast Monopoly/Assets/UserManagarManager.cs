using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManagarManager : MonoBehaviour
{
    public User userPrefab;
    public List<PlayerBehavior> pieces = new List<PlayerBehavior>();
    public List<JailedPlayerBehavior> jailedPieces = new List<JailedPlayerBehavior>();
    public GameObject userManagersParent;

    void Start(){
        for(int i = 0; i < References.numUsers; i++){
            User user = Instantiate(userPrefab, userManagersParent.transform);
            user.name = "User " + (i + 1) + " Manager";
            user.playerNumber = i + 1;
            user.piece = pieces[i];
            user.jailedPiece = jailedPieces[i];
        }
    }

    

}
