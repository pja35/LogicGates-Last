using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoCable : MonoBehaviour {

	
	

	static public List<Vector3> resolve(GameObject start, Vector3 target){
		List<Vector3> ListPositions = new List<Vector3>();
		Vector3 posAct= start.transform.position;
		Vector3 posFin= target;
		List<AnchorState> anchors = GameObject.Find("GridHolder").GetComponent<GridCreater>().anchor_list;
		AnchorState anchor = FindNearAnchor(anchors, posAct);

		if(posAct.x > posFin.x){ // Il faut aller vers la gauche
			ListPositions = AlgoCable.resolveTopLeft(posAct,posFin,anchor);	
		}
		else if(posAct.x <= posFin.x){ // Il faut aller vers la droite
			ListPositions = AlgoCable.resolveTopRight(posAct,posFin,anchor);
		}
		return ListPositions;
	}

	private static List<Vector3> resolveTopLeft(Vector3 start, Vector3 target,AnchorState anchor){
		AnchorState[,] anchor_mat = GameObject.Find("GridHolder").GetComponent<GridCreater>().anchor_mat;
		List<Vector3> ListPositions = new List<Vector3>();
		ListPositions.Add(new Vector3(start.x,start.y,1));
		AnchorState actAnc = anchor;
		actAnc=anchor_mat[(int)actAnc.GetGridPos().x,((int)actAnc.GetGridPos().y)+1];
		Vector3 actPos=actAnc.GetPosition();;
		ListPositions.Add(actPos);
		while(actPos != target){
			if((actPos.x-target.x)<(target.y-actPos.y)){
				actAnc=anchor_mat[(int)actAnc.GetGridPos().x,(int)actAnc.GetGridPos().y+1];
				if(actAnc.GetPosition().y>target.y){
						actPos=target;
				}
				else{
					actPos=actAnc.GetPosition();
				}
				ListPositions.Add(actPos);
			}
			else{
                actAnc =anchor_mat[(int)actAnc.GetGridPos().x-1,(int)actAnc.GetGridPos().y];
                
				if(actAnc.GetPosition().x<target.x){
					actPos=target;
				}
				else{
					actPos=actAnc.GetPosition();
				}
				ListPositions.Add(actPos);
			}
		}
		return ListPositions;
	}

	private static List<Vector3> resolveTopRight(Vector3 start, Vector3 target,AnchorState anchor){
		AnchorState[,] anchor_mat = GameObject.Find("GridHolder").GetComponent<GridCreater>().anchor_mat;
		List<Vector3> ListPositions = new List<Vector3>();
		ListPositions.Add(new Vector3(start.x,start.y,1));
		AnchorState actAnc = anchor;
		actAnc=anchor_mat[(int)actAnc.GetGridPos().x,((int)actAnc.GetGridPos().y)+1];
		Vector3 actPos=actAnc.GetPosition();;
		ListPositions.Add(actPos);
		while(actPos != target){
			if((target.x-actPos.x)-(target.y-actPos.y)<0){
				actAnc=anchor_mat[(int)actAnc.GetGridPos().x,((int)actAnc.GetGridPos().y)+1];
				if(actAnc.GetPosition().y>target.y){
					actPos=target;
				}
				else{
					actPos=actAnc.GetPosition();
				}
				ListPositions.Add(actPos);
			}
			else {
				actAnc=anchor_mat[((int)actAnc.GetGridPos().x)+1,(int)actAnc.GetGridPos().y];
				Debug.Log(Vector3.Distance(actPos,actAnc.GetPosition())-Vector3.Distance(actPos,target));
				if(actAnc.GetPosition().x>target.x){
					actPos=target;
				}
				else{
					actPos=actAnc.GetPosition();
				}
				ListPositions.Add(actPos);
			}
		}
		return ListPositions;
	}

	static public Vector3[] listToArray(List<Vector3> list){
		Vector3[] vectArray = new Vector3[list.Count];
		for(int index=0;index<list.Count;index++){
			vectArray[index]=list[index];
		}
		return vectArray;
	}

	public static AnchorState FindNearAnchor(List<AnchorState> anchorList, Vector3 toMove){

        Vector3 toMovePos = toMove;
        //On initialise avec le premier element pour faciliet le calcul.
        AnchorState nearest = anchorList[0];
        float distance = float.MaxValue;

        foreach (AnchorState actPos in anchorList)
        {
            float dist_act = Vector3.Distance(actPos.GetPosition(), toMovePos);
            if (dist_act < distance)
            {
                distance = dist_act;
                nearest = actPos;
            }
        }//si aucune ancre libre n'as été trouvée.
        return nearest;
    }
}
