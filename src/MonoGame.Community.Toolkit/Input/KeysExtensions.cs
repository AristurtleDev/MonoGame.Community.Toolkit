using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Provides extension methods for the <see cref="Keys"/> enumeration to enhance keyboard input handling.
/// </summary>
public static class KeysExtensions
{
    /// <summary>
    /// Converts a <see cref="Keys"/> value to its corresponding character representation, if applicable.
    /// </summary>
    /// <param name="key">The key to convert to a character.</param>
    /// <param name="modifiers">
    /// The keyboard modifier states to consider when converting the key. Defaults to <see cref="KeyboardModifiers.None"/>.
    /// </param>
    /// <returns>
    /// A <see cref="char"/> value representing the character associated with the key, or <see langword="null"/>
    /// if the key does not have a printable character representation.
    /// </returns>
    /// <remarks>
    /// This method converts keyboard keys to their corresponding character values, taking into account
    /// the state of modifier keys. The Shift modifier affects the output for applicable keys:
    /// <list type="bullet">
    /// <item><description>Letters (A-Z) are converted to uppercase when Shift is pressed, lowercase otherwise</description></item>
    /// <item><description>Number keys (0-9) produce their shifted symbols (e.g., Shift+1 = '!') when Shift is pressed</description></item>
    /// <item><description>Punctuation and symbol keys produce their shifted variants when applicable</description></item>
    /// <item><description>Numpad keys always produce their numeric values regardless of Shift state</description></item>
    /// <item><description>Special keys like Space, Tab, Enter, and Backspace produce their corresponding control characters</description></item>
    /// </list>
    /// Keys that do not have printable character representations (such as function keys, arrow keys, or modifier keys)
    /// will return <see langword="null"/>. This method uses US keyboard layout conventions for character mapping.
    /// </remarks>
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
