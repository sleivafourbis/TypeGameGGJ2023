using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [System.Serializable] 
// public class Word
// {
//     public int Id;
//     public string Texto;
//     public int Dificultad;
// }

public class Core : MonoBehaviour
{
    public string _palabraSeleccionada;
    public List<Word> Palabras;
    public List<Word> PalabrasAcutales;
    public int NivelDificultad = 1;
    public List<char> _palabra;
    void Start()
    {
        _palabra = new List<char>();
        PalabrasAcutales = Palabras.Where(c => c.Difficulty == NivelDificultad).ToList();
        var valorMinimo = PalabrasAcutales.Min(c => c.Id);
        var valorMaximo = PalabrasAcutales.Max(c => c.Id);
        var numeroRandom = Random.Range(valorMinimo, valorMaximo);
        _palabraSeleccionada = PalabrasAcutales.SingleOrDefault(c => c.Id == numeroRandom)!.Text;
        var arr = _palabraSeleccionada.ToCharArray();
        Debug.Log(arr[1]);
        foreach (var item in arr)
        {
            _palabra = _palabra.Append(item).ToList();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_palabra.Count > 0)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(_palabra[0].ToString().ToLower()))
                {
                    Debug.Log(_palabra[0].ToString());
                    _palabra.RemoveAt(0);
                }
                else
                {
                    Debug.Log("error");
                }
            }
        }
        else
        {
            Debug.Log("Terminado!");
        }
        
    }

    // public KeyCode GetKeycodeFromChar()
    // {
    //     switch (Input.G)
    //     {
    //         
    //     }
    // }
}
