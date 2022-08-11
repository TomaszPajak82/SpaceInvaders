
namespace SpaceInvaders.Settings
{
    public interface IHighScoreSettings
    {
        string DisplayFormat { get; }

        string DisplayFullFormat { get; }

        int DisplayCount { get; }

        int StoredCount { get; }


    }
}