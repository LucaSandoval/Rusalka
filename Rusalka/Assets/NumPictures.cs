using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NumPictures : Singleton<NumPictures>
{
    public delegate void PieceCollectedEvent();
    public event PieceCollectedEvent OnPieceCollected;

    private int numPieces = 0;

    public void AddPiece()
    {
        numPieces++;
        Debug.Log(getPieceCount() + "- PIECE ADDED!");
        OnPieceCollected?.Invoke();
    }

    public int getPieceCount()
    {
        return numPieces;
    }
}
