using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    // Start is called before the first frame update
    private Image image;
    private Text textCount;

    private int count;

    private Item item;

    private void Awake()
    {
        image = this.gameObject.transform.Find("Image").GetComponent<Image>();
        textCount = this.gameObject.transform.Find("Text").GetComponent<Text>();
        image.gameObject.SetActive(false);
        textCount.gameObject.SetActive(false);
    }

    public void SetGrid(Item _item)
    {
        if (_item == null)
        {
            return;
        }
        else
        {
            if(item == null)
            {
                item = _item;
            }
        }
        if (_item.type == ItemType.Equipment && this.count == 0)
        {
            image.gameObject.SetActive(true);
            if(image == null)
            {
                Debug.Log("image == null");
            }
            image.sprite = _item.sprite;
            count += 1;
            return;
        }
        if(_item.type != ItemType.Equipment && this.count == 0)
        {
            image.gameObject.SetActive(true);
            textCount.gameObject.SetActive(true);
            image.sprite = _item.sprite;
            count += 1;
            textCount.text = count.ToString();
            return;
        }
        count += 1;
        textCount.text = count.ToString();


    }

    public int GetCount() { return this.count; }

    public int GetItemId()
    {
        if (this.item != null)
        {
            return item.id;
        }
        return 0;
    } 
}
