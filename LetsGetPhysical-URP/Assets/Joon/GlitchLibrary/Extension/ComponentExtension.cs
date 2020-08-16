using UnityEngine;
using System;
using System.Collections;


//https://answers.unity.com/questions/530178/how-to-get-a-component-from-an-object-and-add-it-t.html

// Usage:
// var copy = myComp.GetCopyOf(someOtherComponent);

public static class ComponentExtension {
    public static IEnumerator WaitAndDo (this Component component, float time, Action action) {
        yield return new WaitForSeconds (time);
        action();
    }
}
    