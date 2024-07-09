using UnityEngine;

public class SlideScrolling : MonoBehaviour{

    private Transform player;
    
    public float Height = 6.5f;
    public float undergroundHeight = -10f;
    
    private void Awake(){
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate(){
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }

    public void SetUnderground(bool underground){

        Vector3 camaraPosition = transform.position;
        camaraPosition.y = underground ? undergroundHeight : Height;
        transform.position = camaraPosition;

    }

}
