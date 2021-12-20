using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        Menu,
        DevTest,
    }

    public static void loadScene(Scene sc) {
        SceneManager.LoadScene(sc.ToString());
    }

}
