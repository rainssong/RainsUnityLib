using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.rainssong.ui
{
    public class LoopList : MonoBehaviour
    {

        private ScrollRect myScrollView;
        private RectTransform myContent;

        public GameObject CellPerfab;

        /// <summary>
        /// 当前场景中生成的Cell对象列表
        /// </summary>
        public List<GameObject> CellItemList = new List<GameObject> ();
        /// <summary>
        /// Cell与分配给他的Index对应表
        /// </summary>
        public Dictionary<GameObject, int> CellIndexDic = new Dictionary<GameObject, int> ();
        [Header ("参数设置")]
        public RectOffset MyGridPadding;
        public Vector2 MyGridSpacing;
        public Vector2 MyGridCellSize;
        /// <summary>
        /// 创建在场景中的Item数量个数参数
        /// </summary>
        private int creatCount = 15;
        /// <summary>
        /// 当前数据的Item总数
        /// </summary>
        private int dataCount = 68;
        /// <summary>
        ///非法的起始索引
        /// </summary>
        private const int invalidStartIndex = -1;
        /// <summary>
        /// 当前Content的Y值下第一个开始的Index值
        /// </summary>
        private int StartIndex;
        /// <summary>
        /// 一行Cell的个数
        /// </summary>
        public int rowCellNum = 3;
        /// <summary>
        /// 上方缓存行数
        /// </summary>
        private int OffsetTopLineNum = 2;
        /// <summary>
        /// 下方缓存行数
        /// </summary>
        private int OffsetBottomLineNum = 2;
        void Start ()
        {
            Init ();

        }

        void Init ()
        {
            CellItemList.Clear ();
            CellIndexDic.Clear ();

            myScrollView = GetComponent<ScrollRect> ();
            myContent = myScrollView.content;
            MyGridCellSize = CellPerfab.GetComponent<RectTransform> ().sizeDelta;

            #region Content参数初始化
            rowCellNum = (int) (myScrollView.GetComponent<RectTransform> ().sizeDelta.x / MyGridCellSize.x);
            SetContentAnchors ();
            myContent.sizeDelta = SetContentSize ();
            myContent.anchoredPosition = Vector2.zero;
            #endregion

            StartIndex = 0;
            for (int i = 0; i < creatCount; i++)
            {
                CreatCell (i);
            }

            myScrollView.onValueChanged.RemoveAllListeners ();
            myScrollView.onValueChanged.AddListener (OnValueChanged);

        }

        private void OnValueChanged (Vector2 arg0)
        {
            int CurrentIndex = GetCurrentIndex ();
            if (CurrentIndex != StartIndex && CurrentIndex > invalidStartIndex)
            {
                StartIndex = CurrentIndex;
                //为了将需要缓存的也先生出来，以免玩家滑动太快，显示不过来。
                //如果不考虑缓存，min应该为StartIndex，max应该为StartIndex+creatCount
                int min = Mathf.Max (StartIndex - OffsetTopLineNum * rowCellNum, 0);
                int max = Mathf.Min (StartIndex + creatCount + OffsetBottomLineNum * rowCellNum, dataCount);
                for (int i = CellItemList.Count - 1; i >= 0; i--)
                {
                    GameObject go = CellItemList[i];
                    int index = CellIndexDic[go];
                    if (index < min || index > max)
                    {
                        ReturnCell (go);
                    }
                }

                float maxCreat = Mathf.Min (StartIndex + creatCount, dataCount);
                bool isExit = false;

                for (int i = min; i < max; i++)
                {
                    isExit = false;
                    for (int j = 0; j < CellItemList.Count; j++)
                    {
                        GameObject cell = CellItemList[j];
                        if (CellIndexDic[cell] == i)
                        {
                            isExit = true;
                            break;
                        }
                    }
                    if (isExit)
                    {
                        continue;
                    }
                    CreatCell (i);
                }
                #region 
                /* for (int i = StartIndex ; i < maxCreat; i++)
                 {
                     isExit = false;
                     for (int j = 0; j < CellItemList .Count ; j++)
                     {
                         GameObject cell = CellItemList[j];
                         if(CellIndexDic [cell]==i )
                         {
                             isExit = true;
                             break;
                         }
                     }
                     if(isExit )
                     {
                         continue;
                     }
                     CreatCell(i);
                 }*/
                #endregion

            }
        }
        private int GetCurrentIndex ()
        {
            int index = 0;
            if (myScrollView.vertical)
            {
                index = Mathf.FloorToInt (myContent.anchoredPosition.y / (MyGridCellSize.y + MyGridSpacing.y));
            }
            return index * rowCellNum;
        }
        private void CreatCell (int cellIndex)
        {
            GameObject cell = GetItemInPool ();
            cell.SetActive (true);
            cell.transform.name = cellIndex.ToString ();
            cell.transform.Find ("Image/ID").gameObject.GetComponent<Text> ().text = cellIndex.ToString ();
            cell.transform.parent = myContent;
            RectTransform rect = cell.GetComponent<RectTransform> ();
            rect.pivot = new Vector2 (0, 1);
            //垂直拉动以左上为锚点
            rect.anchorMin = new Vector2 (0, 1);
            rect.anchorMax = new Vector2 (0, 1);
            cell.transform.localPosition = GetCellPosition (cellIndex);
            CellItemList.Add (cell);
            CellIndexDic.Add (cell, cellIndex);
        }

        private Vector3 GetCellPosition (int cellIndex)
        {
            Vector3 tempvec = Vector3.zero;
            if (myScrollView.vertical && myScrollView.horizontal == false)
            {
                int row = cellIndex / rowCellNum;
                int column = cellIndex % rowCellNum;
                tempvec.x = MyGridCellSize.x * column + MyGridSpacing.x * (column) + MyGridPadding.left;
                tempvec.y = -(MyGridCellSize.y * row + MyGridSpacing.y * (row) + MyGridPadding.top);
            }
            return tempvec;
        }
        private void ReturnCell (GameObject cell)
        {
            cell.SetActive (false);
            CellItemList.Remove (cell);

            CellIndexDic[cell] = invalidStartIndex;
            ReturnItemToPool (cell);
            CellIndexDic.Remove (cell);
        }
        #region Content相关设置方法
        private Vector2 SetContentSize ()
        {
            Vector2 tempContentSize = Vector2.zero;
            if (myScrollView == null) return tempContentSize;
            if (myScrollView.vertical && myScrollView.horizontal == false)
            {
                int row = Mathf.CeilToInt ((float) dataCount / rowCellNum);
                tempContentSize.x = myContent.sizeDelta.x;
                tempContentSize.y = (MyGridCellSize.y * (row) + MyGridSpacing.y * (row) + MyGridPadding.top);
            }

            return tempContentSize;
        }
        private void SetContentAnchors ()
        {
            if (myScrollView == null) return;
            //仅仅考虑当前只能垂直拉动或水平拉动情况
            if (myScrollView.vertical && myScrollView.horizontal == false)
            {
                //垂直拉动以上方为锚点
                myContent.anchorMin = new Vector2 (0, 1);
                myContent.anchorMax = new Vector2 (1, 1);
            }
            else if ((!myScrollView.vertical) && myScrollView.horizontal)
            {
                //水平拉动以左边为锚点
                myContent.anchorMin = new Vector2 (0, 0);
                myContent.anchorMax = new Vector2 (0, 1);
            }
        }
        #endregion
        #region 简单对象池

        private Stack<GameObject> itemPools = new Stack<GameObject> ();
        // private int poolsMaxCount = 40;
        public GameObject GetItemInPool ()
        {
            GameObject item = null;
            if (itemPools.Count > 0)
            {
                item = itemPools.Pop ();
            }
            if (item == null)
            {
                item = Instantiate (CellPerfab);
            }
            return item;
        }

        public void ReturnItemToPool (GameObject item)
        {
            UnityEngine.Object.Destroy (item);
            //if (item == null) return;
            //if (itemPools.Count > poolsMaxCount)
            //{
            //    DestroyObject(item);
            //}
            //else
            //{
            //    itemPools.Push(item);
            //}
        }
        #endregion
    }

}