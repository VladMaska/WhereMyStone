using System;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour {
    
    // Activated color
    [SerializeField] private Material material;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    // Cache
    private Renderer _renderer;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/
    
    private void Start() => _renderer = this.gameObject.GetComponent<Renderer>();

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void OnTriggerEnter( Collider other ){
        
        // Set comfortable work
        var obj = other.gameObject;
        
        // Check tag
        if ( obj.CompareTag( "Ball" ) )
            
            // Set material not only color
            _renderer.material = material;
        
    }

    // Slow down only after exit
    private void OnTriggerExit( Collider other ){
        
        // Set comfortable work
        var obj = other.gameObject;
        
        // Check tag
        if ( obj.CompareTag( "Ball" ) ){
            
            var bs = obj.GetComponent<BallScript>();
            
            var speed = bs.speed;
            
            if ( speed > 0 )
                bs.speed -= 0.2f;

            Destroy( this.gameObject, 1 );

        }
        
    }

}