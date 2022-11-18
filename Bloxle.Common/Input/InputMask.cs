namespace Bloxle.Common.Input
{
    public static class InputMask
    {
        public static readonly int[,] PlayerMask = new int[,] { { 0, 1, 0 },
                                                          { 1, 2, 1 },
                                                          { 0, 1, 0 } };

        public static readonly int[,] UndoMask = new int[,] { { 0, -1, 0 },
                                                          { -1, -2, -1 },
                                                          { 0, -1, 0 } };
    }
}