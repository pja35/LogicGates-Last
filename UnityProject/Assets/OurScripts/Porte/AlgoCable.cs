using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère le tracé des cables
/// </summary>
public class AlgoCable : MonoBehaviour {


	
	/// <summary>Résolution de l'algorithme des cables.</summary>
	/// <param name="start">Le gameObject de départ, ici donc le OutPut</param>
	/// <param name="target">Les coordonnées de la cible à atteindre.</param>
	static public List<IAnchor> Resolve(GameObject start, Vector3 target){
		IAnchor[,] anchor_mat = GameObject.Find("GridHolder").GetComponent<GridCreater>().GetMatAnchor();
		/// <summary> La liste des coordonnées des positions des ancres par lesquelles le fil passe </summary>
		List<IAnchor>  	ListAnchors = new List<IAnchor>();
		/// <summary>Coordonnées de la position actuelle de l'algorithme</summary>
		IAnchor pred= null;
		Debug.Log("Start position:"+start.transform.position);
		Debug.Log("Target position:"+target);
		IAnchor ancAct = GridUtil.NearestAnchorOfAnchorMat(start.transform.position); // Trouve l'ancre la plus proche du départ.
		IAnchor ancTrg = GridUtil.NearestAnchorOfAnchorMat(target); // Trouve l'ancre la plus proche de la cible.
		if(ancAct.GetGridPos().y > ancTrg.GetGridPos().y-1 ){
			Debug.LogError("Violation de restriction de positionnement.");
			return null;
		}
		else{

		  	ListAnchors.Add((ancAct));
			int cpt=0;
			while( !isover(ancAct,ancTrg)){
				cpt++;
				IAnchor ancNew = null;
				if(ancAct.GetGridPos().y < ancTrg.GetGridPos().y-1 ){ // Il faut monter
					
					ancNew=anchor_mat[(int)ancAct.GetGridPos().x,(int)ancAct.GetGridPos().y+1];

					if(ancNew.IsWireFree()&&!ListAnchors.Contains(ancNew)){
						pred=ancAct;
						ancAct = ancNew;						
					  	ListAnchors.Add((ancAct));
						continue;
					}
					Debug.Log("Need up! But not free");
				}
				
				if(ancAct.GetGridPos().x > ancTrg.GetGridPos().x){ // Il faut aller vers la gauche
					ancNew = anchor_mat[(int)ancAct.GetGridPos().x-1,(int)ancAct.GetGridPos().y];
					if(ancNew.IsWireFree()&&!ListAnchors.Contains(ancNew)){
						pred=ancAct;
						ancAct = ancNew;						
					  	ListAnchors.Add((ancAct));
						continue;
					}
					Debug.Log("Need Left! But not Free");
					ancNew=anchor_mat[((int)ancAct.GetGridPos().x)+1,(int)ancAct.GetGridPos().y];
					if(ancNew.IsWireFree()&&!ListAnchors.Contains(ancNew)){
						pred=ancAct;
						ancAct = ancNew;						
					  	ListAnchors.Add((ancAct));
						continue;
					}
					
				}

				else{// Il faut aller vers la droite
				
					ancNew=anchor_mat[((int)ancAct.GetGridPos().x)+1,(int)ancAct.GetGridPos().y];
				
					if(ancNew.IsWireFree()&&!ListAnchors.Contains(ancNew)){
						pred=ancAct;
						ancAct = ancNew;						
					  	ListAnchors.Add((ancAct));
						continue;
					}
					Debug.Log("Need Right! But not Free!");
					ancNew = anchor_mat[(int)ancAct.GetGridPos().x-1,(int)ancAct.GetGridPos().y];
					
					if(ancNew.IsWireFree()&&!ListAnchors.Contains(ancNew)){
						pred=ancAct;
						ancAct = ancNew;
					  	ListAnchors.Add((ancAct));
						continue;
					}
					
				}
				ancNew=anchor_mat[(int)ancAct.GetGridPos().x,(int)ancAct.GetGridPos().y-1];
				pred=ancAct;
				ancAct = ancNew;
				ListAnchors.Add((ancAct));	
				
			}
			ancAct=anchor_mat[(int)ancAct.GetGridPos().x,(int)ancAct.GetGridPos().y+1];
			ListAnchors.Add(ancAct);
			return ListAnchors;
		}
	}

	private static bool isover (IAnchor ancAct,IAnchor ancTrg){
		return (ancAct.GetGridPos().y == ancTrg.GetGridPos().y-1 ) && ( ancAct.GetGridPos().x == ancTrg.GetGridPos().x );  
	}

    
	
}
