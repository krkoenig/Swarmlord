using UnityEngine;
using System;
using System.Collections;

public class Heap<T> where T : IHeapItem<T>{

    public int Count {
        get
        {
            return m_currItemCount;
        }
    }

    private T[] m_items;
    private int m_currItemCount;


    public Heap(int maxHeapSize)
    {
        m_items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = m_currItemCount;
        m_items[m_currItemCount] = item;

        SortUp(item);
        m_currItemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = m_items[0];
        m_currItemCount--;
        m_items[0] = m_items[m_currItemCount];
        m_items[0].HeapIndex = 0;
        SortDown(m_items[0]);
        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    private void SortDown(T item)
    {
        while(true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < m_currItemCount)
            {
                swapIndex = childIndexLeft;

                if (childIndexRight < m_currItemCount)
                    if (m_items[childIndexLeft].CompareTo(m_items[childIndexRight]) < 0)
                        swapIndex = childIndexRight;

                if (item.CompareTo(m_items[swapIndex]) < 0)
                    Swap(item, m_items[swapIndex]);
                else
                    break;
            }
            else
                break;
           
        }
    }

    private void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while(true)
        {
            T parentItem = m_items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
                Swap(item, parentItem);
            else
                break;
        }
    }

    public bool Contains(T item)
    {
        return Equals(m_items[item.HeapIndex], item);
    }

    private void Swap(T itemA, T itemB)
    {
        m_items[itemA.HeapIndex] = itemB;
        m_items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}
