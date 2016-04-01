namespace PyriteClientIntefaces
{
    public interface ICustomChecker
    {
        bool IsCanDoNow { get; }

        string Name { get; }

        bool BeginUserSettings();

        bool AllowUserSettings { get; }

        void Refresh();
    }
}
