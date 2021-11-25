using System.Text;

namespace game.util
{

public static class StringMaker
{
    public static string Concatenate(string front, string back)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(front);
        sb.Append(back);
        return sb.ToString();
    }
}

}