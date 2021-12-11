using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnapsackPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public KnapsackPanel Instance;

    public Button EquipmentBut;
    public Button MaterialBut;
    public Button ConsualBut;

    public Button AddItemBut;

    public GameObject EquipmentPanel;
    public GameObject MaterialPanel;
    public GameObject ConsualPanel;

    private Sprite lightBut;
    private Sprite nolightBut;

    private Dictionary<int, Item> itemDic = new Dictionary<int, Item>();
    private void Awake()
    {
        Instance = this;

        EquipmentBut = this.gameObject.transform.Find("EquipmentButton").GetComponent<Button>();
        MaterialBut = this.gameObject.transform.Find("MaterialButton").GetComponent<Button>();
        ConsualBut = this.gameObject.transform.Find("ConsuButton").GetComponent<Button>();

        //AddItemBut = this.gameObject.transform.Find("Button").GetComponent<Button>();

        EquipmentBut.onClick.AddListener(equipmentButCall);
        MaterialBut.onClick.AddListener(materialButCall);
        ConsualBut.onClick.AddListener(consualButCall);

        AddItemBut.onClick.AddListener(addItemButCall);

        lightBut = Resources.Load<Sprite>("ui/lightButton");
        nolightBut = Resources.Load<Sprite>("ui/noLightbutton");

        initItem();

        Debug.Log(itemDic.Count);
    }

    private void addItemButCall()
    {
        int id = UnityEngine.Random.Range(1, 72);
        AddItem(id);
    }
    private void equipmentButCall()
    {
        EquipmentBut.gameObject.GetComponent<Image>().sprite = lightBut;
        MaterialBut.gameObject.GetComponent<Image>().sprite = nolightBut;
        ConsualBut.gameObject.GetComponent<Image>().sprite = nolightBut;
        EquipmentPanel.SetActive(true);
        MaterialPanel.SetActive(false);
        ConsualPanel.SetActive(false);
    }
    private void materialButCall()
    {
        EquipmentBut.gameObject.GetComponent<Image>().sprite = nolightBut;
        MaterialBut.gameObject.GetComponent<Image>().sprite = lightBut;
        ConsualBut.gameObject.GetComponent<Image>().sprite = nolightBut;
        EquipmentPanel.SetActive(false);
        MaterialPanel.SetActive(true);
        ConsualPanel.SetActive(false);
    }
    private void consualButCall()
    {
        EquipmentBut.gameObject.GetComponent<Image>().sprite = nolightBut;
        MaterialBut.gameObject.GetComponent<Image>().sprite = nolightBut;
        ConsualBut.gameObject.GetComponent<Image>().sprite = lightBut;
        EquipmentPanel.SetActive(false);
        MaterialPanel.SetActive(false);
        ConsualPanel.SetActive(true);

    }

    private void initItem()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("ui/Bowandarrow");
        int id = 0;
        id = initIten(ItemType.Equipment, id, sprites);

        sprites = Resources.LoadAll<Sprite>("ui/Helmet");
        id = initIten(ItemType.Equipment, id, sprites);

        sprites = Resources.LoadAll<Sprite>("ui/Sword");
        id = initIten(ItemType.Equipment, id, sprites);

        sprites = Resources.LoadAll<Sprite>("ui/Consumable");
        id = initIten(ItemType.Consuable, id, sprites);

        sprites = Resources.LoadAll<Sprite>("ui/Material");
        id = initIten(ItemType.Material, id, sprites);


    }

    private int initIten(ItemType _type, int _id, Sprite[] _sprites)
    {
        int id = _id;
        for(int i = 0; i < _sprites.Length; i++) 
        {
            id += 1;
            Item item = new Item();
            item.type = _type;
            item.id = id;
            item.sprite = _sprites[i];

            if(!itemDic.ContainsKey(id))
            {
                itemDic.Add(id, item);
            }
        }
        return id;
    }
    void Start()
    {
        equipmentButCall();
    }

    // Update is called once per frame
    private void AddItem(Item _item)
    {
        Grid grid = getGridByType(_item); 
            if(grid == null)
            {
                Debug.Log("添加物品失败，背包已满");
                return;
            }

            grid.SetGrid(_item);
       
    }

    public void AddItem(int _id)
    {
        Item item = null;
        if(itemDic.ContainsKey(_id))
        {
            itemDic.TryGetValue(_id, out item);

            if(item != null)
            {
                AddItem(item);
            }
        }
        Debug.Log("id不存在");
    }
    private Grid getGridByType(Item _item)
    {
        Grid[] grids = null;
        Grid grid = null;

        switch(_item.type)
        {
            case ItemType.Equipment:
                grids = gameObject.transform.Find("EquipmentPanel").GetComponentsInChildren<Grid>();
                foreach(Grid g in grids)
                {
                    if(g.GetCount() == 0)
                    {
                        grid = g;
                        break;
                    }
                }
                return grid;
                break;
            case ItemType.Material:
                grids = gameObject.transform.Find("MaterialPanel").GetComponentsInChildren<Grid>();
                break;
            case ItemType.Consuable:
                grids = gameObject.transform.Find("ConsuPanel").GetComponentsInChildren<Grid>();
                break;
            default:
                break;
        }
        Grid rg = null;

        foreach (Grid g in grids)
        {
            if (g.GetCount() > 0 && g.GetItemId() == _item.id)
            {
                rg = g;
                return rg;
            }
            if (g.GetCount() == 0)
            {
                if (grid == null)
                    grid = g;

            }
        }
        if (rg != null) grid = rg;
        return grid;
    }

    
}
