using System;

namespace PhasmophobiaSaveEditor.Utils
{
    public static class PathUtil
    {
        public static string LogFile => "PhasmophobiaSaveEditor.log";
        public static string SaveFile => $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\LocalLow\Kinetic Games\Phasmophobia\saveData.txt";
    }
}