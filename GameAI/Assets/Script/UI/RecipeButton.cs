using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RecipeButton : MonoBehaviour
{
    [SerializeField] private OwnedItemsData.OwnedItem[] useItems; // 材料アイテム
    [SerializeField] private OwnedItemsData.OwnedItem addItem; // 作成できるアイテム
    [SerializeField] private Image addItemIcon; // 作成できるアイテムのアイコン

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            // アイテムを作成
            foreach (var useItem in useItems)
            {
                OwnedItemsData.Instance.Use(useItem.Type, useItem.Number);
            }

            OwnedItemsData.Instance.Add(addItem.Type, addItem.Number);
            OwnedItemsData.Instance.Save();

            // 作成アニメーション
            var icon = Instantiate(addItemIcon, transform.parent.parent);
            icon.transform.position = addItemIcon.transform.position;
            icon.transform.DOScale(Vector3.one * 2, 1f);
            icon.DOColor(Color.clear, 1f)
                .OnComplete(() => { Destroy(icon.gameObject); });

            AudioManager.instance.PlaySE("create_item");

            // 時間経過
            //MainSceneController.Instance.MinutesInGame += MainSceneController.IncrementMinutesPerCreateItem;
        });
    }

    private void Update()
    {
        if (useItems.Any(useItem =>
            !OwnedItemsData.Instance.OwnedItems.Any(x => x.Type == useItem.Type && x.Number >= useItem.Number)))
        {
            // 材料が1種類でも足りなければ、ボタンを押せなくする
            _button.interactable = false;
            return;
        }

        _button.interactable = true;
    }
}