using UnityEngine;

public class BallScript : MonoBehaviour {

    // Dynamic spe..ed
    public float speed;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    // Cache
    private Transform _transform;
    
    // Game Over UI
    private GameObject _uiPanel;
    // Finish Point
    private Transform _point;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void Start(){
        
        _transform = this.gameObject.GetComponent<Transform>();

        speed = _transform.localScale.x * 2f;

    }

    // Moving with dynamic speed
    private void Update(){
        
        _transform.position = Vector3.MoveTowards( _transform.position, _point.position, speed * Time.deltaTime );

        if ( speed != 0 && speed < ( _transform.localScale.x * 2.25f ) * 0.01f ){
        
            speed = 0;
            _uiPanel.SetActive( true );
            
        }

    }
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    public void SetUI( GameObject ui ) => _uiPanel = ui;
    
    public void SetPoint( Transform point ) => _point = point;

}