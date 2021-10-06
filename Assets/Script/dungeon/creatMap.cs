using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class creatMap : MonoBehaviour
{
    [Header("Dungeon Data")]
    [SerializeField] int[,] dungeonMap;
    [SerializeField] int[,] mapDistance;
    int max_distance;
    int[,] map_nodes;
    [SerializeField] int start_x, start_y;
    [SerializeField] RectTransform maps;

    [Header("Layout")]
    [SerializeField] float paddingX;
    [SerializeField] float paddingY;

    private void Awake()
    {
        dungeonMap = new int[12,12];
        mapDistance = new int[12,12];
        // Initialize
        for(int i=0; i<12; i++)
        {
            for(int j=0; j<12; j++)
            {
                dungeonMap[i,j] = 0;
                mapDistance[i,j] = -1;
            }
        }
        makeMap(16);//10개의 맵 생성
        for (int i = 0; i < 12; i++){
            Debug.Log(dungeonMap[i,0]+" "+dungeonMap[i,1]+" "+dungeonMap[i,2]+" "+dungeonMap[i,3]+" "+dungeonMap[i,4]+" "+dungeonMap[i,5]+" "+dungeonMap[i,6]+" "+dungeonMap[i,7]+" "+dungeonMap[i,8]+" "+dungeonMap[i,9]);
            // Debug.Log(i+". "+map_nodes[i,0]+","+map_nodes[i,1]);
        }
        mapView(16);
    }

    void makeMap(int count)
    {
        max_distance = 0;
        //시작위치 랜덤생성
        start_x = Random.Range(1, 11);
        start_y = Random.Range(1, 11);
        dungeonMap[start_x, start_y] = 1;
        mapDistance[start_x, start_y] = 0;
        map_nodes = new int[count,2];
        map_nodes[0,0] = start_x;
        map_nodes[0,1] = start_y;
        for(int i=1; i<count; i++)
        {
            max_distance = makeOneMap(i);
        }
    }

    int makeOneMap(int nowMapNum)
    {
        int point = Random.Range(0,nowMapNum);
        int x = map_nodes[point,0];
        int y = map_nodes[point,1];
        int distance = mapDistance[x,y];
        int[,] option = new int[4,2] {{-1,0},{0,-1},{1,0},{0,1}};
        int[,] possible_list = new int[4,2];
        int possible_count = 0;
        int temp_x,temp_y,temp_count;
        for(int i = 0; i < 4; i++)
        {
            temp_count = 0;
            temp_x = x + option[i,0];
            temp_y = y + option[i,1];
            // Debug.Log(i+"temp "+temp_x+","+temp_y);
            if(temp_x != 0 && temp_x != 11 && temp_y != 0 && temp_y != 11 && dungeonMap[temp_x, temp_y]!=1)
            {
                for(int j = 0; j < 4; j++)
                    if(dungeonMap[temp_x+option[j,0], temp_y+option[j,1]] == 1)
                        temp_count++;
                // Debug.Log("tempcount=" + temp_count);
                if(temp_count == 1)
                {
                    possible_list[possible_count, 0] = temp_x;
                    possible_list[possible_count, 1] = temp_y;
                    possible_count++;
                }
            }
        }
        // Debug.Log(possible_count);
        if (possible_count > 0)
        {
            point = Random.Range(0, possible_count);
            x = possible_list[point,0];
            y = possible_list[point,1];
            // Debug.Log("choice"+x+","+y);
            map_nodes[nowMapNum,0] = x;
            map_nodes[nowMapNum,1] = y;
            dungeonMap[x, y] = 1;
            mapDistance[x, y] = distance+1;
            if (max_distance < distance+1)
                max_distance = distance+1;
        }
        else
            return makeOneMap(nowMapNum);

        return distance+1;
    }

    void mapView(int count)
    {
        GameObject mapPrefab = Resources.Load<GameObject>("Prefab/Map/Node");
        List<GameObject> mapList = new List<GameObject>();

        for(int i = 1; i < 12; i++)
        {
            for(int j = 1; j < 12; j++)
            {
                if(dungeonMap[i,j] == 1)
                {
                    GameObject mapObj = Instantiate(mapPrefab);
                    RectTransform nodeTransform = mapObj.GetComponent<RectTransform>();
                    nodeTransform.SetParent(maps);
                    nodeTransform.anchoredPosition = new Vector2(+paddingX*i, +paddingY*j);
                    if(start_x == i && start_y == j)
                    {
                        mapObj.GetComponent<Image>().color = Color.red;
                        // 시작 노드는 항상 중앙으로 오게 만듦
                        Vector2 distFromCenter = nodeTransform.anchoredPosition - maps.anchoredPosition;
                        maps.anchoredPosition = maps.anchoredPosition - distFromCenter;
                    }
                }
            }
        }
        
    }
}
