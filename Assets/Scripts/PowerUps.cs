using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour{
    
    public enum Type{
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player){
        switch(type){

            case Type.Coin:
                //ToDo
                GameManager.Instance.AddCoin();
            break;

            case Type.ExtraLife:
                //ToDo
                GameManager.Instance.AddLife();
            break;

            case Type.MagicMushroom:
                //ToDo
                player.GetComponent<Player>().Grow();
            break;

            case Type.Starpower:
                //ToDo
                player.GetComponent<Player>().Starpower();
            break;

        }
        
        Destroy(gameObject);
    }

}
