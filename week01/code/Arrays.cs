public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: First, I need to create a double array to hold the results with the size of 'length'.
        // Step 2: Then, I will use a for loop to iterate from 0 up to 'length'.
        // Step 3: Inside the loop, calculate the multiple. Since the index 'i' starts at 0, I have to multiply 'number' by (i + 1).
        // Step 4: Save the calculated value in the current index of the array, and return the array at the end.

        double[] results = new double[length];

        for (int i = 0; i < length; i++)
        {
            results[i] = number * (i + 1);
        }

        return results;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Step 1: I need to find the split point. This is the total count of the list minus the 'amount' we want to rotate.
        // Step 2: Extract the items from that split point to the end using GetRange, and save them in a temporary list.
        // Step 3: Remove those same items from the end of the original list so they don't duplicate.
        // Step 4: Finally, insert the temporary list at the very beginning (index 0) of the original list.

        int splitIndex = data.Count - amount;

        List<int> tail = data.GetRange(splitIndex, amount);
        data.RemoveRange(splitIndex, amount);
        data.InsertRange(0, tail);
    }
}