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
    private Poolable myPoolable;
    
    public CharacterData MyCharcterData => myCharcterData;
    public Define.Character CharacterType => myCharcterData.character;
    public Poolable MyPoolable
    {
        get
        {
            if(myPoolable == null)
                myPoolable = GetComponent<Poolable>();
            return myPoolable;
        }
    }
    
    public async UniTask Init(CharacterData data)
    {
        myCharcterData = data;
        spriteRenderer = GetComponent<SpriteRenderer>();
        string addressableKey = new StringBuilder(data.character.ToString().ToLower()).Insert(0, "Art/").ToString();
        spriteRenderer.sprite = await Managers.Resource.LoadAsset<Sprite>(addressableKey);
    }

    public void ClearAndPush()
    {
        Managers.Pool.Push(MyPoolable);
    }
}
