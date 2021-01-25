using System;

namespace brickwork
{
    class Program
    {
        //NOTE: Solution is not working properly, this is my best shot
        //Algorithm explanation: we keep iterating columns and then rows,
        //checking every single item for its orientation, and based on this,
        //we first iterate the whole array to find 2 matching places,
        //which have different values(because this is the only condition in this task),
        //without rotating the current brick. If we don't find these 2 matching places,
        //we rotate the brick and start looking again for 2 matching places,
        //which are directed the opposite way.
        //EX: if we have horizontal brick -> we look for 2 horizontal places
        //    or if we dont find such -> we look for 2 vertical places
        static int[,] solve(int[,] input)
        {
            //boolean array to keep track of whether we placed any brick at given position
            bool[,] check = new bool[input.GetLength(0), input.GetLength(1)];
            //new output array so we don't change values in the input itself
            int[,] output = new int[input.GetLength(0), input.GetLength(1)];
            int current = 0;
            //flag var to keep track if we made any change
            bool replaced = false;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    replaced = false;
                    current = input[i, j];

                    if (isHorizontal(current, i, j, input))
                    {
                        for (int ii = 0; ii < input.GetLength(0); ii++)
                        {
                            for (int jj = 0; jj < input.GetLength(1) - 1; jj++)
                            {

                                if (input[ii, jj] != input[ii, jj + 1] && !check[ii, jj] && !check[ii, jj + 1] && !replaced && !check[i, j])
                                {
                                    output[ii, jj] = current;
                                    output[ii, jj + 1] = current;
                                    check[ii, jj] = true;
                                    check[ii, jj + 1] = true;
                                    replaced = true;
                                }
                            }
                        }
                        //if it does not fit 2 neighbour horizontal spaces we search for 2 neighbour vertical spaces 
                        if (!replaced)
                        {
                            for (int ii = 0; ii < input.GetLength(0) - 1; ii++)
                            {
                                for (int jj = 0; jj < input.GetLength(1); jj++)
                                {
                                    if (input[ii, jj] != input[ii + 1, jj] && !check[ii, jj] && !check[ii + 1, jj] && !replaced && !check[i, j])
                                    {
                                        output[ii, jj] = current;
                                        output[ii + 1, jj] = current;
                                        check[ii, jj] = true;
                                        check[ii + 1, jj] = true;
                                        replaced = true;
                                    }
                                }
                            }
                        }
                        //if it is vertical, we do the same, reverse way
                    }
                    else
                    {
                        for (int ii = 0; ii < input.GetLength(0) - 1; ii++)
                        {
                            for (int jj = 0; jj < input.GetLength(1); jj++)
                            {
                                if (input[ii, jj] != input[ii + 1, jj] && !check[ii, jj] && !check[ii + 1, jj] && !replaced && !check[i, j])
                                {
                                    output[ii, jj] = current;
                                    output[ii + 1, jj] = current;
                                    check[ii, jj] = true;
                                    check[ii + 1, jj] = true;
                                    replaced = true;
                                }
                            }
                        }
                        if (!replaced)
                        {
                            for (int ii = 0; ii < input.GetLength(0); ii++)
                            {
                                for (int jj = 0; jj < input.GetLength(1) - 1; jj++)
                                {
                                    if (input[ii, jj] != input[ii, jj + 1] && !check[ii, jj] && !check[ii, jj + 1] && !replaced && !check[i, j])
                                    {
                                        output[ii, jj] = current;
                                        output[ii, jj + 1] = current;
                                        check[ii, jj] = true;
                                        check[ii, jj + 1] = true;
                                        replaced = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return output;
        }

        static bool isHorizontal(int curr, int i, int j, int[,] input)
        {
            /*
                this function checks all directions to find neighbour with the same value as the current
                based on this, we can decide whether the brick is horizontal or vertical
                         |
                      __ O __
                         |

            */
            bool match = false;
            //middle
            if (j > 0 && j + 1 < input.GetLength(0) && (input[i, j - 1] == curr || input[i, j + 1] == curr))
            {
                match = true;
            }
            //bottom left
            else if (i == input.GetLength(1) - 1 && j == 0 && input[i, j + 1] == curr)
            {
                match = true;
            }
            //top left
            else if (j == 0 && input[i, j + 1] == curr)
            {
                match = true;
            }
            //top right
            else if (j == input.GetLength(0) - 1 && input[i, j - 1] == curr)
            {
                match = true;
            }
            //bottom right
            else if (i == input.GetLength(1) - 1 && j == input.GetLength(0) - 1 && input[i, j - 1] == curr)
            {
                match = true;
            }
            return match;
        }


        static void Main(string[] args)
        {

            //i am using static array since the point of the task is the algorithm
            int[,] array = new int[,] { { 1, 1, 2, 4}, { 3, 3, 2, 4} };

            array = solve(array);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {

                    Console.WriteLine(array[i, j]);
                }
            }

        }
    }
}
