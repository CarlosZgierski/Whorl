using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
 

    int NextSegmentToCreate = 0;
    public GameObject[] SelectedSegments;
    public List<GameObject> instantiatedSegments = new List<GameObject>();
    public GameObject NextInstantiatedSegment;

    private Vector3 pastExitPoint;
    public int LevelLengthInSegments = 8;
    public GameObject StartSegment;
    public GameObject EndSegment;
    public List<GameObject> PossibleSegments = new List<GameObject>();
    public string seed;

    void Start()
    {
        SelectedSegments = SelectSegments();
        InstantiateNextSegment();
        InstantiateNextSegment();
        

    }

    public int GetCurrentSegmentIndex()
    {
        return NextSegmentToCreate - 2;
    }

    private int GetSegmentIndexBySegment(GameObject x)
    {
        for(int i = 0; i < instantiatedSegments.Count; i++)
        {
            if(instantiatedSegments[i] == x)
            {
                return i;
            }
        }
        return -1;
    }


    public void SetActiveOfPastSegmentTo(bool x, GameObject currentSegmentGameObject)
    {
        int currentSegmentIndex = GetSegmentIndexBySegment(currentSegmentGameObject);
        if (currentSegmentIndex != 0)
          instantiatedSegments[currentSegmentIndex - 1].SetActive(x);
    }

    public void SetActiveOfNextSegmentTo(bool x, GameObject currentSegmentGameObject)
    {
        int currentSegmentIndex = GetSegmentIndexBySegment(currentSegmentGameObject);
        if (currentSegmentIndex != LevelLengthInSegments - 1)
        instantiatedSegments[currentSegmentIndex + 1].SetActive(x);
    }

    public string GetCurrentSegmentName()
    {
        return (SelectedSegments[NextSegmentToCreate-2].name);
    }

    GameObject[] SelectSegments()
    {
        GameObject[] x = new GameObject[LevelLengthInSegments];
        x[0] = StartSegment;
        for (int i = 1; i < x.Length - 1; i++)
        {
            int RandomIndex = Random.Range(0, PossibleSegments.Count);

            x[i] = PossibleSegments[RandomIndex];
            PossibleSegments.Remove(PossibleSegments[RandomIndex]);
        }
        x[x.Length - 1] = EndSegment;
        return x;
    }

    public GameObject GetCurrentSegment()
    {
        return SelectedSegments[NextSegmentToCreate - 2];
    }

    public void InstantiateNextSegment()
    {

        if (NextSegmentToCreate < LevelLengthInSegments)
        {
            NextInstantiatedSegment = Instantiate(SelectedSegments[NextSegmentToCreate]);
            instantiatedSegments.Add(NextInstantiatedSegment);
            if (pastExitPoint != null)
            {
                NextInstantiatedSegment.transform.position = pastExitPoint;
            }
            Segment segmentComponent = NextInstantiatedSegment.GetComponent<Segment>();
            pastExitPoint = segmentComponent.Exit.position;
            if(NextSegmentToCreate == (LevelLengthInSegments /2) + 1 || NextSegmentToCreate == LevelLengthInSegments - 2)
            {
                segmentComponent.ActivateSacrificeTrigger();
            }
            if (NextSegmentToCreate != 0)
            {
                NextInstantiatedSegment.gameObject.SetActive(false);
            }
        }
       
        NextSegmentToCreate++;
    }
}