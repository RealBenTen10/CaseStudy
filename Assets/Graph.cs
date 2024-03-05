using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;
public class Graph
{
    private float[,] adjacencyMatrix;
    private int numNodes;
    private float best_weight = float.MaxValue;
    private int[] bestPath;

    public Graph(float[,] matrix)
    {
        adjacencyMatrix = matrix;
        numNodes = matrix.GetLength(0);
        bestPath = new int[numNodes];
    }

    public int[] FindBestPath()
    {
        
        int[] currentPath = new int[numNodes];
        bool[] visited = new bool[numNodes];
        float currentWeight = 0;

        // Start the DFS from each node to find the best path? 
        int startNode = 0;
        Array.Clear(visited, 0, numNodes);
        
        Debug.Log("Computing best path");
        DFS(startNode, startNode, visited, currentPath, ref currentWeight, 0);

        //Debug.Log("End - BestPath: " + bestPath[0] + "-" + bestPath[1] + "-" + bestPath[2] + "-" + bestPath[3] + "-" + bestPath[4]);
        return bestPath;
    }
    private void DFS(int startNode, int currentNode, bool[] visited, int[] path, ref float currentWeight, int depth)
    {
        visited[currentNode] = true;
        //Debug.Log("Depth: "+depth + "| CurrentNode: "+ currentNode);
        //Debug.Log("Visited: " + visited[0] + "|" + visited[1] + "|" + visited[2] + "|" + visited[3] + "|" + visited[4] + "|");
        path[depth] = currentNode;
        if (depth < (numNodes-1))
        {
            //Debug.Log("In if-Clause");

            int[] best_three_nodes = new int[] { 0, 0, 0 };
            float[] best_three_nodes_weight = new float[] { float.MaxValue, float.MaxValue, float.MaxValue };
            //Debug.Log("currentNode: " + currentNode);
            for (int nextNode = 0; nextNode < numNodes; nextNode++)
            {
                
                // Check if the edge is not part of the best path - check if already visited
                float weight_of_nextNode = adjacencyMatrix[currentNode, nextNode];
                if (!visited[nextNode] && weight_of_nextNode > 0)
                {
                    // 
                    int biggest_weight_index = 0;
                    float biggest_weight = best_three_nodes_weight[biggest_weight_index];
                    for (int i = 1; i <  best_three_nodes.Length; i++)
                    {
                        if (biggest_weight < best_three_nodes_weight[i])
                        {
                            biggest_weight_index = i;
                            biggest_weight = best_three_nodes_weight[i];
                        }
                    }
                    if (weight_of_nextNode < biggest_weight)
                    {
                        best_three_nodes[biggest_weight_index] = nextNode;
                        best_three_nodes_weight[biggest_weight_index] = weight_of_nextNode;
                    }
                    
                }
            }
            //Debug.Log("Best_three_nodes: " + best_three_nodes[0] + " | " + best_three_nodes[1] + " | ");
            //Debug.Log("2nd if-Clause");
            for (int i = 0; i < best_three_nodes.Length; i++)
            {
                //Debug.Log("Best_three_nodes: " + best_three_nodes[i]);
                if (best_three_nodes[i] != 0)
                {
                    currentWeight += best_three_nodes_weight[i];
                    depth++;
                    DFS(currentNode, best_three_nodes[i], visited, path, ref currentWeight, depth);
                    visited[best_three_nodes[i]] = false;
                    depth--;
                    currentWeight -= best_three_nodes_weight[i];
                }
            }
        }
        else
        {
            //Debug.Log("BestWeight: " + best_weight + " vs CurrentWeight: " + currentWeight);
            //Debug.Log("CurrentPath[1]: " + path[1] + "| BestPath[1]: " + bestPath[1]);
            if (best_weight > currentWeight) { best_weight = currentWeight; Array.Copy(path, bestPath, numNodes); } //Debug.Log("Set bestPath"); }
        }
        
    }
}
