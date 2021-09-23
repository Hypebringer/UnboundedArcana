using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnboundedArcana.Extensions
{
    internal static class ReflectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsType<T>(this object self) => self is T;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotType<T>(this object self) => !IsType<T>(self);
    }
}
