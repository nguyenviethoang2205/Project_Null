﻿using System.Collections.Generic;
using UnityEngine;

public static class Data{

    public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
    public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);

    // Xoay Tetromino
    public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };
    
    // Tạo tọa độ Tetromino ban đầu
    public static readonly Dictionary<Tetromino, Vector2Int[]> Cells = new Dictionary<Tetromino, Vector2Int[]>(){
        { Tetromino.I, new Vector2Int[]{ new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int(2, 1) } },
        { Tetromino.J, new Vector2Int[]{ new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int(1, 0) } }, 
        { Tetromino.L, new Vector2Int[]{ new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int(1, 0) } }, 
        { Tetromino.O, new Vector2Int[]{ new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 0, 0), new Vector2Int(1, 0) } }, 
        { Tetromino.S, new Vector2Int[]{ new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0) } }, 
        { Tetromino.T, new Vector2Int[]{ new Vector2Int( 0, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int(1, 0) } }, 
        { Tetromino.Z, new Vector2Int[]{ new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 0, 0), new Vector2Int(1, 0) } }, 
    };

    // Tọa độ của Tetromino I khi đụng tường
    private static readonly Vector2Int[,] WallKicksI = new Vector2Int[,] {
        { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2, -1), new Vector2Int( 1,  2) },
        { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2,  1), new Vector2Int(-1, -2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1,  2), new Vector2Int( 2, -1) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1, -2), new Vector2Int(-2,  1) },
        { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2,  1), new Vector2Int(-1, -2) },
        { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2, -1), new Vector2Int( 1,  2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1, -2), new Vector2Int(-2,  1) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1,  2), new Vector2Int( 2, -1) },
    };

    // Tọa độ của các Tetromino không phải I khi đụng tường
    private static readonly Vector2Int[,] WallKicksJLOSTZ = new Vector2Int[,]{
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,  1), new Vector2Int(0, -2), new Vector2Int(-1, -2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, -1), new Vector2Int(0,  2), new Vector2Int( 1,  2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, -1), new Vector2Int(0,  2), new Vector2Int( 1,  2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,  1), new Vector2Int(0, -2), new Vector2Int(-1, -2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,  1), new Vector2Int(0, -2), new Vector2Int( 1, -2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0,  2), new Vector2Int(-1,  2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0,  2), new Vector2Int(-1,  2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,  1), new Vector2Int(0, -2), new Vector2Int( 1, -2) },
    };

    // Ánh xạ các Tetromino vào các WallKicks tương ứng khi đụng tường
    public static readonly Dictionary<Tetromino, Vector2Int[,]> WallKicks = new Dictionary<Tetromino, Vector2Int[,]>(){
        {Tetromino.I, WallKicksI },
        {Tetromino.J, WallKicksJLOSTZ },
        {Tetromino.L, WallKicksJLOSTZ },
        {Tetromino.O, WallKicksJLOSTZ },
        {Tetromino.S, WallKicksJLOSTZ },
        {Tetromino.T, WallKicksJLOSTZ },
        {Tetromino.Z, WallKicksJLOSTZ },
    };
}