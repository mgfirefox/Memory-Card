using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int score = 0;

    [SerializeField]
    private TextMesh scoreLabel;

    public const int columnsQuantity = 4;
    public const int rowsQuantity = 2;

    public const float offsetX = 2.5f;
    public const float offsetY = 3.0f;

    [SerializeField]
    private MemoryCard originalMemoryCard;

    [SerializeField]
    private Sprite[] images;

    private MemoryCard firstRevealedMemoryCard;
    private MemoryCard secondRevealedMemoryCard;

    public bool CanRevealMemoryCard {
        get
        {
            return secondRevealedMemoryCard == null;
        }
    }

    void Start()
    {
        if (2 * images.Length != columnsQuantity * rowsQuantity)
        {
            Destroy(originalMemoryCard);
            return;
        }

        Vector3 originalPosition = originalMemoryCard.transform.position;

        MemoryCard memoryCard;

        int[] ids = FillArray();
        ids = ShuffleArray(ids);

        for (int i = 0; i < columnsQuantity; i++)
        {
            for (int j = 0; j < rowsQuantity; j++)
            {
                if (i == 0 && j == 0)
                {
                    memoryCard = originalMemoryCard;
                }
                else
                {
                    memoryCard = Instantiate(originalMemoryCard);
                }

                int id = ids[j + rowsQuantity * i];
                memoryCard.SetId(id, images[id]);

                float positionX = originalPosition.x + (offsetX * i);
                float positionY = originalPosition.y - (offsetY * j);

                memoryCard.transform.position = new Vector3(positionX, positionY, originalPosition.z);
            }
        }
    }

    private int[] FillArray()
    {
        int[] ids = new int[columnsQuantity * rowsQuantity];
        
        for (int i = 0, id = 0; i < columnsQuantity * rowsQuantity; i += 2, id++)
        {
            ids[i] = id;
            ids[i + 1] = id;
        }

        return ids;
    }

    private int[] ShuffleArray(int[] ids)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            int temp = ids[i];
            int randomIndex = Random.Range(i, ids.Length);
            ids[i] = ids[randomIndex];
            ids[randomIndex] = temp;
        }

        return ids;
    }

    public void OnCardRevealed(MemoryCard memoryCard)
    {
        if (firstRevealedMemoryCard == null)
        {
            firstRevealedMemoryCard = memoryCard;
        }
        else
        {
            secondRevealedMemoryCard = memoryCard;
            StartCoroutine(CheckMatch());
        }
    }
    private IEnumerator CheckMatch()
    {
        if (firstRevealedMemoryCard.GetId() == secondRevealedMemoryCard.GetId())
        {
            score++;
            scoreLabel.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(1.0f);

            firstRevealedMemoryCard.Unreveal();
            secondRevealedMemoryCard.Unreveal();
        }

        firstRevealedMemoryCard = null;
        secondRevealedMemoryCard = null;
    }

    public void Restart()
    {
        //Application.LoadLevel("SampleScene");
        SceneManager.LoadScene("SampleScene");
    }
}
