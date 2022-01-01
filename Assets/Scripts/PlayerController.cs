using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public enum playerState {Body, Light, Swapping, Interacting, Inventory};

    public playerState myState;

    private Transform body;
    private SpriteRenderer bodySprite;
    private Rigidbody2D rb_body;
    private Animator body_anim;

    private Transform lightHolder;
    private Transform lightBall;
    private Rigidbody2D rb_light;
    private Animator light_anim;

    public GameObject lightRemnant;

    private Animator head_anim;
    private Transform head_pointer;

    private CameraFollow camFollow;
    private Transform bodyCameraTarget;
    private Transform lightCameraTarget;

    [Header("Variables")]

    public float moveSpeed = 3f;
    public float lightSpeed = 5f;
    public float rotationSpeed = 10f;
    Vector2 movement;

    private static PlayerController _player;
    public static PlayerController Instance { get { return _player; } }

    //interaction stuff
    [Header("Interaction")]

    public Texture2D cursorDefault;
    public Texture2D cursorInteract;

    private int interactablesMask = 0;

    public Interactable currentInteractable;

    //dialogue
    [Header("Dialogue")]

    public RectTransform dialoguePanelRect;

    public RectTransform topPosition;
    public RectTransform bottomPosition;

    //inventory
    [Header("Inventory")]

    public RectTransform inventoryPanelRect;

    public RectTransform leftPosition;
    public RectTransform rightPosition;

    public List<Item> inventory;

    public GameObject itemDisplayPrefab;
    public Transform inventoryItemParent;

    public GameObject itemDescriptionPanel;
    public TextMeshProUGUI itemDescriptionText;

    //currently equipped item
    private string equippedItem = null;

    public Image itemThumbnail;
    private GameObject itemThumbnailParent;

    void Awake()
    {
        //singleton
        if (_player != null && _player != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _player = this;
        }

        //variable fetching
        myState = playerState.Body;
        body = transform.Find("Body");
        bodySprite = body.GetComponent<SpriteRenderer>();
        rb_body = body.GetComponent<Rigidbody2D>();
        body_anim = body.GetComponent<Animator>();

        lightHolder = body.Find("LightHolder");
        lightBall = lightHolder.Find("LightBall");
        lightBall.localScale = new Vector3(0, 0, 0);
        rb_light = lightBall.GetComponent<Rigidbody2D>();
        light_anim = lightBall.GetComponent<Animator>();

        lightBall.gameObject.SetActive(false);

        head_anim = body.transform.Find("Head").GetComponent<Animator>();
        head_pointer = head_anim.gameObject.transform.Find("Pointer");

        camFollow = transform.Find("Player Camera").GetComponent<CameraFollow>();

        bodyCameraTarget = body.Find("Body Camera Target");
        lightCameraTarget = lightBall.Find("LightBall Camera Target");

        camFollow.player = bodyCameraTarget;

        SetCursor("default");

        interactablesMask = LayerMask.GetMask("Interactable");

        inventoryPanelRect.gameObject.SetActive(false);

        itemThumbnailParent = itemThumbnail.transform.parent.gameObject;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(myState == playerState.Body)
        {
            //body movement
            body_anim.SetFloat("Speed", movement.sqrMagnitude);
            if(movement.x < 0)
            {
                if(bodySprite.flipX == true)
                {
                    //flip light holder
                    lightHolder.localPosition = new Vector3(lightHolder.localPosition.x * -1, lightHolder.localPosition.y, lightHolder.localPosition.z);
                }
                bodySprite.flipX = false;
            }
            else if (movement.x > 0)
            {
                if (bodySprite.flipX == false)
                {
                    //flip light holder
                    lightHolder.localPosition = new Vector3(lightHolder.localPosition.x * -1, lightHolder.localPosition.y, lightHolder.localPosition.z);
                }
                bodySprite.flipX = true;
            }

            //check mouse position for interactable
            Vector2 interactRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D interactHit = Physics2D.Raycast(interactRay, Vector2.zero, Mathf.Infinity, interactablesMask);

            if (interactHit)
            {
                Interactable i = interactHit.collider.GetComponent<Interactable>();
                if (i.inRange)
                {
                    SetCursor("interact");
                    currentInteractable = i;
                    TooltipUI.ShowTooltip_Static(i.gameObject.name);
                }
            }
            else
            {
                SetCursor("default");
                currentInteractable = null;
                TooltipUI.HideTooltip_Static();
            }

            //on click, check for interactable
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(currentInteractable != null)
                {
                    //we found an interactable!
                    StartInteraction();
                }
            }

            //check for mode transition
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //start transition to light mode
                myState = playerState.Swapping;
                body_anim.SetFloat("Speed", 0);
                body_anim.Play("bodyLightOut");

                //reset cursor and tooltip
                SetCursor("default");
                TooltipUI.HideTooltip_Static();

                //hide cursor
                Cursor.visible = false;
            }

            //check for opening inventory
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                ShowInventory();
            }
        }
        else if(myState == playerState.Light)
        {
            //check for mode transition
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //start animations to transition back to body mode
                myState = playerState.Swapping;
                body_anim.Play("bodyLightIn");
            }
        }
        else if(myState == playerState.Interacting)
        {
            //check for button select with number keys
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (currentInteractable != null && currentInteractable.myTalk.choicesParent.childCount > 0)
                {
                    currentInteractable.myTalk.choicesParent.GetChild(0).GetComponent<Button>().onClick.Invoke();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (currentInteractable != null && currentInteractable.myTalk.choicesParent.childCount > 1)
                {
                    currentInteractable.myTalk.choicesParent.GetChild(1).GetComponent<Button>().onClick.Invoke();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (currentInteractable != null && currentInteractable.myTalk.choicesParent.childCount > 2)
                {
                    currentInteractable.myTalk.choicesParent.GetChild(2).GetComponent<Button>().onClick.Invoke();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (currentInteractable != null && currentInteractable.myTalk.choicesParent.childCount > 3)
                {
                    currentInteractable.myTalk.choicesParent.GetChild(3).GetComponent<Button>().onClick.Invoke();
                }
            }
        }
        else if (myState == playerState.Inventory)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                HideInventory();
            }
        }
    }

    void FixedUpdate()
    {
        //player movement
        if(myState == playerState.Body)
        {
            rb_body.MovePosition(rb_body.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

            //turn head to face cursor
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - head_pointer.transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion headRot = Quaternion.Lerp(head_pointer.transform.rotation, Quaternion.Euler(0f, 0f, rotation_z), Time.deltaTime * rotationSpeed);
            head_pointer.transform.rotation = headRot;

            //set head sprite to face cursor
            float angle = 1 - (headRot.eulerAngles.z / 360);
            head_anim.SetFloat("Rotation", angle);
        }
        else if (myState == playerState.Light)
        {
            rb_light.MovePosition(rb_light.position + movement.normalized * lightSpeed * Time.fixedDeltaTime);
        }
    }

    public void MoveDialoguePanelToTop()
    {
        dialoguePanelRect.pivot = topPosition.pivot;
        dialoguePanelRect.anchorMax = topPosition.anchorMax;
        dialoguePanelRect.anchorMin = topPosition.anchorMin;
        dialoguePanelRect.anchoredPosition = topPosition.anchoredPosition;
    }

    public void MoveDialoguePanelToBottom()
    {
        dialoguePanelRect.pivot = bottomPosition.pivot;
        dialoguePanelRect.anchorMax = bottomPosition.anchorMax;
        dialoguePanelRect.anchorMin = bottomPosition.anchorMin;
        dialoguePanelRect.anchoredPosition = bottomPosition.anchoredPosition;
    }

    public void MoveInventoryPanelToLeft()
    {
        inventoryPanelRect.pivot = leftPosition.pivot;
        inventoryPanelRect.anchorMax = leftPosition.anchorMax;
        inventoryPanelRect.anchorMin = leftPosition.anchorMin;
        inventoryPanelRect.anchoredPosition = leftPosition.anchoredPosition;
    }

    public void MoveInventoryPanelToRight()
    {
        inventoryPanelRect.pivot = rightPosition.pivot;
        inventoryPanelRect.anchorMax = rightPosition.anchorMax;
        inventoryPanelRect.anchorMin = rightPosition.anchorMin;
        inventoryPanelRect.anchoredPosition = rightPosition.anchoredPosition;
    }

    #region Transition

    public void LightOut()
    {
        //play head animation
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = bodySprite.flipX;
        head_anim.Play("headLightOut");

        //activate light ball
        lightBall.gameObject.SetActive(true);

        //have camera follow light
        camFollow.player = lightCameraTarget;
    }

    public void FreeLight()
    {
        //allow light to roam freely
        lightBall.parent = null;
        myState = playerState.Light;
    }

    public void LightIn()
    {
        //snap!
        Instantiate(lightRemnant, lightBall.position + (Vector3)lightBall.GetComponent<CircleCollider2D>().offset, lightBall.rotation);

        //return light to parent
        lightBall.parent = lightHolder;
        lightBall.localPosition = new Vector3(0, 0, 0);
        lightBall.localScale = new Vector3(0, 0, 0);
        lightBall.gameObject.SetActive(false);

        //zero out head rotation and play animation
        if (bodySprite.flipX)
        {
            head_pointer.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        }
        else
        {
            head_pointer.transform.rotation = Quaternion.Euler(0f, 0f, 225f);
        }
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = bodySprite.flipX;
        head_anim.Play("headLightIn");

        //set camera to follow body
        camFollow.player = bodyCameraTarget;
    }

    public void ActivateHead()
    {
        //deactivate light
        lightBall.gameObject.SetActive(false);
    }

    public void ActivateBody()
    {
        //set body mode
        myState = playerState.Body;

        //free head to turn
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        head_anim.Play("headSpin");

        //enable cursor
        Cursor.visible = true;
    }

    #endregion

    #region Interaction

    //set the mouse indicator
    public void SetCursor(string state)
    {
        if(state == "default")
        {
            Cursor.SetCursor(cursorDefault, new Vector2(0, 0), CursorMode.Auto);
        }
        else if (state == "interact")
        {
            Cursor.SetCursor(cursorInteract, new Vector2(0, 0), CursorMode.Auto);
        }
    }

    public static Interactable GetInteractable()
    {
        return _player.currentInteractable;
    }

    public static void LeavingInteractable(Interactable i)
    {
        if(_player.currentInteractable == i)
        {
            _player.SetCursor("default");
            _player.currentInteractable = null;
            TooltipUI.HideTooltip_Static();
        }
    }

    //start talking
    public void StartInteraction()
    {
        myState = playerState.Interacting;
        SetCursor("interact");
        TooltipUI.HideTooltip_Static();

        currentInteractable.Interact();

        //set dialogue panel position
        if (Camera.main.WorldToScreenPoint(body.position).y > Camera.main.scaledPixelHeight / 2)
        {
            //move dialogue panel to bottom
            MoveDialoguePanelToBottom();
        }
        else
        {
            //move dialogue panel to top
            MoveDialoguePanelToTop();
        }

        //check if there is an equipped item to display
        if(equippedItem == null)
        {
            itemThumbnailParent.SetActive(false);
        }
        else
        {
            itemThumbnailParent.SetActive(true);

            //find image corresponding to equipped item
            for (int i = 0; i < inventory.Count; i++)
            {
                if(inventory[i].itemName == equippedItem)
                {
                    itemThumbnail.sprite = inventory[i].itemSprite;
                }
            }
        }

        //freeze body
        body_anim.SetFloat("Speed", 0);
    }

    //done talking
    public static void EndInteraction()
    {
        _player.myState = playerState.Body;
    }

    #endregion

    #region Inventory

    public void ShowInventory()
    {
        //set state
        myState = playerState.Inventory;
        inventoryPanelRect.gameObject.SetActive(true);

        //set position of inventory panel
        if (Camera.main.WorldToScreenPoint(body.position).x > Camera.main.scaledPixelWidth / 2)
        {
            //move dialogue panel to left
            MoveInventoryPanelToLeft();
        }
        else
        {
            //move dialogue panel to right
            MoveInventoryPanelToRight();
        }

        //populate inventory
        foreach (Transform child in inventoryItemParent.transform)
        {
            Destroy(child.gameObject);
        }

        if (_player.inventory.Count > 0)
        {
            for(int i = 0; i < _player.inventory.Count; i++)
            {
                Item thisItem = _player.inventory[i];
                //instantiate item display prefab
                GameObject thisItemDisplay = Instantiate(itemDisplayPrefab, inventoryItemParent);
                thisItemDisplay.transform.Find("Image").GetComponent<Image>().sprite = thisItem.itemSprite;
                thisItemDisplay.transform.Find("Name").GetComponent<TextMeshProUGUI>().SetText(thisItem.itemName);
                thisItemDisplay.GetComponent<ItemHandler>().myItemName = thisItem.itemName;
            }

            //check for equipped item to display
            if (_player.equippedItem != null)
            {
                EquipItem(_player.equippedItem);
            }
        }

        //reset cursor and tooltip
        SetCursor("default");
        TooltipUI.HideTooltip_Static();

        //freeze body
        body_anim.SetFloat("Speed", 0);
    }

    public void HideInventory()
    {
        //set state
        myState = playerState.Body;
        inventoryPanelRect.gameObject.SetActive(false);
        itemDescriptionPanel.SetActive(false);
    }

    public static void EquipItem(string itemName)
    {
        UnequipAllItems();

        _player.equippedItem = itemName;

        for (int i = 0; i < _player.inventoryItemParent.childCount; i++)
        {
            TextMeshProUGUI thisTMP = _player.inventoryItemParent.GetChild(i).Find("Name").GetComponent<TextMeshProUGUI>();
            if (thisTMP.text == itemName)
            {
                _player.SetTMPToEquipped(thisTMP);
            }
        }
    }

    public static void UnequipAllItems()
    {
        _player.equippedItem = null;

        for (int i = 0; i < _player.inventoryItemParent.childCount; i++)
        {
            //set display to default
            TextMeshProUGUI thisTMP = _player.inventoryItemParent.GetChild(i).Find("Name").GetComponent<TextMeshProUGUI>();
            _player.SetTMPToUnequipped(thisTMP);
        }
    }

    public static string GetEquippedItem()
    {
        if(_player.equippedItem == null)
        {
            return "None";
        }
        else
        {
            return _player.equippedItem;
        }
    }

    public void SetTMPToEquipped(TextMeshProUGUI thisTMP)
    {
        thisTMP.color = Color.yellow;
    }

    public void SetTMPToUnequipped(TextMeshProUGUI thisTMP)
    {
        thisTMP.color = Color.white;
    }

    public static void ShowItemDescription(string name)
    {
        _player.itemDescriptionPanel.SetActive(true);
        _player.itemDescriptionText.SetText(GetItem(name).itemDescription);
    }

    public static void HideItemDescription()
    {
        _player.itemDescriptionPanel.SetActive(false);
    }

    public static void AddItem(Item i)
    {
        //add item to inventory
        _player.inventory.Add(i);
    }

    public static void RemoveItem(string i)
    {
        //remove item from inventory
        if(_player.inventory.Count > 0)
        {
            foreach(Item thisItem in _player.inventory)
            {
                if (thisItem.itemName.Equals(i))
                {
                    _player.inventory.Remove(thisItem);
                }
            }
        }
    }
    public static bool CheckForItem(string i)
    {
        //check if player has item in inventory
        foreach(Item thisItem in _player.inventory)
        {
            if(thisItem.itemName.Equals(i))
            {
                return true;
            }
        }
        return false;
    }

    public static Item GetItem(string i)
    {
        foreach (Item thisItem in _player.inventory)
        {
            if (thisItem.itemName.Equals(i))
            {
                return thisItem;
            }
        }
        return null;
    }

    #endregion
}
