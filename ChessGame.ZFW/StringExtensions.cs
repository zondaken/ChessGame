using System.Text;

namespace ChessGame.ZFW;

public static class StringExtensions
{
    public static string RemoveChar(this string text, char c)
    {
        var sb = new StringBuilder();

        for(int i = 0; i < text.Length; i++)
        {
            if (text[i] == c) continue;
            sb.Append(text[i]);
        }

        return sb.ToString();
    }
}
