using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public interface IState<T>
{
    void OnEnter(T t);
    void OnExecute(T t);
    void OnExit(T t);


}
