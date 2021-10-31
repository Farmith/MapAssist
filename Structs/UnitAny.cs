/**
 *   Copyright (C) 2021 okaygo
 *
 *   https://github.com/misterokaygo/MapAssist/
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 **/

using System;
using System.Runtime.InteropServices;

namespace MapAssist.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    unsafe public struct UnitAny
    {
        [FieldOffset(0x0)] public uint UnitType;
        [FieldOffset(0x4)] public uint TxtFileNo;
        [FieldOffset(0x8)] public uint UnitId;
        [FieldOffset(0xC)] public uint Mode;
        [FieldOffset(0x10)] public IntPtr UnitData;
        [FieldOffset(0x10)] public IntPtr IsPlayer;
        [FieldOffset(0x20)] public Act* pAct;
        [FieldOffset(0x38)] public Path* pPath;
        [FieldOffset(0x90)] public IntPtr Stats;
        [FieldOffset(0xB8)] public uint OwnerType; // ?
        [FieldOffset(0xC4)] public ushort X;
        [FieldOffset(0xC6)] public ushort Y;
        [FieldOffset(0x158)] public UnitAny* pUnitNext;

        public override bool Equals(object obj) => obj is UnitAny other && Equals(other);

        public bool Equals(UnitAny p) => UnitId == p.UnitId;

        public override int GetHashCode() => (X, Y).GetHashCode();

        public static bool operator ==(UnitAny lhs, UnitAny rhs) => lhs.Equals(rhs);

        public static bool operator !=(UnitAny lhs, UnitAny rhs) => !(lhs == rhs);
    }
}
