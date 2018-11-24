using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hivemind.Utilities;

public class PlayerController : MonoBehaviour {

    public float movementAmount = 1f;
    public float speed = 0.1f;
    public LevelData levelData;
    public LayerMask collideWith;
    public Sprite[] characters;

    [HideInInspector]
    public bool IsSolvingTextTrap = false;

    public float chanceOfTextTrap = 0.05f;


    private Vector3 newPosition;
    private Vector3 newMapPosition;
    private SpriteRenderer spriteRenderer;
    private int currentCharacterIndex = 0;
    private bool moving = false;
    private Animator textTrapAnim;
    private Animator playerAnim;
    private TextTrapScript textTrap;
    private GameUIManager gameUIManager;
    private CharacterType currentCharacter = CharacterType.Bee;

    enum CharacterType
    {
        Bee,
        Barnacle,
        Ladybug,
        Saw
    }


    // Use this for initialization
    void Start () {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerAnim = gameObject.GetComponent<Animator>();
        textTrapAnim = GameObject.Find("dialogTextTrap").GetComponent<Animator>();
        textTrap = GameObject.Find("dialogTextTrap").GetComponent<TextTrapScript>();
        gameUIManager = GameObject.Find("UIManager").GetComponent<GameUIManager>();
        textTrap.player = this;

    }
	
	// Update is called once per frame
	void Update () {

        float x = 0;
        float y = 0;
        bool move = false;
        bool isMovementAllowed = false;
        Vector3 direction = Vector3.zero;

        if (IsSolvingTextTrap)
        {
            //do nothing
            return;
        }

        #region ProcessInput

        if (Input.GetKeyDown(KeyCode.D))
        {
            x = movementAmount;
            y = 0;
            move = true;
            direction = Vector3.right;
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            x = -movementAmount;
            y = 0;
            move = true;
            direction = Vector3.left;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            x = 0;
            y = movementAmount;
            move = true;
            direction = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            x = 0;
            y = -movementAmount;
            move = true;
            direction = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ToggleMap();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {

            ToggleSprite();

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            gameUIManager.OpenExitPanel();

        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            //for testing brick break
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, movementAmount, collideWith.value);

            if (hit.collider != null)
            {
                GameObject go = hit.collider.gameObject;
                BreakableBrickTile tileScript = go.GetComponent<BreakableBrickTile>();
                if (tileScript != null)
                {
                    tileScript.Break();
                }
                
            }
        }

        #endregion

        if (move)
        {
            
            //int mask = ~(1 << 8);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, movementAmount, collideWith.value);
            if (hit.collider != null)
            {
                //hit object
                GameObject go = hit.collider.gameObject;
                HiveMindTileBaseScript tileScript = go.GetComponent<HiveMindTileBaseScript>();

                Debug.Log("Hit: " + tileScript.TileType);

                isMovementAllowed = IsMovementAllowed(tileScript.TileType);

            }
            else
            {
                //hit nothing
                isMovementAllowed = true;
            }

            if (isMovementAllowed)
            {
                OpenTextTrap();
                newPosition = new Vector3(transform.position.x + x, transform.position.y + y);
                //transform.position = newPosition;
                StartCoroutine(SmoothMove(transform.position, newPosition, 0.25f));
            }
        }
        
    }

    string GetTileTypeByPosition(Vector3 position)
    {

        int x = Mathf.Abs((int)position.x);
        int y = Mathf.Abs((int)position.y);

        //return currentMap[y].Substring(x, 1);
        return levelData.formattedMapData_layer1[y].Substring(x, 1);

    }

    bool IsMovementAllowed(string TileType)
    {

        if (TileType.IndexOfAny("UB".ToCharArray()) != -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void ToggleMap()
    {
        GameObject Layer1Map = GameObject.Find("GameManager").GetComponent<BoardGenerator>().parent_Layer1;
        GameObject Layer2Map = GameObject.Find("GameManager").GetComponent<BoardGenerator>().parent_Layer2;
        //Background Image
        SpriteRenderer BackgroundImage = GameObject.Find("Background Image").GetComponent<SpriteRenderer>();

        if (Layer1Map.activeInHierarchy)
        {
            //Bad map
            Layer1Map.SetActive(false);
            Layer2Map.SetActive(true);
            Color newCol;
            if (ColorUtility.TryParseHtmlString("#A14242", out newCol))
            {
                BackgroundImage.color = newCol;
            }           
        }
        else
        {
            //Good map
            Layer1Map.SetActive(true);
            Layer2Map.SetActive(false);
            Color newCol;
            if (ColorUtility.TryParseHtmlString("#57C557", out newCol))
            {
                BackgroundImage.color = newCol;
            }          
        }

    }

    void ToggleSprite()
    {
        currentCharacterIndex = (currentCharacterIndex < 3) ? currentCharacterIndex + 1 : 0;

        playerAnim.SetInteger("character", currentCharacterIndex);
        currentCharacter = (CharacterType)currentCharacterIndex;
    }

    private void OpenTextTrap()
    {
        float randomChance = Random.value;

        if (randomChance <= chanceOfTextTrap) {
            //5% chance
            IsSolvingTextTrap = true;
            textTrap.OpenTextTrap();
        }
     
    }

    private void CloseTextTrap()
    {
        IsSolvingTextTrap = false;
        textTrap.CloseTextTrap();
    }

    IEnumerator SmoothMove(Vector3 startPosition, Vector3 endPosition, float time)
    {

        if (!moving)
        {
            moving = true;
            float t = 0f;

            while (t < 1.0f)
            {
                t += Time.deltaTime/time;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }

            moving = false;
        }
       
    }
}