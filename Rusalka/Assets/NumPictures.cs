using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPictures : MonoBehaviour
{
    private int numPieces = 0;

    public void AddPiece()
    {
        numPieces++;
        Debug.Log(getPieceCount() + "- PIECE ADDED!");
    }

    public int getPieceCount()
    {
        return numPieces;
    }
}
