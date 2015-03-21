namespace Chess.Core.Authentication
{
    public interface ISaltGenerator
    {
        string GenerateSalt(int length = 64);
    }
}
