using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCommand : ICommand
{
    private readonly GameObject _cube;
    private readonly Color _color;

    private Material _cubeMaterial;
    private Color _previousColor;

    public ClickCommand(GameObject cube, Color color)
    {
        _cube = cube;
        _color = color;

        if (_cube != null)
        {
            _cubeMaterial = _cube.GetComponent<MeshRenderer>().material;
        }
    }

    public void Execute()
    {
        if (_cubeMaterial != null)
        {
            //store previous color
            _previousColor = _cubeMaterial.color;

            //assign new color
            _cubeMaterial.color = _color;
        }
    }

    public void Undo()
    {
        _cubeMaterial.color = _previousColor;
    }    
}
