using UnityEngine;

public class Gate : MonoBehaviour, IFixable, INotifiable
{
    /// <summary>Toutes les entrées de la porte.</summary> 
    private Obj_Input[] inputs;
    /// <summary>Toutes les sorties de la porte.</summary> 
    private Obj_Output[] outputs;
    /// <summary>Cette variable permet de mettre en place le patron stratégie.</summary>
    public Comportement gateBehaviour;
    
    private int maxIO;
    private int anchorsX;
    private int anchorsY;
    private int spaceForIO;

    /// <summary>
    /// Permet à la porte de lancer une vérification de l'état lorsqu'une de ses entrées utilise cette méthode.
    /// </summary>
    public void notify()
    {
        for (int i = 0; i < outputs.Length; i++)
        {
            if (gateBehaviour != null)
            {
                outputs[i].value = gateBehaviour.CalculateOut(inputs);
                outputs[i].notify();
            } else
            {
                outputs[i].value = false;
                outputs[i].notify();
            }
        }
    }

    /// <summary>
    /// Notife d'un changement d'etat et détruit les connexions invalides
    /// </summary>
    public void OnFix()
    {
        notify();
        OnInput onIn =
            delegate (Obj_Input applyTo) { applyTo.DestroyInvalidConnexion(); };
        OnOutput onOut =
            delegate (Obj_Output applyTo) { applyTo.DestroyInvalidConnexion(); };
        ApplyOnIO(onIn, onOut);

    }

    private delegate void OnInput(Obj_Input input);
    private delegate void OnOutput(Obj_Output output);


    private void ApplyOnIO(OnInput onIn, OnOutput onOut)
    {
        foreach (Obj_Input inAct in inputs)
        {
            onIn(inAct);

        }
        foreach (Obj_Output outAct in outputs)
        {
            onOut(outAct);
        }
    }
    
    /// <summary>
    /// Ne Fait rien
    /// </summary>
    public void OnUnfix()
    {
        return;
    }

    /// <summary>
    /// Désactive les IO
    /// </summary>
    public void DisbaleIO()
    {
        OnInput onIn =
            delegate (Obj_Input applyTo) { applyTo.gameObject.SetActive(false); };
        OnOutput onOut =
            delegate (Obj_Output applyTo) { applyTo.gameObject.SetActive(false); };
        ApplyOnIO(onIn, onOut);

    }

    /// <summary>
    /// Réactive les IO
    /// </summary>
    public void EnableIO()
    {
        OnInput onIn =
           delegate (Obj_Input applyTo) { applyTo.enabled = true;};
        OnOutput onOut =
            delegate (Obj_Output applyTo) { applyTo.enabled = true;};
        ApplyOnIO(onIn, onOut);
    }

    /// <summary>
    /// Met à jour le traçé de fin des fils
    /// </summary>
     public void UpdateEndOfLine()
    {
        OnInput onIn =
           delegate (Obj_Input applyTo) { applyTo.GetComponent<Obj_Input>().UpdateEndOfLine(); };
        OnOutput onOut =
            delegate (Obj_Output applyTo) { applyTo.GetComponent<Obj_Output>().UpdateEndOfLine(); };
        ApplyOnIO(onIn, onOut);
    }

    /// <summary>
    /// Met à jour tout les fils
    /// </summary>
    public void UpdateLineDraw()
    {
        OnInput onIn =
          delegate (Obj_Input applyTo) { applyTo.GetComponent<Obj_Input>().UpdateLineDraw(); };
        OnOutput onOut =
            delegate (Obj_Output applyTo) { applyTo.GetComponent<Obj_Output>().UpdateLineDraw(); };
        ApplyOnIO(onIn, onOut);
    }

   /// <summary>
   /// Configure le nombre d'IO d'une porte et son comportement
   /// </summary>
   /// <param name="nbInputs">Nombre d'entrées</param>
   /// <param name="nbOutputs">Nombre de sorties</param>
   /// <param name="comp">Le comportement de la porte</param>
     public void CreateGateIO(int nbInputs, int nbOutputs, Comportement comp)
    {
        gateBehaviour = comp;
        inputs = new Obj_Input[nbInputs];
        outputs = new Obj_Output[nbOutputs];
        maxIO = Mathf.Max(nbInputs, nbOutputs);
        
        ResizeGate(Mathf.Max(nbInputs, nbOutputs));
        InstantiateIO(nbInputs,nbOutputs);

        PlaceIO(nbInputs, maxIO, inputs);
        PlaceIO(nbOutputs, maxIO, outputs); 
        
    }


    private void InstantiateIO(int nbIn,int nbOut)
    {
        for (int i = 0; i < nbIn; i++)
        {
            inputs[i] = Obj_Input.CreateInput(gameObject, i);
        }
        for (int i = 0; i < nbOut; i++)
        {
            outputs[i] = Obj_Output.CreateDisconactableOutput(gameObject, i);
        }
    }

    private void PlaceIO(int nbIO, int maxIO, IInOut[] IO)
    {
        if (nbIO % 2 == 0)
        {
            PlaceEvenIO(nbIO, maxIO, IO);
        }
        else
        {
            PlaceOddIO(nbIO, maxIO, IO);
        }
    }

    private void PlaceOddIO(int nb_outputs,int maxIO, IInOut[] toPlace)
    {
        Vector3 distBetweenAnchors = GridCreater.GetDistBtwnAnchors();
        int nbAnchors = (maxIO * 2) + 1;
        int anchorsToSkip = 2 - maxIO%2;
        float percentBeteweenAnchors = 1f / (nbAnchors - 1);
        float percentToSkip = percentBeteweenAnchors * anchorsToSkip;

        for (int i = 0; i < nb_outputs; i++)
        {
            Vector3 abc = toPlace[i].transform.localScale;
            SpriteUtil.SetGameObjectToAbsSize(toPlace[i].gameObject, distBetweenAnchors.x  + (0.4f * distBetweenAnchors.x), distBetweenAnchors.y + (0.4f * distBetweenAnchors.y));
            toPlace[i].transform.localPosition = new Vector3(-0.5f + (i+((maxIO+1)/2 - (nb_outputs+1)/2)) * (percentBeteweenAnchors*2 ) + percentToSkip, toPlace[i].GetYPlacement(), -1f);
            
        }
    }

    private void PlaceEvenIO(int nb_outputs,int maxIO, IInOut[] toPlace)
    {
        Vector3 distBetweenAnchors = GridCreater.GetDistBtwnAnchors();
        int nbAnchors = (maxIO * 2) + 1;
        int anchorsToSkip = 1 + maxIO%2;
        float percentBeteweenAnchors = 1f / (nbAnchors - 1);
        float percentToSkip = percentBeteweenAnchors * anchorsToSkip;

        for (int i = 0; i < toPlace.Length; i++)
        {
            Vector3 abc = toPlace[i].transform.localScale;
            SpriteUtil.SetGameObjectToAbsSize(toPlace[i].gameObject, distBetweenAnchors.x + (0.4f * distBetweenAnchors.x), distBetweenAnchors.y + (0.4f * distBetweenAnchors.y));
            toPlace[i].transform.localPosition = new Vector3(-0.5f + (i+(maxIO/2 - nb_outputs/2 )) * (percentBeteweenAnchors*2) + percentToSkip   , toPlace[i].GetYPlacement(), -1f);
        }
    }
    
	private void ResizeGate(int nb_IO)
    {
        Vector3 distanceAnchors = GridCreater.GetDistBtwnAnchors();
        int sizeYAdjuster = nb_IO % 2 == 0 ? 0: 1;
        anchorsX = nb_IO * 2;
        anchorsY = nb_IO + sizeYAdjuster;
        spaceForIO = 2;
        float sizeX = distanceAnchors.x * anchorsX;
        float sizeY = distanceAnchors.y * anchorsY;
        SpriteUtil.SetGameObjectToAbsSize(gameObject, sizeX, sizeY);
    }

    /// <summary>
    /// Détruit la porte et les objets qui luis sont attaché.
    /// </summary>
    public void Destroy()
    {
        foreach (Obj_Input inact in inputs)
        {
            inact.DestroyIn();
        }
        foreach (Obj_Output outact in outputs)
        {
            outact.DestroyOut();
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Renvoi le nombre d'entrées
    /// </summary>
    public int NbIn()
    {
        return inputs.Length;
    }

    /// <summary>
    /// Renvoi le nombre de sorties
    /// </summary>
    public int NbOut()
    {
        return outputs.Length;
    }

    /// <summary>
    /// Actualise la couleur des portes en cas de changement dans les paramètres
    /// </summary>
    public void RefreshColor()
    {
        gameObject.GetComponent<Renderer>().material.color = ParametersLoader.GetColor();
    }

    /// <summary>
    /// Essaie de connecter une sortie d'une porte à l'entrée d'une autre
    /// </summary>
    /// <param name="outputNumber">Le numéro de sortie à connecter</param>
    /// <param name="toConnect">L'objet auquel se connecter</param>
    /// <param name="inputNumber">Le numéro d'entrée à connecter</param>
    public void TryToConnect(int outputNumber,GameObject toConnect, int inputNumber)
    {
        if (Obj_Output.TryToConnect(outputs[outputNumber], toConnect, inputNumber))
        {
            outputs[outputNumber].disconectable = false;
        }
    }

    /// <summary>
    /// Renvoi le maximum entre le nombre d'entrées et le nombre de sorties
    /// </summary>
    public int GetMaxIO()
    {
        return maxIO;
    }

    /// <summary>
    /// Renvoi le nombre d'ancres que l'ancre occupe à l'horizontal
    /// </summary>
    public int GetAnchorsX()
    {
        return anchorsX;
    }

    /// <summary>
    /// Renvoi le nombre d'ancres que l'ancre occupe à la verticale
    /// </summary>
    public int GetAnchorsY()
    {
        return anchorsY;
    }

    /// <summary>
    /// Renvoi l'éspace à conserver entre les portes pour éviter les collisions entre IO
    /// </summary>
    public int GetSpaceForIO()
    {
        return spaceForIO;
    }
}