using System;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

public static class KeysExtensions
{
    public static char? ToChar(this Keys key, KeyboardModifiers modifiers = KeyboardModifiers.None)
    {
        bool isShiftDown = (modifiers & KeyboardModifiers.Shift) == KeyboardModifiers.Shift;

        return key switch
        {
            Keys.A => isShiftDown ? 'A' : 'a',
            Keys.B => isShiftDown ? 'B' : 'b',
            Keys.C => isShiftDown ? 'C' : 'c',
            Keys.D => isShiftDown ? 'D' : 'd',
            Keys.E => isShiftDown ? 'E' : 'e',
            Keys.F => isShiftDown ? 'F' : 'f',
            Keys.G => isShiftDown ? 'G' : 'g',
            Keys.H => isShiftDown ? 'H' : 'h',
            Keys.I => isShiftDown ? 'I' : 'i',
            Keys.J => isShiftDown ? 'J' : 'j',
            Keys.K => isShiftDown ? 'K' : 'k',
            Keys.L => isShiftDown ? 'L' : 'l',
            Keys.M => isShiftDown ? 'M' : 'm',
            Keys.N => isShiftDown ? 'N' : 'n',
            Keys.O => isShiftDown ? 'O' : 'o',
            Keys.P => isShiftDown ? 'P' : 'p',
            Keys.Q => isShiftDown ? 'Q' : 'q',
            Keys.R => isShiftDown ? 'R' : 'r',
            Keys.S => isShiftDown ? 'S' : 's',
            Keys.T => isShiftDown ? 'T' : 't',
            Keys.U => isShiftDown ? 'U' : 'u',
            Keys.V => isShiftDown ? 'V' : 'v',
            Keys.W => isShiftDown ? 'W' : 'w',
            Keys.X => isShiftDown ? 'X' : 'x',
            Keys.Y => isShiftDown ? 'Y' : 'y',
            Keys.Z => isShiftDown ? 'Z' : 'z',

            Keys.D0 => isShiftDown ? ')' : '0',
            Keys.D1 => isShiftDown ? '!' : '1',
            Keys.D2 => isShiftDown ? '@' : '2',
            Keys.D3 => isShiftDown ? '#' : '3',
            Keys.D4 => isShiftDown ? '$' : '4',
            Keys.D5 => isShiftDown ? '%' : '5',
            Keys.D6 => isShiftDown ? '^' : '6',
            Keys.D7 => isShiftDown ? '&' : '7',
            Keys.D8 => isShiftDown ? '*' : '8',
            Keys.D9 => isShiftDown ? '(' : '9',

            Keys.NumPad0 => '0',
            Keys.NumPad1 => '1',
            Keys.NumPad2 => '2',
            Keys.NumPad3 => '3',
            Keys.NumPad4 => '4',
            Keys.NumPad5 => '5',
            Keys.NumPad6 => '6',
            Keys.NumPad7 => '7',
            Keys.NumPad8 => '8',
            Keys.NumPad9 => '9',

            Keys.Space => ' ',
            Keys.Tab => '\t',
            Keys.Enter => '\r',
            Keys.Back => '\b',

            Keys.Add => '+',
            Keys.Subtract => '-',
            Keys.Multiply => '*',
            Keys.Divide => '/',
            Keys.Decimal => '.',

            Keys.OemBackslash => '\\',
            Keys.OemComma => isShiftDown ? '<' : ',',
            Keys.OemOpenBrackets => isShiftDown ? '{' : '[',
            Keys.OemCloseBrackets => isShiftDown ? '}' : ']',
            Keys.OemPeriod => isShiftDown ? '>' : '.',
            Keys.OemPipe => isShiftDown ? '|' : '\\',
            Keys.OemPlus => isShiftDown ? '+' : '=',
            Keys.OemMinus => isShiftDown ? '_' : '-',
            Keys.OemQuestion => isShiftDown ? '?' : '/',
            Keys.OemQuotes => isShiftDown ? '"' : '\'',
            Keys.OemSemicolon => isShiftDown ? ':' : ';',
            Keys.OemTilde => isShiftDown ? '~' : '`',

            _ => null
        };
    }
}
