using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField]
    private GameObject cardBack;

    [SerializeField]
    private SceneController sceneController;

    private int id;

    public int GetId()
    {
        return id;
    }

    public void SetId(int id, Sprite image) {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    void OnMouseDown()
    {
        if (!cardBack.activeSelf) { return; }
        if (!sceneController.CanRevealMemoryCard) { return; }

        cardBack.SetActive(false);
        sceneController.OnCardRevealed(this);
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
}
