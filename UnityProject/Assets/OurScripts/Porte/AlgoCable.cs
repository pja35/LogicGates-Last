using System.Collections.Generic;
using UnityEngine;


public class AlgoCable : MonoBehaviour {
	/// <summary>Résolution de l'algorithme des cables.</summary>
	/// <param name="start">Le gameObject de départ, ici donc le OutPut</param>
	/// <param name="target">Les coordonnées de la cible à atteindre.</param>
	static public List<Vector3> resolve(GameObject start, Vector3 target){
		
		/// <summary> La liste des coordonnées des positions des ancres par lesquelles le fil passe </summary>
		List<Vector3> ListPositions = new List<Vector3>();
		/// <summary>Coordonnées de la position actuelle de l'algorithme</summary>
		Vector3 posAct= start.transform.position;
		/// <summary>Coordonnées cibles de l'Algorithme.</summary>
		Vector3 posFin= target;
		/// <summary>La liste de toutes les ancres du niveau actuel.</summary>
		List<AnchorState> anchors = GameObject.Find("GridHolder").GetComponent<GridCreater>().anchor_list;
		// Trouve l'ancre la plus proche du départ.
		AnchorState anchor = FindNearAnchor(anchors, posAct);

		if(posAct.x > posFin.x){ // Il faut aller vers la gauche
			ListPositions = AlgoCable.resolveTopLeft(posAct,posFin,anchor);	
		}
		else if(posAct.x <= posFin.x){ // Il faut aller vers la droite
			ListPositions = AlgoCable.resolveTopRight(posAct,posFin,anchor);
		}
		return ListPositions;
	}

	/// <summary>Sous fonction de la résolution de l'algorithme des cables: Le vecteur directeur global va vers les y positifs et x négatifs.</summary>
	/// <param name="start">Les coordonnées de départ, ici celles de l'OutPut</param>
	/// <param name="target">Les coordonnées de la cible à atteindre.</param>
	/// <param name="anchor">L'ancre la plus proche du départ.</param>
	private static List<Vector3> resolveTopLeft(Vector3 start, Vector3 target,AnchorState anchor){
		/// <summary>La matrice de toutes les ancres: le 0 0 se trouve en bas à gauche car la première ancre est celle qui a les plus petites coordonnées.</summary>
		AnchorState[,] anchor_mat = GameObject.Find("GridHolder").GetComponent<GridCreater>().anchor_mat;
		/// <summary> La liste des coordonnées des positions des ancres par lesquelles le fil passe </summary>	
		List<Vector3> ListPositions = new List<Vector3>();
		ListPositions.Add(new Vector3(start.x,start.y,1));
		/// <summary> Ancre actuelle de l'algorithme </summary>
		AnchorState actAnc = anchor;
		actAnc=anchor_mat[(int)actAnc.GetGridPos().x,((int)actAnc.GetGridPos().y)+1];
		/// <summary>Coordonnées de la position actuelle de l'algorithme</summary>
		Vector3 actPos=actAnc.GetPosition();;
		ListPositions.Add(actPos);
		while(actPos != target){ // Tant que l'algorithme n'est pas arrivé jusqu'à la coordonnée cible:
			if((actPos.x-target.x)-(target.y-actPos.y)<0){//Si il faut plus monter qu'aller à gauche:
				actAnc=anchor_mat[(int)actAnc.GetGridPos().x,(int)actAnc.GetGridPos().y+1];
				if(actAnc.GetPosition().y>target.y){// Condition d'arrêt: si la prochaine ancre est plus loin que la cible alors on préfère choisir directement la cible.
						actPos=target;
				}
				else{
					actPos=actAnc.GetPosition();
				}
				ListPositions.Add(actPos);
			}
			else{// Si il faut plus aller à gauche que monter:
                actAnc =anchor_mat[(int)actAnc.GetGridPos().x-1,(int)actAnc.GetGridPos().y];
                
				if(actAnc.GetPosition().x<target.x){// Condition d'arrêt: si la prochaine ancre est plus loin que la cible alors on préfère choisir directement la cible.
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

	/// <summary>Sous fonction de la résolution de l'algorithme des cables: Le vecteur directeur global va vers les y positifs et x positifs.</summary>
	/// <param name="start">Les coordonnées de départ, ici celles de l'OutPut</param>
	/// <param name="target">Les coordonnées de la cible à atteindre.</param>
	/// <param name="anchor">L'ancre la plus proche du départ.</param>
	private static List<Vector3> resolveTopRight(Vector3 start, Vector3 target,AnchorState anchor){
		/// <summary>La matrice de toutes les ancres: le 0 0 se trouve en bas à gauche car la première ancre est celle qui a les plus petites coordonnées.</summary>
		AnchorState[,] anchor_mat = GameObject.Find("GridHolder").GetComponent<GridCreater>().anchor_mat;
		/// <summary> La liste des coordonnées des positions des ancres par lesquelles le fil passe </summary>	
		List<Vector3> ListPositions = new List<Vector3>();
		ListPositions.Add(new Vector3(start.x,start.y,1));
		/// <summary> Ancre actuelle de l'algorithme </summary>
		AnchorState actAnc = anchor;
		actAnc=anchor_mat[(int)actAnc.GetGridPos().x,((int)actAnc.GetGridPos().y)+1];
		/// <summary>Coordonnées de la position actuelle de l'algorithme</summary>
		Vector3 actPos=actAnc.GetPosition();;
		ListPositions.Add(actPos);
		while(actPos != target){// Tant que l'algorithme n'est pas arrivé jusqu'à la coordonnée cible:
			if((target.x-actPos.x)-(target.y-actPos.y)<0){//Si il faut plus monter qu'aller à droite:
				actAnc=anchor_mat[(int)actAnc.GetGridPos().x,((int)actAnc.GetGridPos().y)+1];
				if(actAnc.GetPosition().y>target.y){// Condition d'arrêt: si la prochaine ancre est plus loin que la cible alors on préfère choisir directement la cible.
					actPos=target;
				}
				else{
					actPos=actAnc.GetPosition();
				}
				ListPositions.Add(actPos);
			}
			else {// Si il faut plus aller à droite que monter:
				actAnc=anchor_mat[((int)actAnc.GetGridPos().x)+1,(int)actAnc.GetGridPos().y];
				if(actAnc.GetPosition().x>target.x){// Condition d'arrêt: si la prochaine ancre est plus loin que la cible alors on préfère choisir directement la cible.
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

	/// <summary>Convertit une liste de Vector3 en Tableau de Vector3 de la même taille.</summary>
	/// <param name="list">La liste de Vecteur à convertir en tableau.</param>
	static public Vector3[] listToArray(List<Vector3> list){
		Vector3[] vectArray = new Vector3[list.Count];
		for(int index=0;index<list.Count;index++){
			vectArray[index]=list[index];
		}
		return vectArray;
	}
	/// <summary>Modification de FindNearAnchor présente dans le Util spécialement pour l'Algo.</summary>
	/// <param name="anchorList">List des ancres à parcourir</param>
	/// <param name="toMove">La cible.</param>
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
