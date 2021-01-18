using UnityEngine;

public class MultiBind : Mod
{
    static HarmonyLib.Traverse consoleSender = HarmonyLib.Traverse.Create(RConsole.instance);
    public void Start()
    {
        Debug.Log("Mod MultiBind has been loaded!");
    }

    [ConsoleCommand(name: "multi", docs: "Syntax: 'multi <command...> [& [command...]] [& ...]' Executes multiple commands consecutivly. (Use & as delimiter)")]
    public static string MyCommand(string[] args)
    {
        if (args.Length == 0)
            return "Requires commands";
        string str = "";
        foreach (string arg in args)
        {
            if (arg == "&")
            {
                runCommand(str);
                str = "";
            }
            else
                str += (str == "" ? "" : " ") + arg;
        }
        runCommand(str);
        return "";
    }

    [ConsoleCommand(name: "mb", docs: "Syntax: 'mb <key> <command...> [& [command...]] [& ...]' Binds multiple commands to a single keybind. (Use & as delimiter)")]
    public static string MyCommand2(string[] args)
    {
        if (args.Length <= 1)
            return "Not enough parameters";
        string str = "";
        for (int i = 1; i < args.Length;i++)
        {
            str += " " + args[i];
        }
        runCommand("bind " + args[0] + " multi" + str);
        return "";
    }

    public void OnModUnload()
    {
        Debug.Log("Mod MultiBind has been unloaded!");
    }

    public static void runCommand(string cmd)
    {
        if (cmd != "")
            consoleSender.Method("SilentlyRunCommand", new System.Type[] { typeof(string) }, new object[] { cmd}).GetValue();
    }
}