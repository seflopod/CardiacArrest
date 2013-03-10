using UnityEngine;
using System.Collections;
 
public class ScrollingBackground : MonoBehaviour 
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
    private string textureName = "_MainTex";
	private string normalName = "_BumpMap";
 
    Vector2 uvOffset = Vector2.zero;
 
    void LateUpdate() 
    {
        uvOffset += ( uvAnimationRate * Time.deltaTime * GameManager.Speed*2);
        if( renderer.enabled )
        {
            renderer.materials[ materialIndex ].SetTextureOffset( textureName, uvOffset );
			renderer.materials[ materialIndex ].SetTextureOffset( normalName, uvOffset );
        }		
    }
	
	public string TextureName
	{
		get { return this.textureName; }
		set { textureName = value; }
	}
	
	public string NormalName
	{
		get { return this.normalName; }
		set { normalName = value; }
	}
}