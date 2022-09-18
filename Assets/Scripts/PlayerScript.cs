using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    // Ball Prefab
    [SerializeField] private GameObject ballGameObject;
    // GameOver UI GameObject
    [SerializeField] private GameObject gameOverUI;
    // No comment :)
    [SerializeField] private float plusSize;
    // Finish point
    [SerializeField] private Transform finish;
    // Max Size
    // start size * max
    [Range( 0f, 5f )][SerializeField] private float max;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/
    
    // Cache
    private Transform _transform;
    private Rigidbody _rigidbody;
    private SphereCollider _sphereCollider;

    private Vector3 _scale;

    private bool _animation = false;
    private bool _ready = true;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/
    
    private void Start(){
        
        _transform = this.gameObject.GetComponent<Transform>();
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        _sphereCollider = this.gameObject.GetComponent<SphereCollider>();

        // Save first localScale for animation
        _scale = _transform.localScale;

    }

    private void Update(){

#if UNITY_EDITOR
        UnityEngine();
#else
        Mobile();
#endif
        
        // Smooth animation
        BallAnimation( 1 );
        
        if ( _transform.localScale.x > _scale.x * max ){
            
            _ready = false;
            gameOverUI.SetActive( true );
            
        }

    }

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    /* For test */
    private void UnityEngine(){
        
        // Check size
        if ( Input.GetMouseButton( 0 ) && _ready && _transform.localScale.x < _scale.x * max ){
            
            // plus SIZE
            _transform.localScale = new Vector3(
                _transform.localScale.x + ( ( plusSize * 0.2f ) * Time.deltaTime ),
                _transform.localScale.y + ( ( plusSize * 0.2f ) * Time.deltaTime ),
                _transform.localScale.z + ( ( plusSize * 0.2f ) * Time.deltaTime )
            );
        
        }
        
        // Create ball
        if ( Input.GetMouseButtonUp( 0 ) && _ready && _transform.localScale.x < _scale.x * max )
            StartCoroutine( ShootBall( 0.9f ) );
        
    }
    
    /* For build */
    private void Mobile(){

        if ( Input.touchCount > 0 && _ready ){

            var touch = Input.GetTouch( 0 );
            var _phase = touch.phase;
            
            if ( _phase == TouchPhase.Stationary && _ready && _transform.localScale.x < _scale.x * max )
                _transform.localScale = new Vector3 (
                    _transform.localScale.x + ( ( plusSize * 0.2f ) * Time.deltaTime ),
                    _transform.localScale.y + ( ( plusSize * 0.2f ) * Time.deltaTime ),
                    _transform.localScale.z + ( ( plusSize * 0.2f ) * Time.deltaTime )
                );
            
            if ( _phase == TouchPhase.Ended && _ready && _transform.localScale.x < _scale.x * max )
                StartCoroutine( ShootBall( 0.9f ) );

        }
        
    }
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void BallAnimation( float time ){
        
        // While true please play
        if ( _animation )
            transform.localScale = Vector3.Lerp( transform.localScale, _scale, time * Time.deltaTime );
        
    }

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private IEnumerator ShootBall( float time = 1 ){
        
        _ready = false;
        
        // Set player ball not active for unity physic 
        _rigidbody.useGravity = false;
        _sphereCollider.isTrigger = true;

        // Set position for ball
        var ballPos = _transform.position + ( Vector3.forward );
        // Create ball
        var ball = Instantiate( ballGameObject, ballPos, Quaternion.identity );

        // Set ball size
        ball.GetComponent<Transform>().localScale = _transform.localScale;
        // Set final point
        ball.GetComponent<BallScript>().SetPoint( finish );
        // Set GameOver UIs
        ball.GetComponent<BallScript>().SetUI( gameOverUI );
        // Only set name
        ball.name = "Ball";
        
        // Please wait... 
        yield return new WaitForSeconds( time / 5 );
        
        // Hello unity physic
        _rigidbody.useGravity = true;
        _sphereCollider.isTrigger = false;

        // Start Animation
        _animation = true;
        
        // Wait Animation
        yield return new WaitForSeconds( time );
        
        // Stop Animation
        _animation = false;

    }

}