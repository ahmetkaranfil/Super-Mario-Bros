using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{

    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;

    private PlayerSpriteRenderer activeRenderer;
    
    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;

    public bool starpower { get; private set; }
    
    public void Awake(){
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }
    
    public void Hit(){
        if(!dead && !starpower){
            if(big) {
                Shrink();
            } else if(small) {
                Death();
            }
        }
    }

    private void Death(){
        bigRenderer.enabled = false;
        smallRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }

    public void Grow(){
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f , 2f);
        capsuleCollider.offset = new Vector2(0f , 0.5f);

        StartCoroutine(SizeAnimation());
        //this line starts the growth animation
    }

    private void Shrink(){
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;
        
        capsuleCollider.size = new Vector2(1f , 1f);
        capsuleCollider.offset = new Vector2(0f , 0f);

        StartCoroutine(SizeAnimation());
        //this line starts the shrink animation
    }

    private IEnumerator SizeAnimation(){
        float elapsed = 0f;
        float duration = 0.5f;

        while(elapsed < duration){
            elapsed += Time.deltaTime;
            if(Time.frameCount % 4 == 0){
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
                gameObject.layer = LayerMask.NameToLayer("Enemy");
            }

            yield return null;
        }

        gameObject.layer = LayerMask.NameToLayer("Player");
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower(float duration = 10f){
        StartCoroutine(StarpowerAnimation(duration));
    }

    private IEnumerator StarpowerAnimation(float duration){
        starpower = true;

        float elapsed = 0f;
        while(elapsed < duration){
            elapsed += Time.deltaTime;
            if(Time.frameCount % 4 == 0){
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }
        
        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
    }

}