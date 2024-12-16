using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CharacterData myCharcterData;
    
    public CharacterData MyCharcterData => myCharcterData;
    
    public async UniTask Init(CharacterData data)
    {
        myCharcterData = data;
        spriteRenderer = GetComponent<SpriteRenderer>();
        string addressableKey = new StringBuilder(data.character.ToString().ToLower()).Insert(0, "Art/").ToString();
        spriteRenderer.sprite = await Managers.Resource.LoadAsset<Sprite>(addressableKey);
    }
}
