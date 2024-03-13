using System.Collections;
using System.Collections.Generic;
using System;


public static class GlobalMethods
{
    // version generate by ChatGPT
    // more randomed - but the time complexity is bad if we have a large size input
    public static List<int> shuffle(List<int> array)
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
}
