using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class GenerateTrialOrder: MonoBehaviour
{
    public List<int> distanceTrial {get; set;}
    private List<int> distanceArray = new List<int> { 6, 6, 6, 8, 8, 8, 10, 10, 10 };

    void Awake()
    {
        distanceTrial = shuffle(distanceArray);

        // insert dummy trials: 7 and 9
        System.Random rnd = new System.Random();
        var index = rnd.Next(0, distanceTrial.Count); // exclusive upper bound
        distanceTrial.Insert(index, 7);
        index = rnd.Next(0, distanceTrial.Count);
        distanceTrial.Insert(index, 9);

        // add practice trials: 7 and 9
        distanceTrial.Insert(0, 7);
        distanceTrial.Insert(0, 9);

        for (int i = 0; i< distanceTrial.Count; i++)
            Debug.Log(distanceTrial[i]);
    }



    // version generate by ChatGPT
    // more randomed - but the time complexity is bad if we have a large size input
    public List<int> shuffle(List<int> array)
    {
        // Shuffle the array using Fisher-Yates shuffle algorithm
        System.Random random = new System.Random();
        for (int i = array.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        // Ensure no two consecutive values are the same
        for (int i = 1; i < array.Count; i++)
        {
            while (array[i] == array[i - 1])
            {
                int j = random.Next(i, array.Count);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
        return array;
    }





    // rearrange numbers in an Array such that no two numbers are adjacent (greedy approach with heap)
    // https://www.geeksforgeeks.org/rearrange-numbers-in-an-array-such-that-no-two-adjacent-numbers-are-same/
    public List<int> GenerateTrial(List<int> arr, int N)
    {
            // Store frequencies of all elements
            // of the array
            Dictionary<int, int> mp = new Dictionary<int, int>();
            Dictionary<int, int> visited = new Dictionary<int, int>();

            for (int i = 0; i < N; i++)
            {
                if (mp.ContainsKey(arr[i]))
                {
                    mp[arr[i]] += 1;
                }
                else
                {
                    mp[arr[i]] = 1;
                }
            }

            List<Tuple<int, int>> pq = new List<Tuple<int, int>>();

            // Adding high freq elements
            // in descending order
            for (int i = 0; i < N; i++)
            {
                int val = arr[i];

                if (mp.ContainsKey(val) && (!visited.ContainsKey(val) || visited[val] != 1))
                {
                    pq.Add(new Tuple<int, int>(mp[val], val));
                }
                visited[val] = 1;
            }

            pq.Sort();
            pq.Reverse();

            // 'result[]' that will store resultant value
            List<int> result = new List<int>();

            // Work as the previous visited element
            // initial previous element will be ( '-1' and
            // it's frequency wiint also be '-1' )
            Tuple<int, int> prev = new Tuple<int, int>(-1, -1);
            int l = 0;

            // Traverse queue
            while (pq.Count != 0)
            {

                // Pop top element from queue and add it
                // to result
                Tuple<int, int> k = pq[0];
                pq.RemoveAt(0);
                // result[l] = k.Item2;
                result.Add(k.Item2);

                // If frequency of previous element is less
                // than zero that means it is useless, we
                // need not to push it
                if (prev.Item1 > 0)
                {
                    pq.Add(prev);
                    // pq.Sort();
                    pq = pq.OrderBy(x => x.Item1).ToList(); // https://stackoverflow.com/questions/20672048/sort-a-list-of-double-based-on-first-element-of-array
                    pq.Reverse();
                }

                // Make current element as the previous
                // decrease frequency by 'one'
                prev = new Tuple<int, int>(k.Item1 - 1, k.Item2);
                l++;
            }
        return result;
    }



}
