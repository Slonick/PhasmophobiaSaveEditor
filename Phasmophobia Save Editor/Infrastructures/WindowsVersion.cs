namespace PhasmophobiaSaveEditor.Infrastructures
{
    public enum WindowsVersion
    {
        Unsupported,
        Win7,
        Win8,
        Win81,

        /// <summary>
        ///     Threshold 1 — Initial Version (1507)
        /// </summary>
        Win10T1,

        /// <summary>
        ///     Threshold 2 — November Update (1511)
        /// </summary>
        Win10T2,

        /// <summary>
        ///     Redstone 1 — Anniversary Update (1607)
        /// </summary>
        Win10Rs1,

        /// <summary>
        ///     Redstone 2 — Creators Update (1703)
        /// </summary>
        Win10Rs2,

        /// <summary>
        ///     Redstone 3 — Fall Creators Update (1709)
        /// </summary>
        Win10Rs3,

        /// <summary>
        ///     Redstone 4 — April 2018 Update (1803)
        /// </summary>
        Win10Rs4,

        /// <summary>
        ///     Redstone 5 — October 2018 Update (1809)
        /// </summary>
        Win10Rs5,

        /// <summary>
        ///     19H1 — May 2019 Update (1903)
        /// </summary>
        Win1019H1,

        /// <summary>
        ///     19H2 (1909)
        /// </summary>
        Win1019H2,

        /// <summary>
        ///     Beta version
        /// </summary>
        Win10Beta
    }
}