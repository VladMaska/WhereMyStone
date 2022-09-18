using UnityEngine;

public class Finish : MonoBehaviour {

    [SerializeField] private GameObject youWin;
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void OnTriggerEnter( Collider other ){

        var obj = other.gameObject;

        if ( obj.CompareTag( "Ball" ) && obj.TryGetComponent<BallScript>( out var ballScript ) ){
            
            ballScript.speed = 0;
            youWin.SetActive( true );
            
        }

    }
    
}