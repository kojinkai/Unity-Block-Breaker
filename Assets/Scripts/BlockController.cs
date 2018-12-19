using UnityEngine;

public class BlockController : MonoBehaviour {

    // config Params
    [SerializeField] AudioClip blockCollisionClip;
    [SerializeField] GameObject blockParticlesVFX;
    [SerializeField] Sprite[] damageSprites;

    // cached references to other GameObjects
    LevelController levelController;
    GameSessionController gameSessionController;
    SpriteRenderer spriteRenderer;

    // state properties
    int hitsReceived;

    void Start()
    {
        gameSessionController = FindObjectOfType<GameSessionController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (tag == "Breakable")
        {
            AddBlockToBreakableCount();
        }
    }

    private void AddBlockToBreakableCount()
    {
        levelController = FindObjectOfType<LevelController>();
        levelController.AddBlockToGlobalBlockCount();
    }

    private void OnCollisionEnter2D()
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }


    protected void HandleHit() {
        int maxHits = damageSprites.Length + 1;
        hitsReceived++;
        if (hitsReceived >= maxHits) {
            DestroyBlock();
        }
        else {
            ShowNextDamageSprite();
        }
    }

    private void ShowNextDamageSprite()
    {
        int spriteIndex = hitsReceived - 1;
        if (damageSprites[spriteIndex] != null) { 
            spriteRenderer.sprite = damageSprites[spriteIndex];
        }
        else {
            Debug.LogError("Block Sprite is missing from array");
        }
    }

    public void DestroyBlock()
    {
        TriggerParticlesVFX();
        DestroyInUI();
        RemoveBlock();
    }

    private void DestroyInUI()
    {
        gameSessionController.AddToScore();
        AudioSource.PlayClipAtPoint(blockCollisionClip, Camera.main.transform.position);
    }

    private void RemoveBlock()
    {
        Destroy(gameObject);
        levelController.DecrementBlockCount();
    }

    private void TriggerParticlesVFX()
    {
        GameObject particleEffect = Instantiate(blockParticlesVFX, transform.position, transform.rotation);
        Destroy(particleEffect, 1f);
    }
}
