using System;
using System.Collections;
using System.Collections.Generic;
using QTea;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MazeController : MonoBehaviour
{
    public readonly struct GeneratedMaze
    {
        public readonly Maze Maze;
        public readonly int Entrance;
        public readonly int Exit;
        public readonly int Columns;
        public readonly int Rows;

        public GeneratedMaze(Maze maze, int entrance, int exit, int columns, int rows)
        {
            Maze = maze;
            Entrance = entrance;
            Exit = exit;
            Columns = columns;
            Rows = rows;
        }
    }
    
    [SerializeField] private UnityEvent<GeneratedMaze> mazeCompletedAction;

    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private int removeWalls = 0;

    private void Start()
    {
        MazeCreator.UnityRandom random = new MazeCreator.UnityRandom();
        int entrance = random.Range(columns-1);
        int exit = random.Range(columns-1);
        MazeGenerator mazeGenerator =
            new MazeGenerator(columns, rows, 
                entrance, exit,
                random, removeWalls);

        Maze maze = mazeGenerator.Generate(0);

        GeneratedMaze generatedMaze = new GeneratedMaze(maze, entrance, exit, columns, rows);
        mazeCompletedAction?.Invoke(generatedMaze);
    }
}