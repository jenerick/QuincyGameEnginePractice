﻿using Microsoft.Xna.Framework;

namespace QuincyGameEnginePractice.EngineCode
{
    public static class Vector2Extensions
    {
        public static Vector2 Up(this Vector2 vec)
        {
            return -Vector2.UnitY;
        }

        public static Vector2 Down(this Vector2 vec)
        {
            return Vector2.UnitY;
        }

        public static Vector2 Left(this Vector2 vec)
        {
            return -Vector2.UnitX;
        }

        public static Vector2 Right(this Vector2 vec)
        {
            return Vector2.UnitX;
        }
    }
}
